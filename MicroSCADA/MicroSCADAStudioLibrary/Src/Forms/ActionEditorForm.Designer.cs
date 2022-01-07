namespace MicroSCADAStudioLibrary.Src.Forms
{
    partial class ActionEditorForm
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
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lbxActionLines = new System.Windows.Forms.ListBox();
            this.btAddAction = new System.Windows.Forms.Button();
            this.btAddLine = new System.Windows.Forms.Button();
            this.lbxCommand = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(271, 298);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(373, 298);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lbxActionLines
            // 
            this.lbxActionLines.FormattingEnabled = true;
            this.lbxActionLines.Location = new System.Drawing.Point(246, 166);
            this.lbxActionLines.Name = "lbxActionLines";
            this.lbxActionLines.Size = new System.Drawing.Size(222, 108);
            this.lbxActionLines.TabIndex = 3;
            // 
            // btAddAction
            // 
            this.btAddAction.Location = new System.Drawing.Point(12, 12);
            this.btAddAction.Name = "btAddAction";
            this.btAddAction.Size = new System.Drawing.Size(100, 23);
            this.btAddAction.TabIndex = 4;
            this.btAddAction.Text = "Add Action";
            this.btAddAction.UseVisualStyleBackColor = true;
            this.btAddAction.Click += new System.EventHandler(this.btAddAction_Click);
            // 
            // btAddLine
            // 
            this.btAddLine.Location = new System.Drawing.Point(246, 137);
            this.btAddLine.Name = "btAddLine";
            this.btAddLine.Size = new System.Drawing.Size(100, 23);
            this.btAddLine.TabIndex = 5;
            this.btAddLine.Text = "Add Line";
            this.btAddLine.UseVisualStyleBackColor = true;
            this.btAddLine.Click += new System.EventHandler(this.btAddLine_Click);
            // 
            // lbxCommand
            // 
            this.lbxCommand.FormattingEnabled = true;
            this.lbxCommand.Location = new System.Drawing.Point(246, 23);
            this.lbxCommand.Name = "lbxCommand";
            this.lbxCommand.Size = new System.Drawing.Size(222, 108);
            this.lbxCommand.TabIndex = 6;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(222, 225);
            this.listBox1.TabIndex = 7;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.lbxActionList_SelectedIndexChanged);
            // 
            // ActionEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 333);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lbxCommand);
            this.Controls.Add(this.btAddLine);
            this.Controls.Add(this.btAddAction);
            this.Controls.Add(this.lbxActionLines);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActionEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ActionEditorForm";
            this.Shown += new System.EventHandler(this.ActionEditorForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ListBox lbxActionLines;
        private System.Windows.Forms.Button btAddAction;
        private System.Windows.Forms.Button btAddLine;
        private System.Windows.Forms.ListBox lbxCommand;
        private System.Windows.Forms.ListBox listBox1;
    }
}