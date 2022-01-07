using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Tags
{

    public abstract class CDesignInternalTag : CDesignCustomTag, ICustomInternalTag    
    {
        protected CCustomInternalTag customInternalTag;
        private CDemoType m_type;
        /*!
         * Construtor
         */
        public CDesignInternalTag(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customInternalTag = new CCustomInternalTag();
            this.customTag.timeout = 0;
        }
        [Browsable(false)]
        public virtual int ArraySize 
        {
            get { return this.customInternalTag.arraySize; }
            set { this.customInternalTag.arraySize = value; }
        }
        [ReadOnly(true)]
        public virtual CDemoType Type
        {
            get { return m_type; }
            set { m_type = value; }
        }
    }
}
