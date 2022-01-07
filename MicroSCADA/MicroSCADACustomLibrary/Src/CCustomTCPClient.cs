using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public class CCustomTCPClient
    {
        private String address;
        private int port;
        public CCustomTCPClient()
        {
            this.address = "127.0.0.1";
            this.port = 502;
        }
        public String Address
        {
            get { return this.address; }
            set { this.address = value; }
        }
        public int Port
        {
            get { return this.port; }
            set { this.port = value; }
        }
        public override string ToString()
        {
            return address + ":" + port.ToString();
        }
    }
}
