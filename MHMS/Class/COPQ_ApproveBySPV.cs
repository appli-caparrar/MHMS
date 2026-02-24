using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using MHMS.Forms;
using MHMS.Connection;

namespace MHMS.Class
{
    internal class COPQ_ApproveBySPV
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        string ResponsibleSection;
        public async Task ApproveByReceivingSPV(DataGridView approvalDatagrid)
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
                        string ApprovalType = ApprovalForm.SelectedApprovalType;
                        string DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

                        ResponsibleSection = row.Cells["Responsible Section"].Value.ToString();

                        // -> SQL query to update approval status
                        if (con.State == ConnectionState.Closed)
                        {
                            await con.OpenAsync();
                        }


                        if (Dashboard.SectionText.Replace("BIPH-", "") == "Equipment Engineering")
                        {
                            //Modified date: 10/05/2023
                            //Note: For ovservation

                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBy_EESPV");
                            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);    
                            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                            UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Automatic System Approved");
                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Automatic System Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                            await UpdateApprovalStatus.ExecuteNonQueryAsync();

                        }
                        else
                        {
                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
                            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                            UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by MGR");
                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                            UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                            await UpdateApprovalStatus.ExecuteNonQueryAsync();

                        }

                    }
                }

                //AcceptButtonIsClicked = true;
                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);


                ////Send email to Receiving section Process In-charge
                //COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                //COPQ_SendEmail.SendEmailToReceivingMGR(ResponsibleSection);
            }
            catch (Exception ex)
            {
                // Handle any errors during the process
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public async Task ApproveByApplyingSPV(DataGridView approvalDatagrid)
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
                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
                        UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalType);
                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by MGR");
                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                        UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                        await UpdateApprovalStatus.ExecuteNonQueryAsync();

                    }
                }

                //AcceptButtonIsClicked = true;
                MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ////Send email to MGR
                //COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
                //COPQ_SendEmail.SendEmailToApplyingMGR(Dashboard.SectionText.Replace("BIPH-", ""));
            }
            catch (Exception ex)
            {
                // Handle any errors during the process
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
