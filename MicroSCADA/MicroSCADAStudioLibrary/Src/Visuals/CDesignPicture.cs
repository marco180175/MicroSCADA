using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using MicroSCADAStudioLibrary.Src.TypeConverter;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    public class CDesignPicture : CDesignScreenObject, ICustomPicture
    {
        private CCustomPicture customPicture;
        protected int indexBitmapItem;
        public string bitmapHMIStudio;
        public const string IMAGE_FILTER = "Image files(*.gif;*.jpg;*.jpeg;*.bmp;*.wmf*;*.png)|"+
                                    "*.GIF;*.JPG;*.JPEG;*.BMP;*.WMF*;*.PNG|"+
                                    "All files (*.*)|*.*";
        /*!
         * Construtor
         */
        public CDesignPicture(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject(); 
            this.customPicture = new CCustomPicture();
            this.indexBitmapItem = this.ReferenceList.AddReference();
                
            this.DoubleClick += new EventHandler(Project.DoubleClick);
            this.imageIndex = 30;
        }
        /*!
         * Destrutor
         */
        ~CDesignPicture()
        {
            Dispose();
        }        
        /*!
         * 
         */
        public override void Dispose()
        {
            this.SetReference(indexBitmapItem, null);        
            base.Dispose();
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignPicture> subSet = Objects.OfType<CDesignPicture>();
            return subSet.ToArray();
        }
        public CCustomPicture CustomPicture
        {
            get { return this.customPicture; }
        }
        //! Figura de fundo
        [EditorAttribute(typeof(CBitmapDialogTypeEditor), 
                         typeof(System.Drawing.Design.UITypeEditor))]
        public CDesignBitmapItem ImageOfObject
        {
            get { return (CDesignBitmapItem)this.BitmapItem; }
            set { this.BitmapItem = value; }
        }
        //!
        [Browsable(false)]
        public ICustomBitmapItem BitmapItem
        {
            get { return this.GetBitmapItem(); }
            set { this.SetBitmapItem((CDesignBitmapItem)value); }
        }        
        /*!
         * Evento OnPaint
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if(BitmapItem != null)
                customPicture.DrawPicture(e.Graphics, GetBitmapItem().GetBitmap());    
            if (selected)
                DrawSelectedRect(e.Graphics);
        }
        protected override void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(EventArgs.Empty);
        }
        public event EventHandler DoubleClick;
        private void OnDoubleClick(EventArgs e)
        {
            if (DoubleClick != null)
                DoubleClick(this, e);
        }
        /*!
         * Adiciona imagem na lista de bitmaps da aplicação
         */
        public void SetImage(String FileName)
        {
            CDesignBitmapList bitmapList = (CDesignBitmapList)project.BitmapList;
            BitmapItem = bitmapList.AddBitmap(FileName);                  
        }

        protected void SetBitmapItem(CDesignBitmapItem Value)
        {
            this.SetReference(indexBitmapItem, Value);
        }

        protected CDesignBitmapItem GetBitmapItem()
        {
            return (CDesignBitmapItem)this.GetReference(indexBitmapItem);
        }
        
        public void SetGuidBitmapItem(Guid Value)
        {
            this.SetReferenceGuid(indexBitmapItem, Value);
        }
        /*!
         * Resolve referencia do objeto
         */
        public override void LinkObjects()
        {
            Object obj;
            Guid guid = GetReferenceGuid(indexBitmapItem);
            //
            if (CHashObjects.ObjectDictionary.ContainsKey(guid))
                obj = CHashObjects.ObjectDictionary[guid];
            else
                obj = null;   
            SetReference(indexBitmapItem, obj);
        }        
    }
}
