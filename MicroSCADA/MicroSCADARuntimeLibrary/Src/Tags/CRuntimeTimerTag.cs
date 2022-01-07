using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src.Tags
{
    public class CRuntimeTimerTag : CRuntimeDinamicTag, ICustomTimerTag
    {
        private long m_beginTick;
        //private TimeSpan m_timeSpanActual;
        //private string m_format;
        private bool f_start;
        public CRuntimeTimerTag(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            //this.customDemoTag = new CCustomDemoTag();    
            //this.m_format = "hh\\:mm\\:ss\\.fff";
            this.f_start = true;
            this.Enabled = true;
            this.Scan = 100;
        }
        public TimeSpan MaxValue { get; set; }

        public TimeSpan MinValue { get; set; }
        /*!
         * 
         */
        public override void DoStep()
        {
            this.customTag.dataType = CCustomDataType.dtTimer;
            long totalTick;
            if (f_start)
            {
                m_beginTick = Environment.TickCount;
                totalTick = 0;
                f_start = false;
                //f_event = false;
            }
            totalTick = (Environment.TickCount - m_beginTick) * 10000;
            if (totalTick < MaxValue.Ticks)
            {
                SetValue(totalTick.ToString());                
            }
            else
            {
                //if (f_event == false)
                //{
                //    SetValueTime(m_maxValue.Ticks);
                //    string value = m_maxValue.ToString(m_format);
                //    //OnValueChange(new ValueChangeEventArgs(Address, value));
                //    //
                //    //OnPreset(new EventArgs());
                //    //f_event = true;
                //    //if (f_loop)
                //    //{
                //    //    f_start = true;
                //    //}
                //}
            }
        }
    }
}
