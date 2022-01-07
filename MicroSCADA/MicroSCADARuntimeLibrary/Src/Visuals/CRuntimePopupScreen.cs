using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimePopupScreen : CRuntimeCustomScreen, ICustomPopUpScreen
    {
        private int left;
        private int top;
        private int width;
        private int height;
        private Form m_form;
        private IWin32Window m_owner;
        public CRuntimePopupScreen(Object AOwner, CRuntimeProject Project, IWin32Window Owner)
            : base(AOwner, Project)
        {
            this.m_owner = Owner;
            this.width = 640;
            this.height = 480;           
        }
               
        public bool ShowTitleBar { get; set; }
        
        public string Title { get; set; }
                
        protected override int getLeft() { return this.left; }
        protected override void setLeft(int Value) { this.left = Value; }
        protected override int getTop() { return this.top; }
        protected override void setTop(int Value) { this.top = Value; }
        protected override int getWidth() { return this.width; }
        protected override void setWidth(int Value) { this.width = Value; }
        protected override int getHeight() { return this.height; }
        protected override void setHeight(int Value) { this.height = Value; }

        public void Show()
        {
            m_form = new Form();
            m_form.BackColor = BackColor;
            m_form.Left = left;
            m_form.Top = top;
            m_form.Width = width;
            m_form.Height = height;
            m_form.Text = Title;
            m_form.StartPosition = FormStartPosition.CenterParent;
            SetParent();
            m_form.ShowDialog(m_owner);
        }

        private void SetParent()
        {
            foreach (CRuntimeScreenObject screenObject in ObjectList)                          
                screenObject.getPictureBox().Parent = m_form;            
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
