using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src
{
    public class CDesignActionList : CDesignSystem, ICustomActionList
    {
        private Dictionary<CCustomActionCode, string> m_dictionary;
        private static CDesignActionList instance = null;
        private CDesignActionList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            base.Name = "ActionList";
            imageIndex = 43;
            SetGUID(Guid.NewGuid());
            m_dictionary = new Dictionary<CCustomActionCode, string>();
            m_dictionary.Add(CCustomActionCode.NextScreen, "NextScreen()");
            m_dictionary.Add(CCustomActionCode.PrevScreen, "PrevScreen()");
            m_dictionary.Add(CCustomActionCode.ShowScreen, "ShowScreen({0})");
            m_dictionary.Add(CCustomActionCode.ShowPopup, "ShowPopup({0})");           
        }
        /*!
         * singleton
         */
        public static CDesignActionList getInstance(Object AOwner, CDesignProject Project)
        {
            if (instance == null)
                instance = new CDesignActionList(AOwner, Project);
            return instance;
        }

        public static CDesignActionList getInstance()
        {            
            return instance;
        }

        public override void Dispose()
        {
            m_dictionary.Clear();
            base.Dispose();
        }

        //!
        [ReadOnly(true)]
        new public string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        //!
        [Browsable(false)]
        public Dictionary<CCustomActionCode, string> Dictionary
        {
            get { return m_dictionary; }
        }
        /*!
         * 
         */
        public ICustomAction NewAction()
        {
            CDesignAction action = new CDesignAction(this, project);
            ObjectList.Add(action);            
            OnAddItem(new AddItemEventArgs(action, action.ImageIndex));
            return action;
        }
        /*!
         * 
         */
        public CDesignAction NewActionEx()
        {
            CDesignAction action = (CDesignAction)NewAction();
            action.SetGUID(Guid.NewGuid()); 
            action.Name = string.Format("Action{0}", ObjectList.Count);            
            return action;
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            foreach (CDesignAction action in ObjectList)            
                action.LinkObjects();            
        }
    }
}
