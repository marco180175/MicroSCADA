using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomBitmapItem : ICustomSystem
    {
        CCustomBitmapItem CustomBitmapItem { get; }
        int Position { get; set; }        
        int Size { get; set; }
        Bitmap GetBitmap();
        Stream GetBitmapFromStream();
    }

    public class CCustomBitmapItem : Object
    {
        public Bitmap bitmap;
        public int position;
        public int size;
        public CCustomBitmapItem()
            : base()
        {            
            this.bitmap = null;
            this.position = -1;
            this.size = 0;
        }
        
        public void SetPosition(int Value)
        {
            this.position = Value;
        }        
    }
}
