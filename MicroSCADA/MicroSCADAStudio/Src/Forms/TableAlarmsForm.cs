using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aga.Controls.Tree;
using MicroSCADAStudio.Src.EnvironmentDesigner;
using MicroSCADAStudioLibrary.Src;
namespace MicroSCADAStudio.Src.Forms
{
    public partial class TableAlarmsForm : Form
    {
        private Crownwood.Magic.Controls.TabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage m_tabPage;
        private TreeModel m_model;
        private CDesignAlarmsManager m_alarmsManager;
        public TableAlarmsForm(Crownwood.Magic.Controls.TabControl tabControl, CDesignAlarmsManager AlarmsManager, ImageList ImageList)
        {
            InitializeComponent();
            this.m_alarmsManager = AlarmsManager;
            //
            this.m_tabControl = tabControl;            
            this.TopLevel = false;//importante
            this.FormBorderStyle = FormBorderStyle.None;
            this.m_tabPage = new Crownwood.Magic.Controls.TabPage("Alarms Browser");
            this.Parent = m_tabPage;
            this.Dock = DockStyle.Fill;
            //
            this.m_model = new CTreeViewAdvModelOfAlarmTable(this.m_alarmsManager, ImageList);
            this.treeViewAdv1.Model = this.m_model; 
        }

        new public void Show()
        {
            if (m_tabControl.TabPages.IndexOf(m_tabPage) == -1)
            {
                m_tabControl.TabPages.Add(m_tabPage);
                base.Show();
            }
            m_tabPage.Selected = true;
            treeViewAdv1.BeginUpdate();
            ((CTreeViewAdvModelOfAlarmTable)m_model).Clear();
            ((CTreeViewAdvModelOfAlarmTable)m_model).Populate();
            treeViewAdv1.EndUpdate();
        }        
    }
    
}
