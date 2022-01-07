using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Aga.Controls.Tree;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudio.Src.EnvironmentDesigner
{

    class CAlarmNode : Node
    {
        private CDesignAlarm m_alarm;
        private ImageList m_imageList;
        public CAlarmNode(CDesignAlarm Alarm, ImageList ImageList)
            : base()
        {
            m_alarm = Alarm;
            m_imageList = ImageList;
        }

        public Bitmap Icon { get { return new Bitmap(m_imageList.Images[m_alarm.ImageIndex]); } }

        public string Name { get { return m_alarm.Name; } }

        public string NameOfTag { get { return m_alarm.NameOfTag; } }

        public string Value { get { return m_alarm.Value.ToString(); } }

        public string Enabled { get { return m_alarm.Enabled.ToString(); } }

        public string Delay { get { return m_alarm.Delay.ToString(); } }

        public string Message { get { return m_alarm.AlarmMessage; } }
    }

    class CTreeViewAdvModelOfAlarmTable : TreeModel
    {
        private CDesignAlarmsManager m_alarmManager;
        private ImageList m_imageList;
        private Dictionary<object, Node> m_dictionary;
        public CTreeViewAdvModelOfAlarmTable(CDesignAlarmsManager AlarmManager, ImageList ImageList)
            : base()
        {
            this.m_alarmManager = AlarmManager;
            this.m_imageList = ImageList;
            this.m_dictionary = new Dictionary<object, Node>();
        }

        public void Populate()
        {            
            foreach (CDesignAlarm alarm in CDesignAlarmsManager.alarmList)
            {
                Node node = new CAlarmNode(alarm, m_imageList);
                Root.Nodes.Add(node);
                alarm.DelItem -= alarm_DelItem;
                alarm.DelItem += new DelItemEventHandler(alarm_DelItem);
                m_dictionary.Add(alarm, node);
            }
        }

        private void alarm_DelItem(object sender, EventArgs e)
        {
            Node node = m_dictionary[sender];
            Root.Nodes.Remove(node);
            m_dictionary.Remove(sender);
        }

        public void Clear()
        {
            m_dictionary.Clear();
            Root.Nodes.Clear();
        }

    }
}
