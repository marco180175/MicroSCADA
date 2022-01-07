using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src.HMI
{
    class CDesignComHMI: CDesignSystem, ICustomComHMI
    {
        public CDesignComHMI(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        { }
        public int COMType { get; set; }
        public int BaudRate { get; set; }
        public int TimeOut { get; set; }
        public int ComId { get; set; }
        public int Protocol { get; set; }
        public bool ModcomAddress { get; set; }
    }
}
