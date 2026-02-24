using MHMS.Connection;
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

namespace MHMS.Forms
{
    public partial class DashboardForm3 : Form
    {
        //Connection String
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection conn = new SqlConnection(SQLControl.MHMS_Conn); 
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);
        

        public DashboardForm3()
        {
            InitializeComponent();
        }


        private void DashboardForm3_Load(object sender, EventArgs e)
        {
            if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
            {
                SectionDropdown.Enabled = true;
                LoadSection();

            }
            else
            {
                SectionDropdown.Enabled = false;
                SectionDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            }
            
        }

        //Load Section in combobox
        public void LoadSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            // -> SQL query to select User Account
            SqlCommand LoadSection = new SqlCommand("SP_LoadSection", conn);
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


        private void AddCategoryPerApplication()
        {
            if (ApplicationTypeDropdown.Text == "COPQ")
            {

            }
            else if (ApplicationTypeDropdown.Text == "ST")
            {
                //Remove
                CategoryDropdown.Items.Remove("All Category");
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                //Remove
                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Add
                CategoryDropdown.Items.Add("All Category");
                CategoryDropdown.Items.Add("Annual ST Change");
                CategoryDropdown.Items.Add("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Add("MH Change ST Model List Form");
                CategoryDropdown.Items.Add("MH New ST Model List Form");

            }
            else if (ApplicationTypeDropdown.Text == "WC/CC")
            {
                //Remove
                CategoryDropdown.Items.Remove("All Category");
                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                //Remove
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Add
                CategoryDropdown.Items.Add("All Category");
                CategoryDropdown.Items.Add("Work Center New");
                CategoryDropdown.Items.Add("Work Center Revision");
                CategoryDropdown.Items.Add("Work Center Deletion");
                CategoryDropdown.Items.Add("Cost Center New");
                CategoryDropdown.Items.Add("Cost Center Revision");
                CategoryDropdown.Items.Add("Cost Center Deletion");
            }
            else if (ApplicationTypeDropdown.Text == "Open MH System")
            {
                //Remove
                CategoryDropdown.Items.Remove("All Category");
                CategoryDropdown.Items.Remove("Manpower/Man-hour");
                CategoryDropdown.Items.Remove("Standard Time (ST mins)");
                CategoryDropdown.Items.Remove("Linestop/Loss Man-hour/Loss Factor");

                //Remove
                CategoryDropdown.Items.Remove("Annual ST Change");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form - No BIL Approval");
                CategoryDropdown.Items.Remove("MH Change ST Model List Form");
                CategoryDropdown.Items.Remove("MH New ST Model List Form");

                CategoryDropdown.Items.Remove("Work Center New");
                CategoryDropdown.Items.Remove("Work Center Revision");
                CategoryDropdown.Items.Remove("Work Center Deletion");
                CategoryDropdown.Items.Remove("Cost Center New");
                CategoryDropdown.Items.Remove("Cost Center Revision");
                CategoryDropdown.Items.Remove("Cost Center Deletion");

                //Add
                CategoryDropdown.Items.Add("All Category");
                CategoryDropdown.Items.Add("Manpower/Man-hour");
                CategoryDropdown.Items.Add("Standard Time (ST mins)");
                CategoryDropdown.Items.Add("Linestop/Loss Man-hour/Loss Factor");
            }

        }

        private void ApplicationTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please select a section.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (ApplicationTypeDropdown.Text == "COPQ")
                {
                    CategoryDropdown.Enabled = false;
                }
                else
                {
                    CategoryDropdown.Enabled = true;
                    AddCategoryPerApplication();
                }
            }
            
        }

        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ApplicationTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select application type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

            }
        }

        private void MonthDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (CategoryDropdown.Text == "")
            //{
            //    MessageBox.Show("Please select a category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else
            //{
            //    SelectAllApplication();
            //    SelectPendingApplication();
            //    SelectApprovedApplication();
            //    SelectRejectedApplication();
            //    SelectCountOfPendingApplicationPerApprover();
            //}
        }


        private void SelectAllApplication()
        {
            if (ApplicationTypeDropdown.Text == "COPQ")
            {

            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectAllApplication = new SqlCommand("SP_SelectAllApplication", con);
                SelectAllApplication.CommandType = CommandType.StoredProcedure;
                SelectAllApplication.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectAllApplication.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectAllApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectAllApplication.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectAllApplication);
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader = SelectAllApplication.ExecuteReader();

                    while (reader.Read())
                    {
                        NoOfApplication.Text = reader["NoOfApplication"].ToString();
                    }
                }
                else
                {
                    NoOfApplication.Text = "0";
                }

                con.Close();
            }
        }

        private void SelectPendingApplication()
        {
            if (ApplicationTypeDropdown.Text == "COPQ")
            {

            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectPendingApplication = new SqlCommand("SP_SelectPendingApplication", con);
                SelectPendingApplication.CommandType = CommandType.StoredProcedure;
                SelectPendingApplication.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectPendingApplication.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectPendingApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectPendingApplication.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPendingApplication);
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader = SelectPendingApplication.ExecuteReader();

                    while (reader.Read())
                    {
                        NoOfPendingApplication.Text = reader["PendingApplication"].ToString();
                    }
                }
                else
                {
                    NoOfPendingApplication.Text = "0";
                }

                con.Close();

            }
        }

        private void SelectApprovedApplication()
        {
            if (ApplicationTypeDropdown.Text == "COPQ")
            {

            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectApprovedApplication = new SqlCommand("SP_SelectApprovedApplication", con);
                SelectApprovedApplication.CommandType = CommandType.StoredProcedure;
                SelectApprovedApplication.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectApprovedApplication.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectApprovedApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectApprovedApplication.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectApprovedApplication);
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader = SelectApprovedApplication.ExecuteReader();

                    while (reader.Read())
                    {
                        NoOfApprovedApplication.Text = reader["ApprovedApplication"].ToString();
                    }
                }
                else
                {
                    NoOfApprovedApplication.Text = "0";
                }

                con.Close();
            }
        }


        private void SelectRejectedApplication()
        {
            if (ApplicationTypeDropdown.Text == "COPQ")
            {

            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectRejectedApplication = new SqlCommand("SP_SelectRejectedApplication", con);
                SelectRejectedApplication.CommandType = CommandType.StoredProcedure;
                SelectRejectedApplication.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectRejectedApplication.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectRejectedApplication.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectRejectedApplication.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectRejectedApplication);
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader = SelectRejectedApplication.ExecuteReader();

                    while (reader.Read())
                    {
                        NoOfRejectedApplication.Text = reader["RejectedApplication"].ToString();
                    }
                }
                else
                {
                    NoOfRejectedApplication.Text = "0";
                }

                con.Close();
            }
        }


        private void SelectCountOfPendingApplicationPerApprover()
        {
            if (ApplicationTypeDropdown.Text == "COPQ")
            {
                conn.Close();
                conn.Open();
                SqlCommand SelectCountOfPendingApplicationPerApprover = new SqlCommand("SP_SelectCOPQCountOfPendingApplicationPerApprover", conn);
                SelectCountOfPendingApplicationPerApprover.CommandType = CommandType.StoredProcedure;
                SelectCountOfPendingApplicationPerApprover.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectCountOfPendingApplicationPerApprover.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectCountOfPendingApplicationPerApprover.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCountOfPendingApplicationPerApprover);
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);
                PendingApplicationDatagrid.DataSource = dataTable;
                conn.Close();
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectCountOfPendingApplicationPerApprover = new SqlCommand("SP_SelectCountOfPendingApplicationPerApprover", con);
                SelectCountOfPendingApplicationPerApprover.CommandType = CommandType.StoredProcedure;
                SelectCountOfPendingApplicationPerApprover.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectCountOfPendingApplicationPerApprover.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectCountOfPendingApplicationPerApprover.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
                SelectCountOfPendingApplicationPerApprover.Parameters.AddWithValue("@Month", MonthDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCountOfPendingApplicationPerApprover);
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);
                PendingApplicationDatagrid.DataSource = dataTable;
                con.Close();

            }
        }

        private void PendingApplicationDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in PendingApplicationDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void COPQDisposalPartsCostBtn_MouseEnter(object sender, EventArgs e)
        {
            ReportSummaryLabel.Text = "REPORT SUMMARY (COPQ Disposal Parts Cost)";
        }

        private void COPQDisposalPartsCostBtn_MouseLeave(object sender, EventArgs e)
        {
            ReportSummaryLabel.Text = "REPORT SUMMARY";
        }

        private void Top5DisposalCostBtn_MouseEnter(object sender, EventArgs e)
        {
            ReportSummaryLabel.Text = "REPORT SUMMARY (COPQ Top 5 Disposal Parts Cost)";
        }

        private void Top5DisposalCostBtn_MouseLeave(object sender, EventArgs e)
        {
            ReportSummaryLabel.Text = "REPORT SUMMARY";
        }

        private void Top5DefectRecurrenceBtn_MouseEnter(object sender, EventArgs e)
        {
            ReportSummaryLabel.Text = "REPORT SUMMARY (Top 5 Defect Recurrence)";
        }

        private void Top5DefectRecurrenceBtn_MouseLeave(object sender, EventArgs e)
        {
            ReportSummaryLabel.Text = "REPORT SUMMARY";
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (CategoryDropdown.Text == "")
            {
                MessageBox.Show("Please select a category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (ApplicationTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select application form type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (CategoryDropdown.Text == "")
                {
                    MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (MonthDropdown.Text == "")
                {
                    MessageBox.Show("Please select month.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    SelectAllApplication();
                    SelectPendingApplication();
                    SelectApprovedApplication();
                    SelectRejectedApplication();
                    SelectCountOfPendingApplicationPerApprover();
                }
            }
        }


        public static string ApplicationFormType;

        private void ProceedButton_Click(object sender, EventArgs e)
        {
            ApplicationFormType = ApplicationTypeDropdown.Text;

            MHApproval.IsProceedClicked = true;

            Dashboard.ProceedBtnIsClicked = true;
        }

        private void COPQDisposalPartsCostBtn_Click(object sender, EventArgs e)
        {

        }

        private void Top5DisposalCostBtn_Click(object sender, EventArgs e)
        {

        }

        private void Top5DefectRecurrenceBtn_Click(object sender, EventArgs e)
        {

        }

        private void SectionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
