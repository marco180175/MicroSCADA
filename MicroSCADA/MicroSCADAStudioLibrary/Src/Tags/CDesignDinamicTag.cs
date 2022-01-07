using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Tags
{
    public abstract class CDesignDinamicTag : CDesignInternalTag
    {
        /*!
         * Construtor
         * @param AOwner Referencia para objeto proprietario        
         * @param Project Referencia para o projeto
         */
        public CDesignDinamicTag(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            //this.customDemoTag = new CCustomDemoTag();
            //this.m_imageIndex = 19;
            //this.Value = "0";
            //this.customTag.dataType = CCustomDataType.dtInt64;
        }
        /*!
         * 
         */
        public override void Dispose()
        {            
            //this.customDemoTag.Dispose();
            base.Dispose();
        }


    }
}
