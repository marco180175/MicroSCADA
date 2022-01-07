using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomGroupTags : ICustomSystem
    {
        ICustomTag AddTag();
        ICustomGroupTags AddGroup();
    }

    public interface ICustomSlave : ICustomGroupTags
    {
        int Address { get; set; }
        CModbusType Protocol { get; set; }
        CCustomSerialPort COMClientConfig { get; }
        CCustomTCPClient TCPClientConfig { get; }
    }

    public class CCustomSlave : Object
    {
        public int address;
        public CModbusType protocol;
        public CCustomSlave()
            : base()
        {
            this.address = 1;
        }
    }
}
