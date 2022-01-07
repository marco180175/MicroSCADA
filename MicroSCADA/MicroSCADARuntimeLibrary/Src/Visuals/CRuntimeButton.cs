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
    public class CRuntimeButton : CRuntimeCustomField, ICustomButton
    {
        private CCustomButton m_customButton;
        private bool m_down;        
        private int indexAction;        
        /*!
         * Construtor
         * @param AOwner
         * @param Node
         * @param Project
         * @param Parent
         */
        public CRuntimeButton(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.m_customButton = new CCustomButton();            
            this.m_down = false;
            this.indexAction = this.ReferenceList.AddReference();
            this.pictureBox = new SelectablePictureBox();
            ((SelectablePictureBox)this.pictureBox).Enter += new EventHandler(pictureBox_Enter);
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);            
            this.pictureBox.MouseDown += new MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseUp += new MouseEventHandler(this.pictureBox_MouseUp);
            this.pictureBox.KeyDown += new KeyEventHandler(this.pictureBox_KeyDown);
            this.pictureBox.KeyUp += new KeyEventHandler(this.pictureBox_KeyUp);
        }
        #region Propriedades
        //!
        public override CFieldType FieldType
        {
            get { return CFieldType.ftReadWrite; }
        }
        //!
        public int BorderWidth
        {
            get { return this.m_customButton.borderWidth; }
            set { this.m_customButton.borderWidth = value; }
        }
        //!
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
        public string Text
        {
            get { return this.m_customButton.textProp.text[0]; }
            set { this.m_customButton.textProp.text[0] = value; }
        }
        public Font TextFont
        {
            get { return this.m_customButton.textProp.font; }
            set { this.m_customButton.textProp.font = value; }
        }
        public Color TextFontColor
        {
            get { return this.m_customButton.textProp.fontColor; }
            set { this.m_customButton.textProp.fontColor = value; }
        }
        public StringAlignment Alignment
        {
            get { return this.m_customButton.textProp.alignment; }
            set { this.m_customButton.textProp.alignment = value; }
        }
        public ICustomAction ActionClick
        {
            get { return (ICustomAction)this.GetReference(indexAction); }
            set { this.SetReference(indexAction, value); }
        }
        //!        
        public int ValueOff
        {
            get { return this.m_customButton.valueOff; }
            set { this.m_customButton.valueOff = value; }
        }
        //!        
        public int ValueOn
        {
            get { return this.m_customButton.valueOn; }
            set { this.m_customButton.valueOn = value; }
        }
        public override int TabIndex
        {
            get { return pictureBox.TabIndex; }
        }
        #endregion
        public override event EventHandler Enter;
        private void pictureBox_Enter(object sender, EventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            m_customButton.DrawButton(e.Graphics, pictureBox, m_down);
            //
            //if (focus)
            //    DrawFocusRectangle(e.Graphics);
        }
        /*!
         * Evento 
         * @param sender
         * @param e
         */        
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!m_customButton.jog)
            {
                m_down = true;
                pictureBox.Refresh();
                //
                if (m_customButton.buttonType == CButtonType.btToggle)
                {
                    string value = m_customButton.valueOn.ToString();
                    OnEditValue(new FieldEditValueEventArgs(value));
                }
            }
        }
        /*!
         * Evento 
         * @param sender
         * @param e
         */
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_customButton.jog)
            {
                buttonJogClick();
            }
            else
            {
                m_down = false;
                pictureBox.Refresh();
                //
                if (m_customButton.buttonType == CButtonType.btToggle)
                {
                    string value = m_customButton.valueOff.ToString();
                    OnEditValue(new FieldEditValueEventArgs(value));
                }
                else
                {
                    //
                    if (ActionClick != null)
                    {
                        CRuntimeActionList actionList = (CRuntimeActionList)project.ActionList;
                        actionList.Execute((CRuntimeAction)ActionClick);
                    }
                }
            }       
        }

        private void pictureBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                pictureBox_MouseDown(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
            }
        }

        private void pictureBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                pictureBox_MouseUp(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
            }
        }
        /*!
         * Evento : Muda o estado do  e escreve no tag
         * @param sender
         * @param e
         */
        private void buttonJogClick()
        {            
            m_down ^= true;            
            pictureBox.Invalidate();
            //escreve no tag
            string value;
            if(m_down)
                value = m_customButton.valueOn.ToString();
            else
                value = m_customButton.valueOff.ToString();
            //
            OnEditValue(new FieldEditValueEventArgs(value));
        }
        
        public void SetGuidAction(Guid Value)
        {
            SetReferenceGuid(indexAction, Value);
        }

        public override void LinkObjects()
        {    
            //
            base.LinkObjects();
            //
            Object obj;
            Guid keyGUID = GetReferenceGuid(indexAction);
            if (CHashObjects.ObjectDictionary.ContainsKey(keyGUID))
                obj = CHashObjects.ObjectDictionary[keyGUID];
            else
                obj = null;
            SetReference(indexAction, obj);            
        }
    }
}
