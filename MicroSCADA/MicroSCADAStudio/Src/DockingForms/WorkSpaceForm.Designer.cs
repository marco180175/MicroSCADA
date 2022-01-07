namespace MicroSCADAStudio.Src.DockingForms
{
    partial class WorkSpaceForm
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
            this.tabControl1 = new Crownwood.Magic.Controls.TabControl();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PositionTop = true;
            this.tabControl1.ShowArrows = true;
            this.tabControl1.ShowClose = true;
            this.tabControl1.Size = new System.Drawing.Size(582, 311);
            this.tabControl1.TabIndex = 13;
            this.tabControl1.ClosePressed += new System.EventHandler(this.tabControl1_ClosePressed);
            // 
            // WorkSpaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 311);
            this.Controls.Add(this.tabControl1);
            this.Name = "WorkSpaceForm";
            this.TabText = "WorkSpaceForm";
            this.Text = "WorkSpaceForm";
            this.ResumeLayout(false);

        }

        #endregion

        public Crownwood.Magic.Controls.TabControl tabControl1;

    }
}