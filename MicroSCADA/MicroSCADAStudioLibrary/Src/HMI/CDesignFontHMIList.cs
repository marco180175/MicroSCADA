using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src.HMI
{
    class CDesignFontHMI : CDesignSystem, ICustomFontHMI
    {
        public CDesignFontHMI(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {

        }

        public int Size { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool Strikeout { get; set; }
        public bool Full { get; set; }
    }

    class CDesignFontHMIList: CDesignSystem, ICustomFontHMIList
    {
        /*!
         * Construtor
         * @param AOwner         
         * @param Project
         */
        public CDesignFontHMIList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {            
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("FontList");
            this.imageIndex = 10;
        }
        //!Modifica Name para apenas leitura
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }

        public ICustomFontHMI NewFont()
        {
            CDesignFontHMI font = new CDesignFontHMI(this, project);
            ObjectList.Add(font);
            return font;
        }
    }
}
