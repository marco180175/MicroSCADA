using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public enum CShapeType
    {
        Rectangle,
        RoundRect,
        Elipse
    }

    public interface ICustomShape : ICustomScreenObject
    {
        CCustomShape CustomShape { get; }
        Color BorderColor { get; set; }
        int BorderWidth { get; set; }
        int Radius { get; set; }
        CShapeType ShapeType { get; set; }
    }

    public class CCustomShape
    {
        public CShapeType shapeType;
        public Color borderColor;
        public Color backColor;
        public int borderWidth;
        public int radius;
        public CCustomShape()
        {
            this.borderColor = Color.Black;
            this.borderWidth = 1;
            this.radius = 20;
            this.shapeType = CShapeType.Rectangle;
        }

        public void DrawShape(Graphics graphics, float width, float height)
        {
            float x = (borderWidth / 2);
            float y = (borderWidth / 2);            
            width = width - borderWidth;
            height = height - borderWidth;
            Pen pen = new Pen(borderColor, borderWidth);
            switch (shapeType)
            {
                case CShapeType.Rectangle:
                    {
                        graphics.FillRectangle(new SolidBrush(backColor), x, y, width, height);
                        graphics.DrawRectangle(pen, x, y, width, height);
                    }; break;
                case CShapeType.RoundRect:
                    {
                        int r = radius;
                        System.Drawing.Drawing2D.GraphicsPath path;
                        path = new System.Drawing.Drawing2D.GraphicsPath();
                        
                        //raio superior esquerdo
                        path.AddArc(x, y, 2 * r, 2 * r, 180, 90);
                        //linha superior
                        path.AddLine(r, y, width - r + x + 1, y);
                        //raio superior direito
                        path.AddArc(width + x - 2 * r, y - 1, 2 * r, 2 * r, 270, 90);
                        //linha direita
                        path.AddLine(width + x, r, width + x, height + y + 1 - r);
                        //raio inferior direito
                        path.AddArc(width + x - (2 * r), height + y - (2 * r), 2 * r, 2 * r, 0, 90);
                        //linha inferior (desenhada da direita para esquerda)
                        path.AddLine(width - r + x + 1, height + y, r, height + y);
                        //raio inferior esquerdo
                        path.AddArc(x, height + y - (2 * r), 2 * r, 2 * r, 90, 90);
                        //linha esquerda (desenhada de cima para baixo)
                        path.AddLine(x, height + y - r + 1, x, r);
                        //
                        graphics.FillPath(new SolidBrush(backColor), path);
                        graphics.DrawPath(pen, path);
                        path.Dispose();
                    }; break;
                case CShapeType.Elipse:
                    {
                        Rectangle rect = new Rectangle(0, 0, (int)(width + borderWidth), (int)(height + borderWidth));
                        
                        if ((borderWidth % 2) == 0)
                        {
                            x -= 0.5F;
                            y -= 0.5F;
                        }                        
                        graphics.FillRectangle(new SolidBrush(Color.Transparent), rect);                                                
                        graphics.FillEllipse(new SolidBrush(backColor), x, y, width - 1, height - 1);
                        graphics.DrawEllipse(pen, x, y, width - 1, height - 1);
                    }; break;
            }            
        }
        
    }
}
