using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src
{
    public abstract class CRuntimeCustomScreenList : CRuntimeSystem, ICustomScreenList
    {
        protected int width;
        protected int height;
        public static CRuntimeCustomField fieldInEdit;//!< Campo em edição
        
        /*!
         * Construtor
         */
        public CRuntimeCustomScreenList(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.width = 640;
            this.height = 480;
        }
        /*!
         * Destructor
         */
        ~CRuntimeCustomScreenList() { }
        //!
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }
        //!
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        /*!
         * 
         */
        public abstract ICustomScreen NewScreen();
        /*!
         * Seta campo que sera editado na tela.
         * No momento estou considerando que apenas um campo
         * podera ser editado por vez.
         */
        //public static void BeginFieldEdit(CRuntimeCustomField Value)
        //{
        //    if (fieldInEdit != null)
        //        fieldInEdit.IsEditing = false;
        //    //
        //    fieldInEdit = Value;
        //    //
        //    if (fieldInEdit != null)
        //        fieldInEdit.IsEditing = true;
        //}

        //public static void EndFieldEdit()
        //{
        //    if (fieldInEdit != null)
        //    {
        //        fieldInEdit.IsEditing = false;
        //        fieldInEdit = null;
        //    }
        //}

        //public void Begin_FieldEdit(object sender,EventArgs e)
        //{
        //    if (fieldEditing != null)
        //        fieldEditing.IsEditing = false;
        //    //
        //    fieldEditing = (CRuntimeCustomField)sender;
        //    //
        //    if (fieldEditing != null)
        //        fieldEditing.IsEditing = true;
        //}

        //public void End_FieldEdit(object sender, EventArgs e)
        //{
        //    if (fieldEditing != null)
        //    {
        //        fieldEditing.IsEditing = false;
        //        fieldEditing = null;
        //    }
        //}


    }
}
