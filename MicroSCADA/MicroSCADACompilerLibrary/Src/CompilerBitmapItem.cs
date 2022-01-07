using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADACompilerLibrary.Src
{
    public class CCompilerBitmapItem : CCompilerSystem, ICustomBitmapItem
    {
        private CCustomBitmapItem customBitmapItem;
        /*!
         * 
         */
        public CCompilerBitmapItem(Object AOwner, CCompilerProject Project)
            : base(AOwner, Project)
        {
            this.customBitmapItem = new CCustomBitmapItem();  
        }
        public CCustomBitmapItem CustomBitmapItem
        {
            get { return this.customBitmapItem; }
        }
        //
        public int Size 
        {
            get { return customBitmapItem.size; }
            set { customBitmapItem.size = value; }
        }       
        //
        public int Position
        {
            get { return this.customBitmapItem.position; }
            set { this.customBitmapItem.position = value; }
        }
        public bool IsUsed
        {
            get { return true; }
        }
        /*!
         * 
         */
        public Bitmap GetBitmap()
        {
            CCompilerBitmapList bitmapList;
            bitmapList = (CCompilerBitmapList)Owner;
            customBitmapItem.bitmap = bitmapList.GetBitmap(customBitmapItem.position);
            return customBitmapItem.bitmap;
        }
        /*!
         * 
         */
        public Stream GetBitmapFromStream()
        {
            CCompilerBitmapList bitmapList;
            bitmapList = (CCompilerBitmapList)Owner;
            Stream st = bitmapList.GetBitmap(customBitmapItem.position, customBitmapItem.size);
            return st;
        }
    }
}
