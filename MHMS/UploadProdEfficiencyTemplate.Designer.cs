namespace MHMS
{
    partial class UploadProdEfficiencyTemplate
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
            this.SectionDropdownPanel = new System.Windows.Forms.Panel();
            this.TemplateDropdownList = new System.Windows.Forms.ComboBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SheetDropdownList = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.YearDropdown = new System.Windows.Forms.ComboBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.MonthDropdwn = new System.Windows.Forms.ComboBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.UploadButton = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.EfficiencyDatagrid = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SectionDropdownPanel.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EfficiencyDatagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SectionDropdownPanel
            // 
            this.SectionDropdownPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SectionDropdownPanel.Controls.Add(this.TemplateDropdownList);
            this.SectionDropdownPanel.Controls.Add(this.panel12);
            this.SectionDropdownPanel.Location = new System.Drawing.Point(13, 12);
            this.SectionDropdownPanel.Name = "SectionDropdownPanel";
            this.SectionDropdownPanel.Size = new System.Drawing.Size(308, 35);
            this.SectionDropdownPanel.TabIndex = 10;
            // 
            // TemplateDropdownList
            // 
            this.TemplateDropdownList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TemplateDropdownList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.TemplateDropdownList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TemplateDropdownList.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemplateDropdownList.FormattingEnabled = true;
            this.TemplateDropdownList.Location = new System.Drawing.Point(94, 2);
            this.TemplateDropdownList.Name = "TemplateDropdownList";
            this.TemplateDropdownList.Size = new System.Drawing.Size(208, 28);
            this.TemplateDropdownList.TabIndex = 2;
       
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
            this.label4.Text = "Template";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SheetDropdownList);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(328, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 35);
            this.panel1.TabIndex = 11;
            // 
            // SheetDropdownList
            // 
            this.SheetDropdownList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SheetDropdownList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SheetDropdownList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SheetDropdownList.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SheetDropdownList.FormattingEnabled = true;
            this.SheetDropdownList.Location = new System.Drawing.Point(95, 2);
            this.SheetDropdownList.Name = "SheetDropdownList";
            this.SheetDropdownList.Size = new System.Drawing.Size(285, 28);
            this.SheetDropdownList.TabIndex = 2;
            this.SheetDropdownList.SelectedIndexChanged += new System.EventHandler(this.SheetDropdownList_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel2.CausesValidation = false;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(90, 33);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sheet";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.BrowseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.BrowseButton.FlatAppearance.BorderSize = 0;
            this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseButton.ForeColor = System.Drawing.Color.White;
            this.BrowseButton.Location = new System.Drawing.Point(470, 0);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(64, 33);
            this.BrowseButton.TabIndex = 30;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = false;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.BrowseButton);
            this.panel3.Controls.Add(this.ChooseFileButton);
            this.panel3.Controls.Add(this.FilePath);
            this.panel3.Location = new System.Drawing.Point(328, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(536, 35);
            this.panel3.TabIndex = 29;
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
            // 
            // FilePath
            // 
            this.FilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FilePath.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePath.Location = new System.Drawing.Point(95, 7);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(370, 18);
            this.FilePath.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.UploadButton);
            this.panel4.Controls.Add(this.SectionDropdownPanel);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(877, 102);
            this.panel4.TabIndex = 31;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.YearDropdown);
            this.panel8.Location = new System.Drawing.Point(221, 53);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(100, 35);
            this.panel8.TabIndex = 47;
            // 
            // YearDropdown
            // 
            this.YearDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.YearDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.YearDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.YearDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YearDropdown.FormattingEnabled = true;
            this.YearDropdown.Location = new System.Drawing.Point(3, 5);
            this.YearDropdown.Name = "YearDropdown";
            this.YearDropdown.Size = new System.Drawing.Size(92, 24);
            this.YearDropdown.TabIndex = 2;
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.panel9);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Location = new System.Drawing.Point(13, 53);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(308, 35);
            this.panel10.TabIndex = 48;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.MonthDropdwn);
            this.panel9.Location = new System.Drawing.Point(90, -1);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(117, 35);
            this.panel9.TabIndex = 46;
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
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel11.CausesValidation = false;
            this.panel11.Controls.Add(this.label3);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(90, 33);
            this.panel11.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "Month/Year";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.UploadButton.Location = new System.Drawing.Point(720, 53);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(144, 35);
            this.UploadButton.TabIndex = 30;
            this.UploadButton.Text = " UPLOAD";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.EfficiencyDatagrid);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 102);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(877, 344);
            this.panel5.TabIndex = 32;
            // 
            // EfficiencyDatagrid
            // 
            this.EfficiencyDatagrid.AllowUserToAddRows = false;
            this.EfficiencyDatagrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.EfficiencyDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EfficiencyDatagrid.Location = new System.Drawing.Point(12, 6);
            this.EfficiencyDatagrid.Name = "EfficiencyDatagrid";
            this.EfficiencyDatagrid.RowHeadersVisible = false;
            this.EfficiencyDatagrid.Size = new System.Drawing.Size(853, 326);
            this.EfficiencyDatagrid.TabIndex = 0;
            this.EfficiencyDatagrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ProdEfficiencyDatagrid_CellFormatting);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;

            // 
            // UploadProdEfficiencyTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(877, 446);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UploadProdEfficiencyTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload Efficiency Template";
            this.Load += new System.EventHandler(this.UploadProdEfficiencyTemplate_Load);
            this.SectionDropdownPanel.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EfficiencyDatagrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SectionDropdownPanel;
        private System.Windows.Forms.ComboBox TemplateDropdownList;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox SheetDropdownList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView EfficiencyDatagrid;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox YearDropdown;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ComboBox MonthDropdwn;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}