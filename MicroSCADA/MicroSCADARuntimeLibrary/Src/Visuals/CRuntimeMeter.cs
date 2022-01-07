using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimeMeter : CRuntimeCustomField, ICustomMeter
    {
        private CCustomMeter m_customMeter;
        /*!
         * Construtor
         * @param AOwner      
         * @param Project         
         */
        public CRuntimeMeter(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.m_customMeter = new CCustomMeter();
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);
        }
        #region Propriedades
        //!
        public int StartAngle
        {
            get { return m_customMeter.startAngle; }
            set
            {
                m_customMeter.startAngle = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public int SweepAngle
        {
            get { return m_customMeter.sweepAngle; }
            set
            {
                m_customMeter.sweepAngle = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public string Title
        {
            get { return m_customMeter.title; }
            set
            {
                m_customMeter.title = value;
                if (m_customMeter.titleEnabled)
                    pictureBox.Invalidate();
            }
        }
        //!
        public Font TitleFont
        {
            get { return m_customMeter.titleFont; }
            set
            {
                m_customMeter.titleFont = value;
                if (m_customMeter.titleEnabled)
                    pictureBox.Invalidate();
            }
        }
        //!
        public Color TitleFontColor
        {
            get { return m_customMeter.titleFontColor; }
            set
            {
                m_customMeter.titleFontColor = value;
                if (m_customMeter.titleEnabled)
                    pictureBox.Invalidate();
            }
        }
        //!
        public bool TitleEnabled
        {
            get { return m_customMeter.titleEnabled; }
            set
            {
                m_customMeter.titleEnabled = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public Font ScaleFont
        {
            get { return m_customMeter.scaleFont; }
            set
            {
                m_customMeter.scaleFont = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public Color ScaleFontColor
        {
            get { return m_customMeter.scaleFontColor; }
            set
            {
                m_customMeter.scaleFontColor = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public int PointerWidth
        {
            get { return m_customMeter.indicatorWidth; }
            set
            {
                m_customMeter.indicatorWidth = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public double MaxValue
        {
            get { return m_customMeter.maxValue; }
            set
            {
                m_customMeter.maxValue = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public double MinValue
        {
            get { return m_customMeter.minValue; }
            set
            {
                m_customMeter.minValue = value;
                pictureBox.Invalidate();
            }
        }
        //!
        public CSweepDirection Direction
        {
            get { return m_customMeter.direction; }
            set { m_customMeter.direction = value; }
        }
        //!
        public int DivisionsCount
        {
            get { return m_customMeter.divisions; }
            set
            {
                m_customMeter.divisions = value;
                pictureBox.Invalidate();
            }
        }
        #endregion
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            m_customMeter.DrawMeter(e.Graphics, pictureBox);
        }      
    }
}
