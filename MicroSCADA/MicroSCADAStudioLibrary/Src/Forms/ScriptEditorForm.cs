using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MicroSCADAStudioLibrary.Src.Forms
{
    public partial class ScriptEditorForm : Form
    {
        
        public ScriptEditorForm()
        {
            InitializeComponent();
                     
        }

        public String Script { get; set; }
        //{
        //    get { return this.textEditorControl1.Text; }
        //    set { this.textEditorControl1.Text = value; }
        //}

        private void btOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
    
}
