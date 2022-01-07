using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MicroSCADARuntimeLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntime.Src.Threads
{
    class CThreadInternalTags : CThreadTags
    {
        
        public CThreadInternalTags()
            : base()
        {
            
        }
        /*!
         * 
         */
        public override void Start()
        {
            this.loop = true;
            this.thread = new Thread(new ThreadStart(this.Execute));
            this.thread.IsBackground = true;
            this.thread.Start();
        }
        /*!
         * 
         */
        public override void Stop()
        {
            if (thread != null)
            {
                //Termina loop da thread
                loop = false;
                //Aguarda temino da thread
                thread.Join();
                thread = null;                
            }
        }
        /*!
         * 
         */
        protected override void Execute()
        {
            CRuntimeDinamicTag demoTag;
            while (loop)
            {
                if (tagList.Count > 0)
                {
                    if (tagList[index] is CRuntimeDinamicTag)
                    {
                        demoTag = (CRuntimeDinamicTag)tagList[index];
                        if (demoTag.Enabled)
                        {
                            if ((Environment.TickCount - demoTag.TickCount) > demoTag.Scan)
                            {
                                demoTag.TickCount = Environment.TickCount;
                                demoTag.DoStep();                                
                            }
                        }
                    }
                    //
                    if (index < tagList.Count - 1)
                        index++;
                    else
                        index = 0;
                }
                //
                Thread.Sleep(10);
            }
        }        
    }
}
