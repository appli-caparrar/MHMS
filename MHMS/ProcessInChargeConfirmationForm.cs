using MHMS.Connection;
using MHMS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class ProcessInChargeConfirmationForm : Form
    {
        // Connection string
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public ProcessInChargeConfirmationForm()
        {
            InitializeComponent();
        }

        private void ProcessInChargeConfirmationForm_Load(object sender, EventArgs e)
        {
            LineStopDetail.Text = ApprovalForm.LineStopDetail;
        }

        private async void ApproveButton_Click(object sender, EventArgs e)
        {
            if (CauseTextBox.Text == "")
            {
                MessageBox.Show("Please type the cause.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (CountermeasureTextBox.Text == "")
            {
                MessageBox.Show("Please type the countermeasure.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    await UpdateApprovalStatus();
                }
                    catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async Task UpdateApprovalStatus()
        {
            try
            {
               
                await con.OpenAsync();

                if (LoginForm.ProcessInCharge == "✔️")
                {
                    using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                    {
                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQProcessInCharge");
                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", ApprovalForm.DistinctionCode);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by SPV");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                            "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));

                        await UpdateApprovalStatus.ExecuteNonQueryAsync();
                    }

                    await UpdateCauseAndCountemeasure();

                    MessageBox.Show("Approved Successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
              
                 con.Close();
               
            }
        }


        public async Task UpdateCauseAndCountemeasure()
        {
            await con.OpenAsync();

            // Use try-catch-finally to ensure proper resource management
            try
            {
                // Create a SqlCommand object to execute the stored procedure
                using (SqlCommand updateCommand = new SqlCommand("SP_UpdateCauseAndCountemeasure", con))
                {
                    updateCommand.CommandType = CommandType.StoredProcedure;

                    // Add parameters explicitly with their data types
                    updateCommand.Parameters.Add("@DistinctionCode", SqlDbType.VarChar).Value = ApprovalForm.DistinctionCode;
                    updateCommand.Parameters.Add("@Cause", SqlDbType.VarChar).Value = CauseTextBox.Text;
                    updateCommand.Parameters.Add("@Countermeasure", SqlDbType.VarChar).Value = CountermeasureTextBox.Text;

                    // Execute the command asynchronously
                    await updateCommand.ExecuteNonQueryAsync();
                }

                // Call to insert the submission date asynchronously
                await InsertCountermeasureSubmissionDateAsync(ApprovalForm.DistinctionCode, DateTime.Now);
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                // Close the connection after the operation is complete
                con.Close(); // Close the connection asynchronously
            }
        }

        public async Task InsertCountermeasureSubmissionDateAsync(string distinctionCode, DateTime submissionDate)
        {
            await con.OpenAsync();  // Open the connection asynchronously

            try
            {
                // SQL query to insert DistinctionCode and Date into ActualSubmissionOfCountermeasure
                string query = "INSERT INTO ActualSubmissionOfCountermeasure (DistinctionCode, Date) VALUES (@DistinctionCode, @Date)";

                // Create a SQL command and add parameters
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@DistinctionCode", distinctionCode);
                    cmd.Parameters.AddWithValue("@Date", submissionDate);

                    // Execute the command asynchronously
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions here
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection after the operation is complete
                con.Close();  // Close the connection asynchronously
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
    }
}
