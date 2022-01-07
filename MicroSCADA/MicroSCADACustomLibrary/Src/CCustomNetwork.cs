using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomNetwork : ICustomObject
    {
        ICustomSlave NewSlave();
    }

    public class CCustomNetwork 
    {
        public CCustomNetwork()
            : base()
        {
        }       
    }
}
