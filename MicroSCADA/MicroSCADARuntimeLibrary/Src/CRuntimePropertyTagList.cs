using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimePropertyTagList : CRuntimeSystem, ICustomPropertyTagList
    {
        /*!
         * 
         */
        public CRuntimePropertyTagList(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("PropertyTagList");                        
        }
        /*!
         * 
         */
        public ICustomPropertyTag AddTag()
        {
            CRuntimePropertyTag propertyTag = new CRuntimePropertyTag(this, project);
            ObjectList.Add(propertyTag);            
            return propertyTag;
        }
        /*!
         * 
         */
        public void LinkObjects()
        {
            foreach (CRuntimePropertyTag propertyTag in ObjectList)
            {
                propertyTag.LinkObjects();
            }
        }     
    }
}
