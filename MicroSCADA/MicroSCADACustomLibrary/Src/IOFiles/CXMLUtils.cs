using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MicroSCADACustomLibrary.Src.IOFiles
{
    static class CXMLUtils
    {
        /*!
         * 
         */
        static public string GetHashMD5(XmlDocument xmlDocument)
        {
            string xmlText = GetXMLText(xmlDocument);
            return CalculateMD5Hash(xmlText);
        }
        /*!
         * 
         */
        static private string CalculateMD5Hash(string input)
        {
            // Primeiro passo, calcular o MD5 hash a partir da string
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            //byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // Segundo passo, converter o array de bytes em uma string haxadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        /*!
         * 
         */        
        static private string GetXMLText(XmlDocument xmlDocument)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDocument.Save(xw);
            return sw.ToString();
        }
    }
}
