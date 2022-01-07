using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimePicture : CRuntimeScreenObject, ICustomPicture
    {
        protected CCustomPicture customPicture;
        private static int count = 0;
        protected int indexBitmapItem;
        //private CRuntimeBitmapItem bitmapItem;
        //Construtor
        public CRuntimePicture(Object AOwner, CRuntimeProject Project)
            : base(AOwner,Project)
        {           
            this.customPicture = new CCustomPicture();
            this.indexBitmapItem = this.ReferenceList.AddReference();
            this.pictureBox.Paint += new PaintEventHandler(pictureBox_Paint);                 
            count++;
        }
        //Destrutor
        ~CRuntimePicture()
        {
            count--;    
        }
        public CCustomPicture CustomPicture
        {
            get { return this.customPicture; }
        }
        public ICustomBitmapItem BitmapItem
        {
            get { return (CRuntimeBitmapItem)this.GetBitmapItem(); }
            set { this.SetBitmapItem((CRuntimeBitmapItem)value); }
        }
        public static int getCount() { return count; }
        //Evento OnPaint
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (GetBitmapItem() != null)
                customPicture.DrawPicture(e.Graphics, GetBitmapItem().GetBitmap());                
        }
        public override void LinkObjects()
        {
            Guid keyGUID = GetReferenceGuid(indexBitmapItem);
            Object obj;
            //
            if (CHashObjects.ObjectDictionary.ContainsKey(keyGUID))
                obj = CHashObjects.ObjectDictionary[keyGUID];
            else
                obj = null;
            SetReference(indexBitmapItem, obj);
        }
        
        public void SetGuidBitmapItem(Guid Value)
        {
            this.SetReferenceGuid(indexBitmapItem, Value);
        }
        protected void SetBitmapItem(CRuntimeBitmapItem Value)
        {
            this.SetReference(indexBitmapItem, Value);
        }

        protected CRuntimeBitmapItem GetBitmapItem()
        {
            return (CRuntimeBitmapItem)this.GetReference(indexBitmapItem);
        }
    }
}
