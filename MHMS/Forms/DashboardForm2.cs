using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class DashboardForm2 : Form
    {
        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);


        public DashboardForm2()
        {
            InitializeComponent();
        }

        public static bool COPQAcceptanceButtonIsClicked = false;
        public static bool STButtonIsClicked = false;
        public static bool WCCCButtonIsClicked = false;

        private void COPQAcceptanceForm_Click(object sender, EventArgs e)
        {
            Dashboard.COPQAcceptanceIsClicked = true;
            COPQAcceptanceButtonIsClicked = true;
            STButtonIsClicked = false;
            WCCCButtonIsClicked = false;
        }

        private void STButton2_Click(object sender, EventArgs e)
        {
            Dashboard.STIsClicked = true;
            STButtonIsClicked = true;
            COPQAcceptanceButtonIsClicked = false;
            WCCCButtonIsClicked = false;
        }

        private void CostWorkCenterFormButton_Click(object sender, EventArgs e)
        {
            Dashboard.WCCCIsClicked = true;
            WCCCButtonIsClicked = true;
            STButtonIsClicked = false;
            COPQAcceptanceButtonIsClicked = false;
        }

        private void DashboardForm2_Load(object sender, EventArgs e)
        {
            LoadAllPendingRequest();
            LoadAllApprovedRequest();
        }

        private void LoadAllPendingRequest()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (LoginForm.UserSection == "BPS" || LoginForm.UserSection == "Production Engineering")
            {
                // -> SQL query to select approval data for COPQ PIC approval
                SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllForApprovalData");
                SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", "");
                SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PendingDataGrid.DataSource = dt;
                con.Close();
            }
            else if (LoginForm.UserSection == "Quality Innovation")
            {
                PendingLabel.Text = "FOR CONFIRMATION";

                // -> SQL query to select approval data for COPQ PIC approval
                SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllForConfirmationByQI");
                SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PendingDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                // -> SQL query to select approval data for COPQ PIC approval
                SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllForApprovalDataBySection");
                SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                PendingDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void LoadAllApprovedRequest()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (ExcludeEECheckBox.Checked == true)
            {
                if (LoginForm.UserSection == "BPS" || LoginForm.UserSection == "Production Engineering")
                {
                    // -> SQL query to select approval data for COPQ PIC approval
                    SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                    SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllApprovedData");
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", "");
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "true");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovedDataGrid.DataSource = dt;
                    con.Close();
                }
                else if (LoginForm.UserSection == "Quality Innovation")
                {
                    ApprovedLabel.Text = "CONFIRMED";

                    // -> SQL query to select approval data for COPQ PIC approval
                    SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                    SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllConfirmed");
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "true");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovedDataGrid.DataSource = dt;
                    con.Close();
                }
                else
                {
                    // -> SQL query to select approval data for COPQ PIC approval
                    SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                    SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllApprovedDataBySection");
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "true");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovedDataGrid.DataSource = dt;
                    con.Close();
                }
            }
            else
            {
                if (LoginForm.UserSection == "BPS" || LoginForm.UserSection == "Production Engineering")
                {
                    // -> SQL query to select approval data for COPQ PIC approval
                    SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                    SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllApprovedData");
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", "");
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovedDataGrid.DataSource = dt;
                    con.Close();
                }
                else if (LoginForm.UserSection == "Quality Innovation")
                {
                    ApprovedLabel.Text = "CONFIRMED";

                    // -> SQL query to select approval data for COPQ PIC approval
                    SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                    SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllConfirmed");
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovedDataGrid.DataSource = dt;
                    con.Close();
                }
                else
                {
                    // -> SQL query to select approval data for COPQ PIC approval
                    SqlCommand SelectAllApprovalRequestData = new SqlCommand("SP_SelectAllApprovalRequestData", con);
                    SelectAllApprovalRequestData.CommandType = CommandType.StoredProcedure;
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Procedure", "SelectAllApprovedDataBySection");
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SelectAllApprovalRequestData.Parameters.AddWithValue("@ExcludeEE", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SelectAllApprovalRequestData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovedDataGrid.DataSource = dt;
                    con.Close();
                }
            }
        }

        private void PartsLossButton_Click(object sender, EventArgs e)
        {
          
        }

        private void MHLossButton_Click(object sender, EventArgs e)
        {
            
        }

        private void ExcludeEECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadAllApprovedRequest();
        }
    }
}
