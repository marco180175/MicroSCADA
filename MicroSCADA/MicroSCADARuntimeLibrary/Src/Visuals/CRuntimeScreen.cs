using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimeScreen : CRuntimeCustomScreen
    {        
        private int left;
        private int top;
        private int width;
        private int height;
        private ArrayList tagList;
        private Control m_parent;
        
        //Construtor
        public CRuntimeScreen(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {            
            this.Name = "Screen";
            this.tagList = new ArrayList();
            
            
        }
        //Destrutor
        ~CRuntimeScreen()
        {
            Dispose();
        }
        public override void Dispose()
        {
            
            base.Dispose();
        }
        //Propriedades(implementar para popupscreen}
        protected override int getLeft() { return this.left; }
        protected override void setLeft(int Value) { this.left = Value; }
        protected override int getTop() { return this.top; }
        protected override void setTop(int Value) { this.top = Value; }
        protected override int getWidth() { return this.width; }
        protected override void setWidth(int Value) { this.width = Value; }
        protected override int getHeight() { return this.height; }
        protected override void setHeight(int Value) { this.height = Value; }
        //!
        public ArrayList TagList
        {
            get { return this.tagList; }
        }
        //!
        public int Index
        {
            get 
            {
                CRuntimeScreenList list = (CRuntimeScreenList)Owner;
                return list.ObjectList.IndexOf(this); 
            }
        }
        public event EventHandler Enter;
        private void OnEnter( EventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }
        /*!
         * 
         */
        //public void IncTabIndex()
        //{            
        //    if (tabIndex < editTableFields.Count - 1)
        //        tabIndex++;
        //    else
        //        tabIndex = 0;
        //    //SetFocus();
        //}
        /*!
         * 
         */
        //public void DecTabIndex()
        //{            
        //    if (tabIndex > 0)
        //        tabIndex--;
        //    else
        //        tabIndex = editTableFields.Count - 1;
        //    //SetFocus();
        //}        
        
        /*!
         * 
         */
        public void setParent(Control parent)
        {            
            CRuntimeScreenObject screenObject;

            if (parent != null)
            {
                parent.BackColor = customScreen.backColor;
                if (BitmapItem != null)
                    parent.BackgroundImage = ((CRuntimeBitmapItem)BitmapItem).GetBitmap();
                else
                    parent.BackgroundImage = null;
                OnEnter(EventArgs.Empty);
            }
            for (int i = 0; i < ObjectList.Count; i++)
            {
                screenObject = (CRuntimeScreenObject)ObjectList[i];                                  
                screenObject.getPictureBox().Parent = parent;             
            }            
        }
        /*!
         * Lista de campos editaveis
         */
        private void GetEditabledFields()
        {
            List<CRuntimeCustomField> list1 = ObjectList.OfType<CRuntimeCustomField>().Where(obj => obj.FieldType == CFieldType.ftReadWrite).ToList();
            editTableFields = new ArrayList(list1);
            foreach (CRuntimeCustomField c in editTableFields)
                c.Enter += new EventHandler(focus_Enter);
        }
        public event EventHandler FieldEnter;
        private void focus_Enter(object sender, EventArgs e)
        {
            if (FieldEnter != null)
                FieldEnter(sender, e);
        }
        
        /*!
         * 
         */
        public void LinkObjects()
        {
            Guid keyGUID = GetReferenceGuid(indexBitmapItem);
            Object obj;
            //
            if(CHashObjects.ObjectDictionary.ContainsKey(keyGUID))            
                obj = CHashObjects.ObjectDictionary[keyGUID];            
            else            
                obj = null;            
            SetReference(indexBitmapItem, obj);
            //            
            foreach (CRuntimeScreenObject screenObject in ObjectList)                
                screenObject.LinkObjects();        
            //
            GetEditabledFields();
        }

        
    }
}
