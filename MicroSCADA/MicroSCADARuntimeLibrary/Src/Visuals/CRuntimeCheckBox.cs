using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Tags;
using MicroSCADARuntimeLibrary.Src.CustomControls;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimeCheckBox : CRuntimeCustomField, ICustomCheckBox
    {
        private CCustomCheckBox m_customCheckBox;
        /*!
         * Construtor
         * @param AOwner      
         * @param Project         
         */
        public CRuntimeCheckBox(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.m_customCheckBox = new CCustomCheckBox();
            this.pictureBox = new SelectablePictureBox();
            ((SelectablePictureBox)this.pictureBox).Enter += new EventHandler(pictureBox_Enter);
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.Click += new EventHandler(this.pictureBox_Click);
            this.pictureBox.KeyUp += new KeyEventHandler(this.pictureBox_KeyUp);
        }

        #region Propriedades
        //!
        public override CFieldType FieldType
        {
            get { return CFieldType.ftReadWrite; }
        }
        //!
        public bool Checked
        {
            get { return m_customCheckBox.boxChecked; }
            set
            {
                m_customCheckBox.boxChecked = value;
                //pictureBox.Invalidate();
            }
        }
        //!
        public string Caption
        {
            get { return m_customCheckBox.caption; }
            set
            {
                m_customCheckBox.caption = value;
                //pictureBox.Invalidate();
            }
        }
        //!
        public Font Font
        {
            get { return m_customCheckBox.font; }
            set
            {
                m_customCheckBox.font = value;
                //pictureBox.Invalidate();
            }
        }
        //!
        public Color FontColor
        {
            get { return m_customCheckBox.fontColor; }
            set
            {
                m_customCheckBox.fontColor = value;
                //pictureBox.Invalidate();
            }
        }
        public override int TabIndex
        {
            get { return pictureBox.TabIndex; }
        }
        #endregion
        #region EventHandlers
        public override event EventHandler Enter;
        private void pictureBox_Enter(object sender, EventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }
        /*!
         * 
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            m_customCheckBox.DrawCheckBox(e.Graphics, pictureBox);
            //
            //if (focus)
            //    DrawFocusRectangle(e.Graphics);
        }
        /*!
         * Muda o estado do checkbox e escreve no tag
         */
        private void pictureBox_Click(object sender, EventArgs e)
        {
            //pictureBox.Select();//pulo do gato para eventos de teclado
            //SetTabIndex();
            //
            m_customCheckBox.boxChecked ^= true;
            pictureBox.Invalidate();
            //escreve no tag
            if (TagValue != null)
            {
                if (TagValue.DataType == CCustomDataType.dtBool)
                {
                    string value = m_customCheckBox.boxChecked.ToString();
                    OnEditValue(new FieldEditValueEventArgs(value));
                }
            }            
        }

        private void pictureBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                pictureBox_Click(sender, EventArgs.Empty);
            }
        }        
        #endregion


        
    }
}
