using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADAStudioLibrary.Src.Visuals;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src
{
    public class CDesignScreenList : CDesignCustomScreenList, ICustomDefaultScreenList
    {
        
        /*!
         * Construtor
         * A variavel customScreens não deve ser instanciada, ela sera
         * setada no construtor do projeto.
         * @param AOwner
         * @param Project         
         */
        public CDesignScreenList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("Screens");
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
            CDesignScreen screen;
            while (ObjectList.Count > 0)
            {
                screen = (CDesignScreen)ObjectList[ObjectList.Count - 1];
                screen.Dispose();
            }
        }        

        [ActionProperty]
        [Category("Size")]
        public int Width
        {
            get { return this.width; }
            set { this.SetWidth(value); }
        }
        [ActionProperty]
        [Category("Size")]
        public int Height
        {
            get { return this.height; }
            set { this.SetHeight(value); }
        }
        /*!
         * Seta largura e atualiza em todas as telas
         * @param Value Nova largura
         */
        private void SetWidth(int Value)
        {
            this.width = Value;
            for (int i = 0; i < ObjectList.Count; i++)
            {
                CDesignScreen screen = (CDesignScreen)ObjectList[i];
                screen.Width = this.width;
            }
        }
        /*!
         * Seta altura e atualiza em todas as telas
         * @param Value Nova largura
         */
        private void SetHeight(int Value)
        {
            this.height = Value;
            for (int i = 0; i < ObjectList.Count; i++)
            {
                CDesignScreen screen = (CDesignScreen)ObjectList[i];
                screen.Height = this.height;
            }
        }
        /*!
         * Adiciona nova tela retornando referencia para interface.
         * Esta função é usada nas rotinas save e open
         */
        public override ICustomScreen NewScreen()
        {
            CDesignScreen screen = new CDesignScreen(this, project);            
            ObjectList.Add(screen);
            OnAddItem(new AddItemEventArgs(screen, screen.ImageIndex));            
            return screen;
        }
        /*!
         * Adiciona nova tela retornando referencia para objeto.
         * Esta função e chamada em runtime
         */
        public CDesignScreen AddScreenEx()
        {
            CDesignScreen screen =(CDesignScreen)NewScreen();
            screen.SetGUID(Guid.NewGuid());            
            screen.Name = "Screen" + ObjectList.Count.ToString();
            screen.Width = this.width;
            screen.Height = this.height;
            return screen;
        } 
        /*!
         * Faz link dos objetos na tela.
         */
        public override void LinkObjects()
        {
            foreach (CDesignCustomScreen screen in ObjectList)            
            {                
                screen.Width = this.width;
                screen.Height = this.height;
                screen.LinkObjects();
            }
        }       
    }
}
