using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src.Forms;
using MicroSCADAStudioLibrary.Src.TypeConverter;
using MicroSCADAStudioLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    public abstract class CDesignCustomScreen : CDesignView, ICustomScreen, IPageAdapter
    {        
        private Point currentPoint;
        protected Point beginPoint;
        protected Point endPoint;                
        private bool enabledSelectedRect;        
        protected CCustomScreen customScreen;
        protected BackgroundScreenForm form;        
        protected static int createID;
        public static Boolean enpaint;        
        //
        protected int indexBitmapItem;
        protected const int DEFAULT_OBJECT_WIDTH = 100;
        protected const int DEFAULT_OBJECT_HEIGHT = 25;
        /*!
         * Construtor
         */
        public CDesignCustomScreen(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {            
            this.customScreen = new CCustomScreen();
            
            this.width = 640;
            this.height = 480;
                          
            this.beginPoint = new Point(0,0);
            this.currentPoint = new Point(0, 0);
            this.endPoint = new Point(0,0);            
            
            this.enabledSelectedRect = false;
            this.indexBitmapItem = this.ReferenceList.AddReference();
            
            createID = 0;            
            enpaint = true;           
        }
        /*!
         * Instancia form
         */
        public override void Initialize(Control Parent)
        {
            this.form = new BackgroundScreenForm(Parent);            
            this.form.Name = "Screen";
            this.Name = this.form.Name;
            this.form.Width = width;
            this.form.Height = height;            
            this.form.Click += new EventHandler(form_Click);
            this.form.MouseMove += new MouseEventHandler(form_MouseMove);
            this.form.MouseMove += new MouseEventHandler(project.MouseMove);  
            this.form.MouseDown += new MouseEventHandler(form_MouseDown);
            this.form.MouseUp += new MouseEventHandler(form_MouseUp);
            this.form.KeyDown += new KeyEventHandler(form_KeyDown);
            this.form.KeyDown += new KeyEventHandler(project.KeyDown);
            this.form.KeyUp += new KeyEventHandler(form_KeyUp);                      
        }
        //Destrutor
        //~CDesignCustomScreen()
        //{
        //    Dispose();
        //}
        public override void Dispose()
        {            
            this.form.Dispose();
            this.SetReference(indexBitmapItem, null); 
            base.Dispose();
        }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignShape> ShapeList { get { return GetShapeList(); } }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignText> TextList { get { return GetTextList(); } }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif        
        public List<CDesignPicture> PictureList { get { return GetPictureList(); } }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignAlphaNumeric> AlphanumericList { get { return GetAlphaNumericList(); } }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignDinamicText> DinamicTextList { get { return GetDinamicTextList(); } }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignAnimation> AnimationList { get { return GetAnimationList(); } }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignBargraph> BargraphList { get { return GetBargraphList(); } }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignTrendChart> TrendChartList { get { return GetTrendChartList(); } }
        //! Lista de buttons
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignButton> ButtonList { get { return GetButtonList(); } }
        //! Lista de checkbox
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignCheckBox> CheckBoxList { get { return GetCheckBoxList(); } }
        //! Lista de checkbox
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignRadioGroup> RadioGroupList { get { return GetRadioGroupList(); } }
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignMeter> MeterList { get { return GetMeterList(); } }
        //
        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(form.Width, form.Height);
            form.DrawToBitmap(bitmap, new Rectangle(0, 0, form.Width, form.Height));
            return bitmap;
        }
        //Propriedades(implementar para popupscreen}
        protected override int getLeft() { return this.form.Left; }
        protected override void setLeft(int Value) { this.form.Left = Value; }
        protected override int getTop() { return this.form.Top; }
        protected override void setTop(int Value) { this.form.Top = Value; }
        protected override int getWidth() 
        { 
            return this.width; 
        }
        protected override void setWidth(int Value)
        {
            this.width = Value;
            this.form.Width = Value; 
        }
        protected override int getHeight() 
        { 
            return this.height; 
        }
        protected override void setHeight(int Value) 
        {
            this.height = Value;
            this.form.Height = Value; 
        }
        /*!
         * Seta nome do objeto e texto no node do treeview
         * @param value Nome da tela
         */
        new private void SetName(string Value)
        {
            this.customObject.name = Value;
            this.form.Text = Value;
            OnSetObjectName(new SetNameEventArgs(Value));
        }
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        public Form getForm() { return form; }
        //! Propriedade Color 
        [ActionProperty]
        public Color BackColor
        {
            get { return this.form.BackColor; }
            set { this.form.BackColor = value; }
        }
        
        
        public int getBeginX() { return beginPoint.X; }
        public void setBeginX(int value) { beginPoint.X = value; }
        public int getBeginY() { return beginPoint.Y; }
        public void setBeginY(int value) { beginPoint.Y = value; }
        public static void setCreateID(int Value)
        {
            createID = Value;
        }
        /*!
         * 
         */
        //protected void OnSelectedObject(SelectedObjectEventArgs e)
        //{
        //    if (SelectedObject != null)
        //        SelectedObject(this, e);
        //}
        /*!
         * Evento Paint do form
         */
        //private void form_Paint(object sender, PaintEventArgs e)
        //{            
        //}
        protected virtual void form_Click(object sender, EventArgs e)
        {

        }        
        /*!
         * 
         */
        private void form_MouseDown(object sender, MouseEventArgs e)
        {
            //
            beginPoint = e.Location;
            endPoint = beginPoint;
            //
            enabledSelectedRect = true;            
        }
        /*!
         * 
         */
        private void form_MouseMove(object sender, MouseEventArgs e)
        {
            //
            
            currentPoint = e.Location;                        
            
            //
            if (enabledSelectedRect)
            {
                // Draw again in that spot to remove the lines.
                DrawSelectionRect(beginPoint, endPoint);
                // Draw new lines.
                DrawSelectionRect(beginPoint, currentPoint);
                // Update last point.
                endPoint = currentPoint;                
            }
        }
        /*!
         * 
         */
        protected virtual void form_MouseUp(object sender, MouseEventArgs e)
        {
            if (enabledSelectedRect)
            {                
                //Apaga retangulo anterior
                DrawSelectionRect(beginPoint, endPoint);                
                //
                enabledSelectedRect = false;
            }            
        }
       
        /*!
         * Convert and normalize the points and draw the reversible frame.
         */
        private void DrawSelectionRect(Point p1, Point p2)
        {
            Rectangle rc = new Rectangle();

            // Convert the points to screen coordinates.
            p1 = form.PointToScreen(p1);
            p2 = form.PointToScreen(p2);
            // Normalize the rectangle.
            rc = CreateNormalizedRect(p1, p2);            
            // Draw the reversible frame.
            ControlPaint.DrawReversibleFrame(rc, Color.Black, FrameStyle.Dashed);            
        }
        /*!
         * 
         */
        protected Rectangle CreateNormalizedRect(Point p1,Point p2)
        {
            Rectangle rc = new Rectangle();

            if (p1.X < p2.X)
            {
                rc.X = p1.X;
                rc.Width = p2.X - p1.X;
            }
            else
            {
                rc.X = p2.X;
                rc.Width = p1.X - p2.X;
            }
            if (p1.Y < p2.Y)
            {
                rc.Y = p1.Y;
                rc.Height = p2.Y - p1.Y;
            }
            else
            {
                rc.Y = p2.Y;
                rc.Height = p1.Y - p2.Y;
            }
            return rc;
        }
        /*!
         * Evento OnKeyDown.         
         * @param sender objeto que chamou o evento
         * @param e Argumentos do evento Key
         */
        protected abstract void form_KeyDown(object sender, KeyEventArgs e);
        
        protected virtual void form_KeyUp(object sender, KeyEventArgs e)
        {
        }
        /*!
         * Cria novo objeto shape
         * @return Referencia para objeto criado
         */
        public ICustomShape NewShape()
        {
            CDesignShape shape;

            shape = new CDesignShape(this, project, form);
            ObjectList.Add(shape);            
            OnAddItem(new AddItemEventArgs(shape, shape.ImageIndex));
            shape.BringToFront();
            shape.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            
            return shape;
        }
        /*!
         * 
         */
        protected List<CDesignShape> GetShapeList()
        {
            IEnumerable<CDesignShape> subset = ObjectList.OfType<CDesignShape>();
            return subset.ToList();
        }
        /*!
         * 
         * @return
         */
        public ICustomText NewText()
        {
            CDesignText text;

            text = new CDesignText(this, project, form);
            ObjectList.Add(text);
            OnAddItem(new AddItemEventArgs(text, text.ImageIndex));
            text.BringToFront();
            text.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            return text;
        }

        protected List<CDesignText> GetTextList()
        {
            IEnumerable<CDesignText> subset = ObjectList.OfType<CDesignText>();
            return subset.ToList();
        }
        /*!
         * 
         * @return
         */
        public ICustomPicture NewPicture()
        {
            CDesignPicture picture;

            picture = new CDesignPicture(this, project, form);
            ObjectList.Add(picture);
            OnAddItem(new AddItemEventArgs(picture, picture.ImageIndex));
            picture.BringToFront();
            picture.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            return picture;
        }
        protected List<CDesignPicture> GetPictureList()
        {
            IEnumerable<CDesignPicture> subset = ObjectList.OfType<CDesignPicture>();
            return subset.ToList();
        }
        /*!
         * 
         */
        public ICustomAlphaNumeric NewAlphaNumeric()
        {
            CDesignAlphaNumeric alphaNumeric;

            alphaNumeric = new CDesignAlphaNumeric(this, project, form);
            ObjectList.Add(alphaNumeric);
            OnAddItem(new AddItemEventArgs(alphaNumeric, alphaNumeric.ImageIndex));
            alphaNumeric.BringToFront();
            alphaNumeric.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            alphaNumeric.ChangeTabOrder += new ChangeTabOrderEventHandler(object_ChangeTabOrder);
            return alphaNumeric;
        }
        protected List<CDesignAlphaNumeric> GetAlphaNumericList()
        {
            IEnumerable<CDesignAlphaNumeric> subset = ObjectList.OfType<CDesignAlphaNumeric>();
            return subset.ToList();
        }
        /*!
         * 
         */
        public ICustomDinamicText NewDinamicText()
        {
            CDesignDinamicText dinamicText;

            dinamicText = new CDesignDinamicText(this, project, form);
            ObjectList.Add(dinamicText);
            OnAddItem(new AddItemEventArgs(dinamicText, dinamicText.ImageIndex));
            dinamicText.BringToFront();
            dinamicText.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            return dinamicText;
        }

        protected List<CDesignDinamicText> GetDinamicTextList()
        {
            IEnumerable<CDesignDinamicText> subset = ObjectList.OfType<CDesignDinamicText>();
            return subset.ToList();
        }

        public ICustomAnimation NewAnimation()
        {
            CDesignAnimation animation;
            
            animation = new CDesignAnimation(this, project, form);
            ObjectList.Add(animation);
            OnAddItem(new AddItemEventArgs(animation, animation.ImageIndex));
            animation.BringToFront();
            animation.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            return animation;
        }
        protected List<CDesignAnimation> GetAnimationList()
        {
            IEnumerable<CDesignAnimation> subset = ObjectList.OfType<CDesignAnimation>();
            return subset.ToList();
        }
        /*!
         * 
         */
        public ICustomTrendChart NewTrendChart()
        {
            CDesignTrendChart trendChart;

            trendChart = new CDesignTrendChart(this, project, form);
            ObjectList.Add(trendChart);
            OnAddItem(new AddItemEventArgs(trendChart, trendChart.ImageIndex));
            trendChart.BringToFront();
            trendChart.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            return trendChart;
        }
        protected List<CDesignTrendChart> GetTrendChartList()
        {
            IEnumerable<CDesignTrendChart> subset = ObjectList.OfType<CDesignTrendChart>();
            return subset.ToList();
        }
        /*!
         * 
         */
        public ICustomBargraph NewBargraph()
        {
            CDesignBargraph bargraph;

            bargraph = new CDesignBargraph(this, project, form);
            ObjectList.Add(bargraph);
            OnAddItem(new AddItemEventArgs(bargraph, bargraph.ImageIndex));
            bargraph.BringToFront();
            bargraph.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            return bargraph;
        }

        protected List<CDesignBargraph> GetBargraphList()
        {
            IEnumerable<CDesignBargraph> subset = ObjectList.OfType<CDesignBargraph>();
            return subset.ToList();
        }
        /*!
         * 
         */
        public ICustomButton NewButton()
        {
            CDesignButton button;

            button = new CDesignButton(this, project, form);
            ObjectList.Add(button);
            OnAddItem(new AddItemEventArgs(button, button.ImageIndex));
            button.BringToFront();
            button.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            button.ChangeTabOrder += new ChangeTabOrderEventHandler(object_ChangeTabOrder);
            return button;
        }

        protected List<CDesignButton> GetButtonList()
        {
            IEnumerable<CDesignButton> subset = ObjectList.OfType<CDesignButton>();
            return subset.ToList();
        }
        /*!
         * Cria e adiciona novo objeto na lista
         * @return Referencia para novo objeto
         */
        public ICustomCheckBox NewCheckBox()
        {
            CDesignCheckBox checkBox = new CDesignCheckBox(this, project, form);
            ObjectList.Add(checkBox);
            OnAddItem(new AddItemEventArgs(checkBox, checkBox.ImageIndex));
            checkBox.BringToFront();
            checkBox.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            checkBox.ChangeTabOrder += new ChangeTabOrderEventHandler(object_ChangeTabOrder);
            return checkBox;
        }
        /*!
         * Lista de checkbox
         */
        protected List<CDesignCheckBox> GetCheckBoxList()
        {
            IEnumerable<CDesignCheckBox> subset = ObjectList.OfType<CDesignCheckBox>();
            return subset.ToList();
        }
        /*!
         * Cria e adiciona novo objeto na lista
         * @return Referencia para novo objeto
         */
        public ICustomRadioGroup NewRadioGroup()
        {
            CDesignRadioGroup radioGroup = new CDesignRadioGroup(this, project, form);
            ObjectList.Add(radioGroup);
            OnAddItem(new AddItemEventArgs(radioGroup, radioGroup.ImageIndex));
            radioGroup.BringToFront();
            radioGroup.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            radioGroup.ChangeTabOrder += new ChangeTabOrderEventHandler(object_ChangeTabOrder);
            return radioGroup;
        }        
        /*!
         * Lista de RadioGroup
         */
        protected List<CDesignRadioGroup> GetRadioGroupList()
        {
            IEnumerable<CDesignRadioGroup> subset = ObjectList.OfType<CDesignRadioGroup>();
            return subset.ToList();
        }

        public ICustomMeter NewMeter()
        {
            CDesignMeter meter = new CDesignMeter(this, project, form);
            ObjectList.Add(meter);
            OnAddItem(new AddItemEventArgs(meter, meter.ImageIndex));
            meter.BringToFront();
            meter.SelectedObject += new SelectedObjectEventHandler(object_SelectedObject);
            return meter;
        }     
        protected List<CDesignMeter> GetMeterList()
        {
            IEnumerable<CDesignMeter> subset = ObjectList.OfType<CDesignMeter>();
            return subset.ToList();
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            Object obj;
            Guid guid = GetReferenceGuid(indexBitmapItem);
            if(CHashObjects.ObjectDictionary.ContainsKey(guid))            
            {
                obj = CHashObjects.ObjectDictionary[guid];
            }
            else
            {
                obj = null;
            }
            SetReference(indexBitmapItem, obj);
            if(BitmapItem != null)
                form.BackgroundImage = ((CDesignBitmapItem)BitmapItem).GetBitmap();
            //
            for (int I = 0; I < ObjectList.Count; I++)
            {
                CDesignScreenObject screenObject = (CDesignScreenObject)ObjectList[I];
                screenObject.LinkObjects();
            }
        }        
        

        
        public event ShowItemEventHandler ShowItem;
        public void Show()
        {            
            OnShowItem(new ShowItemEventArgs(form));
            form.Width = width;
            form.Height = height;
            form.Show();            
        }
        private void OnShowItem(ShowItemEventArgs e)
        {
            if (ShowItem != null)
                ShowItem(this, e);
        }
        [EditorAttribute(typeof(CBitmapDialogTypeEditor), 
                         typeof(System.Drawing.Design.UITypeEditor))]
        public CDesignBitmapItem ImageOfObject
        {
            get { return (CDesignBitmapItem)this.BitmapItem; }
            set { this.BitmapItem = value; }
        }
        [Browsable(false)]
        public ICustomBitmapItem BitmapItem
        {
            get { return (CDesignBitmapItem)this.GetBitmapItem(); }
            set { this.SetBitmapItem((CDesignBitmapItem)value); }
        }
        private void SetBitmapItem(CDesignBitmapItem Value)
        {
            this.SetReference(indexBitmapItem, Value);
            if (ImageOfObject != null)
                form.BackgroundImage = ImageOfObject.GetBitmap();
        }
        private CDesignBitmapItem GetBitmapItem()
        {
            return (CDesignBitmapItem)this.GetReference(indexBitmapItem);
        }
        public void SetGuidBitmapItem(Guid Value)
        {
            this.SetReferenceGuid(indexBitmapItem, Value);
        }
        public bool shift;
        /*!
         * Seta selected dos objetos para false menos o que foi passado em sender
         * @param sender Objeto que continuara selecionado
         */
        public void UnSelectedObjects(object sender)
        {
            foreach (CDesignScreenObject item in ObjectList)
            {
                if (sender != item)
                {
                    if (item.Selected)
                        item.Selected = false;
                }
            }
        }

        private void object_SelectedObject(object sender, SelectedObjectEventArgs e)
        {
            if (shift == false)
            {
                UnSelectedObjects(sender);
            }
        }

        protected int tabOrderCounter = 0;        

        private void object_ChangeTabOrder(object sender, ChangeTabOrderEventArgs e)
        {
            Exchange(tabOrderCounter, e.Index);
            tabOrderCounter++;
            form.Invalidate(true);
        }
    }
}
