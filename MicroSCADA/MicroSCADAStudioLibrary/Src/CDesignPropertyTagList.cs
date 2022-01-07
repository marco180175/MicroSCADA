using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudioLibrary.Src
{
    public class CDesignPropertyTagList : CDesignSystem, ICustomPropertyTagList
    {
        /*!
         * 
         */
        public CDesignPropertyTagList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("PropertyTagList");
            this.imageIndex = 32;            
        }
        //! Modifica Name para apenas leitura
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        /*!
         * 
         */
        public ICustomPropertyTag AddTag()
        {
            CDesignPropertyTag propertyTag = new CDesignPropertyTag(this, project);
            ObjectList.Add(propertyTag);
            OnAddItem(new AddItemEventArgs(propertyTag, propertyTag.ImageIndex));
            return propertyTag;
        }
        /*!
         * 
         */
        public CDesignPropertyTag AddTagEx(CDesignSystem Reference, string PropertyName)
        {
            CDesignPropertyTag propertyTag = (CDesignPropertyTag)AddTag();
            propertyTag.SetGUID(Guid.NewGuid());
            propertyTag.SetParams(Reference, PropertyName);
            return propertyTag;
        }
        /*!
         * 
         */
        public void Clear()
        {
            while (ObjectList.Count > 0)
                ((CDesignPropertyTag)ObjectList[0]).Dispose();
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            foreach (CDesignPropertyTag propertyTag in ObjectList)
            {
                propertyTag.LinkObjects();
            }
        }     
    }
}
