using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MicroSCADARuntime.Src.Forms
{
    public partial class AlarmsTableForm : Form
    {
        public AlarmsTableForm(DataTable DataSource)
        {
            InitializeComponent();
            //
            //this.TopLevel = false;//importante
            //this.FormBorderStyle = FormBorderStyle.None;
            ////this.tabPage = tabPage;
            //this.Parent = tabPage;
            //this.Dock = DockStyle.Fill;  
            //
            this.dataGridView1.DataSource = DataSource;
            //this.dataGridView1.DataMember = DataMember;
        }
    }
}
