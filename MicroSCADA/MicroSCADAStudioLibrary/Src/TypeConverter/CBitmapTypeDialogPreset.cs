using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using MicroSCADAStudioLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.TypeConverter
{
    public class CBitmapDialogTypeEditor : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        //public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = CDesignPicture.IMAGE_FILTER;
        //    openFileDialog.FileName = "";
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        value = openFileDialog.FileName;           
        //    openFileDialog.Dispose();
        //    return value;            
        //}

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            OpenFileDialog openFileDialog;

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = CDesignPicture.IMAGE_FILTER;
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                CDesignBitmapList bitmapList = (CDesignBitmapList)(CDesignProject.getInstance().BitmapList);
                value = bitmapList.AddBitmap(openFileDialog.FileName);
            }
            
            openFileDialog.Dispose();
            return value;
        }
    }
}
