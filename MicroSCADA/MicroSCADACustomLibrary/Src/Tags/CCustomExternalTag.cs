using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MicroSCADACustomLibrary.Src
{
    public enum CTagType
    {
        Read,
        ReadWrite
    }

    public interface ICustomExternalTag : ICustomTag, IArrayOfTag, IControlOfTag
    {
        int Slave { get; }
        int Address { get; set; }        
        int TimeOut { get; set; }        
        CCustomExternalTag CustomExternalTag { get; }
    }

    public interface ICustomExternalTagItem
    {
        Guid GUID { get; }
        void SetGUID(Guid Value);
    }

    public class CCustomExternalTag : Object    
    {        
        public int slave;
        public int address;
        public int size;
        public int arraySize;
        public Boolean swap;
        public Boolean writeSingle;
        public CTagType tagType;
        /*!
         * Construtor
         */
        public CCustomExternalTag()
            : base()
        {                        
            this.address = 1;
            this.size = 2;
            this.arraySize = 0;
            this.writeSingle = false;
            this.tagType = CTagType.ReadWrite;
            this.swap = true;
        }       
    }
}
