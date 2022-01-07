using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerPasswordList:CCompilerSystem, ICustomPasswordList
    {
        private string[] password;
        public CCompilerPasswordList(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        {
            password = new string[8];
        }

        public string[] Password { get { return password; } }

        public bool Enabled { get; set; }
    }
}
