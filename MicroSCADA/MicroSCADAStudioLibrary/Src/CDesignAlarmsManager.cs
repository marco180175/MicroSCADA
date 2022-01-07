using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src
{
    public class CDesignAlarmsManager : CDesignSystem, ICustomAlarmsManager
    {
        protected CCustomAlarmsManager customAlarmsManager;
        public CDesignAlarmsManager(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customAlarmsManager = new CCustomAlarmsManager();
            this.Name = "AlarmsManager";
            this.MaxRegisterCount = 100;
            this.SetGUID( Guid.NewGuid());
            this.imageIndex = 36;
        }
        //!
        [ReadOnly(true)]
        new public string Name 
        {
            get { return base.Name; }
            set { base.Name = value; }
        }        
        //!
        [Category("Popup Dialog")]
        public bool ShowPopup
        {
            get { return this.customAlarmsManager.showPopup; }
            set { this.customAlarmsManager.showPopup = value; }
        }
        //!
        [Category("Popup Dialog")]
        public int TimeOn
        {
            get { return this.customAlarmsManager.timeOn; }
            set { this.customAlarmsManager.timeOn = value; }
        }
        //!
        [Category("Popup Dialog")]
        public int TimeOff
        {
            get { return this.customAlarmsManager.timeOff; }
            set { this.customAlarmsManager.timeOff = value; }
        }
        //!
        [Category("Log File")]
        public bool EnabledLogRegisters { get; set; }
        //!
        [Category("Log File")]
        public string LogFileName { get; set; }
        //!
        [Category("Log File")]
        public uint MaxRegisterCount { get; set; }

        public static List<CDesignAlarm> alarmList = new List<CDesignAlarm>();
        public CDesignAlarm[] AlarmList { get { return alarmList.ToArray(); } }
    }
}
