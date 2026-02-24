namespace MHMS
{
    partial class UpdatePartsLossData2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatePartsLossData2));
            this.SheetDropdownList = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.DownloadTemplateButton = new System.Windows.Forms.Button();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.UploadPartsLossDatagrid = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.DataTypeDropdown = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DefectLastUpdateDateLabel = new System.Windows.Forms.Label();
            this.DataUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.GMMSAndSAPLastUpdateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.SectionDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SectionLabel = new System.Windows.Forms.Label();
            this.DateAndTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.IDFTemplateButton = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.MonthDropdwn = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.FiscalYearDropdown = new System.Windows.Forms.ComboBox();
            this.UploadButton = new System.Windows.Forms.Button();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UploadPartsLossDatagrid)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // SheetDropdownList
            // 
            this.SheetDropdownList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SheetDropdownList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SheetDropdownList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SheetDropdownList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SheetDropdownList.ForeColor = System.Drawing.Color.DimGray;
            this.SheetDropdownList.FormattingEnabled = true;
            this.SheetDropdownList.Location = new System.Drawing.Point(3, 5);
            this.SheetDropdownList.Name = "SheetDropdownList";
            this.SheetDropdownList.Size = new System.Drawing.Size(200, 24);
            this.SheetDropdownList.TabIndex = 2;
            this.SheetDropdownList.Text = "Select sheet";
            this.SheetDropdownList.SelectedIndexChanged += new System.EventHandler(this.SheetDropdownList_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.SheetDropdownList);
            this.panel6.Location = new System.Drawing.Point(699, 109);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(208, 35);
            this.panel6.TabIndex = 42;
            // 
            // DownloadTemplateButton
            // 
            this.DownloadTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.DownloadTemplateButton.FlatAppearance.BorderSize = 0;
            this.DownloadTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadTemplateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadTemplateButton.ForeColor = System.Drawing.Color.White;
            this.DownloadTemplateButton.Location = new System.Drawing.Point(610, 495);
            this.DownloadTemplateButton.Name = "DownloadTemplateButton";
            this.DownloadTemplateButton.Size = new System.Drawing.Size(163, 35);
            this.DownloadTemplateButton.TabIndex = 41;
            this.DownloadTemplateButton.Text = "OPEN TEMPLATE";
            this.DownloadTemplateButton.UseVisualStyleBackColor = false;
            this.DownloadTemplateButton.Click += new System.EventHandler(this.DownloadTemplateButton_Click);
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.BrowseButton.FlatAppearance.BorderSize = 0;
            this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseButton.ForeColor = System.Drawing.Color.White;
            this.BrowseButton.Location = new System.Drawing.Point(387, -2);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(62, 35);
            this.BrowseButton.TabIndex = 40;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = false;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // UploadPartsLossDatagrid
            // 
            this.UploadPartsLossDatagrid.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UploadPartsLossDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.UploadPartsLossDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UploadPartsLossDatagrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.UploadPartsLossDatagrid.Location = new System.Drawing.Point(12, 152);
            this.UploadPartsLossDatagrid.Name = "UploadPartsLossDatagrid";
            this.UploadPartsLossDatagrid.Size = new System.Drawing.Size(895, 327);
            this.UploadPartsLossDatagrid.TabIndex = 39;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(9, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 19);
            this.label10.TabIndex = 0;
            this.label10.Text = "Data Type";
            // 
            // DataTypeDropdown
            // 
            this.DataTypeDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DataTypeDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DataTypeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DataTypeDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataTypeDropdown.FormattingEnabled = true;
            this.DataTypeDropdown.Items.AddRange(new object[] {
            "GMMS & SAP",
            "Defect"});
            this.DataTypeDropdown.Location = new System.Drawing.Point(96, 5);
            this.DataTypeDropdown.Name = "DataTypeDropdown";
            this.DataTypeDropdown.Size = new System.Drawing.Size(293, 24);
            this.DataTypeDropdown.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel5.CausesValidation = false;
            this.panel5.Controls.Add(this.label10);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(90, 33);
            this.panel5.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DataTypeDropdown);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Location = new System.Drawing.Point(513, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 35);
            this.panel1.TabIndex = 38;
            // 
            // DefectLastUpdateDateLabel
            // 
            this.DefectLastUpdateDateLabel.AutoSize = true;
            this.DefectLastUpdateDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DefectLastUpdateDateLabel.Location = new System.Drawing.Point(9, 522);
            this.DefectLastUpdateDateLabel.Name = "DefectLastUpdateDateLabel";
            this.DefectLastUpdateDateLabel.Size = new System.Drawing.Size(106, 13);
            this.DefectLastUpdateDateLabel.TabIndex = 37;
            this.DefectLastUpdateDateLabel.Text = "Defect : mm/dd/yyyy";
            // 
            // DataUpdateTimer
            // 
            this.DataUpdateTimer.Enabled = true;
            this.DataUpdateTimer.Tick += new System.EventHandler(this.DataUpdateTimer_Tick);
            // 
            // GMMSAndSAPLastUpdateLabel
            // 
            this.GMMSAndSAPLastUpdateLabel.AutoSize = true;
            this.GMMSAndSAPLastUpdateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GMMSAndSAPLastUpdateLabel.Location = new System.Drawing.Point(9, 505);
            this.GMMSAndSAPLastUpdateLabel.Name = "GMMSAndSAPLastUpdateLabel";
            this.GMMSAndSAPLastUpdateLabel.Size = new System.Drawing.Size(139, 13);
            this.GMMSAndSAPLastUpdateLabel.TabIndex = 36;
            this.GMMSAndSAPLastUpdateLabel.Text = "GMMS / SAP : mm/dd/yyyy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.label3.Location = new System.Drawing.Point(8, 482);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 35;
            this.label3.Text = "Last Update";
            // 
            // ChooseFileButton
            // 
            this.ChooseFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.ChooseFileButton.FlatAppearance.BorderSize = 0;
            this.ChooseFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChooseFileButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseFileButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ChooseFileButton.Location = new System.Drawing.Point(-2, -2);
            this.ChooseFileButton.Name = "ChooseFileButton";
            this.ChooseFileButton.Size = new System.Drawing.Size(92, 36);
            this.ChooseFileButton.TabIndex = 2;
            this.ChooseFileButton.Text = "Choose File";
            this.ChooseFileButton.UseVisualStyleBackColor = false;
            // 
            // FilePath
            // 
            this.FilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FilePath.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePath.Location = new System.Drawing.Point(96, 7);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(285, 18);
            this.FilePath.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ChooseFileButton);
            this.panel2.Controls.Add(this.FilePath);
            this.panel2.Controls.Add(this.BrowseButton);
            this.panel2.Location = new System.Drawing.Point(243, 109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(450, 35);
            this.panel2.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(16, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Section";
            // 
            // SectionDropdown
            // 
            this.SectionDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SectionDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SectionDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SectionDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionDropdown.FormattingEnabled = true;
            this.SectionDropdown.Location = new System.Drawing.Point(96, 5);
            this.SectionDropdown.Name = "SectionDropdown";
            this.SectionDropdown.Size = new System.Drawing.Size(395, 24);
            this.SectionDropdown.TabIndex = 2;
            this.SectionDropdown.SelectedIndexChanged += new System.EventHandler(this.SectionDropdown_SelectedIndexChanged);
            this.SectionDropdown.TextChanged += new System.EventHandler(this.SectionDropdown_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel4.CausesValidation = false;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(90, 33);
            this.panel4.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.SectionDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(11, 61);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(496, 35);
            this.panel3.TabIndex = 31;
            // 
            // SectionLabel
            // 
            this.SectionLabel.AutoSize = true;
            this.SectionLabel.ForeColor = System.Drawing.Color.White;
            this.SectionLabel.Location = new System.Drawing.Point(235, 19);
            this.SectionLabel.Name = "SectionLabel";
            this.SectionLabel.Size = new System.Drawing.Size(43, 13);
            this.SectionLabel.TabIndex = 28;
            this.SectionLabel.Text = "Section";
            this.SectionLabel.Visible = false;
            // 
            // DateAndTimeLabel
            // 
            this.DateAndTimeLabel.AutoSize = true;
            this.DateAndTimeLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.DateAndTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateAndTimeLabel.ForeColor = System.Drawing.Color.White;
            this.DateAndTimeLabel.Location = new System.Drawing.Point(826, 0);
            this.DateAndTimeLabel.Name = "DateAndTimeLabel";
            this.DateAndTimeLabel.Padding = new System.Windows.Forms.Padding(0, 20, 9, 0);
            this.DateAndTimeLabel.Size = new System.Drawing.Size(93, 35);
            this.DateAndTimeLabel.TabIndex = 18;
            this.DateAndTimeLabel.Text = "Date and TIme";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "UPDATE PARTS LOSS DATA";
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.SectionLabel);
            this.TopPanel.Controls.Add(this.DateAndTimeLabel);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(919, 48);
            this.TopPanel.TabIndex = 30;
            // 
            // IDFTemplateButton
            // 
            this.IDFTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.IDFTemplateButton.FlatAppearance.BorderSize = 0;
            this.IDFTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IDFTemplateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDFTemplateButton.ForeColor = System.Drawing.Color.White;
            this.IDFTemplateButton.Location = new System.Drawing.Point(441, 495);
            this.IDFTemplateButton.Name = "IDFTemplateButton";
            this.IDFTemplateButton.Size = new System.Drawing.Size(163, 35);
            this.IDFTemplateButton.TabIndex = 43;
            this.IDFTemplateButton.Text = "OPEN IDF TEMPLATE";
            this.IDFTemplateButton.UseVisualStyleBackColor = false;
            this.IDFTemplateButton.Click += new System.EventHandler(this.IDFTemplateButton_Click);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.MonthDropdwn);
            this.panel7.Location = new System.Drawing.Point(12, 109);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(117, 35);
            this.panel7.TabIndex = 44;
            // 
            // MonthDropdwn
            // 
            this.MonthDropdwn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.MonthDropdwn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.MonthDropdwn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MonthDropdwn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonthDropdwn.FormattingEnabled = true;
            this.MonthDropdwn.Items.AddRange(new object[] {
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
            "January",
            "February",
            "March"});
            this.MonthDropdwn.Location = new System.Drawing.Point(3, 5);
            this.MonthDropdwn.Name = "MonthDropdwn";
            this.MonthDropdwn.Size = new System.Drawing.Size(109, 24);
            this.MonthDropdwn.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.FiscalYearDropdown);
            this.panel8.Location = new System.Drawing.Point(135, 109);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(102, 35);
            this.panel8.TabIndex = 45;
            // 
            // FiscalYearDropdown
            // 
            this.FiscalYearDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.FiscalYearDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.FiscalYearDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FiscalYearDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiscalYearDropdown.FormattingEnabled = true;
            this.FiscalYearDropdown.Location = new System.Drawing.Point(3, 5);
            this.FiscalYearDropdown.Name = "FiscalYearDropdown";
            this.FiscalYearDropdown.Size = new System.Drawing.Size(94, 24);
            this.FiscalYearDropdown.TabIndex = 2;
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
            this.UploadButton.Location = new System.Drawing.Point(779, 495);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(128, 35);
            this.UploadButton.TabIndex = 33;
            this.UploadButton.Text = "  UPLOAD";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // UpdatePartsLossData2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(919, 542);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.IDFTemplateButton);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.DownloadTemplateButton);
            this.Controls.Add(this.UploadPartsLossDatagrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DefectLastUpdateDateLabel);
            this.Controls.Add(this.GMMSAndSAPLastUpdateLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.TopPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdatePartsLossData2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Parts Loss Data";
            this.Load += new System.EventHandler(this.UpdatePartsLossData2_Load);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UploadPartsLossDatagrid)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox SheetDropdownList;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button DownloadTemplateButton;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.DataGridView UploadPartsLossDatagrid;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox DataTypeDropdown;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label DefectLastUpdateDateLabel;
        private System.Windows.Forms.Timer DataUpdateTimer;
        private System.Windows.Forms.Label GMMSAndSAPLastUpdateLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SectionDropdown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label SectionLabel;
        private System.Windows.Forms.Label DateAndTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button IDFTemplateButton;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox MonthDropdwn;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox FiscalYearDropdown;
    }
}