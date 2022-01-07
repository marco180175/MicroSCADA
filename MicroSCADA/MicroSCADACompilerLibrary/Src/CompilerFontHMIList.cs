using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerFontHMI : CCompilerSystem, ICustomFontHMI
    {
        public CCompilerFontHMI(Object AOwner, CCompilerProject Project)
            : base(AOwner, Project)
        {

        }

        public int Size { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool Strikeout { get; set; }
        public bool Full { get; set; }
    }

    class CCompilerFontHMIList: CCompilerSystem, ICustomFontHMIList
    {
        public CCompilerFontHMIList(Object AOwner, CCompilerProject Project)
            : base(AOwner, Project)
        {
         
        }

        public ICustomFontHMI NewFont()
        {
            CCompilerFontHMI font = new CCompilerFontHMI(this, project);
            ObjectList.Add(font);
            return font;
        }
    }
}
