using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Xml;

namespace MicroSCADAStudio.Src.EnvironmentDesigner
{
    /*!
     * Classe que implementa biblioteca de tabela de bitmaps
     * O arquivo com nome das tabelas esta no diretorio "lib" na rais 
     * da instalação.
     * Cada tabela posui um arquivos de indices e um de dados
     * TODO:descrever formato dos arquivos
     */
    public class CImageLibray
    {
        private string m_fileName;
        private string m_filePath;
        private List<string> m_fileList;
        private MemoryStream m_imageTable;
        private MemoryStream m_imageIndex;
        private BinaryReader m_reader;        
        private const string m_ID = "BMP_LIB";
        /*!
         * Construtor
         * @param FilePath Caminho da biblioteca
         */
        public CImageLibray(string FilePath)
        {
            this.m_fileList = new List<string>();
            this.m_imageIndex = new MemoryStream();
            this.m_imageTable = new MemoryStream();
            this.m_reader = new BinaryReader(this.m_imageIndex);            
            this.m_filePath = FilePath + "lib\\";
            this.OpenLibrary(m_filePath+"library.xml");        
        }
        private void OpenLibrary(string FileName)
        {            
            m_fileName = FileName;
            if (File.Exists(m_fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(m_fileName);
                XmlNode node = xmlDoc.DocumentElement["FileNameList"];                
                foreach (XmlNode n in node.ChildNodes)
                {
                    m_fileList.Add(n.InnerText);
                }
            }
            else
            {
                m_fileList.Add("FILE NOT FOUND");
                m_fileList.Add(m_fileName);
            }            
        }
        public List<string> FileList
        {
            get { return m_fileList; }
        }
        /*!
         * Abre tabela de figuras apontada pelo indice
         * @param Index Indice da tabela
         */
        public void Open(int Index)
        {
            FileStream file;
            byte[] buffer;
            string fn;
            //Limpa arquivos indice e tabela
            m_imageIndex.Seek(0, SeekOrigin.Begin);
            m_imageIndex.SetLength(0);
            m_imageTable.Seek(0, SeekOrigin.Begin);
            m_imageTable.SetLength(0);
            //Abre index
            fn = Path.ChangeExtension(m_filePath + m_fileList[Index], ".idx");
            file = File.Open(fn, FileMode.Open);
            buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            m_imageIndex.Write(buffer, 0, (int)file.Length);
            file.Close();
            //Abre table
            fn = Path.ChangeExtension(fn, ".tab");
            file = File.Open(fn, FileMode.Open);
            buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            m_imageTable.Write(buffer, 0, (int)file.Length);
            file.Close();
        }
        /*!
         * Retorna figura da tabela que estiver aberta
         * @param Index Indice da figura na tabela
         * @return Figura apontada pelo indice
         */
        public Bitmap GetImage(int Index)
        {
            Bitmap image;
            int endPosition;
            m_imageIndex.Position = 16 + (Index * 4);
            int position = m_reader.ReadInt32();
            if (Index < (GetImageCount() - 1))
                endPosition = m_reader.ReadInt32();
            else
                endPosition = (int)m_imageTable.Length;
            m_imageTable.Position = position;
            int offset = m_imageTable.ReadByte();
            m_imageTable.Position += offset;            
            MemoryStream ms = new MemoryStream();
            int size = endPosition - (int)m_imageTable.Position;
            byte[] buffer = new byte[size];
            m_imageTable.Read(buffer, 0, size);
            ms.Write(buffer, 0, size);
            ms.Seek(0, SeekOrigin.Begin);
            image = new Bitmap(ms);
            ms.Dispose();
            return image;
        }
        /*!
         * Retorna numero de figuras da tabela que estiver aberta
         * @return Numero de figuras
         */
        public Int32 GetImageCount()
        {
            Int32 count;
            long ret = m_imageIndex.Position;
            m_imageIndex.Position = 8;
            count = m_reader.ReadInt32();
            m_imageIndex.Position = ret;
            return count;
        }
        public void NewTable()
        {
            //TODO:implementar
        }
        public void AddTable()
        {
            //TODO:implementar
        }
        public void DeleteTable()
        {
            //TODO:implementar
        }
    }
}
