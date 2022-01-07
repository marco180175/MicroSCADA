using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADAStudioLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudio.Src.Forms
{
    public partial class TableScreenForm : Form
    {
        private Crownwood.Magic.Controls.TabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage m_tabPage;
        public TableScreenForm(Crownwood.Magic.Controls.TabControl tabControl)
        {
            InitializeComponent();
            //
            this.m_tabControl = tabControl;            
            this.TopLevel = false;//importante
            this.FormBorderStyle = FormBorderStyle.None;
            this.m_tabPage = new Crownwood.Magic.Controls.TabPage("Screen Browser");
            this.Parent = m_tabPage;
            this.Dock = DockStyle.Fill;
        }
        new public void Show()
        {
            if (m_tabControl.TabPages.IndexOf(m_tabPage) == -1)
            {
                CDesignProject project = MainFormDesign.getProject();
                listView1.Clear();
                listView1.BeginUpdate();
                imageList1.Images.Clear();
                for (int i = 0; i < project.Screens.ObjectList.Count; i++)
                {
                    CDesignScreen screen = (CDesignScreen)project.Screens.ObjectList[i];
                    imageList1.Images.Add(screen.GetBitmap());
                    ListViewItem lvItem = new ListViewItem(screen.Name);
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
