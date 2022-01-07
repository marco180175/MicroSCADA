using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace MicroSCADACustomLibrary.Src.Visuals
{
    public enum CIdCreateObject
    {
        IdText,
        IdPicture,
        IdAlphaNumeric
    }

    public interface ICustomScreen : ICustomSystem
    {
        int Width { get; set; }
        int Height { get; set; }
        Color BackColor { get; set; }
        ICustomBitmapItem BitmapItem { get; set; }
        void SetGuidBitmapItem(Guid Value);
        ICustomText NewText();
        ICustomPicture NewPicture();
        ICustomShape NewShape();
        ICustomAlphaNumeric NewAlphaNumeric();
        ICustomTrendChart NewTrendChart();
        ICustomButton NewButton();
        ICustomBargraph NewBargraph();
        ICustomDinamicText NewDinamicText();
        ICustomAnimation NewAnimation();
        ICustomCheckBox NewCheckBox();
        ICustomRadioGroup NewRadioGroup();
        ICustomMeter NewMeter();
    }

    public interface ICustomPopUpScreen : ICustomScreen
    {
        int Left { get; set; }
        int Top { get; set; }
        bool ShowTitleBar { get; set; }
        string Title { get; set; }
    }

    public class CCustomScreen : Object
    {
        public Color backColor;
        public const int CREATE_ID_TEXT = 1;
        public const int CREATE_ID_PICTURE = 2;
        public const int CREATE_ID_ALPHANUMERIC_FIELD = 3;
        public const int CREATE_ID_DINAMIC_TEXT = 4;
        public const int CREATE_ID_TREND_CHART = 5;
        public const int CREATE_ID_SHAPE = 6;
        public const int CREATE_ID_BUTTON = 7;
        public const int CREATE_ID_BARGRAPH = 8;
        public const int CREATE_ID_ANIMATION = 9;
        public const int CREATE_ID_CHECKBOX = 10;
        public const int CREATE_ID_RADIOGROUP = 11;
        public const int CREATE_ID_METER = 12;
        //Construtor
        public CCustomScreen()
            : base()
        {
            this.backColor = Color.White;
        }
        //Destructor
        ~CCustomScreen()
        {
        }
            
        
        /*
        public override void LinkObjects()
        {
            CCustomPanel customPanel;
            int i;
            for (i = 0; i < objectList.Count; i++)
            {
                customPanel = (CCustomPanel)objectList[i];
                customPanel.LinkObjects();
            }
        }
         */
        public void setParent(Control parent)
        {
            //int i;
            //CCustomPanel customPanel;
            /*
            for (i = 0; i < objectList.Count; i++)
            {
                customPanel = (CCustomPanel)objectList[i];
                customPanel.getPictureBox().Parent = parent;
            }
             */
        }
    }
}
