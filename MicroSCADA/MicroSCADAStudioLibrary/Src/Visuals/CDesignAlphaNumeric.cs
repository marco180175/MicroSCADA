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
using MicroSCADAStudioLibrary.Src.TypeConverter;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * Campo alfanumerico 
     */
    public class CDesignAlphaNumeric : CDesignCustomField, ICustomAlphaNumeric
    {
        private CCustomAlphaNumeric customAlphaNumeric;        
        
        /*!
         * Construtor
         * @param AOwner
         * @param Node
         * @param Project
         * @param Parent
         */
        public CDesignAlphaNumeric(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject();
            this.customAlphaNumeric = new CCustomAlphaNumeric();                        
            this.imageIndex = 14;                     
        }
        /*!
         * Destrutor
         */
        //~CDesignAlphaNumeric()
        //{
        //    Dispose();    
        //}
        /*!
         * Destrutor
         */
        public override void Dispose()
        {            
            this.customAlphaNumeric.Dispose();           
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
            IEnumerable<CDesignAlphaNumeric> subSet = Objects.OfType<CDesignAlphaNumeric>();
            return subSet.ToArray();
        }
        
        #region Propriedades
        //!
        [Category("Appearance")]
        public HorizontalAlignment TextAlign
        {
            get { return customAlphaNumeric.textAlign; }
            set
            {
                customAlphaNumeric.textAlign = value;
                pictureBox.Invalidate();
            }
        } 
        //!
        public CValueFormat ValueFormat
        {
            get { return this.customAlphaNumeric.valueFormat; }
            set { this.customAlphaNumeric.valueFormat = value; }
        }
        public int DecimalCount
        {
            get { return this.customAlphaNumeric.decimalCount; }
            set 
            { 
                this.customAlphaNumeric.decimalCount = value;
                this.pictureBox.Invalidate();
            }
        }
        public double MaxValue
        {
            get { return this.customAlphaNumeric.maxValue; }
            set { this.customAlphaNumeric.maxValue = value; }
        }
        public double MinValue
        {
            get { return this.customAlphaNumeric.minValue; }
            set { this.customAlphaNumeric.minValue = value; }
        }
        //!
        [Category("Appearance")]
        public Font Font
        {
            get { return this.customAlphaNumeric.font; }
            set { this.customAlphaNumeric.font = value; }
        }
        //!
        [Category("Appearance")]
        public Color FontColor
        {
            get { return this.customAlphaNumeric.fontColor; }
            set { this.customAlphaNumeric.fontColor = value; }
        }
        //!
        //[Category("Appearance")]
        //public StringAlignment Alignment
        //{
        //    get { return this.customAlphaNumeric.alignment; }
        //    set
        //    {
        //        this.customAlphaNumeric.alignment = value;
        //        this.pictureBox.Refresh();
        //    }
        //}
        #endregion
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         * Formata exsibição do valor do tag
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {            
            if (TagValue != null)
            {
                string fValue = customAlphaNumeric.FormatValue(TagValue.Value, TagValue.DataType);
                customAlphaNumeric.DrawAlfaNumeric(e.Graphics,pictureBox, fValue);                
            }
            else
                customAlphaNumeric.DrawAlfaNumeric(e.Graphics,pictureBox, "null");
            //
            if (selected)
                DrawSelectedRect(e.Graphics);
            if (tabOrder)
                base.pictureBox_Paint(sender, e);
        }                
    }
    
}
