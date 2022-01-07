using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace MicroSCADACustomLibrary.Src.Visuals
{
    //Specifies that arcs are drawn in a clockwise (positive-angle) direction.
    public enum CSweepDirection
    {
        Clockwise	,
        CounterClockwise
    }

    public interface ICustomMeter : ICustomField
    {        
        int StartAngle { get; set; }
        int SweepAngle { get; set; }
        string Title { get; set; }
        Font TitleFont { get; set; }
        Color TitleFontColor { get; set; }
        bool TitleEnabled { get; set; }
        Font ScaleFont { get; set; }
        Color ScaleFontColor { get; set; }
        int PointerWidth { get; set; }
        //Color PointerColor { get; set; }
        double MaxValue { get; set; }
        double MinValue { get; set; }
        CSweepDirection Direction { get; set; }
        int DivisionsCount { get; set; }
    }

    public class CCustomMeter
    {
        private double m_value;        
        public int startAngle;
        public int sweepAngle;
        public string title;
        public Font titleFont;
        public Color titleFontColor;
        public bool titleEnabled;
        public Font scaleFont;
        public Color scaleFontColor;
        public int divisions;
        public int divisionsWidth;
        public Color divisionsColor;
        public int arcWidth;
        public int indicatorWidth;
        public Color indicatorColor;
        public double maxValue;
        public double minValue;
        public CSweepDirection direction;
        private Rectangle m_rect;
        private int m_offsetY;
        private float m_r;//!< raio
        private PointF m_pc;
        public CCustomMeter() 
        {
            title = "Meter";
            titleFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            titleFontColor = Color.Black;
            titleEnabled = true;
            scaleFont = new Font("Courier New", 9, FontStyle.Regular);
            scaleFontColor = Color.Black;
            divisions = 10;
            //arcWidth = 15;
            divisionsWidth = 3;
            divisionsColor = Color.Red;
            arcWidth = 10;
            startAngle = 45;
            sweepAngle = -270;            
            indicatorWidth = 5;
            indicatorColor = Color.Blue;
            maxValue = 1000;
            minValue = 0;
            direction = CSweepDirection.CounterClockwise;
            m_pc = new PointF();
        }

        public void SetValue(double Value)
        {
            if (Value >= minValue && Value <= maxValue)
                m_value = Value;
        }

        private void WriteTitle(Graphics graphics, Control control)
        {
            SizeF titleSize = graphics.MeasureString(title, titleFont);
            float tx = (control.Width - titleSize.Width) / 2;
            graphics.DrawString(title, titleFont, new SolidBrush(titleFontColor), tx, 0);
            m_offsetY = (int)titleSize.Height;
            graphics.DrawLine(new Pen(Color.Black), 0, m_offsetY-1, control.Width, m_offsetY-1);
        }

        private void WriteScale(Graphics graphics, Control control)
        {
            const int TOLERANCE = 5;
            int step = sweepAngle / divisions;            
            int start;
            for (int i = 0; i <= divisions; i++)
            {
                start = startAngle + (step * i);
                double angleRag = (float)(start * Math.PI / 180F);
                float dx = (float)(m_r * Math.Cos(angleRag)) + m_pc.X;
                float dy = (float)(m_r * Math.Sin(angleRag)) + m_pc.Y;

                int v = (int)CSysUtils.ConvertScale((step * i), 0, sweepAngle, minValue, maxValue);
                SizeF mLat1 = graphics.MeasureString(v.ToString(), scaleFont);
                if ((dx >= m_pc.X) && (dy <= m_pc.Y))
                {
                    if ((dx - m_pc.X) < TOLERANCE)
                        graphics.DrawString(v.ToString(), scaleFont, new SolidBrush(scaleFontColor), dx - (mLat1.Width / 2F), dy - mLat1.Height);
                    else if ((m_pc.X-dy) < TOLERANCE)
                        graphics.DrawString(v.ToString(), scaleFont, new SolidBrush(scaleFontColor), dx , dy - (mLat1.Height/2F));
                    else
                        graphics.DrawString(v.ToString(), scaleFont, new SolidBrush(scaleFontColor), dx, dy - mLat1.Height);
                }
                //else if (dx < m_pc.X && dy < m_pc.Y)
                //{
                //    if ((m_pc.X - dx) < TOLERANCE)
                //        graphics.DrawString(v.ToString(), scaleFont, new SolidBrush(scaleFontColor), dx - (mLat1.Width / 2F), dy - mLat1.Height);
                //    else
                //        graphics.DrawString(v.ToString(), scaleFont, new SolidBrush(scaleFontColor), dx - mLat1.Width, dy - mLat1.Height);
                //}
                //else if (dx < m_pc.X && dy > m_pc.Y)
                //{
                //    if ((dy-m_pc.Y) < TOLERANCE)
                //        graphics.DrawString(v.ToString(), scaleFont, new SolidBrush(scaleFontColor), dx - mLat1.Width, dy - (mLat1.Height/2F));
                //    else
                //        graphics.DrawString(v.ToString(), scaleFont, new SolidBrush(scaleFontColor), dx - mLat1.Width, dy);
                //}
                //else if (dx > m_pc.X && dy > m_pc.Y)
                //{
                //    graphics.DrawString(v.ToString(), scaleFont, new SolidBrush(scaleFontColor), dx, dy);
                //}
            }
        }

        private void DrawScale(Graphics graphics, Control control)
        {
            int step = sweepAngle / divisions;
            int start;
            for (int i = 0; i <= divisions; i++)
            {
                start = startAngle + (step * i);
                double angleRag = (float)(start * Math.PI / 180F);
                float dx = (float)(m_r * Math.Cos(angleRag));
                float dy = (float)(m_r * Math.Sin(angleRag));
                float sx = (float)((m_r - 20) * Math.Cos(angleRag));
                float sy = (float)((m_r - 20) * Math.Sin(angleRag));
                graphics.DrawLine(new Pen(divisionsColor, divisionsWidth), m_pc.X + sx, m_pc.Y + sy, m_pc.X + dx, m_pc.Y + dy);
            }
        }

        public void DrawMeter(Graphics graphics, Control control)
        {
            int angleDeg = startAngle + (int)CSysUtils.ConvertScale(m_value, minValue, maxValue, 0, sweepAngle);
            
            if (titleEnabled)            
                WriteTitle(graphics, control);            
            else
                m_offsetY = 0;
            m_rect = new Rectangle(0, m_offsetY, control.Width - 1, control.Height - m_offsetY - 1);
            graphics.FillRectangle(new SolidBrush(control.BackColor), m_rect);
            //graphics.DrawRectangle(new Pen(Color.Black), m_rect);
            SizeF mLat1 = graphics.MeasureString(maxValue.ToString(), scaleFont);
            SizeF mLat2 = graphics.MeasureString(minValue.ToString(), scaleFont);
            //largura da escala Y
            float w1;
            if (mLat1.Width > mLat2.Width)
                w1 = mLat1.Width;
            else
                w1 = mLat2.Width;

            float d;
            if (m_rect.Width < m_rect.Height)
                d = m_rect.Width-w1;
            else
                d = m_rect.Height-w1;
            m_r = d / 2F;
            float x = (m_rect.Width - d) / 2F;
            float y = ((m_rect.Height - d) / 2F) + m_offsetY;
            //graphics.DrawArc(new Pen(Color.Black, arcWidth), x + arcWidth/2, y + arcWidth/2, d - arcWidth, d - arcWidth, startAngle, sweepAngle);
            m_pc.X = m_rect.Width / 2F;
            m_pc.Y = (m_rect.Height / 2F) + m_offsetY;
            float angleRag = (float)(angleDeg * Math.PI / 180F);
            float dx = (float)(m_r * Math.Cos(angleRag));
            float dy = (float)(m_r * Math.Sin(angleRag));
            graphics.DrawLine(new Pen(indicatorColor, indicatorWidth), m_pc.X, m_pc.Y, m_pc.X + dx, m_pc.Y + dy);
            //desenha escala
            DrawScale(graphics, control);
            //escreve escala
            WriteScale(graphics, control);
        }
    }
}
