using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomBitArrayList:ICustomSystem
    {
        ICustomBitArrayTriggerList NewTriggerList();
        ICustomBitArrayAlarmList NewAlarmList();
    }
}
