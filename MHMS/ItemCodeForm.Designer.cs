namespace MHMS
{
    partial class ItemCodeForm
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
            this.ReasonOfApplicationPanel = new System.Windows.Forms.Panel();
            this.ItemCodeTextBox = new System.Windows.Forms.TextBox();
            this.AutoFillBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ApplicationNoLabel = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ItemCodeType = new System.Windows.Forms.Label();
            this.ReasonOfApplicationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReasonOfApplicationPanel
            // 
            this.ReasonOfApplicationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReasonOfApplicationPanel.Controls.Add(this.ItemCodeTextBox);
            this.ReasonOfApplicationPanel.Location = new System.Drawing.Point(17, 79);
            this.ReasonOfApplicationPanel.Name = "ReasonOfApplicationPanel";
            this.ReasonOfApplicationPanel.Size = new System.Drawing.Size(292, 35);
            this.ReasonOfApplicationPanel.TabIndex = 12;
            // 
            // ItemCodeTextBox
            // 
            this.ItemCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ItemCodeTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemCodeTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ItemCodeTextBox.Location = new System.Drawing.Point(3, 7);
            this.ItemCodeTextBox.Name = "ItemCodeTextBox";
            this.ItemCodeTextBox.Size = new System.Drawing.Size(283, 18);
            this.ItemCodeTextBox.TabIndex = 1;
            // 
            // AutoFillBtn
            // 
            this.AutoFillBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(69)))), ((int)(((byte)(180)))));
            this.AutoFillBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AutoFillBtn.FlatAppearance.BorderSize = 0;
            this.AutoFillBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AutoFillBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoFillBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.AutoFillBtn.Location = new System.Drawing.Point(17, 120);
            this.AutoFillBtn.Name = "AutoFillBtn";
            this.AutoFillBtn.Size = new System.Drawing.Size(292, 38);
            this.AutoFillBtn.TabIndex = 22;
            this.AutoFillBtn.Text = "Auto Fill";
            this.AutoFillBtn.UseVisualStyleBackColor = false;
            this.AutoFillBtn.Click += new System.EventHandler(this.AutoFillBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 10);
            this.panel1.TabIndex = 23;
            // 
            // ApplicationNoLabel
            // 
            this.ApplicationNoLabel.AutoSize = true;
            this.ApplicationNoLabel.Location = new System.Drawing.Point(3, 13);
            this.ApplicationNoLabel.Name = "ApplicationNoLabel";
            this.ApplicationNoLabel.Size = new System.Drawing.Size(88, 13);
            this.ApplicationNoLabel.TabIndex = 24;
            this.ApplicationNoLabel.Text = "Application No. 1";
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(46)))), ((int)(((byte)(74)))));
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.ForeColor = System.Drawing.Color.White;
            this.CloseBtn.Location = new System.Drawing.Point(275, 13);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(47, 23);
            this.CloseBtn.TabIndex = 25;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // ItemCodeType
            // 
            this.ItemCodeType.AutoSize = true;
            this.ItemCodeType.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemCodeType.Location = new System.Drawing.Point(15, 63);
            this.ItemCodeType.Name = "ItemCodeType";
            this.ItemCodeType.Size = new System.Drawing.Size(65, 15);
            this.ItemCodeType.TabIndex = 26;
            this.ItemCodeType.Text = "Item Code:";
            // 
            // ItemCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(326, 184);
            this.Controls.Add(this.ItemCodeType);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.ApplicationNoLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AutoFillBtn);
            this.Controls.Add(this.ReasonOfApplicationPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "ItemCodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Code";
            this.Load += new System.EventHandler(this.ItemCodeForm_Load);
            this.ReasonOfApplicationPanel.ResumeLayout(false);
            this.ReasonOfApplicationPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ReasonOfApplicationPanel;
        private System.Windows.Forms.TextBox ItemCodeTextBox;
        private System.Windows.Forms.Button AutoFillBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ApplicationNoLabel;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label ItemCodeType;
    }
}