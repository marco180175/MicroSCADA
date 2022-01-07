using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimeText : CRuntimeScreenObject, ICustomText
    {
        private CCustomTextProperties textProperties;
        private static int count = 0;       
       
        /*!
         * Construtor
         * @param AOwner
         * @param Node
         * @param Project
         * @param Parent
         */
        public CRuntimeText(Object AOwner,CRuntimeProject Project)
            : base(AOwner,Project)
        {
            this.textProperties = new CCustomTextProperties();          
            this.pictureBox.Paint += new PaintEventHandler(pictureBox_Paint);            
            count++;
        }
        /*!
         * Destrutor
         */
        ~CRuntimeText()
        {
            Dispose();    
        }
        /*!
         * Destrutor
         */
        public override void Dispose()
        {            
            count--;    
 	        base.Dispose();
        }
        public int getCount() { return count; }
        
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
        public StringAlignment Alignment
        {
            get { return this.textProperties.alignment; }
            set { this.textProperties.alignment = value; }
        }
        //public override CBorder Border
        //{
        //    get { return customText.border; }
        //    set
        //    {
        //        customText.border = value;
                
        //    }
        //}
        //!
        
        //public override CFrame Frame
        //{
        //    get { return customText.frame; }
        //    set
        //    {
        //        customText.frame = value;
                
        //    }
        //}
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            CCustomText.DrawText(e.Graphics, pictureBox, textProperties, Border, Frame);
        }
        /*!
         * Não é necessario neste objeto
         */
        public override void LinkObjects() { }

        public String TextToString()
        {
            return textProperties.TextToString();
        }
        public void StringToText(String Value)
        {
            textProperties.StringToText(Value);
        }
    }
}
