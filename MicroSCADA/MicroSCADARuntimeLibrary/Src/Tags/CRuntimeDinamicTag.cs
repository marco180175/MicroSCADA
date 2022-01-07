using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADARuntimeLibrary.Src.Tags
{
    public abstract class CRuntimeDinamicTag : CRuntimeInternalTag
    {
        public CRuntimeDinamicTag(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            //this.customInternalTag = new CCustomInternalTag();
        }
        /*!
         * 
         */
        public abstract void DoStep();
    }
}
