using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADACompilerLibrary.Src.Tags
{
    class CCompilerExternalTag: CCompilerCustomTag, ICustomExternalTag
    {
        private CCustomExternalTag customExternalTag;
        public CCompilerExternalTag(Object AOwner, CCompilerProject Project)
            : base(AOwner, Project)
        {
            this.customExternalTag = new CCustomExternalTag();
        }
        public CCustomExternalTag CustomExternalTag { get; }
                
        public int TimeOut { get; set; }
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
        public bool Enabled
        {
            get { return this.customTag.enabled; }
            set { this.customTag.enabled = value; }
        }
        public int Scan
        {
            get { return this.customTag.scanTime; }
            set { this.customTag.scanTime = value; }
        }
        public int ArraySize
        {
            get { return this.customExternalTag.arraySize; }
            set { this.customExternalTag.arraySize=value; }
        }
    }
}
