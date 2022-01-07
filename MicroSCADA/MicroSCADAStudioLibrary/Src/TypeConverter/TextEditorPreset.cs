using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using MicroSCADAStudioLibrary.Src.Forms;

namespace MicroSCADAStudioLibrary.Src.TypeConverter
{
    
    public class CTextEditorPreset : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            TextEditorForm textEditor = new TextEditorForm((string)value);

            if (textEditor.ShowDialog() == DialogResult.OK)
                value = textEditor.Value;
            
            return value;
        }
    }
}
