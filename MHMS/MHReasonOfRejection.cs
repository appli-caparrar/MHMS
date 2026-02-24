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
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class MHReasonOfRejection : Form
    {
        //Connection String
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public MHReasonOfRejection()
        {
            InitializeComponent();
        }

        private void MHReasonOfRejection_Load(object sender, EventArgs e)
        {

        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            if (ReasonTextBox.Text == "")
            {
                MessageBox.Show("Please type reason of rejection.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (MHApproval.ApplicationFormType == "ST")
                {
                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "RejectST");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ReasonTextBox.Text);
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Rejected by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString());
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "Rejected");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Rejected");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email...
                    SendSTApplicationRejectionEmailMessage();

                    //MHApproval.IsRejectClicked = true;

                    MessageBox.Show("Rejected successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else if (MHApproval.ApplicationFormType == "WC/CC")
                {
                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "RejectWCCC");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ReasonTextBox.Text);
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Rejected by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString());
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "Rejected");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Rejected");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email...
                    SendWCCCRejectionEmailMessage();

                    //MHApproval.IsRejectClicked = true;

                    MessageBox.Show("Rejected successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                }
                else if (MHApproval.ApplicationFormType == "Open MH System")
                {
                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", MHApproval.ApplicationFormType);
                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "RejectOpenMH");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", MHApproval.ReferenceNumber);
                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ReasonTextBox.Text);
                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Rejected by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString());
                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "Rejected");
                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Rejected");
                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "");
                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                    UpdateApprovalStatus.ExecuteNonQuery();
                    con.Close();

                    //Send email...
                    SendOpenMHRejectionEmailMessage();

                    //MHApproval.IsRejectClicked = true;

                    MessageBox.Show("Rejected successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                }
            }
        }


        string innerString;
        string FirstName;
        string LastName;
        string Email;

        private void SendSTApplicationRejectionEmailMessage()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            //SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            //SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
            //SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            //SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            //SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            //DataTable dTable = new DataTable();
            //sda.Fill(dTable);
            //con.Close();


            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();

            if (dTable2.Rows.Count > 0)
            {
                //string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                //string EmailListTo = String.Join("; ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListTo = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());


                foreach (DataRow row in dTable2.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                   

                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の年計ST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P－タッチ課の年計ST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課の年計ST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課の年計ST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課の年計ST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課の年計ST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課の年計ST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Annual Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課の年計ST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P－タッチ課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }

                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P－タッチ課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("Change ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のST変更機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }

                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の新規ST機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P－タッチ課の新規ST機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課の新規ST機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課の新規ST機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>生産技術課の新規ST機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課の新規ST機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課の新規ST機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("New ST Model List application of " + MHApproval.Section + " section is rejected, see link below");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課の新規ST機種一覧申請が却下されました。以下をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }

                    }

                  

                    builder.Append("(See 'Remarks' box in below file name for the reason)");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>以下のファイル名の備考欄にて却下理由をご覧下さい。</font>");

                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("<b>Please check and revise immediately</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>直ぐに内容を確認し、改訂お願いします。</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                    builder.Append("<br>");
                    builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                    builder.Append("<br><br><br>");

                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>[本メールは自動配信されています]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        //mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's disapproved " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void SendWCCCRejectionEmailMessage()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            //SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            //SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
            //SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            //SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            //SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            //DataTable dTable = new DataTable();
            //sda.Fill(dTable);
            //con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();


            if (dTable2.Rows.Count > 0)
            {
                //string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                //string EmailListTo = String.Join("; ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListTo = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable2.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                   

                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>お疲れ様です。</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    if (MHApproval.Category == "Work Center New")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                     
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が却下された事をお知らせします。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (MHApproval.Category == "Work Center Revision")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("This is to inform you that the Revision of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請が却下された事をお知らせします。</font>");//For edit -> For Translate
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
            
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のワークセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("This is to inform you that the New Work Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のワークセンターの改訂申請が却下された事をお知らせします。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (MHApproval.Category == "Work Center Deletion")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のワークセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                        
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のワークセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のワークセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("This is to inform you that the Deletion of Work Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のワークセンターの削除申請が却下された事をお知らせします。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (MHApproval.Category == "Cost Center New")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                    
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課の新規コストセンター登録申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("This is to inform you that the New Cost Center Registration for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課の新規コストセンター登録申請が却下された事をお知らせします。</font>");
                            //builder.Append("<br>");
                        }

                    }
                    else if (MHApproval.Category == "Cost Center Revision")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                   
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のコストセンターの改訂申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("This is to inform you that the Revision of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のコストセンターの改訂申請が却下された事をお知らせします。</font>");
                            //builder.Append("<br>");
                        }


                    }
                    else if (MHApproval.Category == "Cost Center Deletion")
                    {
                        if (MHApproval.Section == "Printer")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>プリンター課のコストセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "P-Touch")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Cartridge")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Ink Head")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Tape Cassette")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "BPS")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                    
                        }
                        else if (MHApproval.Section == "PCBA")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のコストセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Molding Production")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>成形課のコストセンターの削除申請が却下された事をお知らせします。</font>");
                            builder.Append("<br>");
                        }
                        else if (MHApproval.Section == "Toner")
                        {
                            builder.Append("This is to inform you that the Deletion of Cost Center for " + MHApproval.Section + " section has been disapproved.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>成形課のコストセンターの削除申請が却下された事をお知らせします。</font>");
                            //builder.Append("<br>");
                        }


                    }

                    //builder.Append("<b>Please check and revise immediately</b>");
                    //builder.Append("<br>");
                    //builder.Append("<font color=blue>速やかにに確認と修正をお願いします</font>");
                    //builder.Append("<br>");
                    //builder.Append("<br>");

                    builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                    builder.Append("<br>");
                    builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                    builder.Append("<br><br><br>");

                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>申請ファイルに問題が生じた場合は、下記のメーリングリストに連絡下さい</font>");
                    builder.Append("<br><br>");

                    builder.Append("<b>PM Group Mailing List</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                    builder.Append("<br>");
                    builder.Append("Bautista, Princess (BIPH-PE) <princess.bautista@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Tan, Lina (BIPH-PE) <lina.tan@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Mateo, Bradly (BIPH-PE) <bradly.mateo@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Dimayuga, Jeancy (BIPH-PE) <jeancy.dimayuga@brother-biph.com.ph>");
                    builder.Append("<br>");
                    builder.Append("Balba, Donnalie (BIPH-PE) <donnalie.balba@brother-biph.com.ph>");
                    builder.Append("<br><br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<font color=blue>ご尽力頂きありがとうございます。</font>");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        //mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's " + MHApproval.Category + " section's Disapproved Application.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }


            }

        }

        private void SendOpenMHRejectionEmailMessage()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            //SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            //SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
            //SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            //SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            //SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            //DataTable dTable = new DataTable();
            //sda.Fill(dTable);
            //con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", MHApproval.ApplicationFormType);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();

            if (dTable2.Rows.Count > 0)
            {
                //string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                //string EmailListTo = String.Join("; ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListTo = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable2.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                   
                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>お疲れ様です。</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");


                  
                    if (MHApproval.Section == "Printer")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>プリンター課のMHシステム編集解除許可(OPEN MH)申請が却下されました。</font>");
                        builder.Append("<br>");
                    }
                    else if (MHApproval.Section == "P-Touch")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>P-タッチ課のMHシステム編集解除許可(OPEN MH)申請が却下されました。</font>");
                        builder.Append("<br>");
                    }
                    else if (MHApproval.Section == "Ink Cartridge")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>インクカートリッジのオープンMHシステム要求が拒否されました、以下のリンクを参照してください</font>");
                        builder.Append("<br>");
                    }
                    else if (MHApproval.Section == "Ink Head")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>プリンター課のMHシステム編集解除許可(OPEN MH)申請が却下されました。</font>");
                        builder.Append("<br>");
                    }
                    else if (MHApproval.Section == "Tape Cassette")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>テープカセット課のMHシステム編集解除許可(OPEN MH)申請が却下されました。</font>");
                        builder.Append("<br>");
                    }
                    else if (MHApproval.Section == "BPS")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                     
                    }
                    else if (MHApproval.Section == "PCBA")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>基板組立課のMHシステム編集解除許可(OPEN MH)申請が却下されました。</font>");
                        builder.Append("<br>");
                    }
                    else if (MHApproval.Section == "Molding Production")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>成形課のMHシステム編集解除許可(OPEN MH)申請が却下されました。</font>");
                        builder.Append("<br>");
                    }
                    else if (MHApproval.Section == "Toner")
                    {
                        builder.Append("Open MH system request of " + MHApproval.Section + "is Rejected, see link below");
                        builder.Append("<br>");
                        //builder.Append("<font color=blue>成形課のMHシステム編集解除許可(OPEN MH)申請が却下されました。</font>");
                        //builder.Append("<br>");
                    }


                    builder.Append("<b>Please check and revise immediately</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>速やかにに確認と修正をお願いします</font>");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                    builder.Append("<br>");
                    builder.Append("<b><i>" + MHApproval.ReferenceNumber + "</i></b>");
                    builder.Append("<br><br><br>");

                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        //mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " disapproved Open MH system request";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }






    }
}
