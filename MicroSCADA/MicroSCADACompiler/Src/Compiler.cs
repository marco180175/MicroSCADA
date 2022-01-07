using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using MicroSCADACompilerLibrary.Src;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.IOFiles;

namespace MicroSCADACompiler.Src
{
    class BinaryWriterCompiler : BinaryWriter
    {
        public BinaryWriterCompiler(Stream output)
            : base(output)
        { }
        public void WriteLittleEndian(Int16 Value)
        {
            byte[] buffer = new byte[2];
            buffer[0] = (byte)(Value / 256);
            buffer[1] = (byte)(Value % 256);
            Write(buffer, 0, 2);
        }
        public void WriteLittleEndian(Int32 Value)
        {
            byte[] buffer1 = new byte[4];
            UInt16[] buffer2 = new UInt16[2];
            buffer2[0] = (UInt16)(Value / 65536);
            buffer2[1] = (UInt16)(Value % 65536);
            buffer1[0] = (byte)(buffer2[0] / 256);
            buffer1[1] = (byte)(buffer2[0] % 256);
            buffer1[2] = (byte)(buffer2[1] / 256);
            buffer1[3] = (byte)(buffer2[1] % 256);
            Write(buffer1, 0, 4);
        }
        public void Write(Stream Value,int Count)
        {
            byte[] buffer1 = new byte[Count];
            Value.Read(buffer1, 0, Count);
            Write(buffer1, 0, Count);
        }
    }

    class CCompilerItem
    {
        public Guid guid;
        public int position;
    }

    class CCompilerList:List<CCompilerItem>
    { }

    static class CCompiler
    {
        private static Dictionary<Guid, int> compilerTable = new Dictionary<Guid, int>();
        private static CCompilerList compilerList = new CCompilerList();
        private static FileStream outputFile;        
        private static BinaryWriterCompiler outputWriter;
        public static event MessageEventHandler MessageEvent;

        private static void OnMessageEvent(MessageEventArgs e)
        {
            if (MessageEvent != null)
                MessageEvent(typeof(CCompiler), e);
        }

        public static void Compiler(string FileName,CCompilerProject Project)
        {
            //
            string outputFileName = Path.ChangeExtension(FileName, ".m00");
            outputFile = new FileStream(outputFileName, FileMode.Create);
            outputWriter = new BinaryWriterCompiler(outputFile);
            //
            //CompilerWriteHeader2720XX(Project.GetHMI());
            //
            CompilerBitmapList2720XX(FileName, Project.GetBitmapList());
            //
            outputFile.Close();
            outputFile.Dispose();
        }

        private static void CompilerWriteHeader2720XX(CCompilerHMI HMI)
        {
            string input;
            byte[] buffer;
            //IDENTIFICADOR
            input = "ON-LINE ";
            buffer = Encoding.ASCII.GetBytes(input);
            outputWriter.Write(buffer, 0, 8);
            //DRIVER
            buffer = Encoding.ASCII.GetBytes(HMI.Code);
            outputWriter.Write(buffer, 0, buffer.Length);
            //CODIGO DE ESTACOES
            //FillChar(Buffer, 8,$FF);
            //m_File.Write(Buffer, 8);
            //NOME DO ARQUVO DE PROJETO SEM EXTENÇÃO
            //GetFileName(Buffer);
            //m_File.Write(Buffer, 16);
            //DATA
            input = DateTime.Now.Date.ToString();
            buffer = Encoding.ASCII.GetBytes(input);
            outputWriter.Write(buffer, 0, buffer.Length);
            //VERSAO DO ARQUIVO Dmp
            //Version := 1;
            //Version := 2;
            //Version := 3;
            int Version = 4;            
            outputWriter.WriteLittleEndian((Int16)Version);
            //ALTURA DA TELA EM PIXELS
            outputWriter.WriteLittleEndian((Int16)HMI.ScreenHeight);
            //LARGURA DA TELA EM PIXELS
            outputWriter.WriteLittleEndian((Int16)HMI.ScreenWidth);            
        }

        private static void CompilerBitmapList2720XX(string FileName, CCompilerBitmapList BitmapList)
        {
            FileName = Path.ChangeExtension(FileName, ".tbm");
            BitmapList.Open(FileName);
            foreach (CCompilerBitmapItem item in BitmapList.ObjectList)
            {                
                if (item.IsUsed)
                {
                    OnMessageEvent(new MessageEventArgs("compiler " + item.Name));
                    compilerTable.Add(item.GUID, (int)outputFile.Position);
                    Stream bitmapStream = item.GetBitmapFromStream();
                    //string bmpFile = Path.GetDirectoryName(FileName) + "\\"+item.Name + ".bmp";
                    //Bitmap bm = new Bitmap(bitmapStream);
                    //bm.Save(bmpFile);                    
                    CompilerBitmap8bpp(bitmapStream);
                }
            }          
        }
        
        private static void CompilerBitmap8bpp(Stream bitmapStream)
        {               
            BinaryReader bitmapReader = new BinaryReader(bitmapStream);
            bitmapReader.BaseStream.Position = 0x1c;
            int colorBit = bitmapReader.ReadInt16();
            if (colorBit == 8)
            {                
                Stream bitmapHMI = new MemoryStream();
                bitmapReader.BaseStream.Position = 0x12;
                int width = bitmapReader.ReadInt32();
                int height = bitmapReader.ReadInt32();
                bitmapReader.BaseStream.Position = 0x2E;
                int colorCount = bitmapReader.ReadInt32();

                int alignment;
                if (width % 4 == 0)
                    alignment = 0;
                else
                    alignment = 4 - (width % 4);

                byte[] buffer = new byte[width];
                bitmapHMI.WriteByte((byte)colorCount);
                bitmapStream.Position = 0x36;
                while (colorCount > 0)
                {
                    bitmapStream.Read(buffer, 0, 4);
                    bitmapHMI.Write(buffer, 0, 3);
                    colorCount--;
                }
                while (bitmapStream.Position < bitmapStream.Length)
                {
                    bitmapStream.Read(buffer, 0, width);
                    bitmapHMI.Write(buffer, 0, width);
                    bitmapStream.Seek(alignment, SeekOrigin.Current);
                }
                //                
                Stream bitmapCompress = CLZW.Compress(bitmapHMI);
                bitmapCompress.Position = 0;
                outputWriter.Write((byte)0x01);
                outputWriter.WriteLittleEndian((Int16)width);
                outputWriter.WriteLittleEndian((Int16)height);                
                outputWriter.Write(bitmapCompress, (int)bitmapCompress.Length);
                outputWriter.WriteLittleEndian((Int16)0x03FF);
            }
        }
    }
}
