using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MicroSCADARuntimeLibrary.Src.CustomControls
{
    /*!
     * Extende pictureBox para receber tab e mostrar focu
     */
    public class SelectablePictureBox : PictureBox
    {
        public SelectablePictureBox()
        {
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }
        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.Focused)
            {
                Pen pen = new Pen(Color.Black, 3);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                pe.Graphics.DrawRectangle(pen, new Rectangle(3, 3, Width - 6, Height - 6));

                //var rc = this.ClientRectangle;
                //rc.Inflate(-2, -2);
                //ControlPaint.DrawFocusRectangle(pe.Graphics, rc);
            }
        }        
    }
}
