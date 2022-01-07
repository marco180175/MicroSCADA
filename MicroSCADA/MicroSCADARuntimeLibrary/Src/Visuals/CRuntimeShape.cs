using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimeShape : CRuntimeScreenObject, ICustomShape
    {
        private CCustomShape customShape;
        public CRuntimeShape(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customShape = new CCustomShape();
            this.pictureBox.BackColor = Color.Transparent;
            this.pictureBox.Paint += new PaintEventHandler(pictureBox_Paint);           
        }
        public CCustomShape CustomShape { get { return this.customShape; } }
        public CShapeType ShapeType
        {
            get { return this.customShape.shapeType; }
            set { this.customShape.shapeType = value; }
        }

        public Color BorderColor
        {
            get { return this.customShape.borderColor; }
            set { this.customShape.borderColor = value; }
        }
        override public Color BackColor
        {
            get { return this.customShape.backColor; }
            set { this.customShape.backColor = value; }
        }
        public int BorderWidth
        {
            get { return this.customShape.borderWidth; }
            set { this.customShape.borderWidth = value; }
        }

        public int Radius
        {
            get { return this.customShape.radius; }
            set { this.customShape.radius = value; }
        }
        /*!
         * Evento OnPaint do PictureBox
         * @param sender Referencia para objeto que chamou o evento
         * @param e Estrutura com parametor do evento
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            customShape.DrawShape(e.Graphics, pictureBox.Width, pictureBox.Height);
        }
        /*!
         * Não é necessario neste objeto
         */
        public override void LinkObjects() { }
    }
}
