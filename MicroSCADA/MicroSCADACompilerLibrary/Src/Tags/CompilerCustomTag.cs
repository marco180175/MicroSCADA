using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADACompilerLibrary.Src.Tags
{
    abstract class CCompilerCustomTag : CCompilerSystem, ICustomTag
    {
        protected CCustomTag customTag;//!<  
        public CCompilerCustomTag(Object AOwner, CCompilerProject Project)
            : base(AOwner, Project)
        {
            this.customTag = new CCustomTag();
        }
        //!
        public CCustomDataType DataType
        {
            get { return this.customTag.dataType; }
            set { customTag.dataType=value; }
        }
        //!
        public string Value { get; set; }
        //! Interface para alarme
        public ICustomAlarm AlarmHi
        {
            get { return null; }
        }
        //! Interface para alarme
        public ICustomAlarm AlarmLo
        {
            get { return null; }
        }
    }
}
