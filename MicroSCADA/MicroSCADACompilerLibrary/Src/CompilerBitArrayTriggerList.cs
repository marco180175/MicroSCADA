using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;
namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerBitArrayTriggerList:CCompilerSystem,ICustomBitArrayTriggerList
    {
        public CCompilerBitArrayTriggerList(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }

        public int Slave { get; set; }

        public int Address { get; set; }

        public ICustomBitArrayTriggerItem NewTriggerItem()
        {
            CCompilerBitArrayTriggerItem item;
            item = new CCompilerBitArrayTriggerItem(this, project);
            ObjectList.Add(item);
            return item;
        }
        public ICustomBitArrayTriggerProgram NewTriggerProgram()
        {
            CCompilerBitArrayTriggerProgram item;
            item = new CCompilerBitArrayTriggerProgram(this, project);
            ObjectList.Add(item);
            return item;
        }
    }

    class CCompilerBitArrayTriggerItem : CCompilerSystem, ICustomBitArrayTriggerItem
    {
        public CCompilerBitArrayTriggerItem(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }
    }

    class CCompilerBitArrayTriggerProgram : CCompilerSystem, ICustomBitArrayTriggerProgram
    {
        public CCompilerBitArrayTriggerProgram(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }
    }
}
