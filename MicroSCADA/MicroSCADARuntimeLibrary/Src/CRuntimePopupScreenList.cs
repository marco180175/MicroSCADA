using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimePopupScreenList : CRuntimeCustomScreenList 
    {
        private IWin32Window m_owner;
        /*!
         * Construtor
         */
        public CRuntimePopupScreenList(Object AOwner, CRuntimeProject Project, IWin32Window Owner)
            : base(AOwner, Project)
        {
            this.m_owner = Owner;
        }
        /*!
         * 
         */
        public override ICustomScreen NewScreen()
        {
            CRuntimePopupScreen screen;

            screen = new CRuntimePopupScreen(this, project, m_owner);
            ObjectList.Add(screen);            
            return screen;
        }
        
    }
}
