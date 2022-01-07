using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using MicroSCADAStudio.Src.Forms;

namespace MicroSCADAStudio
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Show splash
            SplashForm splashForm = new SplashForm();            
            splashForm.ShowDialog();
            splashForm.Dispose();
            //Start application
            Application.Run(new MainFormDesign());            
        }
        
    }
}
