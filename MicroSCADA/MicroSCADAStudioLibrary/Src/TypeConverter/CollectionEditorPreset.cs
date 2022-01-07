using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADAStudioLibrary.Src.Forms;
using MicroSCADAStudioLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.TypeConverter
{
    
    public class CollectionEditorPreset : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            CollectionEditorForm collectionEditor;
            collectionEditor = new CollectionEditorForm((CDesignScreenObject)context.Instance);
            collectionEditor.ShowDialog();
            return value;
        }
    }
}
