using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MHMS.Connection;
using MHMS.Forms;

namespace MHMS.Class
{
    internal class COPQ_ApproveReceivingCOPQPIC
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);


        public async Task ApproveByReceivingCOPQPIC(string selectedProcessInCharge)
        {
            try
            {

                // Open the connection asynchronously
                await con.OpenAsync();

                // Prepare the SQL command for the stored procedure
                SqlCommand updateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add the parameters to the command
                updateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQPIC");
                updateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", ApprovalForm.DistinctionCode);
                updateApprovalStatus.Parameters.AddWithValue("@Reason", ""); // Empty parameter to prevent error
                updateApprovalStatus.Parameters.AddWithValue("@MHLossType", ""); // Empty parameter to prevent error
                updateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by COPQ Process In-Charge");
                updateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.SelectedApprovalType);
                updateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", ApprovalForm.SelectedProcessIncharge);
                updateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));

                // Execute the command asynchronously
                await updateApprovalStatus.ExecuteNonQueryAsync();


                // Call the method to update process in charge name
                //await UpdateProcessInChargeNameAsync(DistinctionCode);  


                // Show the success message after all approvals are processed
                MessageBox.Show("Approved Successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                // Handle any errors during the process
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // This is an example of how you can implement the UpdateProcessInChargeNameAsync method asynchronously
        private async Task UpdateProcessInChargeNameAsync(string DistinctionCode)
        {
            try
            {
                // Use 'using' to automatically manage the connection's lifecycle
                using (SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn))
                {
                    await con.OpenAsync(); // Open the connection asynchronously

                    SqlCommand updateApprovalStatus = new SqlCommand("SP_ProcessInChargeName", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    updateApprovalStatus.Parameters.AddWithValue("@ProcessInChargeName", ProcessInchargeForm.ProcessInCharge);
                    updateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);

                    // Execute the command asynchronously
                    await updateApprovalStatus.ExecuteNonQueryAsync();
                } // The connection will be automatically closed and disposed when done
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                MessageBox.Show($"Error updating process in charge: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
