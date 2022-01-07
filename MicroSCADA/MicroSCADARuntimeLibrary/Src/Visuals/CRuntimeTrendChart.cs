using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimeTrendChart : CRuntimeCustomField, ICustomTrendChart
    {
        private CCustomTrendChart customTrendChart;
        private System.Timers.Timer timerUpdate;
        private int margin;
        private static int count = 0;
        private PictureBox m_pbPlot;//!< PictureBox de plotagem
        /*!
         * Construtor
         * @param AOwner
         * @param Node
         * @param Project
         * @param Parent
         */
        public CRuntimeTrendChart(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customTrendChart = new CCustomTrendChart();
            this.customTrendChart.bufferSize = 300;
            this.timerUpdate = new System.Timers.Timer(1000);
            this.timerUpdate.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Tick);
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);
            
            this.m_pbPlot = new PictureBox();
            this.m_pbPlot.Parent = this.pictureBox;

            this.margin = this.customTrendChart.margin;
            this.timerUpdate.Enabled = true;
            count++;
        }
        #region Propriedades
        public Font ScaleFont
        {
            get { return this.customTrendChart.scaleFont; }
            set { this.customTrendChart.scaleFont = value; }
        }
        public Color ScaleFontColor
        {
            get { return this.customTrendChart.scaleFontColor; }
            set { this.customTrendChart.scaleFontColor = value; }
        }
        public string Title
        {
            get { return this.customTrendChart.title; }
            set { this.customTrendChart.title = value; }
        }
        public Font TitleFont
        {
            get { return this.customTrendChart.titleFont; }
            set { this.customTrendChart.titleFont = value; }
        }
        public Color TitleFontColor
        {
            get { return this.customTrendChart.titleFontColor; }
            set { this.customTrendChart.titleFontColor = value; }
        }
        public CTrendChartOrientation Orientation 
        {
            get { return this.customTrendChart.orientation; }
            set { this.customTrendChart.orientation = value; }
        }
        public float MaxValueY
        {
            get { return this.customTrendChart.maxY; }
            set { this.customTrendChart.maxY = value; }
        }
        public float MinValueY
        {
            get { return this.customTrendChart.minY; }
            set { this.customTrendChart.minY = value; }
        }
        public Color ChartAreaColor
        {
            get { return this.customTrendChart.chartAreaColor; }
            set { this.customTrendChart.chartAreaColor = value; }
        }
        public Color PlotAreaColor
        {
            get { return this.m_pbPlot.BackColor; }
            set { this.m_pbPlot.BackColor = value; }
        }
        public int BufferSize
        {
            get { return this.customTrendChart.bufferSize; }
            set { this.customTrendChart.bufferSize = value; }
        }
        public int UpdateTime
        {
            get { return this.customTrendChart.updateTime; }
            set { 
                    this.customTrendChart.updateTime = value;
                    this.timerUpdate.Interval = this.customTrendChart.updateTime;
                }
        }
        #endregion
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            //Superficie de chart
            customTrendChart.DrawChart(e.Graphics, ObjectList, pictureBox.Width, pictureBox.Height);            
            m_pbPlot.Left = customTrendChart.plotRect.Left+1;
            m_pbPlot.Top = customTrendChart.plotRect.Top+1;
            m_pbPlot.Width = customTrendChart.plotRect.Width-1;
            m_pbPlot.Height = customTrendChart.plotRect.Height-1;            
            customTrendChart.DrawPenLabel(e.Graphics, ObjectList);
            //Troca para superficie de plot
            Graphics graphicsPlot = m_pbPlot.CreateGraphics();
            ClearPlot(graphicsPlot, m_pbPlot.Width, m_pbPlot.Height);
            customTrendChart.DrawGrid(graphicsPlot, new Point(0, 0));
            DrawPlot(graphicsPlot, m_pbPlot.Width, m_pbPlot.Height);
        }
        /*!
         * 
         */
        private void ClearPlot(Graphics graphics, int width, int height)
        {
            Rectangle rect = new Rectangle(0, 0, width-1, height-1);
            Pen pen = new Pen(Color.Black, 1);
            Brush sb = new SolidBrush(PlotAreaColor);
            graphics.FillRectangle(sb, rect);
            graphics.DrawRectangle(pen, rect);
        }
        /*!
         * 
         */
        private void DrawPlot(Graphics graphics, int width, int height)
        {           
            for (int i = 0; i < ObjectList.Count; i++)
            {
                CRuntimePenTrendChart penChart;
                int j;
                float v;
                penChart = (CRuntimePenTrendChart)ObjectList[i];
                PointF p0 = new Point();
                PointF p1 = new Point();
                Pen pen = new Pen(penChart.PenColor, penChart.Width);
                //
                j = 0;
                //calcula ponto em x                
                p0.X = customTrendChart.ConvertScale(j, 0, customTrendChart.bufferSize - 1, 0, width);
                //calcula ponto em y
                v = penChart.GetValueFromQueue(j);
                p0.Y = height - customTrendChart.ConvertScale(v, MinValueY, MaxValueY, 0, height);
                //
                for (j = 1; j < customTrendChart.bufferSize; j++)
                {
                    //calcula ponto em x                                        
                    p1.X = customTrendChart.ConvertScale(j, 0, customTrendChart.bufferSize - 1, 0, width);
                    //calcula ponto em y                    
                    v = penChart.GetValueFromQueue(j);
                    p1.Y = height - customTrendChart.ConvertScale(v, MinValueY, MaxValueY, 0, height);
                    //
                    graphics.DrawLine(pen, p0, p1);
                    //
                    p0.X = p1.X;
                    p0.Y = p1.Y;
                }
            }                     
        }        
        /*!
         * 
         */
        public override void LinkObjects()
        {            
            foreach (CRuntimePenTrendChart pen in ObjectList)            
                pen.LinkObjects();            
        }
        /*!
         * 
         */
        public ICustomPenTrendChart AddPen()
        {
            CRuntimePenTrendChart pen;

            pen = new CRuntimePenTrendChart(this, customTrendChart.bufferSize, project);
            ObjectList.Add(pen);          
            return pen;
        }
        /*!
         * Atualiza lista de valores e desenha grafico
         */        
        private void timer_Tick(object sender, System.Timers.ElapsedEventArgs e) 
        {
            //mantem o sincronismo de todas as penas
            foreach(CRuntimePenTrendChart pen in ObjectList)         
                pen.SetValueToQueue();
            //
            pictureBox.Invalidate();             
        }
    }
    /*!
     * 
     */
    class CRuntimePenTrendChart : CRuntimeSystem, ICustomPenTrendChart, IRuntimeField
    {
        protected CCustomPenTrendChart customPenTrendChart;
        protected int indexTagValue;
        protected CQueueScroll penValues;
        private float m_value;
        public CRuntimePenTrendChart(Object AOwner, int QueueSize, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customPenTrendChart = new CCustomPenTrendChart();
            this.indexTagValue = this.ReferenceList.AddReference();
            this.penValues = new CQueueScroll(QueueSize);
            this.m_value = 0;
        }
        #region Propriedades
        //!
        public ICustomTag TagValue
        {
            get { return (ICustomTag)GetReference(indexTagValue); }
            set { SetReference(indexTagValue, value); }
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
        #endregion
        /*!
         * Evento setvalue do tag
         */
        private void field_SetValue(object sender, TagSetValueEventArgs e)
        {
            m_value = float.Parse(e.Value);
        }
        /*!
         * 
         */
        public void LinkObjects()
        {
            Object obj;
            Guid keyGUID = GetReferenceGuid(indexTagValue);
            if (CHashObjects.ObjectDictionary.ContainsKey(keyGUID))
                obj = CHashObjects.ObjectDictionary[keyGUID];
            else
                obj = null;
            SetReference(indexTagValue, obj);
            //seta evento 
            if (TagValue != null)
            {
                CRuntimeCustomTag RuntimeTag = (CRuntimeCustomTag)TagValue;
                RuntimeTag.SetValueEvent += new TagSetValueEventHandler(field_SetValue);
            }
        }
        
        /*!
         * 
         */
        public void SetGuidTagValue(Guid Value)
        {
            this.SetReferenceGuid(indexTagValue, Value);
        }
        /*!
         * 
         */
        public void SetValue(String Value)
        {
            m_value = float.Parse(Value);
        }
        /*!
         * 
         */
        public string GetValue()
        {
            return this.m_value.ToString();
        }
        /*!
         * 
         */
        public PictureBox getPictureBox()
        {
            CRuntimeTrendChart trendChart;
            trendChart = (CRuntimeTrendChart)Owner;
            return trendChart.getPictureBox();
        }
        /*!
         * Adiciona value no inicio da fila
         */
        public void SetValueToQueue()
        {
            penValues.Add(m_value);
        }
        /*!
         * 
         */
        public float GetValueFromQueue(int Index)
        {
            return penValues.Item(Index);
        }

    }
}
