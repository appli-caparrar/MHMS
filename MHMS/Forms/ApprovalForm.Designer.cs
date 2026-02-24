namespace MHMS.Forms
{
    partial class ApprovalForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApprovalForm));
            this.panel3 = new System.Windows.Forms.Panel();
            this.CategoryDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TypeText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StatusDropdown = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.GeneratePreviousQIForConfirmationBtn = new System.Windows.Forms.Button();
            this.ApproveAllPendingBtn = new System.Windows.Forms.Button();
            this.ExcludeCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.RoleDropDown = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TypeofApprovalDropdown = new System.Windows.Forms.ComboBox();
            this.SelectAllChkBox = new System.Windows.Forms.CheckBox();
            this.RejectButton = new System.Windows.Forms.Button();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.ApprovalCount = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.FrefreshDatagridTimer = new System.Windows.Forms.Timer(this.components);
            this.ApprovalDataGrid = new System.Windows.Forms.DataGridView();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApprovalDataGrid)).BeginInit();
            this.panel14.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.CategoryDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(183, 35);
            this.panel3.TabIndex = 9;
            // 
            // CategoryDropdown
            // 
            this.CategoryDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CategoryDropdown.Enabled = false;
            this.CategoryDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryDropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryDropdown.FormattingEnabled = true;
            this.CategoryDropdown.Items.AddRange(new object[] {
            "COPQ"});
            this.CategoryDropdown.Location = new System.Drawing.Point(81, 4);
            this.CategoryDropdown.Name = "CategoryDropdown";
            this.CategoryDropdown.Size = new System.Drawing.Size(96, 25);
            this.CategoryDropdown.TabIndex = 2;
            this.CategoryDropdown.Text = "COPQ";
            this.CategoryDropdown.SelectedIndexChanged += new System.EventHandler(this.CategoryDropdown_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel4.CausesValidation = false;
            this.panel4.Controls.Add(this.TypeText);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(76, 33);
            this.panel4.TabIndex = 0;
            // 
            // TypeText
            // 
            this.TypeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TypeText.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeText.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TypeText.Location = new System.Drawing.Point(0, 0);
            this.TypeText.Name = "TypeText";
            this.TypeText.Size = new System.Drawing.Size(76, 33);
            this.TypeText.TabIndex = 0;
            this.TypeText.Text = "Category";
            this.TypeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.StatusDropdown);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(691, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(191, 35);
            this.panel1.TabIndex = 10;
            // 
            // StatusDropdown
            // 
            this.StatusDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.StatusDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.StatusDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StatusDropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusDropdown.FormattingEnabled = true;
            this.StatusDropdown.Items.AddRange(new object[] {
            "For Approval",
            "Approved",
            "Rejected",
            "Cancelled"});
            this.StatusDropdown.Location = new System.Drawing.Point(65, 4);
            this.StatusDropdown.Name = "StatusDropdown";
            this.StatusDropdown.Size = new System.Drawing.Size(121, 25);
            this.StatusDropdown.TabIndex = 2;
            this.StatusDropdown.Text = "For Approval";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel2.CausesValidation = false;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(62, 33);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Status";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.GeneratePreviousQIForConfirmationBtn);
            this.panel5.Controls.Add(this.ApproveAllPendingBtn);
            this.panel5.Controls.Add(this.ExcludeCheckBox);
            this.panel5.Controls.Add(this.GenerateButton);
            this.panel5.Controls.Add(this.panel12);
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Controls.Add(this.SelectAllChkBox);
            this.panel5.Controls.Add(this.RejectButton);
            this.panel5.Controls.Add(this.AcceptButton);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(10, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1179, 130);
            this.panel5.TabIndex = 11;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // GeneratePreviousQIForConfirmationBtn
            // 
            this.GeneratePreviousQIForConfirmationBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(190)))), ((int)(((byte)(118)))));
            this.GeneratePreviousQIForConfirmationBtn.FlatAppearance.BorderSize = 0;
            this.GeneratePreviousQIForConfirmationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GeneratePreviousQIForConfirmationBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GeneratePreviousQIForConfirmationBtn.ForeColor = System.Drawing.Color.White;
            this.GeneratePreviousQIForConfirmationBtn.Location = new System.Drawing.Point(238, 98);
            this.GeneratePreviousQIForConfirmationBtn.Name = "GeneratePreviousQIForConfirmationBtn";
            this.GeneratePreviousQIForConfirmationBtn.Size = new System.Drawing.Size(132, 26);
            this.GeneratePreviousQIForConfirmationBtn.TabIndex = 40;
            this.GeneratePreviousQIForConfirmationBtn.Text = "Generate Previous";
            this.GeneratePreviousQIForConfirmationBtn.UseVisualStyleBackColor = false;
            this.GeneratePreviousQIForConfirmationBtn.Visible = false;
            this.GeneratePreviousQIForConfirmationBtn.Click += new System.EventHandler(this.GeneratePreviousQIForConfirmationBtn_Click);
            // 
            // ApproveAllPendingBtn
            // 
            this.ApproveAllPendingBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(190)))), ((int)(((byte)(118)))));
            this.ApproveAllPendingBtn.FlatAppearance.BorderSize = 0;
            this.ApproveAllPendingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApproveAllPendingBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApproveAllPendingBtn.ForeColor = System.Drawing.Color.White;
            this.ApproveAllPendingBtn.Location = new System.Drawing.Point(311, 57);
            this.ApproveAllPendingBtn.Name = "ApproveAllPendingBtn";
            this.ApproveAllPendingBtn.Size = new System.Drawing.Size(160, 35);
            this.ApproveAllPendingBtn.TabIndex = 39;
            this.ApproveAllPendingBtn.Text = "APPROVE ALL PENDING";
            this.ApproveAllPendingBtn.UseVisualStyleBackColor = false;
            this.ApproveAllPendingBtn.Visible = false;
            this.ApproveAllPendingBtn.Click += new System.EventHandler(this.ApproveAllPendingBtn_Click);
            // 
            // ExcludeCheckBox
            // 
            this.ExcludeCheckBox.AutoSize = true;
            this.ExcludeCheckBox.Checked = true;
            this.ExcludeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExcludeCheckBox.Location = new System.Drawing.Point(120, 110);
            this.ExcludeCheckBox.Name = "ExcludeCheckBox";
            this.ExcludeCheckBox.Size = new System.Drawing.Size(107, 17);
            this.ExcludeCheckBox.TabIndex = 38;
            this.ExcludeCheckBox.Text = "Exclude EE Data";
            this.ExcludeCheckBox.UseVisualStyleBackColor = true;
            this.ExcludeCheckBox.Visible = false;
            this.ExcludeCheckBox.CheckedChanged += new System.EventHandler(this.ExcludeCheckBox_CheckedChanged);
            // 
            // GenerateButton
            // 
            this.GenerateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.GenerateButton.FlatAppearance.BorderSize = 0;
            this.GenerateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateButton.ForeColor = System.Drawing.Color.White;
            this.GenerateButton.Image = global::MHMS.Properties.Resources.available_updates_24;
            this.GenerateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GenerateButton.Location = new System.Drawing.Point(889, 0);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(107, 35);
            this.GenerateButton.TabIndex = 14;
            this.GenerateButton.Text = "      GENERATE";
            this.GenerateButton.UseVisualStyleBackColor = false;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.panel13);
            this.panel12.Controls.Add(this.RoleDropDown);
            this.panel12.Location = new System.Drawing.Point(381, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(304, 35);
            this.panel12.TabIndex = 37;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel13.CausesValidation = false;
            this.panel13.Controls.Add(this.label4);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(109, 33);
            this.panel13.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 33);
            this.label4.TabIndex = 0;
            this.label4.Text = "Approver Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RoleDropDown
            // 
            this.RoleDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RoleDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RoleDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleDropDown.FormattingEnabled = true;
            this.RoleDropDown.Location = new System.Drawing.Point(115, 4);
            this.RoleDropDown.Name = "RoleDropDown";
            this.RoleDropDown.Size = new System.Drawing.Size(184, 25);
            this.RoleDropDown.TabIndex = 29;
            this.RoleDropDown.SelectedIndexChanged += new System.EventHandler(this.RoleDropDown_SelectedIndexChanged);
            this.RoleDropDown.TextChanged += new System.EventHandler(this.RoleDropDown_TextChanged);
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Controls.Add(this.TypeofApprovalDropdown);
            this.panel9.Location = new System.Drawing.Point(189, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(186, 35);
            this.panel9.TabIndex = 30;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel10.CausesValidation = false;
            this.panel10.Controls.Add(this.label1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(59, 33);
            this.panel10.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TypeofApprovalDropdown
            // 
            this.TypeofApprovalDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TypeofApprovalDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TypeofApprovalDropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeofApprovalDropdown.FormattingEnabled = true;
            this.TypeofApprovalDropdown.Items.AddRange(new object[] {
            "Applying",
            "Receiving"});
            this.TypeofApprovalDropdown.Location = new System.Drawing.Point(63, 4);
            this.TypeofApprovalDropdown.Name = "TypeofApprovalDropdown";
            this.TypeofApprovalDropdown.Size = new System.Drawing.Size(117, 25);
            this.TypeofApprovalDropdown.TabIndex = 29;
            this.TypeofApprovalDropdown.Text = "Applying";
            this.TypeofApprovalDropdown.SelectedIndexChanged += new System.EventHandler(this.TypeofApprovalDropdown_SelectedIndexChanged);
            // 
            // SelectAllChkBox
            // 
            this.SelectAllChkBox.AutoSize = true;
            this.SelectAllChkBox.BackColor = System.Drawing.Color.White;
            this.SelectAllChkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllChkBox.Location = new System.Drawing.Point(0, 108);
            this.SelectAllChkBox.Name = "SelectAllChkBox";
            this.SelectAllChkBox.Size = new System.Drawing.Size(85, 21);
            this.SelectAllChkBox.TabIndex = 26;
            this.SelectAllChkBox.Text = "Select All";
            this.SelectAllChkBox.UseVisualStyleBackColor = false;
            this.SelectAllChkBox.CheckedChanged += new System.EventHandler(this.SelectAllChkBox_CheckedChanged);
            // 
            // RejectButton
            // 
            this.RejectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(46)))), ((int)(((byte)(74)))));
            this.RejectButton.FlatAppearance.BorderSize = 0;
            this.RejectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RejectButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RejectButton.ForeColor = System.Drawing.Color.White;
            this.RejectButton.Location = new System.Drawing.Point(156, 57);
            this.RejectButton.Name = "RejectButton";
            this.RejectButton.Size = new System.Drawing.Size(144, 35);
            this.RejectButton.TabIndex = 12;
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
            this.AcceptButton.Location = new System.Drawing.Point(1, 57);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(144, 35);
            this.AcceptButton.TabIndex = 11;
            this.AcceptButton.Text = "ACCEPT";
            this.AcceptButton.UseVisualStyleBackColor = false;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel11);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(844, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(335, 130);
            this.panel6.TabIndex = 15;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.ApprovalCount);
            this.panel11.Location = new System.Drawing.Point(191, 95);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(143, 32);
            this.panel11.TabIndex = 39;
            // 
            // ApprovalCount
            // 
            this.ApprovalCount.AutoSize = true;
            this.ApprovalCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.ApprovalCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApprovalCount.Location = new System.Drawing.Point(49, 0);
            this.ApprovalCount.Name = "ApprovalCount";
            this.ApprovalCount.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.ApprovalCount.Size = new System.Drawing.Size(94, 30);
            this.ApprovalCount.TabIndex = 39;
            this.ApprovalCount.Text = "Approval Count:";
            this.ApprovalCount.Visible = false;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.SearchBox);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Location = new System.Drawing.Point(3, 57);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(332, 35);
            this.panel7.TabIndex = 5;
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
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel8.CausesValidation = false;
            this.panel8.Controls.Add(this.SearchButton);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(92, 33);
            this.panel8.TabIndex = 0;
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
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            this.SearchButton.MouseEnter += new System.EventHandler(this.SearchButton_MouseEnter);
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
            this.ExportButton.Location = new System.Drawing.Point(29, 7);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(110, 35);
            this.ExportButton.TabIndex = 16;
            this.ExportButton.Text = "    EXPORT";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // FrefreshDatagridTimer
            // 
            this.FrefreshDatagridTimer.Enabled = true;
            this.FrefreshDatagridTimer.Tick += new System.EventHandler(this.FrefreshDatagridTimer_Tick);
            // 
            // ApprovalDataGrid
            // 
            this.ApprovalDataGrid.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.ApprovalDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ApprovalDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ApprovalDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ApprovalDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ApprovalDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ApprovalDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.ApprovalDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApprovalDataGrid.Location = new System.Drawing.Point(0, 0);
            this.ApprovalDataGrid.Name = "ApprovalDataGrid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ApprovalDataGrid.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ApprovalDataGrid.Size = new System.Drawing.Size(1179, 295);
            this.ApprovalDataGrid.TabIndex = 25;
            this.ApprovalDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ApprovalDataGrid_CellFormatting);
            this.ApprovalDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ApprovalDataGrid_CellValueChanged);
            this.ApprovalDataGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.ApprovalDataGrid_DataBindingComplete);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel16);
            this.panel14.Controls.Add(this.label8);
            this.panel14.Controls.Add(this.label7);
            this.panel14.Controls.Add(this.label6);
            this.panel14.Controls.Add(this.label5);
            this.panel14.Controls.Add(this.label3);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(10, 440);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1179, 49);
            this.panel14.TabIndex = 26;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.ExportButton);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel16.Location = new System.Drawing.Point(1036, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(143, 49);
            this.panel16.TabIndex = 45;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(335, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(343, 15);
            this.label8.TabIndex = 44;
            this.label8.Text = "COPQ PIC → COPQ Process In-Charge → Supervisor → Manager";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(276, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 15);
            this.label7.TabIndex = 43;
            this.label7.Text = "Receiving:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 42;
            this.label6.Text = "Applying:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(60, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 15);
            this.label5.TabIndex = 41;
            this.label5.Text = "COPQ PIC → Supervisor → Manager";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 19);
            this.label3.TabIndex = 40;
            this.label3.Text = "Approval Workflow:";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.ApprovalDataGrid);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(10, 140);
            this.panel15.Name = "panel15";
            this.panel15.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel15.Size = new System.Drawing.Size(1179, 300);
            this.panel15.TabIndex = 27;
            // 
            // ApprovalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1199, 499);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.panel5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ApprovalForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "COPQ Approval Form";
            this.Load += new System.EventHandler(this.ApprovalForm_Load);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ApprovalDataGrid)).EndInit();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox CategoryDropdown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox StatusDropdown;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Button RejectButton;
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.CheckBox SelectAllChkBox;
        private System.Windows.Forms.Timer FrefreshDatagridTimer;
        private System.Windows.Forms.ComboBox TypeofApprovalDropdown;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox RoleDropDown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label TypeText;
        private System.Windows.Forms.DataGridView ApprovalDataGrid;
        private System.Windows.Forms.CheckBox ExcludeCheckBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button ApproveAllPendingBtn;
        private System.Windows.Forms.Button GeneratePreviousQIForConfirmationBtn;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label ApprovalCount;
        private System.Windows.Forms.Panel panel16;
    }
}