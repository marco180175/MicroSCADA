using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
//using MicroSCADAStudioLibrary.Src.EnvironmentDesigner;

namespace MicroSCADAStudioLibrary.Src
{
    public abstract class CDesignCustomScreenList : CDesignSystem, ICustomScreenList, IPageAdapter
    {
        protected int width;
        protected int height;        
        /*!
         * Construtor
         */
        public CDesignCustomScreenList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {            
            this.width = 640;
            this.height = 480;
        }
        /*!
         *Destructor
         */
        ~CDesignCustomScreenList()
        {
        }

        public abstract ICustomScreen NewScreen();

        public abstract void Clear();
        
        public event ShowItemEventHandler ShowItem;//ignorar
    }
}
