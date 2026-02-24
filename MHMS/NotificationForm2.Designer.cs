namespace MHMS
{
    partial class NotificationForm2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.NotificationDataGridView = new System.Windows.Forms.DataGridView();
            this.ApproverBtnPanel = new System.Windows.Forms.Panel();
            this.Recv_MGRBtn = new System.Windows.Forms.Button();
            this.Recv_SPVBtn = new System.Windows.Forms.Button();
            this.Recv_COPQProcessInchargeBtnsda = new System.Windows.Forms.Button();
            this.Recv_COPQPICBtn = new System.Windows.Forms.Button();
            this.App_MGRBtn = new System.Windows.Forms.Button();
            this.App_SPVBtn = new System.Windows.Forms.Button();
            this.App_COPQPICBtn = new System.Windows.Forms.Button();
            this.RolePanel = new System.Windows.Forms.Panel();
            this.ReceivingBtn = new System.Windows.Forms.Button();
            this.ApplyingBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SeeAllForApprovalButton = new System.Windows.Forms.Button();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.NotificationTopPanel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotificationDataGridView)).BeginInit();
            this.ApproverBtnPanel.SuspendLayout();
            this.RolePanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(936, 511);
            this.panel1.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.ApproverBtnPanel);
            this.panel4.Controls.Add(this.RolePanel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(10, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(916, 447);
            this.panel4.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.NotificationDataGridView);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 88);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(916, 359);
            this.panel5.TabIndex = 50;
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
            this.NotificationDataGridView.Location = new System.Drawing.Point(0, 0);
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
            this.NotificationDataGridView.Size = new System.Drawing.Size(916, 359);
            this.NotificationDataGridView.TabIndex = 50;
            // 
            // ApproverBtnPanel
            // 
            this.ApproverBtnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.ApproverBtnPanel.Controls.Add(this.Recv_MGRBtn);
            this.ApproverBtnPanel.Controls.Add(this.Recv_SPVBtn);
            this.ApproverBtnPanel.Controls.Add(this.Recv_COPQProcessInchargeBtnsda);
            this.ApproverBtnPanel.Controls.Add(this.Recv_COPQPICBtn);
            this.ApproverBtnPanel.Controls.Add(this.App_MGRBtn);
            this.ApproverBtnPanel.Controls.Add(this.App_SPVBtn);
            this.ApproverBtnPanel.Controls.Add(this.App_COPQPICBtn);
            this.ApproverBtnPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ApproverBtnPanel.Location = new System.Drawing.Point(0, 44);
            this.ApproverBtnPanel.Name = "ApproverBtnPanel";
            this.ApproverBtnPanel.Size = new System.Drawing.Size(916, 44);
            this.ApproverBtnPanel.TabIndex = 49;
            this.ApproverBtnPanel.Visible = false;
            // 
            // Recv_MGRBtn
            // 
            this.Recv_MGRBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.Recv_MGRBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.Recv_MGRBtn.FlatAppearance.BorderSize = 0;
            this.Recv_MGRBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Recv_MGRBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Recv_MGRBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.Recv_MGRBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Recv_MGRBtn.Location = new System.Drawing.Point(773, 0);
            this.Recv_MGRBtn.Name = "Recv_MGRBtn";
            this.Recv_MGRBtn.Size = new System.Drawing.Size(113, 44);
            this.Recv_MGRBtn.TabIndex = 54;
            this.Recv_MGRBtn.Text = "MGR (0)";
            this.Recv_MGRBtn.UseVisualStyleBackColor = false;
            this.Recv_MGRBtn.Click += new System.EventHandler(this.Recv_MGRBtn_Click);
            // 
            // Recv_SPVBtn
            // 
            this.Recv_SPVBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.Recv_SPVBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.Recv_SPVBtn.FlatAppearance.BorderSize = 0;
            this.Recv_SPVBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Recv_SPVBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Recv_SPVBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.Recv_SPVBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Recv_SPVBtn.Location = new System.Drawing.Point(660, 0);
            this.Recv_SPVBtn.Name = "Recv_SPVBtn";
            this.Recv_SPVBtn.Size = new System.Drawing.Size(113, 44);
            this.Recv_SPVBtn.TabIndex = 53;
            this.Recv_SPVBtn.Text = "SPV (0)";
            this.Recv_SPVBtn.UseVisualStyleBackColor = false;
            this.Recv_SPVBtn.Click += new System.EventHandler(this.Recv_SPVBtn_Click);
            // 
            // Recv_COPQProcessInchargeBtnsda
            // 
            this.Recv_COPQProcessInchargeBtnsda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.Recv_COPQProcessInchargeBtnsda.Dock = System.Windows.Forms.DockStyle.Left;
            this.Recv_COPQProcessInchargeBtnsda.FlatAppearance.BorderSize = 0;
            this.Recv_COPQProcessInchargeBtnsda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Recv_COPQProcessInchargeBtnsda.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Recv_COPQProcessInchargeBtnsda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.Recv_COPQProcessInchargeBtnsda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Recv_COPQProcessInchargeBtnsda.Location = new System.Drawing.Point(452, 0);
            this.Recv_COPQProcessInchargeBtnsda.Name = "Recv_COPQProcessInchargeBtnsda";
            this.Recv_COPQProcessInchargeBtnsda.Size = new System.Drawing.Size(208, 44);
            this.Recv_COPQProcessInchargeBtnsda.TabIndex = 52;
            this.Recv_COPQProcessInchargeBtnsda.Text = "COPQ Process In-Charge (0)";
            this.Recv_COPQProcessInchargeBtnsda.UseVisualStyleBackColor = false;
            this.Recv_COPQProcessInchargeBtnsda.Click += new System.EventHandler(this.Recv_COPQProcessInchargeBtn_Click);
            // 
            // Recv_COPQPICBtn
            // 
            this.Recv_COPQPICBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.Recv_COPQPICBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.Recv_COPQPICBtn.FlatAppearance.BorderSize = 0;
            this.Recv_COPQPICBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Recv_COPQPICBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Recv_COPQPICBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.Recv_COPQPICBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Recv_COPQPICBtn.Location = new System.Drawing.Point(339, 0);
            this.Recv_COPQPICBtn.Name = "Recv_COPQPICBtn";
            this.Recv_COPQPICBtn.Size = new System.Drawing.Size(113, 44);
            this.Recv_COPQPICBtn.TabIndex = 50;
            this.Recv_COPQPICBtn.Text = "COPQ PIC (0)";
            this.Recv_COPQPICBtn.UseVisualStyleBackColor = false;
            this.Recv_COPQPICBtn.Click += new System.EventHandler(this.Recv_COPQPICBtn_Click);
            // 
            // App_MGRBtn
            // 
            this.App_MGRBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.App_MGRBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.App_MGRBtn.FlatAppearance.BorderSize = 0;
            this.App_MGRBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.App_MGRBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App_MGRBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.App_MGRBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.App_MGRBtn.Location = new System.Drawing.Point(226, 0);
            this.App_MGRBtn.Name = "App_MGRBtn";
            this.App_MGRBtn.Size = new System.Drawing.Size(113, 44);
            this.App_MGRBtn.TabIndex = 49;
            this.App_MGRBtn.Text = "MGR (0)";
            this.App_MGRBtn.UseVisualStyleBackColor = false;
            this.App_MGRBtn.Click += new System.EventHandler(this.App_MGRBtn_Click);
            // 
            // App_SPVBtn
            // 
            this.App_SPVBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.App_SPVBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.App_SPVBtn.FlatAppearance.BorderSize = 0;
            this.App_SPVBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.App_SPVBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App_SPVBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.App_SPVBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.App_SPVBtn.Location = new System.Drawing.Point(113, 0);
            this.App_SPVBtn.Name = "App_SPVBtn";
            this.App_SPVBtn.Size = new System.Drawing.Size(113, 44);
            this.App_SPVBtn.TabIndex = 48;
            this.App_SPVBtn.Text = "SPV (0)";
            this.App_SPVBtn.UseVisualStyleBackColor = false;
            this.App_SPVBtn.Click += new System.EventHandler(this.App_SPVBtn_Click);
            // 
            // App_COPQPICBtn
            // 
            this.App_COPQPICBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.App_COPQPICBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.App_COPQPICBtn.FlatAppearance.BorderSize = 0;
            this.App_COPQPICBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.App_COPQPICBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App_COPQPICBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.App_COPQPICBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.App_COPQPICBtn.Location = new System.Drawing.Point(0, 0);
            this.App_COPQPICBtn.Name = "App_COPQPICBtn";
            this.App_COPQPICBtn.Size = new System.Drawing.Size(113, 44);
            this.App_COPQPICBtn.TabIndex = 46;
            this.App_COPQPICBtn.Text = "COPQ PIC (0)";
            this.App_COPQPICBtn.UseVisualStyleBackColor = false;
            this.App_COPQPICBtn.Click += new System.EventHandler(this.App_COPQPICBtn_Click);
            // 
            // RolePanel
            // 
            this.RolePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.RolePanel.Controls.Add(this.ReceivingBtn);
            this.RolePanel.Controls.Add(this.ApplyingBtn);
            this.RolePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RolePanel.Location = new System.Drawing.Point(0, 0);
            this.RolePanel.Name = "RolePanel";
            this.RolePanel.Size = new System.Drawing.Size(916, 44);
            this.RolePanel.TabIndex = 46;
            // 
            // ReceivingBtn
            // 
            this.ReceivingBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.ReceivingBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.ReceivingBtn.FlatAppearance.BorderSize = 0;
            this.ReceivingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReceivingBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReceivingBtn.ForeColor = System.Drawing.Color.White;
            this.ReceivingBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReceivingBtn.Location = new System.Drawing.Point(458, 0);
            this.ReceivingBtn.Name = "ReceivingBtn";
            this.ReceivingBtn.Size = new System.Drawing.Size(458, 44);
            this.ReceivingBtn.TabIndex = 47;
            this.ReceivingBtn.Text = "RECEIVING (0)";
            this.ReceivingBtn.UseVisualStyleBackColor = false;
            this.ReceivingBtn.Click += new System.EventHandler(this.ReceivingBtn_Click);
            // 
            // ApplyingBtn
            // 
            this.ApplyingBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.ApplyingBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.ApplyingBtn.FlatAppearance.BorderSize = 0;
            this.ApplyingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApplyingBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyingBtn.ForeColor = System.Drawing.Color.White;
            this.ApplyingBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ApplyingBtn.Location = new System.Drawing.Point(0, 0);
            this.ApplyingBtn.Name = "ApplyingBtn";
            this.ApplyingBtn.Size = new System.Drawing.Size(458, 44);
            this.ApplyingBtn.TabIndex = 46;
            this.ApplyingBtn.Text = "APPLYING (0)";
            this.ApplyingBtn.UseVisualStyleBackColor = false;
            this.ApplyingBtn.Click += new System.EventHandler(this.ApplyingBtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.SeeAllForApprovalButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(10, 457);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(916, 44);
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
            this.SeeAllForApprovalButton.Location = new System.Drawing.Point(804, 6);
            this.SeeAllForApprovalButton.Name = "SeeAllForApprovalButton";
            this.SeeAllForApprovalButton.Size = new System.Drawing.Size(111, 38);
            this.SeeAllForApprovalButton.TabIndex = 41;
            this.SeeAllForApprovalButton.Text = "      SEE ALL";
            this.SeeAllForApprovalButton.UseVisualStyleBackColor = false;
            this.SeeAllForApprovalButton.Click += new System.EventHandler(this.SeeAllForApprovalButton_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.CloseButton);
            this.TopPanel.Controls.Add(this.NotificationTopPanel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(936, 48);
            this.TopPanel.TabIndex = 4;
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::MHMS.Properties.Resources.delete;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(896, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(40, 48);
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
            this.NotificationTopPanel.Size = new System.Drawing.Size(936, 48);
            this.NotificationTopPanel.TabIndex = 0;
            this.NotificationTopPanel.Text = "NOTIFICATION";
            this.NotificationTopPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NotificationTopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NotificationTopPanel_MouseDown);
            // 
            // NotificationForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(936, 559);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotificationForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification Form";
            this.Load += new System.EventHandler(this.NotificationForm2_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NotificationDataGridView)).EndInit();
            this.ApproverBtnPanel.ResumeLayout(false);
            this.RolePanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel ApproverBtnPanel;
        private System.Windows.Forms.Button Recv_COPQProcessInchargeBtnsda;
        private System.Windows.Forms.Button Recv_COPQPICBtn;
        private System.Windows.Forms.Button App_MGRBtn;
        private System.Windows.Forms.Button App_SPVBtn;
        private System.Windows.Forms.Button App_COPQPICBtn;
        private System.Windows.Forms.Panel RolePanel;
        private System.Windows.Forms.Button ReceivingBtn;
        private System.Windows.Forms.Button ApplyingBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SeeAllForApprovalButton;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label NotificationTopPanel;
        private System.Windows.Forms.DataGridView NotificationDataGridView;
        private System.Windows.Forms.Button Recv_MGRBtn;
        private System.Windows.Forms.Button Recv_SPVBtn;
    }
}