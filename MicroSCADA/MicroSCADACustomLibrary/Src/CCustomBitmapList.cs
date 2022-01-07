using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomBitmapList : ICustomObject
    {
        String FileName { get; }
        CCustomBitmapList CustomBitmapList { get; }
        ICustomBitmapItem AddBitmap();
        void Open(String FileName);
        void Open();
        void Close();
        void New(String FileName);
    }

    public class CCustomBitmapList : Object
    {
        public FileStream fileStream;
        public IFormatter serializer;
        public String fileName;

        public CCustomBitmapList()
            : base()
        {            
            this.fileName = String.Empty;            
        }
        /*
        public void Open(String FileName)
        {
            fileName = FileName.Replace(".xml", ".tbm");
            fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            serializer = new BinaryFormatter();
        }
        */
        public Bitmap GetBitmap(int Position)
        {
            Bitmap bmp;
            fileStream.Position = Position;
            bmp = serializer.Deserialize(fileStream) as Bitmap;
            return bmp;
        }

        public Stream GetBitmap(int Position, int Size)
        {            
            byte[] buffer = new byte[Size];
            Stream stream = new MemoryStream();
            fileStream.Position = Position;
            fileStream.Read(buffer, 0, Size);            
            stream.Write(buffer, 0, Size);
                        
            return stream;
        }
    }
}
