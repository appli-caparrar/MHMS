namespace MHMS
{
    partial class ViewNewlyAppliedForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewNewlyAppliedForm));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.AppCategoryLabel = new System.Windows.Forms.Label();
            this.DateAndTimeLabel = new System.Windows.Forms.Label();
            this.ApplicationTypeLabel = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ViewNewlyAppliedDataGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.CancelApplicationBtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ExportButton = new System.Windows.Forms.Button();
            this.TopPanel.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewNewlyAppliedDataGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.AppCategoryLabel);
            this.TopPanel.Controls.Add(this.DateAndTimeLabel);
            this.TopPanel.Controls.Add(this.ApplicationTypeLabel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1034, 42);
            this.TopPanel.TabIndex = 34;
            // 
            // AppCategoryLabel
            // 
            this.AppCategoryLabel.AutoSize = true;
            this.AppCategoryLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.AppCategoryLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppCategoryLabel.ForeColor = System.Drawing.SystemColors.Window;
            this.AppCategoryLabel.Location = new System.Drawing.Point(135, 0);
            this.AppCategoryLabel.Name = "AppCategoryLabel";
            this.AppCategoryLabel.Padding = new System.Windows.Forms.Padding(9, 13, 0, 0);
            this.AppCategoryLabel.Size = new System.Drawing.Size(91, 30);
            this.AppCategoryLabel.TabIndex = 19;
            this.AppCategoryLabel.Text = "<Category>";
            this.AppCategoryLabel.Visible = false;
            // 
            // DateAndTimeLabel
            // 
            this.DateAndTimeLabel.AutoSize = true;
            this.DateAndTimeLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.DateAndTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateAndTimeLabel.ForeColor = System.Drawing.Color.White;
            this.DateAndTimeLabel.Location = new System.Drawing.Point(941, 0);
            this.DateAndTimeLabel.Name = "DateAndTimeLabel";
            this.DateAndTimeLabel.Padding = new System.Windows.Forms.Padding(0, 14, 9, 0);
            this.DateAndTimeLabel.Size = new System.Drawing.Size(93, 29);
            this.DateAndTimeLabel.TabIndex = 18;
            this.DateAndTimeLabel.Text = "Date and Time";
            // 
            // ApplicationTypeLabel
            // 
            this.ApplicationTypeLabel.AutoSize = true;
            this.ApplicationTypeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ApplicationTypeLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplicationTypeLabel.ForeColor = System.Drawing.SystemColors.Window;
            this.ApplicationTypeLabel.Location = new System.Drawing.Point(0, 0);
            this.ApplicationTypeLabel.Name = "ApplicationTypeLabel";
            this.ApplicationTypeLabel.Padding = new System.Windows.Forms.Padding(9, 13, 0, 0);
            this.ApplicationTypeLabel.Size = new System.Drawing.Size(135, 30);
            this.ApplicationTypeLabel.TabIndex = 0;
            this.ApplicationTypeLabel.Text = "<Application Type>";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.panel2);
            this.panel12.Controls.Add(this.panel1);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 42);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(10);
            this.panel12.Size = new System.Drawing.Size(1034, 495);
            this.panel12.TabIndex = 38;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ViewNewlyAppliedDataGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel2.Size = new System.Drawing.Size(1014, 436);
            this.panel2.TabIndex = 36;
            // 
            // ViewNewlyAppliedDataGrid
            // 
            this.ViewNewlyAppliedDataGrid.AllowUserToAddRows = false;
            this.ViewNewlyAppliedDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ViewNewlyAppliedDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ViewNewlyAppliedDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ViewNewlyAppliedDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ViewNewlyAppliedDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ViewNewlyAppliedDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.ViewNewlyAppliedDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewNewlyAppliedDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.ViewNewlyAppliedDataGrid.Location = new System.Drawing.Point(0, 0);
            this.ViewNewlyAppliedDataGrid.Name = "ViewNewlyAppliedDataGrid";
            this.ViewNewlyAppliedDataGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ViewNewlyAppliedDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ViewNewlyAppliedDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ViewNewlyAppliedDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.ViewNewlyAppliedDataGrid.RowTemplate.Height = 50;
            this.ViewNewlyAppliedDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ViewNewlyAppliedDataGrid.Size = new System.Drawing.Size(1014, 426);
            this.ViewNewlyAppliedDataGrid.TabIndex = 34;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 446);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 39);
            this.panel1.TabIndex = 35;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.CancelApplicationBtn);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.ExportButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(676, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(338, 39);
            this.panel3.TabIndex = 0;
            // 
            // CancelApplicationBtn
            // 
            this.CancelApplicationBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(46)))), ((int)(((byte)(74)))));
            this.CancelApplicationBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.CancelApplicationBtn.FlatAppearance.BorderSize = 0;
            this.CancelApplicationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelApplicationBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelApplicationBtn.ForeColor = System.Drawing.Color.White;
            this.CancelApplicationBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelApplicationBtn.Image")));
            this.CancelApplicationBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelApplicationBtn.Location = new System.Drawing.Point(83, 0);
            this.CancelApplicationBtn.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.CancelApplicationBtn.Name = "CancelApplicationBtn";
            this.CancelApplicationBtn.Size = new System.Drawing.Size(120, 39);
            this.CancelApplicationBtn.TabIndex = 19;
            this.CancelApplicationBtn.Text = "  CANCEL";
            this.CancelApplicationBtn.UseVisualStyleBackColor = false;
            this.CancelApplicationBtn.Click += new System.EventHandler(this.CancelApplicationBtn_Click);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(203, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 39);
            this.panel4.TabIndex = 18;
            // 
            // ExportButton
            // 
            this.ExportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.ExportButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExportButton.FlatAppearance.BorderSize = 0;
            this.ExportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportButton.ForeColor = System.Drawing.Color.White;
            this.ExportButton.Image = global::MHMS.Properties.Resources.download_2_24__2_;
            this.ExportButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportButton.Location = new System.Drawing.Point(213, 0);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(125, 39);
            this.ExportButton.TabIndex = 17;
            this.ExportButton.Text = "EXPORT";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ViewNewlyAppliedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1034, 537);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.TopPanel);
            this.Name = "ViewNewlyAppliedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Newly Applied";
            this.Load += new System.EventHandler(this.ViewNewlyAppliedForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewNewlyAppliedDataGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label DateAndTimeLabel;
        private System.Windows.Forms.Label ApplicationTypeLabel;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.DataGridView ViewNewlyAppliedDataGrid;
        private System.Windows.Forms.Label AppCategoryLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button CancelApplicationBtn;
        private System.Windows.Forms.Panel panel4;
    }
}