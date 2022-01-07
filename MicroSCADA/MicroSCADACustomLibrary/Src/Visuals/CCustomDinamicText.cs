using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public interface ICustomTextZone : ICustomSystem, ICustomRange
    {
        String Text { get; set; }
        Font TextFont { get; set; }
        Color TextFontColor { get; set; }
        Color BackColor { get; set; }
        StringAlignment Alignment { get; set; }
        String TextToString();
        void StringToText(String Value);
    }

    public interface ICustomDinamicText : ICustomField
    {
        ICustomTextZone AddZone();                        
        CDinamicType DinamicType { get; set; }
    }

    public class CCustomDinamicText : Object
    {
        private PictureBox pictureBox;
        public CCustomDinamicText(PictureBox PictureBox)
            : base()
        {
            this.pictureBox = PictureBox;
        }

        public void DrawTextZone(Graphics graphics,ICustomTextZone textZone)
        {
            if (textZone != null)
            {
                Rectangle rect = new Rectangle(0, 0, pictureBox.Width, pictureBox.Height);
                StringFormat sf = new StringFormat();
                Brush sb = new SolidBrush(textZone.BackColor);
                graphics.FillRectangle(sb, 0, 0, pictureBox.Width, pictureBox.Height);
                sb = new SolidBrush(textZone.TextFontColor);
                sf.Alignment = textZone.Alignment;
                //for (int i = 0; i < textZone.Text.Length; i++)
                //{
                //    graphics.DrawString(textZone.Text[i], textZone.TextFont, sb, rect, sf);
                //    rect.Y += textZone.TextFont.Height;
                //}
                graphics.DrawString(textZone.Text, textZone.TextFont, sb, rect, sf);
                sb.Dispose();
            }
            else
            {
                StringFormat sf = new StringFormat();
                Brush brush = new SolidBrush(Color.Black);
                Font font = new Font("Arial", 16, FontStyle.Bold);
                int y = (pictureBox.Height - font.Height) / 2;
                Rectangle rect = new Rectangle(0, y, pictureBox.Width, pictureBox.Height);
                sf.Alignment = StringAlignment.Center;

                graphics.DrawString("no text", font, brush, rect, sf);

                font.Dispose();
                brush.Dispose();
            }
        }
    }
}
