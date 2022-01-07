using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADAStudioLibrary.Src.Forms;

namespace MicroSCADAStudioLibrary.Src.TypeConverter
{
    public class CActionTypeConverter : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ActionEditorForm actionEditor = new ActionEditorForm((CDesignAction)value);

            //actionEditor.Action = (CDesignAction)value;
            if (actionEditor.ShowDialog() == DialogResult.OK)
                return actionEditor.Action;
            else
                return value;
        }
    }
}
