using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomBitArrayTriggerList : ICustomSystem
    {
        int Slave { get; set; }
        int Address { get; set; }
        ICustomBitArrayTriggerItem NewTriggerItem();
        ICustomBitArrayTriggerProgram NewTriggerProgram();
    }

    public interface ICustomBitArrayTriggerItem : ICustomSystem
    { }

    public interface ICustomBitArrayTriggerProgram : ICustomSystem
    { }
}
