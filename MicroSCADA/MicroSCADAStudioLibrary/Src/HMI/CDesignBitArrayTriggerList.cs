using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src.HMI
{
    class CDesignBitArrayTriggerList: CDesignSystem, ICustomBitArrayTriggerList
    {
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         */
        public CDesignBitArrayTriggerList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("BitArrayList");
            this.imageIndex = -1;
        }
        public int Slave { get; set; }
        public int Address { get; set; }
        public ICustomBitArrayTriggerItem NewTriggerItem()
        {
            CDesignBitArrayTriggerItem item;
            item = new CDesignBitArrayTriggerItem(this, project);
            ObjectList.Add(item);
            return item;
        }
        public ICustomBitArrayTriggerProgram NewTriggerProgram()
        {
            CDesignBitArrayTriggerProgram item;
            item = new CDesignBitArrayTriggerProgram(this, project);
            ObjectList.Add(item);
            return item;
        }

    }

    class CDesignBitArrayTriggerItem : CDesignSystem, ICustomBitArrayTriggerItem
    {
        public CDesignBitArrayTriggerItem(Object AOwner, CDesignProject Project) :
            base(AOwner, Project)
        { }
    }

    class CDesignBitArrayTriggerProgram : CDesignSystem, ICustomBitArrayTriggerProgram
    {
        public CDesignBitArrayTriggerProgram(Object AOwner, CDesignProject Project) :
            base(AOwner, Project)
        { }
    }
}
