using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudio.Src.Forms
{
    public partial class ResourcesForm : Form
    {
        public ResourcesForm()
        {
            InitializeComponent();
        }

        private void ResourcesForm_Load(object sender, EventArgs e)
        {
            //label1.Text = CDesignCustomTag.getCount().ToString();
            //label2.Text = CDesignValuesTag.getCount().ToString();
            //label3.Text = CDesignMemoryTag.getCount().ToString();
            //label4.Text = CDesignExternalTag.getCount().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainFormDesign.getProject().Clear();
        }
    }
}
