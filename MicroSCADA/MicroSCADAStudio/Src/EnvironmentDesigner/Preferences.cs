using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace MicroSCADAStudio.Src.EnvironmentDesigner
{
    public static class CPreferences
    {
        private static CRecentFiles m_recentFiles = new CRecentFiles();
        private static XmlDocument m_xmlDocument;
        private const string XML_VERSION = "1.0";
        private const string XML_ENCODING = "UTF-8";
        private static string m_fileName = Application.StartupPath + "\\Preferences.xml";
        //! Diretorio de trabalho
        public static string WorkDirectory { get; set; }
        //! Lista de arquivos recentes
        public static CRecentFiles RecentFiles { get { return m_recentFiles; } }

        public static void Save()
        {
            XmlDeclaration xmldecl;
            XmlNode rootNode,node1;
            m_xmlDocument = new XmlDocument();

            xmldecl = m_xmlDocument.CreateXmlDeclaration(XML_VERSION, XML_ENCODING, null);
            m_xmlDocument.AppendChild(xmldecl);

            rootNode = m_xmlDocument.CreateNode(XmlNodeType.Element, "Preferences", "");
            m_xmlDocument.AppendChild(rootNode);

            node1 = m_xmlDocument.CreateNode(XmlNodeType.Element, "WorkDirectory", "");
            node1.InnerText = WorkDirectory;
            rootNode.AppendChild(node1);

            m_xmlDocument.Save(m_fileName);
        }

        public static void Open()
        {
            if (File.Exists(m_fileName))
            {
                m_xmlDocument = new XmlDocument();
                XmlNode rootNode, node1;
                m_xmlDocument.Load(m_fileName);
                rootNode = m_xmlDocument.DocumentElement;
                node1 = rootNode["WorkDirectory"];
                WorkDirectory = node1.InnerText;
            }
            else
            {
                Save();
            }
        }
    }
}
