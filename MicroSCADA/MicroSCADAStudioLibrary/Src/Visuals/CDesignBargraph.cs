using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADAStudioLibrary.Src.TypeConverter;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * Grafico de barras
     */
    public class CDesignBargraph : CDesignCustomField, ICustomBargraph, IDesignCollection
    {
        protected CCustomBargraph customBargraph;

        public CDesignBargraph(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject();
            this.customBargraph = new CCustomBargraph(ObjectList);                        
            this.imageIndex = 33;            
        }

        
        #region Propriedades
        //! Lista de items
        [Editor(typeof(CollectionEditorPreset), typeof(UITypeEditor))]
        public ArrayList Items
        {
            get { return ObjectList; }
        }    
        //!
        [Category("Ranges")]
        public float MaxValue 
        {
            get { return this.customBargraph.maxValue; }
            set { this.customBargraph.maxValue = value; }
        }
        //!
        [Category("Ranges")]
        public float MinValue 
        {
            get { return this.customBargraph.minValue; }
            set { this.customBargraph.minValue = value; }
        }
        //!
        [Category("Appearance")]
        public Font ScaleFont
        {
            get { return this.customBargraph.font; }
            set { this.customBargraph.font = value; }
        }
        //!
        [Category("Appearance")]
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
        //!
        [ReadOnly(true)]
        public override CFieldType FieldType
        {
            get { return CFieldType.ftRead; }
        }
        //! Não utilizado
        [Browsable(false)]
        public override CDesignCustomTag TagValueEx
        {
            get { return this.GetTagValue(); }        
        }
        #endregion
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignBargraph> subSet = Objects.OfType<CDesignBargraph>();
            return subSet.ToArray();
        }
        /*!
         * 
         */
        public ICustomBargraphElement NewBar()
        {
            CDesignBargraphElement bar = new CDesignBargraphElement(this, project);
            bar.UpdateHint += new EventHandler(bar_UpdateHint);
            bar.DelItem += new DelItemEventHandler(bar_DelItem);
            OnAddItem(new AddItemEventArgs(bar, bar.ImageIndex));
            ObjectList.Add(bar);
            bar.MaxValue = MaxValue;
            bar.MinValue = MinValue;
            return bar;
        }
        /*!
         * 
         */
        public CDesignBargraphElement NewBarEx()
        {
            CDesignBargraphElement bar = (CDesignBargraphElement)NewBar();
            bar.SetGUID(Guid.NewGuid());
            bar.Name = "Bar" + ObjectList.Count.ToString();            
            pictureBox.Invalidate();
            return bar;
        }
        /*!
         * Event handler
         */
        private void bar_UpdateHint(object sender, EventArgs e)
        {
            MakeHint();
        }
        /*!
         * Event handler
         */
        private void bar_DelItem(object sender, EventArgs e)
        {            
            ObjectList.Remove(sender);
            MakeHint();
            pictureBox.Invalidate();
        }
        /*!
         * 
         */
        public IDesignCollectionItem NewItem()
        {
            return (IDesignCollectionItem)NewBarEx();
        }
        /*!
         * 
         */
        public void AddTagValue(CDesignCustomTag Value)
        {
            CDesignBargraphElement bar = NewBarEx();
            bar.TagValue = Value;
            MakeHint();
        }
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            customBargraph.DrawBargraph(e.Graphics, pictureBox);
            for (int i = 0; i < ObjectList.Count; i++)            
                customBargraph.DrawBar(e.Graphics, i, MaxValue);
            //
            if (selected)
                DrawSelectedRect(e.Graphics);
        }        
        /*!
         * 
         */
        public override void LinkObjects()
        {
            foreach (CDesignBargraphElement bar in ObjectList)                           
                bar.LinkObjects();
            MakeHint();
        }
        /*!
         * Monta Hint com multiplos tags 
         */
        protected override void MakeHint()
        {
            string hint = Name;            
            foreach (CDesignBargraphElement bar in ObjectList)
            {                
                if (bar.TagValue != null)
                    hint += "\r\n" + bar.TagValue.ToString();
                else
                    hint += "\r\n null";
            }               
            toolTip.SetToolTip(pictureBox, hint);
        }        
    }
    /*!
     * Barra
     */
    public class CDesignBargraphElement : CDesignSystem, ICustomBargraphElement, IDesignCollectionItem
    {
        private CCustomBargraphElement customBargraphElement;
        private int indexTagValue;
        public CDesignBargraphElement(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customBargraphElement = new CCustomBargraphElement();
            this.imageIndex = 46;
            this.indexTagValue = this.ReferenceList.AddReference();
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignBargraphElement> subSet = Objects.OfType<CDesignBargraphElement>();
            return subSet.ToArray();
        }

        public event EventHandler UpdateHint;
        private void OnUpdateHint(EventArgs e)
        {
            if (UpdateHint != null)
                UpdateHint(this, e);
        }
        //!
        [Browsable(false)]
        public ICustomTag TagValue
        {
            get { return (CDesignCustomTag)this.GetTagValue(); }
            set { this.SetTagValue((CDesignCustomTag)value); }
        }
        //!
        [Category("Tags")]
        [Editor(typeof(CTagTypeDialogPreset), typeof(UITypeEditor))]
        public CDesignCustomTag TagValueEx
        {
            get { return (CDesignCustomTag)this.GetTagValue(); }
            set 
            { 
                this.SetTagValue((CDesignCustomTag)value); 
                this.OnUpdateHint(EventArgs.Empty);
            }
        }
        //!
        public Color BarColor
        {
            get { return this.customBargraphElement.barColor; }
            set { this.customBargraphElement.barColor = value; }
        }
        //!
        [ReadOnly(true)]
        public float MaxValue { get; set; }
        //!
        [ReadOnly(true)]
        public float MinValue { get; set; }
        /*!
         * 
         */
        protected void SetTagValue(CDesignCustomTag Value)
        {
            this.SetReference(indexTagValue, Value);
        }
        /*!
         * 
         */
        public CDesignCustomTag GetTagValue()
        {
            return (CDesignCustomTag)this.GetReference(indexTagValue);
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
        public override void LinkObjects()
        {
            Object obj;
            Guid guid = GetReferenceGuid(indexTagValue);
            //
            if (CHashObjects.ObjectDictionary.ContainsKey(guid))
                obj = CHashObjects.ObjectDictionary[guid];
            else
                obj = null;
            //
            SetReference(indexTagValue, obj);
        }        
    }

    
}
