using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomScript : ICustomObject
    {
        String Script { get; set; }
    }
}
