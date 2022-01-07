using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerSlave: CCompilerTagSlaveGroup, ICustomSlave
    {
        protected CCustomTCPClient tcpClient;
        protected CCustomSerialPort serialPort;
        /*!
         * Construtor
         */
        public CCompilerSlave(Object AOwner, CCompilerProject Project)
            : base(AOwner, Project)
        {
            this.tcpClient = new CCustomTCPClient();
            this.serialPort = new CCustomSerialPort();
        }
        public int Address { get; set; }
        public CModbusType Protocol { get; set; }
        public CCustomSerialPort COMClientConfig
        {
            get { return this.serialPort; }
        }

        public CCustomTCPClient TCPClientConfig
        {
            get { return this.tcpClient; }
        }
    }
}
