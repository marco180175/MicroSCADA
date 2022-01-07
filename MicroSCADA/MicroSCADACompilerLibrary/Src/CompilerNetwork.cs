using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerNetwork: CCompilerSystem, ICustomNetwork
    {
        /*!
         * Construtor
         */
        public CCompilerNetwork(Object AOwner, CCompilerProject Project)
            : base(AOwner, Project)
        {

        }
        /*!
         * Adiciona nova slave na rede
         * @return Referencia para nova slave
         */
        public ICustomSlave NewSlave()
        {
            CCompilerSlave slave;

            slave = new CCompilerSlave(this, project);
            ObjectList.Add(slave);

            return slave;
        }
    }
}
