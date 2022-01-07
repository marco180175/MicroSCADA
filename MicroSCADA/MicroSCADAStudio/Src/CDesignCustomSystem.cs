using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary;

namespace MicroSCADAStudio.Src
{
    public abstract class CDesignCustomSystem : CDesignObject, ICustomSystem
    {
        protected CDesignProject project;
        protected CCustomSystem customSystem;
        protected Guid guid;
        static protected Hashtable hashTable;
        /*!
         * Construtor                  
         */
        public CDesignCustomSystem(Object AOwner, CDesignProject Project)
            : base(AOwner)
        {
            this.customSystem = new CCustomSystem();
            this.project = Project;
            hashTable = new Hashtable();
        }
        //Destrutor
        ~CDesignCustomSystem()
        {
        }
        public CCustomSystem CustomSystem
        {
            get { return this.customSystem; }
        }
        public Guid GUID
        {
            get { return this.guid; }
        }
        public void SetGUID(Guid Value)
        {
            guid = Value;
            hashTable.Add(guid, this);
        }
        [Browsable(false)]
        public ArrayList CrossReference
        {
            get { return this.customSystem.crossReferenceList; }
        }
        [Browsable(false)]
        public ArrayList ReferenceList 
        {
            get { return this.customSystem.referenceList; } 
        }
        public void SetReference(int Index, Object Value)
        {
            customSystem.SetReference(this, Index, Value);
        }
        public Object GetReference(int Index)
        {
            return customSystem.GetReference(Index);
        }
        public void SetReferenceName(int Index, String Value)
        {
            customSystem.SetReferenceName(Index, Value);
        }
        public String GetReferenceName(int Index)
        {
            return customSystem.GetReferenceName(Index);
        }
        
    }
}
