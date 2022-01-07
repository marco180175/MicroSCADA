namespace MicroSCADAStudio.Src.DockingForms
{
    partial class ProjectManagerForm
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
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddTagDemo = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddTagRam = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddTagProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.addTextZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPictureZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddGroupOfInternalTags = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddAction = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddActionLine = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewTagTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(298, 448);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.addTextZoneToolStripMenuItem,
            this.addGroupToolStripMenuItem,
            this.addBarToolStripMenuItem,
            this.addPictureZoneToolStripMenuItem,
            this.miAddGroupOfInternalTags,
            this.miAddAction,
            this.miAddActionLine});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 268);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem2.Text = "Add slave";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem3.Text = "Add tag";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddTagDemo,
            this.miAddTagRam,
            this.miAddTagProperty,
            this.miNewTagTimer});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem4.Text = "Add internal tag";
            // 
            // miAddTagDemo
            // 
            this.miAddTagDemo.Name = "miAddTagDemo";
            this.miAddTagDemo.Size = new System.Drawing.Size(152, 22);
            this.miAddTagDemo.Text = "tag demo";
            this.miAddTagDemo.Click += new System.EventHandler(this.tagDemoToolStripMenuItem_Click);
            // 
            // miAddTagRam
            // 
            this.miAddTagRam.Name = "miAddTagRam";
            this.miAddTagRam.Size = new System.Drawing.Size(152, 22);
            this.miAddTagRam.Text = "tag ram";
            this.miAddTagRam.Click += new System.EventHandler(this.tagRamToolStripMenuItem_Click);
            // 
            // miAddTagProperty
            // 
            this.miAddTagProperty.Name = "miAddTagProperty";
            this.miAddTagProperty.Size = new System.Drawing.Size(152, 22);
            this.miAddTagProperty.Text = "tag property";
            this.miAddTagProperty.Click += new System.EventHandler(this.miAddTagProperty_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem5.Text = "Add pen";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // addTextZoneToolStripMenuItem
            // 
            this.addTextZoneToolStripMenuItem.Name = "addTextZoneToolStripMenuItem";
            this.addTextZoneToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.addTextZoneToolStripMenuItem.Text = "Add text zone";
            this.addTextZoneToolStripMenuItem.Click += new System.EventHandler(this.addTextZoneToolStripMenuItem_Click);
            // 
            // addGroupToolStripMenuItem
            // 
            this.addGroupToolStripMenuItem.Name = "addGroupToolStripMenuItem";
            this.addGroupToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.addGroupToolStripMenuItem.Text = "Add group";
            this.addGroupToolStripMenuItem.Click += new System.EventHandler(this.addGroupToolStripMenuItem_Click);
            // 
            // addBarToolStripMenuItem
            // 
            this.addBarToolStripMenuItem.Name = "addBarToolStripMenuItem";
            this.addBarToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.addBarToolStripMenuItem.Text = "Add bar";
            this.addBarToolStripMenuItem.Click += new System.EventHandler(this.addBarToolStripMenuItem_Click);
            // 
            // addPictureZoneToolStripMenuItem
            // 
            this.addPictureZoneToolStripMenuItem.Name = "addPictureZoneToolStripMenuItem";
            this.addPictureZoneToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.addPictureZoneToolStripMenuItem.Text = "Add picture zone";
            this.addPictureZoneToolStripMenuItem.Click += new System.EventHandler(this.addPictureZoneToolStripMenuItem_Click);
            // 
            // miAddGroupOfInternalTags
            // 
            this.miAddGroupOfInternalTags.Name = "miAddGroupOfInternalTags";
            this.miAddGroupOfInternalTags.Size = new System.Drawing.Size(164, 22);
            this.miAddGroupOfInternalTags.Text = "Add group";
            this.miAddGroupOfInternalTags.Click += new System.EventHandler(this.miAddGroupOfInternalTags_Click);
            // 
            // miAddAction
            // 
            this.miAddAction.Name = "miAddAction";
            this.miAddAction.Size = new System.Drawing.Size(164, 22);
            this.miAddAction.Text = "Add action";
            this.miAddAction.Click += new System.EventHandler(this.miAddAction_Click);
            // 
            // miAddActionLine
            // 
            this.miAddActionLine.Name = "miAddActionLine";
            this.miAddActionLine.Size = new System.Drawing.Size(164, 22);
            this.miAddActionLine.Text = "Add action line";
            this.miAddActionLine.Click += new System.EventHandler(this.miAddActionLine_Click);
            // 
            // miNewTagTimer
            // 
            this.miNewTagTimer.Name = "miNewTagTimer";
            this.miNewTagTimer.Size = new System.Drawing.Size(152, 22);
            this.miNewTagTimer.Text = "tag timer";
            this.miNewTagTimer.Click += new System.EventHandler(this.miNewTagTimer_Click);
            // 
            // ProjectManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 448);
            this.Controls.Add(this.treeView1);
            this.Name = "ProjectManagerForm";
            this.TabText = "FormProjectManager";
            this.Text = "FormProjectManager";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miAddTagDemo;
        private System.Windows.Forms.ToolStripMenuItem miAddTagRam;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem addTextZoneToolStripMenuItem;
        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem addGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPictureZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miAddGroupOfInternalTags;
        private System.Windows.Forms.ToolStripMenuItem miAddAction;
        private System.Windows.Forms.ToolStripMenuItem miAddTagProperty;
        private System.Windows.Forms.ToolStripMenuItem miAddActionLine;
        private System.Windows.Forms.ToolStripMenuItem miNewTagTimer;


    }
}