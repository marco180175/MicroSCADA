using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADARuntimeLibrary.Src;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntime.Src.Forms
{
    public partial class CommLogForm : Form
    {
        //private CCommunicationManager commManager;
        //Create a Bitmpap Object.
        private Bitmap statusAnimated;
        private PictureBox pictureBox;
        //public CommLogForm(CCommunicationManager CommManager)
        public CommLogForm()
        {
            InitializeComponent();
            //this.commManager = CommManager;
            this.statusAnimated = new Bitmap(MicroSCADARuntimeLibrary.Properties.Resources.status_anim);
            
            pictureBox = new PictureBox();
            pictureBox.Paint +=new PaintEventHandler(pictureBox_Paint);
            pictureBox.Parent = this;
            pictureBox.Image = MicroSCADARuntimeLibrary.Properties.Resources.status_anim;
            pictureBox.Show();
        }
        
        private void CommLogForm_Load(object sender, EventArgs e)
        {
            /*
            //
            toolStripComboBox1.SelectedIndex = 0;
            //
            for (int i = 0; i < commManager.SlaveList.Count; i++)
            {
                CRuntimeSlave slave = (CRuntimeSlave)commManager.SlaveList[i];
                slave.listItem = listView1.Items.Add(slave.Name);
                
                //1
                switch (slave.Protocol)
                {
                    case EModbusType.ModbusMasterRTU:                        
                        slave.listItem.SubItems.Add(slave.SerialPortConfig.ToString());
                        break;
                    case EModbusType.ModbusMasterTCP:
                        slave.listItem.SubItems.Add(slave.TCPClientConfig.ToString());
                        break;
                }
                //2
                slave.listItem.SubItems.Add("connected");
                //3
                slave.listSubItem = slave.listItem.SubItems.Add("00:00:00");
                //
                //Begin the animation only once.
                ImageAnimator.Animate(statusAnimated, new EventHandler(this.timer1_Tick));
                
                
            }
             */
        }
        
        public void Add(String Value)
        {
            if (toolStripComboBox1.ComboBox != null)
            {
                if (toolStripComboBox1.SelectedIndex == 1)
                    richTextBox1.Text += Value + '\n';
            }
        }

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {            
            if (e.ColumnIndex == 1)
            {                
                e.Graphics.DrawImage(pictureBox.Image, e.Bounds.Left, e.Bounds.Top);
            }
            else
                e.DrawDefault = true;
            
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ImageAnimator.UpdateFrames();
            listView1.Invalidate();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            listView1.Invalidate();
        }
    }
}
