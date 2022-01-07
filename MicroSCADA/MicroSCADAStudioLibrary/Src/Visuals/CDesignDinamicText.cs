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
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADAStudioLibrary.Src.TypeConverter;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    
    /*!
     * Texto dinamico
     */
    public class CDesignDinamicText : CDesignCustomField, ICustomDinamicText, IDesignCollection,
                                      IDesignZoneCollection
    {
        private CCustomDinamicText customDinamicText;        
        public CDesignDinamicText(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject();           
            this.customDinamicText = new CCustomDinamicText(this.pictureBox);
            this.DinamicType = CDinamicType.dtSequence;
            this.imageIndex = 35;            
        }
        #region Propriedades
        //!
        [Browsable(false)]
        new public Color BackColor
        {
            get { return this.pictureBox.BackColor; }
            set { this.pictureBox.BackColor = value; }
        }
        //!
        [ReadOnly(true)]
        public override CFieldType FieldType
        {
            get { return CFieldType.ftRead; }            
        }
        //! Incremento ou Range
        public CDinamicType DinamicType { get; set; }
        //! Numero de items
        public int ZoneCount
        {
            get { return this.ObjectList.Count; }
        }
        //! Lista de items
        [Editor(typeof(CollectionEditorPreset), typeof(UITypeEditor))]
        public ArrayList Items
        {
            get { return ObjectList; }
        }       
        #endregion
        /*!
         * Evento OnPaint
         * @param sender Object que chamou o evento
         * @param e Argumentos do evento
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (ObjectList.Count > 0)
            {
                int i;
                if (TagValue != null)
                    i = int.Parse(TagValue.Value);
                else
                    i = 0;
                CDesignDinamicTextZone zone = (CDesignDinamicTextZone)ObjectList[i];
                pictureBox.BackColor = zone.BackColor;
                customDinamicText.DrawTextZone(e.Graphics, zone);
            }
            else
                customDinamicText.DrawTextZone(e.Graphics, null);
            //
            if (selected)
                DrawSelectedRect(e.Graphics);
        }
        /*!
         * Retorna zona desenhada em um bitmap para que seja mostrada
         * na lista de zonas da IDE.
         * @return Referencia para bitmap ou null se não tiver zonas
         */
        public Bitmap GetZone(int index)
        {
            if (index < ObjectList.Count)
            {
                Bitmap bitmap = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(bitmap);
                CDesignDinamicTextZone zone = (CDesignDinamicTextZone)ObjectList[index];
                customDinamicText.DrawTextZone(g, zone);
                return bitmap;
            }
            else
                return null;
        }       
        /*!
         * Adiciona nova zona de texto.
         * @return Referencia para nova zona.
         */
        public ICustomTextZone AddZone()
        {
            CDesignDinamicTextZone textZone;
            textZone = new CDesignDinamicTextZone(this, project);            
            ObjectList.Add(textZone);
            OnAddItem(new AddItemEventArgs(textZone, textZone.ImageIndex));            
            return textZone;
        }
        /*!
         * Adiciona nova zona de texto. IDE
         * @return Referencia para nova zona.
         */
        public CDesignDinamicTextZone AddZoneEx()
        {
            CDesignDinamicTextZone textZone;
            textZone = (CDesignDinamicTextZone)AddZone();
            textZone.SetGUID(Guid.NewGuid());            
            textZone.Name = "TextZone" + ObjectList.Count.ToString();
            textZone.StringToText(textZone.Name);
            textZone.MaxValue = ObjectList.Count - 1;
            textZone.MinValue = ObjectList.Count - 1;
            return textZone;
        }
        /*!
         * 
         */ 
        public IDesignCollectionItem NewItem()
        {
            return (IDesignCollectionItem)AddZoneEx();
        }        
        /*!
         * 
         */        
        public CDesignSystem GetThis()
        {
            return this;
        }
        
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignDinamicText> subSet = Objects.OfType<CDesignDinamicText>();
            return subSet.ToArray();
        }
    }

    /*!
     * Zona do texto dinamico
     */
    //[TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class CDesignDinamicTextZone : CDesignSystem, ICustomTextZone, IDesignCollectionItem
    {
        private CCustomTextProperties m_textProperties;
        private float m_maxValue;
        private float m_minValue;
        public CDesignDinamicTextZone(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.m_textProperties = new CCustomTextProperties();
            this.imageIndex = 26;
        }
        #region Propriedades
        [Category("Ranges")]
        public float MaxValue
        {
            get { return this.m_maxValue; }
            set { this.m_maxValue = value; }
        }
        [Category("Ranges")]
        public float MinValue
        {
            get { return this.m_minValue; }
            set { this.m_minValue = value; }
        }
        //!
        [Editor(typeof(CTextEditorPreset), typeof(UITypeEditor))]
        public String Text
        {
            get { return this.m_textProperties.TextToString(); }
            set { this.m_textProperties.StringToText(value); }
        }

        public Font TextFont
        {
            get { return this.m_textProperties.font; }
            set { this.m_textProperties.font = value; }
        }
        public Color TextFontColor
        {
            get { return this.m_textProperties.fontColor; }
            set { this.m_textProperties.fontColor = value; }
        }
        public Color BackColor
        {
            get { return this.m_textProperties.backColor; }
            set { this.m_textProperties.backColor = value; }
        }
        public StringAlignment Alignment
        {
            get { return this.m_textProperties.alignment; }
            set { this.m_textProperties.alignment = value; }
        }
        #endregion
        public String TextToString()
        {
            return m_textProperties.TextToString();
        }
        public void StringToText(String Value)
        {
            m_textProperties.StringToText(Value);
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignDinamicTextZone> subSet = Objects.OfType<CDesignDinamicTextZone>();
            return subSet.ToArray();
        }
    }
}
