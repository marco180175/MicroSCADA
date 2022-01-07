using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomAlarmsManager : ICustomObject
    {
        Boolean ShowPopup { get; set; }
        int TimeOn { get; set; }
        int TimeOff { get; set; }
        bool EnabledLogRegisters { get; set; }        
        string LogFileName { get; set; }        
        uint MaxRegisterCount { get; set; }
    }

    public class CCustomAlarmsManager
    {        
        public Boolean showPopup;        
        public int timeOn;
        public int timeOff;
        public CCustomAlarmsManager()
        {
            this.showPopup = true;
            this.timeOff = 1000;
            this.timeOn = 1500;
        }
    }
}
