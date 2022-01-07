using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeBitmapList : CRuntimeSystem, ICustomBitmapList
    {
        private CCustomBitmapList customBitmapList;
        public CRuntimeBitmapList(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customBitmapList = new CCustomBitmapList();  
        }
        //
        public CCustomBitmapList CustomBitmapList
        {
            get { return this.customBitmapList; }
        }
        public String FileName
        {
            get { return this.customBitmapList.fileName; }
        }
        public ICustomBitmapItem AddBitmap()
        {
            CRuntimeBitmapItem bitmapItem;

            bitmapItem = new CRuntimeBitmapItem(this, project);
            ObjectList.Add(bitmapItem);
            //bitmapItem.Name = "BitmapItem" + ObjectList.Count.ToString();
            return bitmapItem;
        }
        public void New(String FileName)
        {
        }
        public void Open(String FileName)
        {
            customBitmapList.fileName = Path.ChangeExtension(FileName, ".tbm");
            customBitmapList.fileStream = new FileStream(customBitmapList.fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            customBitmapList.serializer = new BinaryFormatter();   
        }
        public Bitmap GetBitmap(int Position)
        {
            return customBitmapList.GetBitmap(Position);
        }
        public void Open() { }
        public void Close() { }
    }
}
