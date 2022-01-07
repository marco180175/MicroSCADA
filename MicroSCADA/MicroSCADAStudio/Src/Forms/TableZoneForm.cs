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
using MicroSCADAStudioLibrary.Src.Visuals;

namespace MicroSCADAStudio.Src.Forms
{
    public partial class TableZoneForm : Form
    {
        private TreeModel m_model;
        //private CDesignSystem designObject;
        private Crownwood.Magic.Controls.TabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage m_tabPage;
        private IDesignZones m_zones;
        public TableZoneForm(Crownwood.Magic.Controls.TabControl tabControl)
        {
            InitializeComponent();
            //
            this.m_tabControl = tabControl;           
            this.TopLevel = false;//importante
            this.FormBorderStyle = FormBorderStyle.None;
            this.m_tabPage = new Crownwood.Magic.Controls.TabPage("Zone Browser");
            this.Parent = m_tabPage;
            this.Dock = DockStyle.Fill;
            //
            this.m_model = new TreeModel();
            this.treeViewAdv1.Model = m_model;
        }

        public void Show(IDesignZones zones)
        {
            //designObject = zones.GetThis();
            //
            m_zones = zones;
            treeViewAdv1.BeginUpdate();
            m_model.Nodes.Clear();            
            for (int i = 0; i < zones.ZoneCount; i++)
            {               
                CPictureNode node = new CPictureNode(zones.GetZone(i));
                treeViewAdv1.RowHeight = node.Picture.Height + 4;
                m_model.Nodes.Add(node);
            }
            treeViewAdv1.EndUpdate();    
            //
            if (m_tabControl.TabPages.IndexOf(m_tabPage) == -1)
            {
                m_tabControl.TabPages.Add(m_tabPage);
                base.Show();
            }
            m_tabPage.Selected = true;
        }

        private void tbUp_Click(object sender, EventArgs e)
        {                    
            int index1 = treeViewAdv1.SelectedNode.Index;
            int index2 = index1 - 1;
            m_zones.Exchange(index1, index2);
            //
            Node node = m_model.Nodes[index1];
            m_model.Nodes.Remove(node);
            m_model.Nodes.Insert(index2, node);
            //
            treeViewAdv1.Root.Children[index2].IsSelected = true;            
        }

        private void tbDown_Click(object sender, EventArgs e)
        {
            int index1 = treeViewAdv1.SelectedNode.Index;
            int index2 = index1 + 1;
            m_zones.Exchange(index1, index2);
            //
            Node node = m_model.Nodes[index1];
            m_model.Nodes.Remove(node);
            m_model.Nodes.Insert(index2, node);
            //
            treeViewAdv1.Root.Children[index2].IsSelected = true; 
        }
    }

    class CPictureNode : Node
    {        
        private Bitmap icon;
        public CPictureNode(String text)
            : base(text)
        { }
        public CPictureNode(Bitmap bitmap)
            : base()
        {
            icon = bitmap;
        }
        public Bitmap Picture
        {
            get { return this.icon; }
        }
    }
}
