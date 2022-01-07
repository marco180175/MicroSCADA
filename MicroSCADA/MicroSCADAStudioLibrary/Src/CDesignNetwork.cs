using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
//using MicroSCADAStudioLibrary.Src.EnvironmentDesigner;

namespace MicroSCADAStudioLibrary.Src
{
    public class CDesignNetwork : CDesignSystem, ICustomNetwork
    {
        protected CCustomNetwork customNetwork;        
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         */
        public CDesignNetwork(Object AOwner, CDesignProject Project)
            : base(AOwner,Project)
        {
            this.customNetwork = new CCustomNetwork();            
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("Network");
            this.imageIndex = 10;
        }
        //!Modifica Name para apenas leitura
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        public void Clear()
        {
            while (ObjectList.Count > 0)
                ((CDesignSystem)ObjectList[0]).Dispose();
        }
        /*!
         * Adiciona nova slave na rede
         * @return Referencia para nova slave
         */
        public ICustomSlave NewSlave()
        {            
            CDesignSlave slave = new CDesignSlave(this, project);
            ObjectList.Add(slave);
            OnAddItem(new AddItemEventArgs(slave, slave.ImageIndex));            
            return slave;
        }
        public CDesignSlave AddSlaveEx()
        {
            CDesignSlave slave = (CDesignSlave)NewSlave();            
            slave.Name = "Slave" + ObjectList.Count.ToString();
            return slave;
        }
    }
}
