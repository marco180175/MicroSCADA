using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomPasswordList:ICustomSystem
    {
        string[] Password { get; }
        bool Enabled { get; set; }
    }
}
