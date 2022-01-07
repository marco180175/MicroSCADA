using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADACompilerLibrary.Src
{
    public class CCompilerBitmapList : CCompilerSystem, ICustomBitmapList
    {
        private CCustomBitmapList customBitmapList;
        public CCompilerBitmapList(Object AOwner, CCompilerProject Project)
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
            CCompilerBitmapItem bitmapItem;

            bitmapItem = new CCompilerBitmapItem(this, project);
            ObjectList.Add(bitmapItem);            
            return bitmapItem;
        }
        public void New(String FileName)
        {
        }
        public void Open(String FileName)
        {
            customBitmapList.fileName = Path.ChangeExtension(FileName, ".tbm");
            customBitmapList.fileStream = new FileStream(customBitmapList.fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            customBitmapList.serializer = new BinaryFormatter();
        }
        public Bitmap GetBitmap(int Position)
        {
            return customBitmapList.GetBitmap(Position);
        }
        public Stream GetBitmap(int Position, int Size)
        {
            return customBitmapList.GetBitmap(Position, Size);
        }
        public void Open() { }
        public void Close() { }
    }
}
