using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src.IOFiles;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * 
     */
    public delegate void UndoDelegate(CDesignCustomScreen Screen, string ObjectString);
    /*!
     * 
     */
    public class CCommand : IDisposable
    {
        private StreamWriter m_writer;
        private StreamReader m_reader;
        private MemoryStream m_memory;
        private CDesignCustomScreen r_screen;
        private event UndoDelegate m_function;
        private UndoDelegate r_function;
        /*!
         * 
         */
        public CCommand(CDesignCustomScreen Screen, UndoDelegate Function)
        {
            m_memory = new MemoryStream();
            m_writer = new StreamWriter(m_memory);
            m_reader = new StreamReader(m_memory);
            r_screen = Screen;
            r_function = Function;
            if (Function != null)
                m_function += new UndoDelegate(Function);
        }
        /*!
         * Destrutor
         */
        public void Dispose()
        {
            if (r_function != null)
                m_function -= r_function;
            m_reader.Dispose();
            m_writer.Dispose();
            m_reader.Close();
            m_reader.Dispose();
        }
        /*!
         * 
         */
        public void SaveToMemory(string ObjectString)
        {
            //salva xml            
            m_memory.Position = 0;
            m_writer.Write(ObjectString);
            m_writer.Flush();
        }
        /*!
         * 
         */
        public string OpenFromMemory()
        {
            m_memory.Position = 0;
            return m_reader.ReadToEnd();            
        }
        /*!
         * 
         */
        public void Undo(CDesignCustomScreen Screen)
        {
            m_memory.Position = 0;
            string objectString = m_reader.ReadToEnd();
            if ((m_function != null) && (r_screen == Screen))
                m_function(Screen, objectString);
        }
    }
    /*!
     * 
     */
    public class CDesignEditCommandManager : IDisposable
    {
        private Stack<CCommand> m_undoStack;//!< Pilha de commands que suportan undo
        private CCommand m_lastCommand;//!< Ultimo comando executado
        /*!
         * Construtor
         */
        public CDesignEditCommandManager()
        {
            m_undoStack = new Stack<CCommand>();
            m_lastCommand = null;
        }
        /*!
         * Destrutor
         */
        public void Dispose()
        {
            while (m_undoStack.Count > 0)
            {
                CCommand command = m_undoStack.Pop();
                command.Dispose();
            }
        }
        #region Funçoes Copy,Paste,Cut e Delete
        /*!
         * 
         */
        public void Copy(CDesignCustomScreen Screen, ArrayList ObjectList)
        {
            //copia/salva
            CCommand command = new CCommand(Screen, null);
            CSaveToXML saveToXML = new CSaveToXML();
            string objectString = saveToXML.SaveScreenObjects(ObjectList);
            command.SaveToMemory(objectString);
            m_lastCommand = command;
        }
        /*!
         * 
         */
        public void Cut(CDesignCustomScreen Screen, ArrayList ObjectList)
        {
            //copia/salva
            CCommand command = new CCommand(Screen, null);
            CSaveToXML saveToXML = new CSaveToXML();
            string objectString = saveToXML.SaveScreenObjects(ObjectList);
            command.SaveToMemory(objectString);
            m_lastCommand = command;
            //deleta 
            foreach (CDesignScreenObject obj in ObjectList)
                obj.Dispose();
        }
        /*!
         * 
         */
        public void Paste(CDesignCustomScreen Screen)
        {
            if (m_lastCommand != null)
            {
                COpenFromXML openFromXML = new COpenFromXML();
                string objectString = m_lastCommand.OpenFromMemory();
                //
                ArrayList list = openFromXML.OpenScreenObjects(Screen, objectString);
                //
                OnPasteCommand(new PasteCommandEventArgs(list));                
            }            
        }
        /*!
         * 
         */
        public void Delete(CDesignCustomScreen Screen, ArrayList ObjectList)
        {
            //salva
            CCommand command = new CCommand(Screen, UndoDelete);
            CSaveToXML saveToXML = new CSaveToXML();
            string objectString = saveToXML.SaveScreenObjects(ObjectList);
            command.SaveToMemory(objectString);
            //pilha
            m_undoStack.Push(command);
            //deleta
            foreach (CDesignScreenObject obj in ObjectList)
                obj.Dispose();
            //fim
        }
        #endregion
        
        #region Funçoes de Undo
        /*!
         * 
         */
        public void Undo(CDesignCustomScreen Screen)
        {
            if (m_undoStack.Count > 0)
            {
                CCommand command = m_undoStack.Pop();
                command.Undo(Screen);
                command.Dispose();
            }
            else
            {
                MessageBox.Show(Screen.getForm(),"Undo log is empty!");
            }
        }
        /*!
         * Undo para comando paste
         * Valido apenas na mesma tela
         */
        private void UndoPaste(CDesignCustomScreen Screen, string ObjectString)
        {
            //deleta objetos colados
            //foreach (CDesignScreenObject obj in ObjectList)
            //    obj.Dispose();         
        }
        /*!
         * Undo para comando delete
         * Valido apenas na mesma tela
         */
        private void UndoDelete(CDesignCustomScreen Screen, string ObjectString)
        {
            COpenFromXML openFromXML = new COpenFromXML();
            openFromXML.OpenScreenObjects(Screen, ObjectString);
        }
        #endregion
        //!
        public event PasteCommandEventHandler PasteCommand;
        /*!
         * 
         */
        private void OnPasteCommand(PasteCommandEventArgs e)
        {
            if (PasteCommand != null)
                PasteCommand(this, e);
        }
    }


    public class PasteCommandEventArgs : EventArgs
    {
        private ArrayList m_objectList;
        public PasteCommandEventArgs(ArrayList ObjectList)
        {
            m_objectList = ObjectList;
        }
        public ArrayList ObjectList { get { return m_objectList; } }
    }

    public delegate void PasteCommandEventHandler(object sender,PasteCommandEventArgs e);
}
