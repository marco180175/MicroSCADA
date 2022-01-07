using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aga.Controls.Tree;
using MicroSCADAStudioLibrary.Src;
using MicroSCADAStudio.Src.EnvironmentDesigner;

namespace MicroSCADAStudio.Src.Forms
{
    public partial class TableInternalTagForm : Form
    {
        private Crownwood.Magic.Controls.TabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage m_tabPage;
        private TreeModel m_model;
        private CDesignInternalTagList m_designTagGroup;
        public TableInternalTagForm(Crownwood.Magic.Controls.TabControl tabControl, CDesignInternalTagList designTagGroup,ImageList ImageList)
        {
            InitializeComponent();
            this.m_tabControl = tabControl;
            this.m_designTagGroup = designTagGroup;
            this.TopLevel = false;//importante
            this.FormBorderStyle = FormBorderStyle.None;
            this.m_tabPage = new Crownwood.Magic.Controls.TabPage("Internal Tag Browser");
            this.Parent = m_tabPage;
            this.Dock = DockStyle.Fill;
            //
            this.m_model = new CTreeViewAdvModelOfInternalTagTable(this.m_designTagGroup,ImageList);
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
        }
        
    }
}
