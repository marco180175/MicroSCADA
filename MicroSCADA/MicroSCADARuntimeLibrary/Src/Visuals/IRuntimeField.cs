using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{

    public interface IRuntimeField
    {
        void SetValue(string Value);
        string GetValue();
        PictureBox getPictureBox();
        //event FieldEditValueEventHandler EditValueEvent;
    }
}
