using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    /*!
     * Formato do valor mostrado pelo campo
     */
    public enum CValueFormat
    {
        fmText,//!< char, string        
        fmDecimal,//!< byte,sbyte,int16,uint16,int32,uint32,int64,uint64
        fmHexadecimal,//!< byte,sbyte,int16,uint16,int32,uint32,int64,uint64
        fmBinary,//!< byte,sbyte,int16,uint16,int32,uint32,int64,uint64
        fmFixedPoint,//!< float,double
        fmScientific,//!< float,double
        fmTimer,//!< timer       
        fmDateTime//!< time,date,datetime
    }

    public interface ICustomAlphaNumeric : ICustomField
    {
        CValueFormat ValueFormat { get; set; }        
        int DecimalCount { get; set; }
        double MaxValue { get; set; }
        double MinValue { get; set; }
        Font Font { get; set; }
        Color FontColor { get; set; }        
        HorizontalAlignment TextAlign { get; set; }  
    }

    public class CCustomAlphaNumeric : IDisposable
    {       
        public CValueFormat valueFormat;
        public Boolean readOnly;
        public int decimalCount;
        public double maxValue;
        public double minValue;
        public Font font;
        public Color fontColor;        
        public HorizontalAlignment textAlign;       
        /*!
         * Construtor         
         */
        public CCustomAlphaNumeric()
            : base()
        {            
            this.font = new Font("Arial", 16, FontStyle.Bold);
            this.fontColor = Color.Black;            
            this.readOnly = false;
            this.valueFormat = CValueFormat.fmDecimal;
            this.decimalCount = 0;
            this.maxValue = 30000;
            this.minValue = -30000;
            this.textAlign = HorizontalAlignment.Center;
        }        
        /*!
         * Destrutor
         */
        ~CCustomAlphaNumeric()
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

        public void DrawAlfaNumeric(Graphics graphics, PictureBox pictureBox, string value)
        {
            if (pictureBox != null)
            {
                Rectangle rect = new Rectangle(0, 0, pictureBox.Width, font.Height);
                StringFormat sf = new StringFormat();
                SolidBrush sb = new SolidBrush(fontColor);
                switch(textAlign)
                {
                    case HorizontalAlignment.Left:
                        sf.Alignment = StringAlignment.Near;
                        break;
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                    default:
                        sf.Alignment = StringAlignment.Center;
                        break;
                }
                rect.Y = (pictureBox.Height - font.Height) / 2;                
                graphics.DrawString(value, font, sb, rect, sf);
                sb.Dispose();
            }
        }
        /*!
         * Formata valor
         */
        public string FormatValue(string inputValue, CCustomDataType DataType)
        {
            string formatFormat;
            switch (DataType)
            {
                case CCustomDataType.dtSByte:
                case CCustomDataType.dtInt16:
                case CCustomDataType.dtInt32:
                case CCustomDataType.dtInt64:
                case CCustomDataType.dtByte:
                case CCustomDataType.dtUInt16:
                case CCustomDataType.dtUInt32:
                case CCustomDataType.dtUInt64:
                    {
                        Int64 int64Value = Int64.Parse(inputValue);
                        switch (valueFormat)
                        {
                            case CValueFormat.fmDecimal:
                                return inputValue;
                            case CValueFormat.fmHexadecimal:
                                {
                                    int s = CCustomTag.GetSizeOf(DataType) * 2;
                                    formatFormat = "{0:X" + s.ToString() + "}";
                                    return string.Format(formatFormat, int64Value);
                                }
                            case CValueFormat.fmBinary:
                                {
                                    return inputValue = Convert.ToString(int64Value, 2);
                                }
                            default:
                                return inputValue;
                        }
                    }
                case CCustomDataType.dtFloat32:
                case CCustomDataType.dtFloat64:
                    {
                        double doubleValue = double.Parse(inputValue);
                        
                        switch (valueFormat)
                        {
                            case CValueFormat.fmFixedPoint:                                
                                formatFormat = "{0:F" + decimalCount.ToString() + "}";
                                return string.Format(formatFormat, doubleValue);
                            case CValueFormat.fmScientific:
                                formatFormat = "{0:E" + decimalCount.ToString() + "}";
                                return string.Format(formatFormat, doubleValue);
                            default:
                                return inputValue;
                        }                        
                    };
                case CCustomDataType.dtTimer:
                    long longValue = long.Parse(inputValue);
                    TimeSpan ts = new TimeSpan(longValue);
                    return ts.ToString("hh\\:mm\\:ss\\.fff");
                case CCustomDataType.dtTime:
                    longValue = long.Parse(inputValue);
                    DateTime dt = new DateTime(longValue);
                    return dt.ToString("hh:mm:ss");
                case CCustomDataType.dtDate:
                    longValue = long.Parse(inputValue);
                    dt = new DateTime(longValue);
                    return dt.ToString("dd/MM/yyyy");
                case CCustomDataType.dtDateTime:
                    longValue = Int64.Parse(inputValue);
                    dt = new DateTime(longValue);
                    return dt.ToString();
                default:
                    return inputValue;                   
            }               
        }
        
    }
}
