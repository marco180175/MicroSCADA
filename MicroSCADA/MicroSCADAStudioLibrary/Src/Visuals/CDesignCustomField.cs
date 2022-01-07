using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADAStudioLibrary.Src.TypeConverter;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * Super classe para todos os objetos visuais que usam tag (campos).
     */
    public abstract class CDesignCustomField : CDesignScreenObject, ICustomField
    {
        private CCustomField customField;//!< 
        protected int indexTagValue;//!< Indice do tagValue
        /*!
         * Construtor
         * @param AOwner
         * @param Project
         * @param Parent         
         */
        public CDesignCustomField(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.customField = new CCustomField();
            this.indexTagValue = this.ReferenceList.AddReference();
            this.DoubleClick += new EventHandler(Project.DoubleClick);  
        }
        /*!
         * Destrutor
         */
        public override void Dispose()
        {
            //customField.Dispose();
            base.Dispose();
        }
        #region Propriedades
        //!
        [Browsable(false)]
        public ICustomTag TagValue
        {
            get { return (ICustomTag)this.TagValueEx; }
            set { this.TagValueEx = (CDesignCustomTag)value; }
        }
        //!
        [Browsable(true), Category("Tags")]
        [Editor(typeof(CTagTypeDialogPreset), typeof(UITypeEditor))]
        public virtual CDesignCustomTag TagValueEx
        {
            get { return this.GetTagValue(); }
            set { this.SetTagValue(value); }
        }
        //! leitura ou leitura e escrita
        public virtual CFieldType FieldType
        {
            get { return this.customField.fieldType; }
            set { this.customField.fieldType = value; }
        }
        //!
        public event EventHandler DoubleClick;
        #endregion
        /*!
         * 
         */
        protected void SetTagValue(CDesignCustomTag Value)
        {
            this.SetReference(indexTagValue, Value);
            this.MakeHint();
        }
        /*!
         * 
         */
        protected CDesignCustomTag GetTagValue()
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
        protected virtual void MakeHint()
        {
            String hint;
            if (TagValue != null)
                hint = Name + "\r\n" + TagValue.ToString();
            else
                hint = Name;
            toolTip.SetToolTip(pictureBox, hint);
        }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            Object obj;

            Guid guid = GetReferenceGuid(indexTagValue);
            if (CHashObjects.ObjectDictionary.ContainsKey(guid))
                obj = CHashObjects.ObjectDictionary[guid];
            else
                obj = null;
            SetReference(indexTagValue, obj);
            //
            MakeHint();
        }
        /*!
         * 
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (tabOrder)
            {
                DrawTabOrderLabel(e.Graphics);                
            }
        }
        /*!
         * Evento DoubleClick para edição de parametros pela IDE
         */
        protected override void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }
        /*!
         * Invoke DoubleCkick
         * @param e Argumentos do evento
         */
        private void OnDoubleClick(EventArgs e)
        {
            if (DoubleClick != null)
                DoubleClick(this, e);
        }
        #region EditTabOrder
        //!
        
        /*!
         * 
         */
        private void DrawTabOrderLabel(Graphics graphics)
        {
            int index = GetIndex(this);
            string strIndex = string.Format("{0:D3}", index);
            Font font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            SizeF sizeF = graphics.MeasureString(strIndex, font);
            RectangleF rect = new RectangleF(1, 1, sizeF.Width, sizeF.Height);
            Brush fontBrush = new SolidBrush(Color.Black);
            Brush rectBrush;
            if(isOrdenated)
                rectBrush = new SolidBrush(Color.Lime);
            else
                rectBrush = new SolidBrush(Color.Yellow);
            graphics.FillRectangle(rectBrush, rect);
            graphics.DrawRectangle(new Pen(fontBrush), rect.X, rect.Y, rect.Width, rect.Height);
            graphics.DrawString(strIndex, font, fontBrush, 1, 1);     
        }
        #endregion

        
        
        
    }

    public class ChangeTabOrderEventArgs : EventArgs
    {
        int m_index;
        public ChangeTabOrderEventArgs(int Index)
        {
            m_index = Index;
        }
        public int Index { get { return m_index; } }
    }
    public delegate void ChangeTabOrderEventHandler(object sender, ChangeTabOrderEventArgs e);
}
