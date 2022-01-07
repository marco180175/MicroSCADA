using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADAStudioLibrary.Src.TypeConverter;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Visuals
{    

    public class CDesignTrendChart : CDesignCustomField, ICustomTrendChart, IDesignCollection
    {        
        private CCustomTrendChart customTrendChart;        
        /*!
         * Construtor
         * @param AOwner
         * @param Node
         * @param Project
         * @param Parent
         */
        public CDesignTrendChart(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            InitializeObject();
            this.customTrendChart = new CCustomTrendChart();
            this.imageIndex = 17;                                  
        }
        /*!
         * 
         */
        ~CDesignTrendChart()
        {
            Dispose();
        }
        /*!
         * 
         */
        public override void Dispose()
        {            
            this.customTrendChart.Dispose();
            base.Dispose();
        }
        
        #region Propriedades
        //! Lista de items
        [Editor(typeof(CollectionEditorPreset), typeof(UITypeEditor))]
        public ArrayList Items
        {
            get { return ObjectList; }
        }   
        //!
        [Browsable(false)]
        public override Color BackColor
        {
            get { return this.pictureBox.BackColor; }
            set { this.pictureBox.BackColor = value; }
        }
        //!
        [ReadOnly(true)]
        public override CFieldType FieldType
        {
            get { return CFieldType.ftRead; }
        }
        //!
        public CTrendChartOrientation Orientation
        {
            get { return this.customTrendChart.orientation; }
            set { this.customTrendChart.orientation = value; }
        }
        //!
        [Category("Appearance")]
        public Font ScaleFont
        {
            get { return this.customTrendChart.scaleFont; }
            set { this.customTrendChart.scaleFont = value; }
        }
        //!
        [Category("Appearance")]
        public Color ScaleFontColor
        {
            get { return this.customTrendChart.scaleFontColor; }
            set { this.customTrendChart.scaleFontColor = value; }
        }
        //!
        [Category("Appearance")]
        public string Title
        {
            get { return this.customTrendChart.title; }
            set { this.customTrendChart.title = value; }
        }
        //!
        [Category("Appearance")]
        public Font TitleFont
        {
            get { return this.customTrendChart.titleFont; }
            set { this.customTrendChart.titleFont = value; }
        }
        //!
        [Category("Appearance")]
        public Color TitleFontColor
        {
            get { return this.customTrendChart.titleFontColor; }
            set { this.customTrendChart.titleFontColor = value; }
        }
        //!
        [Category("Ranges")]
        public float MaxValueY 
        {
            get { return this.customTrendChart.maxY; }
            set { this.customTrendChart.maxY = value; }
        }
        //!
        [Category("Ranges")]
        public float MinValueY 
        {
            get { return this.customTrendChart.minY; }
            set { this.customTrendChart.minY = value; }
        }
        //!
        [Category("Appearance")]
        public Color ChartAreaColor 
        {
            get { return this.customTrendChart.chartAreaColor; }
            set { this.customTrendChart.chartAreaColor = value; }
        }
        //!
        [Category("Appearance")]
        public Color PlotAreaColor 
        {
            get { return this.customTrendChart.plotAreaColor; }
            set { this.customTrendChart.plotAreaColor = value; }
        }
        public int BufferSize 
        {
            get { return this.customTrendChart.bufferSize; }
            set { this.customTrendChart.bufferSize = value; }
        }
        public int UpdateTime 
        {
            get { return this.customTrendChart.updateTime; }
            set { this.customTrendChart.updateTime = value; }
        }

        //! Não utilizado
        [Browsable(false)]
        public override CDesignCustomTag TagValueEx
        {
            get { return this.GetTagValue(); }
        }
        #endregion
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {           
            customTrendChart.DrawChart(e.Graphics, ObjectList, pictureBox.Width, pictureBox.Height);
            customTrendChart.DrawPenLabel(e.Graphics, ObjectList);
            customTrendChart.DrawGrid(e.Graphics, customTrendChart.plotRect.Location);                
            //seleciona
            if (selected)
                DrawSelectedRect(e.Graphics);
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            foreach (CDesignTrendChartPen penObj in ObjectList)
            {                
                penObj.LinkObjects();
            }
            MakeHint();
        }
        /*!
         * 
         */
        public ICustomPenTrendChart AddPen()
        {
            CDesignTrendChartPen pen = new CDesignTrendChartPen(this, project);
            pen.UpdateHint += new EventHandler(pen_UpdateHint);
            pen.DelItem += new DelItemEventHandler(pen_DelItem);
            OnAddItem(new AddItemEventArgs(pen, pen.ImageIndex));
            ObjectList.Add(pen);            
            return pen;
        }
        /*!
         * 
         */
        public CDesignTrendChartPen AddPenEx()
        {
            CDesignTrendChartPen pen = (CDesignTrendChartPen)AddPen();
            pen.SetGUID(Guid.NewGuid());            
            pen.Name = "Pen" + ObjectList.Count.ToString();
            pen.Label = pen.Name;
            return pen;
        }
        /*!
         * Event handler
         */
        private void pen_UpdateHint(object sender, EventArgs e)
        {
            MakeHint();
        }
        /*!
         * Event handler
         */
        private void pen_DelItem(object sender, EventArgs e)
        {
            ObjectList.Remove(sender);
            MakeHint();
            pictureBox.Invalidate();
        }
        /*!
         * 
         */
        public IDesignCollectionItem NewItem()
        {
            return (IDesignCollectionItem)AddPenEx();
        }
        /*!
         * 
         */
        public void AddTagValue(CDesignCustomTag Value)
        {
            CDesignTrendChartPen pen = AddPenEx();
            pen.TagValue = Value;
            MakeHint();
        }
        /*!
         * Monta Hint com multiplos tags 
         */
        protected override void MakeHint()
        {
            string hint = Name;
            foreach (CDesignTrendChartPen pen in ObjectList)
            {
                if (pen.TagValue != null)
                    hint += "\r\n" + pen.TagValue.ToString();
                else
                    hint += "\r\n null";
            }
            toolTip.SetToolTip(pictureBox, hint);
        }        
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignTrendChart> subSet = Objects.OfType<CDesignTrendChart>();
            return subSet.ToArray();
        }
    }

    public class CDesignTrendChartPen : CDesignSystem, ICustomPenTrendChart, IDesignCollectionItem
    {
        protected CCustomPenTrendChart customPenTrendChart;
        protected int indexTagValue;
        public CDesignTrendChartPen(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customPenTrendChart = new CCustomPenTrendChart();
            this.imageIndex = 18;
            this.indexTagValue = this.ReferenceList.AddReference();
        }

        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignTrendChartPen> subSet = Objects.OfType<CDesignTrendChartPen>();
            return subSet.ToArray();
        }
        #region Propriedades
        [Browsable(false)]
        public ICustomTag TagValue
        {
            get { return (ICustomTag)this.TagValueEx; }
            set { TagValueEx = (CDesignCustomTag)value; }
        }
        //!
        [Editor(typeof(CTagTypeDialogPreset), typeof(UITypeEditor))]
        public CDesignCustomTag TagValueEx
        {
            get { return (CDesignCustomTag)this.GetTagValue(); }
            set { this.SetTagValue(value); }
        }

        public String Label
        {
            get { return this.customPenTrendChart.label; }
            set { this.customPenTrendChart.label = value; }
        }
        public Color PenColor
        {
            get { return this.customPenTrendChart.color; }
            set { this.customPenTrendChart.color = value; }
        }
        public int Width
        {
            get { return this.customPenTrendChart.width; }
            set { this.customPenTrendChart.width = value; }
        }
        //!
        [ReadOnly(true)]
        public float MaxValue { get; set; }
        //!
        [ReadOnly(true)]
        public float MinValue { get; set; }
        //!
        public event EventHandler UpdateHint;
        #endregion
        
        private void OnUpdateHint(EventArgs e)
        {
            if (UpdateHint != null)
                UpdateHint(this, e);
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            Object obj;
            Guid guid = GetReferenceGuid(indexTagValue);
            //
            if (CHashObjects.ObjectDictionary.ContainsKey(guid))
                obj = CHashObjects.ObjectDictionary[guid];
            else
                obj = null;
            //
            SetReference(indexTagValue, obj);
        }
        /*!
         * 
         */
        protected void SetTagValue(CDesignCustomTag Value)
        {
            this.SetReference(indexTagValue, Value);
        }
        /*!
         * 
         */
        public CDesignCustomTag GetTagValue()
        {
            return (CDesignCustomTag)this.GetReference(indexTagValue);
        }
        /*!
         * 
         */
        public void SetGuidTagValue(Guid Value)
        {
            this.SetReferenceGuid(indexTagValue, Value);
        }
    }
}
