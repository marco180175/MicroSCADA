using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    /*!
     * RadioButton
     */
    public interface ICustomRadioButton : ICustomSystem, ICustomView
    {
        bool Checked { get; set; }
        string Caption { get; set; }
        int Y { get; set; }
    }
    /*!
     * Grupo de radiobuttons
     */
    public interface ICustomRadioGroup : ICustomField
    {
        Font Font { get; set; }
        Color FontColor { get; set; }
        int Count { get; set; }
        int CheckedButtonIndex { get; set; }
        ICustomRadioButton[] Items { get; }
    }    
        
    /*!
     * RadioButton
     */
    public class CCustomRadioButton 
    {
        //private ICustomRadioGroup m_radioGroup;
        public bool _checked;
        public string caption;
        public int y;
        public CCustomRadioButton(ICustomRadioGroup RadioGroup, int Index)
        {
            //this.m_radioGroup = RadioGroup;
            this._checked = false;
            this.caption = string.Format("RadioButton{0}", Index);
            this.y = 0;
        }
        
        /*!
         *
         * 
         */
        public static void DrawRadioButton(Graphics graphics, Control controlDraw, ICustomRadioButton button, int offset, Font font, Color fontColor)
        {
            //Retangulo do check      
            Rectangle cbRect = new Rectangle(button.Left, button.Y + offset, button.Height, button.Height);
            //Rectangle cbRect = new Rectangle(offset, button.Y + offset, h, h);
            //graphics.FillRectangle(Brushes.Red, cbRect);
            if (button.Checked)
                ControlPaint.DrawRadioButton(graphics, cbRect, ButtonState.Checked);
            else
                ControlPaint.DrawRadioButton(graphics, cbRect, ButtonState.Normal);
            graphics.DrawRectangle(new Pen(Color.Black, 1), cbRect);
            //Retangulo do caption
            Rectangle cpRect = new Rectangle(cbRect.Right, cbRect.Top,
                controlDraw.Width - cbRect.Width - (offset * 2), cbRect.Height);
            //graphics.FillRectangle(Brushes.Green, cpRect);
            graphics.DrawRectangle(new Pen(Color.Black, 1), cpRect);
            //texto do caption
            graphics.DrawString(button.Caption,
                font, new SolidBrush(fontColor), cbRect.Right, cbRect.Top + 1);
        }
    }
    /*!
     * Grupo de radiobuttons
     */
    public class CCustomRadioGroup
    {
        public Font font;
        public Color fontColor;        
        public const int MIN_COUNT = 2;
        public const int BORDER_WIDTH = 2;
        public int m_h;
        public CCustomRadioGroup()
        {              
            this.font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular);
            this.fontColor = Color.Black;            
        }        
        /*!
         * 
         */
        public void DrawRadioGroup(Graphics graphics, Control controlDraw, List<ICustomRadioButton> Items)
        {
            m_h = font.Height;            
            controlDraw.Height = (m_h * Items.Count) + 4;
        }        
    }
}
