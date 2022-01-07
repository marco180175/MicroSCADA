using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src.Tags
{
    public abstract class CRuntimeInternalTag : CRuntimeCustomTag, ICustomInternalTag
    {
        protected CCustomInternalTag customInternalTag;
        private CDemoType m_type;
        public CRuntimeInternalTag(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customInternalTag = new CCustomInternalTag();
        }
        public virtual CDemoType Type
        {
            get { return m_type; }
            set { m_type = value; }
        }       
    }
}
