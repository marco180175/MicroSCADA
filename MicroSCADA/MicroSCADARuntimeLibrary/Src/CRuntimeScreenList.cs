using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADARuntimeLibrary.Src.Visuals;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
//using MicroSCADARuntimeLibrary.Src.Threads;


namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeScreenList : CRuntimeCustomScreenList, ICustomDefaultScreenList
    {
        protected CCustomScreenList customScreens;
        protected Control parentControl;
        //protected CCommunicationManager commManager;
        private int currentIndex;
        //public CRuntimeScreens(Object AOwner, CRuntimeProject Project, Control Parent, CCommunicationManager CommManager)
        public CRuntimeScreenList(Object AOwner, CRuntimeProject Project, Control Parent)
            : base(AOwner, Project)
        {
            this.customScreens = new CCustomScreenList();            
            this.parentControl = Parent;
            this.parentControl.BackgroundImageLayout = ImageLayout.None;
            //this.commManager = CommManager;
            this.currentIndex = 0;
        }

        public int CurrentIndex
        {
            get { return this.currentIndex; }
        }

        public CRuntimeScreen CurrentScreen
        {
            get 
            {
                if (ObjectList.Count > 0)
                    return (CRuntimeScreen)ObjectList[currentIndex];
                else
                    return null;
            }
        }

        public override ICustomScreen NewScreen()
        {
            CRuntimeScreen screen;

            screen = new CRuntimeScreen(this, project);            
            ObjectList.Add(screen);
            screen.Name = "Screen" + ObjectList.Count.ToString();
            screen.Enter += new EventHandler(screen_Enter);
            screen.FieldEnter += new EventHandler(screen_FieldEnter);
            return screen;
        }
        /*!
         * Mostra tela apontada pelo index.
         * @param index Indice da tela que sera mostrada.
         */
        public void ShowScreen(int index)
        {
            CRuntimeScreen screen;
            //if (index != currentIndex)
            //{
                if ((currentIndex >= 0) && (currentIndex < ObjectList.Count))
                {
                    screen = (CRuntimeScreen)ObjectList[currentIndex];
                    screen.setParent(null);
                    //
                    currentIndex = index;
                    //
                    screen = (CRuntimeScreen)ObjectList[currentIndex];
                    screen.setParent(parentControl);
                }
            //}
        }
        /*!
         * Mostra proxima tela.
         */
        public void ShowNextScreen()
        {
            if (currentIndex < ObjectList.Count - 1)
            {
                ShowScreen(currentIndex + 1);
            }
        }
        /*!
         * Mostra tela anterior.
         */
        public void ShowPrevScreen()
        {
            if (currentIndex > 0)
            {
                ShowScreen(currentIndex - 1);
            }
        }
        public event EventHandler FieldEnter;
        private void screen_FieldEnter(object sender, EventArgs e)
        {
            if (FieldEnter != null)
                FieldEnter(sender, e);
        }
        public event EventHandler ScreenEnter;
        private void screen_Enter(object sender, EventArgs e)
        {
            if (ScreenEnter != null)
                ScreenEnter(sender, e);
        }
        /*!
         * 
         */
        public void LinkObjects()
        {
            foreach (CRuntimeScreen screen in ObjectList)
            {                
                screen.LinkObjects();
                
            }
        }
    }
}
