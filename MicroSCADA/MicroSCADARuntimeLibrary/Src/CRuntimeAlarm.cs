using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeAlarm : CRuntimeSystem, ICustomAlarm
    {
        protected CCustomAlarm customAlarm;
        /*!
         * Construtor
         */
        public CRuntimeAlarm(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customAlarm = new CCustomAlarm();
            this.State = 0;
        }
        public bool Enabled
        {
            get { return this.customAlarm.enabled; }
            set { this.customAlarm.enabled = value; }
        }
        public float Value
        {
            get { return this.customAlarm.value; }
            set { this.customAlarm.value = value; }
        }
        public string AlarmMessage
        {
            get { return this.customAlarm.alarmMessage; }
            set { this.customAlarm.alarmMessage = value; }
        }
        public uint Delay { get; set; }

        public int State { get; set; }
    }
}
