using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerKeyboard: CCompilerObject, ICustomKeyboard
    {
        public CCompilerKeyboard(object AOwner):base(AOwner)
        { }
        public string HMICode { get; set; }
        public ICustomKey NewKey()
        {
            return null;
        }
    }
}
