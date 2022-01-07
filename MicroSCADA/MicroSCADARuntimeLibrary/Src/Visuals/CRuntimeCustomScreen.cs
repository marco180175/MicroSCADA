using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public abstract class CRuntimeCustomScreen : CRuntimeView, ICustomScreen
    {
        protected CCustomScreen customScreen;
        protected int indexBitmapItem;
        protected ArrayList editTableFields;
        protected int tabIndex;        
        /*!
         * Construtor
         */
        public CRuntimeCustomScreen(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customScreen = new CCustomScreen();
            this.indexBitmapItem = this.ReferenceList.AddReference();
            this.tabIndex = 0;
        }
        // Propriedade Color
        public Color BackColor
        {
            get { return this.customScreen.backColor; }
            set { this.customScreen.backColor = value; }
        }      
        public ICustomBitmapItem BitmapItem
        {
            get { return this.GetBitmapItem(); }
            set { this.SetBitmapItem(value); }
        }
        private void SetBitmapItem(ICustomBitmapItem Value)
        {
            this.SetReference(indexBitmapItem, Value);
        }
        private ICustomBitmapItem GetBitmapItem()
        {
            return (ICustomBitmapItem)this.GetReference(indexBitmapItem);
        }
        public void SetGuidBitmapItem(Guid Value)
        {
            this.SetReferenceGuid(indexBitmapItem, Value);
        }
        public CRuntimeCustomField CurrentField
        {
            get { return (CRuntimeCustomField)editTableFields[tabIndex]; }
        }
        /*!
         * 
         * @return
         */
        public ICustomText NewText()
        {
            CRuntimeText text;

            text = new CRuntimeText(this, project);
            ObjectList.Add(text);
            return text;
        }
        /*!
         * 
         * @return
         */
        public ICustomPicture NewPicture()
        {
            CRuntimePicture picture;

            picture = new CRuntimePicture(this, project);
            ObjectList.Add(picture);
            return picture;
        }
        /*!
         * 
         * @return
         */
        public ICustomShape NewShape()
        {
            CRuntimeShape shape;

            shape = new CRuntimeShape(this, project);
            ObjectList.Add(shape);
            return shape;
        }
        /*!
         * 
         */
        public ICustomAlphaNumeric NewAlphaNumeric()
        {
            CRuntimeAlphaNumeric alphaNumeric;

            alphaNumeric = new CRuntimeAlphaNumeric(this, project);
            ObjectList.Add(alphaNumeric);            
            return alphaNumeric;
        }
        /*!
         * 
         */
        public ICustomTrendChart NewTrendChart()
        {
            CRuntimeTrendChart trendChart;

            trendChart = new CRuntimeTrendChart(this, project);
            ObjectList.Add(trendChart);
            return trendChart;
        }
        /*!
         * 
         */
        public ICustomBargraph NewBargraph()
        {
            CRuntimeBargraph bargraph;

            bargraph = new CRuntimeBargraph(this, project);
            ObjectList.Add(bargraph);
            return bargraph;
        }
        /*!
         * 
         */
        public ICustomButton NewButton()
        {
            CRuntimeButton button;

            button = new CRuntimeButton(this, project);
            ObjectList.Add(button);
            return button;
        }
        /*!
         * 
         */
        public ICustomDinamicText NewDinamicText()
        {
            CRuntimeDinamicText dinamicText;

            dinamicText = new CRuntimeDinamicText(this, project);
            ObjectList.Add(dinamicText);
            return dinamicText;
        }
        /*!
         * 
         */
        public ICustomAnimation NewAnimation()
        {
            CRuntimeAnimation animation;

            animation = new CRuntimeAnimation(this, project);
            ObjectList.Add(animation);
            return animation;
        }
        /*!
         * 
         */
        public ICustomCheckBox NewCheckBox()
        {
            CRuntimeCheckBox checkBox = new CRuntimeCheckBox(this, project);
            ObjectList.Add(checkBox);
            return checkBox;
        }
        /*!
         * 
         */
        public ICustomRadioGroup NewRadioGroup()
        {
            CRuntimeRadioGroup radioGroup = new CRuntimeRadioGroup(this, project);
            ObjectList.Add(radioGroup);
            return radioGroup;
        }
        /*!
         * 
         */
        public ICustomMeter NewMeter()
        {
            ICustomMeter meter = new CRuntimeMeter(this, project);
            ObjectList.Add(meter);
            return meter;            
        }

        //public void SetTabIndex(CRuntimeCustomField field)
        //{
        //    tabIndex = editTableFields.IndexOf(field);
        //    SetFocus();
        //}

        /*!
         * 
         */
        //protected void SetFocus()
        //{
        //    CRuntimeCustomField screenObject;
        //    for (int i = 0; i < editTableFields.Count; i++)
        //    {
        //        screenObject = (CRuntimeCustomField)editTableFields[i];

        //        if (i == tabIndex)
        //        {
        //            screenObject.focus = true;
        //        }
        //        else
        //        {
        //            screenObject.focus = false;
        //        }
        //        screenObject.getPictureBox().Invalidate();
        //    }
        //}
    }
}
