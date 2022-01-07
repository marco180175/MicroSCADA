using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudio.Src.Forms
{
    public partial class TableBitmapForm : Form
    {
        private Crownwood.Magic.Controls.TabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage m_tabPage;
        public TableBitmapForm(Crownwood.Magic.Controls.TabControl tabControl)
        {
            InitializeComponent();
            this.m_tabControl = tabControl;
            this.TopLevel = false;//importante
            this.FormBorderStyle = FormBorderStyle.None;
            this.m_tabPage = new Crownwood.Magic.Controls.TabPage("Picture Browser");
            this.Parent = m_tabPage;
            this.Dock = DockStyle.Fill;   
        }
        new public void Show()
        {
            if (m_tabControl.TabPages.IndexOf(m_tabPage) == -1)
            {                
                CDesignProject project = MainFormDesign.getProject();
                listView1.BeginUpdate();
                imageList1.Images.Clear();
                for (int i = 0; i < project.BitmapList.ObjectList.Count; i++)
                {
                    CDesignBitmapItem bitmapItem = (CDesignBitmapItem)project.BitmapList.ObjectList[i];
                    imageList1.Images.Add(bitmapItem.GetBitmap());
                    ListViewItem lvItem = new ListViewItem(bitmapItem.Name);
                    lvItem.ImageIndex = i;
                    listView1.Items.Add(lvItem);
                }
                listView1.EndUpdate();
                m_tabControl.TabPages.Add(m_tabPage);                
                base.Show();
            }
            m_tabPage.Selected = true;
        }
    }
}
