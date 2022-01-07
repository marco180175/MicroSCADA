using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    /*!
     * Campo alfanumerico
     */
    public class CRuntimeAlphaNumeric : CRuntimeCustomField, ICustomAlphaNumeric
    {        
        private CCustomAlphaNumeric m_customAlphaNumeric;        
        private TextBox m_textBox;
        private NumberFormatInfo m_currentInfo;            
        /*!
         * Construtor
         * @param AOwner      
         * @param Project         
         */
        public CRuntimeAlphaNumeric(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.m_customAlphaNumeric = new CCustomAlphaNumeric();
            this.m_textBox = new TextBox();
            this.m_textBox.TextAlign = HorizontalAlignment.Center;
            this.m_textBox.Enter += new EventHandler(this.textBox_Enter);
            this.m_currentInfo = NumberFormatInfo.CurrentInfo;
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);            
            this.EditError += new FieldEditErrorEventHandler(this.runtimeAlphaNumeric_EditError);            
        }           
        /*!
         * Destrutor
         */
        ~CRuntimeAlphaNumeric()
        {
            Dispose();    
        }
        /*!
         * Destrutor
         */
        public override void Dispose()
        {           
            this.m_customAlphaNumeric.Dispose();
 	        base.Dispose();
        }
        #region Propriedades
        //!
        public CValueFormat ValueFormat
        {
            get { return this.m_customAlphaNumeric.valueFormat; }
            set { this.m_customAlphaNumeric.valueFormat = value; }
        }
        //!
        public int DecimalCount
        {
            get { return this.m_customAlphaNumeric.decimalCount; }
            set { this.m_customAlphaNumeric.decimalCount = value; }
        }
        //!
        public double MaxValue
        {
            get { return this.m_customAlphaNumeric.maxValue; }
            set { this.m_customAlphaNumeric.maxValue = value; }
        }
        //!
        public double MinValue
        {
            get { return this.m_customAlphaNumeric.minValue; }
            set { this.m_customAlphaNumeric.minValue = value; }
        }
        //!
        public Font Font
        {
            get { return this.m_customAlphaNumeric.font; }
            set { this.m_customAlphaNumeric.font = value; }
        }
        //!
        public Color FontColor
        {
            get { return this.m_customAlphaNumeric.fontColor; }
            set { this.m_customAlphaNumeric.fontColor = value; }
        }
        //!
        public override int TabIndex
        {
            get { return pictureBox.TabIndex; }
        }
        //!
        //public StringAlignment Alignment
        //{
        //    get { return this.m_customAlphaNumeric.alignment; }
        //    set { this.m_customAlphaNumeric.alignment = value; }
        //}
        public HorizontalAlignment TextAlign 
        {
            get { return m_textBox.TextAlign; }
            set { m_textBox.TextAlign = value; }
        } 
        #endregion
        #region Eventos
        public event FieldEditErrorEventHandler EditError;
        private void OnEditError(FieldEditErrorEventArgs e)
        {
            if (EditError != null)
                EditError(this, e);
        }
        #endregion
        /*!
         * Evento OnEditError
         * Poderia ser implementado no form so que aqui fica mais facil.
         * @param sender
         * @param e
         */
        private void runtimeAlphaNumeric_EditError(object sender, FieldEditErrorEventArgs e)
        {
            if (pictureBox.Parent != null)
                MessageBox.Show(pictureBox.Parent, e.ErrorMessage);
        }    
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (TagValue != null)
            {
                string fValue = m_customAlphaNumeric.FormatValue(TagValue.Value, TagValue.DataType);
                m_customAlphaNumeric.DrawAlfaNumeric(e.Graphics, pictureBox, fValue);
                m_textBox.Text = fValue;
            }
            else
            {
                m_customAlphaNumeric.DrawAlfaNumeric(e.Graphics, pictureBox, "null");
                m_textBox.Text = "null";
            }
            //
            //if(focus)
            //    DrawFocusRectangle(e.Graphics);
        }       
        
        /*!
         * Seta evento OnKeyPress do TextBox
         */
        private void SetKeyPress()
        {
            if (TagValue != null)
            {
                switch (TagValue.DataType)
                {
                    case CCustomDataType.dtSByte:
                    case CCustomDataType.dtInt16:
                    case CCustomDataType.dtInt32:
                    case CCustomDataType.dtInt64:
                        {
                            switch (ValueFormat)
                            {
                                case CValueFormat.fmDecimal:
                                    m_textBox.KeyPress += new KeyPressEventHandler(maskedInt_KeyPress);
                                    break;
                                case CValueFormat.fmHexadecimal:
                                    m_textBox.KeyPress += new KeyPressEventHandler(maskedHexadecimal_KeyPress);
                                    break;
                                case CValueFormat.fmBinary:
                                    m_textBox.KeyPress += new KeyPressEventHandler(maskedBinary_KeyPress);
                                    break;
                                default:
                                    break;
                            }
                        }; break;
                    case CCustomDataType.dtByte:
                    case CCustomDataType.dtUInt16:
                    case CCustomDataType.dtUInt32:
                    case CCustomDataType.dtUInt64:
                        switch (ValueFormat)
                        {
                            case CValueFormat.fmDecimal:
                                m_textBox.KeyPress += new KeyPressEventHandler(maskedUInt_KeyPress);
                                break;
                            case CValueFormat.fmHexadecimal:
                                m_textBox.KeyPress += new KeyPressEventHandler(maskedHexadecimal_KeyPress);
                                break;
                            case CValueFormat.fmBinary:
                                m_textBox.KeyPress += new KeyPressEventHandler(maskedBinary_KeyPress);
                                break;
                            default:
                                break;
                        }
                        break;
                    case CCustomDataType.dtFloat32:
                    case CCustomDataType.dtFloat64:
                        {
                            switch (ValueFormat)
                            {
                                case CValueFormat.fmFixedPoint:
                                    m_textBox.KeyPress += new KeyPressEventHandler(maskedFloatFix_KeyPress);
                                    break;
                                case CValueFormat.fmScientific:
                                    m_textBox.KeyPress += new KeyPressEventHandler(maskedFloatExp_KeyPress);
                                    break;
                            }
                        }; break;
                    default:
                        {
                        }; break;
                }
            }
        }
        /*!
         * Evento OnKeyPress. Mascara para edição de int
         * @param sender
         * @param e
         */
        private void maskedInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                if (e.KeyChar == '-')
                {
                    e.Handled = tb.Text.Contains('-');
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
        /*!
         * Evento OnKeyPress. Mascara para edição de uint
         * @param sender
         * @param e
         */
        private void maskedUInt_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        /*!
         * Evento OnKeyPress. Mascara para edição de float ponto fixo
         * @param sender
         * @param e
         */
        private void maskedFloatFix_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            char nds = m_currentInfo.NumberDecimalSeparator[0];
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                if (e.KeyChar == '-')
                {
                    e.Handled = tb.Text.Contains('-');
                }
                else if (e.KeyChar == nds)
                {
                    e.Handled = tb.Text.Contains(nds);
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
        /*!
         * Evento OnKeyPress. Mascara para edição de float exponencial
         * @param sender
         * @param e
         */
        private void maskedFloatExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            char nds = m_currentInfo.NumberDecimalSeparator[0];
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                if (e.KeyChar == '-')
                {
                    int c = tb.Text.Count(v => v == '-');
                    e.Handled = c >= 2;
                }
                else if (e.KeyChar == 'e' || e.KeyChar == 'E')
                {
                    e.Handled = tb.Text.Contains('e') || tb.Text.Contains('E');
                }
                else if (e.KeyChar == nds)
                {
                    e.Handled = tb.Text.Contains(nds);
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
        /*!
         * Evento OnKeyPress. Mascara para edição de int, uint em hex
         * @param sender
         * @param e
         */
        private void maskedHexadecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || (char.ToUpper(e.KeyChar) >= 'A' && char.ToUpper(e.KeyChar) <= 'F') || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        /*!
         * Evento OnKeyPress. Mascara para edição de int, uint em bin
         * @param sender
         * @param e
         */
        private void maskedBinary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '0') || (e.KeyChar == '1') || (e.KeyChar == '\b'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        /*!
         * Evento OnKeyUp do textBox
         * @param sender
         * @param e
         */
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    {
                        if (IsValidValue(m_textBox.Text))
                        {
                            OnEditValue(new FieldEditValueEventArgs(m_textBox.Text));                            
                        }
                        else
                        {
                            string m = string.Format("Edit error: out of range. MinValue={0}, MaxValue={1}", MinValue, MaxValue);
                            OnEditError(new FieldEditErrorEventArgs(m));
                        }
                    }; break;
                case Keys.Escape:                    
                    //e.Handled = true;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
        /*!
         * Verifica se esta dentro do minimo e maximo 
         * @param Value: Valor em formato string
         * @return true se estiver no range
         */
        private bool IsValidValue(string Value)
        {
            if ((TagValue.DataType == CCustomDataType.dtString) || 
                (TagValue.DataType == CCustomDataType.dtChar))
            {
                return true;
            }
            else
            {
                double value = double.Parse(Value);                
                return ((value >= MinValue) && (value <= MaxValue));               
            }
        }
        /*!
         * Seta propriedade isEditing
         */
        //protected override void SetIsEditing(Boolean Value)
        //{
        //    isEditing = Value;
        //    if (isEditing == false)
        //    {
        //        if (m_textBox != null)
        //        {
        //            m_textBox.Dispose();
        //            m_textBox = null;
        //        }
        //    }
        //}        
        /*!
         * 
         */
        protected void SetTagValue(IRuntimeTag Value)
        {
            this.SetReference(indexTagValue, Value);
        }
        /*!
         * 
         */
        protected IRuntimeTag GetTagValue()
        {
            return (IRuntimeTag)this.GetReference(indexTagValue);
        }
        public override event EventHandler Enter;
        private void textBox_Enter(object sender, EventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }
        
        /*!
         * 
         */
        public override void LinkObjects()
        {
            base.LinkObjects();
            
            this.m_textBox.Parent = pictureBox;
            this.m_textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
            this.m_textBox.BorderStyle = BorderStyle.None;
            this.m_textBox.Font = Font;
            this.m_textBox.BackColor = BackColor;
            this.m_textBox.ForeColor = FontColor;
            this.m_textBox.Text = this.GetValue();            
            this.m_textBox.Left = 0;
            this.m_textBox.Top = ((pictureBox.Height - m_textBox.Height) / 2);
            this.m_textBox.Width = pictureBox.Width;
            this.m_textBox.Show();
            this.m_textBox.BringToFront();
            this.m_value = "0";
            if (TagValue == null)
            {
                m_textBox.Text = "null";
                m_textBox.Enabled = false;
            }
            else
            {
                if (FieldType == CFieldType.ftReadWrite)
                {
                    m_textBox.ReadOnly = false;
                    SetKeyPress();
                }
                else
                {
                    m_textBox.ReadOnly = true;
                }
            }
        }
    }    
}
