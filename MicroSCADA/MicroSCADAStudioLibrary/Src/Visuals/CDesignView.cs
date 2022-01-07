using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{

    public abstract class CDesignView : CDesignSystem, ICustomSize
    {
        protected int width;//sem zoom
        protected int height;//sem zoom
        protected static float zoomScale = 1.0F;
        public static void setZoomScale(float value)
        {
            zoomScale = value;
        }
        //Construtor
        public CDesignView(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            
        }
        //Destrutor
        //~CDesignView()
        //{
        //    Dispose();
        //}

        public override void Dispose()
        {
            base.Dispose();
        }
                
        //protected override void SetName(string value)
        //{
        //    //CDesignSystem myOwner = (Owner as CDesignSystem);
        //    //if (myOwner == null)
        //    //{
        //    customObject.name = value;
        //    //}
        //    //else
        //    //{
        //    //    CDesignSystem[] objects = (CDesignSystem[])myOwner.ObjectList.ToArray();
        //    //    customObject.name = myOwner.GetNewName(value, objects);
        //    //}
        //    OnSetObjectName(new SetNameEvent.SetNameEventArgs(customObject.name));
        //}       

        protected abstract int getLeft();
        protected abstract void setLeft(int Value);
        protected abstract int getTop();
        protected abstract void setTop(int Value);
        protected abstract int getWidth();
        protected abstract void setWidth(int Value);
        protected abstract int getHeight();
        protected abstract void setHeight(int Value);
        [ActionProperty]
        [Category("Size")]
        public int Width
        {
            get { return this.getWidth(); }
            set { this.setWidth(value); }
        }
        [ActionProperty]
        [Category("Size")]
        public int Height
        {
            get { return this.getHeight(); }
            set { this.setHeight(value); }
        }
    }
}
