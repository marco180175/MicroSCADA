using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary;

namespace MicroSCADARuntimeLibrary.Src
{
    public abstract class CRuntimeObject : ICustomObject, IDisposable
    {
        protected CCustomObject customObject;       
        public CRuntimeObject(Object AOwner)
        {
            this.customObject = new CCustomObject();
            this.customObject.owner = AOwner;                        
        }
        /*!
         * Destrutor implicito (sera chamado por gc)
         */
        ~CRuntimeObject()
        {
            Dispose();
        }
        /*!
        * Destrutor explicito
        */
        public virtual void Dispose()
        {

        }

        public String Name
        {
            get { return this.customObject.name; }
            set { this.customObject.name = value; }
        }
        public Guid GUID
        {
            get { return this.customObject.guid; }
        }
        public String FullName
        {
            get { return CCustomObject.GetFullName(customObject.name, customObject.owner); }
        }
        public String Description
        {
            get { return this.customObject.description; }
            set { this.customObject.description = value; }
        }
        public ArrayList ObjectList
        {
            get { return this.customObject.objectList; }
        }
        public Object Owner
        {
            get { return this.customObject.owner; }
        }        
        
        /*!
         * Seta identificador unico do objeto
         * @param Value Valor do identificador
         */
        public void SetGUID(Guid Value)
        {
            this.customObject.guid = Value;
        }
    }
}
