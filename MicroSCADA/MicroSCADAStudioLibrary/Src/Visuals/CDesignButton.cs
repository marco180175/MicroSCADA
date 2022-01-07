using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src.TypeConverter;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * 
     */
    public class CDesignButton : CDesignCustomField, ICustomButton, IDesignZones 
    {
        private CCustomButton m_customButton;
        private int m_indexAction;//!< Indice da action        
        /*!
         * Construtor
         * @param AOwner
         * @param Node
         * @param Project
         * @param Parent
         */
        public CDesignButton(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject();   
            this.m_customButton = new CCustomButton();
            this.imageIndex = 28;                     
            this.pictureBox.BackColor = Color.FromArgb(236, 233, 216);
            this.m_indexAction = this.ReferenceList.AddReference();            
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignButton> subSet = Objects.OfType<CDesignButton>();
            return subSet.ToArray();
        }
        #region Propriedades
        //!
        [Category("Appearance")]
        public int BorderWidth 
        {
            get { return this.m_customButton.borderWidth; }
            set { this.m_customButton.borderWidth = value; }
        }
        public CButtonType ButtonType 
        {
            get { return this.m_customButton.buttonType; }
            set { this.m_customButton.buttonType = value; }
        }
        public bool Jog
        {
            get { return this.m_customButton.jog; }
            set { this.m_customButton.jog = value; }
        }
        //!
        [Category("Appearance")]
        public string Text
        {
            get { return this.m_customButton.textProp.text[0]; }
            set { this.m_customButton.textProp.text[0] = value; }
        }
        //!
        [Category("Appearance")]
        public Font TextFont
        {
            get { return this.m_customButton.textProp.font; }
            set { this.m_customButton.textProp.font = value; }
        }
        //!
        [Category("Appearance")]
        public Color TextFontColor
        {
            get { return this.m_customButton.textProp.fontColor; }
            set { this.m_customButton.textProp.fontColor = value; }
        }
        //!
        [Category("Appearance")]
        public StringAlignment Alignment
        {
            get { return this.m_customButton.textProp.alignment; }
            set { this.m_customButton.textProp.alignment = value; }
        }
        //!
        [Browsable(false)]
        public ICustomAction ActionClick
        {
            get { return this.ActionClickEx; }
            set { this.ActionClickEx = (CDesignAction)value; }
        }
        //!
        [EditorAttribute(typeof(CActionTypeConverter), typeof(System.Drawing.Design.UITypeEditor))]
        public CDesignAction ActionClickEx
        {
            get { return this.GetAction(); }
            set { this.SetAction(value); }
        }        
        //!
        [Category("Tags")]
        public int ValueOff
        {
            get { return this.m_customButton.valueOff; }
            set { this.m_customButton.valueOff = value; }
        }
        //!
        [Category("Tags")]
        public int ValueOn
        {
            get { return this.m_customButton.valueOn; }
            set { this.m_customButton.valueOn = value; }
        }
        //!
        [Browsable(false)]
        public int ZoneCount
        {
            get { return 2; }
        }
        //! leitura ou leitura e escrita
        [ReadOnly(true)]
        public override CFieldType FieldType
        {
            get { return CFieldType.ftReadWrite; }            
        }
        #endregion
        /*!
         * 
         */
        private void SetAction(CDesignAction Value)
        {
            this.SetReference(m_indexAction, Value);
        }
        /*!
         * 
         */
        private CDesignAction GetAction()
        {
            return (CDesignAction)this.GetReference(m_indexAction);
        }
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            m_customButton.DrawButton(e.Graphics, pictureBox, false);
            if (selected)
                DrawSelectedRect(e.Graphics);
            if (tabOrder)
                base.pictureBox_Paint(sender, e);
        }
        /*!
         * 
         */
        public Bitmap GetZone(int index)
        {
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(bitmap);
            if(index == 0)
                m_customButton.DrawButton(g, pictureBox, false);
            else
                m_customButton.DrawButton(g, pictureBox, true);
            return bitmap;
        }
        /*!
         * 
         */
        public void SetGuidAction(Guid Value)
        {
            SetReferenceGuid(m_indexAction, Value);
        }        
        /*!
         * 
         */
        public override void LinkObjects()
        {            
            Object obj;
            //Guid guid = GetReferenceGuid(indexTagValue);
            ////
            //if (CHashObjects.ObjectDictionary.ContainsKey(guid))
            //    obj = CHashObjects.ObjectDictionary[guid];
            //else
            //    obj = null;            
            //SetReference(indexTagValue, obj);
            //
            base.LinkObjects();
            //
            Guid guid = GetReferenceGuid(m_indexAction);            
            if (CHashObjects.ObjectDictionary.ContainsKey(guid))
                obj = CHashObjects.ObjectDictionary[guid];
            else
                obj = null;            
            SetReference(m_indexAction, obj);
        }
        /*!
         * 
         */
        public CDesignSystem GetThis()
        {
            return this;
        }
    }
}
