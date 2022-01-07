using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomAlarm : ICustomSystem
    {
        bool Enabled { get; set; }
        float Value { get; set; }
        string AlarmMessage { get; set; }
        uint Delay { get; set; }
    }

    public class CCustomAlarm
    {
        public Boolean enabled;
        public float value;
        public String alarmMessage;
        public CCustomAlarm()
        {
            this.enabled = false;
            this.value = 0;
            this.alarmMessage = String.Empty;
        }
    }
}
