using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    public class CDesignShape : CDesignScreenObject, ICustomShape
    {
        protected CCustomShape customShape;        
        public static bool showRadius;       
        public CDesignShape(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.customShape = new CCustomShape();
            this.InitializeObject();
            this.imageIndex = 27;
            this.pictureBox.BackColor = Color.Transparent;
            showRadius = true;            
        }

        ~CDesignShape()
        {
            Dispose();
        }

        public override void Dispose()
        {
            //customShape.D
            
            base.Dispose();
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignShape> subSet = Objects.OfType<CDesignShape>();
            return subSet.ToArray();
        }        
        //!
        public CCustomShape CustomShape
        {
            get { return this.customShape; }
        }
        public CShapeType ShapeType
        {
            get { return this.customShape.shapeType; }
            set 
            { 
                this.customShape.shapeType = value;
                this.pictureBox.Refresh();
            }
        }
        public override Color BackColor
        {
            get { return this.customShape.backColor; }
            set { this.customShape.backColor = value; }
        }

        public Color BorderColor
        {
            get { return this.customShape.borderColor; }
            set { this.customShape.borderColor = value; }
        }

        public int BorderWidth
        {
            get { return this.customShape.borderWidth; }
            set 
            { 
                this.customShape.borderWidth = value;
                this.pictureBox.Refresh();
            }
        }
        
        public int Radius
        {
            get { return this.customShape.radius; }
            set 
            { 
                this.customShape.radius = value;
                this.pictureBox.Invalidate();
            }
        }
        
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            customShape.backColor = BackColor;
            customShape.DrawShape(e.Graphics, pictureBox.Width, pictureBox.Height);
            if(selected)
                DrawSelectedRect(e.Graphics);
        }
        
     
        public override void LinkObjects()
        {
           
        }
    }
}
