using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MicroSCADAStudio.Src.EnvironmentDesigner
{
    /*!
     * Lista dos aquivos recentes
     */
    public class CRecentFiles
    {
        private ToolStripMenuItem m_menuItem;//!< Menu raiz
        private String fileName;
        private List<string> fileList;
        private const int MAX_FILE_COUNT = 10;
        /*!
         * Construtor
         * @param MenuItem Referencia para menu raiz.
         */
        public CRecentFiles(ToolStripMenuItem MenuItem)
        {
            this.m_menuItem = MenuItem;
            this.fileList = new List<string>();            
            this.fileName = AppDomain.CurrentDomain.BaseDirectory+"RecentFilesList.xml";
        }
        public CRecentFiles()
        {
            this.m_menuItem = null;
            this.fileList = new List<string>();
            this.fileName = Application.StartupPath + "\\RecentFilesList.xml";
        }
        //! Ponteiro para evento OnClick dos submenus
        public EventHandler SubMenuItemClick;
        //! Lista de arquivos
        public string[] LastFiles
        {
            get { return fileList.ToArray(); }
        }
        /*!
         * Abre lista de ultimos arquivos e monta menu
         */
        public void Open()
        {
            if (System.IO.File.Exists(fileName))
            {
                XmlDocument xmlDocument;
                xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                XmlNode xmlNode1 = xmlDocument.DocumentElement;
                for (int i = 0; i < xmlNode1.ChildNodes.Count; i++)
                {
                    String path = xmlNode1.ChildNodes[i].InnerText;
                    fileList.Add(path);
                    ToolStripMenuItem subMenuItem = new ToolStripMenuItem();
                    subMenuItem.Click += new EventHandler(this.SubMenuItemClick);
                    subMenuItem.Text = path;
                    m_menuItem.DropDownItems.Add(subMenuItem);
                }
            }
        }
        public void Open(ToolStripMenuItem MenuItem)
        {
            this.m_menuItem = MenuItem;
            if (System.IO.File.Exists(fileName))
            {
                XmlDocument xmlDocument;
                xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                XmlNode xmlNode1 = xmlDocument.DocumentElement;
                for (int i = 0; i < xmlNode1.ChildNodes.Count; i++)
                {
                    String path = xmlNode1.ChildNodes[i].InnerText;
                    fileList.Add(path);
                    ToolStripMenuItem subMenuItem = new ToolStripMenuItem();
                    subMenuItem.Click += new EventHandler(this.SubMenuItemClick);
                    subMenuItem.Text = path;
                    m_menuItem.DropDownItems.Add(subMenuItem);
                }
            }
        }
        /*!
         * Salva lista
         */
        public void Save()
        {
            XmlDocument xmlDocument;            
            XmlNode rootNode, xmlNode1;
            XmlDeclaration xmldecl;

            xmlDocument = new XmlDocument();
            xmldecl = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.AppendChild(xmldecl);
            //
            rootNode = xmlDocument.CreateNode(XmlNodeType.Element, "RecentFiles", "");
            xmlDocument.AppendChild(rootNode);
            //
            for (int i = 0; i < fileList.Count; i++)
            {
                xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "File", "");
                rootNode.AppendChild(xmlNode1);                
                xmlNode1.InnerText = fileList[i];
            }
            xmlDocument.Save(fileName);
        }
        /*!
         * Inseri novo path na lista.
         * @param fileName Nome do arquivo aberto
         */
        public void Add(String fileName)
        {
            if (fileList.Count >= MAX_FILE_COUNT)
                fileList.RemoveAt(fileList.Count - 1);
            //
            int index = fileList.IndexOf(fileName);
            if (index > -1)            
                fileList.RemoveAt(index);
            //
            fileList.Insert(0, fileName);
            //
            Save();
        }
        /*!
         * Retorna ultimo arquivo aberto
         */
        public string GetLastFile()
        {
            if (fileList.Count > 0)
                return fileList[0];
            else
                return "";
        }

        
    }
}
