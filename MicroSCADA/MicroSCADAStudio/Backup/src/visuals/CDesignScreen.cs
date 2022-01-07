using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using MicroSCADACustomLibrary;

namespace MicroSCADAStudio.Src.Visuals
{
    public class CDesignScreen : CDesignBaseScreen
    {        
        public CDesignScreen(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {            
            this.imageIndex = 15;
            this.InitializeFunctions();
        }
        
        protected override void InitializeFunctions()
        {
            this.functions = new Dictionary<CCustomActionCode, string>();
            this.functions.Add(CCustomActionCode.ScreenX_Show, "Show()");            
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
