namespace MHMS.Forms
{
    partial class MPRSettingForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btn1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label28 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn1ToolStripMenuItem,
            this.btn2ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1199, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btn1ToolStripMenuItem
            // 
            this.btn1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sTToolStripMenuItem,
            this.workCenterToolStripMenuItem,
            this.openMHToolStripMenuItem});
            this.btn1ToolStripMenuItem.Name = "btn1ToolStripMenuItem";
            this.btn1ToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.btn1ToolStripMenuItem.Text = "Application Form";
            this.btn1ToolStripMenuItem.Click += new System.EventHandler(this.btn1ToolStripMenuItem_Click);
            // 
            // sTToolStripMenuItem
            // 
            this.sTToolStripMenuItem.Name = "sTToolStripMenuItem";
            this.sTToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.sTToolStripMenuItem.Text = "ST";
            // 
            // workCenterToolStripMenuItem
            // 
            this.workCenterToolStripMenuItem.Name = "workCenterToolStripMenuItem";
            this.workCenterToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.workCenterToolStripMenuItem.Text = "Work Center";
            // 
            // openMHToolStripMenuItem
            // 
            this.openMHToolStripMenuItem.Name = "openMHToolStripMenuItem";
            this.openMHToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openMHToolStripMenuItem.Text = "Open MH";
            // 
            // btn2ToolStripMenuItem
            // 
            this.btn2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newApplicationToolStripMenuItem,
            this.resubmitApplicationToolStripMenuItem});
            this.btn2ToolStripMenuItem.Name = "btn2ToolStripMenuItem";
            this.btn2ToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.btn2ToolStripMenuItem.Text = "Application Type";
            // 
            // newApplicationToolStripMenuItem
            // 
            this.newApplicationToolStripMenuItem.Name = "newApplicationToolStripMenuItem";
            this.newApplicationToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.newApplicationToolStripMenuItem.Text = "New Application";
            // 
            // resubmitApplicationToolStripMenuItem
            // 
            this.resubmitApplicationToolStripMenuItem.Name = "resubmitApplicationToolStripMenuItem";
            this.resubmitApplicationToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.resubmitApplicationToolStripMenuItem.Text = "ResubmitApplication";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.SystemColors.Control;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label28.Location = new System.Drawing.Point(0, 24);
            this.label28.Name = "label28";
            this.label28.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label28.Size = new System.Drawing.Size(1199, 465);
            this.label28.TabIndex = 40;
            this.label28.Text = "<ON-GOING DEVELOPMENT>";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MPRSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 489);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MPRSettingForm";
            this.Text = "MPR Setting Form";
            this.Load += new System.EventHandler(this.MPRSettingForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btn1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btn2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitApplicationToolStripMenuItem;
        private System.Windows.Forms.Label label28;
    }
}