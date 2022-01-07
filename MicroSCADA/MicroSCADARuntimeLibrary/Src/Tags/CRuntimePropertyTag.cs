using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src.Tags
{
    /*!
     * Tag de leirura e escrita de propriedades dos objetos
     */
    public class CRuntimePropertyTag : CRuntimeInternalTag, ICustomPropertyTag
    {
        private CRuntimeSystem m_reference;
        private Guid m_guidReference;
        private string m_propertyName;
        /*!
         * Construtor
         * @param AOwner Referencia para proprietario
         * @param Project Referencia para o projeto
         */
        public CRuntimePropertyTag(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {            
            this.m_reference = null;
            this.m_propertyName = string.Empty;            
        }
        //!        
        public ICustomSystem Reference
        {
            get { return this.m_reference; }
        }        
        //!        
        public string PropertyName
        {
            get { return this.m_propertyName; }
            set { this.m_propertyName = value; }
        }
        /*!
         * 
         */
        public void SetParams(CRuntimeSystem Reference, string PropertyName)
        {
            this.m_reference = Reference;
            this.m_propertyName = PropertyName;
        }        
        /*!
         * 
         */
        public void SetGuidReference(Guid Value)
        {
            m_guidReference = Value;            
        }
        /*!
         * 
         */
        public void LinkObjects()
        {                     
            //
            if (CHashObjects.ObjectDictionary.ContainsKey(m_guidReference))
                m_reference = (CRuntimeSystem)CHashObjects.ObjectDictionary[m_guidReference];
            else
                m_reference = null;         
        }
        /*!
         * TODO:falta implementar para outros dataTypes
         */
        public override void PutValue(string Value)
        {            
            PropertyInfo propertyInfo = m_reference.GetType().GetProperty(m_propertyName);
            
            if (propertyInfo.PropertyType == typeof(Int32))
            {
                propertyInfo.SetValue(m_reference, Int32.Parse(Value), null);
            }
            else if (propertyInfo.PropertyType == typeof(string))
            {
                propertyInfo.SetValue(m_reference, Value, null);
            }
            else if (propertyInfo.PropertyType == typeof(bool))
            {
                propertyInfo.SetValue(m_reference, bool.Parse(Value), null);
            }
            
        }
        /*!
         * Retorna valor do tag
         */
        public override string GetValue()
        {
            PropertyInfo propertyInfo = m_reference.GetType().GetProperty(m_propertyName);
            string propertyValue;
            if (propertyInfo.PropertyType == typeof(Int32))
            {
                object propertyObject = propertyInfo.GetValue(m_reference, null);
                propertyValue = propertyObject.ToString();
            }
            else
                propertyValue = "0";
            return propertyValue;
        }
    }    
}
