using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudioLibrary.Src.TypeConverter
{
    public class CAlarmTypeConverter : ExpandableObjectConverter
    {
        /*!
         * 
         */
        public override bool CanConvertTo(ITypeDescriptorContext context,
                          System.Type destinationType)
        {
            if (destinationType == typeof(CDesignCustomTag))
                return true;

            return base.CanConvertTo(context, destinationType);
        }
        /*!
         * 
         */
        public override bool CanConvertFrom(ITypeDescriptorContext context,
                              System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }
        /*!
         * 
         */
        public override object ConvertTo(ITypeDescriptorContext context,
                               CultureInfo culture,
                               object value,
                               System.Type destinationType)
        {
            if (destinationType == typeof(System.String) &&
                 value is CDesignCustomTag)
            {

                CDesignCustomTag so = (CDesignCustomTag)value;

                return "p1=" + so.Name;

            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        /*!
         * 
         */
        public override object ConvertFrom(ITypeDescriptorContext context,
                                      CultureInfo culture, object value)
        {
            
            return base.ConvertFrom(context, culture, value);
        }
    }
}
