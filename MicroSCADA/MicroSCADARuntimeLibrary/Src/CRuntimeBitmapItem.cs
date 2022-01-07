using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeBitmapItem : CRuntimeSystem, ICustomBitmapItem
    {
        private CCustomBitmapItem customBitmapItem;
        /*!
         * 
         */
        public CRuntimeBitmapItem(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customBitmapItem = new CCustomBitmapItem();  
        }

        public int Size { get; set; }

        public CCustomBitmapItem CustomBitmapItem 
        { 
            get { return this.customBitmapItem; } 
        }
        /*!
         * 
         */
        public int Position
        {
            get { return this.customBitmapItem.position; }
            set { this.customBitmapItem.position = value; }
        }
        /*!
         * 
         */
        public Bitmap GetBitmap()
        {
            CRuntimeBitmapList bitmapList;
            bitmapList = (CRuntimeBitmapList)Owner;
            customBitmapItem.bitmap = bitmapList.GetBitmap(customBitmapItem.position);
            return customBitmapItem.bitmap;
        }
        public Stream GetBitmapFromStream()
        {
            CRuntimeBitmapList bitmapList;
            bitmapList = (CRuntimeBitmapList)Owner;
            //customBitmapItem.bitmap = bitmapList.GetBitmap(customBitmapItem.position, customBitmapItem.size);
            return null;
        }
    }
}
