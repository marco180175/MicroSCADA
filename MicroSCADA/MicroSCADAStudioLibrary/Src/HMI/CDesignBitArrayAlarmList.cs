using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src.HMI
{
    class CDesignBitArrayAlarmList : CDesignSystem, ICustomBitArrayAlarmList
    {
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         */
        public CDesignBitArrayAlarmList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("BitArrayList");
            this.imageIndex = -1;
        }
        
        public ICustomBitArrayAlarmItem NewAlarmItem()
        {            
            CDesignBitArrayAlarmItem item = new CDesignBitArrayAlarmItem(this, project);
            ObjectList.Add(item);
            return item;
        }
    }

    class CDesignBitArrayAlarmItem : CDesignSystem, ICustomBitArrayAlarmItem
    {
        private string[] message;
        public CDesignBitArrayAlarmItem(Object AOwner, CDesignProject Project) :
            base(AOwner, Project)
        {
            message = new string[3];
        }
        public int Alignment { get; set; }
        public string[] Message { get { return message; } }
    }
}
