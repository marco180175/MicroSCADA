using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src.Forms;
using MicroSCADAStudioLibrary.Src.TypeConverter;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    public class CDesignText : CDesignScreenObject, ICustomText
    {
        private CCustomTextProperties textProperties;
        //private CCustomText customText;      
       
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         * @param Parent
         */
        public CDesignText(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject();
            this.textProperties = new CCustomTextProperties();                         
            this.DoubleClick += new EventHandler(Project.DoubleClick);
            this.imageIndex = 26;
        }
        /*!
         * Destrutor
         */
        //~CDesignText()
        //{            
        //    Dispose();    
        //}
       
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
                
        
        
        //!Propriedade texto
        [ActionProperty]
        [Editor(typeof(CTextEditorPreset), typeof(UITypeEditor))]
        public String Text
        {
            get { return this.textProperties.TextToString(); }
            set 
            { 
                this.textProperties.StringToText(value);
                this.pictureBox.Invalidate();
            }
        }
        //!
        [Category("Appearance")]
        public Font TextFont
        {
            get { return this.textProperties.font; }
            set 
            { 
                this.textProperties.font = value;
                this.pictureBox.Invalidate();
            }
        }
        //!
        [Category("Appearance")]
        [ActionProperty]
        public Color TextFontColor
        {
            get { return this.textProperties.fontColor; }
            set 
            { 
                this.textProperties.fontColor = value;
                this.pictureBox.Invalidate();
            }
        }
        //!
        [Category("Appearance")]
        public StringAlignment Alignment
        {
            get { return this.textProperties.alignment; }
            set 
            { 
                this.textProperties.alignment = value;
                this.pictureBox.Invalidate();
            }
        }
        //!
        [Category("Appearance")]
        public override CBorder Border
        {
            get { return border; }
            set
            {
                border = value;
                pictureBox.Invalidate();
            }
        }
        //!
        [Category("Appearance")]
        public override CFrame Frame
        {
            get { return frame; }
            set
            {
                frame = value;
                pictureBox.Invalidate();
            }
        }
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            CCustomText.DrawText(e.Graphics, pictureBox, textProperties, border, frame);
            if(selected)
                DrawSelectedRect(e.Graphics);                    
        }               
        
        public String TextToString()
        {
            return textProperties.TextToString();
        }
        public void StringToText(String Value)
        {
            textProperties.StringToText(Value);
            pictureBox.Invalidate();
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
