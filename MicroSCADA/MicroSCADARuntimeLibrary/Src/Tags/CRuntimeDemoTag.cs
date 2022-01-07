using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src.Tags
{
    public class CRuntimeDemoTag : CRuntimeDinamicTag, ICustomDemoTag
    {
        protected CCustomDemoTag customDemoTag;
        
        public CRuntimeDemoTag(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customDemoTag = new CCustomDemoTag();            
        }
        #region Propriedades
        public override CDemoType Type
        {
            get { return this.customDemoTag.type; }
            set { this.customDemoTag.type = value; }
        }
        public float MaxValue
        {
            get { return this.customDemoTag.maxValue; }
            set { this.customDemoTag.maxValue = value; }
        }
        public float MinValue
        {
            get { return this.customDemoTag.minValue; }
            set { this.customDemoTag.minValue = value; }
        }        
        public float Increment
        {
            get { return this.customDemoTag.increment; }
            set { this.customDemoTag.increment = value; }
        }
        #endregion
        private float v2 = 0;
        private void DoStepRandom()
        {            
            float v1;
            Random random = new Random();
            v1 = random.Next((int)MinValue, (int)MaxValue);
            v1 = (v1 + v2) / 2;
            v2 = v1;
            Value = v1.ToString();
        }
        /*!
         * 
         */
        private int degree = 0; 
        protected void DoStepSine()
        {
            float radians, midValue, v;
            radians = (float)((degree * Math.PI) / 180F);
            midValue = (MaxValue - MinValue) / 2;
            v = (float)((midValue * Math.Sin(radians)) + midValue);
            degree += (int)Increment;
            Value = v.ToString();
        }
        /*!
         * 
         */
        protected void DoStepSquare()
        {
            float v = float.Parse(GetValue());

            if (v == MaxValue)
                v = MinValue;
            else
                v = MaxValue;
            Value = v.ToString();
        }
        /*!
         * 
         */
        private bool up = false;
        protected void DoStepTriangulate()
        {
            float v = float.Parse(GetValue());

            if (up)
            {
                if (v < MaxValue)
                    v += Increment;
                else
                {
                    up = false;
                    v -= Increment;
                }
            }
            else
            {
                if (v > MinValue)
                    v -= Increment;
                else
                {
                    up = true;
                    v += Increment;
                }
            }
            Value = v.ToString();
        }
        /*!
         * 
         */
        protected void DoStepTriangulateLeft()
        {
            float v = float.Parse(GetValue());

            if (v < MaxValue)
                v += Increment;
            else
                v = MinValue;
            Value = v.ToString();
        }
        /*!
         * 
         */
        protected void DoStepTriangulateRight()
        {
            float v = float.Parse(GetValue());

            if (v > MinValue)
                v -= Increment;
            else
                v = MaxValue;
            Value = v.ToString();
        }
        /*!
         * 
         */
        public override void DoStep()
        {
            this.customTag.dataType = CCustomDataType.dtFloat32;
            switch (Type)
            {      
                case CDemoType.dtRandom:
                    DoStepRandom();
                    break;
                case CDemoType.dtSine:
                    DoStepSine();
                    break;
                case CDemoType.dtSquare:
                    DoStepSquare();
                    break;
                case CDemoType.dtTriangulate:
                    DoStepTriangulate();
                    break;
                case CDemoType.dtTriangulateLeft:
                    DoStepTriangulateLeft();
                    break;
                case CDemoType.dtTriangulateRight:
                    DoStepTriangulateRight();
                    break;
            }
        }   
    }
}
