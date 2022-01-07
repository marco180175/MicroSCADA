using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public interface ICustomBargraphElement : ICustomSystem
    {
        Color BarColor { get; set; }        
        void SetGuidTagValue(Guid Value);
        ICustomTag TagValue { get; set; }
    }

    public class CCustomBargraphElement
    {
        public Color barColor;
        public CCustomBargraphElement()
        {
            this.barColor = Color.Black;
        }
    }
}
