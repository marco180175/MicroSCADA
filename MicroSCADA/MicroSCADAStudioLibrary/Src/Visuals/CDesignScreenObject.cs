using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    public enum CRegion
    {
        rgCenter,
        rgUpper,
        rgLower,
        rgLeft,
        rgRight,
        rgUpperLeft,
        rgUpperRight,
        rgLowerLeft,
        rgLowerRight,
        rgNone
    }
    public abstract class CDesignScreenObject : CDesignView, ICustomScreenObject//, ICustomLocation
    {        
        protected PictureBox pictureBox;        
        private Rectangle rgnCenter;
        private Rectangle rgnUpper;
        private Rectangle rgnLower;
        private Rectangle rgnRight;
        private Rectangle rgnLeft;
        private Rectangle rgnUpperRight;
        private Rectangle rgnUpperLeft;
        private Rectangle rgnLowerRight;
        private Rectangle rgnLowerLeft;
        private bool moveObject;
        //protected bool selected;
        //private static bool shift = false;
        private int beginX;
        private int beginY;
        private int beginWidth;
        private int beginHeight;
        private const int RGN_WIDTH = 5;
        //        
        protected Rectangle selRect;
        protected Rectangle clrRect;
        protected CRegion region;
        protected ToolTip toolTip;
        protected CBorder border;
        protected CFrame frame;
        
        /*!
         * Construtor
         * @param AOwner 
         * @param Node
         * @param Project
         * @param Parent
         * @param OwnerList
         */
        public CDesignScreenObject(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project)
        {            
            this.pictureBox = new PictureBox();
            this.pictureBox.Parent = Parent;
            this.pictureBox.BackColor = Color.White;
            
            //this.customScreenObject = new CCustomScreenObject();
            
            this.toolTip = new ToolTip();
                        
            this.moveObject = false;
            this.selected = false;
            this.selRect = new Rectangle();
            this.clrRect = new Rectangle();
            this.rgnCenter = new Rectangle();
            this.rgnUpper = new Rectangle();
            this.rgnLower = new Rectangle();
            this.rgnRight= new Rectangle();
            this.rgnLeft = new Rectangle();
            this.rgnUpperRight = new Rectangle();
            this.rgnUpperLeft = new Rectangle();
            this.rgnLowerRight = new Rectangle();
            this.rgnLowerLeft = new Rectangle();
        }
        //Destrutor
        //~CDesignScreenObject()
        //{
        //    Dispose();
        //}
        /*!
         * 
         */
        public override void Dispose()
        {            
            //this.customScreenObject.Dispose();
            this.pictureBox.Dispose();            
            base.Dispose();
        }        
        //!Propriedades
        [ActionProperty]
        [Category("Location")]
        public int Left
        {
            get { return this.getLeft(); }
            set { this.setLeft(value); }
        }
        [ActionProperty]
        [Category("Location")]
        public int Top
        {
            get { return this.getTop(); }
            set { this.setTop(value); }
        }
        protected override int getLeft() { return this.pictureBox.Left; }
        protected override void setLeft(int Value) { this.pictureBox.Left = Value; }
        protected override int getTop() { return this.pictureBox.Top; }
        protected override void setTop(int Value) { this.pictureBox.Top = Value; }
        protected override int getWidth() 
        {
            this.width = (int)Math.Round(this.pictureBox.Width / zoomScale);            
            return this.width; 
        }
        protected override void setWidth(int Value) 
        {
            this.width = Value;
            this.pictureBox.Width = (int)(this.width * zoomScale); 
        }
        protected override int getHeight() 
        {
            this.height = (int)Math.Round(this.pictureBox.Height / zoomScale);
            return this.height; 
        }
        protected override void setHeight(int Value) 
        {
            this.height = Value;
            this.pictureBox.Height = (int)(this.height * zoomScale); 
        }
        public void BringToFront()
        {
            pictureBox.BringToFront();
        }
        //!
        [Category("Appearance")]
        [ActionProperty]
        public virtual Color BackColor
        {
            get { return this.pictureBox.BackColor; }
            set { this.pictureBox.BackColor = value; }
        }
        //!
        [Category("Appearance")]
        public virtual CBorder Border
        {
            get { return border; }
            set 
            { 
                border = value;
                pictureBox.Invalidate();
            }
        }
        //!
        [Category("Appearance")]
        public virtual CFrame Frame
        {
            get { return frame; }
            set
            {
                frame = value;
                pictureBox.Invalidate();
            }
        }
        /*!
         * TODO: O parametro value sera usado para selecionar varios objetos
         * @param value         
         */
        protected override void SetSelected(bool value)
        {
            selected = value;            
            pictureBox.Invalidate();
            if (selected)            
                OnSelectedObject(new SelectedObjectEventArgs(selected));           
        }
        /*!
         * 
         */
        protected virtual void InitializeObject()
        {
            this.pictureBox.MouseDown += new MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new MouseEventHandler(this.pictureBox_MouseUp);
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.DoubleClick += new EventHandler(this.pictureBox_DoubleClick);
            //this.pictureBox.Click += new EventHandler(this.pictureBox_Click); 
        }

        protected int GetIndex(CDesignScreenObject ScreenObject)
        {
            return ((CDesignBaseScreen)Owner).ObjectList.IndexOf(ScreenObject);
        }
        /*!
         * Eventos do objeto picturebox
         * @param sender
         * @param e
         */
        protected virtual void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (tabOrder)
            {
                if (isOrdenated == false)
                {
                    int index = GetIndex(this);
                    OnChangeTabOrder(new ChangeTabOrderEventArgs(index));
                    isOrdenated = true;
                }
            }
            else
            {
                UpdateRegions();
                region = GetRegion(e.X, e.Y);
                //
                beginX = e.X;
                beginY = e.Y;
                beginWidth = pictureBox.Width;
                beginHeight = pictureBox.Height;
                moveObject = true;
                SetSelected(true);
            }
        }
        public event ChangeTabOrderEventHandler ChangeTabOrder;
        private void OnChangeTabOrder(ChangeTabOrderEventArgs e)
        {
            if (ChangeTabOrder != null)
                ChangeTabOrder(this, e);
        }
        /*!
         * Eventos do objeto picturebox
         * @param sender
         * @param e
         */
        protected virtual void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (tabOrder)
            {
                pictureBox.Cursor = Cursors.Default; 
            }
            else
            {
                UpdateRegions();
                SetCursor(e.X, e.Y);

                if (moveObject)
                {
                    switch (region)
                    {
                        //Move objeto
                        case CRegion.rgCenter:
                            {
                                int x = pictureBox.Left - (beginX - e.X);
                                int y = pictureBox.Top - (beginY - e.Y);
                                pictureBox.Left = x;
                                pictureBox.Top = y;
                                /*
                                pictureBox.Left = (x / 20) * 20;
                                pictureBox.Top = (y / 20) * 20;
                                 */
                            }; break;
                        //Redimensiona para baixo
                        case CRegion.rgLower:
                            {
                                pictureBox.Height = beginHeight - (beginY - e.Y);
                                pictureBox.Invalidate();
                            }; break;
                        case CRegion.rgUpper:
                            {
                                pictureBox.Top = pictureBox.Top - (beginY - e.Y);
                                pictureBox.Height = pictureBox.Height + (beginY - e.Y);
                                pictureBox.Refresh();
                            }; break;
                        case CRegion.rgRight:
                            {
                                pictureBox.Width = beginWidth - (beginX - e.X);
                                pictureBox.Refresh();
                            }; break;
                        case CRegion.rgLeft:
                            {
                                pictureBox.Left = pictureBox.Left - (beginX - e.X);
                                pictureBox.Width = pictureBox.Width + (beginX - e.X);
                                pictureBox.Refresh();
                            }; break;
                        case CRegion.rgLowerRight:
                            {
                                pictureBox.Width = beginWidth - (beginX - e.X);
                                pictureBox.Height = beginHeight - (beginY - e.Y);
                                pictureBox.Refresh();
                            }; break;
                        case CRegion.rgLowerLeft:
                            {
                                pictureBox.Left = pictureBox.Left - (beginX - e.X);
                                pictureBox.Width = pictureBox.Width + (beginX - e.X);
                                pictureBox.Height = beginHeight - (beginY - e.Y);
                                pictureBox.Refresh();
                            }; break;
                        case CRegion.rgUpperRight:
                            {
                                pictureBox.Top = pictureBox.Top - (beginY - e.Y);
                                pictureBox.Height = pictureBox.Height + (beginY - e.Y);
                                pictureBox.Width = beginWidth - (beginX - e.X);
                                pictureBox.Refresh();
                            }; break;
                        case CRegion.rgUpperLeft:
                            {
                                pictureBox.Top = pictureBox.Top - (beginY - e.Y);
                                pictureBox.Height = pictureBox.Height + (beginY - e.Y);
                                pictureBox.Left = pictureBox.Left - (beginX - e.X);
                                pictureBox.Width = pictureBox.Width + (beginX - e.X);
                                pictureBox.Refresh();
                            }; break;

                    }
                }
            }
        }
        /*!
         * Eventos do objeto picturebox
         * @param sender
         * @param e
         */
        protected virtual void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if(moveObject)
            {
                moveObject = false;
                UpdateRegions();
            }
            CDesignCustomScreen.enpaint = true;
        }

        protected abstract void pictureBox_Paint(object sender, PaintEventArgs e);
        

        protected virtual void pictureBox_DoubleClick(object sender, EventArgs e) { }
        /*!
         * Desenha retangulo de seleção 
         */
        protected void DrawSelectedRect(Graphics graphics)
        {
            const float RECT_SIZE = 7F;
            
            Pen pen = new Pen(Color.Black, 1);
            pen.DashStyle = DashStyle.Dash;
            graphics.DrawRectangle(pen, 3.5F, 3.5F, pictureBox.Width - 7, pictureBox.Height - 7);
            pen.DashStyle = DashStyle.Solid;
            Brush brush = new SolidBrush(Color.Gray);
            float x, y;

            x = 0;
            y = 0;
            graphics.FillRectangle(brush, x, y, RECT_SIZE, RECT_SIZE);
            graphics.DrawRectangle(pen, x, y, RECT_SIZE, RECT_SIZE);
            x = pictureBox.Width - 8;
            graphics.FillRectangle(brush, x, y, RECT_SIZE, RECT_SIZE);
            graphics.DrawRectangle(pen, x, y, RECT_SIZE, RECT_SIZE);
            y = pictureBox.Height - 8;
            graphics.FillRectangle(brush, x, y, RECT_SIZE, RECT_SIZE);
            graphics.DrawRectangle(pen, x, y, RECT_SIZE, RECT_SIZE);
            x = 0;
            graphics.FillRectangle(brush, x, y, RECT_SIZE, RECT_SIZE);
            graphics.DrawRectangle(pen, x, y, RECT_SIZE, RECT_SIZE);
            x = (pictureBox.Width - RECT_SIZE) / 2;
            y = 0;
            graphics.FillRectangle(brush, x, y, RECT_SIZE, RECT_SIZE);
            graphics.DrawRectangle(pen, x, y, RECT_SIZE, RECT_SIZE);   
            y = pictureBox.Height - 8;
            graphics.FillRectangle(brush, x, y, RECT_SIZE, RECT_SIZE);
            graphics.DrawRectangle(pen, x, y, RECT_SIZE, RECT_SIZE);
            x = 0;
            y = (pictureBox.Height - RECT_SIZE) / 2;
            graphics.FillRectangle(brush, x, y, RECT_SIZE, RECT_SIZE);
            graphics.DrawRectangle(pen, x, y, RECT_SIZE, RECT_SIZE);   
            x = pictureBox.Width - 8;
            graphics.FillRectangle(brush, x, y, RECT_SIZE, RECT_SIZE);
            graphics.DrawRectangle(pen, x, y, RECT_SIZE, RECT_SIZE);   

            //Pen pen = new Pen(Brushes.Red, 3);
            //graphics.DrawRectangle(pen, 1, 1, pictureBox.Width - 3, pictureBox.Height - 3);
        }

        /*!
         * Atualiza regioes para determinar o curzor do mouse         
         */
        protected void UpdateRegions()
        {            
            //
            rgnCenter.X = RGN_WIDTH;
            rgnCenter.Y = RGN_WIDTH;
            rgnCenter.Width = pictureBox.Width - (2 * RGN_WIDTH);
            rgnCenter.Height = pictureBox.Height - (2 * RGN_WIDTH);
            // 
            rgnUpper.X = RGN_WIDTH;
            rgnUpper.Y = 0;
            rgnUpper.Width = pictureBox.Width - (2 * RGN_WIDTH);
            rgnUpper.Height = RGN_WIDTH;
            //
            rgnLower.X = RGN_WIDTH;
            rgnLower.Y = pictureBox.Height - RGN_WIDTH;
            rgnLower.Width = pictureBox.Width - (2 * RGN_WIDTH);
            rgnLower.Height = pictureBox.Height;
            //
            rgnLeft.X = 0;
            rgnLeft.Y = RGN_WIDTH;
            rgnLeft.Width = RGN_WIDTH;
            rgnLeft.Height = pictureBox.Height - (2 * RGN_WIDTH);
            //
            rgnRight.X = pictureBox.Width - RGN_WIDTH;
            rgnRight.Y = RGN_WIDTH;
            rgnRight.Width = pictureBox.Width;
            rgnRight.Height = pictureBox.Height - (2 * RGN_WIDTH);
            //
            rgnUpperLeft.X = 0;
            rgnUpperLeft.Y = 0;
            rgnUpperLeft.Width = RGN_WIDTH;
            rgnUpperLeft.Height = RGN_WIDTH;
            //
            rgnUpperRight.X = pictureBox.Width - RGN_WIDTH;
            rgnUpperRight.Y = 0;
            rgnUpperRight.Width = RGN_WIDTH;
            rgnUpperRight.Height = RGN_WIDTH;
            //
            rgnLowerLeft.X = 0;
            rgnLowerLeft.Y = pictureBox.Height - RGN_WIDTH;
            rgnLowerLeft.Width = RGN_WIDTH;
            rgnLowerLeft.Height = RGN_WIDTH;
            //
            rgnLowerRight.X = pictureBox.Width - RGN_WIDTH;
            rgnLowerRight.Y = pictureBox.Height - RGN_WIDTH;
            rgnLowerRight.Width = RGN_WIDTH;
            rgnLowerRight.Height = RGN_WIDTH;
        }
        
        /*!
         * 
         * @param x
         * @param y
         * @return
         */
        private CRegion GetRegion(int x, int y)
        {
            if (rgnCenter.Contains(x, y))
                return CRegion.rgCenter;
            else if (rgnUpper.Contains(x, y))
                return CRegion.rgUpper;
            else if (rgnLower.Contains(x, y))
                return CRegion.rgLower;
            else if (rgnRight.Contains(x, y))
                return CRegion.rgRight;
            else if (rgnLeft.Contains(x, y))
                return CRegion.rgLeft;
            else if (rgnUpperRight.Contains(x, y))
                return CRegion.rgUpperRight;
            else if (rgnUpperLeft.Contains(x, y))
                return CRegion.rgUpperLeft;
            else if (rgnLowerRight.Contains(x, y))
                return CRegion.rgLowerRight;
            else if (rgnLowerLeft.Contains(x, y))
                return CRegion.rgLowerLeft;
            else
                return CRegion.rgNone;
        }
        /*!
         * 
         * @param x
         * @param y        
         */
        private void SetCursor(int x, int y)
        {
            switch (GetRegion(x, y))
            {
                case CRegion.rgCenter:
                    pictureBox.Cursor = Cursors.SizeAll; break;
                case CRegion.rgUpper:
                    pictureBox.Cursor = Cursors.SizeNS; break;
                case CRegion.rgLower:
                    pictureBox.Cursor = Cursors.SizeNS; break;
                case CRegion.rgLeft:
                    pictureBox.Cursor = Cursors.SizeWE; break;
                case CRegion.rgRight:
                    pictureBox.Cursor = Cursors.SizeWE; break;
                case CRegion.rgUpperLeft:
                    pictureBox.Cursor = Cursors.SizeNWSE; break;
                case CRegion.rgUpperRight:
                    pictureBox.Cursor = Cursors.SizeNESW; break;
                case CRegion.rgLowerLeft:
                    pictureBox.Cursor = Cursors.SizeNESW; break;
                case CRegion.rgLowerRight:
                    pictureBox.Cursor = Cursors.SizeNWSE; break;
                default:
                    pictureBox.Cursor = Cursors.Default; break;
            }
        }
        /*!
         * Calcula tamanho do retangulo a ser desenhado em função da regiao
         * onde o ponteiro do mouse baixou
         * @param region
         * @param posX
         * @param posY
         * @param x
         * @param y
         */
        protected void DimRect(CRegion region, int posX, int posY, int x, int y)
        {
            switch (region)
            {
                case CRegion.rgCenter:
                {//SelRect :=Rect(Left-(PosX - X),Top-(PosY - Y),Left+Width-(PosX - X),Top+Height-(PosY - Y));
                    this.selRect.X = Left - (posX - x);
                    this.selRect.Y = Top - (posY - y);
                    this.selRect.Width = Width;
                    this.selRect.Height = Height;
                }; break;
                case CRegion.rgUpper:
                {//SelRect :=Rect(Left,Top-(PosY - Y),Left+Width,Top+Height);
                    this.selRect.X = Left ;
                    this.selRect.Y = Top - (posY - y);
                    this.selRect.Width = Width ;
                    this.selRect.Height = Height + (posY - y);
                };break;
                case CRegion.rgLower:
                {//SelRect :=Rect(Left,Top,Left+Width,Top+Height-(PosY - Y));
                    this.selRect.X = Left ;
                    this.selRect.Y = Top ;
                    this.selRect.Width = Width;
                    this.selRect.Height = Height - (posY - y);
                };break;
                case CRegion.rgLeft:
                {//SelRect :=Rect(Left-(PosX - X),Top,Left+Width,Top+Height);
                    this.selRect.X = Left - (posX - x);
                    this.selRect.Y = Top;
                    this.selRect.Width = Width + (posX - x);
                    this.selRect.Height = Height ;
                };break;
                case CRegion.rgRight:
                {//SelRect :=Rect(Left,Top,Left+Width-(PosX - X),Top+Height);
                    this.selRect.X = Left ;
                    this.selRect.Y = Top ;
                    this.selRect.Width = Width - (posX - x);
                    this.selRect.Height = Height;
                }; break;
                case CRegion.rgUpperLeft:
                {//SelRect :=Rect(Left-(PosX - X),Top-(PosY - Y),Left+Width,Top+Height);
                    this.selRect.X = Left - (posX - x);
                    this.selRect.Y = Top - (posY - y);
                    this.selRect.Width = Width + (posX - x);
                    this.selRect.Height = Height + (posY - y);
                };break;
                case CRegion.rgUpperRight:
                {//SelRect :=Rect(Left,Top-(PosY - Y),Left+Width-(PosX - X),Top+Height);
                    this.selRect.X = Left;
                    this.selRect.Y = Top - (posY - y);
                    this.selRect.Width = Width - (posX - x);
                    this.selRect.Height = Height + (posY - y);
                };break;
                case CRegion.rgLowerLeft:
                {//SelRect :=Rect(Left-(PosX - X),Top,Left+Width,Top+Height-(PosY - Y));
                    this.selRect.X = Left - (posX - x);
                    this.selRect.Y = Top ;
                    this.selRect.Width = Width + (posX - x);
                    this.selRect.Height = Height - (posY - y);
                };break;
                case CRegion.rgLowerRight:
                {//SelRect :=Rect(Left,Top,Left+Width-(PosX - X),Top+Height-(PosY - Y));
                    this.selRect.X = Left;
                    this.selRect.Y = Top;
                    this.selRect.Width = Width - (posX - x);
                    this.selRect.Height = Height - (posY - y);
                }; break;
                default:
                {//SelRect :=Rect(0,0,0,0);
                    this.selRect.X = 0;
                    this.selRect.Y = 0;
                    this.selRect.Width = 0;
                    this.selRect.Height = 0;
                }; break;
            }
        }
        public bool tabOrder = false;
        public bool isOrdenated = false;
        //public abstract void LinkObjects();
        
    }//close class
}//close namespace
