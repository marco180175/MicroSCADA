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
     * Tag armazena valor na ram statica
     */
    public class CDesignSRAMTag : CDesignInternalTag, ICustomSRAMTag   
    {
        //protected CCustomMemoryTag customMemoryTag;
        private static int count = 0;
         
        /*!
         * Construtor
         */
        public CDesignSRAMTag(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            //this.customMemoryTag = new CCustomMemoryTag();            
            this.imageIndex = 11;
            this.DataType = CCustomDataType.dtInt16;
            //this.m_value = "0";
            count++;
        }
        /*!
         * 
         */
        public override void Dispose()
        {
            count--;            
            //this.customMemoryTag.Dispose();
            base.Dispose();
        }       
        //! 
        [Browsable(false)]
        public override bool Selected
        {
            get { return this.selected; }
            set { this.SetSelected(value); }
        }

        //public override string Value 
        //{ 
        //    get; 
        //    set; 
        //}

        //private string GetValue()
        //{
        //    switch (this.DataType)
        //    {
        //        case CIECDataType.dtBool:                                   
        //            return bool.FalseString;  
        //        case CIECDataType.dtString:
        //            return string.Empty;
        //        default:
        //            return "0";
        //    }
        //}

        //private void SetValue(string Value)
        //{

        //}
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */        
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignSRAMTag> subSet = Objects.OfType<CDesignSRAMTag>();
            return subSet.ToArray(); 
        }
    }
}
