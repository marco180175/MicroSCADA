using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudioLibrary.Src
{
    /*!
     *  
     */
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class CDesignAlarm : CDesignSystem, ICustomAlarm
    {
        private CCustomAlarm m_customAlarm;
        private CDesignCustomTag m_owner;

        public CDesignAlarm(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.m_customAlarm = new CCustomAlarm();
            this.m_owner = (CDesignCustomTag)AOwner;
            CDesignAlarmsManager.alarmList.Add(this);
            this.imageIndex = 40;
            this.SetGUID(Guid.NewGuid());
        }
        /*!
         * 
         */
        public override void Dispose()
        {
            CDesignAlarmsManager.alarmList.Remove(this);
            OnDelItem(EventArgs.Empty);    
            base.Dispose();
        }        
        #region Propriedades
        //!
        [Browsable(false)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.customObject.name = value; }
        }
        //!
        [Browsable(false)]
        public string NameOfTag
        {
            get { return this.m_owner.Name; }
        }
        public Boolean Enabled
        {
            get { return this.m_customAlarm.enabled; }
            set { this.m_customAlarm.enabled = value; }
        }
        public float Value
        {
            get { return this.m_customAlarm.value; }
            set { this.m_customAlarm.value = value; }
        }
        public string AlarmMessage
        {
            get { return this.m_customAlarm.alarmMessage; }
            set { this.m_customAlarm.alarmMessage = value; }
        }

        public uint Delay { get; set; }
        #endregion

    }
}
