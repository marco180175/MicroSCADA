using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public enum CBargraphOrientation
    {
        DownToUpper,
        UpperToDown,
        RightToLeft,
        LeftToRight
    }

    public interface ICustomBargraph : ICustomField, ICustomRange
    {
        Font ScaleFont { get; set; }
        Color FontColor { get; set; }        
        CBargraphOrientation Orientation { get; set; }
        ICustomBargraphElement NewBar();
    }

    public class CCustomBargraph : Object, IDisposable
    {
        public Font font;
        public Color fontColor;
        public float maxValue;
        public float minValue;
        public CBargraphOrientation orientation;
        public Rectangle plotRect;
        private ArrayList barList;
        public CCustomBargraph(ArrayList barList)
        {
            this.font = new Font("Arial", 8, FontStyle.Bold);
            this.fontColor = Color.Black;
            this.barList = barList;
            this.maxValue = 32000;
            this.minValue = 0;
            this.plotRect = new Rectangle();
        }
        /*!
         * Destrutor
         */
        ~CCustomBargraph()
        {
            Dispose();    
        }
        /*!
         * Destrutor
         */
        public virtual void Dispose()
        {
            this.font.Dispose();             	        
        }
        /*!
                 * 
                 */
        private float ConvertToPixels(float Value, float X0, float X1, float Y0, float Y1)
        {
            float a, b, x;
            a = (Y1 - Y0) / (X1 - X0);
            b = Y1 - a * X1;
            x = Value;
            return (a * x + b);
        }
        
        private void DrawBarDownToUpper(Graphics graphics, int Index, float Value)
        {
            ICustomBargraphElement bar = (ICustomBargraphElement)barList[Index];
            int barWidth = (int)Math.Round(((float)plotRect.Width / (float)barList.Count) - 1);
            Brush brush = new SolidBrush(bar.BarColor);
            Rectangle rect = new Rectangle();
            float penHeight = ConvertToPixels(Value, minValue, maxValue, 0, plotRect.Height);
            rect.X = plotRect.X + (Index * (barWidth + 1));
            rect.Y = plotRect.Y + plotRect.Height - (int)penHeight;
            rect.Width = barWidth;
            rect.Height = (int)penHeight;
            graphics.FillRectangle(brush, rect);
        }

        private void DrawBarUpperToDown(Graphics graphics, int Index, float Value)
        {
            ICustomBargraphElement bar = (ICustomBargraphElement)barList[Index];
            int barWidth = (int)Math.Round(((float)plotRect.Width / (float)barList.Count) - 1);
            Brush brush = new SolidBrush(bar.BarColor);
            Rectangle rect = new Rectangle();
            float penHeight = ConvertToPixels(Value, minValue, maxValue, 0, plotRect.Height);
            rect.X = plotRect.X + (Index * (barWidth + 1));
            rect.Y = plotRect.Y;
            rect.Width = barWidth;
            rect.Height = (int)penHeight;
            graphics.FillRectangle(brush, rect);
        }

        private void DrawBarRightToLeft(Graphics graphics, int Index, float Value)
        {
            ICustomBargraphElement bar = (ICustomBargraphElement)barList[Index];
            int barWidth = (int)Math.Round(((float)plotRect.Height / (float)barList.Count) - 1);
            Brush brush = new SolidBrush(bar.BarColor);
            Rectangle rect = new Rectangle();
            float penHeight = ConvertToPixels(Value, minValue, maxValue, 0, plotRect.Width);

            rect.X = plotRect.X + plotRect.Width - (int)penHeight; 
            rect.Y = plotRect.Y + (Index * (barWidth + 1));
            rect.Width = (int)penHeight;
            rect.Height = barWidth;
            graphics.FillRectangle(brush, rect);
        }

        private void DrawBarLeftToRight(Graphics graphics, int Index, float Value)
        {
            ICustomBargraphElement bar = (ICustomBargraphElement)barList[Index];
            int barWidth = (int)Math.Round(((float)plotRect.Height / (float)barList.Count) - 1);
            Brush brush = new SolidBrush(bar.BarColor);
            Rectangle rect = new Rectangle();
            float penHeight = ConvertToPixels(Value, minValue, maxValue, 0, plotRect.Width);

            rect.X = plotRect.X;
            rect.Y = plotRect.Y + (Index * (barWidth + 1));
            rect.Width = (int)penHeight;
            rect.Height = barWidth;
            graphics.FillRectangle(brush, rect);
        }
        /*!
         * 
         */
        public void DrawBar(Graphics graphics,int Index, float Value)
        {
            switch (orientation)
            {
                case CBargraphOrientation.DownToUpper:
                    DrawBarDownToUpper(graphics, Index, Value);
                    break;
                case CBargraphOrientation.UpperToDown:
                    DrawBarUpperToDown(graphics, Index, Value);
                    break;
                case CBargraphOrientation.LeftToRight:
                    DrawBarLeftToRight(graphics, Index, Value);
                    break;
                case CBargraphOrientation.RightToLeft:
                    DrawBarRightToLeft(graphics, Index, Value);
                    break;
            }
        }

        public void DrawHorizontal(Graphics graphics, PictureBox pictureBox)
        {
            String strMax = maxValue.ToString();
            String strMin = minValue.ToString();

            System.Drawing.Size wt1 = TextRenderer.MeasureText(strMax, font);
            System.Drawing.Size wt2 = TextRenderer.MeasureText(strMin, font);
            int bw;
            if (wt1.Width > wt2.Width)
                bw = wt1.Width;
            else
                bw = wt2.Width;

            plotRect.X = bw;
            plotRect.Y = wt1.Height / 2;
            plotRect.Width = pictureBox.Width - 2 * bw;
            plotRect.Height = pictureBox.Height - wt1.Height;
            //
            graphics.DrawRectangle(new Pen(fontColor), new Rectangle(0, 0, pictureBox.Width - 1, pictureBox.Height - 1));
            graphics.DrawRectangle(new Pen(fontColor), plotRect);
            //
            switch (orientation)
            {
                case CBargraphOrientation.LeftToRight:                
                    {
                        graphics.DrawString(strMax, font, new SolidBrush(fontColor), 1, 1);
                        graphics.DrawString(strMax, font, new SolidBrush(fontColor), plotRect.X + plotRect.Width, 1);
                        graphics.DrawString(strMin, font, new SolidBrush(fontColor), plotRect.X - wt2.Width, pictureBox.Height - wt2.Height - 2);
                        graphics.DrawString(strMin, font, new SolidBrush(fontColor), plotRect.X + plotRect.Width, pictureBox.Height - wt2.Height - 2);
                    }; break;
                case CBargraphOrientation.RightToLeft:                
                    {
                        graphics.DrawString(strMin, font, new SolidBrush(fontColor), plotRect.X - wt2.Width, 1);
                        graphics.DrawString(strMin, font, new SolidBrush(fontColor), plotRect.X + plotRect.Width, 1);
                        graphics.DrawString(strMax, font, new SolidBrush(fontColor), 1, pictureBox.Height - wt2.Height - 2);
                        graphics.DrawString(strMax, font, new SolidBrush(fontColor), plotRect.X + plotRect.Width, pictureBox.Height - wt2.Height - 2);
                    }; break;
            }
        }
        public void DrawVertical(Graphics graphics, PictureBox pictureBox)
        {
            String strMax = maxValue.ToString();
            String strMin = minValue.ToString();

            System.Drawing.Size wt1 = TextRenderer.MeasureText(strMax, font);
            System.Drawing.Size wt2 = TextRenderer.MeasureText(strMin, font);
            int bw;
            if (wt1.Width > wt2.Width)
                bw = wt1.Width;
            else
                bw = wt2.Width;

            plotRect.X = bw;
            plotRect.Y = wt1.Height / 2;
            plotRect.Width = pictureBox.Width - 2 * bw;
            plotRect.Height = pictureBox.Height - wt1.Height;
            //
            graphics.DrawRectangle(new Pen(fontColor), new Rectangle(0, 0, pictureBox.Width - 1, pictureBox.Height - 1));
            graphics.DrawRectangle(new Pen(fontColor), plotRect);
            //
            switch (orientation)
            {                
                case CBargraphOrientation.DownToUpper:
                    {
                        graphics.DrawString(strMax, font, new SolidBrush(fontColor), 1, 1);
                        graphics.DrawString(strMax, font, new SolidBrush(fontColor), plotRect.X + plotRect.Width, 1);
                        graphics.DrawString(strMin, font, new SolidBrush(fontColor), plotRect.X - wt2.Width, pictureBox.Height - wt2.Height - 2);
                        graphics.DrawString(strMin, font, new SolidBrush(fontColor), plotRect.X + plotRect.Width, pictureBox.Height - wt2.Height - 2);
                    }; break;                
                case CBargraphOrientation.UpperToDown:
                    {
                        graphics.DrawString(strMin, font, new SolidBrush(fontColor), plotRect.X - wt2.Width, 1);
                        graphics.DrawString(strMin, font, new SolidBrush(fontColor), plotRect.X + plotRect.Width, 1);
                        graphics.DrawString(strMax, font, new SolidBrush(fontColor), 1, pictureBox.Height - wt2.Height - 2);
                        graphics.DrawString(strMax, font, new SolidBrush(fontColor), plotRect.X + plotRect.Width, pictureBox.Height - wt2.Height - 2);
                    }; break;
            }
         
        }
        /*!
         * 
         */
        public void DrawBargraph(Graphics graphics, PictureBox pictureBox)
        {
            switch (orientation)
            {
                case CBargraphOrientation.UpperToDown:
                case CBargraphOrientation.DownToUpper:
                    {
                        DrawVertical(graphics, pictureBox);
                    }; break;
                case CBargraphOrientation.RightToLeft:
                case CBargraphOrientation.LeftToRight:
                    {
                        DrawHorizontal(graphics, pictureBox);
                    }; break;
            }
        }
    }
}
