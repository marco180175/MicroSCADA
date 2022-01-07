using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.IOFiles
{
    public class OpenProgressEventArgs : EventArgs
    {
        private double progress;
        public OpenProgressEventArgs(double Value)
        { progress = Value; }
        public double Progress { get { return progress; } }
    }
    public delegate void OpenProgressEventHandler(object sender, OpenProgressEventArgs e);
}
