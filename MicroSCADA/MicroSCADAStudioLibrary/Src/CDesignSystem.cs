using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudioLibrary.Src
{    
    /*!
     * Super classe para todos os objetos que serão mostrados no treeview.
     * Implementa a interface ITreeApater
     */
    public abstract class CDesignSystem : CDesignObject, ICustomSystem, ITreeAdapter
    {
        protected CCustomSystem customSystem;//!< Objeto com campos comuns     
        protected ArrayList crossReferenceList;//!< Referencia cruzada para o objeto          
        protected CDesignProject project;//!< Referencia para o objeto project        
        protected bool selected;        
        /*!
         * Construtor                  
         * @param AOwner Referencia para objeto proprietario
         * @param Project Referencia para projeto
         */
        public CDesignSystem(Object AOwner, CDesignProject Project)
            : base(AOwner)
        {
            this.customSystem = new CCustomSystem();
            this.crossReferenceList = new ArrayList();          
            this.project = Project;            
        }        
        /*!
         * Destrutor implicito (sera chamado por GC)
         */
        //~CDesignSystem()
        //{
        //    Dispose();
        //}
        /*!
         * Destrutor explicito
         * Gera evento OnDelItem para remover da IDE o proprio ponteiro
         * Chama evento da classe base para remover todas as refencias
         * Resolve referencia cruzada
         */
        public override void Dispose()
        {
            //
            OnDelItem(EventArgs.Empty);
            //
            ClearReference();
            //
            ClearCrossReference();
            //
            customSystem.Dispose();            
            //
            base.Dispose();
        }                
        #region Propriedades
        //! Nome do objeto
        [Category("Accessibility")]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        //! Lista de referencia cruzada
        [Browsable(false)]
        public ArrayList CrossReferenceList
        {
            get { return this.crossReferenceList; }
        }
        //! Lista de referencia direta
        [Browsable(false)]
        public CReferenceList ReferenceList 
        {
            get { return this.customSystem.referenceList; } 
        }
        //! 
        [Browsable(false)]
        public virtual bool Selected
        {
            get { return this.selected; }
            set { this.SetSelected(value); }
        }
        #endregion
        #region Funções
        /*!
         *  Remove proprio ponteiro da referecia do objeto na referencia cruzada
         */
        private void ClearCrossReference()
        {
            foreach (CDesignSystem designSystem in CrossReferenceList)
            {
                CReferenceItem[] Objects;
                Objects = (CReferenceItem[])designSystem.ReferenceList.ToArray();
                var result = (from obj in Objects select obj).Where(obj => (obj.GUID == this.GUID));
                if (result.Count() > 0)
                {
                    CReferenceItem item = (CReferenceItem)result.ElementAt(0);
                    item.Reference = null;
                    item.GUID = Guid.Empty;
                }
            }
            CrossReferenceList.Clear();
        }
        /*!
         * Remove proprio ponteiro na referencia cruzada da referecia do objeto 
         */
        private void ClearReference()
        {
            foreach (CReferenceItem item in ReferenceList)
            {
                if (item.Reference != null)
                {
                    CDesignSystem designSystem = (CDesignSystem)item.Reference;
                    designSystem.CrossReferenceList.Remove(this);
                    item.Reference = null;
                    item.GUID = Guid.Empty;
                }
            }
        }
        /*!
         * Seta refencia para um objeto
         * @param Index Indice na lista de referencias
         * @param Referencia para objeto
         */        
        public void SetReference(int Index, Object Value)
        {
            if ((Index >= 0) && (Index < ReferenceList.Count))
            {
                CReferenceItem item = ReferenceList[Index];
                //
                if (item.Reference != null)
                {
                    CDesignSystem designSystem = (CDesignSystem)item.Reference;
                    designSystem.CrossReferenceList.Remove(this);
                }
                //
                item.Reference = Value;
                //
                if (item.Reference != null)
                {
                    CDesignSystem designSystem = (CDesignSystem)item.Reference;
                    designSystem.CrossReferenceList.Add(this);
                }
            }
        }
        /*!
         * Retorna referencia para objeto
         * @param Index Indice na lista de referencias
         */
        public Object GetReference(int Index)
        {
            //if (referenceList.Count >= 0 && Index < referenceList.Count)
            if ((Index >= 0) && (Index < ReferenceList.Count))
            {
                CReferenceItem item = (CReferenceItem)ReferenceList[Index];
                return item.Reference;
            }
            else
            {
                return null;
            }
        }
        /*!
         * Seta GUID de refencia para um objeto
         * @param Index Indice na lista de referencias
         * @param Value GUID do objeto
         */
        public virtual void SetReferenceGuid(int Index, Guid Value)
        {
            if (Index >= 0 && Index < ReferenceList.Count)
            {
                CReferenceItem item = ReferenceList[Index];
                item.GUID = Value;                    
            }           
        }
        /*!
         * Retorna referencia para objeto
         * @param Index Indice na lista de referencias
         * @return GUID
         */
        public virtual Guid GetReferenceGuid(int Index)
        {
            if (Index >= 0 && Index < ReferenceList.Count)
            {
                CReferenceItem item = ReferenceList[Index];
                return item.GUID;
            }
            else
            {
                return Guid.Empty;
            }
        }
        /*!
         * 
         */
        public void SetOpenName(string Value)
        {
            customObject.name = Value;
            OnSetObjectName(new SetNameEventArgs(customObject.name));
        }
        /*!
         * Seta nome do objeto.
         * Se objetos de tipos diferentes tiverem o mesmo proprietario
         * esta função deve ser sobreescrita.
         * @param value Nome do objeto
         */
        protected virtual void SetName(string value)
        {
            SetNewName(value);
            OnSetObjectName(new SetNameEventArgs(customObject.name));
        }
        /*!
         * Seta matriz de objetos de mesmo tipo.
         * Se objetos de tipos diferentes tiverem o mesmo proprietario,
         * esta função deve ser sobreescrita.
         * @param value Nome do objeto
         */
        protected void SetNewName(string value)
        {
            CDesignSystem myOwner = (Owner as CDesignSystem);
            if (myOwner == null)
            {
                customObject.name = value;
            }
            else
            {
                //IEnumerable<CDesignSystem> subSet = myOwner.ObjectList.OfType<CDesignSystem>();
                //CDesignSystem[] objects = subSet.ToArray();
                CDesignSystem[] objects = GetArrayOfObjects(myOwner.ObjectList);
                if (objects == null)
                    customObject.name = value;
                else
                    customObject.name = myOwner.GetNewName(value, objects);
            }
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo.
         * Se objetos de tipos diferentes tiverem o mesmo proprietario,
         * esta função deve ser sobreescrita.
         * @param Objects Lista de objetos do proprietario (owner.ObjectList)
         * @return Array de objetos do mesmo tipo
         */
        protected virtual CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            return (CDesignSystem[])Objects.ToArray(typeof(CDesignSystem));
        }
        /*!
         * Verifica se não ha nome repetido no array de objetos.
         * Usa recursos do LINQ...
         * @param Value Novo nome 
         * @param Objects Array de objetos do mesmo tipo
         * @return Nome do objeto
         */
        protected string GetNewName(string Value, CDesignSystem[] Objects)
        {
            var result = (from obj in Objects select obj).Where(obj => (obj.Name == Value));
            if (result.Count() > 0)
            {
                int i = 1;
                string newName = string.Format("{0}{1}", Value, i);
                result = (from obj in Objects select obj).Where(obj => (obj.Name == newName));
                while ((result.Count() > 0))
                {
                    i++;
                    newName = string.Format("{0}{1}", Value, i);
                    result = (from obj in Objects select obj).Where(obj => (obj.Name == newName));
                }
                return newName;
            }
            else
            {
                return Value;
            }
        }
        /*!
         * 
         */
        public virtual void Initialize(Control Parent) { }
        /*!
         * 
         */
        public virtual void Initialize() { }
        /*!
         * 
         */
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetFilteredProperties();
        }
        /*!
         * 
         */
        public void Exchange(int Index1, int Index2)
        {
            Object temp = ObjectList[Index1];
            ObjectList[Index1] = ObjectList[Index2];
            ObjectList[Index2] = temp;
            OnExchange(new ExchangeEventArgs(Index1, Index2));
        }
        /*!
         * 
         */
        protected virtual void SetSelected(bool value)
        {
            selected = value;
            OnSelectedObject(new SelectedObjectEventArgs(selected));
        }
        #endregion
        #region Eventos
        //!
        public event SetNameEventHandler SetObjectName;
        //!
        public event AddItemEventHandler AddItem;
        //!
        public event DelItemEventHandler DelItem;
        //!
        public event AddItemEventHandler SetObjectIcon;
        //!        
        public event ExchangeEventHandler ExchangeObject;
        //!        
        public event SelectedObjectEventHandler SelectedObject;        
        #endregion 
        #region Invocadores de eventos
        /*!
         * 
         */
        protected void OnSetObjectName(SetNameEventArgs e)
        {
            if (this.SetObjectName != null)
                this.SetObjectName(this, e);
        }
        protected void OnAddItem(AddItemEventArgs e)
        {
            if (this.AddItem != null)
                this.AddItem(this, e);
        }
        protected void OnDelItem(EventArgs e)
        {
            if (this.DelItem != null)
                this.DelItem(this, e);
        }
        protected void OnSetObjectIcon(AddItemEventArgs e)
        {
            if (this.SetObjectIcon != null)
                this.SetObjectIcon(this, e);
        }
        protected void OnSelectedObject(SelectedObjectEventArgs e)
        {
            if (SelectedObject != null)
                SelectedObject(this, e);
        }
        protected void OnExchange(ExchangeEventArgs e)
        {
            if (this.ExchangeObject != null)
                this.ExchangeObject(this, e);
        }
        #endregion
    }
}
