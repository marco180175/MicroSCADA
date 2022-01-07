using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomHMI : ICustomSystem
    {
        string Code { get; set; }
        int Id { get; set; }
        int ScreenWidth { get; set; }
        int ScreenHeight { get; set; }
        int ColorCount { get; set; }
        bool FlagOff { get; set; }
        int TimeOff { get; set; }     //9-TEMPO AUTO APAGAMENTO
        int TimeMaxOff { get; set; }     //10-TEMPO MAXIMO DE AUTOAPAGAMENTO
        int TimeMinOff { get; set; }     //11-TEMPO MINIMO DE AUTOAPAGAMENTO
        int KeyCount { get; set; }     //12-NUMERO DE TECLAS (SE EXISTIR TECLADO)
        bool FlagTouch { get; set; }       //13-VERDADEIRO SE FOR TOUCH SCREEM
        int Cursor { get; set; }    //14-
        int MaxCursor { get; set; }     //15-
        int MinCursor { get; set; }     //16-
        int ComCount { get; set; }   //17-NUMERO DE PORTAS SERIAIS
        string Protocols { get; set; }     //18-PROTOCOLOS
        int BaudRate { get; set; }   //19-BAUD RATE MAXIMO
        int TagCount { get; set; }    //20-NUMERO MAXIMO DE TAGS
        int SlaveCount { get; set; }    //21-NUMERO MAXIMO DE SLAVES
        bool Snap { get; set; }     //22-VERDADEIRO SE USA GRID 8 X 16
        bool ZeroLeft { get; set; }       //23-ZEROS A ESQUERDA
        string FileNameBmp { get; set; }
        bool FlagTagButtonKey { get; set; }
        bool FlagTagButtonExt { get; set; }
        bool FlagKeyBoard { get; set; }
        bool FlagTag { get; set; }        
        bool ChangeScreen { get; set; }
        bool EnabledKeyButtons { get; set; }
        int LedKeyCount { get; set; }
        int ButtonKeyCount { get; set; }
        bool EnabledExtButtons { get; set; }
        int LedExtCount { get; set; }
        int ButtonExtCount { get; set; }
        string IPAddress { get; set; }
        ICustomComHMI NewCOM();
        int COMCount { get; }
        ICustomKeyboard Keyboard { get; }
    }
    public class CustomHMI
    {
    }
}
