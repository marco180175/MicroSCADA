using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    public class CDesignCheckBox : CDesignCustomField, ICustomCheckBox
    {
        private CCustomCheckBox m_customCheckBox;
        public CDesignCheckBox(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject();
            this.m_customCheckBox = new CCustomCheckBox();
            this.pictureBox.BackColor = Color.FromArgb(236, 233, 216);
            this.imageIndex = 41;
        }
        #region Propriedades
        //!
        public bool Checked
        {
            get { return m_customCheckBox.boxChecked; }
            set 
            { 
                m_customCheckBox.boxChecked = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public string Caption
        {
            get { return m_customCheckBox.caption; }
            set 
            { 
                m_customCheckBox.caption = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public Font Font
        {
            get { return m_customCheckBox.font; }
            set 
            { 
                m_customCheckBox.font = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public Color FontColor
        {
            get { return m_customCheckBox.fontColor; }
            set 
            { 
                m_customCheckBox.fontColor = value;
                pictureBox.Invalidate();
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
            m_customCheckBox.DrawCheckBox(e.Graphics, pictureBox);
            if (selected)
                DrawSelectedRect(e.Graphics);
            if (tabOrder)
                base.pictureBox_Paint(sender, e);
        }        
        #endregion
    }
}
