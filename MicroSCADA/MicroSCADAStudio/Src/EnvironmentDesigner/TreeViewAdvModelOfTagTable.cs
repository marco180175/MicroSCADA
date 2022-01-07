using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Aga.Controls.Tree;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudio.Src.EnvironmentDesigner
{
    class CTagNode : Node
    {
        private CDesignCustomTag customTag;
        private Bitmap icon;

        public CTagNode(String text)
            : base(text)
        { }
        public CTagNode(CDesignCustomTag CustomTag)
            : base()
        {
            this.customTag = CustomTag;
            //this.customTag.AddItemObject += new AddItemEventHandle(deviceTag_AddItemObject);
            this.Tag = this.customTag;
            this.icon = new Bitmap(MicroSCADAStudio.Properties.Resources.devicetag);
            this.icon.MakeTransparent(Color.Fuchsia);
        }
        public String FullName
        {
            get { return customTag.FullName; }            
        }
        public String Name
        {
            get { return customTag.Name; }
            set { customTag.Name = value; }
        }
        public String GUID
        {
            get { return customTag.GUID.ToString(); }            
        }
        public Bitmap Icon
        {
            get { return this.icon; }
        }
        public int Address
        {
            get
            {
                if (customTag is CDesignExternalTag)
                    return ((CDesignExternalTag)customTag).Address;
                else
                    return 0;
            }
        }
        public CCustomDataType DataType
        {
            get { return customTag.DataType; }
        }
        public int ArraySize
        {
            get
            {
                if (customTag is CDesignExternalTag)
                    return ((CDesignExternalTag)customTag).ArraySize;
                else
                    return 0;
            }
        }
        private void deviceTag_AddItemObject(Object sender, AddItemEventArgs e)
        {/*
            CDeviceTagItemNode node;
            CDeviceTagItem deviceTagItem;

            deviceTagItem = (CDeviceTagItem)e.ObjectItem;
            deviceTagItem.DisposeObject += new EventHandler(deviceTagItem_DisposeObject);
            node = new CDeviceTagItemNode(deviceTagItem);
            node.Tag = deviceTagItem;
            Nodes.Add(node);
            CDeviceMasterTreeModel.hashTable.Add(node.Tag, node);*/
        }

        private void deviceTagItem_DisposeObject(Object sender, EventArgs e)
        {/*
            CDeviceTagItemNode node;
            node = (CDeviceTagItemNode)CDeviceMasterTreeModel.hashTable[sender];
            CDeviceMasterTreeModel.hashTable.Remove(sender);
            Nodes.Remove(node);*/
        }
    }

    class TreeViewAdvModelOfTagTable : TreeModel
    {
        private CDesignNetwork designTagGroup;
        private Dictionary<CDesignCustomTag, Node> tagTable;
        public TreeViewAdvModelOfTagTable(CDesignNetwork DesignTagGroup)
            : base()
        {
            this.designTagGroup = DesignTagGroup;
            //this.deviceMasterCollection.AddItemObject+=new AddItemEventHandle(deviceMasterCollection_AddItemObject);
            this.tagTable = new Dictionary<CDesignCustomTag, Node>();            
        }
        /*!
         * Adiciona todos tags na lista
         */
        public void AddCustomTag()
        {            
            for (int i = 0; i < designTagGroup.ObjectList.Count; i++)
            {
                AddCustomTag((CDesignGroupOfExternalTags)designTagGroup.ObjectList[i]);
            }
        }
        private void AddCustomTag(CDesignGroupOfExternalTags DesignTagGroup)
        {
            for (int i = 0; i < DesignTagGroup.ObjectList.Count; i++)
            {
                if (DesignTagGroup.ObjectList[i] is CDesignCustomTag)
                {
                    CDesignCustomTag customTag = (CDesignCustomTag)DesignTagGroup.ObjectList[i];
                    CTagNode node = new CTagNode(customTag);
                    this.Nodes.Add(node);
                }
                else
                    AddCustomTag((CDesignGroupOfExternalTags)DesignTagGroup.ObjectList[i]);
            }
        }
    }
}
