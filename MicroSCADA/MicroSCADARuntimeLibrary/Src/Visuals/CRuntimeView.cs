using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src
{
    public abstract class CRuntimeView : CRuntimeSystem, ICustomSize
    {
        public CRuntimeView(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
        }
        protected abstract int getLeft();
        protected abstract void setLeft(int Value);
        protected abstract int getTop();
        protected abstract void setTop(int Value);
        protected abstract int getWidth();
        protected abstract void setWidth(int Value);
        protected abstract int getHeight();
        protected abstract void setHeight(int Value);
        public int Left
        {
            get { return this.getLeft(); }
            set { this.setLeft(value); }
        }
        public int Top
        {
            get { return this.getTop(); }
            set { this.setTop(value); }
        }
        public int Width
        {
            get { return this.getWidth(); }
            set { this.setWidth(value); }
        }
        public int Height
        {
            get { return this.getHeight(); }
            set { this.setHeight(value); }
        }
    }
}
