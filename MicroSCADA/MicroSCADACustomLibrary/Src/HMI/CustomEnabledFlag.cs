using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomEnabledFlag:ICustomSystem
    {
        int Slave { get; set; }
        int Address { get; set; }
    }
}
