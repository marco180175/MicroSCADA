using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADAStudioLibrary.Src.Visuals;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src
{
    /*!
     * Lista de telas popup
     */
    public class CDesignPopupScreenList : CDesignCustomScreenList
    {        
        /*!
         * Construtor       
         * @param AOwner
         * @param Project         
         */
        public CDesignPopupScreenList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.Name = "PopupScreens";
            this.imageIndex = 9;
        }
        //!
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        /*!
         * Deleta todas as telas e seta parametros defalut
         */
        public override void Clear()
        {
            CDesignPopupScreen screen;
            while (ObjectList.Count > 0)
            {
                screen = (CDesignPopupScreen)ObjectList[ObjectList.Count - 1];
                screen.Dispose();
            }
        }
        /*!
         * 
         */
        public override ICustomScreen NewScreen()
        {            
            CDesignPopupScreen screen;

            screen = new CDesignPopupScreen(this, project);            
            ObjectList.Add(screen);
            OnAddItem(new AddItemEventArgs(screen, screen.ImageIndex));
            return screen;
        }
        /*!
         * 
         */
        public CDesignPopupScreen NewScreenEx()
        {
            CDesignPopupScreen screen;

            screen = (CDesignPopupScreen)NewScreen();
            screen.SetGUID(Guid.NewGuid());            
            screen.Name = "PopupScreen" + ObjectList.Count.ToString();
            return screen;
        }         
    }
}
