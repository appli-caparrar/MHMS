namespace MHMS
{
    partial class RejectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RejectionForm));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.SectionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SectionDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ReasonTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LineStopDetailTextBox = new System.Windows.Forms.TextBox();
            this.AttachedFileButton = new System.Windows.Forms.Button();
            this.FileName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.TopPanel.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ContinueButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(486, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 48);
            this.panel1.TabIndex = 30;
            // 
            // ContinueButton
            // 
            this.ContinueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(111)))), ((int)(((byte)(238)))));
            this.ContinueButton.FlatAppearance.BorderSize = 0;
            this.ContinueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueButton.ForeColor = System.Drawing.Color.White;
            this.ContinueButton.Location = new System.Drawing.Point(44, 9);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(82, 30);
            this.ContinueButton.TabIndex = 30;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = false;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
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
            this.label1.Size = new System.Drawing.Size(156, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reason of Rejection";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.SectionDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(11, 117);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(337, 35);
            this.panel3.TabIndex = 21;
            // 
            // SectionDropdown
            // 
            this.SectionDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SectionDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SectionDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SectionDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionDropdown.FormattingEnabled = true;
            this.SectionDropdown.Location = new System.Drawing.Point(105, 5);
            this.SectionDropdown.Name = "SectionDropdown";
            this.SectionDropdown.Size = new System.Drawing.Size(227, 23);
            this.SectionDropdown.TabIndex = 2;
            this.SectionDropdown.DropDown += new System.EventHandler(this.SectionDropdown_DropDown);
            this.SectionDropdown.SelectedIndexChanged += new System.EventHandler(this.SectionDropdown_SelectedIndexChanged);
            this.SectionDropdown.DropDownClosed += new System.EventHandler(this.SectionDropdown_DropDownClosed);
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
            this.label2.Location = new System.Drawing.Point(10, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Transfer to:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBox1.Controls.Add(this.ReasonTextBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 153);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reason";
            // 
            // ReasonTextBox
            // 
            this.ReasonTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ReasonTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReasonTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReasonTextBox.Location = new System.Drawing.Point(13, 20);
            this.ReasonTextBox.Multiline = true;
            this.ReasonTextBox.Name = "ReasonTextBox";
            this.ReasonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ReasonTextBox.Size = new System.Drawing.Size(575, 120);
            this.ReasonTextBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LineStopDetailTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 51);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line Stop Detail";
            // 
            // LineStopDetailTextBox
            // 
            this.LineStopDetailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineStopDetailTextBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.LineStopDetailTextBox.Location = new System.Drawing.Point(6, 16);
            this.LineStopDetailTextBox.Multiline = true;
            this.LineStopDetailTextBox.Name = "LineStopDetailTextBox";
            this.LineStopDetailTextBox.Size = new System.Drawing.Size(588, 29);
            this.LineStopDetailTextBox.TabIndex = 0;
            // 
            // AttachedFileButton
            // 
            this.AttachedFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.AttachedFileButton.FlatAppearance.BorderSize = 0;
            this.AttachedFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AttachedFileButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttachedFileButton.ForeColor = System.Drawing.Color.White;
            this.AttachedFileButton.Location = new System.Drawing.Point(11, 330);
            this.AttachedFileButton.Name = "AttachedFileButton";
            this.AttachedFileButton.Size = new System.Drawing.Size(100, 30);
            this.AttachedFileButton.TabIndex = 31;
            this.AttachedFileButton.Text = "Attached File";
            this.AttachedFileButton.UseVisualStyleBackColor = false;
            this.AttachedFileButton.Click += new System.EventHandler(this.AttachedFileButton_Click);
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
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.FileName);
            this.panel2.Location = new System.Drawing.Point(117, 330);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 30);
            this.panel2.TabIndex = 35;
            // 
            // RejectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(624, 373);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.AttachedFileButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RejectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rejection Form";
            this.Load += new System.EventHandler(this.RejectionForm_Load);
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
        private System.Windows.Forms.Label SectionLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox SectionDropdown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ReasonTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox LineStopDetailTextBox;
        private System.Windows.Forms.Button AttachedFileButton;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Panel panel2;
    }
}