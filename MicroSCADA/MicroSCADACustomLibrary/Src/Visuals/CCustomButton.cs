using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public enum CButtonType
    {
        //OnOff,
        //Transparent,
        //Picture
        //btTagValue,
        btToggle,
        btAction
    }
    /*!
     * Interface com propriadades e funções comuns para botão modo design e runtime
     */
    public interface ICustomButton : ICustomField
    {
        int BorderWidth { get; set; }//TODO:remover
        CButtonType ButtonType { get; set; }
        string Text { get; set; }
        Font TextFont { get; set; }
        Color TextFontColor { get; set; }        
        StringAlignment Alignment { get; set; }
        ICustomAction ActionClick { get; set; }
        int ValueOff { get; set; }
        int ValueOn { get; set; }
        bool Jog { get; set; }
        void SetGuidAction(Guid Value);
    }
    /*!
     * Classe com propriadades e funçoes comuns para botão modo design e runtime
     */
    public class CCustomButton
    {
        public int borderWidth;
        public CButtonType buttonType;
        public Color backColor;
        public CCustomTextProperties textProp;
        public int valueOff;
        public int valueOn;
        public bool jog;
        public CCustomButton()
        {
            this.textProp = new CCustomTextProperties();   
            this.borderWidth = 1;
            this.buttonType = CButtonType.btToggle;
            this.backColor = Color.Gray;            
            this.textProp.text[0] = "Button";
            this.textProp.alignment = StringAlignment.Center;
            this.valueOff = 0;
            this.valueOn = 1;
            this.jog = false;
        }

        private void DrawBorder(Graphics graphics, int width, int height, bool down)
        {
            Pen pen = new Pen(Color.Black);
            graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);
            if (down)
            {
                //
                pen.Color = Color.FromArgb(113, 111, 100);
                graphics.DrawLine(pen, 1, height - 2, 1, 1);
                graphics.DrawLine(pen, 1, 1, width - 2, 1);
                //
                pen.Color = Color.FromArgb(172, 168, 153);
                graphics.DrawLine(pen, 2, height - 3, 2, 2);
                graphics.DrawLine(pen, 2, 2, width - 3, 2);
                //
                pen.Color = Color.FromArgb(255, 255, 255);
                graphics.DrawLine(pen, width - 2, 1, width - 2, height - 2);
                graphics.DrawLine(pen, width - 2, height - 2, 1, height - 2);
                //
                pen.Color = Color.FromArgb(241, 239, 226);
                graphics.DrawLine(pen, width - 3, 2, width - 3, height - 3);
                graphics.DrawLine(pen, width - 3, height - 3, 2, height - 3);
            }
            else
            {
                //
                pen.Color = Color.FromArgb(255, 255, 255);
                graphics.DrawLine(pen, 1, height - 2, 1, 1);
                graphics.DrawLine(pen, 1, 1, width - 2, 1);
                //
                pen.Color = Color.FromArgb(241, 239, 226);
                graphics.DrawLine(pen, 2, height - 3, 2, 2);
                graphics.DrawLine(pen, 2, 2, width - 3, 2);
                //
                pen.Color = Color.FromArgb(113, 111, 100);
                graphics.DrawLine(pen, width - 2, 1, width - 2, height - 2);
                graphics.DrawLine(pen, width - 2, height - 2, 1, height - 2);
                //
                pen.Color = Color.FromArgb(172, 168, 153);
                graphics.DrawLine(pen, width - 3, 2, width - 3, height - 3);
                graphics.DrawLine(pen, width - 3, height - 3, 2, height - 3);
            }
        }
        /*!
         * Desenha botão usando metodo da classe "ControlPaint"
         * @param graphics
         * @param pictureBox
         * @param down Verdadeiro se botão abaixado
         */
        public void DrawButton(Graphics graphics, PictureBox pictureBox, bool down)
        {
            int textOffset;//offset do texto
            Rectangle btRect = new Rectangle(0, 0, pictureBox.Width, pictureBox.Height);
            if (down)
            {
                textOffset = 2;
                ControlPaint.DrawButton(graphics, btRect, ButtonState.Pushed);
            }
            else
            {
                textOffset = 0;
                ControlPaint.DrawButton(graphics, btRect, ButtonState.Normal);
            }
            //pinta fundo
            graphics.FillRectangle(new SolidBrush(pictureBox.BackColor), new Rectangle(2, 2, pictureBox.Width - 4, pictureBox.Height - 4));
            //desenha texto do botão
            Rectangle rect = new Rectangle(textOffset, 0, pictureBox.Width, textProp.font.Height);
            StringFormat sf = new StringFormat();
            SolidBrush sb = new SolidBrush(textProp.fontColor);            
            sf.Alignment = textProp.alignment;
            //rect.Y = (pictureBox.Height - textProp.text.Length * textProp.font.Height) / 2;
            rect.Y = ((pictureBox.Height - textProp.font.Height) / 2) + textOffset;
            //for (int i = 0; i < textProp.text.Length; i++)
            //{
                graphics.DrawString(textProp.text[0], textProp.font, sb, rect, sf);
                //rect.Y += textProp.font.Height;
            //}
            sb.Dispose();
        }
    }
}
