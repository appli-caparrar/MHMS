namespace MHMS
{
    partial class UpdateMHLoss2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateMHLoss2));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.DateAndTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.MHLossLastUpdateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.manhourLossData2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mH_Management_SystemDataSet1 = new MHMS.MH_Management_SystemDataSet1();
            this.UpdateDataTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExcelSheetDropdownList = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.mH_Management_SystemDataSet = new MHMS.MH_Management_SystemDataSet();
            this.manhourLossDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.manhourLossDataTableAdapter = new MHMS.MH_Management_SystemDataSetTableAdapters.ManhourLossDataTableAdapter();
            this.manhourLossData2TableAdapter = new MHMS.MH_Management_SystemDataSet1TableAdapters.ManhourLossData2TableAdapter();
            this.mH_Management_SystemDataSet2 = new MHMS.MH_Management_SystemDataSet2();
            this.manhourLossData2BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.manhourLossData2TableAdapter1 = new MHMS.MH_Management_SystemDataSet2TableAdapters.ManhourLossData2TableAdapter();
            this.MHLossUploadDatagrid = new System.Windows.Forms.DataGridView();
            this.manhourLossData2BindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.mH_Management_SystemDataSet3 = new MHMS.MH_Management_SystemDataSet3();
            this.manhourLossData2TableAdapter2 = new MHMS.MH_Management_SystemDataSet3TableAdapters.ManhourLossData2TableAdapter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ActionDropdownList = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.rowCount = new System.Windows.Forms.Label();
            this.LabelTimeElapsed = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.UpdateInfo = new System.Windows.Forms.Panel();
            this.ReadyToUpload = new System.Windows.Forms.PictureBox();
            this.infoText = new System.Windows.Forms.Label();
            this.UploadButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.TopPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manhourLossData2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manhourLossDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manhourLossData2BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MHLossUploadDatagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manhourLossData2BindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet3)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.UpdateInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReadyToUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.DateAndTimeLabel);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(851, 48);
            this.TopPanel.TabIndex = 35;
            // 
            // DateAndTimeLabel
            // 
            this.DateAndTimeLabel.AutoSize = true;
            this.DateAndTimeLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.DateAndTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateAndTimeLabel.ForeColor = System.Drawing.Color.White;
            this.DateAndTimeLabel.Location = new System.Drawing.Point(758, 0);
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
            this.label1.Size = new System.Drawing.Size(185, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "UPDATE MH LOSS DATA";
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.BrowseButton.FlatAppearance.BorderSize = 0;
            this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseButton.ForeColor = System.Drawing.Color.White;
            this.BrowseButton.Location = new System.Drawing.Point(762, 60);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(77, 35);
            this.BrowseButton.TabIndex = 38;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = false;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ChooseFileButton);
            this.panel2.Controls.Add(this.FilePath);
            this.panel2.Location = new System.Drawing.Point(11, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(755, 35);
            this.panel2.TabIndex = 36;
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
            this.ChooseFileButton.Size = new System.Drawing.Size(103, 36);
            this.ChooseFileButton.TabIndex = 2;
            this.ChooseFileButton.Text = "Choose File";
            this.ChooseFileButton.UseVisualStyleBackColor = false;
            // 
            // FilePath
            // 
            this.FilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FilePath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePath.Location = new System.Drawing.Point(107, 7);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(637, 18);
            this.FilePath.TabIndex = 1;
            // 
            // MHLossLastUpdateLabel
            // 
            this.MHLossLastUpdateLabel.AutoSize = true;
            this.MHLossLastUpdateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MHLossLastUpdateLabel.Location = new System.Drawing.Point(8, 521);
            this.MHLossLastUpdateLabel.Name = "MHLossLastUpdateLabel";
            this.MHLossLastUpdateLabel.Size = new System.Drawing.Size(68, 13);
            this.MHLossLastUpdateLabel.TabIndex = 42;
            this.MHLossLastUpdateLabel.Text = " mm/dd/yyyy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.label3.Location = new System.Drawing.Point(8, 500);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 41;
            this.label3.Text = "Last Update";
            // 
            // manhourLossData2BindingSource
            // 
            this.manhourLossData2BindingSource.DataMember = "ManhourLossData2";
            this.manhourLossData2BindingSource.DataSource = this.mH_Management_SystemDataSet1;
            // 
            // mH_Management_SystemDataSet1
            // 
            this.mH_Management_SystemDataSet1.DataSetName = "MH_Management_SystemDataSet1";
            this.mH_Management_SystemDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // UpdateDataTimer
            // 
            this.UpdateDataTimer.Enabled = true;
            this.UpdateDataTimer.Tick += new System.EventHandler(this.UpdateDataTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ExcelSheetDropdownList);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(269, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 35);
            this.panel1.TabIndex = 43;
            // 
            // ExcelSheetDropdownList
            // 
            this.ExcelSheetDropdownList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExcelSheetDropdownList.FormattingEnabled = true;
            this.ExcelSheetDropdownList.Location = new System.Drawing.Point(106, 4);
            this.ExcelSheetDropdownList.Name = "ExcelSheetDropdownList";
            this.ExcelSheetDropdownList.Size = new System.Drawing.Size(312, 24);
            this.ExcelSheetDropdownList.TabIndex = 3;
            this.ExcelSheetDropdownList.SelectedIndexChanged += new System.EventHandler(this.ExcelSheetDropdownList_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(-2, -2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select Sheet";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // mH_Management_SystemDataSet
            // 
            this.mH_Management_SystemDataSet.DataSetName = "MH_Management_SystemDataSet";
            this.mH_Management_SystemDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // manhourLossDataBindingSource
            // 
            this.manhourLossDataBindingSource.DataMember = "ManhourLossData";
            this.manhourLossDataBindingSource.DataSource = this.mH_Management_SystemDataSet;
            // 
            // manhourLossDataTableAdapter
            // 
            this.manhourLossDataTableAdapter.ClearBeforeFill = true;
            // 
            // manhourLossData2TableAdapter
            // 
            this.manhourLossData2TableAdapter.ClearBeforeFill = true;
            // 
            // mH_Management_SystemDataSet2
            // 
            this.mH_Management_SystemDataSet2.DataSetName = "MH_Management_SystemDataSet2";
            this.mH_Management_SystemDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // manhourLossData2BindingSource1
            // 
            this.manhourLossData2BindingSource1.DataMember = "ManhourLossData2";
            this.manhourLossData2BindingSource1.DataSource = this.mH_Management_SystemDataSet2;
            // 
            // manhourLossData2TableAdapter1
            // 
            this.manhourLossData2TableAdapter1.ClearBeforeFill = true;
            // 
            // MHLossUploadDatagrid
            // 
            this.MHLossUploadDatagrid.AllowUserToAddRows = false;
            this.MHLossUploadDatagrid.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MHLossUploadDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.MHLossUploadDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MHLossUploadDatagrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.MHLossUploadDatagrid.Location = new System.Drawing.Point(10, 164);
            this.MHLossUploadDatagrid.Name = "MHLossUploadDatagrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MHLossUploadDatagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.MHLossUploadDatagrid.Size = new System.Drawing.Size(829, 330);
            this.MHLossUploadDatagrid.TabIndex = 44;
            this.MHLossUploadDatagrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.MHLossUploadDatagrid_DataBindingComplete);
            // 
            // manhourLossData2BindingSource2
            // 
            this.manhourLossData2BindingSource2.DataMember = "ManhourLossData2";
            this.manhourLossData2BindingSource2.DataSource = this.mH_Management_SystemDataSet3;
            // 
            // mH_Management_SystemDataSet3
            // 
            this.mH_Management_SystemDataSet3.DataSetName = "MH_Management_SystemDataSet3";
            this.mH_Management_SystemDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // manhourLossData2TableAdapter2
            // 
            this.manhourLossData2TableAdapter2.ClearBeforeFill = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.ActionDropdownList);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Location = new System.Drawing.Point(10, 102);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(253, 35);
            this.panel3.TabIndex = 45;
            // 
            // ActionDropdownList
            // 
            this.ActionDropdownList.Enabled = false;
            this.ActionDropdownList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActionDropdownList.FormattingEnabled = true;
            this.ActionDropdownList.Items.AddRange(new object[] {
            "Additional",
            "Delete"});
            this.ActionDropdownList.Location = new System.Drawing.Point(84, 4);
            this.ActionDropdownList.Name = "ActionDropdownList";
            this.ActionDropdownList.Size = new System.Drawing.Size(161, 24);
            this.ActionDropdownList.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(0, -2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 36);
            this.button2.TabIndex = 2;
            this.button2.Text = "Action";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // rowCount
            // 
            this.rowCount.AutoSize = true;
            this.rowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rowCount.Location = new System.Drawing.Point(8, 148);
            this.rowCount.Name = "rowCount";
            this.rowCount.Size = new System.Drawing.Size(61, 13);
            this.rowCount.TabIndex = 46;
            this.rowCount.Text = "No. of rows";
            // 
            // LabelTimeElapsed
            // 
            this.LabelTimeElapsed.AutoSize = true;
            this.LabelTimeElapsed.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelTimeElapsed.Location = new System.Drawing.Point(136, 0);
            this.LabelTimeElapsed.Name = "LabelTimeElapsed";
            this.LabelTimeElapsed.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.LabelTimeElapsed.Size = new System.Drawing.Size(64, 22);
            this.LabelTimeElapsed.TabIndex = 48;
            this.LabelTimeElapsed.Text = "Time elapse";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.LabelTimeElapsed);
            this.panel4.Location = new System.Drawing.Point(639, 139);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 23);
            this.panel4.TabIndex = 49;
            // 
            // UpdateInfo
            // 
            this.UpdateInfo.Controls.Add(this.ReadyToUpload);
            this.UpdateInfo.Controls.Add(this.infoText);
            this.UpdateInfo.Location = new System.Drawing.Point(320, 500);
            this.UpdateInfo.Name = "UpdateInfo";
            this.UpdateInfo.Size = new System.Drawing.Size(198, 37);
            this.UpdateInfo.TabIndex = 51;
            // 
            // ReadyToUpload
            // 
            this.ReadyToUpload.Image = global::MHMS.Properties.Resources.check_mark_verified;
            this.ReadyToUpload.Location = new System.Drawing.Point(0, 0);
            this.ReadyToUpload.Name = "ReadyToUpload";
            this.ReadyToUpload.Size = new System.Drawing.Size(41, 37);
            this.ReadyToUpload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ReadyToUpload.TabIndex = 50;
            this.ReadyToUpload.TabStop = false;
            // 
            // infoText
            // 
            this.infoText.AutoSize = true;
            this.infoText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoText.Location = new System.Drawing.Point(41, 6);
            this.infoText.Name = "infoText";
            this.infoText.Size = new System.Drawing.Size(137, 21);
            this.infoText.TabIndex = 51;
            this.infoText.Text = "Ready to Upload!";
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
            this.UploadButton.Location = new System.Drawing.Point(699, 102);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(140, 35);
            this.UploadButton.TabIndex = 37;
            this.UploadButton.Text = "UPLOAD";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
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
            this.ExportButton.Location = new System.Drawing.Point(699, 500);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(140, 35);
            this.ExportButton.TabIndex = 40;
            this.ExportButton.Text = "EXPORT";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.Visible = false;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // UpdateMHLoss2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(851, 542);
            this.Controls.Add(this.UpdateInfo);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.rowCount);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.MHLossUploadDatagrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.MHLossLastUpdateLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExportButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateMHLoss2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UpdateMHLoss2_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manhourLossData2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manhourLossDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manhourLossData2BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MHLossUploadDatagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manhourLossData2BindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet3)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.UpdateInfo.ResumeLayout(false);
            this.UpdateInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReadyToUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label DateAndTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Label MHLossLastUpdateLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Timer UpdateDataTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ExcelSheetDropdownList;
        private System.Windows.Forms.Button button1;
        private MH_Management_SystemDataSet mH_Management_SystemDataSet;
        private System.Windows.Forms.BindingSource manhourLossDataBindingSource;
        private MH_Management_SystemDataSetTableAdapters.ManhourLossDataTableAdapter manhourLossDataTableAdapter;
        private MH_Management_SystemDataSet1 mH_Management_SystemDataSet1;
        private System.Windows.Forms.BindingSource manhourLossData2BindingSource;
        private MH_Management_SystemDataSet1TableAdapters.ManhourLossData2TableAdapter manhourLossData2TableAdapter;
        private MH_Management_SystemDataSet2 mH_Management_SystemDataSet2;
        private System.Windows.Forms.BindingSource manhourLossData2BindingSource1;
        private MH_Management_SystemDataSet2TableAdapters.ManhourLossData2TableAdapter manhourLossData2TableAdapter1;
        private System.Windows.Forms.DataGridView MHLossUploadDatagrid;
        private MH_Management_SystemDataSet3 mH_Management_SystemDataSet3;
        private System.Windows.Forms.BindingSource manhourLossData2BindingSource2;
        private MH_Management_SystemDataSet3TableAdapters.ManhourLossData2TableAdapter manhourLossData2TableAdapter2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox ActionDropdownList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label rowCount;
        private System.Windows.Forms.Label LabelTimeElapsed;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox ReadyToUpload;
        private System.Windows.Forms.Panel UpdateInfo;
        private System.Windows.Forms.Label infoText;
    }
}