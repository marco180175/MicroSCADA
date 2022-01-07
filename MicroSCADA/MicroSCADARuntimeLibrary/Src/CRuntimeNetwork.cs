using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeNetwork : CRuntimeSystem, ICustomNetwork
    {
        /*!
         * Construtor
         */
        public CRuntimeNetwork(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            
        }
        /*!
         * Adiciona nova slave na rede
         * @return Referencia para nova slave
         */
        public ICustomSlave NewSlave()
        {
            CRuntimeSlave slave;

            slave = new CRuntimeSlave(this, project);
            ObjectList.Add(slave);

            return slave;
        }
    }
}
