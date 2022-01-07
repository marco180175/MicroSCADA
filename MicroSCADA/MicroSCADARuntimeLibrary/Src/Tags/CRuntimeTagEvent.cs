using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src.Tags
{

    public class TagSetValueEventArgs : EventArgs
    {
        private string m_value;
        private CCustomDataType m_dataType;
        public TagSetValueEventArgs(string Value, CCustomDataType DataType)
        {
            m_value = Value;
            m_dataType = DataType;
        }
        public string Value { get { return m_value; } }
        public CCustomDataType DataType { get { return m_dataType; } }
    }

    public delegate void TagSetValueEventHandler(object sender, TagSetValueEventArgs e);
}
