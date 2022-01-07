using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.CustomControls;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    /*!
     * 
     * 
     */
    class CRuntimeRadioButton : CRuntimeScreenObject, ICustomRadioButton
    {        
        private CCustomRadioButton m_customRadioButton;
        /*!
         * 
         */
        public CRuntimeRadioButton(Object AOwner, CRuntimeProject Project, Panel Panel, int Index)
            : base(AOwner, Project)
        {
            this.m_customRadioButton = new CCustomRadioButton((CRuntimeRadioGroup)AOwner, Index);
            this.pictureBox = new SelectablePictureBox();            
            this.pictureBox.Parent = Panel;
            this.pictureBox.Anchor = AnchorStyles.None;            
            this.pictureBox.BringToFront();
            this.pictureBox.Show();
            SelectablePictureBox selectablePictureBox = (SelectablePictureBox)this.pictureBox;
            selectablePictureBox.Click += new EventHandler(this.pictureBox_Click);
            selectablePictureBox.MouseDown += new MouseEventHandler(this.pictureBox_MouseDown);
            selectablePictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);
            selectablePictureBox.KeyUp += new KeyEventHandler(this.pictureBox_KeyUp);
        }
        //!
        public bool Checked
        {
            get { return this.m_customRadioButton._checked; }
            set { this.m_customRadioButton._checked = value; }
        }
        //!
        public string Caption
        {
            get { return this.m_customRadioButton.caption; }
            set { this.m_customRadioButton.caption = value; }
        }
        //!        
        public int Y
        {
            get { return this.m_customRadioButton.y; }
            set { this.m_customRadioButton.y = value; }
        }
        public event EventHandler Click;
        public event KeyEventHandler KeyUp;
        /*!
         * 
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            CRuntimeRadioGroup rg = (CRuntimeRadioGroup)Owner;
            CCustomRadioButton.DrawRadioButton(e.Graphics, pictureBox,
                this, 0, rg.Font, rg.FontColor);            
        }
        /*!
         * 
         */
        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }
        /*!
         * 
         */
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {            
            pictureBox.Invalidate();
        }
        /*!
         *  
         */
        private void pictureBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUp != null)            
                KeyUp(this, e);           
        }
        /*!
         *  
         */
        public override void LinkObjects() { }
    }
    /*!
     * 
     * 
     */
    public class CRuntimeRadioGroup : CRuntimeCustomField, ICustomRadioGroup
    {
        private CCustomRadioGroup m_customRadioGroup;                
        private int m_index;
        private Panel m_panel;
        /*!
         * Construtor
         * @param AOwner      
         * @param Project         
         */
        public CRuntimeRadioGroup(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {            
            this.m_customRadioGroup = new CCustomRadioGroup();
            this.m_panel = new Panel();
            this.pictureBox = new SelectablePictureBox();
            this.m_panel.Parent = this.pictureBox;
            this.m_panel.Dock = DockStyle.None;            
            SelectablePictureBox selectablePictureBox = (SelectablePictureBox)this.pictureBox;
            selectablePictureBox.Enter += new EventHandler(this.pictureBox_Enter);
            selectablePictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);            
        }
        
        #region Propriedades
        //!
        public override CFieldType FieldType
        {
            get { return CFieldType.ftReadWrite; }
        }
        //!
        public Font Font
        {
            get { return m_customRadioGroup.font; }
            set
            {
                m_customRadioGroup.font = value;
                //pictureBox.Invalidate();
            }
        }
        //!
        public Color FontColor
        {
            get { return m_customRadioGroup.fontColor; }
            set
            {
                m_customRadioGroup.fontColor = value;
                //pictureBox.Invalidate();
            }
        }
        //!
        public int Count
        {
            get { return ObjectList.Count; }
            set
            {
                SetCount(value);
                //pictureBox.Invalidate();
            }
        }
        //!TODO:implementar
        public int CheckedButtonIndex { get; set; }
        //!
        public ICustomRadioButton[] Items
        {
            get 
            {
                IEnumerable<CRuntimeRadioButton> subset = ObjectList.OfType<CRuntimeRadioButton>();
                return subset.ToArray();                
            }            
        }
        //!
        public override int TabIndex
        {
            get { return pictureBox.TabIndex; }
        }
        //!
        public override event EventHandler Enter;
        #endregion

        #region Funções
        /*!
         * Seta item
         */
        private void CheckItem(int j)
        {            
            for (int i = 0; i < Items.Count(); i++)
            {
                Items[i].Checked = (i == j);
            }            
        }
        /*!
         *  
         */
        private void radioButton_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        if (m_index < Count - 1)
                            m_index++;
                        else
                            m_index = 0;
                        CheckItem(m_index);
                    };break;
                case Keys.Down:
                    {
                        if (m_index >= 0)
                            m_index--;
                        else
                            m_index = Count-1;
                        CheckItem(m_index);
                    };break;
            }
            e.Handled = true;
            pictureBox.Invalidate(true); 
        }
        /*!
         * 
         */
        private void radioButton_Click(object sender, EventArgs e)
        {
            m_index = ObjectList.IndexOf(sender);
            CheckItem(m_index);
            string value = m_index.ToString();
            OnEditValue(new FieldEditValueEventArgs(value));
            pictureBox.Invalidate(true);
        }
        /*!
         *  
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            m_customRadioGroup.DrawRadioGroup(e.Graphics, pictureBox, Items.ToList());
            for (int i = 0; i < Items.Count(); i++)
            {
                Items[i].Y = 0;
                Items[i].Left = 0;
                Items[i].Top = (i * m_customRadioGroup.m_h);
                Items[i].Width = m_panel.Width;
                Items[i].Height = m_customRadioGroup.m_h;                                                           
            }
        }
        /*!
         * 
         */
        private void pictureBox_Enter(object sender, EventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }        
        /*!
         * 
         */
        public void SetCount(int Value)
        {
            if (Value >= CCustomRadioGroup.MIN_COUNT)
            {
                if (ObjectList.Count < Value)
                {
                    while (ObjectList.Count < Value)
                    {
                        CRuntimeRadioButton rb = new CRuntimeRadioButton(this, project, m_panel, ObjectList.Count + 1);
                        rb.Click += new EventHandler(this.radioButton_Click);
                        rb.KeyUp += new KeyEventHandler(this.radioButton_KeyUp);
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
        /*!
         * 
         */
        public override void LinkObjects()
        {
            m_panel.Left = CCustomRadioGroup.BORDER_WIDTH;
            m_panel.Top = CCustomRadioGroup.BORDER_WIDTH;
            m_panel.Width = Width - (CCustomRadioGroup.BORDER_WIDTH * 2);
            m_panel.Height = Height - (CCustomRadioGroup.BORDER_WIDTH * 2);
        }
        #endregion
    }
}
