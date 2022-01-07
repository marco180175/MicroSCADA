using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomFontHMI : ICustomSystem
    {
        int Size { get; set; }
        bool Bold { get; set; }
        bool Italic { get; set; }
        bool Underline { get; set; }
        bool Strikeout { get; set; }
        bool Full { get; set; }
    }

    public interface ICustomFontHMIList : ICustomSystem
    {
        ICustomFontHMI NewFont();
    }
}
