using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.HMI;
namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerEnabledFlag: CCompilerSystem, ICustomEnabledFlag
    {
        public CCompilerEnabledFlag(Object AOwner, CCompilerProject Project) :
            base(AOwner,Project)
        { }
        public int Slave { get; set; }
        public int Address { get; set; }
    }
}
