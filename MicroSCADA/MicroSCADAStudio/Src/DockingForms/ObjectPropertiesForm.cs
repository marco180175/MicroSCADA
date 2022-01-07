using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudio.Src.DockingForms
{
    //public partial class ObjectPropertiesForm : Form
    public partial class ObjectPropertiesForm : DockContent 
    {
        /*!
         * Construtor
         * @param ImageList: Referencia para imagelist do form principal
         */
        public ObjectPropertiesForm(ImageList imageList)
        {
            InitializeComponent();
            this.lvCrossReference.SmallImageList = imageList;            
        }
        public Object SelectedObject 
        {
            get { return this.propertyGrid1.SelectedObject; }
            set { this.SetSelectedObject(value); }
        }

        private void SetSelectedObject(Object Value)
        {
            //
            propertyGrid1.SelectedObject = Value;
            lvCrossReference.Items.Clear();
            //
            CDesignSystem designSystem = propertyGrid1.SelectedObject as CDesignSystem;
            if (designSystem != null)
            {
                foreach (CDesignSystem item in designSystem.CrossReferenceList)
                {
                    lvCrossReference.Items.Add(item.Name, item.ImageIndex);
                }
            }
        }

        public void Refresh()
        {
            propertyGrid1.Refresh();
        }
        
        private void lvCrossReference_ClientSizeChanged(object sender, EventArgs e)
        {
            lvCrossReference.Columns[0].Width = lvCrossReference.ClientSize.Width;
        }
    }
}
