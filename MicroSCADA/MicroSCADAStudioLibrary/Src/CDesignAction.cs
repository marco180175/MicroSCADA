using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src
{
    /*!
     * Linha de codigo das actions. Cada action podera ter uma ou mais
     * linhas de codigo.
     */
    public class CDesignCodeLine : CDesignSystem, ICustomCodeLine
    {
        private CCustomCodeLine m_customCodeLine;       
        private CDesignActionList m_actionList;
        /*!
         * Construtor
         */
        public CDesignCodeLine(Object AOwner, CDesignProject Project,CDesignActionList ActionList)
            : base(AOwner, Project)
        {
            this.m_customCodeLine = new CCustomCodeLine();
            this.m_actionList = ActionList;
        }
        #region Propriedades
        //!
        public CCustomActionCode Opcode
        {
            get { return this.m_customCodeLine.opcode; }            
        }
        //!
        public ArrayList ParamList
        {
            get { return this.GetParamList(); }
        }
        //!
        public int ParamCount
        {
            get { return this.GetParamCount(); }
        }
        
        //!
        public string Code
        {
            get { return this.GetCode(); }
        }
        #endregion
        /*!
         * 
         */
        public void SetOpcode(CCustomActionCode Value)
        {
            this.m_customCodeLine.opcode = Value;
        }
        /*!
         * 
         */
        public void AddParam(object Value)
        {
            int index = ReferenceList.AddReference();
            m_customCodeLine.indexOperand.Add(index);
            SetReference(index, Value);
        }
        /*!
         * 
         */
        public void SetGuidParam(Guid Value)
        {
            int index = ReferenceList.AddReference();
            m_customCodeLine.indexOperand.Add(index);
            SetReferenceGuid(index, Value);            
        }
        /*!
         * 
         */
        private ArrayList GetParamList()
        {
            ArrayList arl = new ArrayList();

            foreach (CReferenceItem item in ReferenceList)
            {
                arl.Add(item.Reference);
            }
            return arl;
        }
        /*!
         * 
         */
        private String GetCode()
        {
            switch (m_customCodeLine.opcode)
            {
                case CCustomActionCode.NextScreen:
                case CCustomActionCode.PrevScreen:
                    {
                        string s = m_actionList.Dictionary[m_customCodeLine.opcode];
                        return s;
                    }
                case CCustomActionCode.ShowScreen:
                    {
                        CDesignScreen screen = (CDesignScreen)ParamList[0];
                        string fs = m_actionList.Dictionary[m_customCodeLine.opcode];
                        return string.Format(fs, screen.Name);
                    }
                case CCustomActionCode.ShowPopup:
                    {
                        CDesignPopupScreen screen = (CDesignPopupScreen)ParamList[0];
                        string fs = m_actionList.Dictionary[m_customCodeLine.opcode];
                        return string.Format(fs, screen.Name);
                    }
                default:
                    return "";
            }
        }
        /*!
         * 
         */
        private int GetParamCount()
        {
            switch (m_customCodeLine.opcode)
            {
                case CCustomActionCode.NextScreen:
                case CCustomActionCode.PrevScreen:
                    return 0;
                case CCustomActionCode.ShowPopup:
                case CCustomActionCode.ShowScreen:
                    return 1;
                default:
                    return 0;
            }
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            Object obj;
            for (int i = 0; i < m_customCodeLine.indexOperand.Count; i++)
            {
                int index = m_customCodeLine.indexOperand[i];
                Guid guid = GetReferenceGuid(index);
                //
                if (CHashObjects.ObjectDictionary.ContainsKey(guid))
                    obj = CHashObjects.ObjectDictionary[guid];
                else
                    obj = null;
                //
                SetReference(index, obj);
            }
        }
    }
    /*!
     * 
     * 
     */
    public class CDesignAction : CDesignSystem, ICustomAction
    {
        private CDesignActionList m_actionList;
        public CDesignAction(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.m_actionList = (CDesignActionList)AOwner;
            this.imageIndex = 45;
        }
        //!
        [ReadOnly(true)]
        public string[] Lines
        {
            get { return this.GetCodeLines(); }
        }
        //!
        public int LineCount
        {
            get { return this.ObjectList.Count; }
        }
        /*!
         * 
         */
        public ICustomCodeLine NewCodeLine()
        {
            CDesignCodeLine codeLine = new CDesignCodeLine(this, project, m_actionList);
            ObjectList.Add(codeLine);
            return codeLine;
        }
        /*!
         * 
         */
        public CDesignCodeLine NewCodeLineEx(CCustomActionCode OpCode)
        {
            CDesignCodeLine codeLine = (CDesignCodeLine)NewCodeLine();
            codeLine.SetOpcode(OpCode);            
            return codeLine;
        }
        /*!
         * 
         */
        private string[] GetCodeLines()
        {
            List<string> lines = new List<string>();
            foreach (CDesignCodeLine codeLine in ObjectList)
                lines.Add(codeLine.Code);
            return lines.ToArray();
        }
        /*!
         * 
         */
        public override string ToString()
        {
            return Name;            
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            foreach (CDesignCodeLine line in ObjectList)
                line.LinkObjects();
        }
    }

    
}
