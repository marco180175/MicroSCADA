using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADAStudio.Src;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudio.Src.EnvironmentDesigner
{
    public class CTreeViewAdapter
    {
        private Dictionary<Object, TreeNode> dictionary;       
        private TreeView treeView;
        private TreeNode rootNode;
        public CTreeViewAdapter(TreeView treeView, ITreeAdapter objectAdapter,int ImageIndex)
        {
            this.dictionary = new Dictionary<Object, TreeNode>();            
            this.treeView = treeView;
            this.rootNode = this.treeView.Nodes.Add(objectAdapter.Name);            
            this.rootNode.Tag = objectAdapter;
            this.rootNode.ImageIndex = ImageIndex;
            this.rootNode.SelectedImageIndex = ImageIndex;
            dictionary.Add(objectAdapter, this.rootNode);
            objectAdapter.SetObjectName += new SetNameEventHandler(objectAdapter_SetObjectName);
            objectAdapter.AddItem += new AddItemEventHandler(objectAdapter_AddItem);
            objectAdapter.DelItem += new DelItemEventHandler(objectAdapter_DelItem);
            objectAdapter.SelectedObject +=new SelectedObjectEventHandler(objectAdapter_SelectedObject);                    
            objectAdapter.Initialize();
        }
        private void objectAdapter_SetObjectName(object sender, SetNameEventArgs e)
        {
            TreeNode node = (TreeNode)dictionary[sender];
            node.Text = e.Name;
        }
        private void objectAdapter_SetObjectIcon(object sender, AddItemEventArgs e)
        {
            TreeNode node = (TreeNode)dictionary[sender];
            node.ImageIndex = e.ImageIndex;
            node.SelectedImageIndex = node.ImageIndex;
        }

        private void objectAdapter_AddItem(object sender, AddItemEventArgs e)
        {
            TreeNode node = (TreeNode)dictionary[sender];
            TreeNode newNode = node.Nodes.Add(e.ObjectAdapter.Name);            
            newNode.Tag = e.ObjectAdapter;
            newNode.ImageIndex = e.ImageIndex;
            newNode.SelectedImageIndex = e.ImageIndex;
            dictionary.Add(e.ObjectAdapter, newNode);
            e.ObjectAdapter.SetObjectName += new SetNameEventHandler(objectAdapter_SetObjectName);
            e.ObjectAdapter.AddItem += new AddItemEventHandler(objectAdapter_AddItem);
            e.ObjectAdapter.DelItem += new DelItemEventHandler(objectAdapter_DelItem);
            e.ObjectAdapter.SetObjectIcon +=new AddItemEventHandler(objectAdapter_SetObjectIcon);
            e.ObjectAdapter.ExchangeObject+=new ExchangeEventHandler(objectAdapter_Exchange);
            e.ObjectAdapter.SelectedObject += new SelectedObjectEventHandler(objectAdapter_SelectedObject);                    
            //TODO:e.ObjectAdapter.Initialize(null);
        }
        private void objectAdapter_DelItem(object sender, EventArgs e)
        {
            if (dictionary.Count > 0)
            {
                if (dictionary.ContainsKey(sender))
                {
                    TreeNode node = (TreeNode)dictionary[sender];
                    dictionary.Remove(sender);
                    if (node != null)
                        node.Remove();
                }
            }
        }

        private void objectAdapter_Exchange(object sender, ExchangeEventArgs e)
        {
            TreeNode node = (TreeNode)dictionary[sender];            
            TreeNode temp = node.Nodes[e.Index1];
            node.Nodes.Remove(temp);
            node.Nodes.Insert(e.Index2, temp);           
        }

        private void objectAdapter_SelectedObject(object sender, SelectedObjectEventArgs e)
        {
            if (e.Value)
            {
                TreeNode node = (TreeNode)dictionary[sender];
                treeView.SelectedNode = node;
            }
            else
            {
                treeView.SelectedNode = null;
            }
        }
    }

    public class CTabControlAdapter
    {
        private Dictionary<Object, Crownwood.Magic.Controls.TabPage> dictionary;        
        private Crownwood.Magic.Controls.TabControl tabControl;
        private Crownwood.Magic.Controls.TabPage tabPage;
        public CTabControlAdapter(Crownwood.Magic.Controls.TabControl tabControl, IPageAdapter objectAdapter)
        {
            this.dictionary = new Dictionary<Object, Crownwood.Magic.Controls.TabPage>();            
            this.tabControl = tabControl;
            //objectAdapter.SetObjectName +=new SetNameEventHandle(objectAdapter_SetObjectName);
            objectAdapter.AddItem += new AddItemEventHandler(objectAdapter_AddItem);
            //objectAdapter.DelItem += new DelItemEventHandle(objectAdapter_DelItem);
            //objectAdapter.Initialize();
        }
        /*!
         * Esta função e assinada no objeto pai no construtor
         */
        private void objectAdapter_AddItem(object sender, AddItemEventArgs e)
        {            
            tabPage = new Crownwood.Magic.Controls.TabPage();            
            dictionary.Add(e.ObjectAdapter, tabPage);
            e.ObjectAdapter.SetObjectName += new SetNameEventHandler(objectAdapter_SetObjectName);            
            e.ObjectAdapter.DelItem += new DelItemEventHandler(objectAdapter_DelItem);
            ((IPageAdapter)e.ObjectAdapter).ShowItem += new ShowItemEventHandler(objectAdapter_ShowItem);
            e.ObjectAdapter.Initialize(tabPage);
        }

        private void objectAdapter_SetObjectName(object sender, SetNameEventArgs e)
        {
            tabPage = (Crownwood.Magic.Controls.TabPage)dictionary[sender];
            tabPage.Title = e.Name;
        }                
        private void objectAdapter_DelItem(object sender, EventArgs e)
        {
            if (dictionary.ContainsKey(sender))
            {
                tabPage = (Crownwood.Magic.Controls.TabPage)dictionary[sender];
                dictionary.Remove(sender);
                if(tabControl.TabPages.Contains(tabPage))
                    tabControl.TabPages.Remove(tabPage);
                tabPage.Dispose();
            }
        }
        private void objectAdapter_ShowItem(object sender, ShowItemEventArgs e)
        {
            tabPage = (Crownwood.Magic.Controls.TabPage)dictionary[sender];
            if (tabControl.TabPages.IndexOf(tabPage) == -1)
                tabControl.TabPages.Add(tabPage);
            tabPage.Selected = true;                     
        }
    }
}
