using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    /*!
     * Objeto da tela
     */
    public abstract class CRuntimeScreenObject : CRuntimeView, ICustomScreenObject//, ICustomLocation
    {
        //protected CCustomScreenObject customScreenObject;
        protected PictureBox pictureBox;
        
        public CRuntimeScreenObject(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.pictureBox = new PictureBox();
            //this.customScreenObject = new CCustomScreenObject();
            //focus = false;
        }
        //Propriedades
        protected override int getLeft() { return this.pictureBox.Left; }
        protected override void setLeft(int Value) { this.pictureBox.Left = Value; }
        protected override int getTop() { return this.pictureBox.Top; }
        protected override void setTop(int Value) { this.pictureBox.Top = Value; }
        protected override int getWidth() { return this.pictureBox.Width; }
        protected override void setWidth(int Value) { this.pictureBox.Width = Value; }
        protected override int getHeight() { return this.pictureBox.Height; }
        protected override void setHeight(int Value) { this.pictureBox.Height = Value; }
        public void setParent(Control Parent)
        {
            this.pictureBox.Parent = Parent;
        }
        public virtual PictureBox getPictureBox() { return this.pictureBox; }
        //public bool focus;
        public virtual Color BackColor
        {
            get { return this.pictureBox.BackColor; }
            set { this.pictureBox.BackColor = value; }
        }
        public virtual CBorder Border { get; set; }
        public virtual CFrame Frame { get; set; }
        public abstract void LinkObjects();
    }
}
