using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using MicroSCADACustomLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Tags
{
    /*!
     * 
     */
    public interface IRuntimeTag
    {
        //event TagSetValueEventHandler SetValueEvent;
        //void RegisterField(IRuntimeField Field);
        ArrayList FieldList { get; }        
    }
    /*!
     * Classe abstrata para implementação de todos os tags
     */
    public abstract class CRuntimeCustomTag : CRuntimeSystem, ICustomTag, IRuntimeTag 
    {
        protected MemoryStream memoryStream;//!< Stream para manipular o buffer
        protected BinaryReader binaryReader;//!< Objeto de leitura
        protected BinaryWriter binaryWriter;//!< Objeto de escrita
        protected CCustomTag customTag;//!<         
        protected CRuntimeAlarm alarmHi;//!< Alarme Auto
        protected CRuntimeAlarm alarmLo;//!< Alarme Baixo        
        protected ArrayList fieldList;//!< Lista de campos que usam o tag
        //protected String m_value;//!< Valor do tag
        protected int m_stringSize;
        public bool forceWrite=false;        
        /*!
         * Construtor
         * @param AOwner Referencia para proprietario
         * @param Project Referencia para projeto
         */
        public CRuntimeCustomTag(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {            
            this.customTag = new CCustomTag();
            this.fieldList = new ArrayList();
            this.memoryStream = new MemoryStream();
            this.customTag.dataType = CCustomDataType.dtInt32;
            this.memoryStream.SetLength(GetSizeOf());
            this.binaryReader = new BinaryReader(this.memoryStream);
            this.binaryWriter = new BinaryWriter(this.memoryStream);            
            this.alarmHi = new CRuntimeAlarm(this, Project);
            this.alarmHi.Name = "AlarmHi";
            this.alarmLo = new CRuntimeAlarm(this, Project);
            this.alarmLo.Name = "AlarmLo";            
        }
        #region Propriedades

        public string Value
        {
            get { return GetValue(); }
            set 
            { 
                SetValue(value);
                //UpdateFields();
                //UpdateAlarms();
            }
        }

        //! Tamanho do tag em bytes
        public virtual int Size { get; set; }
        
        //! Numero de elementos se tag array
        public virtual int ArraySize { get; set; }

        //! Tipo de tag demo
        //public virtual CDemoType Type { get; set; }       

        //! Tipo de dado padrão IEC61131
        public CCustomDataType DataType 
        {
            get { return this.customTag.dataType; }
            set { this.customTag.dataType = value; }
        }
        //! Hablilita comunicação ou atualização
        public Boolean Enabled
        {
            get { return this.customTag.enabled; }
            set { this.customTag.enabled = value; }
        }
        //! Armazena numero de ticks a cada comunicação
        public int TickCount
        {
            get { return this.customTag.tickCount; }
            set { this.customTag.tickCount = value; }
        }
        //! Endereço do tag na rede
        public int Scan
        {
            get { return this.customTag.scanTime; }
            set { this.customTag.scanTime = value; }
        }
        //! Tempo de timeout (espera resposta da slave)
        public int TimeOut
        {
            get { return this.customTag.timeout; }
            set { this.customTag.timeout = value; }
        }
        //! Lista de campos
        public ArrayList FieldList
        {
            get { return this.fieldList; }
        }
        //! Tipo Leitura ou Leitura e Escrita
        public CTagType TagType
        {
            get { return CTagType.ReadWrite; }
        }
        
        //! Interface para alarme
        public ICustomAlarm AlarmHi
        {
            get { return this.alarmHi; }
        }
        //! Interface para alarme
        public ICustomAlarm AlarmLo
        {
            get { return this.alarmLo; }
        }
        #endregion
        /*!
         * Tamanho do tipo em bytes
         */
        public int GetSizeOf()
        {
            switch (customTag.dataType)
            {
                case CCustomDataType.dtBool:
                    return sizeof(bool);
                case CCustomDataType.dtSByte:
                    return sizeof(sbyte);
                case CCustomDataType.dtByte:
                    return sizeof(byte);
                case CCustomDataType.dtInt16:
                    return sizeof(Int16);
                case CCustomDataType.dtUInt16:
                    return sizeof(UInt16);
                //case CCustomDataType.dtWORD:                    
                case CCustomDataType.dtInt32:
                    return sizeof(Int32);
                case CCustomDataType.dtUInt32:
                    return sizeof(UInt32);
                case CCustomDataType.dtInt64:
                    return sizeof(Int64);
                case CCustomDataType.dtUInt64:
                    return sizeof(UInt64);
                //case CCustomDataType.dtDWORD:                    
                case CCustomDataType.dtFloat32:
                    return sizeof(float);
                case CCustomDataType.dtFloat64:
                    return sizeof(double);
                case CCustomDataType.dtTimer:
                case CCustomDataType.dtTime:
                case CCustomDataType.dtDate:
                case CCustomDataType.dtDateTime:
                    return sizeof(Int64);
                case CCustomDataType.dtChar:
                    return sizeof(char);
                case CCustomDataType.dtString:
                    return m_stringSize; 
                default:
                    return 0;
            }
        }
        /*!
         * Retorna valor do tag
         * @param Index indice do valor
         * @return Valor
         */
        public virtual string GetValue(int Index)
        {
            binaryReader.BaseStream.Position = Index * GetSizeOf();
            switch (customTag.dataType)
            {
                case CCustomDataType.dtBool:
                    return binaryReader.ReadBoolean().ToString();
                case CCustomDataType.dtByte:
                    return binaryReader.ReadByte().ToString();
                case CCustomDataType.dtSByte:
                    return binaryReader.ReadSByte().ToString();
                case CCustomDataType.dtInt16:
                    return binaryReader.ReadInt16().ToString();
                case CCustomDataType.dtUInt16:
                    return binaryReader.ReadUInt16().ToString();
                //case CCustomDataType.dtWORD:
               //     return binaryReader.ReadUInt16().ToString("X4");
                case CCustomDataType.dtInt32:
                    return binaryReader.ReadInt32().ToString();
                case CCustomDataType.dtUInt32:
                    return binaryReader.ReadUInt32().ToString();
                case CCustomDataType.dtInt64:
                    return binaryReader.ReadInt64().ToString();
                case CCustomDataType.dtUInt64:
                    return binaryReader.ReadUInt64().ToString();
                //case CCustomDataType.dtDWORD:
                //    return binaryReader.ReadUInt32().ToString("X8");
                case CCustomDataType.dtFloat32:
                    return binaryReader.ReadSingle().ToString();
                case CCustomDataType.dtFloat64:
                    return binaryReader.ReadDouble().ToString();
                case CCustomDataType.dtTimer:
                case CCustomDataType.dtTime:
                case CCustomDataType.dtDate:
                case CCustomDataType.dtDateTime:
                    return binaryReader.ReadInt64().ToString();
                case CCustomDataType.dtChar:
                    return binaryReader.ReadChar().ToString();
                case CCustomDataType.dtString:
                    char[] charArray = binaryReader.ReadChars(m_stringSize);
                    return new string(charArray);
                default:
                    return "?????";
            }            
        }
        /*!
         * Seta valor do tag 
         * @return Valor
         */
        public virtual string GetValue()
        {
            return GetValue(0);
        }
        /*!
         * Seta valor do tag 
         * e dipara evento para atualização dos campos
         * @param Value
         */
        public virtual void SetValue(string Value)
        {
            //gambiarra
            //if (Value == "")
            //    Value = "0";
            //if (Value.ToLower() == "false")
            //    Value = "0";            
            //
            try
            {
                SetValue(Value, 0);
                OnSetValue(new TagSetValueEventArgs(Value, DataType));
            }
            catch
            {
                //OnSetValueError(new EventArgs(message));
            }
        }
        /*!
         * Seta valor do tag 
         * @param Value
         * @param Index
         */
        public virtual void SetValue(string Value, int Index)
        {
            binaryWriter.BaseStream.Position = Index * GetSizeOf();
            switch (DataType)
            {
                case CCustomDataType.dtBool:
                    binaryWriter.Write(bool.Parse(Value));
                    break;
                case CCustomDataType.dtByte:
                    binaryWriter.Write(byte.Parse(Value));
                    break;
                case CCustomDataType.dtInt16:
                    binaryWriter.Write(Int16.Parse(Value));
                    break;
                case CCustomDataType.dtUInt16:
                    binaryWriter.Write(UInt16.Parse(Value));
                    break;
                //case CCustomDataType.dtWORD:
                //    binaryWriter.Write(UInt16.Parse(Value, NumberStyles.AllowHexSpecifier));
                //    break;
                case CCustomDataType.dtInt32:
                    binaryWriter.Write(Int32.Parse(Value));
                    break;
                case CCustomDataType.dtUInt32:
                    binaryWriter.Write(UInt32.Parse(Value));
                    break;
                //case CCustomDataType.dtDWORD:
                //    binaryWriter.Write(UInt32.Parse(Value, NumberStyles.AllowHexSpecifier));
                //    break;
                case CCustomDataType.dtInt64:
                    binaryWriter.Write(long.Parse(Value));
                    break;
                case CCustomDataType.dtUInt64:
                    binaryWriter.Write(ulong.Parse(Value));                    
                    break;
                case CCustomDataType.dtFloat32:
                    binaryWriter.Write(float.Parse(Value));
                    break;
                case CCustomDataType.dtFloat64:
                    binaryWriter.Write(double.Parse(Value));
                    break;
                case CCustomDataType.dtTimer:
                case CCustomDataType.dtTime:
                case CCustomDataType.dtDate:
                case CCustomDataType.dtDateTime:
                    binaryWriter.Write(long.Parse(Value));
                    break;
                case CCustomDataType.dtChar:                    
                    binaryWriter.Write(char.Parse(Value));
                    break;
                case CCustomDataType.dtString:
                    char[] charArray = Value.ToCharArray();
                    m_stringSize = charArray.Length;
                    binaryWriter.Write(charArray);
                    break;
                default:
                    //this.value = Value;
                    break;
            }
        }
        /*!
         * 
         * @param Value
         */
        public virtual void PutValue(string Value)
        {            
            SetValue(Value, 0);  
            forceWrite = true;
        }
      
        /*!
         * Atualiza valor do tag nos campos
         */
        //public virtual void UpdateFields()
        //{
        //    //    foreach (object objFld in fieldList)
        //    //    {
        //    //        IRuntimeField field = (IRuntimeField)objFld;
        //    //        UpdateField(field);
        //    //    }
        //}
        
        
        /*!
         * Atualiza status dos alarmes
         */
        protected void UpdateAlarms()
        {
            if (alarmHi.Enabled)
            {
                float currentValue = float.Parse(Value);
                if (currentValue > alarmHi.Value)
                {
                    if (alarmHi.State == 0)
                    {
                        alarmHi.State = 1;
                        CRuntimeAlarmsManager alarms = (CRuntimeAlarmsManager)project.AlarmsManager;
                        alarms.Register(alarmHi,false);
                    }
                }
                else
                {//retorno do alarme
                    if (alarmHi.State == 1)
                    {
                        alarmHi.State = 0;
                        CRuntimeAlarmsManager alarms = (CRuntimeAlarmsManager)project.AlarmsManager;
                        alarms.Register(alarmHi, true);
                    }
                }
            }
            if (alarmLo.Enabled)
            {
                float currentValue = float.Parse(Value);
                if (currentValue < alarmLo.Value)
                {
                    if (alarmLo.State == 0)
                    {
                        alarmLo.State = 1;                        
                        ((CRuntimeAlarmsManager)project.AlarmsManager).Register(alarmLo, false);
                    }                    
                }
                else
                {//retorno do alarme
                    if (alarmLo.State == 1)
                    {
                        alarmLo.State = 0;
                        CRuntimeAlarmsManager alarms = (CRuntimeAlarmsManager)project.AlarmsManager;
                        alarms.Register(alarmLo,true);
                    } 
                }
            }
        }
        //
        

        //!
        public event TagSetValueEventHandler SetValueEvent;
        /*!
         * 
         */
        protected void OnSetValue(TagSetValueEventArgs e)
        {
            if (SetValueEvent != null)
                SetValueEvent(this, e);
        }
        /*!
         * TODO:possibilidade de trocar por interface
         */
        public void RegisterField(CRuntimeCustomField Field)
        {
            Field.EditValueEvent += new FieldEditValueEventHandler(field_EditValue);
        }
        /*!
         * 
         */
        private void field_EditValue(object sender, FieldEditValueEventArgs e)
        {
            PutValue(e.Value);
            OnSetValue(new TagSetValueEventArgs(e.Value, DataType));            
        }       
    }
}
