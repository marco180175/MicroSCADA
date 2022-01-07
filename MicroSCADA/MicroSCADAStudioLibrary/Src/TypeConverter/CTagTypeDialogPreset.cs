using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using MicroSCADAStudioLibrary.Src.Forms;

namespace MicroSCADAStudioLibrary.Src.TypeConverter
{    
    public class CTagTypeDialogPreset : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ApplicationBrowserForm formSelectTag;
            
            formSelectTag = new ApplicationBrowserForm(true);
            //if (formSelectTag.ShowDialog(MicroSCADAStudio.MainFormDesign.ActiveForm) == DialogResult.OK)
            if (formSelectTag.ShowDialog() == DialogResult.OK)
                value = formSelectTag.SelectedObject;
            
            return value;
        }
    }
}
