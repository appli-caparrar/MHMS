namespace MHMS.Forms
{
    partial class ApproverSettingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApproverSettingForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.TypeDropdown = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.ApproverSettingDataGrid = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Section = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LossFactorButton = new System.Windows.Forms.Button();
            this.DownloadTemplateButton = new System.Windows.Forms.Button();
            this.UploadTemplateButton = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApproverSettingDataGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.ApproverSettingDataGrid);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(961, 460);
            this.panel1.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.TypeDropdown);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Location = new System.Drawing.Point(335, 10);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(189, 35);
            this.panel8.TabIndex = 7;
            // 
            // TypeDropdown
            // 
            this.TypeDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TypeDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.TypeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TypeDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeDropdown.FormattingEnabled = true;
            this.TypeDropdown.Items.AddRange(new object[] {
            "Prod",
            "Non-Prod"});
            this.TypeDropdown.Location = new System.Drawing.Point(63, 5);
            this.TypeDropdown.Name = "TypeDropdown";
            this.TypeDropdown.Size = new System.Drawing.Size(121, 23);
            this.TypeDropdown.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel9.CausesValidation = false;
            this.panel9.Controls.Add(this.label3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(57, 33);
            this.panel9.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(9, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Type";
            // 
            // ApproverSettingDataGrid
            // 
            this.ApproverSettingDataGrid.AllowUserToAddRows = false;
            this.ApproverSettingDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(219)))), ((int)(((byte)(232)))));
            this.ApproverSettingDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ApproverSettingDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ApproverSettingDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ApproverSettingDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApproverSettingDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ApproverSettingDataGrid.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ApproverSettingDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.ApproverSettingDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApproverSettingDataGrid.Location = new System.Drawing.Point(10, 111);
            this.ApproverSettingDataGrid.Name = "ApproverSettingDataGrid";
            this.ApproverSettingDataGrid.ReadOnly = true;
            this.ApproverSettingDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ApproverSettingDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            this.ApproverSettingDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.ApproverSettingDataGrid.RowTemplate.Height = 42;
            this.ApproverSettingDataGrid.Size = new System.Drawing.Size(941, 339);
            this.ApproverSettingDataGrid.TabIndex = 4;
            this.ApproverSettingDataGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ApproverSettingDataGrid_CellContentDoubleClick);
            this.ApproverSettingDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ApproverSettingDataGrid_CellFormatting);
            this.ApproverSettingDataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ApproverSettingDataGrid_CellMouseClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Section);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(319, 35);
            this.panel2.TabIndex = 0;
            // 
            // Section
            // 
            this.Section.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Section.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Section.Location = new System.Drawing.Point(77, 8);
            this.Section.Name = "Section";
            this.Section.Size = new System.Drawing.Size(236, 18);
            this.Section.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel3.CausesValidation = false;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(72, 33);
            this.panel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Section";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.SaveButton);
            this.panel4.Controls.Add(this.LossFactorButton);
            this.panel4.Controls.Add(this.DownloadTemplateButton);
            this.panel4.Controls.Add(this.UploadTemplateButton);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(941, 101);
            this.panel4.TabIndex = 3;
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.SaveButton.FlatAppearance.BorderSize = 0;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SaveButton.Location = new System.Drawing.Point(520, 0);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(92, 35);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "SAVE";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveSectionButton_Click);
            // 
            // LossFactorButton
            // 
            this.LossFactorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.LossFactorButton.FlatAppearance.BorderSize = 0;
            this.LossFactorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LossFactorButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LossFactorButton.ForeColor = System.Drawing.Color.White;
            this.LossFactorButton.Location = new System.Drawing.Point(325, 61);
            this.LossFactorButton.Name = "LossFactorButton";
            this.LossFactorButton.Size = new System.Drawing.Size(140, 35);
            this.LossFactorButton.TabIndex = 6;
            this.LossFactorButton.Text = "Loss Factor";
            this.LossFactorButton.UseVisualStyleBackColor = false;
            this.LossFactorButton.Click += new System.EventHandler(this.LossFactorButton_Click);
            // 
            // DownloadTemplateButton
            // 
            this.DownloadTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.DownloadTemplateButton.FlatAppearance.BorderSize = 0;
            this.DownloadTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadTemplateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadTemplateButton.ForeColor = System.Drawing.Color.White;
            this.DownloadTemplateButton.Image = global::MHMS.Properties.Resources.download_2_24__2_;
            this.DownloadTemplateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DownloadTemplateButton.Location = new System.Drawing.Point(0, 61);
            this.DownloadTemplateButton.Name = "DownloadTemplateButton";
            this.DownloadTemplateButton.Size = new System.Drawing.Size(165, 35);
            this.DownloadTemplateButton.TabIndex = 2;
            this.DownloadTemplateButton.Text = "       Download Template";
            this.DownloadTemplateButton.UseVisualStyleBackColor = false;
            this.DownloadTemplateButton.Click += new System.EventHandler(this.DownloadTemplateButton_Click);
            // 
            // UploadTemplateButton
            // 
            this.UploadTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.UploadTemplateButton.FlatAppearance.BorderSize = 0;
            this.UploadTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UploadTemplateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UploadTemplateButton.ForeColor = System.Drawing.Color.White;
            this.UploadTemplateButton.Image = global::MHMS.Properties.Resources.upload_3_24;
            this.UploadTemplateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UploadTemplateButton.Location = new System.Drawing.Point(171, 61);
            this.UploadTemplateButton.Name = "UploadTemplateButton";
            this.UploadTemplateButton.Size = new System.Drawing.Size(148, 35);
            this.UploadTemplateButton.TabIndex = 3;
            this.UploadTemplateButton.Text = "       Upload Template";
            this.UploadTemplateButton.UseVisualStyleBackColor = false;
            this.UploadTemplateButton.Click += new System.EventHandler(this.UploadTemplateButton_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(618, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(323, 101);
            this.panel7.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.SearchBox);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(4, 61);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(319, 35);
            this.panel5.TabIndex = 4;
            // 
            // SearchBox
            // 
            this.SearchBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBox.Location = new System.Drawing.Point(91, 7);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(222, 18);
            this.SearchBox.TabIndex = 1;
            this.SearchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchBox_KeyPress);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel6.CausesValidation = false;
            this.panel6.Controls.Add(this.SearchButton);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(85, 33);
            this.panel6.TabIndex = 0;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.SearchButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchButton.FlatAppearance.BorderSize = 0;
            this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.ForeColor = System.Drawing.Color.White;
            this.SearchButton.Image = global::MHMS.Properties.Resources.search_2_24;
            this.SearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SearchButton.Location = new System.Drawing.Point(0, 0);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(85, 33);
            this.SearchButton.TabIndex = 40;
            this.SearchButton.Text = "       Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ApproverSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 460);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ApproverSettingForm";
            this.Text = "Approver Setting";
            this.Load += new System.EventHandler(this.ApproverSettingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApproverSettingDataGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button DownloadTemplateButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox Section;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView ApproverSettingDataGrid;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button UploadTemplateButton;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button LossFactorButton;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TypeDropdown;
        private System.Windows.Forms.Button SearchButton;
    }
}