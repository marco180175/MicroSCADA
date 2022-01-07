using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * 
     */
    public class CDesignPopupScreen : CDesignBaseScreen, ICustomPopUpScreen
    {
        /*!
         * Construtor
         * @param AOwner Objeto (pai) proprietario
         * @param Project Referencia para o projeto
         */
        public CDesignPopupScreen(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.imageIndex = 15;
            this.width = 600;
            this.height = 200;
        }
        //!Não muda posição em desegn. Armazena e parassa para runtime
        [Category("Location")]
        public int Left { get; set; }
        //!Não muda posição em desegn. Armazena a parassa para runtime
        [Category("Location")]
        public int Top{ get; set; }
        //!Não mostra em desegn. Armazena a parassa para runtime
        [Category("Title")]
        public bool ShowTitleBar { get; set; }
        //!Não mostra em desegn. Armazena a parassa para runtime
        [Category("Title")]
        public string Title { get; set; }
    }
}
