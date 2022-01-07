using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src
{
    /*!
     * Linha de codigo das actions. Cada action podera ter uma ou mais
     * linhas de codigo.
     */
    public class CRuntimeCodeLine : CRuntimeSystem, ICustomCodeLine
    {
        private CCustomCodeLine m_customCodeLine;  
        public CRuntimeCodeLine(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.m_customCodeLine = new CCustomCodeLine(); 
        }
        #region Propriedades
        //!
        public CCustomActionCode Opcode
        {
            get { return m_customCodeLine.opcode; }
        }
        //!
        public ArrayList ParamList
        {
            get { return GetParamList(); }
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
                arl.Add(item.Reference);            
            return arl;
        }
        /*!
         * 
         */
        public void LinkObjects()
        {
            Object obj;
            for (int i = 0; i < m_customCodeLine.indexOperand.Count; i++)
            {
                int index = m_customCodeLine.indexOperand[i];
                Guid guid = GetReferenceGuid(index);
                if (CHashObjects.ObjectDictionary.ContainsKey(guid))
                    obj = CHashObjects.ObjectDictionary[guid];
                else
                    obj = null;                
                SetReference(index, obj);                
            }
        }
    }

    public class CRuntimeAction : CRuntimeSystem, ICustomAction
    {        
        
        public CRuntimeAction(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            
        }
        /*!
         * 
         */
        public ICustomCodeLine NewCodeLine()
        {
            CRuntimeCodeLine line = new CRuntimeCodeLine(this, project);
            ObjectList.Add(line);
            return line;
        }
        /*!
         * 
         */
        public void LinkObjects()
        {
            foreach (CRuntimeCodeLine line in ObjectList)
                line.LinkObjects();
        }
    }

    
}
