using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MicroSCADARuntime.Src.Threads
{
    abstract class CThreadTags
    {
        protected Thread thread;
        protected ArrayList tagList;
        protected int index;
        //protected CRuntimeCustomTag variable;
        protected Boolean loop;        
        //protected delegate void FieldSetValueCallBack(String Value);
        public CThreadTags()
        {
            this.thread = null;
            this.tagList = new ArrayList();
            this.index = 0;
            this.loop = false;
        }

        public abstract void Start();

        public abstract void Stop();
        
        public Boolean IsRunning
        {
            get { return this.loop; }
        }
        public virtual void SetTagList(ArrayList Value)
        {
            this.tagList = Value;
        }
        protected abstract void Execute();
        //protected abstract void UpdateVariable();
    }
}
