namespace MicroSCADAStudio.Src.Forms
{
    partial class TableTagForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableTagForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this.tcFullName = new Aga.Controls.Tree.TreeColumn();
            this.tcName = new Aga.Controls.Tree.TreeColumn();
            this.tcGUID = new Aga.Controls.Tree.TreeColumn();
            this.nodeStateIcon1 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this.ntbFullName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.ntbName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.ntbGUID = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.tcAddress = new Aga.Controls.Tree.TreeColumn();
            this.tcDataType = new Aga.Controls.Tree.TreeColumn();
            this.tcArraySize = new Aga.Controls.Tree.TreeColumn();
            this.ntbAddress = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.ntbDataType = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.ntbArraySize = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(476, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.Columns.Add(this.tcFullName);
            this.treeViewAdv1.Columns.Add(this.tcName);
            this.treeViewAdv1.Columns.Add(this.tcGUID);
            this.treeViewAdv1.Columns.Add(this.tcAddress);
            this.treeViewAdv1.Columns.Add(this.tcDataType);
            this.treeViewAdv1.Columns.Add(this.tcArraySize);
            this.treeViewAdv1.DefaultToolTipProvider = null;
            this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(0, 25);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.NodeControls.Add(this.nodeStateIcon1);
            this.treeViewAdv1.NodeControls.Add(this.ntbFullName);
            this.treeViewAdv1.NodeControls.Add(this.ntbName);
            this.treeViewAdv1.NodeControls.Add(this.ntbGUID);
            this.treeViewAdv1.NodeControls.Add(this.ntbAddress);
            this.treeViewAdv1.NodeControls.Add(this.ntbDataType);
            this.treeViewAdv1.NodeControls.Add(this.ntbArraySize);
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(476, 241);
            this.treeViewAdv1.TabIndex = 1;
            this.treeViewAdv1.Text = "treeViewAdv1";
            this.treeViewAdv1.UseColumns = true;
            // 
            // tcFullName
            // 
            this.tcFullName.Header = "FullName";
            this.tcFullName.SortOrder = System.Windows.Forms.SortOrder.None;
            this.tcFullName.TooltipText = null;
            // 
            // tcName
            // 
            this.tcName.Header = "Name";
            this.tcName.SortOrder = System.Windows.Forms.SortOrder.None;
            this.tcName.TooltipText = null;
            // 
            // tcGUID
            // 
            this.tcGUID.Header = "GUID";
            this.tcGUID.SortOrder = System.Windows.Forms.SortOrder.None;
            this.tcGUID.TooltipText = null;
            // 
            // nodeStateIcon1
            // 
            this.nodeStateIcon1.DataPropertyName = "Icon";
            this.nodeStateIcon1.LeftMargin = 1;
            this.nodeStateIcon1.ParentColumn = this.tcFullName;
            this.nodeStateIcon1.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // ntbFullName
            // 
            this.ntbFullName.DataPropertyName = "FullName";
            this.ntbFullName.IncrementalSearchEnabled = true;
            this.ntbFullName.LeftMargin = 3;
            this.ntbFullName.ParentColumn = this.tcFullName;
            // 
            // ntbName
            // 
            this.ntbName.DataPropertyName = "Name";
            this.ntbName.IncrementalSearchEnabled = true;
            this.ntbName.LeftMargin = 3;
            this.ntbName.ParentColumn = this.tcName;
            // 
            // ntbGUID
            // 
            this.ntbGUID.DataPropertyName = "GUID";
            this.ntbGUID.IncrementalSearchEnabled = true;
            this.ntbGUID.LeftMargin = 3;
            this.ntbGUID.ParentColumn = this.tcGUID;
            this.ntbGUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tcAddress
            // 
            this.tcAddress.Header = "Address";
            this.tcAddress.SortOrder = System.Windows.Forms.SortOrder.None;
            this.tcAddress.TooltipText = null;
            // 
            // tcDataType
            // 
            this.tcDataType.Header = "DataType";
            this.tcDataType.SortOrder = System.Windows.Forms.SortOrder.None;
            this.tcDataType.TooltipText = null;
            // 
            // tcArraySize
            // 
            this.tcArraySize.Header = "ArraySize";
            this.tcArraySize.SortOrder = System.Windows.Forms.SortOrder.None;
            this.tcArraySize.TooltipText = null;
            // 
            // ntbAddress
            // 
            this.ntbAddress.DataPropertyName = "Address";
            this.ntbAddress.IncrementalSearchEnabled = true;
            this.ntbAddress.LeftMargin = 3;
            this.ntbAddress.ParentColumn = this.tcAddress;
            // 
            // ntbDataType
            // 
            this.ntbDataType.DataPropertyName = "DataType";
            this.ntbDataType.IncrementalSearchEnabled = true;
            this.ntbDataType.LeftMargin = 3;
            this.ntbDataType.ParentColumn = this.tcDataType;
            // 
            // ntbArraySize
            // 
            this.ntbArraySize.DataPropertyName = "ArraySize";
            this.ntbArraySize.IncrementalSearchEnabled = true;
            this.ntbArraySize.LeftMargin = 3;
            this.ntbArraySize.ParentColumn = this.tcArraySize;
            // 
            // TableTagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 266);
            this.Controls.Add(this.treeViewAdv1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TableTagForm";
            this.Text = "TableTagForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private Aga.Controls.Tree.TreeColumn tcFullName;
        private Aga.Controls.Tree.TreeColumn tcName;
        private Aga.Controls.Tree.TreeColumn tcGUID;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ntbFullName;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ntbName;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ntbGUID;
        private Aga.Controls.Tree.NodeControls.NodeStateIcon nodeStateIcon1;
        private Aga.Controls.Tree.TreeColumn tcAddress;
        private Aga.Controls.Tree.TreeColumn tcDataType;
        private Aga.Controls.Tree.TreeColumn tcArraySize;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ntbAddress;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ntbDataType;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ntbArraySize;
    }
}