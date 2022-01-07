using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Tags
{
    public class CDesignPropertyTag : CDesignInternalTag, ICustomPropertyTag
    {
        private CDesignSystem m_reference;
        private Guid guidReference;
        private string m_propertyName;
        private PropertyInfo m_propertyInfo;
        /*!
         * Construtor
         * @param AOwner Referencia para proprietario
         * @param Project Referencia para o projeto
         */
        public CDesignPropertyTag(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {            
            this.m_reference = null;
            this.m_propertyName = string.Empty;
            this.imageIndex = 39;    
        }
        //!
        [Browsable(false)]
        public ICustomSystem Reference
        {
            get { return this.m_reference; }
        }
        //!
        [Browsable(true)]
        public CDesignSystem ObjectReference
        {
            get { return this.m_reference; }
        }
        //!
        [ReadOnly(true)]
        public string PropertyName
        {
            get { return this.m_propertyName; }
            set { this.m_propertyName = value; }
        }
        /*!
         * 
         */
        public override string Value
        {
            get { return this.GetValue(); }
            set { this.SetValue(value); }
        }
        /*!
         * 
         */
        private string GetValue()
        {            
            return m_propertyInfo.GetValue(m_reference, null).ToString();
        }
        /*!
         * 
         */
        private void SetValue(string Value)
        {
            if (m_propertyInfo != null)
            {
                if (m_propertyInfo.PropertyType == typeof(Int32))
                    m_propertyInfo.SetValue(m_reference, Int32.Parse(Value), null);
                else if (m_propertyInfo.PropertyType == typeof(string))
                    m_propertyInfo.SetValue(m_reference, Value, null);
            }
        }
        /*!
         * 
         */
        public void SetParams(CDesignSystem Reference, string PropertyName)
        {
            m_reference = Reference;
            m_propertyName = PropertyName;
            m_propertyInfo = m_reference.GetType().GetProperty(m_propertyName);
        }
        /*!
         * 
         */
        public override void SetGUID(Guid Value)
        {
            base.SetGUID(Value);
            SetOpenName(GUID.ToString("B").ToUpper());
        }
        /*!
         * 
         */
        public void SetGuidReference(Guid Value)
        {
            guidReference = Value;            
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {           
            if (CHashObjects.ObjectDictionary.ContainsKey(guidReference))
            {
                m_reference = (CDesignSystem)CHashObjects.ObjectDictionary[guidReference];
                m_propertyInfo = m_reference.GetType().GetProperty(m_propertyName);
            }
            else
                m_reference = null;         
        }     
        /*!
         * Subescreve ToString para retornar nome que sera mostrado no propertyGrid
         */
        public override string ToString()
        {
            if (m_reference == null)
                return base.ToString();
            else
                return m_reference.FullName+"."+m_propertyName;
        }
    }
}
