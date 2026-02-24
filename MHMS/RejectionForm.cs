using ExcelDataReader;
using MHMS.Connection;
using MHMS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class RejectionForm : Form
    {
        // Connection string
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        ////Table collection
        //DataTableCollection tableCollection;

        public RejectionForm()
        {
            InitializeComponent();
        }

        //=======================================================================================================================>>>>>>>>>>>>

        private void RejectionForm_Load(object sender, EventArgs e)
        {
            LineStopDetailTextBox.Text = ApprovalForm.LineStopDetail;
            LineStopDetailTextBox.ReadOnly = true;

            LoadResponsibleSection();

            SectionDropdown.Text = "Section (Optional)";
        }

       
        //Load Section in combobox
        public void LoadSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand LoadSection = new SqlCommand("SP_LoadSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            SectionDropdown.ValueMember = "Section";
        }// <---- end

        //=======================================================================================================================>>>>>>>>>>>>>>>>>>

        private void SectionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReasonTextBox.Focus();
        }

        //=======================================================================================================================>>>>>>>>>>>>

        //Load Section in combobox
        public void LoadResponsibleSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand LoadSection = new SqlCommand("SP_LoadResponsibleSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            SectionDropdown.ValueMember = "Section";
        }// <---- end

        //=======================================================================================================================>>>>>>>>>>>>

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            if (ReasonTextBox.Text == "")
            {
                MessageBox.Show("Please type the reason of rejection!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                UpdateApprovalStatus();

                //ApprovalForm.ContinueButtonIsClicked = true;

                this.Close();
            }
            
        }

        //=======================================================================================================================>>>>>>>>>>>>
        
        private void UpdateReasonOfRejection()
        {

            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //SqlCommand UpdateReasonOfRejection = new SqlCommand("SP_UpdateReasonOfRejection", con);
            //UpdateReasonOfRejection.CommandType = CommandType.StoredProcedure;
            //UpdateReasonOfRejection.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            //UpdateReasonOfRejection.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            ////UpdateReasonOfRejection.Parameters.AddWithValue("@ResponsibleSection", SectionDropdown.Text);

            if (SectionDropdown.Text == "Section (Optional)" || SectionDropdown.Text == "")
            {
                SqlCommand UpdateReasonOfRejection = new SqlCommand("SP_UpdateReasonOfRejection", con);
                UpdateReasonOfRejection.CommandType = CommandType.StoredProcedure;
                UpdateReasonOfRejection.Parameters.AddWithValue("@DistinctionCode", ApprovalForm.DistinctionCode);
                //UpdateReasonOfRejection.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
                //UpdateReasonOfRejection.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
                //UpdateReasonOfRejection.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
                UpdateReasonOfRejection.Parameters.AddWithValue("@ReasonOfRejection", ReasonTextBox.Text);
                UpdateReasonOfRejection.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                SqlCommand UpdateReasonOfRejection = new SqlCommand("SP_UpdateReasonOfRejection", con);
                UpdateReasonOfRejection.CommandType = CommandType.StoredProcedure;
                UpdateReasonOfRejection.Parameters.AddWithValue("@DistinctionCode", ApprovalForm.DistinctionCode);
                //UpdateReasonOfRejection.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
                //UpdateReasonOfRejection.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
                //UpdateReasonOfRejection.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
                UpdateReasonOfRejection.Parameters.AddWithValue("@ReasonOfRejection", "Transfer to " + SectionDropdown.Text + " - " + ReasonTextBox.Text);
                UpdateReasonOfRejection.ExecuteNonQuery();
                con.Close();

                //Send notification email to responsible section 
                SendEmail(); //NOTE: Temporary enabled due to ongoing email content creation
            }
            

            MessageBox.Show("Item with line stop detail of " + ApprovalForm.LineStopDetail + " was rejected successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        //===================================================================================================================>>>>>>>>>>>>

        private void UpdateResponsibleSection()
        {
            //// -> SQL query to insert user account
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            //SqlCommand UpdateReasonOfRejection = new SqlCommand("SP_UpdateResponsibleSection", con);
            //UpdateReasonOfRejection.CommandType = CommandType.StoredProcedure;
            //UpdateReasonOfRejection.Parameters.AddWithValue("@ReferenceNo", LineStopDetailTextBox.Text);
            //UpdateReasonOfRejection.Parameters.AddWithValue("@ResponsibleSection", SectionDropdown.Text);
            //UpdateReasonOfRejection.ExecuteNonQuery();
            //con.Close();

            //this.Close();
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void UpdateApprovalStatus()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "Rejected");
            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", ApprovalForm.DistinctionCode);
            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
            UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Rejected by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            UpdateApprovalStatus.ExecuteNonQuery();
            con.Close();

            UpdateReasonOfRejection();
            InsertActualSubmissionDate(ApprovalForm.DistinctionCode, DateTime.Now);
        }

        public void InsertActualSubmissionDate(string distinctionCode, DateTime submissionDate)
        {
            string insertQuery = @"
            INSERT INTO ActualSubmissionOfCountermeasure (DistinctionCode, SubmissionDate)
            VALUES (@DistinctionCode, @SubmissionDate);
        ";

            using (SqlConnection conn = new SqlConnection(Connection.SQLControl.MHMS_Conn))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@DistinctionCode", distinctionCode);
                cmd.Parameters.AddWithValue("@SubmissionDate", submissionDate);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting submission date: " + ex.Message);
                }
            }
        }

        private void AttachedFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog AttachFile = new OpenFileDialog();
                AttachFile.Filter = "Select Valid Document(*.pdf; *.doc; *.xlsx; *.html; *.jpg)|*.pdf; *.docx; *.xlsx; *.html; *.jpg";

                if (AttachFile.ShowDialog() == DialogResult.OK)
                {
                    FileName.Text = AttachFile.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void SendEmail()
        {
            //Email header.
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            builder.Append("<h2>Manhour Management System (MHMS)</h2>");
            builder.Append("<br>" + DateTime.Now);
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("Good day!");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("This is to inform you that " + LoginForm.UserSection + " section have transfered Manhour loss data in your section.");
            builder.Append("<br>");
            builder.Append("<br>");

            //builder.Append("Please see the details below.");
            //builder.Append("<br>");
            //builder.Append("<br>");

            //ONGOING DEVELOPMENT ==================START========================>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            //string mailBody = "<table width='100%' style='border:Solid 1px #4E4E4E;'>";
            //mailBody += "<tr style = 'background-color: #0D204A;'>"
            //    + "<th style='padding:8px; border:none;'>Date Encountered</th>"
            //    + "<th style='padding:8px; border:none;'>Line Stop Detail</th>"
            //    + "<th style='padding:8px; border:none;'>Stop Time</th>"
            //    + "<th style='padding:8px; border:none;'>Direct MP</th>"
            //    + "<th style='padding:8px; border:none;'>Semi Direct MP</th>"
            //    + "<th style='padding:8px; border:none;'>Loss Manhour</th>"
            //    + "<th style='padding:8px; border:none;'>COPQ Amount</th>";
            //mailBody += "</tr>";

            //mailBody += "<tr align='Center'>";

            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            //SqlCommand SelectTransferedMHData = new SqlCommand("SP_SelectTransferedMHData", con);
            //SelectTransferedMHData.CommandType = CommandType.StoredProcedure;
            //SelectTransferedMHData.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            //SelectTransferedMHData.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            //SelectTransferedMHData.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
            //SqlDataAdapter da = new SqlDataAdapter(SelectTransferedMHData);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //if (dt.Rows.Count > 0)
            //{
            //    SqlDataReader reader = SelectTransferedMHData.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        mailBody += "<td stlye='color:blue;'>" + reader["Date Encountered"].ToString() + "</td>";
            //        mailBody += "<td stlye='color:blue;'>" + reader["Line Stop Detail"].ToString() + "</td>";
            //        mailBody += "<td stlye='color:blue;'>" + reader["Stop Time"].ToString() + "</td>";
            //        mailBody += "<td stlye='color:blue;'>" + reader["Direct MP"].ToString() + "</td>";
            //        mailBody += "<td stlye='color:blue;'>" + reader["Semi-Direct MP"].ToString() + "</td>";
            //        mailBody += "<td stlye='color:blue;'>" + reader["Loss Manhour"].ToString() + "</td>";
            //        mailBody += "<td stlye='color:blue;'>" + reader["COPQ Amount"].ToString() + "</td>";
            //    }
            //}

            //con.Close();

            //mailBody += "</tr>";

            //mailBody += "</table>";

            //builder.Append("" + mailBody);

            //ONGOING DEVELOPMENT 03/07/2023 ==================END========================>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            builder.Append("<br>");
            builder.Append("<br><b><font color=red>This is automatic generated email, Do not reply!</b><br></font>");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("<br>Thank you!").AppendLine();
            innerString = builder.ToString();

            EmailNotif(); // ---> Call out the email notification function
        }

        string EmailTo; // ---> Declared string.
        string innerString; // ---> Declared string.
        string Section;
        Attachment attach; //use to attach file in email

        private void EmailNotif()
        {
            //    Regex r/gex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            //bool isValid = regex.IsMatch(Email.Text.Trim());

            if (FileName.Text != "")
            {
                attach = new Attachment(FileName.Text);
            }

            SqlCommand LoadUsersPIC = new SqlCommand("SP_LoadUsersPIC", con);
            LoadUsersPIC.CommandType = CommandType.StoredProcedure;
            LoadUsersPIC.Parameters.AddWithValue("@Procedure", "SelectUserEmailForRejection");
            LoadUsersPIC.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(LoadUsersPIC);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                Section = row["Section"].ToString();

                EmailTo = row["Email"].ToString();

                //Email structure.
                MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailTo);
                mail.Bcc.Add(new MailAddress("arvin.caparros@brother-biph.com.ph"));
                mail.Bcc.Add(new MailAddress("charlotte.robles@brother-biph.com.ph"));
                mail.Bcc.Add(new MailAddress("donnalie.balba@brother-biph.com.ph"));
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "10.113.10.1";

                if (attach != null)
                {
                    mail.Attachments.Add(attach);
                }

                mail.Subject = "[BIPH_MHMS] - Notification";

                mail.Body = innerString;
                mail.IsBodyHtml = true;
                client.Send(mail);
            }

            MessageBox.Show("Email sent!");
        }

        private void SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection();
        }

        private void SectionDropdown_DropDownClosed(object sender, EventArgs e)
        {
            SectionDropdown.Text = "Section (Optional)";
        }

        //==================================================================================================================>>>>>>>>>>>>
    }
}
