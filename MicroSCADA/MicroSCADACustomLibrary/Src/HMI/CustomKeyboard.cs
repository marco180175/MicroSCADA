using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomKeyboard : ICustomObject
    {
        string HMICode { get; set; }
        ICustomKey NewKey();
    }
    public class CCustomKeyboard
    {
    }
}
