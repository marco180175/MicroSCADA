using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomKey : ICustomObject
    {
        int Left { get; set; }   //2-X
        int Top { get; set; }  //3-Y
        int Width { get; set; }   //4-LARGURA
        int Height { get; set; }   //5-ALTURA
        int Row { get; set; }   //6-LINHA
        int Col { get; set; }   //7-COLUNA
        byte[] FunctionCode { get; set; }        
    }
    public class CCustomKey
    {
    }
}
