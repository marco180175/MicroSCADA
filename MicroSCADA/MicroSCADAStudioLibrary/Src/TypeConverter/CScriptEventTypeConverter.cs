using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using MicroSCADAStudioLibrary.Src.Forms;

namespace MicroSCADAStudioLibrary.Src.TypeConverter
{   
    public class CScriptEventTypeConverter : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //ScriptEditorForm scriptEditor;
            //scriptEditor = new ScriptEditorForm();
            //CDesignScript script = (CDesignScript)value;
            //scriptEditor.Script = script.Script;            
            
            //if (scriptEditor.ShowDialog(MicroSCADAStudio.MainFormDesign.ActiveForm) == DialogResult.OK)
            //    script.Script = scriptEditor.Script;
            
            return value;            
        }
    }
}
