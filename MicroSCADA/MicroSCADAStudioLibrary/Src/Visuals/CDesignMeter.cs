using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * 
     */
    public class CDesignMeter : CDesignCustomField, ICustomMeter
    {
        private CCustomMeter m_customMeter;
        public CDesignMeter(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject();
            this.m_customMeter = new CCustomMeter();            
            this.imageIndex = 48;  
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
        public int DivisionsCount
        {
            get { return m_customMeter.divisions; }
            set
            {
                m_customMeter.divisions = value;
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
        #endregion
        #region Funçoes        
        protected override void  pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (TagValue != null)
            {
                switch (TagValue.DataType)
                {
                    case CCustomDataType.dtByte:
                    case CCustomDataType.dtSByte:
                    case CCustomDataType.dtInt16:
                    case CCustomDataType.dtInt32:
                    case CCustomDataType.dtInt64:
                    case CCustomDataType.dtUInt16:
                    case CCustomDataType.dtUInt32:
                    case CCustomDataType.dtUInt64:
                    case CCustomDataType.dtFloat32:
                    case CCustomDataType.dtFloat64:
                        m_customMeter.SetValue(double.Parse(TagValue.Value));
                        break;
                    default:
                        m_customMeter.SetValue(0);
                        break;
                }
            }
            else
                m_customMeter.SetValue(0);
            m_customMeter.DrawMeter(e.Graphics, pictureBox);
            //
            if (selected)
                DrawSelectedRect(e.Graphics);
        }
        #endregion
    }
}
