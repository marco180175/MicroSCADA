using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    public class CDesignScreen : CDesignBaseScreen
    {        
        public CDesignScreen(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {            
            this.imageIndex = 15;        
        }
        
        
        [Category("Size"), ReadOnly(true)]        
        new public int Width
        {
            get { return this.getWidth(); }
            set { this.setWidth(value); }
        }
        [Category("Size"), ReadOnly(true)]
        new public int Height
        {
            get { return this.getHeight(); }
            set { this.setHeight(value); }
        }                
    }
}
