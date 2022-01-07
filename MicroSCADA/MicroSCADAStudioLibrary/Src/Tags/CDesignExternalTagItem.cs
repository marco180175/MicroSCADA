using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
//using MicroSCADAStudioLibrary.Src.EnvironmentDesigner;

namespace MicroSCADAStudioLibrary.Src.Tags
{
    public class CDesignExternalTagItem : CDesignCustomTag, ICustomExternalTagItem
    {
        private CDesignExternalTag externalTag;
        /*!
         * Construtor
         */
        public CDesignExternalTagItem(Object AOwner, CDesignProject Project)
            : base(AOwner,Project)
        {
            this.externalTag = (CDesignExternalTag)Owner;            
            //this.SetNodeImageIndex(39);            
        }
        [ReadOnly(true)]
        new public string Name
        {
            get
            {
                int index = externalTag.ObjectList.IndexOf(this);
                return String.Format("{0}[{1}]", externalTag.Name, index);
            }            
        }
        new public void SetName(String Value)
        {
            OnSetObjectName(new SetNameEventArgs(Value));
        }
        [Browsable(false)]
        public int Count
        {
            get { return ObjectList.Count; }
        }
        [Browsable(false)]
        new public CCustomDataType DataType
        {
            get { return this.customTag.dataType; }
            set { this.customTag.dataType = value; }
        }
        [Browsable(false)]
        public int Scan
        {
            get { return this.customTag.scanTime; }
            set { this.customTag.scanTime = value; }
        }
        [Browsable(false)]
        public Boolean Enabled
        {
            get { return this.customTag.enabled; }
            set { this.customTag.enabled = value; }
        }
        /*!
         * Subescreve ToString para retornar nome que sera mostrado no propertyGrid
         */
        public override string ToString()
        {            
            return FullName+Name;
        }
    }
}
