namespace MHMS
{
    partial class NotificationForm
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
            this.TopPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.NotificationTopPanel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.DataGridContainer = new System.Windows.Forms.Panel();
            this.NotificationDataGridView = new System.Windows.Forms.DataGridView();
            this.RolePanel = new System.Windows.Forms.Panel();
            this.MGRButton = new System.Windows.Forms.Button();
            this.SPVButton = new System.Windows.Forms.Button();
            this.COPQProcessInchargeButton = new System.Windows.Forms.Button();
            this.COPQPICButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SeeAllForApprovalButton = new System.Windows.Forms.Button();
            this.mH_Management_SystemDataSet1 = new MHMS.MH_Management_SystemDataSet();
            this.TopPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.DataGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotificationDataGridView)).BeginInit();
            this.RolePanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.CloseButton);
            this.TopPanel.Controls.Add(this.NotificationTopPanel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(935, 48);
            this.TopPanel.TabIndex = 2;
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::MHMS.Properties.Resources.delete;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(881, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(54, 48);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // NotificationTopPanel
            // 
            this.NotificationTopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotificationTopPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotificationTopPanel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.NotificationTopPanel.Location = new System.Drawing.Point(0, 0);
            this.NotificationTopPanel.Name = "NotificationTopPanel";
            this.NotificationTopPanel.Size = new System.Drawing.Size(935, 48);
            this.NotificationTopPanel.TabIndex = 0;
            this.NotificationTopPanel.Text = "NOTIFICATION";
            this.NotificationTopPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NotificationTopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NotificationTopPanel_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(935, 511);
            this.panel1.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.DataGridContainer);
            this.panel4.Controls.Add(this.RolePanel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(10, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(915, 447);
            this.panel4.TabIndex = 2;
            // 
            // DataGridContainer
            // 
            this.DataGridContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.DataGridContainer.Controls.Add(this.NotificationDataGridView);
            this.DataGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridContainer.Location = new System.Drawing.Point(0, 44);
            this.DataGridContainer.Name = "DataGridContainer";
            this.DataGridContainer.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.DataGridContainer.Size = new System.Drawing.Size(915, 403);
            this.DataGridContainer.TabIndex = 47;
            // 
            // NotificationDataGridView
            // 
            this.NotificationDataGridView.AllowUserToAddRows = false;
            this.NotificationDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.NotificationDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.NotificationDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NotificationDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.NotificationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NotificationDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.NotificationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotificationDataGridView.Location = new System.Drawing.Point(0, 10);
            this.NotificationDataGridView.Name = "NotificationDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NotificationDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.NotificationDataGridView.RowHeadersVisible = false;
            this.NotificationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NotificationDataGridView.Size = new System.Drawing.Size(915, 393);
            this.NotificationDataGridView.TabIndex = 49;
            this.NotificationDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.NotificationDataGridView_CellFormatting);
            // 
            // RolePanel
            // 
            this.RolePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.RolePanel.Controls.Add(this.MGRButton);
            this.RolePanel.Controls.Add(this.SPVButton);
            this.RolePanel.Controls.Add(this.COPQProcessInchargeButton);
            this.RolePanel.Controls.Add(this.COPQPICButton);
            this.RolePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RolePanel.Location = new System.Drawing.Point(0, 0);
            this.RolePanel.Name = "RolePanel";
            this.RolePanel.Size = new System.Drawing.Size(915, 44);
            this.RolePanel.TabIndex = 46;
            // 
            // MGRButton
            // 
            this.MGRButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.MGRButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.MGRButton.FlatAppearance.BorderSize = 0;
            this.MGRButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MGRButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MGRButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.MGRButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MGRButton.Location = new System.Drawing.Point(630, 0);
            this.MGRButton.Name = "MGRButton";
            this.MGRButton.Size = new System.Drawing.Size(210, 44);
            this.MGRButton.TabIndex = 45;
            this.MGRButton.Text = "MGR (0)";
            this.MGRButton.UseVisualStyleBackColor = false;
            this.MGRButton.Click += new System.EventHandler(this.MGRButton_Click);
            // 
            // SPVButton
            // 
            this.SPVButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.SPVButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.SPVButton.FlatAppearance.BorderSize = 0;
            this.SPVButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SPVButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPVButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.SPVButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SPVButton.Location = new System.Drawing.Point(420, 0);
            this.SPVButton.Name = "SPVButton";
            this.SPVButton.Size = new System.Drawing.Size(210, 44);
            this.SPVButton.TabIndex = 44;
            this.SPVButton.Text = "SPV (0)";
            this.SPVButton.UseVisualStyleBackColor = false;
            this.SPVButton.Click += new System.EventHandler(this.SPVButton_Click);
            // 
            // COPQProcessInchargeButton
            // 
            this.COPQProcessInchargeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.COPQProcessInchargeButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.COPQProcessInchargeButton.FlatAppearance.BorderSize = 0;
            this.COPQProcessInchargeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.COPQProcessInchargeButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.COPQProcessInchargeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.COPQProcessInchargeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.COPQProcessInchargeButton.Location = new System.Drawing.Point(210, 0);
            this.COPQProcessInchargeButton.Name = "COPQProcessInchargeButton";
            this.COPQProcessInchargeButton.Size = new System.Drawing.Size(210, 44);
            this.COPQProcessInchargeButton.TabIndex = 43;
            this.COPQProcessInchargeButton.Text = "COPQ Process In-Charge (0)";
            this.COPQProcessInchargeButton.UseVisualStyleBackColor = false;
            this.COPQProcessInchargeButton.Click += new System.EventHandler(this.COPQProcessInchargeButton_Click);
            // 
            // COPQPICButton
            // 
            this.COPQPICButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.COPQPICButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.COPQPICButton.FlatAppearance.BorderSize = 0;
            this.COPQPICButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.COPQPICButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.COPQPICButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.COPQPICButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.COPQPICButton.Location = new System.Drawing.Point(0, 0);
            this.COPQPICButton.Name = "COPQPICButton";
            this.COPQPICButton.Size = new System.Drawing.Size(210, 44);
            this.COPQPICButton.TabIndex = 42;
            this.COPQPICButton.Text = "COPQ PIC (0)";
            this.COPQPICButton.UseVisualStyleBackColor = false;
            this.COPQPICButton.Click += new System.EventHandler(this.COPQPICButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.SeeAllForApprovalButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(10, 457);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(915, 44);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(-3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(635, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Note: For this table, it would only show the latest (10) for approval items for e" +
    "ach process. Click see all to redirect to the approval form.";
            // 
            // SeeAllForApprovalButton
            // 
            this.SeeAllForApprovalButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.SeeAllForApprovalButton.FlatAppearance.BorderSize = 0;
            this.SeeAllForApprovalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SeeAllForApprovalButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeeAllForApprovalButton.ForeColor = System.Drawing.Color.White;
            this.SeeAllForApprovalButton.Image = global::MHMS.Properties.Resources.show_property_24;
            this.SeeAllForApprovalButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SeeAllForApprovalButton.Location = new System.Drawing.Point(795, 9);
            this.SeeAllForApprovalButton.Name = "SeeAllForApprovalButton";
            this.SeeAllForApprovalButton.Size = new System.Drawing.Size(120, 35);
            this.SeeAllForApprovalButton.TabIndex = 41;
            this.SeeAllForApprovalButton.Text = "      SEE ALL";
            this.SeeAllForApprovalButton.UseVisualStyleBackColor = false;
            this.SeeAllForApprovalButton.Click += new System.EventHandler(this.SeeAllForApprovalButton_Click);
            // 
            // mH_Management_SystemDataSet1
            // 
            this.mH_Management_SystemDataSet1.DataSetName = "MH_Management_SystemDataSet";
            this.mH_Management_SystemDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 559);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NotificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification Form";
            this.Load += new System.EventHandler(this.NotificationForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.DataGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NotificationDataGridView)).EndInit();
            this.RolePanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mH_Management_SystemDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label NotificationTopPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button SeeAllForApprovalButton;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel DataGridContainer;
        private System.Windows.Forms.DataGridView NotificationDataGridView;
        private System.Windows.Forms.Panel RolePanel;
        private System.Windows.Forms.Button MGRButton;
        private System.Windows.Forms.Button SPVButton;
        private System.Windows.Forms.Button COPQProcessInchargeButton;
        private System.Windows.Forms.Button COPQPICButton;
        private System.Windows.Forms.Label label1;
        private MH_Management_SystemDataSet mH_Management_SystemDataSet1;
    }
}