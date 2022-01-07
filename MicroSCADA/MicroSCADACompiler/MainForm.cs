using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADACompiler.Src;
using MicroSCADACustomLibrary.Src.IOFiles;
using MicroSCADACompilerLibrary.Src;

namespace MicroSCADACompiler
{
    public partial class MainForm : Form
    {
        private CCompilerProject project;
        public MainForm()
        {
            InitializeComponent();
            project = new CCompilerProject();
            COpenFromBIN.MessageEvent += new MessageEventHandler(CompilerLinker_MessageEvent);            
            CCompiler.MessageEvent += new MessageEventHandler(CompilerLinker_MessageEvent);            
        }

        void CompilerLinker_MessageEvent(object sender, MessageEventArgs e)
        {
            lbOutputMessages.Items.Add(e.Message);
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                COpenFromBIN.Open(openFileDialog1.FileName, project);
            }
        }

        private void miCompiler_Click(object sender, EventArgs e)
        {
            lbOutputMessages.Items.Clear();
            CCompiler.Compiler(openFileDialog1.FileName, project);
            lbOutputMessages.Items.Add("compiled ok!!!");
        }
    }
}
