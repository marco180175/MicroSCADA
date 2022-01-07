using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src.Forms;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    public abstract class CDesignBaseScreen : CDesignCustomScreen
    {
        protected ContextMenuStrip menuPopup;        
        protected CDesignEditCommandManager editCommand;
        public CDesignBaseScreen(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.menuPopup = new ContextMenuStrip();
            this.editCommand = new CDesignEditCommandManager();
            this.editCommand.PasteCommand += new PasteCommandEventHandler(editCommand_PasteCommand);
        }

        
        /*!
         * TODO: esta sendo chamada 2 vezes verificar bug
         */
        public override void Initialize(Control Parent)
        {
            //importante
            base.Initialize(Parent);
            //
            if (form.ContextMenuStrip == null)
            {
                ToolStripMenuItem subMenuPopup;
                //Copy
                subMenuPopup = new ToolStripMenuItem();
                subMenuPopup.Text = "Copy";
                subMenuPopup.Click += new EventHandler(miCopy_Click);   
                menuPopup.Items.Add(subMenuPopup);
                //Paste
                subMenuPopup = new ToolStripMenuItem();
                subMenuPopup.Text = "Paste";
                subMenuPopup.Click += new EventHandler(miPaste_Click);
                menuPopup.Items.Add(subMenuPopup);
                //Cut
                subMenuPopup = new ToolStripMenuItem();
                subMenuPopup.Text = "Cut";
                subMenuPopup.Click += new EventHandler(miCut_Click);
                menuPopup.Items.Add(subMenuPopup);
                //Delete
                subMenuPopup = new ToolStripMenuItem();
                subMenuPopup.Text = "Delete";
                subMenuPopup.Click += new EventHandler(miDelete_Click);
                menuPopup.Items.Add(subMenuPopup);
                //TabOrder
                subMenuPopup = new ToolStripMenuItem();                
                subMenuPopup.Text = "TabOrder";
                subMenuPopup.Click += new EventHandler(miTabOrder_Click);
                menuPopup.Items.Add(subMenuPopup);
                //
                form.ContextMenuStrip = menuPopup;
            }            
        }
        /*!
         *         
         * @param rect
         * @return
         */
        protected CDesignShape NewShapeEx(Rectangle rect)
        {
            CDesignShape shape = (CDesignShape)NewShape();
            shape.SetGUID(Guid.NewGuid());
            shape.Name = "Shape" + ShapeList.Count.ToString();
            shape.Left = rect.Left;
            shape.Top = rect.Top;
            shape.Width = rect.Width;
            shape.Height = rect.Height;
            return shape;
        }
        /*!
         *
         * @param x
         * @param y
         * @return
         */
        protected CDesignText NewTextEx(Rectangle rect)
        {
            CDesignText text;

            text = (CDesignText)NewText();
            text.SetGUID(Guid.NewGuid());
            text.Name = "Text" + TextList.Count.ToString();
            text.Left = rect.Left;
            text.Top = rect.Top;
            text.Width = rect.Width;
            text.Height = rect.Height;
            return text;
        }
        /*!
         * 
         * @param rect         
         */
        protected CDesignPicture NewPictureEx(Rectangle rect)
        {
            CDesignPicture picture;

            picture = (CDesignPicture)NewPicture();
            picture.SetGUID(Guid.NewGuid());
            picture.Name = "Picture" + PictureList.Count.ToString();
            picture.Left = rect.Left;
            picture.Top = rect.Top;
            picture.Width = rect.Width;
            picture.Height = rect.Height;
            return picture;
        }
        /*!
         * 
         */
        protected CDesignAlphaNumeric NewAlphaNumericEx(Rectangle rect)
        {
            CDesignAlphaNumeric alphaNumeric;

            alphaNumeric = (CDesignAlphaNumeric)NewAlphaNumeric();
            alphaNumeric.SetGUID(Guid.NewGuid());
            alphaNumeric.Name = "AlphaNumeric" + AlphanumericList.Count.ToString();
            alphaNumeric.Left = rect.Left;
            alphaNumeric.Top = rect.Top;
            alphaNumeric.Width = rect.Width;
            alphaNumeric.Height = rect.Height;
            return alphaNumeric;
        }
        /*!
         * 
         */
        protected CDesignDinamicText NewDinamicTextEx(Rectangle rect)
        {
            CDesignDinamicText dinamicText;

            dinamicText = (CDesignDinamicText)NewDinamicText();
            dinamicText.SetGUID(Guid.NewGuid());
            dinamicText.Name = "DinamicText" + DinamicTextList.Count.ToString();
            dinamicText.Left = rect.Left;
            dinamicText.Top = rect.Top;
            dinamicText.Width = rect.Width;
            dinamicText.Height = rect.Height;
            return dinamicText;
        }
        /*!
         * 
         */
        protected CDesignAnimation NewAnimationEx(Rectangle rect)
        {
            CDesignAnimation animation;

            animation = (CDesignAnimation)NewAnimation();
            animation.SetGUID(Guid.NewGuid());
            animation.Name = "Animation" + AnimationList.Count.ToString();
            animation.Left = rect.Left;
            animation.Top = rect.Top;
            animation.Width = rect.Width;
            animation.Height = rect.Height;
            return animation;
        }
        /*!
         * 
         */
        protected CDesignTrendChart NewTrendChartEx(Rectangle rect)
        {
            CDesignTrendChart trendChart;

            trendChart = (CDesignTrendChart)NewTrendChart();
            trendChart.SetGUID(Guid.NewGuid());
            trendChart.Name = "TrendChart" + TrendChartList.Count.ToString();
            trendChart.Left = rect.Left;
            trendChart.Top = rect.Top;
            trendChart.Width = rect.Width;
            trendChart.Height = rect.Height;
            return trendChart;
        }
        /*!
         * 
         */
        protected CDesignBargraph NewBargraphEx(Rectangle rect)
        {
            CDesignBargraph bargraph;

            bargraph = (CDesignBargraph)NewBargraph();
            bargraph.SetGUID(Guid.NewGuid());
            bargraph.Name = "Bargraph" + BargraphList.Count.ToString();
            bargraph.Left = rect.Left;
            bargraph.Top = rect.Top;
            bargraph.Width = rect.Width;
            bargraph.Height = rect.Height;
            return bargraph;
        }
        /*!
         * 
         */
        protected CDesignButton NewButtonEx(Rectangle rect)
        {
            CDesignButton button;

            button = (CDesignButton)NewButton();
            button.SetGUID(Guid.NewGuid());
            button.Name = "Buttom" + ButtonList.Count.ToString();
            button.Left = rect.Left;
            button.Top = rect.Top;
            button.Width = rect.Width;
            button.Height = rect.Height;
            return button;
        }
        /*!
         * 
         */
        protected CDesignCheckBox NewCheckBoxEx(Rectangle rect)
        {
            CDesignCheckBox checkBox = (CDesignCheckBox)NewCheckBox();
            checkBox.SetGUID(Guid.NewGuid());
            checkBox.Name = string.Format("CheckBox{0}", CheckBoxList.Count);
            checkBox.Left = rect.Left;
            checkBox.Top = rect.Top;
            checkBox.Width = rect.Width;
            checkBox.Height = rect.Height;
            return checkBox;
        }
        /*!
         * 
         */
        protected CDesignRadioGroup NewRadioGroupEx(Rectangle rect)
        {
            CDesignRadioGroup radioGroup = (CDesignRadioGroup)NewRadioGroup();
            radioGroup.SetGUID(Guid.NewGuid());
            radioGroup.Name = string.Format("RadioGroup{0}", RadioGroupList.Count);
            radioGroup.Left = rect.Left;
            radioGroup.Top = rect.Top;
            radioGroup.Width = rect.Width;
            radioGroup.Height = rect.Height;
            return radioGroup;
        }
        /*!
         * 
         */
        protected CDesignMeter NewMeterEx(Rectangle rect)
        {
            CDesignMeter meter = (CDesignMeter)NewMeter();
            meter.SetGUID(Guid.NewGuid());
            meter.Name = string.Format("Meter{0}", MeterList.Count);
            meter.Left = rect.Left;
            meter.Top = rect.Top;
            meter.Width = rect.Width;
            meter.Height = rect.Height;
            return meter;
        }
        /*!
         * Cria novo objeto na tela.
         * Verifica se rect não esta menor que o tamanho default, e 
         * corrige se estiver.
         */
        protected void addObject(Rectangle rect)
        {
            if (rect.Width < DEFAULT_OBJECT_WIDTH)
                rect.Width = DEFAULT_OBJECT_WIDTH;
            if (rect.Height < DEFAULT_OBJECT_HEIGHT)
                rect.Height = DEFAULT_OBJECT_HEIGHT;
            switch (createID)
            {
                case CCustomScreen.CREATE_ID_TEXT:
                    {
                        NewTextEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_PICTURE:
                    {
                        NewPictureEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_ALPHANUMERIC_FIELD:
                    {
                        NewAlphaNumericEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_DINAMIC_TEXT:
                    {
                        NewDinamicTextEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_ANIMATION:
                    {
                        NewAnimationEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_TREND_CHART:
                    {
                        NewTrendChartEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_SHAPE:
                    {
                        NewShapeEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_BUTTON:
                    {
                        NewButtonEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_BARGRAPH:
                    {
                        NewBargraphEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_CHECKBOX:
                    {
                        NewCheckBoxEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_RADIOGROUP:
                    {
                        NewRadioGroupEx(rect);
                        createID = 0;
                    }; break;
                case CCustomScreen.CREATE_ID_METER:
                    {
                        NewMeterEx(rect);
                        createID = 0;
                    }; break;
                default:
                    {

                        createID = 0;
                    }; break;
            }
        }
        /*!
         * 
         */
        protected override void form_MouseUp(object sender, MouseEventArgs e)
        {
            base.form_MouseUp(sender, e);
            //
            if (createID != 0)
            {
                Rectangle rect = CreateNormalizedRect(beginPoint, endPoint);
                addObject(rect);
            }
        }        
        /*!
         * Retorna lista de objetos selecionados
         * @return Lista de objetos
         */        
        public ArrayList GetSelectedObjects()
        {
            //
            List<CDesignScreenObject> list1 = 
                ObjectList.OfType<CDesignScreenObject>().Where(
                obj => obj.Selected == true
                ).ToList();
            return new ArrayList(list1);           
        }
        /*!
         * Evento OnKeyDown. 
         * @param sender: Objeto que chamou o evento
         * @param e: Argumentos do evento OnKeyDown
         */
        protected override void form_KeyDown(object sender, KeyEventArgs e)
        {
            shift = e.Shift;
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Z:
                        Undo();
                        break;
                    case Keys.C:
                        Copy();
                        break;
                    case Keys.V:
                        Paste();
                        break;     
                    //não passa eventos para classe base
                    //e.Handled = true;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        Delete();
                        break;
                    
                    //não passa eventos para classe base
                    //e.Handled = true;
                }
            }
        }
        /*!
         * 
         */
        protected override void form_KeyUp(object sender, KeyEventArgs e)
        {
            shift = false;            
        }
        
        /*!
         * 
         */
        protected override void form_Click(object sender, EventArgs e)
        {
            //Ativa form para que possa receber eventos do teclado
            form.Select();
            //
            if (createID == 0)
            {
                OnSelectedObject(new SelectedObjectEventArgs(true));
                UnSelectedObjects(null);
                shift = false;
            }            
        }
        /*!
         * 
         */
        private void SetTabOrder(bool state)
        {
            List<CDesignCustomField> list = ObjectList.OfType<CDesignCustomField>().ToList();
            foreach (CDesignCustomField item in list)
            {
                item.tabOrder = state;
                if (state == false)
                    item.isOrdenated = false;
            }
            if (state == false)
                tabOrderCounter = 0;
            //
            form.Invalidate(true);
        }
        /*!
         * 
         */
        private void miTabOrder_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked ^= true;
            SetTabOrder(((ToolStripMenuItem)sender).Checked);
        }

        #region Copy,Paste,Cut e Delete        
        
        private void Copy()
        {
            m_offset = 0;
            ArrayList list = GetSelectedObjects();
            if (list.Count > 0)
                editCommand.Copy(this, list);
        }
        private void Paste()
        {
            editCommand.Paste(this);
        }
        private void Cut()
        {
            ArrayList list = GetSelectedObjects();
            if (list.Count > 0)
                editCommand.Cut(this, list);
        }

        /*!
         * Deleta objetos passados na lista
         */
        private void Delete()
        {
            ArrayList list = GetSelectedObjects();
            if (list.Count > 0)
            {
                if (MessageBox.Show(form,
                                    "Delete selected objects ?",
                                    Application.ProductName,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    editCommand.Delete(this, list);
                }
            }            
        }

        private void Undo()
        {
            editCommand.Undo(this);
        }

        private void miCopy_Click(object sender, EventArgs e)
        {
            Copy();           
        }

        private void miPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void miCut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        #endregion
        private int m_offset = 0;
        private void editCommand_PasteCommand(object sender, PasteCommandEventArgs e)
        {
            m_offset += form.GetGridStep();
            foreach (CDesignScreenObject item in e.ObjectList)
            {
                item.SetGUID(Guid.NewGuid());
                item.Left += m_offset;
                item.Top += m_offset;
            }                
        }
    }
}
