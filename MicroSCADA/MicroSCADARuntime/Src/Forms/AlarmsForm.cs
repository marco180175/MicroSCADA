using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADARuntimeLibrary.Src;

namespace MicroSCADARuntime.Src.Forms
{
    public partial class AlarmsForm : Form
    {
        private ArrayList alarmList;
        private int index;
        private Boolean blink;
        public AlarmsForm(ArrayList AlarmList)
        {
            InitializeComponent();
            this.alarmList = AlarmList;
            this.index = 0;
            this.blink = false;
        }

        private void AlarmsForm_FormClosed(object sender, FormClosedEventArgs e)
        {            
            CRuntimeAlarmsManager.IsShowing = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (blink)
            {
                pictureBox1.Visible = false;
                label1.Visible = false;
                blink = false;
            }
            else
            {
                pictureBox1.Visible = true;
                label1.Visible = true;
                blink = true;
                //
                if (alarmList.Count > 0)
                {
                    CAlarmRecord alarmRecord;
                    alarmRecord = (CAlarmRecord)alarmList[index];
                    label1.Text = alarmRecord.ToString().ToUpper();
                    if (index < alarmList.Count - 1)
                        index++;
                    else
                        index = 0;
                }
            }

            toolStripStatusLabel1.Text = alarmList.Count.ToString();
        }

        private void AlarmsForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < alarmList.Count; i++)
            {
                alarmList[i] = null;                
            }
            alarmList.Clear();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
