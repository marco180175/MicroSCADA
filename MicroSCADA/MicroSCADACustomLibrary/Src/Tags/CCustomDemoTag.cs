using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    /*!
     * Tipos de sinais gerados
     */
    public enum CDemoType
    {
        dtSRAM,
        dtRandom, //!< Randomico
        dtSine, //!< Senoidal
        dtSquare, //!< Quadrada
        dtTriangulate, //!< Triangular
        dtTriangulateLeft, //!< Dente de serra crescente
        dtTriangulateRight //!< Dente de serra decrescente
    }

    public interface ICustomDemoTag : ICustomInternalTag, IControlOfTag
    {        
        float MaxValue { get; set; }
        float MinValue { get; set; }
        float Increment { get; set; }
        //TODO: implementar - float Offset { get; set; }
    }
        
    public interface ICustomTimerTag : ICustomInternalTag, IControlOfTag
    {
        TimeSpan MaxValue { get; set; }
        TimeSpan MinValue { get; set; }
        //TODO: implementar - float Offset { get; set; }
    }

    public class CCustomDemoTag : Object    
    {
        public CDemoType type;
        public float maxValue;
        public float minValue;       
        public float increment;    
        
        /*!
         * Construtor
         */
        public CCustomDemoTag()
            : base()
        {                   
            this.type = CDemoType.dtRandom;
            this.maxValue = 32000;
            this.minValue = 0;           
            this.increment = 1;            
        }       
    }
}
