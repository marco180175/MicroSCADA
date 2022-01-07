using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADACustomLibrary.Src
{
    /*!
     * Interface para lista de screen
     */
    public interface ICustomScreenList : ICustomSystem
    {
        ICustomScreen NewScreen();
    }
    /*!
     * Interface para screens que implementa Width e Height
     * que serão setados em cada tela instanciada.
     */
    public interface ICustomDefaultScreenList : ICustomScreenList
    {
        int Width { set; get; }
        int Height { set; get; }
    }

    public class CCustomScreenList : Object
    {        
        /*!
         * Construtor
         */
        public CCustomScreenList()
            : base()
        {
        }
        //Destructor
        ~CCustomScreenList()
        {
        }
        public void Clear()
        {
        } 
    }
}
