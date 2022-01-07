using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aga.Controls.Tree;
using MicroSCADAStudioLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Forms
{
    public partial class CollectionEditorForm : Form
    {
        private IDesignCollection scrObj;
        private TreeModel m_model;
        public CollectionEditorForm(CDesignScreenObject screenObject)        
        {
            InitializeComponent();
            this.m_model = new TreeModel();
            this.treeViewAdv1.Model = this.m_model;
            //if (screenObject is CDesignDinamicText || screenObject is CDesignAnimation)
            if (screenObject is IDesignCollection)
            {
                this.scrObj = (IDesignCollection)screenObject;
                for (int i = 0; i < scrObj.ObjectList.Count; i++)
                {                 
                    IDesignCollectionItem item = (IDesignCollectionItem)scrObj.ObjectList[i];
                    CCollectionItemNode node = new CCollectionItemNode((CDesignSystem)item);
                    this.m_model.Nodes.Add(node);
                }                
            }            
            else
            {
                this.scrObj = null;
                throw new NotSupportedException(screenObject.GetType().Name);
            }
            //
            treeViewAdv1_SelectionChanged(this, EventArgs.Empty);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            IDesignCollectionItem zone = scrObj.NewItem();
            CCollectionItemNode item = new CCollectionItemNode((CDesignSystem)zone);
            treeViewAdv1.Root.IsSelected = false;            
            m_model.Nodes.Add(item);
            treeViewAdv1.Root.Children[item.Index].IsSelected = true;
        }     

        private void treeViewAdv1_SelectionChanged(object sender, EventArgs e)
        {
            if (treeViewAdv1.SelectedNode != null)
            {
                CCollectionItemNode node = (CCollectionItemNode)treeViewAdv1.SelectedNode.Tag;
                propertyGrid1.SelectedObject = node.Tag;
                UpdateButtons();
            }
            else
            {
                btUp.Enabled = false;
                btDown.Enabled = false;
                btDelete.Enabled = false;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            int index1 = treeViewAdv1.SelectedNode.Index;
            int index2 = index1 - 1;
            scrObj.Exchange(index1, index2);
            //
            Node node = m_model.Nodes[index1];
            m_model.Nodes.Remove(node);
            m_model.Nodes.Insert(index2, node);
            //
            treeViewAdv1.Root.Children[index2].IsSelected = true;
            //
            UpdateButtons();
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            int index1 = treeViewAdv1.SelectedNode.Index;
            int index2 = index1 + 1;
            scrObj.Exchange(index1, index2);
            //
            Node node = m_model.Nodes[index1];
            m_model.Nodes.Remove(node);
            m_model.Nodes.Insert(index2, node);
            //
            treeViewAdv1.Root.Children[index2].IsSelected = true;
            //
            UpdateButtons();
        }
        
        private void btDelete_Click(object sender, EventArgs e)
        {
            CCollectionItemNode node = (CCollectionItemNode)treeViewAdv1.SelectedNode.Tag;
            ((CDesignSystem)node.Tag).Dispose();
            m_model.Nodes.Remove(node);
        }
        
        private void UpdateButtons()
        {
            switch (treeViewAdv1.ItemCount)
            {
                case 0:
                    {
                        btUp.Enabled = false;
                        btDown.Enabled = false;
                        btDelete.Enabled = false;
                    }; break;
                case 1:
                    {
                        btUp.Enabled = false;
                        btDown.Enabled = false;
                        btDelete.Enabled = true;
                    }; break;
                case 2:
                    {
                        if (treeViewAdv1.SelectedNode.Index == 0)
                        {
                            btUp.Enabled = false;
                            btDown.Enabled = true;
                        }
                        else
                        {
                            btUp.Enabled = true;
                            btDown.Enabled = false;
                        }
                        btDelete.Enabled = true;
                    }; break;
                default:
                    {
                        if (treeViewAdv1.SelectedNode.Index == 0)
                        {
                            btUp.Enabled = false;
                            btDown.Enabled = true;
                        }
                        else if (treeViewAdv1.SelectedNode.Index == treeViewAdv1.ItemCount - 1)
                        {
                            btUp.Enabled = true;
                            btDown.Enabled = false;
                        }
                        else
                        {
                            btUp.Enabled = true;
                            btDown.Enabled = true;
                        }
                        btDelete.Enabled = true;
                    }; break;
            }
        }           
    }

    class CCollectionItemNode : Node
    {
        private CDesignSystem item;
        //private Bitmap icon;

        public CCollectionItemNode(String text)
            : base(text)
        { }
        public CCollectionItemNode(CDesignSystem item)
            : base()
        {
            this.item = item;
            this.Tag = item;
        }
        public String Name
        {
            get { return item.Name; }
            set { item.Name = value; }
        }
    }
}
