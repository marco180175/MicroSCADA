using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
//using MicroSCADAStudioLibrary.Src.EnvironmentDesigner;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudioLibrary.Src
{
    /*!
     * Implementa Grupo de tags na slave
     */
    public class CDesignGroupOfExternalTags : CDesignSystem, ICustomGroupTags
    {
        protected CCustomSlave customSlave;        
        public CDesignGroupOfExternalTags(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customSlave = new CCustomSlave();
            this.imageIndex = 32;
            this.SetGUID(Guid.NewGuid());
        }
        /*!
         * Verifica tipo do owner pois dispose é chamado tambem por CDesignSlave
         * e o owner desta classe é CDesignNetwork.
         */
        public override void Dispose()
        {
            while (ObjectList.Count > 0)
                ((CDesignSystem)ObjectList[ObjectList.Count - 1]).Dispose();
            //if (Owner is CDesignGroupOfExternalTags)
            //    ((CDesignGroupOfExternalTags)Owner).GroupList.Remove(this);
            base.Dispose();
        }
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignExternalTag> TagList 
        { 
            get { return this.GetTagList(); } 
        }
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignGroupOfExternalTags> GroupList 
        { 
            get { return this.GetGroupList(); } 
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignGroupOfExternalTags> subSet = Objects.OfType<CDesignGroupOfExternalTags>();
            return subSet.ToArray();
        }     
        /*!
         * Adiciona tag pela interface de usuario
         * @param Value Nome do tag
         * @return Referencia para novo tag
         */
        public ICustomTag AddTag()
        {
            CDesignExternalTag tag;

            tag = new CDesignExternalTag(this, project);
            ObjectList.Add(tag);            
            tag.SetGUID(Guid.NewGuid());
            OnAddItem(new AddItemEventArgs(tag, tag.ImageIndex));
            tag.SetSlave(customSlave.address);
            return tag;
        }
        /*!
         * Adiciona tag em modo de edição
         */
        public CDesignExternalTag AddTagEx()
        {
            CDesignExternalTag tag;

            tag = (CDesignExternalTag)AddTag();
            tag.Name = string.Format("Tag{0}", TagList.Count);            
            return tag;
        }
        
        
        public ICustomGroupTags AddGroup()
        {
            CDesignGroupOfExternalTags group;

            group = new CDesignGroupOfExternalTags(this, project);
            ObjectList.Add(group);
            //groupList.Add(group);
            OnAddItem(new AddItemEventArgs(group, group.ImageIndex));            
            return group;
        }

        public CDesignGroupOfExternalTags AddGroupEx()
        {
            CDesignGroupOfExternalTags group;

            group = new CDesignGroupOfExternalTags(this, project);
            ObjectList.Add(group);
            //groupList.Add(group);
            OnAddItem(new AddItemEventArgs(group, group.ImageIndex));            
            //
            
            
            group.Name = string.Format("Group{0}", GroupList.Count);             
            return group;
        }
        
        protected void SetAddress(int Value)
        {
            //customSlave.address = Value;
            for (int i = 0; i < ObjectList.Count; i++)
            {
                if (ObjectList[i] is CDesignGroupOfExternalTags)
                {
                    CDesignGroupOfExternalTags group = (CDesignGroupOfExternalTags)ObjectList[i];
                    //group.SetAddress(customSlave.address);
                }
                else
                {
                    CDesignExternalTag tag = (CDesignExternalTag)ObjectList[i];
                    //tag.SetSlave(customSlave.address);
                }
            }
        }
        
        private List<CDesignExternalTag> GetTagList()
        {
            IEnumerable<CDesignExternalTag> subset = ObjectList.OfType<CDesignExternalTag>();
            return subset.ToList();                        
        }

        private List<CDesignGroupOfExternalTags> GetGroupList()
        {
            IEnumerable<CDesignGroupOfExternalTags> subset = ObjectList.OfType<CDesignGroupOfExternalTags>();
            return subset.ToList();                        
        }
    }

    public class CDesignSlave : CDesignGroupOfExternalTags, ICustomSlave
    {        
        protected CCustomTCPClient tcpClient;
        protected CCustomSerialPort serialPort;
        private static int count = 0;        
        
        /*!
         * Construtor
         * @param AOwner
         * @param Project
         */
        public CDesignSlave(Object AOwner, CDesignProject Project)
            : base(AOwner,Project)
        {
            this.imageIndex = 31;
            this.tcpClient = new CCustomTCPClient();
            this.serialPort = new CCustomSerialPort();               
            count++;
        }
        ~CDesignSlave()
        {
            Dispose();
        }
        /*!
         * Destrutor explicito.
         * Remove proprio ponteiro da lista do objeto pai. 
         */
        public override void Dispose()
        {         
            count--;
            this.tcpClient = null;
            this.serialPort = null;
            base.Dispose();
        }

        public static int getCount()
        {
            return count;
        }        

        new protected void SetAddress(int Value)
        {
            customSlave.address = Value;
            for (int i = 0; i < ObjectList.Count; i++)
            {
                if (ObjectList[i] is CDesignGroupOfExternalTags)
                {
                    CDesignGroupOfExternalTags group = (CDesignGroupOfExternalTags)ObjectList[i];
                    //group.SetAddress(customSlave.address);
                }
                else
                {
                    CDesignExternalTag tag = (CDesignExternalTag)ObjectList[i];
                    tag.SetSlave(customSlave.address);
                }
            }
        }
        [Category("Communication")]
        public int Address
        {
            get { return this.customSlave.address; }
            set { this.SetAddress(value); }
        }
        [Category("Communication")]
        public CModbusType Protocol
        {
            get { return this.customSlave.protocol; }
            set { this.customSlave.protocol = value; }
        }
        [Category("Communication")]
        [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
        public CCustomSerialPort COMClientConfig
        {
            get { return this.serialPort; }
        }
        [Category("Communication")]
        [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
        public CCustomTCPClient TCPClientConfig
        {
            get { return this.tcpClient; }
        }        
    }
}
