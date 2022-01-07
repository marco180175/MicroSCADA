using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerBitArrayList:CCompilerSystem,ICustomBitArrayList
    {
        public CCompilerBitArrayList(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }

        public ICustomBitArrayTriggerList NewTriggerList()
        {
            CCompilerBitArrayTriggerList triggerList = new CCompilerBitArrayTriggerList(this, project);
            ObjectList.Add(triggerList);
            return triggerList;
        }

        public ICustomBitArrayAlarmList NewAlarmList()
        {
            CCompilerBitArrayAlarmList alarmList = new CCompilerBitArrayAlarmList(this, project);
            ObjectList.Add(alarmList);
            return alarmList;
        }
    }
}
