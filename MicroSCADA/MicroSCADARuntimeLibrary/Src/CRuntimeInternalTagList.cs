using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeInternalTagList : CRuntimeSystem, ICustomInternalTagList
    {
        protected CCustomInternalTagList customTagList;
        
        public CRuntimeInternalTagList(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {            
            this.customTagList = new CCustomInternalTagList();              
        }

        public ICustomDemoTag AddDemoTag()
        {
            CRuntimeDemoTag demoTag;

            demoTag = new CRuntimeDemoTag(this, project);
            ObjectList.Add(demoTag);                      
            return demoTag;
        }
        public ICustomSRAMTag AddSRAMTag()
        {
            CRuntimeSRAMTag memoryTag;

            memoryTag = new CRuntimeSRAMTag(this, project);
            ObjectList.Add(memoryTag);            
            return memoryTag;
        }
        /*!
         * 
         */
        public ICustomTimerTag NewTimerTag()
        {
            CRuntimeTimerTag timerTag = new CRuntimeTimerTag(this, project);
            ObjectList.Add(timerTag);            
            return timerTag;
        }
        public ICustomInternalTagList AddGroup()
        {            
            return this;
        }
        
    }
}
