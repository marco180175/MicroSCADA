namespace MicroSCADAStudio.Src.Forms
{
    partial class PrefecencesForm
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
            this.tbWorkDirectory = new System.Windows.Forms.TextBox();
            this.btSelectWorkDirectory = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btClose = new System.Windows.Forms.Button();
            this.lbWorkDirectory = new System.Windows.Forms.Label();
            this.lbxRecentFiles = new System.Windows.Forms.ListBox();
            this.lbRecentFiles = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbWorkDirectory
            // 
            this.tbWorkDirectory.BackColor = System.Drawing.Color.White;
            this.tbWorkDirectory.Location = new System.Drawing.Point(12, 38);
            this.tbWorkDirectory.Name = "tbWorkDirectory";
            this.tbWorkDirectory.ReadOnly = true;
            this.tbWorkDirectory.Size = new System.Drawing.Size(433, 20);
            this.tbWorkDirectory.TabIndex = 0;
            // 
            // btSelectWorkDirectory
            // 
            this.btSelectWorkDirectory.Location = new System.Drawing.Point(451, 36);
            this.btSelectWorkDirectory.Name = "btSelectWorkDirectory";
            this.btSelectWorkDirectory.Size = new System.Drawing.Size(99, 23);
            this.btSelectWorkDirectory.TabIndex = 1;
            this.btSelectWorkDirectory.Text = "Select Directory...";
            this.btSelectWorkDirectory.UseVisualStyleBackColor = true;
            this.btSelectWorkDirectory.Click += new System.EventHandler(this.button1_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(12, 230);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 2;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lbWorkDirectory
            // 
            this.lbWorkDirectory.AutoSize = true;
            this.lbWorkDirectory.Location = new System.Drawing.Point(12, 22);
            this.lbWorkDirectory.Name = "lbWorkDirectory";
            this.lbWorkDirectory.Size = new System.Drawing.Size(78, 13);
            this.lbWorkDirectory.TabIndex = 4;
            this.lbWorkDirectory.Text = "Work Directory";
            // 
            // lbxRecentFiles
            // 
            this.lbxRecentFiles.FormattingEnabled = true;
            this.lbxRecentFiles.Location = new System.Drawing.Point(12, 94);
            this.lbxRecentFiles.Name = "lbxRecentFiles";
            this.lbxRecentFiles.Size = new System.Drawing.Size(538, 121);
            this.lbxRecentFiles.TabIndex = 5;
            // 
            // lbRecentFiles
            // 
            this.lbRecentFiles.AutoSize = true;
            this.lbRecentFiles.Location = new System.Drawing.Point(12, 78);
            this.lbRecentFiles.Name = "lbRecentFiles";
            this.lbRecentFiles.Size = new System.Drawing.Size(66, 13);
            this.lbRecentFiles.TabIndex = 6;
            this.lbRecentFiles.Text = "Recent Files";
            // 
            // PrefecencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 265);
            this.Controls.Add(this.lbRecentFiles);
            this.Controls.Add(this.lbxRecentFiles);
            this.Controls.Add(this.lbWorkDirectory);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btSelectWorkDirectory);
            this.Controls.Add(this.tbWorkDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PrefecencesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prefecences";
            this.Load += new System.EventHandler(this.PrefecencesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbWorkDirectory;
        private System.Windows.Forms.Button btSelectWorkDirectory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label lbWorkDirectory;
        private System.Windows.Forms.ListBox lbxRecentFiles;
        private System.Windows.Forms.Label lbRecentFiles;
    }
}