using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src.HMI
{
    class CDesignPasswordList : CDesignSystem, ICustomPasswordList
    {
        private string[] password;
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         */
        public CDesignPasswordList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("PasswordList");
            this.imageIndex = -1;
            this.password = new string[8];
        }
        //!
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        public string[] Password { get { return password; } }

        public bool Enabled { get; set; }
    }
}
