using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Tags
{
    public interface IRuntimeExternalTag : IRuntimeTag
    {
        IRuntimeExternalTag GetReference();
        String GetValue();
    }

    public class CRuntimeExternalTagItem : CRuntimeSystem, IRuntimeExternalTag, ICustomExternalTagItem
    {
        private CRuntimeExternalTag externalTag;
        private ArrayList fieldList;
        public CRuntimeExternalTagItem(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.externalTag = (CRuntimeExternalTag)AOwner;
            this.fieldList = new ArrayList();
        }
        public String Value
        {
            get { return this.GetValue(); }
            set { this.SetValue(value); }
        }
        public String GetValue()
        {
            int Index = externalTag.ObjectList.IndexOf(this);
            return externalTag.GetValue(Index);
        }
        public void SetValue(String Value)
        {
            int Index = externalTag.ObjectList.IndexOf(this);
            externalTag.SetValue(Value, Index);
        }
        public ArrayList FieldList
        {
            get { return this.fieldList; }
        }
        public IRuntimeExternalTag GetReference()
        {
            return this.externalTag;
        }
    }

    class CRuntimeExternalTag : CRuntimeCustomTag, IRuntimeExternalTag, ICustomExternalTag
    {
        private CCustomExternalTag customExternalTag;
        private string[] m_values;
        private static int count = 0;
        public CRuntimeSlave slaveOwner;
        public CRuntimeExternalTag(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customExternalTag = new CCustomExternalTag();
            this.m_values = new string[1];
            count++;
        }
        public CCustomExternalTag CustomExternalTag
        {
            get { return this.customExternalTag; }
        }
        //public override int Slave
        public int Slave
        {
            get { return this.customExternalTag.slave; }
            set { this.customExternalTag.slave = value; }
        }
        //public override int Address
        public int Address
        {
            get { return this.customExternalTag.address; }
            set { this.customExternalTag.address = value; }
        }
        public override int Size
        {
            get { return this.customExternalTag.size; }
            set { this.customExternalTag.size = value; }
        }
        public override int ArraySize
        {
            get { return this.customExternalTag.arraySize; }
            set { this.SetArraySize(value); }
        }
        //! Tipo de dado padrão IEC61131
        new public CCustomDataType DataType
        {
            get { return this.customTag.dataType; }
            set { this.customTag.dataType = value; }
        }
        //! Leitura ou Leitura e Escrita
        new public CTagType TagType
        {
            get { return this.customExternalTag.tagType; }
        }
        /*!
         * 
         */
        public override String GetValue(int Index)
        {
            return m_values[Index];
        }
        /*!
         * 
         */
        public override void SetValue(String Value, int Index)
        {
            m_values[Index] = Value;
            //UpdateFields();
            UpdateAlarms();
        }
        /*!
         * 
         */
        private void SetArraySize(int Value)
        {
            CRuntimeExternalTagItem tagItem;
            customExternalTag.arraySize = Value;                       
            while (ObjectList.Count < Value)
            {
                tagItem = new CRuntimeExternalTagItem(this, project);
                ObjectList.Add(tagItem);                
                //tagItem.SetName(String.Format("{0}[{1}]", Name, ObjectList.Count));
                tagItem.SetGUID(Guid.NewGuid());
            }
            
            UpdateSize();
        }
        private void UpdateSize()
        {
            switch (customTag.dataType)
            {
                case CCustomDataType.dtSByte:
                case CCustomDataType.dtByte:
                    customExternalTag.size = 1;
                    break;
                //case CCustomDataType.dtWORD:
                case CCustomDataType.dtInt16:
                case CCustomDataType.dtUInt16:
                    customExternalTag.size = 2;
                    break;
                //case CCustomDataType.dtDWORD:
                case CCustomDataType.dtInt32:
                case CCustomDataType.dtUInt32:
                case CCustomDataType.dtFloat32:
                //case CCustomDataType.dtTime:
                //case CCustomDataType.dtTimeOfDay:
                //case CCustomDataType.dtDate:
                //case CCustomDataType.dtDateTime:
                    customExternalTag.size = 4;
                    break;
                default:
                    customExternalTag.size = 0;
                    break;
            }
            if (customExternalTag.arraySize > 0)
            {
                customExternalTag.size *= customExternalTag.arraySize;
            }
        }

        public IRuntimeExternalTag GetReference()
        {
            return this;
        }        
        /*!
         * 
         */
        //protected delegate void FieldSetValueCallBack(String Value);
        //public override void UpdateFields()
        //{
        //    SetField(this);
        //    for (int I = 0; I < ObjectList.Count; I++)
        //    {
        //        CRuntimeExternalTagItem tagItem = (CRuntimeExternalTagItem)ObjectList[I];
        //        SetField(tagItem);
        //    }
        //}
        /*!
         * 
         */
        private void SetField(IRuntimeExternalTag ExternalTag)
        {
            for (int I = 0; I < ExternalTag.FieldList.Count; I++)
            {
                //CRuntimeField viewerField = (CRuntimeField)ExternalTag.FieldList[I];
                //if (viewerField.getPictureBox().InvokeRequired)
                //{
                //    FieldSetValueCallBack setValueCallBack;
                //    //setValueCallBack = new FieldSetValueCallBack(viewerField.SetValue);
                //    setValueCallBack = new FieldSetValueCallBack(UpdateFields);
                //    viewerField.getPictureBox().Invoke(setValueCallBack, null);
                //}
                //else
                //{
                //    viewerField.SetValue(ExternalTag.GetValue());
                //}
            }
        }
    }
}
