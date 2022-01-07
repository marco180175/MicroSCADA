using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src.TypeConverter;

namespace MicroSCADAStudioLibrary.Src.Tags
{    
    /*!
     * Implementa classe tag
     * Implementa suporte ao PropertyGrig quando tag for membro de classe
     * [EditorAttribute(typeof(CTagTypeDialogPreset), typeof(System.Drawing.Design.UITypeEditor))]
     */
    public abstract class CDesignCustomTag : CDesignSystem, ICustomTag  
    {        
        protected CCustomTag customTag;        
        protected CDesignAlarm alarmHi;
        protected CDesignAlarm alarmLo;        
        /*!
         * Construtor                  
         * @param AOwner Referencia para objeto proprietario
         * @param Project Referencia para projeto
         */
        public CDesignCustomTag(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customTag = new CCustomTag();            
            this.alarmHi = new CDesignAlarm(this, Project);
            this.alarmHi.Name = "AlarmHi";
            this.alarmLo = new CDesignAlarm(this, Project);
            this.alarmLo.Name = "AlarmLo";            
        }
        /*!
         * 
         */
        public override void Dispose()
        {
            alarmLo.Dispose();
            alarmHi.Dispose();
            base.Dispose();
        }        
        //!( ActionProperty ) MARCA PROPRIEDADE QUE PODERA SER USADA EM RUNTIME
        [ActionProperty]
        public virtual string Value 
        {
            get { return this.customTag.value; }
            set { this.customTag.value = value; } 
        }
        //! Tipo de dado padrao IEC
        public virtual CCustomDataType DataType 
        {
            get { return this.customTag.dataType; }
            set { this.customTag.dataType = value; }
        }
        //[TypeConverterAttribute(typeof(ExpandableObjectConverter))]
        //public ICustomAlarm AlarmHiHi
        //{
        //    get { return this.alarmHiHi; }
        //}
        [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
        public ICustomAlarm AlarmHi
        {
            get { return this.alarmHi; }
        }
        [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
        public ICustomAlarm AlarmLo
        {
            get { return this.alarmLo; }
        }
        //[TypeConverterAttribute(typeof(ExpandableObjectConverter))]
        //public ICustomAlarm AlarmLoLo
        //{
        //    get { return this.alarmLoLo; }
        //}
        /*!
         * Subescreve ToString para retornar nome que sera mostrado no propertyGrid
         */
        public override string ToString()
        {
            return FullName;
        }
    }
}
