namespace MHMS
{
    partial class TopContributorForm
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
            this.TopPanel = new System.Windows.Forms.Panel();
            this.ShowBtn = new System.Windows.Forms.Button();
            this.CategoryDropdown = new System.Windows.Forms.ComboBox();
            this.SectionDropdown = new System.Windows.Forms.ComboBox();
            this.MonthDropdown = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Top3ContributorLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Top3ContributorDaily = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Top3ContributorLabelMonthly = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Top3ContributorMonthly = new System.Windows.Forms.DataGridView();
            this.TopPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Top3ContributorDaily)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Top3ContributorMonthly)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.TopPanel.Controls.Add(this.ShowBtn);
            this.TopPanel.Controls.Add(this.CategoryDropdown);
            this.TopPanel.Controls.Add(this.SectionDropdown);
            this.TopPanel.Controls.Add(this.MonthDropdown);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(745, 42);
            this.TopPanel.TabIndex = 2;
            // 
            // ShowBtn
            // 
            this.ShowBtn.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ShowBtn.FlatAppearance.BorderSize = 0;
            this.ShowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowBtn.Location = new System.Drawing.Point(516, 10);
            this.ShowBtn.Name = "ShowBtn";
            this.ShowBtn.Size = new System.Drawing.Size(75, 23);
            this.ShowBtn.TabIndex = 5;
            this.ShowBtn.Text = "Show";
            this.ShowBtn.UseVisualStyleBackColor = false;
            this.ShowBtn.Click += new System.EventHandler(this.ShowBtn_Click);
            // 
            // CategoryDropdown
            // 
            this.CategoryDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryDropdown.FormattingEnabled = true;
            this.CategoryDropdown.Items.AddRange(new object[] {
            "Direct Efficiency",
            "Semi-direct Efficiency",
            "Total Loss Rate"});
            this.CategoryDropdown.Location = new System.Drawing.Point(11, 10);
            this.CategoryDropdown.Name = "CategoryDropdown";
            this.CategoryDropdown.Size = new System.Drawing.Size(173, 23);
            this.CategoryDropdown.TabIndex = 2;
            this.CategoryDropdown.Text = "- - Select Category - -";
            this.CategoryDropdown.SelectedIndexChanged += new System.EventHandler(this.CategoryDropdown_SelectedIndexChanged);
            // 
            // SectionDropdown
            // 
            this.SectionDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SectionDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionDropdown.FormattingEnabled = true;
            this.SectionDropdown.Location = new System.Drawing.Point(190, 10);
            this.SectionDropdown.Name = "SectionDropdown";
            this.SectionDropdown.Size = new System.Drawing.Size(157, 23);
            this.SectionDropdown.TabIndex = 1;
            this.SectionDropdown.Text = "- - Select Section - -";
            this.SectionDropdown.DropDown += new System.EventHandler(this.SectionDropdown_DropDown);
            // 
            // MonthDropdown
            // 
            this.MonthDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MonthDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonthDropdown.FormattingEnabled = true;
            this.MonthDropdown.Items.AddRange(new object[] {
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
            this.MonthDropdown.Location = new System.Drawing.Point(353, 10);
            this.MonthDropdown.Name = "MonthDropdown";
            this.MonthDropdown.Size = new System.Drawing.Size(157, 23);
            this.MonthDropdown.TabIndex = 4;
            this.MonthDropdown.Text = "- - Select Month - -";
            this.MonthDropdown.SelectedIndexChanged += new System.EventHandler(this.MonthDropdown_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel2.Controls.Add(this.dateTimePicker);
            this.panel2.Controls.Add(this.Top3ContributorLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(745, 36);
            this.panel2.TabIndex = 4;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(535, 8);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker.TabIndex = 1;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // Top3ContributorLabel
            // 
            this.Top3ContributorLabel.AutoSize = true;
            this.Top3ContributorLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Top3ContributorLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Top3ContributorLabel.Location = new System.Drawing.Point(7, 9);
            this.Top3ContributorLabel.Name = "Top3ContributorLabel";
            this.Top3ContributorLabel.Size = new System.Drawing.Size(157, 19);
            this.Top3ContributorLabel.TabIndex = 0;
            this.Top3ContributorLabel.Text = "Top 3 Contributor Daily";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Top3ContributorDaily);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 78);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.panel3.Size = new System.Drawing.Size(745, 163);
            this.panel3.TabIndex = 5;
            // 
            // Top3ContributorDaily
            // 
            this.Top3ContributorDaily.AllowUserToAddRows = false;
            this.Top3ContributorDaily.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Top3ContributorDaily.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.Top3ContributorDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Top3ContributorDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Top3ContributorDaily.Location = new System.Drawing.Point(10, 8);
            this.Top3ContributorDaily.Name = "Top3ContributorDaily";
            this.Top3ContributorDaily.ReadOnly = true;
            this.Top3ContributorDaily.RowHeadersVisible = false;
            this.Top3ContributorDaily.Size = new System.Drawing.Size(725, 147);
            this.Top3ContributorDaily.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel1.Controls.Add(this.Top3ContributorLabelMonthly);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 241);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 36);
            this.panel1.TabIndex = 6;
            // 
            // Top3ContributorLabelMonthly
            // 
            this.Top3ContributorLabelMonthly.AutoSize = true;
            this.Top3ContributorLabelMonthly.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Top3ContributorLabelMonthly.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Top3ContributorLabelMonthly.Location = new System.Drawing.Point(7, 9);
            this.Top3ContributorLabelMonthly.Name = "Top3ContributorLabelMonthly";
            this.Top3ContributorLabelMonthly.Size = new System.Drawing.Size(178, 19);
            this.Top3ContributorLabelMonthly.TabIndex = 0;
            this.Top3ContributorLabelMonthly.Text = "Top 3 Contributor Monthly";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.Top3ContributorMonthly);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 277);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.panel4.Size = new System.Drawing.Size(745, 163);
            this.panel4.TabIndex = 7;
            // 
            // Top3ContributorMonthly
            // 
            this.Top3ContributorMonthly.AllowUserToAddRows = false;
            this.Top3ContributorMonthly.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Top3ContributorMonthly.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.Top3ContributorMonthly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Top3ContributorMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Top3ContributorMonthly.Location = new System.Drawing.Point(10, 8);
            this.Top3ContributorMonthly.Name = "Top3ContributorMonthly";
            this.Top3ContributorMonthly.Size = new System.Drawing.Size(725, 147);
            this.Top3ContributorMonthly.TabIndex = 1;
            // 
            // TopContributorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(745, 441);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TopContributorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TopContributorForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Top3ContributorDaily)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Top3ContributorMonthly)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Top3ContributorLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView Top3ContributorDaily;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Top3ContributorLabelMonthly;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView Top3ContributorMonthly;
        private System.Windows.Forms.ComboBox MonthDropdown;
        private System.Windows.Forms.ComboBox SectionDropdown;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.ComboBox CategoryDropdown;
        private System.Windows.Forms.Button ShowBtn;
    }
}