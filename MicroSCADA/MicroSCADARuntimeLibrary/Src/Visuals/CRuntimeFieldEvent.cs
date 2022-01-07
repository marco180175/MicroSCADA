using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    /*!
     * 
     */
    public class FieldEditValueEventArgs : EventArgs
    {
        private string m_value;
        public FieldEditValueEventArgs(string Value)
        {
            m_value = Value;
        }
        public string Value { get { return m_value; } }
    }
    
    /*!
     * 
     */
    public delegate void FieldEditValueEventHandler(object sender, FieldEditValueEventArgs e);

    /*!
     * 
     */
    public class FieldEditErrorEventArgs : EventArgs
    {
        private string m_errorMessage;
        public FieldEditErrorEventArgs(string ErrorMessage)
            : base()
        {
            m_errorMessage = ErrorMessage;
        }
        public string ErrorMessage { get { return m_errorMessage; } }
    }

    /*!
     * 
     */
    public delegate void FieldEditErrorEventHandler(object sender, FieldEditErrorEventArgs e);
}
