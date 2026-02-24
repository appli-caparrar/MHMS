using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MHMS.Forms;
using MHMS.Connection;

namespace MHMS.Class
{
    internal class COPQ_ApproveByMGR
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public async Task ApproveByReceivingMGR(DataGridView approvalDatagrid)
        {
            try
            {
                foreach (DataGridViewRow row in approvalDatagrid.Rows)
                {
                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                    {
                        string DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                        string LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        string SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        string PartCode = row.Cells["Part Code"].Value.ToString();
                        string ApprovalType = ApprovalForm.ApprovalType;
                        string DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

                        await con.OpenAsync();

                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                        await UpdateApprovalStatus.ExecuteNonQueryAsync();

                    }
                }

                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                // Handle any errors during the process
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string ResponsibleSection;
        public async Task ApproveByApplyingMGR(DataGridView approvalDatagrid)
        {
            try
            {
                foreach (DataGridViewRow row in approvalDatagrid.Rows)
                {
                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                    {
                        string DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                        string LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        string SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        string PartCode = row.Cells["Part Code"].Value.ToString();
                        string ApprovalType = ApprovalForm.ApprovalType;
                        string DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();
                        
                        ResponsibleSection = row.Cells["Responsible Section"].Value.ToString();

                        await con.OpenAsync();

                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                        UpdateApprovalStatus.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ////Send email to Receiving section COPQ PIC
                //COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                //COPQ_SendEmail.SendEmailToReceivingCOPQPIC(ResponsibleSection);

            }
            catch (Exception ex)
            {
                // Handle any errors during the process
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
