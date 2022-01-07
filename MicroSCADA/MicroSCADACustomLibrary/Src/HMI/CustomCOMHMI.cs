using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomComHMI : ICustomObject
    {
        int COMType { get; set; }
        int BaudRate { get; set; }
        int TimeOut { get; set; }
        int ComId { get; set; }
        int Protocol { get; set; }
        bool ModcomAddress { get; set; }
    }
}
