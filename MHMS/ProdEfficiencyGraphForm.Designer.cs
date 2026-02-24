namespace MHMS
{
    partial class ProdEfficiencyGraphForm
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
            this.TotalEffGraphBtn = new System.Windows.Forms.Button();
            this.DirectEffGraphBtn = new System.Windows.Forms.Button();
            this.SemiDirectGraphBtn = new System.Windows.Forms.Button();
            this.TotalLossRateGraphBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TotalEffGraphBtn
            // 
            this.TotalEffGraphBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(24)))), ((int)(((byte)(139)))));
            this.TotalEffGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TotalEffGraphBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalEffGraphBtn.ForeColor = System.Drawing.Color.White;
            this.TotalEffGraphBtn.Location = new System.Drawing.Point(32, 41);
            this.TotalEffGraphBtn.Name = "TotalEffGraphBtn";
            this.TotalEffGraphBtn.Size = new System.Drawing.Size(227, 48);
            this.TotalEffGraphBtn.TabIndex = 0;
            this.TotalEffGraphBtn.Text = "Total Efficiency Graph";
            this.TotalEffGraphBtn.UseVisualStyleBackColor = false;
            this.TotalEffGraphBtn.Click += new System.EventHandler(this.TotalEffGraphBtn_Click);
            // 
            // DirectEffGraphBtn
            // 
            this.DirectEffGraphBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(24)))), ((int)(((byte)(139)))));
            this.DirectEffGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DirectEffGraphBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectEffGraphBtn.ForeColor = System.Drawing.Color.White;
            this.DirectEffGraphBtn.Location = new System.Drawing.Point(265, 41);
            this.DirectEffGraphBtn.Name = "DirectEffGraphBtn";
            this.DirectEffGraphBtn.Size = new System.Drawing.Size(227, 48);
            this.DirectEffGraphBtn.TabIndex = 0;
            this.DirectEffGraphBtn.Text = "Direct Efficiency Graph";
            this.DirectEffGraphBtn.UseVisualStyleBackColor = false;
            this.DirectEffGraphBtn.Click += new System.EventHandler(this.DirectEffGraphBtn_Click);
            // 
            // SemiDirectGraphBtn
            // 
            this.SemiDirectGraphBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(24)))), ((int)(((byte)(139)))));
            this.SemiDirectGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SemiDirectGraphBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SemiDirectGraphBtn.ForeColor = System.Drawing.Color.White;
            this.SemiDirectGraphBtn.Location = new System.Drawing.Point(32, 95);
            this.SemiDirectGraphBtn.Name = "SemiDirectGraphBtn";
            this.SemiDirectGraphBtn.Size = new System.Drawing.Size(227, 48);
            this.SemiDirectGraphBtn.TabIndex = 0;
            this.SemiDirectGraphBtn.Text = "Semi-Direct Rate Graph";
            this.SemiDirectGraphBtn.UseVisualStyleBackColor = false;
            this.SemiDirectGraphBtn.Click += new System.EventHandler(this.SemiDirectGraphBtn_Click);
            // 
            // TotalLossRateGraphBtn
            // 
            this.TotalLossRateGraphBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(24)))), ((int)(((byte)(139)))));
            this.TotalLossRateGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TotalLossRateGraphBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLossRateGraphBtn.ForeColor = System.Drawing.Color.White;
            this.TotalLossRateGraphBtn.Location = new System.Drawing.Point(265, 95);
            this.TotalLossRateGraphBtn.Name = "TotalLossRateGraphBtn";
            this.TotalLossRateGraphBtn.Size = new System.Drawing.Size(227, 48);
            this.TotalLossRateGraphBtn.TabIndex = 0;
            this.TotalLossRateGraphBtn.Text = "Total Loss Rate Graph";
            this.TotalLossRateGraphBtn.UseVisualStyleBackColor = false;
            this.TotalLossRateGraphBtn.Click += new System.EventHandler(this.TotalLossRateGraphBtn_Click);
            // 
            // ProdEfficiencyGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(528, 184);
            this.Controls.Add(this.TotalLossRateGraphBtn);
            this.Controls.Add(this.DirectEffGraphBtn);
            this.Controls.Add(this.SemiDirectGraphBtn);
            this.Controls.Add(this.TotalEffGraphBtn);
            this.MaximizeBox = false;
            this.Name = "ProdEfficiencyGraphForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TotalEffGraphBtn;
        private System.Windows.Forms.Button DirectEffGraphBtn;
        private System.Windows.Forms.Button SemiDirectGraphBtn;
        private System.Windows.Forms.Button TotalLossRateGraphBtn;
    }
}