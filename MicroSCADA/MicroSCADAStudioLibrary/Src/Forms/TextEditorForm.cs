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
    public partial class TextEditorForm : Form
    {
        public TextEditorForm(string Input)
        {
            InitializeComponent();
            textBox1.Text = Input;
        }
        public TextEditorForm(string[] ArrayString)
        {
            InitializeComponent();
            foreach(string str in ArrayString)
                textBox1.Text += str + "\r\n";
        }
        public string Value { get { return textBox1.Text; } }

        private void btOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
