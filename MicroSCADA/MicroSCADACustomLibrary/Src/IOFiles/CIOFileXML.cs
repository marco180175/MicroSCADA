using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MicroSCADACustomLibrary.Src.IOFiles
{
    public abstract class CIOFileXML
    {
        protected ICustomProject project;
        protected XmlDocument xmlDocument;
        protected const string XML_VERSION = "1.0";
        protected const string XML_ENCODING = "UTF-8";
        protected const string GUID_ID = "GUID";
        protected const string NAME_ID = "Name";
        protected const string DESCRIPTION_ID = "Description";
        protected const string COMMENT_ID = "Comment";
        protected const string ID_TAGVALUE = "TagValue";
        public CIOFileXML(ICustomProject Project)
        {
            this.project = Project;
            this.xmlDocument = new XmlDocument();
        }
        public event IOFileErrorEventHandler FileError;
        protected void OnIOError(IOFileErrorEventArgs e)
        {
            if (FileError != null)
                FileError(this, e);
        }
    }


    public class IOFileErrorEventArgs : EventArgs
    {
        string m_errorMessage;
        public IOFileErrorEventArgs(string ErrorMessage)
            : base()
        {
            m_errorMessage = ErrorMessage;
        }
        public string ErrorMessage { get { return m_errorMessage; } }
    }

    public delegate void IOFileErrorEventHandler(object sender, IOFileErrorEventArgs e);
}
