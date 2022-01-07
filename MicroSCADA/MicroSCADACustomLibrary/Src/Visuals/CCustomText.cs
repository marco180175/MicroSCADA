using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    /*!
     * 
     */
    public interface ICustomText : ICustomScreenObject
    {
        String Text { get; set; }
        Font TextFont { get; set; }
        Color TextFontColor { get; set; }
        StringAlignment Alignment { get; set; }
        String TextToString();
        void StringToText(String Value);
    }
    /*!
     * 
     */
    public static class CCustomText 
    {        
        public static void DrawText(Graphics graphics, PictureBox pictureBox, CCustomTextProperties textProperties, CBorder border, CFrame frame)
        {
            Rectangle rect = new Rectangle(0, 0, pictureBox.Width, textProperties.font.Height);
            StringFormat sf = new StringFormat();
            SolidBrush sb = new SolidBrush(textProperties.fontColor);
            CCustomScreenObject.DrawBorder(graphics, pictureBox, border, frame);
            sf.Alignment = textProperties.alignment;
            for (int i = 0; i < textProperties.text.Length; i++)
            {
                graphics.DrawString(textProperties.text[i], textProperties.font, sb, rect, sf);
                rect.Y += textProperties.font.Height;
            }
            sb.Dispose();
        }       
    }
}
