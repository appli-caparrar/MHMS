namespace MHMS.Forms
{
    partial class OtherApproval
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ShowEntriesPanel = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.DropdownEntriesValue = new System.Windows.Forms.ComboBox();
            this.RejectButton = new System.Windows.Forms.Button();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.ApprovalCount = new System.Windows.Forms.Label();
            this.ExportButton = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.StatusDropdown = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ApplicationTypeDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TypeText = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.ApprovalDataGrid = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.ShowEntriesPanel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApprovalDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.ShowEntriesPanel);
            this.panel1.Controls.Add(this.RejectButton);
            this.panel1.Controls.Add(this.AcceptButton);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.GenerateButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(1199, 153);
            this.panel1.TabIndex = 0;
            // 
            // ShowEntriesPanel
            // 
            this.ShowEntriesPanel.Controls.Add(this.label14);
            this.ShowEntriesPanel.Controls.Add(this.label15);
            this.ShowEntriesPanel.Controls.Add(this.DropdownEntriesValue);
            this.ShowEntriesPanel.Location = new System.Drawing.Point(12, 118);
            this.ShowEntriesPanel.Name = "ShowEntriesPanel";
            this.ShowEntriesPanel.Size = new System.Drawing.Size(167, 35);
            this.ShowEntriesPanel.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label14.Location = new System.Drawing.Point(3, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 19);
            this.label14.TabIndex = 0;
            this.label14.Text = "Show";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label15.Location = new System.Drawing.Point(113, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 19);
            this.label15.TabIndex = 0;
            this.label15.Text = "entries";
            // 
            // DropdownEntriesValue
            // 
            this.DropdownEntriesValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DropdownEntriesValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DropdownEntriesValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DropdownEntriesValue.ForeColor = System.Drawing.Color.Black;
            this.DropdownEntriesValue.FormattingEnabled = true;
            this.DropdownEntriesValue.Items.AddRange(new object[] {
            "50",
            "100",
            "500",
            "All"});
            this.DropdownEntriesValue.Location = new System.Drawing.Point(48, 4);
            this.DropdownEntriesValue.Name = "DropdownEntriesValue";
            this.DropdownEntriesValue.Size = new System.Drawing.Size(62, 28);
            this.DropdownEntriesValue.TabIndex = 2;
            this.DropdownEntriesValue.Text = "10";
            // 
            // RejectButton
            // 
            this.RejectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(46)))), ((int)(((byte)(74)))));
            this.RejectButton.FlatAppearance.BorderSize = 0;
            this.RejectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RejectButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RejectButton.ForeColor = System.Drawing.Color.White;
            this.RejectButton.Location = new System.Drawing.Point(167, 68);
            this.RejectButton.Name = "RejectButton";
            this.RejectButton.Size = new System.Drawing.Size(144, 35);
            this.RejectButton.TabIndex = 18;
            this.RejectButton.Text = "REJECT";
            this.RejectButton.UseVisualStyleBackColor = false;
            // 
            // AcceptButton
            // 
            this.AcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.AcceptButton.FlatAppearance.BorderSize = 0;
            this.AcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptButton.ForeColor = System.Drawing.Color.White;
            this.AcceptButton.Location = new System.Drawing.Point(12, 68);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(144, 35);
            this.AcceptButton.TabIndex = 17;
            this.AcceptButton.Text = "APPROVE";
            this.AcceptButton.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel11);
            this.panel6.Controls.Add(this.ExportButton);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(854, 10);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(335, 133);
            this.panel6.TabIndex = 16;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.ApprovalCount);
            this.panel11.Location = new System.Drawing.Point(191, 95);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(143, 32);
            this.panel11.TabIndex = 39;
            // 
            // ApprovalCount
            // 
            this.ApprovalCount.AutoSize = true;
            this.ApprovalCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.ApprovalCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApprovalCount.Location = new System.Drawing.Point(49, 0);
            this.ApprovalCount.Name = "ApprovalCount";
            this.ApprovalCount.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.ApprovalCount.Size = new System.Drawing.Size(94, 30);
            this.ApprovalCount.TabIndex = 39;
            this.ApprovalCount.Text = "Approval Count:";
            this.ApprovalCount.Visible = false;
            // 
            // ExportButton
            // 
            this.ExportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.ExportButton.FlatAppearance.BorderSize = 0;
            this.ExportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportButton.ForeColor = System.Drawing.Color.White;
            this.ExportButton.Image = global::MHMS.Properties.Resources.download_2_24__2_;
            this.ExportButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportButton.Location = new System.Drawing.Point(210, 0);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(125, 35);
            this.ExportButton.TabIndex = 16;
            this.ExportButton.Text = "EXPORT";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.SearchBox);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Location = new System.Drawing.Point(3, 57);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(332, 35);
            this.panel7.TabIndex = 5;
            // 
            // SearchBox
            // 
            this.SearchBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBox.Location = new System.Drawing.Point(98, 7);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(229, 18);
            this.SearchBox.TabIndex = 1;
            this.SearchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchBox_KeyPress);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel8.CausesValidation = false;
            this.panel8.Controls.Add(this.SearchButton);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(92, 33);
            this.panel8.TabIndex = 0;
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
            this.SearchButton.Size = new System.Drawing.Size(92, 33);
            this.SearchButton.TabIndex = 39;
            this.SearchButton.Text = "       Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            // 
            // GenerateButton
            // 
            this.GenerateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.GenerateButton.FlatAppearance.BorderSize = 0;
            this.GenerateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateButton.ForeColor = System.Drawing.Color.White;
            this.GenerateButton.Image = global::MHMS.Properties.Resources.available_updates_24;
            this.GenerateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GenerateButton.Location = new System.Drawing.Point(616, 13);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(111, 35);
            this.GenerateButton.TabIndex = 15;
            this.GenerateButton.Text = "      GENERATE";
            this.GenerateButton.UseVisualStyleBackColor = false;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.StatusDropdown);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Location = new System.Drawing.Point(322, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(283, 35);
            this.panel2.TabIndex = 11;
            // 
            // StatusDropdown
            // 
            this.StatusDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.StatusDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StatusDropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusDropdown.FormattingEnabled = true;
            this.StatusDropdown.Items.AddRange(new object[] {
            "For Approval",
            "Approved",
            "Rejected"});
            this.StatusDropdown.Location = new System.Drawing.Point(81, 4);
            this.StatusDropdown.Name = "StatusDropdown";
            this.StatusDropdown.Size = new System.Drawing.Size(197, 25);
            this.StatusDropdown.TabIndex = 2;
            this.StatusDropdown.Text = "For Approval";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel5.CausesValidation = false;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(76, 33);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.ApplicationTypeDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(13, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(298, 35);
            this.panel3.TabIndex = 10;
            // 
            // ApplicationTypeDropdown
            // 
            this.ApplicationTypeDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ApplicationTypeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApplicationTypeDropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplicationTypeDropdown.FormattingEnabled = true;
            this.ApplicationTypeDropdown.Items.AddRange(new object[] {
            "ST Application",
            "WC/CC Application",
            "Open MH System",
            "Manpower  Forecast"});
            this.ApplicationTypeDropdown.Location = new System.Drawing.Point(81, 4);
            this.ApplicationTypeDropdown.Name = "ApplicationTypeDropdown";
            this.ApplicationTypeDropdown.Size = new System.Drawing.Size(212, 25);
            this.ApplicationTypeDropdown.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel4.CausesValidation = false;
            this.panel4.Controls.Add(this.TypeText);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(76, 33);
            this.panel4.TabIndex = 0;
            // 
            // TypeText
            // 
            this.TypeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TypeText.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeText.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TypeText.Location = new System.Drawing.Point(0, 0);
            this.TypeText.Name = "TypeText";
            this.TypeText.Size = new System.Drawing.Size(76, 33);
            this.TypeText.TabIndex = 0;
            this.TypeText.Text = "Type";
            this.TypeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.ApprovalDataGrid);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 153);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.panel9.Size = new System.Drawing.Size(1199, 336);
            this.panel9.TabIndex = 1;
            // 
            // ApprovalDataGrid
            // 
            this.ApprovalDataGrid.AllowUserToAddRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.ApprovalDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.ApprovalDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ApprovalDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ApprovalDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ApprovalDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.ApprovalDataGrid.ColumnHeadersHeight = 40;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.ApprovalDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApprovalDataGrid.Location = new System.Drawing.Point(10, 5);
            this.ApprovalDataGrid.Name = "ApprovalDataGrid";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.ApprovalDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.ApprovalDataGrid.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ApprovalDataGrid.Size = new System.Drawing.Size(1179, 321);
            this.ApprovalDataGrid.TabIndex = 26;
            this.ApprovalDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ApprovalDataGrid_CellFormatting);
            this.ApprovalDataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ApprovalDataGrid_CellMouseClick);
            // 
            // OtherApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1199, 489);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel1);
            this.Name = "OtherApproval";
            this.Text = "OtherApproval";
            this.Load += new System.EventHandler(this.OtherApproval_Load);
            this.panel1.ResumeLayout(false);
            this.ShowEntriesPanel.ResumeLayout(false);
            this.ShowEntriesPanel.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ApprovalDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox StatusDropdown;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox ApplicationTypeDropdown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label TypeText;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label ApprovalCount;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button RejectButton;
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Panel ShowEntriesPanel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox DropdownEntriesValue;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.DataGridView ApprovalDataGrid;
    }
}