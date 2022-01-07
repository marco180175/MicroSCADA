using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudioLibrary.Src.TypeConverter
{
    //classe para mostrar propriedades do tag
    public class CTagTypeConverter : ExpandableObjectConverter
    {

        public override bool CanConvertTo(ITypeDescriptorContext context,
                                  System.Type destinationType)
        {
            if (destinationType == typeof(CDesignCustomTag))
                return true;

            return base.CanConvertTo(context, destinationType);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context,
                              System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }
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

        public override object ConvertFrom(ITypeDescriptorContext context,
                                      CultureInfo culture, object value)
        {
            /*
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    int colon = s.IndexOf(':');
                    int comma = s.IndexOf(',');

                    if (colon != -1 && comma != -1)
                    {
                        string checkWhileTyping = s.Substring(colon + 1,
                                                        (comma - colon - 1));

                        colon = s.IndexOf(':', comma + 1);
                        comma = s.IndexOf(',', comma + 1);

                        string checkCaps = s.Substring(colon + 1,
                                                        (comma - colon - 1));

                        colon = s.IndexOf(':', comma + 1);

                        string suggCorr = s.Substring(colon + 1);

                        CDesignCustomTag so = new CDesignCustomTag(null,null,null);

                        so.SpellCheckWhileTyping = Boolean.Parse(checkWhileTyping);
                        so.SpellCheckCAPS = Boolean.Parse(checkCaps);
                        so.SuggestCorrections = Boolean.Parse(suggCorr);

                        return so;
                    }
                }
                catch
                {
                    throw new ArgumentException(
                        "Can not convert '" + (string)value +
                                           "' to type SpellingOptions");
                }
            }
             */
            return base.ConvertFrom(context, culture, value);
        }
    }
}
