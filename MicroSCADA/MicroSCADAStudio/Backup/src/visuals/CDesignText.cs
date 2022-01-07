using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using MicroSCADACustomLibrary.Visuals;

namespace MicroSCADAStudio.Src.Visuals
{
    public class CDesignText : CDesignScreenObject, ICustomText
    {
        protected CCustomText customText;      
       
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         * @param Parent
         */
        public CDesignText(Object AOwner, CDesignProject Project, Control Parent, ArrayList OwnerList)
            : base(AOwner, Project, Parent, OwnerList)
        {
            this.customText = new CCustomText();              
            this.InitializeObject();
            this.DoubleClick += new EventHandler(Project.DoubleClick);
        }
        /*!
         * Destrutor
         */
        ~CDesignText()
        {            
            Dispose();    
        }
        /*!
         * Destrutor
         */
        public override void Dispose()
        {
            
 	        base.Dispose();
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignText> subSet = Objects.OfType<CDesignText>();
            return subSet.ToArray();
        }
        [Browsable(false)]
        public CCustomText CustomText
        {
            get { return this.customText; }
        }
        //!Propriedade texto
        [ActionProperty]
        public String[] Text
        {
            get { return this.customText.textProperties.text; }
            set 
            { 
                this.customText.textProperties.text = value;
                this.pictureBox.Invalidate();
            }
        }
        public Font TextFont
        {
            get { return this.customText.textProperties.font; }
            set 
            { 
                this.customText.textProperties.font = value;
                this.pictureBox.Invalidate();
            }
        }
        [ActionProperty]
        public Color TextFontColor
        {
            get { return this.customText.textProperties.fontColor; }
            set 
            { 
                this.customText.textProperties.fontColor = value;
                this.pictureBox.Invalidate();
            }
        }
        public StringAlignment Alignment
        {
            get { return this.customText.textProperties.alignment; }
            set 
            { 
                this.customText.textProperties.alignment = value;
                this.pictureBox.Invalidate();
            }
        }        
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            //Bitmap bmp = new Bitmap(width, height,System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            customText.DrawText(e.Graphics, width, height);

            //Graphics gBmp = Graphics.FromImage(bmp);
            //gBmp.Clear(BackColor);
            //customText.DrawText(gBmp, width, height);
            
            //pictureBox.Width = (int)(width * zoomScale);
            //pictureBox.Height = (int)(height * zoomScale);            
            //e.Graphics.DrawImage(bmp, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            
            if(selected)
                DrawSelectedRect(e.Graphics);
            //gBmp.Dispose();
            //bmp.Dispose();            
        }        
        
        public override void LinkObjects()
        {

        }
        public String TextToString()
        {
            return customText.textProperties.TextToString();
        }
        public void StringToText(String Value)
        {
            customText.textProperties.StringToText(Value);
        }
        protected override void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(EventArgs.Empty);
        }
        public event EventHandler DoubleClick;
        private void OnDoubleClick(EventArgs e)
        {
            if (DoubleClick != null)
                DoubleClick(this, e);
        }
    }
}
