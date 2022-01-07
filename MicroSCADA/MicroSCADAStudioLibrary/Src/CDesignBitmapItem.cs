using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src.TypeConverter;

namespace MicroSCADAStudioLibrary.Src
{
    //Implementa suporte ao PropertyGrig quando tag for membro de classe
    //[EditorAttribute(typeof(CBitmapTypeDialogPreset), typeof(System.Drawing.Design.UITypeEditor))]
    public class CDesignBitmapItem : CDesignSystem , ICustomBitmapItem
    {
        private CCustomBitmapItem m_customBitmapItem;
        private CCustomBitmapList m_customBitmapList;
        /*!
         * 
         */
        public CDesignBitmapItem(Object AOwner, CDesignProject Project, CCustomBitmapList BitmapList)
            : base(AOwner, Project)
        {
            this.m_customBitmapList = BitmapList;
            this.m_customBitmapItem = new CCustomBitmapItem();
            this.imageIndex = 29;
        }
        public CCustomBitmapItem CustomBitmapItem
        {
            get { return this.m_customBitmapItem; }
        }
        //TODO:TEMPORARIO
        protected override void SetName(string value)
        {
            customObject.name = value;
            OnSetObjectName(new SetNameEventArgs(customObject.name));
        }     
        /*!
         * 
         */
        public Bitmap GetBitmap()
        {               
            m_customBitmapItem.bitmap = m_customBitmapList.GetBitmap(m_customBitmapItem.position);
            return m_customBitmapItem.bitmap;
        }
        public Stream GetBitmapFromStream()
        {
            //m_customBitmapItem.bitmap = m_customBitmapList.GetBitmap(m_customBitmapItem.position, m_customBitmapItem.size);
            return null;
        }
        /*!
         * 
         */
        public int Position
        {
            get { return this.m_customBitmapItem.position; }
            set { this.m_customBitmapItem.position = value; }
        }
        public int Size
        {
            get { return this.m_customBitmapItem.size; }
            set { this.m_customBitmapItem.size = value; }
        }
    }
}
