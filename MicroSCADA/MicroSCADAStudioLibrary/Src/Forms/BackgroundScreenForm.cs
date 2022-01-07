using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MicroSCADAStudioLibrary.Src.Forms
{
    public partial class BackgroundScreenForm : Form
    {
        protected int gridStep;
        /*!
         * Construtor customizado recebe tabPage como parametro e seta como parent
         * @param Parent Controle onde form sera docado.
         */
        public BackgroundScreenForm(Control Parent)
        {
            InitializeComponent();
            //Dock form in parent control
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Parent = Parent;
            this.BackColor = Color.White;
            
            // This change control to not flick
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //
            this.gridStep = 20;
        }
        /*!
         * 
         */
        public int GetGridStep() 
        { 
            return this.gridStep; 
        }
        /*!
         * 
         */
        public void SetGridStep(int Value) 
        { 
            this.gridStep = Value;
            this.Invalidate();
        }
        /*!
         * Evento onPaint em background
         */
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            //TODO:escrever codigo aqui
            DrawGrid(e.Graphics);
        }
        /*!
         * Desenha grid
         */
        protected void DrawGrid(Graphics g)
        {
            DrawLineGrid(g);
        }
        /*!
         * Desenha grid de pontos no form
         */
        protected void DrawDotsGrid(Graphics g)
        {
            Rectangle area = new Rectangle(0, 0, Width, Height);
            Size pixelsBetweenDots = new Size(gridStep, gridStep);
            ControlPaint.DrawGrid(g, area, pixelsBetweenDots, Color.Gray);            
        }
        /*!
         * Desenha grid de linhas no form
         */
        protected void DrawLineGrid(Graphics g)
        {
            Pen pen = new Pen(Color.Gray, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            int i, x1, y1, x2, y2, steps;
            //
            steps = Width / gridStep;
            for (i = 1; i < steps; i++)
            {
                x1 = gridStep * i;
                x2 = x1;
                y1 = 0;
                y2 = Height;
                g.DrawLine(pen, x1, y1, x2, y2);
            }
            //
            steps = Height / gridStep;
            for (i = 1; i < steps; i++)
            {
                x1 = 0;
                x2 = Width;
                y1 = gridStep * i;
                y2 = y1;
                g.DrawLine(pen, x1, y1, x2, y2);
            }
            //            
            pen.Dispose();
        }             
    }
}
