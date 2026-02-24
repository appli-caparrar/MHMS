namespace MHMS
{
    partial class ViewApplicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewApplicationForm));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.DateAndTimeLabel = new System.Windows.Forms.Label();
            this.ReferenceNoLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel9 = new System.Windows.Forms.Panel();
            this.ExportButton = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ReasonOfApplicationPanel = new System.Windows.Forms.Panel();
            this.SectionTxtBox = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.DownloadUploadPanel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CategoryTxtBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.DateTimeAppliedTxtBox = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.AppliedByTxtBox = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RejectButton = new System.Windows.Forms.Button();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.ViewApplicationDataGrid = new System.Windows.Forms.DataGridView();
            this.TopPanel.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.ReasonOfApplicationPanel.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewApplicationDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.DateAndTimeLabel);
            this.TopPanel.Controls.Add(this.ReferenceNoLabel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1034, 42);
            this.TopPanel.TabIndex = 33;
            this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPanel_Paint);
            // 
            // DateAndTimeLabel
            // 
            this.DateAndTimeLabel.AutoSize = true;
            this.DateAndTimeLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.DateAndTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateAndTimeLabel.ForeColor = System.Drawing.Color.White;
            this.DateAndTimeLabel.Location = new System.Drawing.Point(941, 0);
            this.DateAndTimeLabel.Name = "DateAndTimeLabel";
            this.DateAndTimeLabel.Padding = new System.Windows.Forms.Padding(0, 20, 9, 0);
            this.DateAndTimeLabel.Size = new System.Drawing.Size(93, 35);
            this.DateAndTimeLabel.TabIndex = 18;
            this.DateAndTimeLabel.Text = "Date and TIme";
            // 
            // ReferenceNoLabel
            // 
            this.ReferenceNoLabel.AutoSize = true;
            this.ReferenceNoLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReferenceNoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(176)))), ((int)(((byte)(242)))));
            this.ReferenceNoLabel.Location = new System.Drawing.Point(12, 13);
            this.ReferenceNoLabel.Name = "ReferenceNoLabel";
            this.ReferenceNoLabel.Size = new System.Drawing.Size(153, 17);
            this.ReferenceNoLabel.TabIndex = 0;
            this.ReferenceNoLabel.Text = "<REFERENCE NUMBER>";
            this.ReferenceNoLabel.Click += new System.EventHandler(this.ReferenceNoLabel_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.ExportButton);
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(689, 10);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(335, 147);
            this.panel9.TabIndex = 42;
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
            this.ExportButton.Location = new System.Drawing.Point(210, 2);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(125, 35);
            this.ExportButton.TabIndex = 16;
            this.ExportButton.Text = "EXPORT";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.SearchBox);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Location = new System.Drawing.Point(3, 101);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(332, 35);
            this.panel10.TabIndex = 5;
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
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel11.CausesValidation = false;
            this.panel11.Controls.Add(this.SearchButton);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(92, 33);
            this.panel11.TabIndex = 0;
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
            // ReasonOfApplicationPanel
            // 
            this.ReasonOfApplicationPanel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ReasonOfApplicationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReasonOfApplicationPanel.Controls.Add(this.SectionTxtBox);
            this.ReasonOfApplicationPanel.Controls.Add(this.panel7);
            this.ReasonOfApplicationPanel.Location = new System.Drawing.Point(10, 13);
            this.ReasonOfApplicationPanel.Name = "ReasonOfApplicationPanel";
            this.ReasonOfApplicationPanel.Size = new System.Drawing.Size(427, 35);
            this.ReasonOfApplicationPanel.TabIndex = 12;
            // 
            // SectionTxtBox
            // 
            this.SectionTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SectionTxtBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionTxtBox.Location = new System.Drawing.Point(162, 7);
            this.SectionTxtBox.Name = "SectionTxtBox";
            this.SectionTxtBox.ReadOnly = true;
            this.SectionTxtBox.Size = new System.Drawing.Size(260, 18);
            this.SectionTxtBox.TabIndex = 1;
            this.SectionTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel7.CausesValidation = false;
            this.panel7.Controls.Add(this.DownloadUploadPanel);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(158, 33);
            this.panel7.TabIndex = 0;
            // 
            // DownloadUploadPanel
            // 
            this.DownloadUploadPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownloadUploadPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadUploadPanel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DownloadUploadPanel.Location = new System.Drawing.Point(0, 0);
            this.DownloadUploadPanel.Name = "DownloadUploadPanel";
            this.DownloadUploadPanel.Size = new System.Drawing.Size(158, 33);
            this.DownloadUploadPanel.TabIndex = 0;
            this.DownloadUploadPanel.Text = "Section";
            this.DownloadUploadPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CategoryTxtBox);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(10, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 35);
            this.panel1.TabIndex = 37;
            // 
            // CategoryTxtBox
            // 
            this.CategoryTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CategoryTxtBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryTxtBox.Location = new System.Drawing.Point(162, 7);
            this.CategoryTxtBox.Name = "CategoryTxtBox";
            this.CategoryTxtBox.ReadOnly = true;
            this.CategoryTxtBox.Size = new System.Drawing.Size(260, 18);
            this.CategoryTxtBox.TabIndex = 1;
            this.CategoryTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel3.CausesValidation = false;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(158, 33);
            this.panel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Category";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.DateTimeAppliedTxtBox);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Location = new System.Drawing.Point(455, 13);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(427, 35);
            this.panel6.TabIndex = 38;
            // 
            // DateTimeAppliedTxtBox
            // 
            this.DateTimeAppliedTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DateTimeAppliedTxtBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeAppliedTxtBox.Location = new System.Drawing.Point(162, 7);
            this.DateTimeAppliedTxtBox.Name = "DateTimeAppliedTxtBox";
            this.DateTimeAppliedTxtBox.ReadOnly = true;
            this.DateTimeAppliedTxtBox.Size = new System.Drawing.Size(260, 18);
            this.DateTimeAppliedTxtBox.TabIndex = 1;
            this.DateTimeAppliedTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel8.CausesValidation = false;
            this.panel8.Controls.Add(this.label3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(158, 33);
            this.panel8.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "Date && Time Applied";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.AppliedByTxtBox);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Location = new System.Drawing.Point(455, 54);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(427, 35);
            this.panel4.TabIndex = 39;
            // 
            // AppliedByTxtBox
            // 
            this.AppliedByTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AppliedByTxtBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppliedByTxtBox.Location = new System.Drawing.Point(162, 7);
            this.AppliedByTxtBox.Name = "AppliedByTxtBox";
            this.AppliedByTxtBox.ReadOnly = true;
            this.AppliedByTxtBox.Size = new System.Drawing.Size(260, 18);
            this.AppliedByTxtBox.TabIndex = 1;
            this.AppliedByTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel5.CausesValidation = false;
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(158, 33);
            this.panel5.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "Applied By";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RejectButton);
            this.panel2.Controls.Add(this.AcceptButton);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.ReasonOfApplicationPanel);
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.panel2.Size = new System.Drawing.Size(1034, 157);
            this.panel2.TabIndex = 36;
            // 
            // RejectButton
            // 
            this.RejectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(46)))), ((int)(((byte)(74)))));
            this.RejectButton.FlatAppearance.BorderSize = 0;
            this.RejectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RejectButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RejectButton.ForeColor = System.Drawing.Color.White;
            this.RejectButton.Location = new System.Drawing.Point(165, 111);
            this.RejectButton.Name = "RejectButton";
            this.RejectButton.Size = new System.Drawing.Size(144, 35);
            this.RejectButton.TabIndex = 41;
            this.RejectButton.Text = "REJECT";
            this.RejectButton.UseVisualStyleBackColor = false;
            this.RejectButton.Click += new System.EventHandler(this.RejectButton_Click);
            // 
            // AcceptButton
            // 
            this.AcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.AcceptButton.FlatAppearance.BorderSize = 0;
            this.AcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptButton.ForeColor = System.Drawing.Color.White;
            this.AcceptButton.Location = new System.Drawing.Point(10, 111);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(144, 35);
            this.AcceptButton.TabIndex = 40;
            this.AcceptButton.Text = "APPROVE";
            this.AcceptButton.UseVisualStyleBackColor = false;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.ViewApplicationDataGrid);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 199);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.panel12.Size = new System.Drawing.Size(1034, 338);
            this.panel12.TabIndex = 37;
            // 
            // ViewApplicationDataGrid
            // 
            this.ViewApplicationDataGrid.AllowUserToAddRows = false;
            this.ViewApplicationDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ViewApplicationDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ViewApplicationDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ViewApplicationDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ViewApplicationDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ViewApplicationDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.ViewApplicationDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewApplicationDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.ViewApplicationDataGrid.Location = new System.Drawing.Point(10, 5);
            this.ViewApplicationDataGrid.Name = "ViewApplicationDataGrid";
            this.ViewApplicationDataGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ViewApplicationDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ViewApplicationDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ViewApplicationDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.ViewApplicationDataGrid.RowTemplate.Height = 50;
            this.ViewApplicationDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ViewApplicationDataGrid.Size = new System.Drawing.Size(1014, 323);
            this.ViewApplicationDataGrid.TabIndex = 34;
            this.ViewApplicationDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ViewApplicationDataGrid_CellFormatting_1);
            // 
            // ViewApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1034, 537);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.TopPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewApplicationForm";
            this.Load += new System.EventHandler(this.ViewApplicationForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.ReasonOfApplicationPanel.ResumeLayout(false);
            this.ReasonOfApplicationPanel.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewApplicationDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label DateAndTimeLabel;
        private System.Windows.Forms.Label ReferenceNoLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Panel ReasonOfApplicationPanel;
        private System.Windows.Forms.TextBox SectionTxtBox;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label DownloadUploadPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox CategoryTxtBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox DateTimeAppliedTxtBox;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox AppliedByTxtBox;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button RejectButton;
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.DataGridView ViewApplicationDataGrid;
    }
}