namespace MHMS
{
    partial class AddLeadTime
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LossFactor = new System.Windows.Forms.ComboBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.rowCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Leadtime = new System.Windows.Forms.TextBox();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TopPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(441, 48);
            this.TopPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Leadtime";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.LossFactor);
            this.panel3.Location = new System.Drawing.Point(11, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(417, 36);
            this.panel3.TabIndex = 30;
            // 
            // LossFactor
            // 
            this.LossFactor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.LossFactor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.LossFactor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LossFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LossFactor.FormattingEnabled = true;
            this.LossFactor.Items.AddRange(new object[] {
            "3-3-2 Printer section Issue",
            "3-3-3 Printer 1 Section Issue",
            "3-3-4 Printer 2 Section Issue",
            "3-4-2 Ink Head Issue (PH) ",
            "3-5-2 Cartridge Issue (PH)",
            "3-6-2 In-house mold issue (PH)",
            "3-7 In-house PCBA issue",
            "6-1 Supplier mold issue",
            "6-2 Supplier PCBA issue",
            "6-3 SQC issue",
            "7-1-1 Equipment issue",
            "7-1-2 Equipment issue (Maintenance)",
            "7-2-2 Production Control issue (PH)",
            "7-3 IT issue",
            "7-4 QA issue",
            "7-5 GA issue",
            "7-6 IQC issue",
            "7-7 LC Issue (倉庫)",
            "7-8 Label Printing (PE) Issue",
            "7-9 Parts Delivery Issue ",
            "8:Development (DE) Issue",
            "8-1: Line stop due to design issues (DE Section)",
            "8-2: Line stop due to design issues (BIL)"});
            this.LossFactor.Location = new System.Drawing.Point(3, 5);
            this.LossFactor.Name = "LossFactor";
            this.LossFactor.Size = new System.Drawing.Size(409, 24);
            this.LossFactor.TabIndex = 51;
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.SaveButton.FlatAppearance.BorderSize = 0;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SaveButton.Location = new System.Drawing.Point(351, 187);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(77, 32);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // rowCount
            // 
            this.rowCount.AutoSize = true;
            this.rowCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rowCount.Location = new System.Drawing.Point(11, 32);
            this.rowCount.Name = "rowCount";
            this.rowCount.Size = new System.Drawing.Size(124, 15);
            this.rowCount.TabIndex = 47;
            this.rowCount.Text = "Loss Factor (Required)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "Input Leadtime (Required)";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Leadtime);
            this.panel1.Location = new System.Drawing.Point(11, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 36);
            this.panel1.TabIndex = 48;
            // 
            // Leadtime
            // 
            this.Leadtime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Leadtime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Leadtime.Location = new System.Drawing.Point(3, 8);
            this.Leadtime.Name = "Leadtime";
            this.Leadtime.Size = new System.Drawing.Size(409, 18);
            this.Leadtime.TabIndex = 1;
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.Silver;
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CloseBtn.Location = new System.Drawing.Point(264, 187);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(77, 32);
            this.CloseBtn.TabIndex = 50;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.CloseBtn);
            this.panel2.Controls.Add(this.SaveButton);
            this.panel2.Controls.Add(this.rowCount);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(441, 232);
            this.panel2.TabIndex = 51;
            // 
            // AddLeadTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(441, 280);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddLeadTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddLossFactorAndLeadTime_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label rowCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Leadtime;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.ComboBox LossFactor;
        private System.Windows.Forms.Panel panel2;
    }
}