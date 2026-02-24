namespace MHMS
{
    partial class UploadTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadTemplate));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.DateAndTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.CategoryDropdown = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.UploadTemplateDatagrid = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.SheetDropdownList = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.DownloadUploadPanel = new System.Windows.Forms.Label();
            this.ReasonOfApplicationTextBox = new System.Windows.Forms.TextBox();
            this.ReasonOfApplicationPanel = new System.Windows.Forms.Panel();
            this.UploadButton = new System.Windows.Forms.Button();
            this.WithSAPRadioButton = new System.Windows.Forms.RadioButton();
            this.NoSAPRadioButton = new System.Windows.Forms.RadioButton();
            this.TopPanel.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UploadTemplateDatagrid)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.ReasonOfApplicationPanel.SuspendLayout();
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
            this.TopPanel.Size = new System.Drawing.Size(1035, 48);
            this.TopPanel.TabIndex = 31;
            // 
            // DateAndTimeLabel
            // 
            this.DateAndTimeLabel.AutoSize = true;
            this.DateAndTimeLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.DateAndTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateAndTimeLabel.ForeColor = System.Drawing.Color.White;
            this.DateAndTimeLabel.Location = new System.Drawing.Point(942, 0);
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
            this.label1.Size = new System.Drawing.Size(152, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "UPLOAD TEMPLATE";
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.CategoryDropdown);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Location = new System.Drawing.Point(11, 63);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(400, 35);
            this.panel8.TabIndex = 32;
            // 
            // CategoryDropdown
            // 
            this.CategoryDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoryDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryDropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryDropdown.FormattingEnabled = true;
            this.CategoryDropdown.Location = new System.Drawing.Point(96, 4);
            this.CategoryDropdown.Name = "CategoryDropdown";
            this.CategoryDropdown.Size = new System.Drawing.Size(299, 25);
            this.CategoryDropdown.TabIndex = 2;
            this.CategoryDropdown.SelectedIndexChanged += new System.EventHandler(this.CategoryDropdown_SelectedIndexChanged);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel9.CausesValidation = false;
            this.panel9.Controls.Add(this.label3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(92, 33);
            this.panel9.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "Category:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ChooseFileButton);
            this.panel2.Controls.Add(this.FilePath);
            this.panel2.Controls.Add(this.BrowseButton);
            this.panel2.Location = new System.Drawing.Point(11, 113);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(608, 35);
            this.panel2.TabIndex = 33;
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
            this.FilePath.Location = new System.Drawing.Point(95, 7);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(431, 18);
            this.FilePath.TabIndex = 1;
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.BrowseButton.FlatAppearance.BorderSize = 0;
            this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseButton.ForeColor = System.Drawing.Color.White;
            this.BrowseButton.Location = new System.Drawing.Point(532, -1);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 35);
            this.BrowseButton.TabIndex = 40;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = false;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // UploadTemplateDatagrid
            // 
            this.UploadTemplateDatagrid.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UploadTemplateDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.UploadTemplateDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UploadTemplateDatagrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.UploadTemplateDatagrid.Location = new System.Drawing.Point(11, 165);
            this.UploadTemplateDatagrid.Name = "UploadTemplateDatagrid";
            this.UploadTemplateDatagrid.ReadOnly = true;
            this.UploadTemplateDatagrid.RowHeadersVisible = false;
            this.UploadTemplateDatagrid.Size = new System.Drawing.Size(1013, 313);
            this.UploadTemplateDatagrid.TabIndex = 40;
            this.UploadTemplateDatagrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.UploadTemplateDatagrid_CellFormatting);
            this.UploadTemplateDatagrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UploadSTTemplateDatagrid_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.SheetDropdownList);
            this.panel6.Location = new System.Drawing.Point(625, 113);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(247, 35);
            this.panel6.TabIndex = 43;
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
            this.SheetDropdownList.Size = new System.Drawing.Size(239, 24);
            this.SheetDropdownList.TabIndex = 2;
            this.SheetDropdownList.Text = "Select sheet";
            this.SheetDropdownList.SelectedIndexChanged += new System.EventHandler(this.SheetDropdownList_SelectedIndexChanged);
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
            this.DownloadUploadPanel.Text = "Reason of Application:";
            this.DownloadUploadPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReasonOfApplicationTextBox
            // 
            this.ReasonOfApplicationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReasonOfApplicationTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReasonOfApplicationTextBox.Location = new System.Drawing.Point(162, 7);
            this.ReasonOfApplicationTextBox.Name = "ReasonOfApplicationTextBox";
            this.ReasonOfApplicationTextBox.Size = new System.Drawing.Size(307, 18);
            this.ReasonOfApplicationTextBox.TabIndex = 1;
            // 
            // ReasonOfApplicationPanel
            // 
            this.ReasonOfApplicationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReasonOfApplicationPanel.Controls.Add(this.ReasonOfApplicationTextBox);
            this.ReasonOfApplicationPanel.Controls.Add(this.panel7);
            this.ReasonOfApplicationPanel.Location = new System.Drawing.Point(417, 63);
            this.ReasonOfApplicationPanel.Name = "ReasonOfApplicationPanel";
            this.ReasonOfApplicationPanel.Size = new System.Drawing.Size(474, 35);
            this.ReasonOfApplicationPanel.TabIndex = 44;
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
            this.UploadButton.Location = new System.Drawing.Point(897, 63);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(126, 35);
            this.UploadButton.TabIndex = 34;
            this.UploadButton.Text = "  UPLOAD";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // WithSAPRadioButton
            // 
            this.WithSAPRadioButton.AutoSize = true;
            this.WithSAPRadioButton.Location = new System.Drawing.Point(883, 123);
            this.WithSAPRadioButton.Name = "WithSAPRadioButton";
            this.WithSAPRadioButton.Size = new System.Drawing.Size(71, 17);
            this.WithSAPRadioButton.TabIndex = 45;
            this.WithSAPRadioButton.TabStop = true;
            this.WithSAPRadioButton.Text = "With SAP";
            this.WithSAPRadioButton.UseVisualStyleBackColor = true;
            // 
            // NoSAPRadioButton
            // 
            this.NoSAPRadioButton.AutoSize = true;
            this.NoSAPRadioButton.Location = new System.Drawing.Point(960, 123);
            this.NoSAPRadioButton.Name = "NoSAPRadioButton";
            this.NoSAPRadioButton.Size = new System.Drawing.Size(63, 17);
            this.NoSAPRadioButton.TabIndex = 46;
            this.NoSAPRadioButton.TabStop = true;
            this.NoSAPRadioButton.Text = "No SAP";
            this.NoSAPRadioButton.UseVisualStyleBackColor = true;
            // 
            // UploadTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1035, 490);
            this.Controls.Add(this.NoSAPRadioButton);
            this.Controls.Add(this.WithSAPRadioButton);
            this.Controls.Add(this.UploadTemplateDatagrid);
            this.Controls.Add(this.ReasonOfApplicationPanel);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UploadTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UploadSTTemplate_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UploadTemplateDatagrid)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ReasonOfApplicationPanel.ResumeLayout(false);
            this.ReasonOfApplicationPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label DateAndTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox CategoryDropdown;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.DataGridView UploadTemplateDatagrid;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox SheetDropdownList;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label DownloadUploadPanel;
        private System.Windows.Forms.TextBox ReasonOfApplicationTextBox;
        private System.Windows.Forms.Panel ReasonOfApplicationPanel;
        private System.Windows.Forms.RadioButton WithSAPRadioButton;
        private System.Windows.Forms.RadioButton NoSAPRadioButton;
    }
}