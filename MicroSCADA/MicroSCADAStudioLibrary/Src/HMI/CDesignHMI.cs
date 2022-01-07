using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src.HMI
{
    class CDesignHMI : CDesignSystem, ICustomHMI
    {
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         */
        public CDesignHMI(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("HMI");
            this.imageIndex = -1;
        }
        //!
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        public string Code { get; set; }
        public int Id { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public int ColorCount { get; set; }
        public bool FlagOff { get; set; }
        public int TimeOff { get; set; }     //9-TEMPO AUTO APAGAMENTO
        public int TimeMaxOff { get; set; }     //10-TEMPO MAXIMO DE AUTOAPAGAMENTO
        public int TimeMinOff { get; set; }     //11-TEMPO MINIMO DE AUTOAPAGAMENTO
        public int KeyCount { get; set; }     //12-NUMERO DE TECLAS (SE EXISTIR TECLADO)
        public bool FlagTouch { get; set; }       //13-VERDADEIRO SE FOR TOUCH SCREEM
        public int Cursor { get; set; }    //14-
        public int MaxCursor { get; set; }     //15-
        public int MinCursor { get; set; }     //16-
        public int ComCount { get; set; }   //17-NUMERO DE PORTAS SERIAIS
        public string Protocols { get; set; }     //18-PROTOCOLOS
        public int BaudRate { get; set; }   //19-BAUD RATE MAXIMO
        public int TagCount { get; set; }    //20-NUMERO MAXIMO DE TAGS
        public int SlaveCount { get; set; }    //21-NUMERO MAXIMO DE SLAVES
        public bool Snap { get; set; }     //22-VERDADEIRO SE USA GRID 8 X 16
        public bool ZeroLeft { get; set; }       //23-ZEROS A ESQUERDA
        public string FileNameBmp { get; set; }
        public bool FlagTagButtonKey { get; set; }
        public bool FlagTagButtonExt { get; set; }
        public bool FlagKeyBoard { get; set; }
        public bool FlagTag { get; set; }
        public bool ChangeScreen { get; set; }
        public bool EnabledKeyButtons { get; set; }
        public int LedKeyCount { get; set; }
        public int ButtonKeyCount { get; set; }
        public bool EnabledExtButtons { get; set; }
        public int LedExtCount { get; set; }
        public int ButtonExtCount { get; set; }
        public string IPAddress { get; set; }
        public ICustomComHMI NewCOM()
        {
            CDesignComHMI com = new CDesignComHMI(this, project);
            ObjectList.Add(com);
            OnAddItem(new AddItemEventArgs(com, com.ImageIndex));
            return com;
        }
        public int COMCount
        {
            get { return ObjectList.OfType<CDesignComHMI>().Count(); }
        }
        public ICustomKeyboard Keyboard { get; }
    }
}
