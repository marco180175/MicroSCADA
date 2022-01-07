using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public enum CFieldType { ftRead, ftReadWrite }

    public enum CDinamicType { dtSequence, dtRange }

    public interface ICustomField : ICustomScreenObject
    {
        CFieldType FieldType { get; set; }
        ICustomTag TagValue { get; set; }
        void SetGuidTagValue(Guid Value);
    }

    public class CCustomField : Object
    {
        public CFieldType fieldType;        
        public CCustomField()
        {
            this.fieldType = CFieldType.ftRead;            
        }
    }
}
