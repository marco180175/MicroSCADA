using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Tags
{
    /*!
     * Tag contador de tempo 
     */
    public class CDesignTimerTag : CDesignDinamicTag, ICustomTimerTag
    {
        private TimeSpan m_maxValue = TimeSpan.MaxValue;
        /*!
         * Construtor
         * @param AOwner Referencia para objeto proprietario        
         * @param Project Referencia para o projeto
         */
        public CDesignTimerTag(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            //this.customDemoTag = new CCustomDemoTag();
            this.imageIndex = 47;
            this.Value = "0";
            this.customTag.scanTime = 100;//ms
            //this.customTag.dataType = CCustomDataType.dtTimer;
            this.m_maxValue = new TimeSpan(23, 59, 59);
        }

        public TimeSpan MaxValue { get { return m_maxValue; } set { m_maxValue = value; } }

        public TimeSpan MinValue { get; set; }

        public bool Enabled { get; set; }
        [ReadOnly(true)]
        public int Scan
        {
            get { return this.customTag.scanTime; }
            set { this.customTag.scanTime = value; }
        }
        //! Tipo de dados apenas leitura
        [ReadOnly(true)]
        public override CCustomDataType DataType
        {
            get { return CCustomDataType.dtTimer; }            
        }
        /*!
         * 
         */
        public override void Dispose()
        {            
            //this.customDemoTag.Dispose();
            base.Dispose();
        }            
    }
}
