namespace MHMS
{
    partial class OpenGraphForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenGraphForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Top5DefectRecurrenceBtn = new System.Windows.Forms.Button();
            this.Top5DisposalCostBtn = new System.Windows.Forms.Button();
            this.COPQDisposalPartsCostBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.PictureBox();
            this.SelectGraphLabel = new System.Windows.Forms.Label();
            this.GraphName = new System.Windows.Forms.Label();
            this.SectionDropdownList = new System.Windows.Forms.ComboBox();
            this.SectionPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).BeginInit();
            this.SectionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "COPQ Tableau Graph";
            // 
            // Top5DefectRecurrenceBtn
            // 
            this.Top5DefectRecurrenceBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.Top5DefectRecurrenceBtn.BackgroundImage = global::MHMS.Properties.Resources.graph__1_;
            this.Top5DefectRecurrenceBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Top5DefectRecurrenceBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Top5DefectRecurrenceBtn.FlatAppearance.BorderSize = 0;
            this.Top5DefectRecurrenceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Top5DefectRecurrenceBtn.Location = new System.Drawing.Point(390, 101);
            this.Top5DefectRecurrenceBtn.Name = "Top5DefectRecurrenceBtn";
            this.Top5DefectRecurrenceBtn.Size = new System.Drawing.Size(137, 112);
            this.Top5DefectRecurrenceBtn.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Top5DefectRecurrenceBtn, "Top 5 Monthly Defect Recurrence");
            this.Top5DefectRecurrenceBtn.UseVisualStyleBackColor = false;
            this.Top5DefectRecurrenceBtn.Click += new System.EventHandler(this.Top5DefectRecurrenceBtn_Click);
            this.Top5DefectRecurrenceBtn.MouseEnter += new System.EventHandler(this.Top5DefectRecurrenceBtn_MouseEnter);
            this.Top5DefectRecurrenceBtn.MouseLeave += new System.EventHandler(this.Top5DefectRecurrenceBtn_MouseLeave);
            // 
            // Top5DisposalCostBtn
            // 
            this.Top5DisposalCostBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.Top5DisposalCostBtn.BackgroundImage = global::MHMS.Properties.Resources.bar_chart;
            this.Top5DisposalCostBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Top5DisposalCostBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Top5DisposalCostBtn.FlatAppearance.BorderSize = 0;
            this.Top5DisposalCostBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Top5DisposalCostBtn.Location = new System.Drawing.Point(213, 101);
            this.Top5DisposalCostBtn.Name = "Top5DisposalCostBtn";
            this.Top5DisposalCostBtn.Size = new System.Drawing.Size(137, 112);
            this.Top5DisposalCostBtn.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Top5DisposalCostBtn, "Top 5 Monthly Disposal Cost");
            this.Top5DisposalCostBtn.UseVisualStyleBackColor = false;
            this.Top5DisposalCostBtn.Click += new System.EventHandler(this.Top5DisposalCostBtn_Click);
            this.Top5DisposalCostBtn.MouseEnter += new System.EventHandler(this.Top5DisposalCostBtn_MouseEnter);
            this.Top5DisposalCostBtn.MouseLeave += new System.EventHandler(this.Top5DisposalCostBtn_MouseLeave);
            // 
            // COPQDisposalPartsCostBtn
            // 
            this.COPQDisposalPartsCostBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.COPQDisposalPartsCostBtn.BackgroundImage = global::MHMS.Properties.Resources.analysis;
            this.COPQDisposalPartsCostBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.COPQDisposalPartsCostBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.COPQDisposalPartsCostBtn.FlatAppearance.BorderSize = 0;
            this.COPQDisposalPartsCostBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.COPQDisposalPartsCostBtn.Location = new System.Drawing.Point(38, 101);
            this.COPQDisposalPartsCostBtn.Name = "COPQDisposalPartsCostBtn";
            this.COPQDisposalPartsCostBtn.Size = new System.Drawing.Size(137, 112);
            this.COPQDisposalPartsCostBtn.TabIndex = 0;
            this.toolTip1.SetToolTip(this.COPQDisposalPartsCostBtn, "COPQ Parts Disposal Cost (Cumulative)");
            this.COPQDisposalPartsCostBtn.UseVisualStyleBackColor = false;
            this.COPQDisposalPartsCostBtn.Click += new System.EventHandler(this.COPQDisposalPartsCostBtn_Click);
            this.COPQDisposalPartsCostBtn.MouseEnter += new System.EventHandler(this.COPQDisposalPartsCostBtn_MouseEnter);
            this.COPQDisposalPartsCostBtn.MouseLeave += new System.EventHandler(this.COPQDisposalPartsCostBtn_MouseLeave);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Image = global::MHMS.Properties.Resources.delete;
            this.CloseBtn.Location = new System.Drawing.Point(457, 0);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(32, 32);
            this.CloseBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CloseBtn.TabIndex = 8;
            this.CloseBtn.TabStop = false;
            this.toolTip1.SetToolTip(this.CloseBtn, "Close");
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // SelectGraphLabel
            // 
            this.SelectGraphLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectGraphLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectGraphLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(59)))));
            this.SelectGraphLabel.Location = new System.Drawing.Point(0, 0);
            this.SelectGraphLabel.Name = "SelectGraphLabel";
            this.SelectGraphLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.SelectGraphLabel.Size = new System.Drawing.Size(565, 43);
            this.SelectGraphLabel.TabIndex = 3;
            this.SelectGraphLabel.Text = "PLEASE SELECT A GRAPH";
            this.SelectGraphLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GraphName
            // 
            this.GraphName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GraphName.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GraphName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(59)))));
            this.GraphName.Location = new System.Drawing.Point(0, 277);
            this.GraphName.Name = "GraphName";
            this.GraphName.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.GraphName.Size = new System.Drawing.Size(565, 43);
            this.GraphName.TabIndex = 4;
            this.GraphName.Text = "SELECT GRAPH";
            this.GraphName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GraphName.Visible = false;
            // 
            // SectionDropdownList
            // 
            this.SectionDropdownList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionDropdownList.FormattingEnabled = true;
            this.SectionDropdownList.Items.AddRange(new object[] {
            "Ink Cartridge",
            "Ink Head",
            "Printer",
            "P-Touch",
            "Tape Cassette"});
            this.SectionDropdownList.Location = new System.Drawing.Point(29, 52);
            this.SectionDropdownList.Name = "SectionDropdownList";
            this.SectionDropdownList.Size = new System.Drawing.Size(348, 28);
            this.SectionDropdownList.TabIndex = 5;
            this.SectionDropdownList.SelectedIndexChanged += new System.EventHandler(this.SectionDropdownList_SelectedIndexChanged);
            // 
            // SectionPanel
            // 
            this.SectionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(59)))));
            this.SectionPanel.Controls.Add(this.CloseBtn);
            this.SectionPanel.Controls.Add(this.button1);
            this.SectionPanel.Controls.Add(this.label1);
            this.SectionPanel.Controls.Add(this.SectionDropdownList);
            this.SectionPanel.Location = new System.Drawing.Point(38, 101);
            this.SectionPanel.Name = "SectionPanel";
            this.SectionPanel.Size = new System.Drawing.Size(489, 112);
            this.SectionPanel.TabIndex = 6;
            this.SectionPanel.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(111)))), ((int)(((byte)(238)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Window;
            this.button1.Location = new System.Drawing.Point(383, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "OPEN";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(25, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Please Select Section:";
            // 
            // OpenGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(565, 320);
            this.Controls.Add(this.SectionPanel);
            this.Controls.Add(this.GraphName);
            this.Controls.Add(this.SelectGraphLabel);
            this.Controls.Add(this.Top5DefectRecurrenceBtn);
            this.Controls.Add(this.Top5DisposalCostBtn);
            this.Controls.Add(this.COPQDisposalPartsCostBtn);
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OpenGraphForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.OpenGraphForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).EndInit();
            this.SectionPanel.ResumeLayout(false);
            this.SectionPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button COPQDisposalPartsCostBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Top5DisposalCostBtn;
        private System.Windows.Forms.Button Top5DefectRecurrenceBtn;
        private System.Windows.Forms.Label SelectGraphLabel;
        private System.Windows.Forms.Label GraphName;
        private System.Windows.Forms.ComboBox SectionDropdownList;
        private System.Windows.Forms.Panel SectionPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox CloseBtn;
    }
}