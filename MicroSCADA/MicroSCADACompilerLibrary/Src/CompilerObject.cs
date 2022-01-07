using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary;

namespace MicroSCADACompilerLibrary.Src
{
    public abstract class CCompilerObject : ICustomObject
    {
        protected CCustomObject customObject;
        /*!
         * Construtor
         */
        public CCompilerObject(object AOwner)
        {
            customObject = new CCustomObject();
            customObject.owner = AOwner;
        }
        /*!
         * Destrutor explicito
         */
        public virtual void Dispose()
        {
            CCompilerObject desObj;
            //Remove proprio ponteiro da lista do objeto pai
            if (Owner != null)
            {
                desObj = (CCompilerObject)Owner;
                desObj.ObjectList.Remove(this);
            }
            //Destroy objetos filhos
            while (ObjectList.Count > 0)
            {
                desObj = (CCompilerObject)ObjectList[ObjectList.Count - 1];
                desObj.Dispose();
            }
        }
        #region Propriedades
        //! Propriedade Nome do objeto        
        public String Name
        {
            get { return this.customObject.name; }
            set { this.customObject.name = value; }
        }
        //! Propriedade Descrição        
        public String Description
        {
            get { return this.customObject.description; }
            set { this.customObject.description = value; }
        }
        //! Nome completo do objeto        
        public String FullName
        {
            get { return CCustomObject.GetFullName(customObject.name, customObject.owner); }
        }
        //! Identificador unico do objeto        
        public Guid GUID
        {
            get { return this.customObject.guid; }
        }
        //! Lista de objetos filhos        
        public ArrayList ObjectList
        {
            get { return this.customObject.objectList; }
        }
        //! Referencia para objeto pai        
        public object Owner
        {
            get { return this.customObject.owner; }
        }        
        #endregion
        /*!
         * Seta identificador unico do objeto
         * @param Value Valor do identificador
         */
        public virtual void SetGUID(Guid Value)
        {
            this.customObject.guid = Value;
        }
    }
}
