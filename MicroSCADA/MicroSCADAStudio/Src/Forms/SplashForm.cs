using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MicroSCADAStudio.Src.Forms
{
    public partial class SplashForm : Form
    {
        private bool fClose = false;
        public SplashForm()
        {
            InitializeComponent();
            this.Width = this.BackgroundImage.Width;
            this.Height = this.BackgroundImage.Height;            
        }
        private void SplashForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 20;
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fClose == false)
            {
                if (progressBar1.Value < 100)
                {
                    progressBar1.Value++;
                    label1.Text = String.Format("Loading {0}%", progressBar1.Value);
                }
                else
                {
                    fClose = true;
                    timer1.Interval = 2000;
                }
            }
            else
                Close();
        }

        
    }
}
