using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
//using MicroSCADAStudioLibrary.Src.EnvironmentDesigner;

namespace MicroSCADAStudioLibrary.Src.Tags
{
    /*!
     * Tag de comunicação com dispositivo externo
     */
    public class CDesignExternalTag : CDesignCustomTag, ICustomExternalTag    
    {
        protected CCustomExternalTag customExternalTag; 
        private static int count = 0;        
        /*!
         * Construtor
         */
        public CDesignExternalTag(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customExternalTag = new CCustomExternalTag();
            this.customExternalTag.arraySize = 0;
            this.customTag.dataType = CCustomDataType.dtInt16;
            this.imageIndex = 38;
            this.UpdateSize();            
            count++;
        }

        public override void  Dispose()
        {            
            count--;
 	        base.Dispose();
        }

        public static int getCount()
        {
            return count;
        }
        //!
        [Browsable(false)]
        public CCustomExternalTag CustomExternalTag
        {
            get { return this.customExternalTag; }
        }

        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        public int Slave
        {
            get { return this.customExternalTag.slave; }
            
        }
        public int Address
        {
            get { return this.customExternalTag.address; }
            set { this.customExternalTag.address = value; }
        }
        public int Size
        {
            get { return this.customExternalTag.size; }            
        }
        public override CCustomDataType DataType
        {
            get { return this.customTag.dataType; }
            set { this.SetDataType(value); }
        }
        public int ArraySize
        {
            get { return this.customExternalTag.arraySize; }
            set { this.SetArraySize(value); }
        }
        public virtual int Scan
        {
            get { return this.customTag.scanTime; }
            set { this.customTag.scanTime = value; }
        }
        public virtual int TimeOut
        {
            get { return this.customTag.timeout; }
            set { this.customTag.timeout = value; }
        }
        public virtual Boolean Enabled
        {
            get { return this.customTag.enabled; }
            set { this.customTag.enabled = value; }
        }
        public void SetSlave(int Value)
        {
            customExternalTag.slave = Value; 
        }        
        /*!
         * 
         */
        protected override void SetName(string value)
        {              
            base.SetName(value);
            //Seta nome dos elementos do array de tags
            CDesignExternalTagItem tagItem;
            for (int i = 0; i < ObjectList.Count; i++)
            {
                tagItem = (CDesignExternalTagItem)ObjectList[i];
                tagItem.SetName(String.Format("{0}[{1}]", Name, i));
            }
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignExternalTag> subSet = Objects.OfType<CDesignExternalTag>();
            return subSet.ToArray();
        }        
        /*!
         * 
         */
        private void SetArraySize(int Value)
        {
            CDesignExternalTagItem tagItem;
            customExternalTag.arraySize = Value;
            
            if (Value < ObjectList.Count)
            {
                while (ObjectList.Count > Value)
                {
                    tagItem = (CDesignExternalTagItem)ObjectList[ObjectList.Count-1];
                    tagItem.Dispose();                   
                }
            }
            else
            {
                while (ObjectList.Count < Value)
                {
                    tagItem = new CDesignExternalTagItem(this, project);
                    ObjectList.Add(tagItem);
                    OnAddItem(new AddItemEventArgs(tagItem, 39));
                    tagItem.SetName(String.Format("{0}[{1}]",Name,ObjectList.Count-1));
                    tagItem.SetGUID(Guid.NewGuid());                    
                }
            }
            UpdateSize();
        }

        /*!
         * 
         */
        private void SetDataType(CCustomDataType Value)
        {
            customTag.dataType = Value;
            UpdateSize();
        }
        /*!
         * 
         */
        private void UpdateSize()
        {
            switch (DataType)
            {
                case CCustomDataType.dtBool:
                    customExternalTag.size = customExternalTag.arraySize / 8;
                    break;
                case CCustomDataType.dtByte:
                    customExternalTag.size = customExternalTag.arraySize ;
                    break;
                case CCustomDataType.dtUInt16:
                    customExternalTag.size = customExternalTag.arraySize * 2;
                    break;
                case CCustomDataType.dtUInt32:
                    customExternalTag.size = customExternalTag.arraySize * 4;
                    break;
            }
        }        
    }
}
