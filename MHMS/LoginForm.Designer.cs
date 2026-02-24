namespace MHMS
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ADID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SignInButton = new System.Windows.Forms.Button();
            this.ForgotPassword = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.ShowPasswordEyeButton = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SystemVersionText = new System.Windows.Forms.Label();
            this.HidePasswordEyeButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowPasswordEyeButton)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HidePasswordEyeButton)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::MHMS.Properties.Resources.MHMH_Login_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(420, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(390, 154);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.label1.Location = new System.Drawing.Point(544, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Login to your account";
            // 
            // ADID
            // 
            this.ADID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this.ADID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ADID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ADID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.ADID.Location = new System.Drawing.Point(434, 313);
            this.ADID.Name = "ADID";
            this.ADID.Size = new System.Drawing.Size(365, 16);
            this.ADID.TabIndex = 2;
            this.ADID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ADID_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.label2.Location = new System.Drawing.Point(421, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter your username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.label3.Location = new System.Drawing.Point(421, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enter your password";
            // 
            // SignInButton
            // 
            this.SignInButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.SignInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignInButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignInButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this.SignInButton.Location = new System.Drawing.Point(421, 427);
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.Size = new System.Drawing.Size(390, 37);
            this.SignInButton.TabIndex = 4;
            this.SignInButton.Text = "SIGN IN";
            this.SignInButton.UseVisualStyleBackColor = false;
            this.SignInButton.Click += new System.EventHandler(this.SignInButton_Click);
            this.SignInButton.MouseEnter += new System.EventHandler(this.SignInButton_MouseEnter);
            this.SignInButton.MouseLeave += new System.EventHandler(this.SignInButton_MouseLeave);
            // 
            // ForgotPassword
            // 
            this.ForgotPassword.AutoSize = true;
            this.ForgotPassword.BackColor = System.Drawing.Color.Transparent;
            this.ForgotPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForgotPassword.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ForgotPassword.Location = new System.Drawing.Point(646, 467);
            this.ForgotPassword.Name = "ForgotPassword";
            this.ForgotPassword.Size = new System.Drawing.Size(165, 16);
            this.ForgotPassword.TabIndex = 6;
            this.ForgotPassword.Text = "Forgot your password?";
            this.ForgotPassword.Click += new System.EventHandler(this.ForgotPassword_Click);
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this.Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.Password.Location = new System.Drawing.Point(434, 382);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '•';
            this.Password.Size = new System.Drawing.Size(331, 16);
            this.Password.TabIndex = 3;
            this.Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Password_KeyPress);
            // 
            // ShowPasswordEyeButton
            // 
            this.ShowPasswordEyeButton.BackColor = System.Drawing.Color.Transparent;
            this.ShowPasswordEyeButton.Image = global::MHMS.Properties.Resources.view;
            this.ShowPasswordEyeButton.Location = new System.Drawing.Point(771, 375);
            this.ShowPasswordEyeButton.Name = "ShowPasswordEyeButton";
            this.ShowPasswordEyeButton.Size = new System.Drawing.Size(28, 30);
            this.ShowPasswordEyeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ShowPasswordEyeButton.TabIndex = 8;
            this.ShowPasswordEyeButton.TabStop = false;
            this.ShowPasswordEyeButton.Click += new System.EventHandler(this.ShowPasswordEyeButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(887, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(340, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "If you have any concern, Please call BPS-Application group local 3407";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.SystemVersionText);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 592);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.panel1.Size = new System.Drawing.Size(1232, 22);
            this.panel1.TabIndex = 10;
            // 
            // SystemVersionText
            // 
            this.SystemVersionText.AutoSize = true;
            this.SystemVersionText.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SystemVersionText.Location = new System.Drawing.Point(7, 0);
            this.SystemVersionText.Name = "SystemVersionText";
            this.SystemVersionText.Size = new System.Drawing.Size(370, 13);
            this.SystemVersionText.TabIndex = 10;
            this.SystemVersionText.Text = "©️ 2022 Brother Industries (Philippines) Inc. | Developed by: BPS | Version 2.1";
            // 
            // HidePasswordEyeButton
            // 
            this.HidePasswordEyeButton.BackColor = System.Drawing.Color.Transparent;
            this.HidePasswordEyeButton.Image = global::MHMS.Properties.Resources.hidden;
            this.HidePasswordEyeButton.Location = new System.Drawing.Point(771, 375);
            this.HidePasswordEyeButton.Name = "HidePasswordEyeButton";
            this.HidePasswordEyeButton.Size = new System.Drawing.Size(28, 30);
            this.HidePasswordEyeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.HidePasswordEyeButton.TabIndex = 11;
            this.HidePasswordEyeButton.TabStop = false;
            this.HidePasswordEyeButton.Click += new System.EventHandler(this.HidePasswordEyeButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MHMS.Properties.Resources.mhlogin_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1232, 614);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.ForgotPassword);
            this.Controls.Add(this.SignInButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ADID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ShowPasswordEyeButton);
            this.Controls.Add(this.HidePasswordEyeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowPasswordEyeButton)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HidePasswordEyeButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ADID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SignInButton;
        private System.Windows.Forms.Label ForgotPassword;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.PictureBox ShowPasswordEyeButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox HidePasswordEyeButton;
        private System.Windows.Forms.Label SystemVersionText;
    }
}