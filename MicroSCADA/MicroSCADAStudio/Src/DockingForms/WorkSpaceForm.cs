using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MicroSCADAStudio.Src.DockingForms
{
    //public partial class WorkSpaceForm : Form
    public partial class WorkSpaceForm : DockContent
    {
        public WorkSpaceForm()
        {
            InitializeComponent();
        }

        private void tabControl1_ClosePressed(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }
    }
}
