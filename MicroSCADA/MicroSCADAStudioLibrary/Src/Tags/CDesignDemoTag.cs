using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Tags
{
    /*!
     * Tag interno tipo DEMO gera formas de onda periodicas
     */
    public class CDesignDemoTag : CDesignDinamicTag, ICustomDemoTag    
    {
        protected CCustomDemoTag customDemoTag;        
        /*!
         * Construtor
         * @param AOwner Referencia para objeto proprietario        
         * @param Project Referencia para o projeto
         */
        public CDesignDemoTag(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customDemoTag = new CCustomDemoTag();
            this.imageIndex = 20;
            this.Value = "0";
            this.customTag.dataType = CCustomDataType.dtFloat32;
        }
        /*!
         * 
         */
        public override void Dispose()
        {            
            //this.customDemoTag.Dispose();
            base.Dispose();
        }            
        /*!
         * 
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignDemoTag> subSet = Objects.OfType<CDesignDemoTag>();
            return subSet.ToArray();                
        }
        [Browsable(true), ReadOnly(true)]
        public override CCustomDataType DataType
        {
            get { return this.customTag.dataType; }            
        }

        [Browsable(false)]
        public override int ArraySize
        {
            get { return this.customInternalTag.arraySize; }
            set { this.customInternalTag.arraySize = value; }
        }
        [ReadOnly(false)]
        public override CDemoType Type
        {
            get { return this.customDemoTag.type; }
            set { this.SetType(value); }
        }

        public float MaxValue
        {
            get { return this.customDemoTag.maxValue; }
            set { this.customDemoTag.maxValue = value; }
        }
        public float MinValue
        {
            get { return this.customDemoTag.minValue; }
            set { this.customDemoTag.minValue = value; }
        }        
        public float Increment
        {
            get { return this.customDemoTag.increment; }
            set { this.customDemoTag.increment = value; }
        }
        public int Scan
        {
            get { return this.customTag.scanTime; }
            set { this.customTag.scanTime = value; }
        }
        public virtual Boolean Enabled
        {
            get { return this.customTag.enabled; }
            set { this.customTag.enabled = value; }
        }

        private void SetType(CDemoType Value)
        {
            customDemoTag.type = Value;            
            switch (customDemoTag.type)
            {   
                case CDemoType.dtRandom:
                    this.imageIndex = 19;                    
                    break;
                case CDemoType.dtSine:
                    this.imageIndex = 20;                    
                    break;
                case CDemoType.dtSquare:
                    this.imageIndex = 21;                    
                    break;
                case CDemoType.dtTriangulate:
                    this.imageIndex = 22;                    
                    break;
                case CDemoType.dtTriangulateLeft:
                    this.imageIndex = 23;                    
                    break;
                case CDemoType.dtTriangulateRight:
                    this.imageIndex = 24;                    
                    break;
            }
            OnSetObjectIcon(new AddItemEventArgs(null, this.imageIndex));           
        }
        
    }
}
