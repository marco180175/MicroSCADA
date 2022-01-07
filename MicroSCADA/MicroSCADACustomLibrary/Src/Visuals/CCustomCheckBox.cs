using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MicroSCADACustomLibrary.Src.Visuals
{

    public interface ICustomCheckBox : ICustomField//, ICustomRadioButton
    {        
        Font Font { get; set; }
        Color FontColor { get; set; }
        bool Checked { get; set; }
        string Caption { get; set; }
    }

    public class CCustomCheckBox
    {
        public bool boxChecked;
        public string caption;
        public Font font;
        public Color fontColor;
        public CCustomCheckBox() 
        {
            this.boxChecked = false;
            this.caption = "CheckBox";
            this.font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular);
            this.fontColor = Color.Black;            
        }

        public void DrawCheckBox(Graphics graphics, Control pictureBox)
        {
            SizeF sizeFont = graphics.MeasureString(caption, font);
            int h = (int)sizeFont.Height;
            Rectangle cbRect = new Rectangle(2, 1, h, h);
            if(boxChecked)
                ControlPaint.DrawCheckBox(graphics, cbRect, ButtonState.Checked);
            else
                ControlPaint.DrawCheckBox(graphics, cbRect, ButtonState.Normal);
            graphics.DrawString(caption, font, new SolidBrush(fontColor), cbRect.Right, cbRect.Top);
            pictureBox.Height = h + 2;
        }
    }
}
