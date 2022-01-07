using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using MicroSCADACustomLibrary;

namespace MicroSCADAStudioLibrary.Src
{
    /*!
     * Super classe para todos os objetos do projeto.
     */
    public abstract class CDesignObject : ICustomObject
    {
        protected CCustomObject customObject;//!< Objeto com campos comuns             
        protected Boolean canDeleteByUser;//!< Permite ser deletado pelo usuário
        protected int imageIndex;//!< Indice do icone        
        /*!
         * Construtor
         * @param AOwner Referencia para objeto proprietario         
         */
        public CDesignObject(Object AOwner)
        {
            this.customObject = new CCustomObject();
            this.customObject.owner = AOwner;                        
            this.canDeleteByUser = true;
            this.imageIndex = -1;
        }
        /*!
         * Destrutor implicito (sera chamado por GC)
         */
        //~CDesignObject()
        //{
        //    Dispose();
        //}
        /*!
         * Destrutor explicito
         */
        public virtual void Dispose()
        {
            CDesignObject desObj;
            //Remove proprio ponteiro da lista do objeto pai
            if (Owner != null)
            {
                desObj = (CDesignObject)Owner;
                desObj.ObjectList.Remove(this);
            }
            //Destroy objetos filhos
            while (ObjectList.Count > 0)
            {
                desObj = (CDesignObject)ObjectList[ObjectList.Count-1];
                desObj.Dispose();
            }            
        }

        //public abstract void Delete();
        
        #region Propriedades
        //! Propriedade Nome do objeto
        [Category("Accessibility")]
        public String Name
        {
            get { return this.customObject.name; }
            set { this.customObject.name = value; }
        }
        //! Propriedade Descrição
        [Category("Accessibility")]
        public String Description
        {
            get { return this.customObject.description; }
            set { this.customObject.description = value; }
        }
        //! Nome completo do objeto
        [Browsable(false)]
        public String FullName
        {
            get { return CCustomObject.GetFullName(customObject.name, customObject.owner); }
        }
        //! Identificador unico do objeto
        [Browsable(true)]
        public Guid GUID
        {
            get { return this.customObject.guid; }
        }
        //! Lista de objetos filhos
        [Browsable(false)]
        public ArrayList ObjectList
        {
            get { return this.customObject.objectList; }
        }
        //! Referencia para objeto pai
        [Browsable(false)]
        public Object Owner
        {
            get { return this.customObject.owner; }
        }
        //! Verdadeiro se objeto pode ser deletado pelo usuário
        [Browsable(false)]
        public Boolean CanDeleteByUser
        {
            get { return this.canDeleteByUser; }
        }
        //! Indice do icone do objeto no imageList
        [Browsable(true)]
        public int ImageIndex
        {
            get { return this.imageIndex; }
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
        /*!
         * Linka projeto 
         */
        public virtual void LinkObjects() { }
    }
}
