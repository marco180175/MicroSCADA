using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public interface ICustomPictureZone : ICustomSystem
    {
        ICustomBitmapItem BitmapItem { get; set; }
        void SetGuidBitmapItem(Guid Value);
    }

    public interface ICustomPicture : ICustomScreenObject
    {
        ICustomBitmapItem BitmapItem { get; set; }
        CCustomPicture CustomPicture { get; }
        void SetGuidBitmapItem(Guid Value);
    }

    public class CCustomPicture : Object
    {
        private static int count = 0;
        //private CBitmapRef bitmapRef;
        //Construtor
        public CCustomPicture()
            : base()
        {                        
            
            count++;
        }
        //Destrutor
        ~CCustomPicture()
        {
            count--;    
        }
        public int getCount() { return count; }
        //
        public void DrawPicture(Graphics graphics,Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            graphics.DrawImage(bitmap, rect);        
        }   
        
    }
}
