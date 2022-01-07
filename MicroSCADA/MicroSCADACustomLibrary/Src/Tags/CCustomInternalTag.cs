using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    

    public interface ICustomInternalTag : ICustomTag
    {
        CDemoType Type { get; set; }
        //int ArraySize { get; set; }
    }

    public class CCustomInternalTag : Object    
    {        
        public int arraySize;        
        /*!
         * Construtor
         */
        public CCustomInternalTag()
            : base()
        {            
            this.arraySize = 0;                       
        }
        
    }
}
