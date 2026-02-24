using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MHMS.Connection;
using MHMS.Forms;

namespace MHMS.Class
{
    internal class COPQ_ApproveByProcessInCharge
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        //public static string DateEncountered;
        //public static string LineStopDetail;
        //public static string SelectedLineStopDetail;
        //public static string PartCode;
        //public static string ApprovalType;
        //public static string COPQAmount;
        //public static string DistinctionCode;
        public static string ResponsibleSection;

        public async Task AprroveByProcessInCharge(DataGridView approvalDataGrid)
        {
            try
            {
                foreach (DataGridViewRow row in approvalDataGrid.Rows)
                {
                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                    {
                        string DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                        string LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        string SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        string PartCode = row.Cells["Part Code"].Value.ToString();
                        string ApprovalType = ApprovalForm.SelectedApprovalType;
                        string COPQAmount = row.Cells["COPQ Amount"].Value.ToString();
                        string DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

                        ResponsibleSection = row.Cells["Responsible Section"].Value.ToString();

                        // Open the connection asynchronously
                        await con.OpenAsync();

                        if (Convert.ToDecimal(COPQAmount) >= 100)
                        {
                            ProcessInChargeConfirmationForm processInChargeConfirmationForm = new ProcessInChargeConfirmationForm();
                            processInChargeConfirmationForm.ShowDialog();
                        }
                        else
                        {
                            // Prepare the SQL command for the stored procedure
                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQProcessInCharge");
                            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                            UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by SPV");
                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "");
                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            // Execute the command asynchronously
                            await UpdateApprovalStatus.ExecuteNonQueryAsync();
                        }
                    }
                }

                //AcceptButtonIsClicked = true;
                MessageBox.Show("Approved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);


                ////Send email to Receiving section SPV
                //COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                //COPQ_SendEmail.SendEmailToReceivingSPV(ResponsibleSection);
            }
            catch (Exception ex)
            {
                // Handle any errors during the process
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

    }
}
