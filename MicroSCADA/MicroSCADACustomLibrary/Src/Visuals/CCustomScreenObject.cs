using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    /*!
     * Tipo de borda
     */
    public enum CBorder
    {
        None,
        Single        
    }
    /*!
     * Tipo de moldura
     */
    public enum CFrame
    {
        None,
        Inner3D,
        Outer3D
    }
    /*!
     * Interface para objetos visuais
     */
    public interface ICustomScreenObject : ICustomSystem, ICustomLocation, ICustomSize
    {
        //int Left { get; set; }
        //int Top { get; set; }
        //int Width { get; set; }
        //int Height { get; set; }
        Color BackColor { get; set; }
        CBorder Border { get; set; }
        CFrame Frame { get; set; }
    }

    public sealed class CCustomScreenObject
    {
        public Color backColor;
        public CBorder border;
        public CFrame frame;
        /*!
         * 
         */
        public static void DrawBorder(Graphics graphics, PictureBox pictureBox, CBorder border, CFrame frame)
        {
            switch (border)
            {
                case CBorder.Single:
                    {
                        graphics.DrawRectangle(new Pen(Color.Black), 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
                        switch (frame)
                        {
                            case CFrame.Inner3D:
                                ControlPaint.DrawBorder3D(graphics, 1, 1, pictureBox.Width - 2, pictureBox.Height - 2, Border3DStyle.Raised);
                                break;
                            case CFrame.Outer3D:
                                ControlPaint.DrawBorder3D(graphics, 1, 1, pictureBox.Width - 2, pictureBox.Height - 2, Border3DStyle.Sunken);
                                break;
                            default:
                                break;
                        }
                    }; break;
                default:
                    {
                        switch (frame)
                        {
                            case CFrame.Inner3D:
                                ControlPaint.DrawBorder3D(graphics, 0, 0, pictureBox.Width, pictureBox.Height, Border3DStyle.Raised);
                                break;
                            case CFrame.Outer3D:
                                ControlPaint.DrawBorder3D(graphics, 0, 0, pictureBox.Width, pictureBox.Height, Border3DStyle.Sunken);
                                break;
                            default:
                                break;
                        }
                    }; break;
            } 
        }       
    }      
}
