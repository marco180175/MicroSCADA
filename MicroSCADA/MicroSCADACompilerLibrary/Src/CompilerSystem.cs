using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADACompilerLibrary.Src
{
    public abstract class CCompilerSystem : CCompilerObject, ICustomSystem
    {
        private CCustomSystem customSystem;
        protected CCompilerProject project;
        public CCompilerSystem(object AOwner,CCompilerProject Project)
            : base(AOwner)
        {
            customSystem = new CCustomSystem();
            project = Project;
        }

        public CReferenceList ReferenceList
        {
            get { return this.customSystem.referenceList; }
        }
        /*!
         * 
         */
        public void SetOpenName(string Value)
        {
            this.customObject.name = Value;
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
