using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src
{

    public class CAlarmRecord
    {
        private DateTime dateTime;
        private CRuntimeAlarm alarm;
        private bool m_returnMessage;
        public CAlarmRecord(CRuntimeAlarm Alarm, bool ReturnMessage)
        {
            this.dateTime = DateTime.Now;
            this.alarm = Alarm;
            this.m_returnMessage = ReturnMessage;
        }
        public override string ToString()
        {
            return dateTime.ToLongTimeString() + " " + AlarmMessage;
        }
        public DateTime DateTime
        {
            get { return this.dateTime; }
        }
        public String AlarmMessage
        {
            get 
            {
                if (m_returnMessage)
                    return this.alarm.AlarmMessage+" return of alarm !"; 
                else
                    return this.alarm.AlarmMessage; 
            }
        }
    }

    public class CRuntimeAlarmsManager : CRuntimeSystem, ICustomAlarmsManager
    {        
        protected CCustomAlarmsManager customAlarmsManager;
        protected ArrayList alarmList;
        //public DataSet alarmDataSet;
        //protected string tableFileName;
        private DataTable m_alarmDataTable;
        //protected AlarmsTableForm alarmsTable;
        public event RegisterAlarmEventHandler RegisterAlarm;
        public static bool IsShowing = false;
        const string NAME_COLUMN_1 = "DateTime";
        const string NAME_COLUMN_2 = "AlarmMessage";
        public CRuntimeAlarmsManager(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customAlarmsManager = new CCustomAlarmsManager();
            this.alarmList = new ArrayList();            
            //
            this.m_alarmDataTable = new DataTable("AlarmsTable");
            this.m_alarmDataTable.Columns.Add(NAME_COLUMN_1, typeof(DateTime));
            this.m_alarmDataTable.Columns.Add(NAME_COLUMN_2, typeof(String));
            //
            //this.alarmDataSet = new DataSet("AlarmsDataSet");            
            //this.alarmDataSet.Tables.Add(this.alarmDataTable);
            //this.alarmsTable = null;
        }
        public DataTable Table
        {
            get { return m_alarmDataTable; }
        }
        public Boolean ShowPopup
        {
            get { return this.customAlarmsManager.showPopup; }
            set { this.customAlarmsManager.showPopup = value; }
        }
        public int TimeOn
        {
            get { return this.customAlarmsManager.timeOn; }
            set { this.customAlarmsManager.timeOn = value; }
        }
        public int TimeOff
        {
            get { return this.customAlarmsManager.timeOff; }
            set { this.customAlarmsManager.timeOff = value; }
        }
        public bool EnabledLogRegisters { get; set; }
        public string LogFileName { get; set; }
        public uint MaxRegisterCount { get; set; }
        /*!
         * 
         */
        public void Register(CRuntimeAlarm alarm,bool returnMessage)
        {
            CAlarmRecord alarmRecord = new CAlarmRecord(alarm, returnMessage);
            alarmList.Add(alarmRecord);
            AddAlarmRecord(alarmRecord);
            if (IsShowing == false && ShowPopup == true)
            {
                IsShowing = true;
                OnRegisterAlarm(new RegisterAlarmEventArgs(alarmList));                
            }
        }

        private void OnRegisterAlarm(RegisterAlarmEventArgs e)
        {
            if (RegisterAlarm != null)
                RegisterAlarm(this, e);
        }

        private void AddAlarmRecord(CAlarmRecord alarmRecord)
        {
            //
            //System.IO.FileInfo fi = new System.IO.FileInfo(project.FileName);
            //this.tableFileName = fi.DirectoryName + "\\AlarmsTable.xml";

            DataRow dataRow = m_alarmDataTable.NewRow();
            dataRow[NAME_COLUMN_1] = alarmRecord.DateTime;
            dataRow[NAME_COLUMN_2] = alarmRecord.AlarmMessage;
            m_alarmDataTable.Rows.Add(dataRow);
            //alarmDataTable.WriteXml(this.tableFileName);            
        }      
    }

    public class RegisterAlarmEventArgs : EventArgs
    {
        ArrayList m_alarmList;
        public RegisterAlarmEventArgs(ArrayList AlarmList)
        {
            m_alarmList=AlarmList;
        }
        public ArrayList AlarmList { get { return m_alarmList; } }
    }
    public delegate void RegisterAlarmEventHandler(object sender, RegisterAlarmEventArgs e);
}
