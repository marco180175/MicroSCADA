using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src.HMI
{
    class CDesignBitArrayList: CDesignSystem, ICustomBitArrayList
    {
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         */
        public CDesignBitArrayList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("BitArrayList");
            this.imageIndex = -1;
        }
        //!
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }

        public ICustomBitArrayTriggerList NewTriggerList()
        {
            CDesignBitArrayTriggerList triggerList = new CDesignBitArrayTriggerList(this, project);
            ObjectList.Add(triggerList);
            return triggerList;
        }

        public ICustomBitArrayAlarmList NewAlarmList()
        {
            CDesignBitArrayAlarmList alarmList = new CDesignBitArrayAlarmList(this, project);
            ObjectList.Add(alarmList);
            return alarmList;
        }
    }
}
