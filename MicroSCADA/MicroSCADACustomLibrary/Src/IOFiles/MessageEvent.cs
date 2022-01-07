using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.IOFiles
{
    public class MessageEventArgs : EventArgs
    {
        private string m_message;
        public MessageEventArgs(string message)
        {
            m_message = message;
        }
        public string Message { get { return m_message; } }
    }
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);
}
