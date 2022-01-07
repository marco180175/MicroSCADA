using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomBitArrayAlarmList:ICustomSystem
    {
        ICustomBitArrayAlarmItem NewAlarmItem();
    }

    public interface ICustomBitArrayAlarmItem : ICustomSystem
    {
        int Alignment { get; set; }
        string[] Message { get; }
    }
}
