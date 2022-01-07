using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public interface IArrayOfTag
    {
        int ArraySize { get; set; }
    }

    public interface IControlOfTag
    {
        bool Enabled { get; set; }
        int Scan { get; set; }
    }

    public interface ICustomTag : ICustomSystem
    {
        CCustomDataType DataType { get; set; }
        string Value { get; set; }               
        ICustomAlarm AlarmHi { get; }
        ICustomAlarm AlarmLo { get; }        
    }

    public class CCustomTag : Object    
    {
        public CCustomDataType dataType;
        public int scanTime;
        public int tickCount;
        public Boolean enabled;
        public int timeout;
        public string value;
        /*!
         * Construtor
         */
        public CCustomTag()
            : base()
        {            
            this.dataType = CCustomDataType.dtInt16;
            this.value = "0";
            this.scanTime = 1000;
            this.tickCount = 1000;
            this.enabled = true;
            this.timeout = 1000;
        }

        public static int GetSizeOf(CCustomDataType dt)
        {
            switch (dt)
            {
                case CCustomDataType.dtBool:
                    return sizeof(bool);
                case CCustomDataType.dtSByte:
                    return sizeof(sbyte);
                case CCustomDataType.dtByte:
                    return sizeof(byte);
                case CCustomDataType.dtInt16:
                    return sizeof(Int16);
                case CCustomDataType.dtUInt16:
                    return sizeof(UInt16);
                //case CCustomDataType.dtWORD:                    
                case CCustomDataType.dtInt32:
                    return sizeof(Int32);
                case CCustomDataType.dtUInt32:
                    return sizeof(UInt32);
                case CCustomDataType.dtInt64:
                    return sizeof(Int64);
                case CCustomDataType.dtUInt64:
                    return sizeof(UInt64);
                //case CCustomDataType.dtDWORD:                    
                case CCustomDataType.dtFloat32:
                    return sizeof(float);
                case CCustomDataType.dtFloat64:
                    return sizeof(double);
                case CCustomDataType.dtTimer:
                case CCustomDataType.dtTime:
                case CCustomDataType.dtDate:
                case CCustomDataType.dtDateTime:
                    return sizeof(Int64);
                case CCustomDataType.dtChar:
                    return sizeof(char);
                //case CCustomDataType.dtString:
                //    return m_stringSize;
                default:
                    return 0;
            }
        }
    }
}
