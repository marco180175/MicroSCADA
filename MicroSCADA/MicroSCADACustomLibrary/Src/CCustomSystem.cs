using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{

    public interface ICustomRange
    {
        float MaxValue { get; set; }
        float MinValue { get; set; }
    }

    public class CReferenceItem
    {
        public Object Reference;
        public Guid GUID;
        public CReferenceItem()
        {
            this.Reference = null;
            this.GUID = Guid.Empty;
        }
    }

    public class CReferenceList : List<CReferenceItem>    
    {
        public int AddReference()
        {
            this.Add(new CReferenceItem());
            return this.Count - 1;
        }        
    }

    public interface ICustomSystem : ICustomObject
    {        
        CReferenceList ReferenceList { get; }
        void SetOpenName(string Value);
        void SetReference(int Index, Object Value);
        Object GetReference(int Index);
        void SetReferenceGuid(int Index, Guid Value);
        Guid GetReferenceGuid(int Index);
    }

    public sealed class CCustomSystem : IDisposable
    {
        public CReferenceList referenceList;

        public CCustomSystem()
        {            
            this.referenceList = new CReferenceList();
        }

        public void Dispose()
        {
            //this.referenceList = null;
        }
    }

    
}
