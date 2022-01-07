using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomSRAMTag : ICustomInternalTag, IArrayOfTag
    {

    }

    public class CCustomSRAMTag : Object    
    {                           
        /*!
         * Construtor
         */
        public CCustomSRAMTag()
            : base()
        {            
            
        }       
    }
}
