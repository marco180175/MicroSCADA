using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Tags
{
    public class CRuntimeSRAMTag : CRuntimeInternalTag, ICustomSRAMTag
    {
        private CCustomSRAMTag customSRAMTag;
        public CRuntimeSRAMTag(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customSRAMTag = new CCustomSRAMTag();
        }
        
        
        public override void PutValue(String Value)
        {
            //m_value = Value;
            SetValue(Value, 0);            
        }
    }
}
