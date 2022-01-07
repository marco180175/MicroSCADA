using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    /*!
     * 
     */
    public class CRuntimeBargraph : CRuntimeCustomField, ICustomBargraph 
    {
        private CCustomBargraph customBargraph;         
        /*!
         * Construtor
         * @param AOwner      
         * @param Project         
         */
        public CRuntimeBargraph(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customBargraph = new CCustomBargraph(ObjectList);                        
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);
        }
        /*!
         * Destrutor
         */
        ~CRuntimeBargraph()
        {
            Dispose();    
        }
        /*!
         * Destrutor
         */
        public override void Dispose()
        {            
            this.customBargraph.Dispose();
            base.Dispose();
        }
        /*!
         * 
         */
        public ICustomBargraphElement NewBar()
        {            
            CRuntimeBargraphElement bar = new CRuntimeBargraphElement(this, project);
            ObjectList.Add(bar);                       
            return bar;
        }

        public float MaxValue
        {
            get { return this.customBargraph.maxValue; }
            set { this.customBargraph.maxValue = value; }
        }

        public float MinValue
        {
            get { return this.customBargraph.minValue; }
            set { this.customBargraph.minValue = value; }
        }
        public Font ScaleFont
        {
            get { return this.customBargraph.font; }
            set { this.customBargraph.font = value; }
        }
        public Color FontColor
        {
            get { return this.customBargraph.fontColor; }
            set { this.customBargraph.fontColor = value; }
        }
        public CBargraphOrientation Orientation
        {
            get { return this.customBargraph.orientation; }
            set { this.customBargraph.orientation = value; }
        }
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            customBargraph.DrawBargraph(e.Graphics, pictureBox);
            for (int i = 0; i < ObjectList.Count; i++)
            {
                CRuntimeBargraphElement bar = (CRuntimeBargraphElement)ObjectList[i];
                float value = float.Parse(bar.GetValue());
                customBargraph.DrawBar(e.Graphics, i, value);
            }          
        }       
        /*!
         * 
         */
        public override void LinkObjects()
        {            
            foreach (CRuntimeBargraphElement bar in ObjectList)            
                bar.LinkObjects();            
        }        
    }
    /*!
     * 
     */
    class CRuntimeBargraphElement : CRuntimeSystem, ICustomBargraphElement, IRuntimeField
    {
        private CCustomBargraphElement customBargraphElement;
        private int indexTagValue;
        private float m_value;
        public CRuntimeBargraphElement(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customBargraphElement = new CCustomBargraphElement();
            this.indexTagValue = this.ReferenceList.AddReference();
            this.m_value = 0;
        }

        public Color BarColor
        {
            get { return this.customBargraphElement.barColor; }
            set { this.customBargraphElement.barColor = value; }
        }
        
        //!
        public ICustomTag TagValue
        {
            get { return (ICustomTag)GetReference(indexTagValue); }
            set { SetReference(indexTagValue, value); }
        }
        /*!
         * 
         */
        public void SetGuidTagValue(Guid Value)
        {
            this.SetReferenceGuid(indexTagValue, Value);
        }
        /*!
         * 
         */
        public void SetValue(string Value)
        {
            this.m_value = float.Parse(Value);
            CRuntimeBargraph bargraph;
            bargraph = (CRuntimeBargraph)Owner;
            bargraph.getPictureBox().Invalidate();
        }
        /*!
         * 
         */
        public String GetValue()
        {
            return this.m_value.ToString();
        }
        /*!
         * 
         */
        public PictureBox getPictureBox()
        {
            CRuntimeBargraph bargraph;
            bargraph = (CRuntimeBargraph)Owner;
            return bargraph.getPictureBox();
        }
        /*!
         * Evento setvalue do tag
         */
        private void field_SetValue(object sender, TagSetValueEventArgs e)
        {
            SetValue(e.Value);
        }
        /*!
         * 
         */
        public void LinkObjects()
        {
            Object obj;
            Guid keyGUID = GetReferenceGuid(indexTagValue);
            if (CHashObjects.ObjectDictionary.ContainsKey(keyGUID))
                obj = CHashObjects.ObjectDictionary[keyGUID];
            else
                obj = null;
            SetReference(indexTagValue, obj);
            //seta evento 
            if (TagValue != null)
            {
                CRuntimeCustomTag RuntimeTag = (CRuntimeCustomTag)TagValue;
                RuntimeTag.SetValueEvent += new TagSetValueEventHandler(field_SetValue);
            }
        }
    }
}
