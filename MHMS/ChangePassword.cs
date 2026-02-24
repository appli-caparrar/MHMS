using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class ChangePassword : Form
    {
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        public ChangePassword()
        {
            InitializeComponent();
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Place holder for email
        private void Email_Enter(object sender, EventArgs e)
        {
            if (Email.Text == "Email address")
            {
                Email.Text = "";
                Email.ForeColor = Color.FromArgb(26, 44, 47);
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Place holder for code
        private void CodeTxtBox_Enter(object sender, EventArgs e)
        {
            if (CodeTxtBox.Text == "Enter your code")
            {
                CodeTxtBox.Text = "";
                CodeTxtBox.ForeColor = Color.FromArgb(26, 44, 47);
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void CodeTxtBox_Leave(object sender, EventArgs e)
        {
            if (CodeTxtBox.Text == "")
            {
                CodeTxtBox.Text = "Enter your code";
                CodeTxtBox.ForeColor = Color.Gray;
            }
        } // end ------->

        //====================================================================================================================>>>>>>>>>>>>

        private void SendEmail()
        {
            //Email header.
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            builder.Append("<h2>Manhour Management System (MHMS) - Password Reset Code</h2>");
            builder.Append("<br>" + DateTime.Now);
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("Good day!");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("You have requested to reset your MHMS account password.");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("Please use this code to reset your password.");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("Here is your code: " + "<strong><font color=blue>" + RandomNumLabel.Text + "</font></strong>");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("If you have not requested resetting your password, please ignore this email.");
            builder.Append("<br>");
            builder.Append("<br><b><font color=red>This is automatic generated email, Do not reply!</b><br></font>");
            builder.Append("<br>Thank you!").AppendLine();
            innerString = builder.ToString();

            EmailNotif(); // ---> Call out the email notification function
        }

        //====================================================================================================================>>>>>>>>>>>>

        string emailto; // ---> Declared string.
        string innerString; // ---> Declared string.

        private void EmailNotif()
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool isValid = regex.IsMatch(Email.Text.Trim());

            emailto = Email.Text;//textbox to for the email.

            if (emailto == "")
            {
                MessageBox.Show("Please type your email address!");
            }
            else if (!isValid)
            {
                MessageBox.Show("Invalid Email.");
            }
            else
            {
                //Email structure.
                MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", emailto);
                // mail.To.Add(new MailAddress("BIPHBPS_Appli@bicusa.onmicrosoft.com"));
                //mail.To.Add(new MailAddress(Email.Text));
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "10.113.10.1";
                mail.Subject = "[BIPH_MHMS] - NOTIFICATION";

                mail.Body = innerString;
                mail.IsBodyHtml = true;
                client.Send(mail);

                MessageBox.Show("We have e-mailed your password reset code. Please check your email!");

                EmailPanel.Visible = false;
                CodePanel.Visible = true;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Generate Code
        private void GenerateCode()
        {
            
            int maxNum = 999999;
            int minNum = 100000;
            Random random = new Random();
            int randomNum = random.Next(minNum, maxNum);
            RandomNumLabel.Text = randomNum.ToString();
            CodeLabel.Text = randomNum.ToString();
            
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            if (Email.Text == "")
            {
                MessageBox.Show("Please type your email address!", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (Email.Text != "Email address")
                {
                    SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand SelectUserEmail = new SqlCommand("SP_SelectUserEmail", con);
                    SelectUserEmail.CommandType = CommandType.StoredProcedure;
                    SelectUserEmail.Parameters.AddWithValue("@Email", Email.Text);
                    SqlDataAdapter da = new SqlDataAdapter(SelectUserEmail);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count >= 1)
                    {
                        SqlDataReader reader = SelectUserEmail.ExecuteReader();

                        if (reader.Read())
                        {
                            GenerateCode();
                            SendEmail();
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Sorry can't find your email address, Please check your email and try again.", "Invalid Email!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        public static string UserEmail = "";
        private void ChangePassword_Load(object sender, EventArgs e)
        {
            CodePanel.SendToBack();
            EmailPanel.BringToFront();
            ResetPasswordPanel.SendToBack();
            ResetPasswordPanel.Visible = false;

            Email.Text = LoginForm.EmailAddress;
            Email.ForeColor = Color.FromArgb(72, 91, 232);

            Email.Select();

            //EmailPanel.Location = new Point(
            //this.ClientSize.Width / 2 - EmailPanel.Size.Width / 2,
            //this.ClientSize.Height / 2 - EmailPanel.Size.Height / 2);
            //EmailPanel.Anchor = AnchorStyles.None;
        }

        //====================================================================================================================>>>>>>>>>>>>
        private void SubmitCodeButton_Click(object sender, EventArgs e)
        {
            if (CodeTxtBox.Text == "")
            {
                MessageBox.Show("Please type you password reset code.");
            }
            else if (CodeTxtBox.Text != CodeLabel.Text)
            {
                MessageBox.Show("Invalid code. Please check your code again.");
            }
            else
            {
                MessageBox.Show("Your code is verified, now you are able to reset your password", "Success!");
                EmailPanel.SendToBack();
                CodePanel.SendToBack();
                ResetPasswordPanel.BringToFront();
                ResetPasswordPanel.Visible = true;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void HidePasswordButton_Click(object sender, EventArgs e)
        {

            if (Password.PasswordChar == '•')
            {
                Password.PasswordChar = '\0';
            }
            else
            {
                Password.PasswordChar = '•';
            }

            ShowPasswordButton.BringToFront();
            HidePasswordButton.SendToBack();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ShowPasswordButton_Click(object sender, EventArgs e)
        {
            if (Password.PasswordChar == '•')
            {
                Password.PasswordChar = '\0';
            }
            else
            {
                Password.PasswordChar = '•';
            }

            ShowPasswordButton.SendToBack();
            HidePasswordButton.BringToFront();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void HideConfirmPassword_Click(object sender, EventArgs e)
        {
            if (ConfirmPassword.PasswordChar == '•')
            {
                ConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                ConfirmPassword.PasswordChar = '•';
            }

            ShowConfirmPassword.BringToFront();
            HideConfirmPassword.SendToBack();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ShowConfirmPassword_Click(object sender, EventArgs e)
        {
            if (ConfirmPassword.PasswordChar == '•')
            {
                ConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                ConfirmPassword.PasswordChar = '•';
            }

            ShowConfirmPassword.SendToBack();
            HideConfirmPassword.BringToFront();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ResetPassword()
        {
            if (Password.Text == "" && ConfirmPassword.Text == "")
            {
                MessageBox.Show("Please fill in all required fields", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Password.Text == "")
            {
                MessageBox.Show("Please type your new password", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ConfirmPassword.Text == "")
            {
                MessageBox.Show("Please confirm your password", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Password.Text != ConfirmPassword.Text)
            {
                MessageBox.Show("Password does not match, Please check your password again!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // -> SQL query to reset password
                SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand ChangePassword = new SqlCommand("SP_ChangePassword", con);
                ChangePassword.CommandType = CommandType.StoredProcedure;
                ChangePassword.Parameters.AddWithValue("@Password", Password.Text);
                ChangePassword.Parameters.AddWithValue("@Email", Email.Text);
                ChangePassword.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Your password is successfuly changed! Please login and use your new password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Application.Restart();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetPassword();
            //this.Close();
            //LoginForm loginForm = new LoginForm();
            //loginForm.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void BackToLoginForm_Click(object sender, EventArgs e)
        {
            Hide();
            Close();
            LoginForm login = new LoginForm();
            login.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>
    }
}
