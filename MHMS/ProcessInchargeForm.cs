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
    public partial class ProcessInchargeForm : Form
    {
        // Connection string
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);


        public ProcessInchargeForm()
        {
            InitializeComponent();
        }

        //================================================================================================>>>>>>>>>

        private void ProcessInchargeForm_Load(object sender, EventArgs e)
        {
            LineStopDetailTextBox.Text = ApprovalForm.LineStopDetail;
            LineStopDetailTextBox.ReadOnly = true;
            
        }

        //================================================================================================>>>>>>>>>

        private void SelectCOPQProcessInchargeUsers()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand SelectProcessInchargeUser = new SqlCommand("SP_SelectProcessInchargeUsers", con);
            SelectProcessInchargeUser.CommandType = CommandType.StoredProcedure;
            SelectProcessInchargeUser.Parameters.AddWithValue("@UserSection", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectProcessInchargeUser);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            SelectProcessInchargeUser.ExecuteNonQuery();
            con.Close();

            UserDropdown.DataSource = ds.Tables[0];
            UserDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            //UserDropdown.ValueMember = "Section";
        }

        //================================================================================================>>>>>>>>>

        private void UserDropdown_DropDown(object sender, EventArgs e)
        {
            SelectCOPQProcessInchargeUsers();
        }

        private void UserDropdown_DropDownClosed(object sender, EventArgs e)
        {
            //UserDropdown.Text = "";
        }

        //================================================================================================>>>>>>>>>

        private void UpdateApprovalStatus()
        {
            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            if (LoginForm.COPQPIC == "✔️")
            {
                SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQPIC");
                UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", ApprovalForm.DistinctionCode);
                //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
                //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
                //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
                UpdateApprovalStatus.Parameters.AddWithValue("@Reason", ""); //this an empty parameter to prevent error 
                UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");//this an empty parameter to prevent error 
                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by COPQ Process In-Charge");
                UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                UpdateApprovalStatus.ExecuteNonQuery();
                con.Close();

                UpdateProcessInChargeName();

                MessageBox.Show("Approved Successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ApprovalForm.ApproveButtonIsClicked = true;

            this.Close();
        }

        //================================================================================================>>>>>>>>>

        private void UpdateProcessInChargeName()
        {
            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_ProcessInChargeName", con);
            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            UpdateApprovalStatus.Parameters.AddWithValue("@ProcessInChargeName", UserDropdown.Text);
            UpdateApprovalStatus.Parameters.AddWithValue("@DistinctionCode", ApprovalForm.DistinctionCode);
            //UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            //UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            //UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
            UpdateApprovalStatus.ExecuteNonQuery();
            con.Close();
        }

        //================================================================================================>>>>>>>>>
        public static string ProcessInCharge = string.Empty;
        private void ApproveButton_Click(object sender, EventArgs e)
        {
            if (UserDropdown.Text == "")
            {
                MessageBox.Show("Please select process in-charge.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                ProcessInCharge = UserDropdown.Text;

                this.Close();
            }
            
            
            //UpdateApprovalStatus();
        }

        //================================================================================================>>>>>>>>>

    }
}
