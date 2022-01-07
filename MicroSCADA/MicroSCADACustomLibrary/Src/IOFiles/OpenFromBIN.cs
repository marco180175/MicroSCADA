using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADACustomLibrary.Src.IOFiles
{
    public class BinaryReaderBIN : BinaryReader
    {
        public BinaryReaderBIN(Stream input)
            : base(input)
        { }
        public string ReadSizeString()
        {
            int size = ReadInt16();
            if (size <= 256)
            {
                byte[] byteArray = ReadBytes(size);
                return Encoding.UTF8.GetString(byteArray);
            }
            else
                return string.Empty;
        }
    }

    public static class COpenFromBIN
    {
        private static FileStream fileStream;
        private static BinaryReaderBIN reader;
        public static event MessageEventHandler MessageEvent;
        public static event OpenProgressEventHandler OpenProgress;
        private static void OnMessageEvent(MessageEventArgs e)
        {
            if (MessageEvent != null)
                MessageEvent(typeof(COpenFromBIN), e);
        }
        private static void OnOpenProgressEvent(OpenProgressEventArgs e)
        {
            if (OpenProgress != null)
                OpenProgress(typeof(COpenFromBIN), e);
        }
        public static void Open(string FileName, ICustomProject Project)        
        {
            fileStream = new FileStream(FileName, FileMode.Open);
            reader = new BinaryReaderBIN(fileStream);
            //
            reader.ReadInt32();//LE ID DE CLASSE DO PROJETO(CASO A PARTE)
            reader.ReadInt32();//LE NUMERO DE PROPRIEDADES
            //LE PROPRIEDADES
            string id = reader.ReadSizeString();
            string version = reader.ReadSizeString();//m_Version
            Project.Name = reader.ReadSizeString();//Name
            Project.Comment = reader.ReadSizeString();//m_Comment
            reader.ReadInt64();//m_Time
            //IHM (COMS, TECLADO E TAGS)
            reader.ReadInt32();                   
            OpenHMI(Project.HMI);
            //
            reader.ReadInt32();             
            OpenNetwork(Project.Network);
            //
            reader.ReadInt32();
            OpenEnabledFlag();
            //
            reader.ReadInt32();
            OpenBitArrayList(Project.BitArrayList);
            //
            reader.ReadInt32();
            OpenPasswords(Project.PasswordList);
            //
            reader.ReadInt32();
            OpenProgramList(Project.ProgramList);
            //            
            reader.ReadInt32();
            OpenBitmapList(Project.BitmapList);
            //
            reader.ReadInt32();
            OpenFontList(Project.FontList);
            //
            fileStream.Close();
            fileStream.Dispose();
        }

        private static void OpenHMI(ICustomHMI HMI)
        {
            reader.ReadInt32();
            //LE PROPRIEDADES
            HMI.Name = reader.ReadSizeString();      //1-NOME
            reader.ReadSizeString();      //2-DESCRICAO
            reader.ReadSizeString();      //3-CODIGO
            reader.ReadInt32();     //4-ID
            reader.ReadInt32();     //5-LAGURA DA TELA
            reader.ReadInt32();     //6-ALTURA DA TELA
            reader.ReadInt32();     //7-NUMERO DE CORES

            //if m_ColorCount = 8 then
            //    m_GrayScale :=true
            //else
            //    m_GrayScale :=false;

            reader.ReadBoolean();        //8-HABILITA AUTO APAGAMENTO
            reader.ReadInt32();     //9-TEMPO AUTO APAGAMENTO
            reader.ReadInt32();     //10-TEMPO MAXIMO DE AUTOAPAGAMENTO
            reader.ReadInt32();     //11-TEMPO MINIMO DE AUTOAPAGAMENTO
            reader.ReadInt32();     //12-NUMERO DE TECLAS (SE EXISTIR TECLADO)
            reader.ReadBoolean();        //13-VERDADEIRO SE FOR TOUCH SCREEM
            reader.ReadInt32();     //14-
            reader.ReadInt32();     //15-
            reader.ReadInt32();     //16-
            reader.ReadInt32();     //17-NUMERO DE PORTAS SERIAIS
            reader.ReadSizeString();      //18-PROTOCOLOS
            reader.ReadInt32();     //19-BAUD RATE MAXIMO
            reader.ReadInt32();     //20-NUMERO MAXIMO DE TAGS
            reader.ReadInt32();     //21-NUMERO MAXIMO DE SLAVES
            reader.ReadInt32();     //22-VERDADEIRO SE USA GRID 8 X 16
            reader.ReadBoolean();        //23-ZEROS A ESQUERDA
            reader.ReadSizeString();      //24-
            //ObjectIndex     :=0;
            reader.ReadSizeString();      //25-TAG EI DE SINCRONISMO
            //ObjectIndex     :=1;
            reader.ReadSizeString();      //26-TAG TELA ATUAL
            //ObjectIndex     :=2;
            reader.ReadSizeString();      //27-TAG LEDS 1
            //ObjectIndex     :=3;
            reader.ReadSizeString();      //28-TAG BOTÕES 1
            //ObjectIndex     :=4;
            reader.ReadSizeString();      //29-TAG LEDS 2
            //ObjectIndex     :=5;
            reader.ReadSizeString();      //30-TAG BOTÕES 2
            reader.ReadBoolean();        //FLAG HABILITA BOTOEIRA TECLADO
            reader.ReadBoolean();        //FLAG HABILITA BOTOEIRA EXTERNA

            int comCount = reader.ReadInt32();            //31-PORTAS COM
            
            while (HMI.COMCount < comCount)
            {
                ICustomComHMI com = HMI.NewCOM();
                int classId = reader.ReadInt32();
                int numProp = reader.ReadInt32();
                com.Name = reader.ReadSizeString();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadBoolean();
            }

            HMI.FlagKeyBoard = reader.ReadBoolean();          //28-TECLADO
            //if(HMI.FlagKeyBoard)
            //{
            //    reader.ReadInt32();
            //    reader.ReadInt32();
            //    //LE PROPRIEDADES
            //    HMI.Keyboard.Name = reader.ReadSizeString();            //1-NOME
            //    HMI.Keyboard.HMICode = reader.ReadSizeString();            //2-CODIGO DA IHM
            //    int keyCount = reader.ReadInt32();           //3-TECLAS
            //    while (HMI.Keyboard.ObjectList.Count < keyCount)
            //    {
            //        reader.ReadInt32();
            //        ICustomKey key = HMI.Keyboard.NewKey();                
            //    }
            //}

            HMI.FlagTag = reader.ReadBoolean();              //29-TAGS
            //if m_fTags then
            //    begin
            //    Config.ReadClassId;
            //    //LE OS TAGS COMO SE FOSSE O OBJETO TAGS IHM
            //    Config.Push;
            //    Config.ReadNumProps;
            //    //LE PROPRIEDADES
            //    Name    :=Config.ReadString;
            //    Count   :=Config.ReadInteger;
            //    for I:=0 to Count - 1 do
            //        begin
            //        Config.ReadClassId;
            //        Tag    :=HTagHmi.CreateObject(Self,m_pProject);
            //        Tag.LoadConfig(Config);
            //        m_ltTagUsr.Add(Tag);
            //        end;
            //    Config.Pop;
            //    end;

            //if Config.Status then goto EndLoadIhm;

            ////NOVAS PROPRIEDADES DEVEM SER LIDAS APOS ESTA LINHA
            //ObjectIndex     :=6;
            reader.ReadSizeString();    //30-TAG BOTÕES 2
            //if Config.Status then goto EndLoadIhm;

            reader.ReadSizeString();  //ENDEREÇO IP (NÃO É UTILIZADO PELO COMPILADOR)
            //if Config.Status then goto EndLoadIhm;

            reader.ReadBoolean();//HMI.Monochrome
            //if Config.Status then goto EndLoadIhm;

            reader.ReadBoolean();//HMI.NewWidth
            //if Config.Status then goto EndLoadIhm;

            reader.ReadInt32();//LanguageIndex
            //if Config.Status then goto EndLoadIhm;

            HMI.ChangeScreen = reader.ReadBoolean();
            //if Config.Status then goto EndLoadIhm;

            HMI.LedKeyCount = reader.ReadInt32();
            HMI.ButtonKeyCount = reader.ReadInt32();
            HMI.LedExtCount = reader.ReadInt32();
            HMI.ButtonExtCount = reader.ReadInt32();
            //if Config.Status then goto EndLoadIhm;

            //ObjectIndex     :=7;
            //reader.ReadSizeString();      //25-TAG RELOGIO

        }

        private static void OpenNetwork(ICustomNetwork Network)
        {
            int numProps = reader.ReadInt32();
            //LE PROPRIEDADES
            Network.Name = reader.ReadSizeString();                    //1-NOME
            int count = reader.ReadInt32();                   //2-LE NUMERO DE OBJETOS
            while (Network.ObjectList.Count < count)                              //CRIA CLPS
            {
                ICustomSlave slave = Network.NewSlave();
                int classId = reader.ReadInt32();
                OpenSlave(slave);
            }
        }

        private static void OpenSlave(ICustomSlave Slave)
        {
            int numProps = reader.ReadInt32();                            //LE NUMERO DE PROPRIEDADES
                                                                          //
            Slave.Name = reader.ReadSizeString();            //1-NOME
            Slave.Description = reader.ReadSizeString();            //2-DESCRIÇÃO
            Slave.Address = reader.ReadInt32();           //3-ESTAÇÃO
            int count = reader.ReadInt32();           //4-NUMERO DE OBJETOS
            
            OpenSlaveItem(Slave,count);
            
            reader.ReadInt32();//m_ModbusFunction:=
            reader.ReadInt32();
            //if Config.Status then
            //goto LoadConfig_End;
            //Nome do arquivo com configuração dos tags (não utilizado aqui)    
            reader.ReadSizeString();
            //if Config.Status then
            //goto LoadConfig_End;
            //
            reader.ReadInt32();//m_MaxCoils:=
            reader.ReadInt32();//m_MaxRegisters:=
        }

        private static void OpenSlaveItem(ICustomGroupTags Item,int Count)
        {
            while (Item.ObjectList.Count < Count)                              //CRIA CLPS
            {
                int classId = reader.ReadInt32();
                switch (classId)
                {
                    case 19:
                        {
                            ICustomExternalTag tag = (ICustomExternalTag)Item.AddTag();
                            OpenExternalTag(tag);
                        }; break;
                    case 18:
                        {
                            ICustomGroupTags group = Item.AddGroup();
                            OpenExternalTagGroup(group);
                        }; break;
                }
            }
        }

        private static void OpenExternalTag(ICustomExternalTag ExternalTag)
        {
            double pos = reader.BaseStream.Position;
            double len = reader.BaseStream.Length;
            double val = 100 * (pos / len);
            OnOpenProgressEvent(new OpenProgressEventArgs(val));
            int numProps = reader.ReadInt32();                            //LE NUMERO DE PROPRIEDADES
            //LE PROPRIEDADES
            ExternalTag.Name = reader.ReadSizeString();    //1-NOME
            ExternalTag.Description = reader.ReadSizeString();            //2-DESCRIÇÃO
            reader.ReadInt32();           //3-NUMERO DA SLAVE
            ExternalTag.Address = reader.ReadInt32();           //4-ENDEREÇO                                            
            ExternalTag.DataType = (CCustomDataType)reader.ReadInt32();//m_DataType:=
            reader.ReadInt32();//m_TagType:=                               
            ExternalTag.Address = reader.ReadInt32();//m_ModbusAddress                               
            bool fArray = reader.ReadBoolean();
            if(fArray)
                ExternalTag.ArraySize = reader.ReadInt32();
            else
                reader.ReadInt32();
        }

        private static void OpenExternalTagGroup(ICustomGroupTags GroupTag)
        {
            int numProps = reader.ReadInt32();                            //LE NUMERO DE PROPRIEDADES
                                                                          //
            GroupTag.Name = reader.ReadSizeString();            //1-NOME
            GroupTag.Description = reader.ReadSizeString();            //2-DESCRIÇÃO            
            int count = reader.ReadInt32();           //4-NUMERO DE OBJETOS
            
            OpenSlaveItem(GroupTag,count);
            
        }


        private static void OpenEnabledFlag()
        {
            int numProps = reader.ReadInt32();
            //LE PROPRIEDADES
            reader.ReadSizeString();    //1-NOME
            reader.ReadInt32();   //2-NUMERO DA SLAVE
            reader.ReadInt32();   //3-ENDEREÇO
                                            //
            //if Config.Status then
                //goto EndLoadConfig;
            //ObjectIndex:= SLAVE_TAG;
            reader.ReadSizeString();//29
        }
        
        private static void OpenBitArrayList(ICustomBitArrayList BitArray)
        {
            int numProps = reader.ReadInt32();
            //LE PROPRIEDADES
            BitArray.Name = reader.ReadSizeString();                    //1-NOME
            int count = reader.ReadInt32();
            while (BitArray.ObjectList.Count < count)
            {
                int classId = reader.ReadInt32();
                switch (classId)
                {
                    case 0x80:
                        {                                                        
                            //$80:CreateArrayCfg(Config);
                        }; break;
                    case 0x81:
                        {
                            ICustomBitArrayTriggerList triggerList = BitArray.NewTriggerList();
                            OpenTriggerList(triggerList);
                        }; break;
                    case 0x82:
                        {
                            ICustomBitArrayAlarmList alarmList = BitArray.NewAlarmList();
                            OpenAlarmList(alarmList);
                        }; break;
                }
            }   
        }
        
        private static void OpenTriggerList(ICustomBitArrayTriggerList TriggerList)
        {
            int numProps = reader.ReadInt32();
            //LE PROPRIEDADES
            TriggerList.Name = reader.ReadSizeString();            //1-NOME
            TriggerList.Description = reader.ReadSizeString();            //2-DESCRIÇÃO        
            reader.ReadSizeString();            //3-TAG
            TriggerList.Slave = reader.ReadInt32();           //4-id
            TriggerList.Address = reader.ReadInt32();           //5-address
            int count = reader.ReadInt32();           //6-GRUPOS
            while(TriggerList.ObjectList.Count<count)
            {
                int classId = reader.ReadInt32();
                switch(classId)
                {
                    case 0x01:
                        {
                            ICustomBitArrayTriggerItem item;
                            item = TriggerList.NewTriggerItem();
                            reader.ReadInt32();
                            item.Name = reader.ReadSizeString();
                            reader.ReadSizeString();
                            reader.ReadInt32();
                            reader.ReadInt32();
                        };break;
                    case 0x02:
                        {
                            ICustomBitArrayTriggerProgram item;
                            item = TriggerList.NewTriggerProgram();
                            reader.ReadInt32();
                            item.Name = reader.ReadSizeString();
                            reader.ReadSizeString();
                            reader.ReadInt32();
                            reader.ReadInt32();
                            reader.ReadSizeString();
                        }; break;
                }
            }
        }

        private static void OpenAlarmList(ICustomBitArrayAlarmList AlarmList)
        {
            int numProps = reader.ReadInt32();
            //LE PROPRIEDADES
            AlarmList.Name = reader.ReadSizeString();            //1-NOME
            AlarmList.Description = reader.ReadSizeString();            //2-DESCRIÇÃO        
            reader.ReadSizeString();            //3-TAG            
            int count = reader.ReadInt32();           //
            while(AlarmList.ObjectList.Count < count)
            {
                ICustomBitArrayAlarmItem item;
                item = AlarmList.NewAlarmItem();
                int classId = reader.ReadInt32();
                numProps = reader.ReadInt32();
                item.Name = reader.ReadSizeString();
                item.Description = reader.ReadSizeString();
                item.Message[0] = reader.ReadSizeString();
                item.Alignment = reader.ReadInt32();
                item.Message[1]=reader.ReadSizeString();
                item.Message[2]=reader.ReadSizeString();
            }
        }

        private static void OpenPasswords(ICustomPasswordList PasswordList)
        {
            int numProps = reader.ReadInt32();
            PasswordList.Name=reader.ReadSizeString();//Nome
            PasswordList.Password[0]=reader.ReadSizeString();//1
            PasswordList.Password[1] = reader.ReadSizeString();
            PasswordList.Password[2] = reader.ReadSizeString();
            PasswordList.Password[3] = reader.ReadSizeString();
            PasswordList.Password[4] = reader.ReadSizeString();
            PasswordList.Password[5] = reader.ReadSizeString();
            PasswordList.Password[6] = reader.ReadSizeString();
            PasswordList.Password[7] = reader.ReadSizeString();//8
            PasswordList.Enabled = reader.ReadBoolean();
        }        

        private static void OpenProgramList(ICustomProgramList ProgramList)
        {
            int numProps = reader.ReadInt32();
            ProgramList.Name = reader.ReadSizeString();//Nome
            int count = reader.ReadInt32();
            while(ProgramList.ObjectList.Count < count)
            {                
                ICustomProgramItem program = ProgramList.NewProgram();
                reader.ReadInt32();//classid
                OpenProgram(program);
            }
        }

        private static void OpenProgram(ICustomProgramItem Program)
        {
            double pos = reader.BaseStream.Position;
            double len = reader.BaseStream.Length;
            double val = 100 * (pos / len);
            OnOpenProgressEvent(new OpenProgressEventArgs(val));
            //
            int numProps = reader.ReadInt32();
            Program.Name = reader.ReadSizeString();//Nome
            Program.Description = reader.ReadSizeString();//
            int count = reader.ReadInt32();
            while (Program.ObjectList.Count < count)
            {                
                ICustomFunction function = Program.NewFunction();
                reader.ReadInt32();//classid
                OpenFunction(function);
            }
        }

        private static void OpenFunction(ICustomFunction Function)
        {
            int numProps = reader.ReadInt32();
            Function.Name = reader.ReadSizeString();//Nome
            Function.Description = reader.ReadSizeString();//
            Function.OpCode= reader.ReadInt32();
            int count = reader.ReadInt32();
            while (Function.ObjectList.Count < count)
            {                 
                ICustomOperand operand = Function.NewOperand();                
                //
                reader.ReadSizeString();
                operand.ValueInt = reader.ReadInt32();
                operand.ValueStr = reader.ReadSizeString();
            }
        }
        /*!
         * 
         */
        private static void OpenBitmapList(ICustomBitmapList BitmapList)
        {
            int p1, p2;
            p1 = reader.ReadInt32();
            BitmapList.Name = reader.ReadSizeString();
            int count = reader.ReadInt32();
            while (BitmapList.ObjectList.Count < count)
            {
                double pos = reader.BaseStream.Position;
                double len = reader.BaseStream.Length;
                double val = 100 * (pos / len);
                OnOpenProgressEvent(new OpenProgressEventArgs(val));
                //
                ICustomBitmapItem customBitmap = BitmapList.AddBitmap();
                p1 = reader.ReadInt32();
                p2 = reader.ReadInt32();
                //LE PROPRIEDADES
                customBitmap.SetGUID(Guid.NewGuid());
                customBitmap.Name = reader.ReadSizeString();    //1-nome
                customBitmap.Position = reader.ReadInt32();   //2-ponteiro no arquivo
                customBitmap.Size = reader.ReadInt32();   //3-tamanho em bytes                               
                reader.ReadInt32(); //4-CRC32                
                //FIM                
                OnMessageEvent(new MessageEventArgs(string.Format("Open bitmap {0}, size{1}", customBitmap.Name, customBitmap.Size)));
            }
        }
        /*!
         * 
         */
        private static void OpenFontList(ICustomFontHMIList FontList)
        {
            int p1, p2;
            p1 = reader.ReadInt32();
            FontList.Name = reader.ReadSizeString();
            int count = reader.ReadInt32();
            while (FontList.ObjectList.Count < count)
            {
                ICustomFontHMI font = FontList.NewFont();
                p1 = reader.ReadInt32();
                p2 = reader.ReadInt32();
                //LE PROPRIEDADES
                reader.ReadSizeString();//1
                font.Name = reader.ReadSizeString();    //2-nome da font
                font.Size= reader.ReadInt32();
                font.Bold = reader.ReadBoolean();
                font.Italic = reader.ReadBoolean();
                font.Underline = reader.ReadBoolean();
                font.Strikeout = reader.ReadBoolean();
                font.Full = reader.ReadBoolean();
                //FIM                
                OnMessageEvent(new MessageEventArgs(string.Format("Open font {0}, size {1}", font.Name,font.Size)));
            }
        }
    }
}
