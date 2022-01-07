using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
//using MicroSCADAStudioLibrary.Src.EnvironmentDesigner;
using MicroSCADAStudioLibrary.Src.Tags;

namespace MicroSCADAStudioLibrary.Src
{
    public class CDesignGroupOfInternalTags : CDesignSystem, ICustomInternalTagList
    {        
        /*!
         * 
         */
        public CDesignGroupOfInternalTags(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {            
            this.InitializeObject();
            this.imageIndex = 32;            
        }
        //!
        //
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignDemoTag> DemoTagList
        {
            get { return this.GetValuesTagList(); }
        }
        //!
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignSRAMTag> SRAMTagList
        {
            get { return this.GetMemoryTagList(); }
        }
        //!
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignTimerTag> TimerTagList
        {
            get { return this.GetTimerTagList(); }
        }
        #if DEBUG
        [Browsable(true), Category("Debug")]
        #else
        [Browsable(false)]
        #endif
        public List<CDesignGroupOfInternalTags> GroupList
        {
            get { return this.GetGroupList(); }
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignGroupOfInternalTags> subSet = Objects.OfType<CDesignGroupOfInternalTags>();
            return subSet.ToArray();
        }     
        /*!
         * 
         */
        public void Clear()
        {
            while (ObjectList.Count > 0)
                ((CDesignSystem)ObjectList[ObjectList.Count-1]).Dispose();            
        }   
        /*!
         * 
         */
        public void InitializeObject()
        {
            this.SetGUID(Guid.NewGuid());            
        }
        /*!
         * 
         */
        private List<CDesignDemoTag> GetValuesTagList()        
        {            
            IEnumerable<CDesignDemoTag> subset = ObjectList.OfType<CDesignDemoTag>();
            return subset.ToList();           
        }
        //!
        //public int ValuesTagCount
        //{
        //    get
        //    {
        //        IEnumerable<CDesignDemoTag> subset = ObjectList.OfType<CDesignDemoTag>();
        //        return subset.Count();
        //    }           
        //}
        /*!
         * 
         */
        private List<CDesignSRAMTag> GetMemoryTagList()
        {
            IEnumerable<CDesignSRAMTag> subset = ObjectList.OfType<CDesignSRAMTag>();
            return subset.ToList();
        }
        /*!
         * 
         */
        private List<CDesignTimerTag> GetTimerTagList()
        {
            IEnumerable<CDesignTimerTag> subset = ObjectList.OfType<CDesignTimerTag>();
            return subset.ToList();
        }
        /*!
         * 
         */
        private List<CDesignGroupOfInternalTags> GetGroupList()
        {
            IEnumerable<CDesignGroupOfInternalTags> subset = ObjectList.OfType<CDesignGroupOfInternalTags>();
            return subset.ToList();
        }
        /*!
         * 
         */
        public ICustomDemoTag AddDemoTag()
        {
            CDesignDemoTag demoTag;

            demoTag = new CDesignDemoTag(this, project);
            ObjectList.Add(demoTag);            
            OnAddItem(new AddItemEventArgs(demoTag, demoTag.ImageIndex));            
            return demoTag;
        }
        /*!
         * 
         */
        public CDesignDemoTag AddDemoTagEx()
        {
            CDesignDemoTag demoTag;

            demoTag = (CDesignDemoTag)AddDemoTag();
            demoTag.SetGUID(Guid.NewGuid());            
            demoTag.Name = "DemoTag" + DemoTagList.Count.ToString();
            return demoTag;
        }        
        /*!
         * 
         */
        public ICustomSRAMTag AddSRAMTag()
        {
            CDesignSRAMTag sramTag;

            sramTag = new CDesignSRAMTag(this, project);
            ObjectList.Add(sramTag);            
            OnAddItem(new AddItemEventArgs(sramTag, sramTag.ImageIndex));            
            return sramTag;
        }
        /*!
         * 
         */
        public CDesignSRAMTag AddSRAMTagEx()
        {
            CDesignSRAMTag sramTag;

            sramTag = (CDesignSRAMTag)AddSRAMTag();            
            sramTag.SetGUID(Guid.NewGuid());            
            sramTag.Name = "SRAMTag" + SRAMTagList.Count.ToString();
            return sramTag;
        }
        /*!
         * 
         */
        public ICustomPropertyTag AddPropertyTag()
        {
            CDesignPropertyTag propertyTag = new CDesignPropertyTag(this, project);
            ObjectList.Add(propertyTag);
            OnAddItem(new AddItemEventArgs(propertyTag, propertyTag.ImageIndex));
            return propertyTag;
        }
        /*!
         * 
         */
        public CDesignPropertyTag AddPropertyTagEx(CDesignSystem Reference, string PropertyName)
        {
            CDesignPropertyTag propertyTag = (CDesignPropertyTag)AddPropertyTag();
            propertyTag.SetGUID(Guid.NewGuid());
            propertyTag.SetParams(Reference, PropertyName);
            return propertyTag;
        }
        /*!
         * 
         */
        public ICustomTimerTag NewTimerTag()
        {
            CDesignTimerTag timerTag = new CDesignTimerTag(this, project);
            ObjectList.Add(timerTag);
            OnAddItem(new AddItemEventArgs(timerTag, timerTag.ImageIndex));
            return timerTag;
        }
        /*!
         * 
         */
        public CDesignTimerTag NewTimerTagEx()
        {
            CDesignTimerTag timerTag = (CDesignTimerTag)NewTimerTag();
            timerTag.SetGUID(Guid.NewGuid());
            timerTag.Name = "TimerTag" + TimerTagList.Count.ToString();
            return timerTag;
        }
        /*!
         * 
         */
        public ICustomInternalTagList AddGroup()
        {
            CDesignGroupOfInternalTags group;

            group = new CDesignGroupOfInternalTags(this, project);
            ObjectList.Add(group);            
            OnAddItem(new AddItemEventArgs(group, group.ImageIndex));            
            return group;
        }
        /*!
         * 
         */
        public CDesignGroupOfInternalTags AddGroupEx(bool Select)
        {
            CDesignGroupOfInternalTags group;

            group = (CDesignGroupOfInternalTags)AddGroup();           
            group.Name = "Group" + GroupList.Count.ToString();
            group.Selected = Select;
            return group;
        }
    }

    public class CDesignInternalTagList : CDesignGroupOfInternalTags 
    {
        protected CCustomInternalTagList customTagList;
        
        /*!
         * 
         */
        public CDesignInternalTagList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {            
            this.customTagList = new CCustomInternalTagList();
            this.SetOpenName("InternalTags");
            this.InitializeObject();
            this.imageIndex = 13;
        }
        //!Modifica Name para apenas leitura
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        /*!
         * 
         */
        public ArrayList GetAllTags()
        {
            ArrayList list = new ArrayList();
            for (int j = 0; j < ObjectList.Count; j++)
            {
                if (ObjectList[j] is CDesignInternalTag)
                {
                    CDesignInternalTag internalTag = (CDesignInternalTag)ObjectList[j];
                    list.Add(internalTag);
                }
                else
                {
                    CDesignGroupOfInternalTags group = (CDesignGroupOfInternalTags)ObjectList[j];
                    GetAllTags(list, group.ObjectList);
                }
            }
            return list;
        }
        /*!
         * 
         */
        private void GetAllTags(ArrayList List,ArrayList GroupList)
        {
            for (int j = 0; j < GroupList.Count; j++)
            {
                if (GroupList[j] is CDesignInternalTag)
                {
                    CDesignInternalTag internalTag = (CDesignInternalTag)GroupList[j];
                    List.Add(internalTag);                        
                }
                else
                {
                    CDesignGroupOfInternalTags group = (CDesignGroupOfInternalTags)GroupList[j];
                    GetAllTags(List, group.ObjectList);
                }
            }            
        }
    }
}
