using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * RadioButton
     */
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class CDesignRadioButton : CDesignSystem, ICustomRadioButton
    {
        private CCustomRadioButton m_customRadioButton;
        /*!
         * 
         */
        public CDesignRadioButton(Object AOwner, CDesignProject Project, Control Parent, int Index)
            : base(AOwner, Project)
        {                     
            this.m_customRadioButton = new CCustomRadioButton((CDesignRadioGroup)AOwner, Index);
        }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        //!
        public bool Checked
        {
            get { return this.m_customRadioButton._checked; }
            set 
            { 
                this.m_customRadioButton._checked = value;
                OnButtonChecked(EventArgs.Empty);
            }
        }
        //!
        public string Caption
        {
            get { return this.m_customRadioButton.caption; }
            set { this.m_customRadioButton.caption = value; }
        }
        //!
        [Browsable(false)]
        public int Y
        {
            get { return this.m_customRadioButton.y; }
            set { this.m_customRadioButton.y = value; }
        }
        //!
        public event EventHandler ButtonChecked;
        /*!
         * 
         */
        private void OnButtonChecked(EventArgs e)
        {
            if (ButtonChecked != null)
                ButtonChecked(this, e);
        }
    }
    /*!
     * Grupo de RadioButtons
     */
    public class CDesignRadioGroup : CDesignCustomField, ICustomRadioGroup
    {
        private CCustomRadioGroup m_customRadioGroup;        
        /*!
         * Construtor
         */
        public CDesignRadioGroup(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject();                               
            this.m_customRadioGroup = new CCustomRadioGroup();            
            this.pictureBox.BackColor = Color.FromArgb(236, 233, 216);
            this.imageIndex = 42;
            this.SetCount(CCustomRadioGroup.MIN_COUNT); 
        }
        #region Propriedades
        //!
        [ReadOnly(true)]
        public override CFieldType FieldType
        {
            get { return CFieldType.ftReadWrite; }
        }
        //!
        [Category("Appearance")]
        public Font Font
        {
            get { return m_customRadioGroup.font; }
            set
            {
                m_customRadioGroup.font = value;
                pictureBox.Invalidate();
            }
        }
        //!
        [Category("Appearance")]
        public Color FontColor
        {
            get { return m_customRadioGroup.fontColor; }
            set
            {
                m_customRadioGroup.fontColor = value;
                pictureBox.Invalidate();
            }
        }
        //!
        [Category("Items")]
        public int Count
        {
            get 
            { 
                return ObjectList.Count; 
            }
            set 
            {                 
                SetCount(value);
                pictureBox.Invalidate();
            }
        }
        //!
        [Category("Items")]
        public int CheckedButtonIndex
        {
            get { return GetCheckedItem(); }
            set 
            {
                SetCheckedItem(value);
                pictureBox.Invalidate();
            }
        }
        //!
        [Category("Items")]
        public CDesignRadioButton[] Buttons
        {
            get 
            {
                IEnumerable<CDesignRadioButton> subset = ObjectList.OfType<CDesignRadioButton>();
                return subset.ToArray();                
            }
        }
        //!    
        [Browsable(false)]
        public ICustomRadioButton[] Items
        {
            get 
            {
                IEnumerable<CDesignRadioButton> subset = ObjectList.OfType<CDesignRadioButton>();
                return subset.ToArray();                
            }
        }
        #endregion
        #region Funções
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            m_customRadioGroup.DrawRadioGroup(e.Graphics, pictureBox, Items.ToList()); 
            //
            for (int i = 0; i < Items.Count(); i++)
            {
                Items[i].Y = i * m_customRadioGroup.m_h;
                Items[i].Left = CCustomRadioGroup.BORDER_WIDTH;
                Items[i].Top = (i * m_customRadioGroup.m_h);
                Items[i].Width = Width - (CCustomRadioGroup.BORDER_WIDTH * 2);
                Items[i].Height = m_customRadioGroup.m_h;    
                CCustomRadioButton.DrawRadioButton(e.Graphics,
                    pictureBox, Items[i], CCustomRadioGroup.BORDER_WIDTH, Font, FontColor);
            }
            //
            if (selected)
                DrawSelectedRect(e.Graphics);
            if (tabOrder)
                base.pictureBox_Paint(sender, e);
        }
        /*!
         * Seta e instancia numero de checkbuttons
         * @param Value Numero de checkbuttons
         */
        public void SetCount(int Value)
        {
            if (Value >= CCustomRadioGroup.MIN_COUNT)
            {
                if (ObjectList.Count < Value)
                {
                    while (ObjectList.Count < Value)
                    {
                        CDesignRadioButton rb = new CDesignRadioButton(this, project, null, ObjectList.Count + 1);
                        rb.ButtonChecked += new EventHandler(radiobutton_ButtonChecked);
                        ObjectList.Add(rb);
                    }
                }
                else
                {
                    while (ObjectList.Count > Value)
                        ObjectList.RemoveAt(ObjectList.Count - 1);
                }
            }
        }
        
        private void SetCheckedItem(int Index)
        {
            int j = (int)Index;
            for (int i = 0; i < Buttons.Count(); i++)
            {
                Buttons[i].ButtonChecked -= radiobutton_ButtonChecked;
                Buttons[i].Checked = (i == j);
                Buttons[i].ButtonChecked += new EventHandler(radiobutton_ButtonChecked);
            }
        }

        private int GetCheckedItem()
        {
            for (int i = 0; i < Buttons.Count(); i++)
            {
                if (Buttons[i].Checked)
                    return i;
            }
            return -1;
        }

        private void radiobutton_ButtonChecked(object sender, EventArgs e)
        {
            int j = ObjectList.IndexOf(sender);            
            SetCheckedItem(j);
            pictureBox.Invalidate();
        }
        #endregion
    }
}
