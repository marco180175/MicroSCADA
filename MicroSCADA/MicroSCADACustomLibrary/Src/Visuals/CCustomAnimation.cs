using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public interface ICustomAnimation : ICustomField
    {
        ICustomPictureZone AddZone();        
    }

    public class CCustomAnimation : Object
    {
        private PictureBox pictureBox;
        public CCustomAnimation(PictureBox PictureBox)
            : base()
        {
            this.pictureBox = PictureBox;
        }

        public void DrawPictureZone(Graphics graphics,Bitmap bitmap)
        {
            if (bitmap != null)
                graphics.DrawImage(bitmap, 0, 0);
            else
            {                
                StringFormat sf = new StringFormat();
                Brush brush = new SolidBrush(Color.Black);
                Font font = new Font("Arial", 16, FontStyle.Bold);
                int y = (pictureBox.Height - font.Height) / 2;
                Rectangle rect = new Rectangle(0, y, pictureBox.Width, pictureBox.Height);               
                sf.Alignment = StringAlignment.Center;

                graphics.DrawString("no picture", font, brush, rect, sf);

                font.Dispose();
                brush.Dispose();
            }
        }
    }
}
