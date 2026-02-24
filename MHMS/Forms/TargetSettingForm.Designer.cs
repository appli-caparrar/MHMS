namespace MHMS.Forms
{
    partial class TargetSettingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TargetSettingForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SectionDropdownPanel = new System.Windows.Forms.Panel();
            this.SectionDropdownList = new System.Windows.Forms.ComboBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FiscalYearDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.DownloadTemplateBtn = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.UploadButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CategoryDropdown = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TargetSettingDataGrid = new System.Windows.Forms.DataGridView();
            this.LabelHeader = new System.Windows.Forms.Panel();
            this.TitleCategory = new System.Windows.Forms.Label();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.DropdownValue = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.HistoryDataGrid = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SectionDropdownPanel.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetSettingDataGrid)).BeginInit();
            this.panel19.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SectionDropdownPanel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1314, 96);
            this.panel1.TabIndex = 0;
            // 
            // SectionDropdownPanel
            // 
            this.SectionDropdownPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SectionDropdownPanel.Controls.Add(this.SectionDropdownList);
            this.SectionDropdownPanel.Controls.Add(this.panel12);
            this.SectionDropdownPanel.Location = new System.Drawing.Point(333, 57);
            this.SectionDropdownPanel.Name = "SectionDropdownPanel";
            this.SectionDropdownPanel.Size = new System.Drawing.Size(328, 35);
            this.SectionDropdownPanel.TabIndex = 9;
            this.SectionDropdownPanel.Visible = false;
            // 
            // SectionDropdownList
            // 
            this.SectionDropdownList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SectionDropdownList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SectionDropdownList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SectionDropdownList.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionDropdownList.FormattingEnabled = true;
            this.SectionDropdownList.Items.AddRange(new object[] {
            "Development Engineering",
            "Equipment Engineering",
            "General Affairs",
            "Incoming Quality Control",
            "Information Technology",
            "Ink Cartridge",
            "Ink Head",
            "Logistics Control",
            "Material Purchasing 2",
            "Molding Production",
            "PCB Assembly",
            "Printer",
            "Production Control",
            "Production Engineering",
            "P-Touch",
            "Quality Assurance",
            "Quality Assurance",
            "Supplier Quality Control",
            "Tape Cassette"});
            this.SectionDropdownList.Location = new System.Drawing.Point(96, 2);
            this.SectionDropdownList.Name = "SectionDropdownList";
            this.SectionDropdownList.Size = new System.Drawing.Size(227, 28);
            this.SectionDropdownList.TabIndex = 2;
            this.SectionDropdownList.DropDown += new System.EventHandler(this.SectionDropdownList_DropDown);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel12.CausesValidation = false;
            this.panel12.Controls.Add(this.label4);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(90, 33);
            this.panel12.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 33);
            this.label4.TabIndex = 0;
            this.label4.Text = "Section";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.FiscalYearDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(1, 57);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(319, 35);
            this.panel3.TabIndex = 8;
            // 
            // FiscalYearDropdown
            // 
            this.FiscalYearDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.FiscalYearDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.FiscalYearDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FiscalYearDropdown.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiscalYearDropdown.FormattingEnabled = true;
            this.FiscalYearDropdown.Location = new System.Drawing.Point(82, 2);
            this.FiscalYearDropdown.Name = "FiscalYearDropdown";
            this.FiscalYearDropdown.Size = new System.Drawing.Size(231, 28);
            this.FiscalYearDropdown.TabIndex = 2;
            this.FiscalYearDropdown.SelectedIndexChanged += new System.EventHandler(this.FiscalYearDropdown_SelectedIndexChanged);
            this.FiscalYearDropdown.TextChanged += new System.EventHandler(this.FiscalYearDropdown_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel4.CausesValidation = false;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(76, 33);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fiscal Year";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.DownloadTemplateBtn);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel11.Location = new System.Drawing.Point(914, 56);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(400, 40);
            this.panel11.TabIndex = 1;
            // 
            // DownloadTemplateBtn
            // 
            this.DownloadTemplateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.DownloadTemplateBtn.FlatAppearance.BorderSize = 0;
            this.DownloadTemplateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadTemplateBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadTemplateBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.DownloadTemplateBtn.Location = new System.Drawing.Point(247, 2);
            this.DownloadTemplateBtn.Name = "DownloadTemplateBtn";
            this.DownloadTemplateBtn.Size = new System.Drawing.Size(150, 35);
            this.DownloadTemplateBtn.TabIndex = 30;
            this.DownloadTemplateBtn.Text = "Download Template";
            this.DownloadTemplateBtn.UseVisualStyleBackColor = false;
            this.DownloadTemplateBtn.Click += new System.EventHandler(this.DownloadTemplateBtn_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.BrowseButton);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Controls.Add(this.UploadButton);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1314, 56);
            this.panel9.TabIndex = 0;
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.BrowseButton.FlatAppearance.BorderSize = 0;
            this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseButton.ForeColor = System.Drawing.Color.White;
            this.BrowseButton.Location = new System.Drawing.Point(829, 0);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(62, 35);
            this.BrowseButton.TabIndex = 28;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = false;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(105)))), ((int)(((byte)(22)))));
            this.label3.Location = new System.Drawing.Point(330, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(298, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "NOTE: The sheet name should be same as selected category";
            // 
            // UploadButton
            // 
            this.UploadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.UploadButton.FlatAppearance.BorderSize = 0;
            this.UploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UploadButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UploadButton.ForeColor = System.Drawing.Color.White;
            this.UploadButton.Image = global::MHMS.Properties.Resources.upload_3_24;
            this.UploadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UploadButton.Location = new System.Drawing.Point(905, 0);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(144, 35);
            this.UploadButton.TabIndex = 7;
            this.UploadButton.Text = " UPLOAD";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ChooseFileButton);
            this.panel2.Controls.Add(this.FilePath);
            this.panel2.Location = new System.Drawing.Point(343, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 35);
            this.panel2.TabIndex = 6;
            // 
            // ChooseFileButton
            // 
            this.ChooseFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.ChooseFileButton.FlatAppearance.BorderSize = 0;
            this.ChooseFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChooseFileButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseFileButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ChooseFileButton.Location = new System.Drawing.Point(-1, -2);
            this.ChooseFileButton.Name = "ChooseFileButton";
            this.ChooseFileButton.Size = new System.Drawing.Size(91, 35);
            this.ChooseFileButton.TabIndex = 2;
            this.ChooseFileButton.Text = "Choose File";
            this.ChooseFileButton.UseVisualStyleBackColor = false;
            this.ChooseFileButton.Click += new System.EventHandler(this.ChooseFileButton_Click);
            // 
            // FilePath
            // 
            this.FilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FilePath.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePath.Location = new System.Drawing.Point(96, 7);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(394, 18);
            this.FilePath.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.CategoryDropdown);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(10, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(319, 35);
            this.panel5.TabIndex = 5;
            // 
            // CategoryDropdown
            // 
            this.CategoryDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CategoryDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CategoryDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryDropdown.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryDropdown.FormattingEnabled = true;
            this.CategoryDropdown.Items.AddRange(new object[] {
            "COPQ Manpower Rate",
            "Disposal Budget",
            "Factory Efficiency",
            "MH Loss Rate",
            "Parts Loss Rate",
            "Production Efficiency",
            "Standard Man-Hour"});
            this.CategoryDropdown.Location = new System.Drawing.Point(83, 2);
            this.CategoryDropdown.Name = "CategoryDropdown";
            this.CategoryDropdown.Size = new System.Drawing.Size(231, 29);
            this.CategoryDropdown.TabIndex = 1;
            this.CategoryDropdown.SelectedIndexChanged += new System.EventHandler(this.CategoryDropdown_SelectedIndexChanged);
            this.CategoryDropdown.TextChanged += new System.EventHandler(this.CategoryDropdown_TextChanged);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel6.CausesValidation = false;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(77, 33);
            this.panel6.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Category";
            // 
            // TargetSettingDataGrid
            // 
            this.TargetSettingDataGrid.AllowUserToAddRows = false;
            this.TargetSettingDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(219)))), ((int)(((byte)(232)))));
            this.TargetSettingDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.TargetSettingDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TargetSettingDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TargetSettingDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetSettingDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.TargetSettingDataGrid.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TargetSettingDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.TargetSettingDataGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.TargetSettingDataGrid.Location = new System.Drawing.Point(10, 106);
            this.TargetSettingDataGrid.Name = "TargetSettingDataGrid";
            this.TargetSettingDataGrid.ReadOnly = true;
            this.TargetSettingDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetSettingDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.TargetSettingDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            this.TargetSettingDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.TargetSettingDataGrid.RowTemplate.Height = 42;
            this.TargetSettingDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TargetSettingDataGrid.Size = new System.Drawing.Size(1314, 271);
            this.TargetSettingDataGrid.TabIndex = 6;
            this.TargetSettingDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TargetSettingDataGrid_CellFormatting);
            this.TargetSettingDataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TargetSettingDataGrid_CellMouseClick);
            this.TargetSettingDataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TargetSettingDataGrid_ColumnHeaderMouseClick);
            // 
            // LabelHeader
            // 
            this.LabelHeader.BackColor = System.Drawing.SystemColors.Window;
            this.LabelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelHeader.Location = new System.Drawing.Point(10, 377);
            this.LabelHeader.Name = "LabelHeader";
            this.LabelHeader.Size = new System.Drawing.Size(1314, 42);
            this.LabelHeader.TabIndex = 7;
            // 
            // TitleCategory
            // 
            this.TitleCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TitleCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleCategory.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TitleCategory.Location = new System.Drawing.Point(0, 0);
            this.TitleCategory.Name = "TitleCategory";
            this.TitleCategory.Size = new System.Drawing.Size(1314, 42);
            this.TitleCategory.TabIndex = 0;
            this.TitleCategory.Text = "HISTORY TABLE";
            this.TitleCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel19
            // 
            this.panel19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel19.Controls.Add(this.panel8);
            this.panel19.Controls.Add(this.panel20);
            this.panel19.Controls.Add(this.DropdownValue);
            this.panel19.Location = new System.Drawing.Point(10, 379);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(195, 35);
            this.panel19.TabIndex = 23;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel8.CausesValidation = false;
            this.panel8.Controls.Add(this.label5);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(136, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(57, 33);
            this.panel8.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(4, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "entries";
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel20.CausesValidation = false;
            this.panel20.Controls.Add(this.label6);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel20.Location = new System.Drawing.Point(0, 0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(57, 33);
            this.panel20.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(4, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Show";
            // 
            // DropdownValue
            // 
            this.DropdownValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DropdownValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DropdownValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DropdownValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DropdownValue.ForeColor = System.Drawing.Color.Gray;
            this.DropdownValue.FormattingEnabled = true;
            this.DropdownValue.Items.AddRange(new object[] {
            "10",
            "50",
            "100",
            "All"});
            this.DropdownValue.Location = new System.Drawing.Point(63, 3);
            this.DropdownValue.Name = "DropdownValue";
            this.DropdownValue.Size = new System.Drawing.Size(67, 28);
            this.DropdownValue.TabIndex = 2;
            this.DropdownValue.Text = "10";
            this.DropdownValue.TextChanged += new System.EventHandler(this.DropdownValue_TextChanged);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.TitleCategory);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(10, 419);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1314, 42);
            this.panel7.TabIndex = 24;
            // 
            // HistoryDataGrid
            // 
            this.HistoryDataGrid.AllowUserToAddRows = false;
            this.HistoryDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(219)))), ((int)(((byte)(232)))));
            this.HistoryDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.HistoryDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HistoryDataGrid.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.HistoryDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HistoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.HistoryDataGrid.ColumnHeadersHeight = 40;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HistoryDataGrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.HistoryDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryDataGrid.Location = new System.Drawing.Point(10, 461);
            this.HistoryDataGrid.Name = "HistoryDataGrid";
            this.HistoryDataGrid.ReadOnly = true;
            this.HistoryDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HistoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.HistoryDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            this.HistoryDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.HistoryDataGrid.RowTemplate.Height = 42;
            this.HistoryDataGrid.Size = new System.Drawing.Size(1314, 83);
            this.HistoryDataGrid.TabIndex = 25;
            this.HistoryDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.HistoryDataGrid_CellFormatting);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TargetSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1334, 554);
            this.Controls.Add(this.HistoryDataGrid);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel19);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.LabelHeader);
            this.Controls.Add(this.TargetSettingDataGrid);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TargetSettingForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Target Setting";
            this.Load += new System.EventHandler(this.TargetSettingForm_Load);
            this.panel1.ResumeLayout(false);
            this.SectionDropdownPanel.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetSettingDataGrid)).EndInit();
            this.panel19.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HistoryDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox FiscalYearDropdown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.ComboBox CategoryDropdown;
        private System.Windows.Forms.DataGridView TargetSettingDataGrid;
        private System.Windows.Forms.Panel LabelHeader;
        private System.Windows.Forms.Label TitleCategory;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox DropdownValue;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataGridView HistoryDataGrid;
        private System.Windows.Forms.Panel SectionDropdownPanel;
        private System.Windows.Forms.ComboBox SectionDropdownList;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button DownloadTemplateBtn;
        private System.Windows.Forms.Timer timer1;
        //private MH_Management_SystemDataSetTableAdapters.ApproverSettingTableAdapter approverSettingTableAdapter;
    }
}