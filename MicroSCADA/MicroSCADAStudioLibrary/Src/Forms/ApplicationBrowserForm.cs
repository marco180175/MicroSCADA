using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Forms
{
    public partial class ApplicationBrowserForm : Form
    {
        private Object selected,m_result;        
        private string m_propertyName;
        private CDesignProject project;
        private bool m_mode;
        public ApplicationBrowserForm(bool Mode)
        {
            InitializeComponent();
            this.project = CDesignProject.getInstance();
            this.m_mode = true;
            if (this.m_mode)
                Text = "Select properties";
            else
                Text = "Select functions";
        }

        private void FormSelectTag_Load(object sender, EventArgs e)
        {
            //project = MainFormDesign.getProject();

            TreeNode node;

            CDesignScreenList screens = (CDesignScreenList)project.Screens;
            node = treeView1.Nodes.Add(screens.Name);
            node.Tag = screens;
            GetNodes(screens, node);

            CDesignPopupScreenList popupScreens = (CDesignPopupScreenList)project.PopupScreens;
            node = treeView1.Nodes.Add(popupScreens.Name);
            node.Tag = popupScreens;
            GetNodes(popupScreens, node);

            CDesignInternalTagList internalTagList = (CDesignInternalTagList)project.InternalTagList;
            node = treeView1.Nodes.Add(internalTagList.Name);
            node.Tag = internalTagList;
            GetNodes(internalTagList, node);

            CDesignNetwork network = (CDesignNetwork)project.Network;
            node = treeView1.Nodes.Add(network.Name);
            node.Tag = network;
            GetNodes(network, node);

            selected = null;
            m_result = null;
            listView1.Columns[0].Width = listView1.ClientSize.Width;
        }

        private void GetNodes(CDesignSystem OwnerItem, TreeNode OwnerNode)
        {
            foreach (CDesignSystem item in OwnerItem.ObjectList)
            {
                TreeNode node = OwnerNode.Nodes.Add(item.Name);
                node.Tag = item;
                GetNodes(item, node);
            }
        }
        /*!
         * Mostra propriedades e funções do objeto
         * @param Item Objeto que sera mostrado
         * 
         */
        private void GetProperties(CDesignSystem Item)
        {
            if (Item != null)
            {
                listView1.Items.Clear();
                //
                if (m_mode)
                {
                    PropertyInfo[] properties = Item.GetProperties();
                    if (properties != null)
                    {
                        foreach (PropertyInfo property in properties)
                        {
                            listView1.Items.Add(property.Name);
                        }
                    }
                }
                //else
                //{
                //    //
                //    KeyValuePair<CCustomActionCode, string>[] functions = Item.GetFunctions();
                //    if (functions != null)
                //    {
                //        foreach (KeyValuePair<CCustomActionCode, string> function in functions)
                //        {
                //            ListViewItem lvItem = listView1.Items.Add(function.Value);
                //            lvItem.Tag = function.Key;
                //        }
                //    }
                //}
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            //if (m_mode)
                ReturnProperty();
            //else
            //    ReturnFunction();
            this.DialogResult = DialogResult.OK;
        }

        //private void ReturnProperty()
        //{    
        //     m_result = selected;
        //     if (listView1.SelectedItems.Count == 0)
        //     {
        //         m_propertyName = string.Empty;
        //     }
        //     else
        //     {
        //         m_propertyName = listView1.SelectedItems[0].Text;
        //     }
        //}

        private void ReturnProperty()
        {
            CDesignPropertyTagList propertyTagList;
            propertyTagList = (CDesignPropertyTagList)project.PropertyTagList;

            if (listView1.SelectedItems.Count >= 1)
            {
                m_propertyName = listView1.SelectedItems[0].Text;
                if ((selected is CDesignCustomTag))
                {
                    if (m_propertyName == "Value")
                        m_result = selected;
                    else
                        m_result = propertyTagList.AddTagEx((CDesignSystem)selected, m_propertyName);
                }
                else
                {
                    m_result = propertyTagList.AddTagEx((CDesignSystem)selected, m_propertyName);
                }
            }
            else
            {
                m_result = selected;
            }
        }
        //private void ReturnFunction()
        //{            
        //    if (listView1.SelectedItems.Count == 1)
        //    {
        //        opCode = (CCustomActionCode)listView1.SelectedItems[0].Tag;                    
        //        result = selected;                
        //    }
        //    else
        //    {
        //        result = null;
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            m_result = null;
            this.DialogResult = DialogResult.Cancel;
        }

        public Object SelectedObject
        {
            get { return this.m_result; }
        }

        public string PropertyName
        {
            get { return this.m_propertyName; }
        }

        //public CCustomActionCode OpCode
        //{
        //    get { return this.opCode; }
        //}

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selected = e.Node.Tag;
            GetProperties((CDesignSystem)selected);
        }

        private void listView1_ClientSizeChanged(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = listView1.ClientSize.Width;
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btOk.Enabled = e.IsSelected;
        }
    }



}
