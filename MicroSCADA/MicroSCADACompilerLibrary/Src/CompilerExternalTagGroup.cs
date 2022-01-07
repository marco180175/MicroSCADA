using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;
using MicroSCADACompilerLibrary.Src.Tags;

namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerTagSlaveGroup: CCompilerSystem, ICustomGroupTags
    {
        protected CCustomSlave customSlave;
        //public CRuntimeSlave slaveOwner;
        public CCompilerTagSlaveGroup(Object AOwner, CCompilerProject Project)
            : base(AOwner, Project)
        {
            this.customSlave = new CCustomSlave();
        }

        public virtual ICustomTag AddTag()
        {
            CCompilerExternalTag tag;

            tag = new CCompilerExternalTag(this, project);
            ObjectList.Add(tag);
            //tag.Name = "Tag" + ObjectList.Count.ToString();
            //tag.Slave = customSlave.address;
            //tag.slaveOwner = slaveOwner;
            return tag;
        }

        public virtual ICustomGroupTags AddGroup()
        {
            CCompilerTagSlaveGroup group;

            group = new CCompilerTagSlaveGroup(this, project);
            ObjectList.Add(group);
            //group.Name = "Group" + ObjectList.Count.ToString();
            //group.SetAddress(customSlave.address);
            //group.slaveOwner = slaveOwner;
            return group;
        }

        //public void SetAddress(int Value)
        //{
        //    this.customSlave.address = Value;
        //    for (int i = 0; i < ObjectList.Count; i++)
        //    {
        //        if (ObjectList[i] is CRuntimeTagGroup)
        //        {
        //            CRuntimeTagGroup tagGroup = (CRuntimeTagGroup)ObjectList[i];
        //            tagGroup.SetAddress(customSlave.address);
        //        }
        //        else
        //        {
        //            CRuntimeExternalTag extTag = (CRuntimeExternalTag)ObjectList[i];
        //            extTag.Slave = customSlave.address;
        //        }
        //    }
        //}
    }
}
