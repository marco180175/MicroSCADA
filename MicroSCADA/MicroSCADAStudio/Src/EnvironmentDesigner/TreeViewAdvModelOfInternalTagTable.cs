using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Aga.Controls.Tree;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudio.Src.EnvironmentDesigner
{
    /*!
     * Node para tags internos
     */
    class CInternalTagNode : Node
    {
        private CDesignCustomTag m_customTag;
        //private Bitmap m_icon;
        private ImageList m_imageList;
        /*!
         * Construtor
         */
        public CInternalTagNode(CDesignCustomTag CustomTag, ImageList ImageList)
            : base()
        {
            this.m_customTag = CustomTag;
            this.m_imageList = ImageList;            
        }
        public String FullName
        {
            get { return m_customTag.FullName; }
        }
        public String Name
        {
            get { return m_customTag.Name; }
            set { m_customTag.Name = value; }
        }
        public String GUID
        {
            get { return m_customTag.GUID.ToString(); }
        }
        public Bitmap Icon
        {
            get
            {
                return new Bitmap(m_imageList.Images[m_customTag.ImageIndex]);
                //this.m_icon.MakeTransparent(Color.Fuchsia);
                //return m_icon;
            }
        }
        public int Address
        {
            get
            {
                if (m_customTag is CDesignExternalTag)
                    return ((CDesignExternalTag)m_customTag).Address;
                else
                    return 0;
            }
        }
        public string Type
        {
            get { return ((CDesignInternalTag)m_customTag).Type.ToString(); }
        }
        public string DataType
        {
            get { return m_customTag.DataType.ToString(); }
        }
        public int ArraySize
        {
            get
            {
                if (m_customTag is CDesignExternalTag)
                    return ((CDesignExternalTag)m_customTag).ArraySize;
                else
                    return 0;
            }
        }
        public string Value
        {
            get { return m_customTag.Value; }
        }
    }
    /*!
     * Node para grupo de tags internos
     */
    class CInternalTagListNode : Node
    {
        private CDesignGroupOfInternalTags m_tagList;
        private ImageList m_imageList;
        private Bitmap m_icon;
        public CInternalTagListNode(CDesignGroupOfInternalTags TagList, ImageList ImageList)
        {
            this.m_tagList = TagList;
            this.m_imageList = ImageList;
        }
        //!
        public String Name
        {
            get { return m_tagList.Name; }
            set { m_tagList.Name = value; }
        }
        //!
        public Bitmap Icon
        {
            get
            {
                this.m_icon = new Bitmap(m_imageList.Images[m_tagList.ImageIndex]);
                this.m_icon.MakeTransparent(Color.Fuchsia);
                return m_icon;
            }
        }
    }
    /*!
     * TreeModel para grupo de tags internos
     */
    class CTreeViewAdvModelOfInternalTagTable: TreeModel
    {
        private CDesignGroupOfInternalTags m_internalTagGroup;
        private Dictionary<object, Node> m_dictionary;
        private ImageList m_imageList;
        /*!
         * 
         */
        public CTreeViewAdvModelOfInternalTagTable(CDesignGroupOfInternalTags DesignTagGroup, ImageList ImageList)
            : base()
        {
            this.m_internalTagGroup = DesignTagGroup;
            this.m_imageList = ImageList;
            this.m_internalTagGroup.AddItem += new AddItemEventHandler(this.internalTag_AddItem);
            this.m_dictionary = new Dictionary<object, Node>();
            this.m_dictionary.Add(m_internalTagGroup, Root);
        }
        /*!
         * Evento addtag
         */
        private void internalTag_AddItem(object sender, AddItemEventArgs e)
        {
            Node node = m_dictionary[sender];
            Node newNode;

            if (e.ObjectAdapter is CDesignInternalTag)
            {
                newNode = new CInternalTagNode((CDesignInternalTag)e.ObjectAdapter, m_imageList);                
            }
            else
            {
                newNode = new CInternalTagListNode((CDesignGroupOfInternalTags)e.ObjectAdapter, m_imageList);
                e.ObjectAdapter.AddItem += new AddItemEventHandler(internalTag_AddItem);
            }
            e.ObjectAdapter.DelItem += new DelItemEventHandler(internalTag_DelItem);
            node.Nodes.Add(newNode);
            m_dictionary.Add(e.ObjectAdapter, newNode);
        }
        /*!
         * Evento deltag
         * TODO:corrigir bug aqui
         */
        private void internalTag_DelItem(object sender, EventArgs e)
        {
            Node node = m_dictionary[sender];

            m_dictionary.Remove(sender);          
            node.Parent.Nodes.Remove(node);
        }       
    }
    
}
