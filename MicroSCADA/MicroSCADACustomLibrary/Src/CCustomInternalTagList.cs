using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{    
    public interface ICustomInternalTagList : ICustomSystem
    {
        ICustomDemoTag AddDemoTag();
        ICustomSRAMTag AddSRAMTag();
        ICustomTimerTag NewTimerTag();
        ICustomInternalTagList AddGroup();
    }

    public class CCustomInternalTagList : Object
    {     
        public CCustomInternalTagList()
            : base()
        {
            //this.objectList = new List<CCustomInternalTag>();
        }       
    }
}
