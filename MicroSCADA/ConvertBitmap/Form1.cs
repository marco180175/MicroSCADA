using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ConvertBitmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bitmap1 = new Bitmap(openFileDialog1.FileName);
                label1.Text = bitmap1.PixelFormat.ToString();
                string destPath = Path.ChangeExtension(openFileDialog1.FileName, ".bmp");
                FileStream fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write);   
                Stream bitmap2 = BitmapConvert.Convert24To08(bitmap1);
                bitmap2.CopyTo(fileStream);
                fileStream.Close();
            }
        }
    }

    public static class BitmapConvert
    {
        private const int BITMAP_HEADER_SIZE = 54;
        private const int ALIGNMENT_SIZE = 4;
        public static Stream Convert24To08(Bitmap Input)
        {
            if (Input.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                int alignmentIn;
                int alignmentOut;
                byte[] alignmentArray = new byte[ALIGNMENT_SIZE] { 0, 0, 0, 0 };
                byte[] bitmapHeader = new byte[BITMAP_HEADER_SIZE];
                //24 bits
                if (((Input.Width * 3) % ALIGNMENT_SIZE) == 0)
                    alignmentIn = 0;
                else
                    alignmentIn = ALIGNMENT_SIZE - ((Input.Width * 3) % ALIGNMENT_SIZE);
                //8 bits
                if ((Input.Width % ALIGNMENT_SIZE) == 0)
                    alignmentOut = 0;
                else
                    alignmentOut = ALIGNMENT_SIZE - (Input.Width % ALIGNMENT_SIZE);
                //
                MemoryStream streamIn = new MemoryStream();
                MemoryStream streamIndex = new MemoryStream();
                ColorPalette pelette = new ColorPalette();
                Input.Save(streamIn, System.Drawing.Imaging.ImageFormat.Bmp);
                streamIn.Position = 0;
                streamIn.Read(bitmapHeader, 0, bitmapHeader.Length);
                while (streamIn.Position < streamIn.Length)
                {
                    int c = Input.Width;
                    while (c > 0)
                    {
                        byte r = (byte)streamIn.ReadByte();
                        byte g = (byte)streamIn.ReadByte();
                        byte b = (byte)streamIn.ReadByte();
                        Color color = Color.FromArgb(r, g, b);
                        byte Index = (byte)pelette.AddColor(color);
                        streamIndex.WriteByte(Index);
                        c--;
                    }
                    //
                    streamIn.Seek(alignmentIn, SeekOrigin.Current);
                    //
                    streamIndex.Write(alignmentArray, 0, alignmentOut);
                }
                //
                MemoryStream streamOut = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(streamOut);
                int bfOffBits;
                streamOut.Write(bitmapHeader, 0, bitmapHeader.Length);
                bfOffBits = BITMAP_HEADER_SIZE + 4 * pelette.Count;
                writer.BaseStream.Position = 0x02;
                writer.Write((Int32)(bfOffBits + streamIndex.Length));//FileHeader.bfSize :=FileHeader.bfOffBits + DWORD(StrIdx.Size);
                writer.BaseStream.Position = 0x0A;
                writer.Write((Int32)bfOffBits);//FileHeader.bfOffBits      :=SizeOf(FileHeader) + SizeOf(InfoHeader) + SizeOf(RGBQUAD) *Count;                                    
                writer.BaseStream.Position = 0x1C;
                writer.Write((Int16)8);//InfoHeader.biBitCount     :=8;
                writer.BaseStream.Position = 0x22;
                writer.Write((Int32)streamIndex.Length);//InfoHeader.biSizeImage    :=StrIdx.Size;
                writer.BaseStream.Position = 0x2E;
                writer.Write((Int32)pelette.Count);//InfoHeader.biClrUsed      :=Count;
                writer.Write((Int32)pelette.Count);//InfoHeader.biClrImportant :=Count;
                writer.BaseStream.Position = 0x36;
                pelette.CopyTo(streamOut);
                byte[] pixelArray = new byte[streamIndex.Length];
                streamIndex.Position = 0;
                streamIndex.Read(pixelArray, 0, pixelArray.Length);
                streamOut.Write(pixelArray, 0, pixelArray.Length);
                streamOut.Position = 0;
                return streamOut;
            }
            else
                return null;
        }
    }

    public class ColorPalette
    {
        private List<Color> colorList;
        public ColorPalette()
        {
            colorList = new List<Color>();
        }
        public int Count { get { return colorList.Count; } }
        public int AddColor(Color Value)
        {
            int best = 0;
            double error = 20000000;
            double howClose;
            double data;

            for (int I = 0; I < colorList.Count; I++)
            {
                if (colorList[I] == Value)
                {
                    return I;
                }
                data = Value.R - colorList[I].R;
                data = data * data;
                howClose = data;
                data = Value.G - colorList[I].G;
                data = data * data;
                howClose = howClose + data;
                data = Value.B - colorList[I].B;
                data = data * data;
                howClose = howClose + data;
                howClose = Math.Sqrt(howClose);
                if (howClose < error)
                {
                    best = I;
                    error = howClose;
                }
            }
            //IDEAL PARA FOTOS 18%
            if ((error > 18) && (colorList.Count < 256))  //erro de aproximação de 2%
            {
                Color color = new Color();
                color = Value;
                colorList.Add(color);
                return colorList.Count - 1;
            }
            else
                return best;
        }
        public void CopyTo(Stream Target)
        {
            foreach (Color color in colorList)
            {
                Target.WriteByte(color.R);
                Target.WriteByte(color.G);
                Target.WriteByte(color.B);
                Target.WriteByte(0);
            }
        }
    }
}
