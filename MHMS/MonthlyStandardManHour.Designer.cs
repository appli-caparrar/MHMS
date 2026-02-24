namespace MHMS
{
    partial class MonthlyStandardManHour
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyStandardManHour));
            this.MonthlyStandardMHTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Month = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.Manhour = new System.Windows.Forms.TextBox();
            this.StandardMHDataGrid = new System.Windows.Forms.DataGridView();
            this.SaveButton = new System.Windows.Forms.Button();
            this.FiscalYearDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SectionLabel = new System.Windows.Forms.Label();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StandardMHDataGrid)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MonthlyStandardMHTimer
            // 
            this.MonthlyStandardMHTimer.Enabled = true;
            this.MonthlyStandardMHTimer.Tick += new System.EventHandler(this.MonthlyStandardMHTimer_Tick);
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
            this.button1.Size = new System.Drawing.Size(60, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Month";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Month);
            this.panel1.Location = new System.Drawing.Point(12, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 35);
            this.panel1.TabIndex = 24;
            // 
            // Month
            // 
            this.Month.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Month.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Month.Location = new System.Drawing.Point(64, 7);
            this.Month.Name = "Month";
            this.Month.Size = new System.Drawing.Size(160, 18);
            this.Month.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ChooseFileButton);
            this.panel2.Controls.Add(this.Manhour);
            this.panel2.Location = new System.Drawing.Point(247, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(295, 35);
            this.panel2.TabIndex = 23;
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
            this.ChooseFileButton.Size = new System.Drawing.Size(83, 36);
            this.ChooseFileButton.TabIndex = 2;
            this.ChooseFileButton.Text = "Man-Hour";
            this.ChooseFileButton.UseVisualStyleBackColor = false;
            // 
            // Manhour
            // 
            this.Manhour.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Manhour.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Manhour.Location = new System.Drawing.Point(87, 7);
            this.Manhour.Name = "Manhour";
            this.Manhour.Size = new System.Drawing.Size(203, 18);
            this.Manhour.TabIndex = 1;
            // 
            // StandardMHDataGrid
            // 
            this.StandardMHDataGrid.AllowUserToAddRows = false;
            this.StandardMHDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.StandardMHDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StandardMHDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.StandardMHDataGrid.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.StandardMHDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.StandardMHDataGrid.Location = new System.Drawing.Point(12, 154);
            this.StandardMHDataGrid.Name = "StandardMHDataGrid";
            this.StandardMHDataGrid.ReadOnly = true;
            this.StandardMHDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StandardMHDataGrid.Size = new System.Drawing.Size(530, 321);
            this.StandardMHDataGrid.TabIndex = 22;
            this.StandardMHDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StandardMHDataGrid_CellClick);
            this.StandardMHDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.StandardMHDataGrid_CellFormatting);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.SaveButton.FlatAppearance.BorderSize = 0;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Location = new System.Drawing.Point(391, 60);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(151, 35);
            this.SaveButton.TabIndex = 21;
            this.SaveButton.Text = "SAVE";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // FiscalYearDropdown
            // 
            this.FiscalYearDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.FiscalYearDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.FiscalYearDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FiscalYearDropdown.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiscalYearDropdown.FormattingEnabled = true;
            this.FiscalYearDropdown.Location = new System.Drawing.Point(96, 2);
            this.FiscalYearDropdown.Name = "FiscalYearDropdown";
            this.FiscalYearDropdown.Size = new System.Drawing.Size(273, 28);
            this.FiscalYearDropdown.TabIndex = 2;
            this.FiscalYearDropdown.SelectedIndexChanged += new System.EventHandler(this.FiscalYearDropdown_SelectedIndexChanged);
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
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fiscal Year";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.FiscalYearDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(11, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(374, 35);
            this.panel3.TabIndex = 20;
            // 
            // SectionLabel
            // 
            this.SectionLabel.AutoSize = true;
            this.SectionLabel.ForeColor = System.Drawing.Color.White;
            this.SectionLabel.Location = new System.Drawing.Point(8, 8);
            this.SectionLabel.Name = "SectionLabel";
            this.SectionLabel.Size = new System.Drawing.Size(43, 13);
            this.SectionLabel.TabIndex = 29;
            this.SectionLabel.Text = "Section";
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.AutoSize = true;
            this.DateTimeLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.DateTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DateTimeLabel.Location = new System.Drawing.Point(456, 0);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Padding = new System.Windows.Forms.Padding(0, 20, 10, 0);
            this.DateTimeLabel.Size = new System.Drawing.Size(98, 35);
            this.DateTimeLabel.TabIndex = 1;
            this.DateTimeLabel.Text = "Date and Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "MONTHLY STANDARD MAN-HOUR";
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.SectionLabel);
            this.TopPanel.Controls.Add(this.DateTimeLabel);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(554, 48);
            this.TopPanel.TabIndex = 19;
            // 
            // MonthlyStandardManHour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(554, 486);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.StandardMHDataGrid);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MonthlyStandardManHour";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monthly Standard Man-Hour";
            this.Load += new System.EventHandler(this.MonthlyStandardManHour_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StandardMHDataGrid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer MonthlyStandardMHTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Month;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.TextBox Manhour;
        private System.Windows.Forms.DataGridView StandardMHDataGrid;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ComboBox FiscalYearDropdown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label SectionLabel;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel TopPanel;
    }
}