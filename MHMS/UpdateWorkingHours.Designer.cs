namespace MHMS
{
    partial class UpdateWorkingHours
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.Month = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.WorkingDays = new System.Windows.Forms.TextBox();
            this.FiscalYearDropdown = new System.Windows.Forms.ComboBox();
            this.WorkingDaysHoursDatagrid = new System.Windows.Forms.DataGridView();
            this.SaveButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MonthlyStandardMHTimer = new System.Windows.Forms.Timer(this.components);
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.WorkingHours = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkingDaysHoursDatagrid)).BeginInit();
            this.panel3.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "UPDATE WORKING DAYS / HOURS";
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Month);
            this.panel1.Location = new System.Drawing.Point(226, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 35);
            this.panel1.TabIndex = 30;
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
            // Month
            // 
            this.Month.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Month.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Month.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Month.Location = new System.Drawing.Point(63, 7);
            this.Month.Name = "Month";
            this.Month.ReadOnly = true;
            this.Month.Size = new System.Drawing.Size(121, 18);
            this.Month.TabIndex = 1;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightYellow;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ChooseFileButton);
            this.panel2.Controls.Add(this.WorkingDays);
            this.panel2.Location = new System.Drawing.Point(11, 101);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 35);
            this.panel2.TabIndex = 29;
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
            this.ChooseFileButton.Size = new System.Drawing.Size(107, 36);
            this.ChooseFileButton.TabIndex = 2;
            this.ChooseFileButton.Text = "Working Days";
            this.ChooseFileButton.UseVisualStyleBackColor = false;
            // 
            // WorkingDays
            // 
            this.WorkingDays.BackColor = System.Drawing.Color.LightYellow;
            this.WorkingDays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WorkingDays.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkingDays.Location = new System.Drawing.Point(111, 7);
            this.WorkingDays.Name = "WorkingDays";
            this.WorkingDays.Size = new System.Drawing.Size(145, 18);
            this.WorkingDays.TabIndex = 1;
            // 
            // FiscalYearDropdown
            // 
            this.FiscalYearDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.FiscalYearDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.FiscalYearDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FiscalYearDropdown.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiscalYearDropdown.FormattingEnabled = true;
            this.FiscalYearDropdown.Location = new System.Drawing.Point(95, 2);
            this.FiscalYearDropdown.Name = "FiscalYearDropdown";
            this.FiscalYearDropdown.Size = new System.Drawing.Size(108, 28);
            this.FiscalYearDropdown.TabIndex = 2;
            this.FiscalYearDropdown.SelectedIndexChanged += new System.EventHandler(this.FiscalYearDropdown_SelectedIndexChanged);
            // 
            // WorkingDaysHoursDatagrid
            // 
            this.WorkingDaysHoursDatagrid.AllowUserToAddRows = false;
            this.WorkingDaysHoursDatagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.WorkingDaysHoursDatagrid.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WorkingDaysHoursDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.WorkingDaysHoursDatagrid.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.WorkingDaysHoursDatagrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.WorkingDaysHoursDatagrid.Location = new System.Drawing.Point(12, 145);
            this.WorkingDaysHoursDatagrid.Name = "WorkingDaysHoursDatagrid";
            this.WorkingDaysHoursDatagrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WorkingDaysHoursDatagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.WorkingDaysHoursDatagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WorkingDaysHoursDatagrid.Size = new System.Drawing.Size(530, 332);
            this.WorkingDaysHoursDatagrid.TabIndex = 28;
            this.WorkingDaysHoursDatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.WorkingDaysHoursDatagrid_CellClick);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.SaveButton.FlatAppearance.BorderSize = 0;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Location = new System.Drawing.Point(422, 58);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(120, 35);
            this.SaveButton.TabIndex = 27;
            this.SaveButton.Text = "SAVE";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.FiscalYearDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(11, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(209, 35);
            this.panel3.TabIndex = 26;
            // 
            // MonthlyStandardMHTimer
            // 
            this.MonthlyStandardMHTimer.Enabled = true;
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(554, 48);
            this.TopPanel.TabIndex = 25;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightYellow;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.WorkingHours);
            this.panel5.Location = new System.Drawing.Point(279, 101);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(263, 35);
            this.panel5.TabIndex = 31;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(-2, -2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 36);
            this.button2.TabIndex = 2;
            this.button2.Text = "Working Hours";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // WorkingHours
            // 
            this.WorkingHours.BackColor = System.Drawing.Color.LightYellow;
            this.WorkingHours.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WorkingHours.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkingHours.Location = new System.Drawing.Point(122, 7);
            this.WorkingHours.Name = "WorkingHours";
            this.WorkingHours.Size = new System.Drawing.Size(135, 18);
            this.WorkingHours.TabIndex = 1;
            // 
            // UpdateWorkingHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(554, 486);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.WorkingDaysHoursDatagrid);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UpdateWorkingHours";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateWorkingHours";
            this.Load += new System.EventHandler(this.UpdateWorkingHours_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkingDaysHoursDatagrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Month;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.TextBox WorkingDays;
        private System.Windows.Forms.ComboBox FiscalYearDropdown;
        private System.Windows.Forms.DataGridView WorkingDaysHoursDatagrid;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer MonthlyStandardMHTimer;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox WorkingHours;
    }
}