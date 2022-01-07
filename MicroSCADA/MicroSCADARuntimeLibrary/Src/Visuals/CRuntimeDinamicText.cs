using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimeDinamicTextZone : CRuntimeSystem, ICustomTextZone
    {
        private CCustomTextProperties textProperties;
        private float maxValue;
        private float minValue;
        public CRuntimeDinamicTextZone(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.textProperties = new CCustomTextProperties();
            
        }
        
        public float MaxValue
        {
            get { return this.maxValue; }
            set { this.maxValue = value; }
        }
        
        public float MinValue
        {
            get { return this.minValue; }
            set { this.minValue = value; }
        }
        public String Text
        {
            get { return this.textProperties.TextToString(); }
            set { this.textProperties.StringToText(value); }
        }
        public Font TextFont
        {
            get { return this.textProperties.font; }
            set { this.textProperties.font = value; }
        }
        public Color TextFontColor
        {
            get { return this.textProperties.fontColor; }
            set { this.textProperties.fontColor = value; }
        }
        public Color BackColor
        {
            get { return this.textProperties.backColor; }
            set { this.textProperties.backColor = value; }
        }
        public StringAlignment Alignment
        {
            get { return this.textProperties.alignment; }
            set { this.textProperties.alignment = value; }
        }
        public String TextToString()
        {
            return textProperties.TextToString();
        }
        public void StringToText(String Value)
        {
            textProperties.StringToText(Value);
        }
    }

    class CRuntimeDinamicText : CRuntimeCustomField, ICustomDinamicText
    {       
        protected CCustomDinamicText customDinamicText;        
        /*!
         * Construtor
         * @param AOwner      
         * @param Project         
         */
        public CRuntimeDinamicText(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customDinamicText = new CCustomDinamicText(this.pictureBox);
            this.DinamicType = CDinamicType.dtSequence;            
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);            
        }

        public CDinamicType DinamicType { get; set; }
        /*!
         * Adiciona nova zona de texto.
         * @return Referencia para nova zona.
         */
        public ICustomTextZone AddZone()
        {
            CRuntimeDinamicTextZone textZone;
            textZone = new CRuntimeDinamicTextZone(this, project);
            ObjectList.Add(textZone);
            return textZone;
        }
        /*!
        * Evento OnPaint
        * @param sender
        * @param e
        */
        protected void pictureBox_Paint(object sender, PaintEventArgs e)
        {            
            if (DinamicType == CDinamicType.dtSequence)
            {
                int index = int.Parse(m_value);
                if (index >= 0 && index < ObjectList.Count)
                {
                    CRuntimeDinamicTextZone textZone = (CRuntimeDinamicTextZone)ObjectList[index];
                    customDinamicText.DrawTextZone(e.Graphics, textZone);
                }
                else
                {
                    DrawIndexOutOfRange(e.Graphics);
                }
            }
            else
            {                
                float fValue = float.Parse(m_value);
                CRuntimeDinamicTextZone[] arrayZone = (CRuntimeDinamicTextZone[])ObjectList.ToArray();
                var zone = (from obj in arrayZone select obj).Where(obj => (obj.MinValue >= fValue  && obj.MaxValue <= fValue));
                if (zone != null)
                {
                    CRuntimeDinamicTextZone textZone = (CRuntimeDinamicTextZone)zone;
                    customDinamicText.DrawTextZone(e.Graphics, textZone);
                }
                else
                {
                    DrawIndexOutOfRange(e.Graphics);
                }
            }
        }
        /*!
         * 
         */
        private void DrawIndexOutOfRange(Graphics g)
        {
            pictureBox.BackColor = Color.White;
            Font font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            int y = (pictureBox.Height - font.Height * 2) / 2;
            Rectangle rect = new Rectangle(0, y, pictureBox.Width, font.Height * 2);
            Brush brush = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            g.DrawString("Index out of range.\n" + m_value, font, brush, rect, stringFormat);
            stringFormat.Dispose();
            brush.Dispose();
            font.Dispose();
        }        
    }
}
