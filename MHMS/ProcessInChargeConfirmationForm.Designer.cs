namespace MHMS
{
    partial class ProcessInChargeConfirmationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessInChargeConfirmationForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CauseTextBox = new System.Windows.Forms.TextBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ApproveButton = new System.Windows.Forms.Button();
            this.SectionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CountermeasureTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FileName = new System.Windows.Forms.TextBox();
            this.AttachedFileButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LineStopDetail = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBox1.Controls.Add(this.CauseTextBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 153);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cause";
            // 
            // CauseTextBox
            // 
            this.CauseTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.CauseTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CauseTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CauseTextBox.Location = new System.Drawing.Point(13, 20);
            this.CauseTextBox.Multiline = true;
            this.CauseTextBox.Name = "CauseTextBox";
            this.CauseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CauseTextBox.Size = new System.Drawing.Size(575, 120);
            this.CauseTextBox.TabIndex = 0;
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
            this.TopPanel.TabIndex = 36;
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
            this.label1.Size = new System.Drawing.Size(242, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Process In-Charge Confirmation";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBox2.Controls.Add(this.CountermeasureTextBox);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 275);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(601, 153);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Countermeasure";
            // 
            // CountermeasureTextBox
            // 
            this.CountermeasureTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.CountermeasureTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CountermeasureTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountermeasureTextBox.Location = new System.Drawing.Point(13, 20);
            this.CountermeasureTextBox.Multiline = true;
            this.CountermeasureTextBox.Name = "CountermeasureTextBox";
            this.CountermeasureTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CountermeasureTextBox.Size = new System.Drawing.Size(575, 120);
            this.CountermeasureTextBox.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.FileName);
            this.panel2.Location = new System.Drawing.Point(118, 441);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 30);
            this.panel2.TabIndex = 39;
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
            this.AttachedFileButton.Location = new System.Drawing.Point(12, 441);
            this.AttachedFileButton.Name = "AttachedFileButton";
            this.AttachedFileButton.Size = new System.Drawing.Size(100, 30);
            this.AttachedFileButton.TabIndex = 38;
            this.AttachedFileButton.Text = "Attached File";
            this.AttachedFileButton.UseVisualStyleBackColor = false;
            this.AttachedFileButton.Click += new System.EventHandler(this.AttachedFileButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LineStopDetail);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(601, 51);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Line Stop Detail";
            // 
            // LineStopDetail
            // 
            this.LineStopDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineStopDetail.ForeColor = System.Drawing.SystemColors.Highlight;
            this.LineStopDetail.Location = new System.Drawing.Point(6, 17);
            this.LineStopDetail.Multiline = true;
            this.LineStopDetail.Name = "LineStopDetail";
            this.LineStopDetail.Size = new System.Drawing.Size(589, 28);
            this.LineStopDetail.TabIndex = 0;
            // 
            // ProcessInChargeConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(624, 483);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.AttachedFileButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProcessInChargeConfirmationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process In-Charge Confirmation";
            this.Load += new System.EventHandler(this.ProcessInChargeConfirmationForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox CauseTextBox;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ApproveButton;
        private System.Windows.Forms.Label SectionLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox CountermeasureTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Button AttachedFileButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox LineStopDetail;
    }
}