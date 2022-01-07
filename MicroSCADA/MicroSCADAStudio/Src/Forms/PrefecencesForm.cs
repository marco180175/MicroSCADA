using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADAStudio.Src.EnvironmentDesigner;

namespace MicroSCADAStudio.Src.Forms
{
    public partial class PrefecencesForm : Form
    {
        public PrefecencesForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CPreferences.WorkDirectory = folderBrowserDialog1.SelectedPath;
                CPreferences.Save();
                tbWorkDirectory.Text = CPreferences.WorkDirectory;
            }
        }

        private void PrefecencesForm_Load(object sender, EventArgs e)
        {
            //
            tbWorkDirectory.Text = CPreferences.WorkDirectory;
            //
            lbxRecentFiles.Items.AddRange(CPreferences.RecentFiles.LastFiles);
        }

        private void btClose_Click(object sender, EventArgs e)
        {            
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
