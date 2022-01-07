using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public interface ICustomPenTrendChart : ICustomSystem
    {
        String Label { get; set; }
        Color PenColor { get; set; }
        int Width { get; set; }
        ICustomTag TagValue { get; set; }
        void SetGuidTagValue(Guid Value);
    }

    public class CCustomPenTrendChart
    {
        public String label;
        public Color color;
        public int width;
        public CCustomPenTrendChart()
        {
            this.label = "pen";
            this.color = Color.Black;
            this.width = 1;
        }
    }

    public enum CTrendChartOrientation
    {
        LeftToRight,
        RightToLeft
    }

    public interface ICustomTrendChart : ICustomScreenObject
    {
        CTrendChartOrientation Orientation { get; set; }
        float MaxValueY { get; set; }
        float MinValueY { get; set; }
        int BufferSize { get; set; }
        int UpdateTime { get; set; }
        Color ChartAreaColor { get; set; }
        Color PlotAreaColor { get; set; }
        Font ScaleFont { get; set; }
        Color ScaleFontColor { get; set; }
        string Title { get; set; }
        Font TitleFont { get; set; }
        Color TitleFontColor { get; set; }
        ICustomPenTrendChart AddPen();
    }

    public class CCustomTrendChart: IDisposable
    {
        public CTrendChartOrientation orientation;
        public int margin;
        public float maxY;
        public float minY;
        public Color chartAreaColor;
        public Color plotAreaColor;
        public Color gridColor;
        public int bufferSize;
        public int updateTime;
        public int divX;
        public int divY;
        public Rectangle plotRect;
        public Font scaleFont;
        public Color scaleFontColor;
        public string title;
        public Font titleFont;
        public Color titleFontColor;
        private DateTime currentTime;
        
        public CCustomTrendChart()
        {
            this.title = "Trend Chart";
            this.titleFont = new Font("Arial", 18, FontStyle.Italic);
            this.titleFontColor = Color.Black;
            this.scaleFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            this.scaleFontColor = Color.Black;
            this.margin = 20;
            this.maxY = 32768;
            this.minY = 0;
            this.bufferSize = 3600;
            this.updateTime = 1000;
            this.divX = 4;
            this.divY = 4;
            this.chartAreaColor = Color.White;
            this.plotAreaColor = Color.White;
            this.gridColor = Color.Black;
            this.plotRect = new Rectangle();
            this.orientation = CTrendChartOrientation.LeftToRight;
        }

        public void Dispose()
        {

        }
        /*!
         * Calcula area de plotagem
         * @param graphics Superficie de desenho da GDI+
         * @param penList Lista de penas
         * @param width Largura do pictureBox
         * @param height Altura do pictureBox
         */
        private void UpdateRects(Graphics graphics, ArrayList penList, int width, int height)
        {            
            SizeF mSup = graphics.MeasureString(title, titleFont);
            SizeF mInf = graphics.MeasureString("hh:mm:ss", scaleFont);
            SizeF mLat1 = graphics.MeasureString(maxY.ToString(), scaleFont);
            SizeF mLat2 = graphics.MeasureString(minY.ToString(), scaleFont);
            //largura da escala Y
            float w1, w2;
            if (mLat1.Width > mLat2.Width)
                w1 = mLat1.Width;
            else
                w1 = mLat2.Width;
            //largura da legenda
            List<float> widthList = new List<float>();
            foreach (ICustomPenTrendChart pen  in penList)
            {
                string label = string.Format("{0}({1:" + "0.00" + "})", pen.Name, 65535F);
                SizeF wPen = graphics.MeasureString(label, scaleFont);
                widthList.Add(wPen.Width + (wPen.Height / 2));
            }
            if (widthList.Count > 0)
                w2 = widthList.Max();
            else
                w2 = w1;
            //
            if (orientation == CTrendChartOrientation.LeftToRight)
            {
                plotRect.X = (int)w2;
                plotRect.Y = (int)mSup.Height;
                plotRect.Width = width - (int)(w1 + w2);
                plotRect.Height = height - (int)(mSup.Height + mInf.Height);
            }
            else
            {
                plotRect.X = (int)w1;
                plotRect.Y = (int)mSup.Height;
                plotRect.Width = width - (int)(w1 + w2);
                plotRect.Height = height - (int)(mSup.Height + mInf.Height);
            }
        }
        /*!
         * Desenha area do grafico
         * @param graphics Superficie de desenho da GDI+
         * @param penList Lista de penas
         * @param width Largura do pictureBox
         * @param height Altura do pictureBox
         */
        public void DrawChart(Graphics graphics,ArrayList penList,int width,int height)
        {
            Rectangle rect = new Rectangle(0, 0, width - 1, height - 1);
            Pen pen = new Pen(Color.Black);                                 
            SolidBrush sb = new SolidBrush(chartAreaColor);
            //
            graphics.FillRectangle(sb, rect);
            graphics.DrawRectangle(pen, rect);
            //calcula e pinta area de plot
            UpdateRects(graphics, penList, width, height);
            sb = new SolidBrush(plotAreaColor);
            graphics.FillRectangle(sb, plotRect);
            graphics.DrawRectangle(pen, plotRect);
            
            //desenha title centralizado
            SizeF titleSize = graphics.MeasureString(title, titleFont);
            float x = (width - titleSize.Width) / 2;
            float y = 1;
            sb = new SolidBrush(titleFontColor);
            graphics.DrawString(title, titleFont, sb, x, y);
            //
            DrawTimeX(graphics);
            //
            DrawScaleY(graphics);            
            sb.Dispose();
            pen.Dispose();
        }
        /*!
         * Desenha grid 
         * @param graphics Superficie de desenho da GDI+
         * @param location Origem da area de plot
         */
        public void DrawGrid(Graphics graphics, Point location)
        {
            int i, offset;
            float step;
            Pen pen = new Pen(gridColor, 1);
            pen.DashStyle = DashStyle.Dash;
            Point pt1 = new Point();
            Point pt2 = new Point();

            step = (float)plotRect.Width / (float)divX;
            for (i = 1; i < divX; i++)
            {
                offset = (int)Math.Round(i * step);
                pt1.X = location.X + offset;
                pt1.Y = location.Y;
                pt2.X = pt1.X;
                pt2.Y = location.Y + plotRect.Height;
                graphics.DrawLine(pen, pt1, pt2);
            }
            step = (float)plotRect.Height / (float)divY;
            for (i = 1; i < divY; i++)
            {
                offset = (int)Math.Round(i * step);
                pt1.X = location.X;
                pt1.Y = location.Y + offset;
                pt2.X = location.X + plotRect.Width;
                pt2.Y = pt1.Y;
                graphics.DrawLine(pen, pt1, pt2);
            }
            pen.Dispose();
        }
        /*!
         * Desenha escala X (tempo) na parte inferior do Chart
         * @param graphics Superficie de desenho da GDI+ 
         */
        private void DrawTimeX(Graphics graphics)
        {
            const string FORMAT_STRING = "hh:mm:ss";

            float Step, StepX;
            DateTime time;
            int TW, OffSet, Y, X, I;
            
            Step = plotRect.Width / divX;
            //StepX = interval / divX;
            StepX = bufferSize / divX;
            currentTime = DateTime.Now;
            for (I = 0; I < divX + 1; I++)
            {
                time = currentTime.Subtract(TimeSpan.FromSeconds(Math.Round(I * StepX)));
                //time  =time - SecondToDateTime(m_OffSet);
                SizeF sizeF = graphics.MeasureString(FORMAT_STRING, scaleFont);
                TW = (int)(sizeF.Width / 2);
                //ESQUERDA PARA DIREITA
                //    OffSet  =ceil((float)I * Step);
                //DIREITA PARA ESQUERDA
                //OffSet  =(int)Math.Round((divX - I) * Step);
                OffSet = (int)Math.Round(I * Step);
                X = plotRect.Left + OffSet - TW;                
                Y = plotRect.Bottom;
                graphics.DrawString(time.ToLongTimeString(), scaleFont, new SolidBrush(scaleFontColor), X, Y);
            }
        }
        /*!
         * Desenha escala Y
         * @param graphics Superficie de desenho da GDI+ 
         */
        private void DrawScaleY(Graphics graphics)
        {
            Brush brush = new SolidBrush(scaleFontColor);
            float step = (maxY - minY) / (float)divY;
            PointF pt = new PointF();

            for (int i = 0; i <= divY; i++)
            {
                int value = (int)(maxY - Math.Round((double)(step * i), 0, MidpointRounding.ToEven));
                string text = value.ToString();
                SizeF sizeF = graphics.MeasureString(text, scaleFont);
                if (i == 0)
                {
                    pt.Y = plotRect.Top;
                }
                else if (i == divY)
                {
                    pt.Y = plotRect.Bottom - sizeF.Height;
                }
                else
                {
                    pt.Y = ConvertScale(value, minY, maxY, plotRect.Bottom, plotRect.Top) -
                     (sizeF.Height / 2F);                  
                }
                //
                if (orientation == CTrendChartOrientation.LeftToRight)
                {
                    //escala direita
                    pt.X = plotRect.Right;
                }
                else
                {
                    //escala esquerda
                    pt.X = plotRect.Left - sizeF.Width;
                }               
                graphics.DrawString(text, scaleFont, brush, pt);
            }           
        }
        /*!
         * Calcula pontos para desenho do label.
         * @param value Valor Y
         * @param txSize Estrutura com dimençoes do texto
         * @param rect Retangulo de plotagem
         * @param pText Ponto de desenho do texto
         * @return Array de pontos
         */
        private PointF[] CalculatePoints(float value, SizeF txSize, Rectangle rect, ref PointF pText)
        {
            List<PointF> pl = new List<PointF>();
            float midTxH = (txSize.Height / 2F);
            
            if(orientation== CTrendChartOrientation.LeftToRight)
            {
                pl.Add(new PointF(rect.Left - txSize.Width - midTxH, ConvertScale(value, minY, maxY, rect.Bottom, rect.Top) - midTxH));
                pl.Add(new PointF(pl[0].X + txSize.Width, pl[0].Y));
                pl.Add(new PointF(pl[1].X + midTxH, pl[1].Y + midTxH));
                pl.Add(new PointF(pl[2].X - midTxH, pl[2].Y + midTxH));
                pl.Add(new PointF(pl[3].X - txSize.Width, pl[3].Y));
                pText.X = pl[0].X;
                pText.Y = pl[0].Y;
            }
            else 
            {
                pl.Add(new PointF(rect.Right, ConvertScale(value, minY, maxY, rect.Bottom, rect.Top)));
                pl.Add(new PointF(pl[0].X + midTxH, pl[0].Y - midTxH));
                pl.Add(new PointF(pl[1].X + txSize.Width, pl[1].Y));
                pl.Add(new PointF(pl[2].X, pl[2].Y + txSize.Height));
                pl.Add(new PointF(pl[3].X - txSize.Width, pl[3].Y));
                pText.X = pl[0].X + midTxH;
                pText.Y = pl[0].Y - midTxH;
            }
            return pl.ToArray();
        }
        /*!
         * Desenha label das penas.
         * @param graphics Superficie de desenho da GDI+
         * @param penList Lista de penas
         */
        public void DrawPenLabel(Graphics graphics, ArrayList penList)
        {
            Rectangle rect;
            rect = new Rectangle(plotRect.Left, plotRect.Top, plotRect.Width, plotRect.Height);
            foreach (ICustomPenTrendChart penChart in penList)
            {
                //if (penChart.Enabled)
                //{
                    float value = float.Parse(penChart.TagValue.Value);
                    
                    //string label = string.Format("{0}({1:" + penChart.Format + "})", penChart.Label, value);
                    string label = string.Format("{0}({1:" + "0.0" + "})", penChart.Name, value);
                    SizeF txSize = graphics.MeasureString(label, scaleFont);
                    //
                    PointF pText = new PointF();
                    PointF[] pt = CalculatePoints(value, txSize, rect, ref pText);
                    //desenha label                    
                    graphics.FillPolygon(new SolidBrush(penChart.PenColor), pt);
                    graphics.DrawPolygon(new Pen(Color.Black), pt);
                    graphics.DrawString(label, scaleFont, new SolidBrush(scaleFontColor), pText);
                //}
            }
        }
        /*!
         * Converte escalas com função 'y=ax+b'
         * @param Value Valor a ser convertido de x para y
         * @param X0 Valor minimo de entada
         * @param X1 Valor maximo de entada
         * @param Y0 Valor minimo de saida
         * @param Y1 Valor maximo de saida
         * @return Valor saida convertido
         */
        public float ConvertScale(float Value, float X0, float X1, float Y0, float Y1)
        {
            float a, b, x, y;
            a = (Y1 - Y0) / (X1 - X0);
            b = Y1 - a * X1;
            x = Value;
            y = (a * x + b);
            return y;
        }
    }
}
