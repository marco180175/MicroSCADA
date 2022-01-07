using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADACustomLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public abstract class CRuntimeCustomField : CRuntimeScreenObject, ICustomField, IRuntimeField
    {
        protected CCustomField customField;
        protected string m_value;
        protected Boolean isEditing;
        protected int indexTagValue;
        public CRuntimeCustomField(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customField = new CCustomField();
            this.indexTagValue = this.ReferenceList.AddReference();
            this.m_value = "0";
            this.isEditing = false;
        }

        public virtual CFieldType FieldType
        {
            get { return this.customField.fieldType; }
            set { this.customField.fieldType = value; }
        }

        public ICustomTag TagValue
        {
            get { return (ICustomTag)GetReference(indexTagValue); }
            set { SetReference(indexTagValue, value); }
        }

        public virtual int TabIndex
        {
            get { return -1; }
        }
        /*!
         * 
         */
        public virtual void SetValue(String Value)
        {
            this.m_value = Value;
            this.pictureBox.Invalidate();
        }
        /*!
         * 
         */
        public virtual String GetValue()
        {
            return this.m_value;            
        }
        /*!
         * 
         */
        public override PictureBox getPictureBox() { return this.pictureBox; }
        //
        public Boolean IsEditing
        {
            get { return this.isEditing; }
            set { this.SetIsEditing(value); }
        }
        /*!
         * 
         */
        protected virtual void SetIsEditing(Boolean Value)
        {
            this.isEditing = Value;
        }
        /*!
         * 
         */
        protected void DrawFocusRectangle(Graphics graphics)
        {
            Pen pen = new Pen(Color.Black, 3);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            graphics.DrawRectangle(pen, new Rectangle(3, 3, pictureBox.Width - 6, pictureBox.Height - 6));
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
        public event FieldEditValueEventHandler EditValueEvent;
        /*!
         * Evento setvalue do tag
         */        
        private delegate void SetValueCallBack(string Value);
        private void field_SetValue(object sender, TagSetValueEventArgs e)
        {
            if (pictureBox.InvokeRequired)
            {
                SetValueCallBack setValueCallBack;
                setValueCallBack = new SetValueCallBack(SetValue);
                pictureBox.Invoke(setValueCallBack, new object[] { e.Value });
            }
            else
            {
                SetValue(e.Value);
            }
        }
        /*!
         * 
         */
        protected void OnEditValue(FieldEditValueEventArgs e)
        {
            if (EditValueEvent != null)
                EditValueEvent(this, e);
        }
        public virtual event EventHandler Enter;        
        /*!
         * 
         */
        public override void LinkObjects()
        {
            Object obj;
            Guid keyGUID = GetReferenceGuid(indexTagValue);
            if (CHashObjects.ObjectDictionary.ContainsKey(keyGUID))
                obj = CHashObjects.ObjectDictionary[keyGUID];
            else
                obj = null;
            SetReference(indexTagValue, obj);
            //seta eventos 
            if (TagValue != null)
            {
                //
                CRuntimeCustomTag RuntimeTag = (CRuntimeCustomTag)TagValue;
                //evento valor do tag para campo
                RuntimeTag.SetValueEvent += new TagSetValueEventHandler(field_SetValue);
                //evento valor do campo para tag
                RuntimeTag.RegisterField(this);
                //evento valorcampo para tag
                ////RuntimeTag.FieldList.Add(this);
                ////EditValueEvent += new FieldEditValueEventHandler(RuntimeTag.tag_SetValue);
                //adiciona lista de tags externos na tela
                if (RuntimeTag is IRuntimeExternalTag)
                {
                    CRuntimeScreen screen = (CRuntimeScreen)Owner;
                    IRuntimeExternalTag extTag = (IRuntimeExternalTag)RuntimeTag;
                    if (screen.TagList.IndexOf(extTag.GetReference()) == -1)
                        screen.TagList.Add(extTag.GetReference());
                }
            }
        }

        
    }   


}
