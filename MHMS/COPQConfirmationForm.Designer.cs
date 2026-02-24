namespace MHMS
{
    partial class COPQConfirmationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(COPQConfirmationForm));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ApproveButton = new System.Windows.Forms.Button();
            this.SectionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MHLossTypeDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ReasonTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LineStopDetail = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FileName = new System.Windows.Forms.TextBox();
            this.AttachedFileButton = new System.Windows.Forms.Button();
            this.TopPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.panel1);
            this.TopPanel.Controls.Add(this.SectionLabel);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(624, 48);
            this.TopPanel.TabIndex = 35;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ApproveButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(486, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 48);
            this.panel1.TabIndex = 30;
            // 
            // ApproveButton
            // 
            this.ApproveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(111)))), ((int)(((byte)(238)))));
            this.ApproveButton.FlatAppearance.BorderSize = 0;
            this.ApproveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApproveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApproveButton.ForeColor = System.Drawing.Color.White;
            this.ApproveButton.Location = new System.Drawing.Point(52, 9);
            this.ApproveButton.Name = "ApproveButton";
            this.ApproveButton.Size = new System.Drawing.Size(74, 30);
            this.ApproveButton.TabIndex = 30;
            this.ApproveButton.Text = "APPROVE";
            this.ApproveButton.UseVisualStyleBackColor = false;
            this.ApproveButton.Click += new System.EventHandler(this.ApproveButton_Click);
            // 
            // SectionLabel
            // 
            this.SectionLabel.AutoSize = true;
            this.SectionLabel.ForeColor = System.Drawing.Color.White;
            this.SectionLabel.Location = new System.Drawing.Point(286, 18);
            this.SectionLabel.Name = "SectionLabel";
            this.SectionLabel.Size = new System.Drawing.Size(43, 13);
            this.SectionLabel.TabIndex = 29;
            this.SectionLabel.Text = "Section";
            this.SectionLabel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "COPQ Confirmation";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.MHLossTypeDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(12, 135);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(337, 35);
            this.panel3.TabIndex = 36;
            // 
            // MHLossTypeDropdown
            // 
            this.MHLossTypeDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.MHLossTypeDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.MHLossTypeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MHLossTypeDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MHLossTypeDropdown.FormattingEnabled = true;
            this.MHLossTypeDropdown.Items.AddRange(new object[] {
            "Man-Hour (Linestop)",
            "Man-Hour (Add process)",
            "Man-Hour (Rework)",
            "Man-Hour (Others)",
            "No COPQ Needed (Delay)",
            "Tendency Check"});
            this.MHLossTypeDropdown.Location = new System.Drawing.Point(105, 5);
            this.MHLossTypeDropdown.Name = "MHLossTypeDropdown";
            this.MHLossTypeDropdown.Size = new System.Drawing.Size(227, 23);
            this.MHLossTypeDropdown.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel4.CausesValidation = false;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(99, 33);
            this.panel4.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(1, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "MH Loss Type";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBox1.Controls.Add(this.ReasonTextBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 153);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reason";
            // 
            // ReasonTextBox
            // 
            this.ReasonTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ReasonTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReasonTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReasonTextBox.Location = new System.Drawing.Point(13, 20);
            this.ReasonTextBox.Multiline = true;
            this.ReasonTextBox.Name = "ReasonTextBox";
            this.ReasonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ReasonTextBox.Size = new System.Drawing.Size(575, 120);
            this.ReasonTextBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LineStopDetail);
            this.groupBox2.Location = new System.Drawing.Point(12, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 70);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line Stop Detail";
            // 
            // LineStopDetail
            // 
            this.LineStopDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineStopDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineStopDetail.ForeColor = System.Drawing.SystemColors.Highlight;
            this.LineStopDetail.Location = new System.Drawing.Point(6, 17);
            this.LineStopDetail.Multiline = true;
            this.LineStopDetail.Name = "LineStopDetail";
            this.LineStopDetail.Size = new System.Drawing.Size(588, 46);
            this.LineStopDetail.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.FileName);
            this.panel2.Location = new System.Drawing.Point(118, 344);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 30);
            this.panel2.TabIndex = 37;
            // 
            // FileName
            // 
            this.FileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileName.Location = new System.Drawing.Point(3, 5);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(487, 18);
            this.FileName.TabIndex = 1;
            // 
            // AttachedFileButton
            // 
            this.AttachedFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.AttachedFileButton.FlatAppearance.BorderSize = 0;
            this.AttachedFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AttachedFileButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttachedFileButton.ForeColor = System.Drawing.Color.White;
            this.AttachedFileButton.Location = new System.Drawing.Point(12, 344);
            this.AttachedFileButton.Name = "AttachedFileButton";
            this.AttachedFileButton.Size = new System.Drawing.Size(100, 30);
            this.AttachedFileButton.TabIndex = 36;
            this.AttachedFileButton.Text = "Attached File";
            this.AttachedFileButton.UseVisualStyleBackColor = false;
            this.AttachedFileButton.Click += new System.EventHandler(this.AttachedFileButton_Click);
            // 
            // COPQConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(624, 385);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.AttachedFileButton);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "COPQConfirmationForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COPQ Confirmation";
            this.Load += new System.EventHandler(this.COPQConfirmationForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ApproveButton;
        private System.Windows.Forms.Label SectionLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox MHLossTypeDropdown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ReasonTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox LineStopDetail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Button AttachedFileButton;
    }
}