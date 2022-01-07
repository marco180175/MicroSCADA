using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeSystem : CRuntimeObject, ICustomSystem
    {
        protected CCustomSystem customSystem;        
        protected CRuntimeProject project;        
        
        public CRuntimeSystem(Object AOwner, CRuntimeProject Project)
            : base(AOwner)
        {
            this.customSystem = new CCustomSystem();            
            this.project = Project;
        }
        
        /*!
         * 
         */
        public void SetOpenName(string Value)
        {
            this.customObject.name = Value;            
        }
                
        public CReferenceList ReferenceList
        {
            get { return this.customSystem.referenceList; }
        }

        public void SetReference(int Index, Object Value)
        {
            if ((Index >= 0) && (Index < ReferenceList.Count))
            {
                CReferenceItem item = ReferenceList[Index];
                item.Reference = Value;
            }
        }

        public Object GetReference(int Index)
        {
            if ((Index >= 0) && (Index < ReferenceList.Count))
            {
                CReferenceItem item = ReferenceList[Index];
                return item.Reference;
            }
            else
                return null;
        }
        
        public void SetReferenceGuid(int Index, Guid Value)
        {
            if ((Index >= 0) && (Index < ReferenceList.Count))
            {
                CReferenceItem item = ReferenceList[Index];
                item.GUID = Value;
            }
        }

        public Guid GetReferenceGuid(int Index)
        {
            if ((Index >= 0) && (Index < ReferenceList.Count))
            {
                CReferenceItem item = ReferenceList[Index];
                return item.GUID;
            }
            else
                return Guid.Empty;
        }
    }
}
