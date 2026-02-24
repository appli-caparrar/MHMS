namespace MHMS
{
    partial class FactoryEfficiencyGraphForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactoryEfficiencyGraphForm));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.CumulativeManhourGraphBtn = new System.Windows.Forms.Button();
            this.FEIdealVarianceRateGraphBtn = new System.Windows.Forms.Button();
            this.FEMonthlyResultGraphBtn = new System.Windows.Forms.Button();
            this.ClearInputsBtn = new System.Windows.Forms.Button();
            this.GraphName = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.ClearInputsBtn);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(655, 48);
            this.TopPanel.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "FACTORY EFFICIENCY GRAPH";
            // 
            // CumulativeManhourGraphBtn
            // 
            this.CumulativeManhourGraphBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CumulativeManhourGraphBtn.BackgroundImage = global::MHMS.Properties.Resources.pie_chart;
            this.CumulativeManhourGraphBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CumulativeManhourGraphBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CumulativeManhourGraphBtn.FlatAppearance.BorderSize = 0;
            this.CumulativeManhourGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CumulativeManhourGraphBtn.Location = new System.Drawing.Point(453, 122);
            this.CumulativeManhourGraphBtn.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.CumulativeManhourGraphBtn.Name = "CumulativeManhourGraphBtn";
            this.CumulativeManhourGraphBtn.Size = new System.Drawing.Size(162, 126);
            this.CumulativeManhourGraphBtn.TabIndex = 32;
            this.CumulativeManhourGraphBtn.UseVisualStyleBackColor = false;
            this.CumulativeManhourGraphBtn.Click += new System.EventHandler(this.CumulativeManhourGraphBtn_Click);
            this.CumulativeManhourGraphBtn.MouseEnter += new System.EventHandler(this.CumulativeManhourGraphBtn_MouseEnter);
            this.CumulativeManhourGraphBtn.MouseLeave += new System.EventHandler(this.CumulativeManhourGraphBtn_MouseLeave);
            // 
            // FEIdealVarianceRateGraphBtn
            // 
            this.FEIdealVarianceRateGraphBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FEIdealVarianceRateGraphBtn.BackgroundImage = global::MHMS.Properties.Resources.stacked_bar;
            this.FEIdealVarianceRateGraphBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FEIdealVarianceRateGraphBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FEIdealVarianceRateGraphBtn.FlatAppearance.BorderSize = 0;
            this.FEIdealVarianceRateGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FEIdealVarianceRateGraphBtn.Location = new System.Drawing.Point(245, 122);
            this.FEIdealVarianceRateGraphBtn.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.FEIdealVarianceRateGraphBtn.Name = "FEIdealVarianceRateGraphBtn";
            this.FEIdealVarianceRateGraphBtn.Size = new System.Drawing.Size(162, 126);
            this.FEIdealVarianceRateGraphBtn.TabIndex = 31;
            this.FEIdealVarianceRateGraphBtn.UseVisualStyleBackColor = false;
            this.FEIdealVarianceRateGraphBtn.Click += new System.EventHandler(this.FEIdealVarianceRateGraphBtn_Click);
            this.FEIdealVarianceRateGraphBtn.MouseEnter += new System.EventHandler(this.FEIdealVarianceRateGraphBtn_MouseEnter);
            this.FEIdealVarianceRateGraphBtn.MouseLeave += new System.EventHandler(this.FEIdealVarianceRateGraphBtn_MouseLeave);
            // 
            // FEMonthlyResultGraphBtn
            // 
            this.FEMonthlyResultGraphBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FEMonthlyResultGraphBtn.BackgroundImage = global::MHMS.Properties.Resources.combination_chart;
            this.FEMonthlyResultGraphBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FEMonthlyResultGraphBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FEMonthlyResultGraphBtn.FlatAppearance.BorderSize = 0;
            this.FEMonthlyResultGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FEMonthlyResultGraphBtn.Location = new System.Drawing.Point(38, 122);
            this.FEMonthlyResultGraphBtn.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.FEMonthlyResultGraphBtn.Name = "FEMonthlyResultGraphBtn";
            this.FEMonthlyResultGraphBtn.Size = new System.Drawing.Size(162, 126);
            this.FEMonthlyResultGraphBtn.TabIndex = 29;
            this.FEMonthlyResultGraphBtn.UseVisualStyleBackColor = false;
            this.FEMonthlyResultGraphBtn.Click += new System.EventHandler(this.FEMonthlyResultGraphBtn_Click);
            this.FEMonthlyResultGraphBtn.MouseEnter += new System.EventHandler(this.FEMonthlyResultGraphBtn_MouseEnter);
            this.FEMonthlyResultGraphBtn.MouseLeave += new System.EventHandler(this.FEMonthlyResultGraphBtn_MouseLeave);
            // 
            // ClearInputsBtn
            // 
            this.ClearInputsBtn.BackColor = System.Drawing.Color.Transparent;
            this.ClearInputsBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ClearInputsBtn.BackgroundImage")));
            this.ClearInputsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClearInputsBtn.FlatAppearance.BorderSize = 0;
            this.ClearInputsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearInputsBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearInputsBtn.ForeColor = System.Drawing.Color.White;
            this.ClearInputsBtn.Location = new System.Drawing.Point(1063, 4);
            this.ClearInputsBtn.Name = "ClearInputsBtn";
            this.ClearInputsBtn.Size = new System.Drawing.Size(38, 38);
            this.ClearInputsBtn.TabIndex = 13;
            this.ClearInputsBtn.UseVisualStyleBackColor = false;
            // 
            // GraphName
            // 
            this.GraphName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GraphName.AutoSize = true;
            this.GraphName.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GraphName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.GraphName.Location = new System.Drawing.Point(33, 71);
            this.GraphName.Name = "GraphName";
            this.GraphName.Size = new System.Drawing.Size(267, 25);
            this.GraphName.TabIndex = 33;
            this.GraphName.Text = "FACTORY EFFICIENCY GRAPH";
            this.GraphName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FactoryEfficiencyGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(655, 292);
            this.Controls.Add(this.GraphName);
            this.Controls.Add(this.CumulativeManhourGraphBtn);
            this.Controls.Add(this.FEIdealVarianceRateGraphBtn);
            this.Controls.Add(this.FEMonthlyResultGraphBtn);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FactoryEfficiencyGraphForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FactoryEfficiencyGraphForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button ClearInputsBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FEMonthlyResultGraphBtn;
        private System.Windows.Forms.Button FEIdealVarianceRateGraphBtn;
        private System.Windows.Forms.Button CumulativeManhourGraphBtn;
        private System.Windows.Forms.Label GraphName;
    }
}