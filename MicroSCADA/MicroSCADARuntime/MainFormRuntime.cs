using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADARuntime.Src;
using MicroSCADARuntime.Src.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.IOFiles;
using MicroSCADARuntime.Src.Threads;
using MicroSCADARuntimeLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src;

namespace MicroSCADARuntime
{
    public partial class MainFormRuntime : Form
    {
        private CRuntimeProject Project;
        private CRuntimeScreen m_screen;
        //private CThreadServiceTags m_threadServiceTags;
        private CThreadInternalTags m_threadInternalTags;
        private AlarmsTableForm m_alarmTable;
        //private CVirtualMCU VirtualMCU;
        //private ServiceReference1.Service1Client m_proxy;
        private delegate void ShowAlarmsCallback(AlarmsForm alarmsDialog);
        public MainFormRuntime()
        {
            InitializeComponent();
            miView.Enabled = false;     
        }

        private void MainFormRuntime_Load(object sender, EventArgs e)
        {                        
            this.Project = new CRuntimeProject(null, this.panel1);
            toolStripStatusLabel4.Text = "Tab Index: -1";
            //
            CRuntimeAlarmsManager alarmsManager = (CRuntimeAlarmsManager)Project.AlarmsManager;
            //alarmsManager.OnRegisterAlarm += new EventHandler(this.alarm_Register);
            //AlarmTable = new AlarmsTableForm(alarmsManager.alarmDataSet, "AlarmsTable", this.tabPage2);
            //AlarmTable.Show();
            //
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {                
                toolStripStatusLabel1.Text = args[1];
                OpenProject(args[1]);               
            }           
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"D:\Projects\VisualStudio2010\AutoToolsCSharp\MicroSCADA\Tests\";
            openFileDialog1.Filter = "SCADA files (*.scd)|*.scd|XML files (*.xml)|*.xml";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenProject(openFileDialog1.FileName);
                miOpen.Enabled = false;  
            }
        }

        private void OpenProject(String FileName)
        {
            COpenFromXML openFromXML;

            Text = FileName;
            openFromXML = new COpenFromXML(Project);
            openFromXML.Open(FileName);
            
            //Seta tamanho da tela                
            ClientSize = new Size(Project.Screens.Width,
                Project.Screens.Height + menuStrip1.Height + statusStrip1.Height);
            CenterToParent();
            //Inicializa e começa comunicação
            //CommManager.SetCommunication(Project.Network.ObjectList);
            //CommManager.Start();

            //m_proxy = new ServiceReference1.Service1Client();
            //m_proxy.Open();
            //Importante
            //m_threadServiceTags = new CThreadServiceTags(m_proxy);
            //m_threadServiceTags.SetSlaveList(Project.Network.ObjectList);
            //m_threadServiceTags.SetTagList(Project.InternalTagList.ObjectList);
            //Importante
            //m_proxy.DoWork();
            //m_threadServiceTags.Start();

            m_threadInternalTags = new CThreadInternalTags();
            m_threadInternalTags.SetTagList(Project.InternalTagList.ObjectList);
            m_threadInternalTags.Start();
            //
            foreach (CRuntimePopupScreen popup in Project.PopupScreens.ObjectList)
            {
                toolStripComboBox1.Items.Add(popup);
            }

            ((CRuntimeAlarmsManager)Project.AlarmsManager).RegisterAlarm += new RegisterAlarmEventHandler(MainFormRuntime_RegisterAlarm);
            ((CRuntimeScreenList)Project.Screens).FieldEnter += new EventHandler(MainFormRuntime_FieldEnter);
            ((CRuntimeScreenList)Project.Screens).ScreenEnter += new EventHandler(MainFormRuntime_ScreenEnter);
            ((CRuntimeScreenList)Project.Screens).ShowScreen(0);
            
            toolStripStatusLabel1.Text = "Screens: " + Project.Screens.ObjectList.Count.ToString();
            toolStripStatusLabel2.Text = "Current: " + (((CRuntimeScreenList)Project.Screens).CurrentIndex + 1).ToString();
            
            miView.Enabled = true;
        }

        private void MainFormRuntime_ScreenEnter(object sender, EventArgs e)
        {
            CRuntimeScreen screen = (CRuntimeScreen)sender;
            toolStripStatusLabel2.Text = string.Format("Current: {0:D3}", screen.Index + 1);
        }

        private void MainFormRuntime_FieldEnter(object sender, EventArgs e)
        {
            CRuntimeCustomField field = (CRuntimeCustomField)sender;
            toolStripStatusLabel4.Text = string.Format("Tab Index: {0:D2}" , field.TabIndex);            
        }

        private void MainFormRuntime_RegisterAlarm(object sender, RegisterAlarmEventArgs e)
        {
            AlarmsForm alarmsDialog = new AlarmsForm(e.AlarmList);

            if (this.InvokeRequired)
            {
                ShowAlarmsCallback d = new ShowAlarmsCallback(ShowAlarms);
                this.Invoke(d, new object[] { alarmsDialog });
            }
            else
                ShowAlarms(alarmsDialog);
        }

        private void ShowAlarms(AlarmsForm alarmsDialog)
        {
            alarmsDialog.Show(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }                
        
           
                                
        private void MainFormRuntime_FormClosed(object sender, FormClosedEventArgs e)
        {            
            //m_proxy.Close();
        }
        
        private void MainFormRuntime_KeyDown(object sender, KeyEventArgs e)
        {           

            if (((CRuntimeScreenList)Project.Screens).CurrentScreen != null)
            {
                m_screen = ((CRuntimeScreenList)Project.Screens).CurrentScreen;
                switch (e.KeyCode)
                {
                    case Keys.PageUp:
                        ((CRuntimeScreenList)Project.Screens).ShowNextScreen();
                        e.Handled = true;
                        break;
                    case Keys.PageDown:
                        ((CRuntimeScreenList)Project.Screens).ShowPrevScreen();
                        e.Handled = true;
                        break;
                    case Keys.Tab:
                        //if (e.Shift)
                        //    m_screen.DecTabIndex();
                        //else
                        //    m_screen.IncTabIndex();
                        //e.Handled = true;
                        break;
                    //case Keys.Return:                    
                    //    m_screen.CurrentField.BeginEdit();
                    //    break;
                    case Keys.Escape:
                    //    m_screen.CurrentField.EndEdit();
                        e.Handled = true;
                        break;
                    default:
                        break;
                }
                //toolStripStatusLabel2.Text = "Screen " + (((CRuntimeScreenList)Project.Screens).CurrentIndex + 1).ToString();
                toolStripStatusLabel3.Text = string.Format("{0} Shift={1}", e.KeyCode, e.Shift);
                
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            
        }

        private void miAlarmTable_Click(object sender, EventArgs e)
        {
            m_alarmTable = new AlarmsTableForm(((CRuntimeAlarmsManager)Project.AlarmsManager).Table);
            m_alarmTable.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedItem != null)
            {
                CRuntimePopupScreen dialog = (CRuntimePopupScreen)toolStripComboBox1.SelectedItem;
                dialog.Show();
            }
        }
               

        //private void UpdateToolStripStatusLabel()
        //{
        //    toolStripStatusLabel1.Text = "Screens: " + Project.Screens.ObjectList.Count.ToString();
        //    toolStripStatusLabel2.Text = "Current: " + (((CRuntimeScreenList)Project.Screens).CurrentIndex + 1).ToString();

        //    toolStripStatusLabel4.Text = "Tab Index: " + (((CRuntimeScreenList)Project.Screens).CurrentScreen).TabIndex.ToString();
        //}
    }
}
