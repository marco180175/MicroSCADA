using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public enum CBaudRate
    {
        BR1200 = 1200,
        BR2400 = 2400,
        BR4800 = 4800,
        BR9600 = 9600,
        BR14400 = 14400,
        BR19200 = 19200,
        BR38400 = 38400,
        BR57600 = 57600
    }
    public enum CDataBits
    {
        DB7=7,
        DB8=8
    }

    

    public interface ICustomSerialPort : ICustomObject
    {
        String COM { get; set; }
        CBaudRate BaudRate { get; set; }
        CDataBits DataBits { get; set; }
        System.IO.Ports.StopBits StopBits { get; set; }
        System.IO.Ports.Parity Parity { get; set; }        
    }

    public class CCustomSerialPort
    {
        private String com;
        private CBaudRate baudrate;
        private CDataBits dataBits;
        private System.IO.Ports.StopBits stopBits;
        private System.IO.Ports.Parity parity;
        public CCustomSerialPort()
        {
            this.com = "COM1";
            this.baudrate = CBaudRate.BR9600;
            this.dataBits = CDataBits.DB8;
            this.stopBits = System.IO.Ports.StopBits.One;
            this.parity = System.IO.Ports.Parity.None;
        }
        public String COM
        {
            get { return this.com; }
            set { this.com = value; }
        }
        public CBaudRate BaudRate
        {
            get { return this.baudrate; }
            set { this.baudrate = value; }
        }
        public CDataBits DataBits
        {
            get { return this.dataBits; }
            set { this.dataBits = value; }
        }
        public System.IO.Ports.StopBits StopBits
        {
            get { return this.stopBits; }
            set { this.stopBits = value; }
        }
        public System.IO.Ports.Parity Parity
        {
            get { return this.parity; }
            set { this.parity = value; }
        }
        public override string ToString()
        {
            return com + ":" + dataBits.ToString();
        }
    }
}
