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
    public partial class ImageLibraryForm : Form
    {
        private CImageLibray m_imageLibray;
        private Bitmap m_image;
        public ImageLibraryForm()
        {
            InitializeComponent();
        }
        public Bitmap Image
        {
            get { return m_image; }
        }
        private void ImageLibraryForm_Load(object sender, EventArgs e)
        {
            string path;
            int index;
            path = AppDomain.CurrentDomain.BaseDirectory;
            #if DEBUG            
            index = path.IndexOf("Debug\\");            
            #else            
            index = path.IndexOf("Release\\");
            #endif
            if (index > 0)
                path = path.Remove(index);
            Text = path;
            
            m_imageLibray = new CImageLibray(path);
            listView1.Items.Clear();
            foreach (string s in m_imageLibray.FileList)
            {
                ListViewItem item = listView1.Items.Add(s);                
            }
            label1.Text = string.Format("ImageFiles : {0}", m_imageLibray.FileList.Count);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                m_imageLibray.Open(item.Index);
                int count = m_imageLibray.GetImageCount();
                label2.Text = string.Format("ImageCount : {0}", count);
                //
                dataGridView1.RowCount = count;
                Bitmap bmp;
                for (int i = 0; i < count; i++)
                {
                    bmp = m_imageLibray.GetImage(i);
                    dataGridView1.Rows[i].Height = bmp.Height;
                    dataGridView1[0, i].Value = bmp;
                }             
            }          
        }        
    }
}
