using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeSlave : CRuntimeTagGroup, ICustomSlave
    {
        protected CCustomTCPClient  tcpClient;
        protected CCustomSerialPort serialPort;                       
        public CRuntimeSlave(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.tcpClient = new CCustomTCPClient();
            this.serialPort = new CCustomSerialPort();
            this.customSlave.address = 1;
        }
        public override ICustomTag AddTag()
        {
            CRuntimeExternalTag tag;

            tag = new CRuntimeExternalTag(this, project);
            ObjectList.Add(tag);
            //tag.Name = "Tag" + ObjectList.Count.ToString();
            tag.Slave = customSlave.address;
            tag.slaveOwner = this;
            return tag;
        }
        /*!
         * 
         */
        public override ICustomGroupTags AddGroup()
        {
            /*
            CRuntimeTagGroup group;
            group = new CRuntimeTagGroup(this, project);
            ObjectList.Add(group);
            group.Name = "Group" + ObjectList.Count.ToString();
            group.SetAddress(customSlave.address);
            group.slaveOwner = this;
            return group;
            */
            return this;
        }
        public int Address
        {
            get { return this.customSlave.address; }
            set { this.SetAddress(value); }
        }
        public CModbusType Protocol
        {
            get { return this.customSlave.protocol; }
            set { this.customSlave.protocol = value; }
        }

        public CCustomSerialPort COMClientConfig
        {
            get { return this.serialPort; }
        }

        public CCustomTCPClient TCPClientConfig
        {
            get { return this.tcpClient; }
        }

        public void SetTime(String Value)
        {
                        
        }
    }
}
