using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADAStudio.Src;
using MicroSCADAStudioLibrary.Src.Visuals;
using MicroSCADAStudio.Src.EnvironmentDesigner;
using WeifenLuo.WinFormsUI.Docking;
using MicroSCADAStudioLibrary.Src;
using MicroSCADAStudioLibrary.Src.Forms;

namespace MicroSCADAStudio.Src.DockingForms
{
    /*!
     * Deriva form da classe DockContent (terceiros) para que a instancia \n
     * seja docada no form principal.
     */
    public partial class ProjectManagerForm : DockContent
    {        
        /*!
         * Construtor
         * @param ImageList: Referencia para imagelist do form principal
         */
        public ProjectManagerForm(ImageList ImageList)
        {
            InitializeComponent();
            this.treeView1.ImageList = ImageList;            
        }
        /*!
         * Habilita item do menu em função do objeto selecionado
         */
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag != null)
                {
                    toolStripMenuItem2.Enabled = (treeView1.SelectedNode.Tag is CDesignNetwork);
                    toolStripMenuItem3.Enabled = (treeView1.SelectedNode.Tag is CDesignGroupOfExternalTags);
                    toolStripMenuItem4.Enabled = (treeView1.SelectedNode.Tag is CDesignInternalTagList);
                    toolStripMenuItem5.Enabled = (treeView1.SelectedNode.Tag is CDesignTrendChart);
                    addTextZoneToolStripMenuItem.Enabled = (treeView1.SelectedNode.Tag is CDesignDinamicText);
                    addGroupToolStripMenuItem.Enabled = (treeView1.SelectedNode.Tag is CDesignSlave);
                    addBarToolStripMenuItem.Enabled = (treeView1.SelectedNode.Tag is CDesignBargraph);
                    addPictureZoneToolStripMenuItem.Enabled = (treeView1.SelectedNode.Tag is CDesignAnimation);
                    miAddGroupOfInternalTags.Visible = (treeView1.SelectedNode.Tag is CDesignInternalTagList);
                    miAddGroupOfInternalTags.Visible = (treeView1.SelectedNode.Tag is CDesignGroupOfInternalTags);
                    toolStripMenuItem4.Enabled = (treeView1.SelectedNode.Tag is CDesignGroupOfInternalTags);
                    miAddAction.Enabled = (treeView1.SelectedNode.Tag is CDesignActionList);
                    miAddActionLine.Enabled = (treeView1.SelectedNode.Tag is CDesignAction);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CDesignNetwork network = (CDesignNetwork)treeView1.SelectedNode.Tag;
            network.AddSlaveEx();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CDesignGroupOfExternalTags slave = (CDesignGroupOfExternalTags)treeView1.SelectedNode.Tag;
            slave.AddTagEx();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {            
            CDesignTrendChart trendChart = (CDesignTrendChart)treeView1.SelectedNode.Tag;
            trendChart.AddPen();
        }

        private void tagDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CDesignGroupOfInternalTags tagList = (CDesignGroupOfInternalTags)treeView1.SelectedNode.Tag;
            tagList.AddDemoTagEx();
        }

        private void tagRamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CDesignGroupOfInternalTags tagList = (CDesignGroupOfInternalTags)treeView1.SelectedNode.Tag;
            tagList.AddSRAMTagEx();
        }

        private void miAddTagProperty_Click(object sender, EventArgs e)
        {
            ApplicationBrowserForm appBrowser = new ApplicationBrowserForm(true);
            if (appBrowser.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CDesignGroupOfInternalTags tagList = (CDesignGroupOfInternalTags)treeView1.SelectedNode.Tag;
                tagList.AddPropertyTagEx((CDesignSystem)appBrowser.SelectedObject, appBrowser.PropertyName);
            }
        }  

        private void addTextZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            CDesignDinamicText dinamicText = (CDesignDinamicText)treeView1.SelectedNode.Tag;
            dinamicText.AddZone();
        }

        private void addPictureZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CDesignAnimation animation = (CDesignAnimation)treeView1.SelectedNode.Tag;
            animation.AddZone();
        }

        private void miAddGroupOfInternalTags_Click(object sender, EventArgs e)
        {
            CDesignGroupOfInternalTags group = (CDesignGroupOfInternalTags)treeView1.SelectedNode.Tag;
            group.AddGroupEx(true);            
        }

        private void addGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CDesignGroupOfExternalTags slave = (CDesignGroupOfExternalTags)treeView1.SelectedNode.Tag;
            slave.AddGroupEx();
        }

        private void addBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CDesignBargraph barGraph = (CDesignBargraph)treeView1.SelectedNode.Tag;
            barGraph.NewBar();
        }
        /*!
         * 
         */
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {                
            //OnSelectObject(new SelectedObjectEventArgs(e.Node.Tag, true));                  
            if (SelectObject != null)
                SelectObject(e.Node.Tag, new SelectedObjectEventArgs(true));
        }

        public event SelectedObjectEventHandler SelectObject;
        private void OnSelectObject(SelectedObjectEventArgs e)
        {
            if (SelectObject != null)
                SelectObject(this, e);
        }       

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                CDesignObject designObject = (CDesignObject)treeView1.SelectedNode.Tag;
                if (designObject.CanDeleteByUser)
                {
                    if (MessageBox.Show("Delete selected object ?",
                                        Application.ProductName,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        designObject.Dispose();
                    }
                }
            }
        }

        public event SelectedObjectEventHandler ObjectDoubleClick;
        private void OnObjectDoubleClick(SelectedObjectEventArgs e)
        {
            if (ObjectDoubleClick != null)
                ObjectDoubleClick(this, e);
        }               

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag != null)
                {
                    SelectedObjectEventArgs ev;
                    ev = new SelectedObjectEventArgs( false);
                    if (ObjectDoubleClick != null)
                        ObjectDoubleClick(treeView1.SelectedNode.Tag, ev);
                    //OnObjectDoubleClick(ev);                     
                }
            }
        }

        private void miAddAction_Click(object sender, EventArgs e)
        {
            CDesignActionList actionList = (CDesignActionList)treeView1.SelectedNode.Tag;
            actionList.NewActionEx();
        }

        private void miAddActionLine_Click(object sender, EventArgs e)
        {
            CDesignAction action = (CDesignAction)treeView1.SelectedNode.Tag;
            ActionEditorForm actionEditor = new ActionEditorForm(action);
            actionEditor.ShowDialog(this);
        }

        private void miNewTagTimer_Click(object sender, EventArgs e)
        {
            CDesignGroupOfInternalTags tagList = (CDesignGroupOfInternalTags)treeView1.SelectedNode.Tag;
            tagList.NewTimerTagEx();
        }

             
    }
}
