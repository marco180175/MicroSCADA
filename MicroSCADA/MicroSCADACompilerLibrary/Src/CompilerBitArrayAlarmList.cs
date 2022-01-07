using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;
namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerBitArrayAlarmList : CCompilerSystem, ICustomBitArrayAlarmList
    {
        public CCompilerBitArrayAlarmList(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }

        public ICustomBitArrayAlarmItem NewAlarmItem()
        {
            CCompilerBitArrayAlarmItem item;
            item = new CCompilerBitArrayAlarmItem(this, project);
            ObjectList.Add(item);
            return item;
        }
    }

    class CCompilerBitArrayAlarmItem : CCompilerSystem, ICustomBitArrayAlarmItem
    {
        private string[] message;
        public CCompilerBitArrayAlarmItem(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        {
            message = new string[3];
        }
        public int Alignment { get; set; }
        public string[] Message { get { return message; } }
    }
}
