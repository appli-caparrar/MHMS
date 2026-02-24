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
using System.Windows.Controls;
using System.Windows.Forms;
using Z.BulkOperations.Internal.InformationSchema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MHMS.Forms
{
    public partial class FactoryEfficiencyForm : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        //SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public FactoryEfficiencyForm()
        {
            InitializeComponent();

            // Create a CheckedListBox
            CheckedListBox checkedListBox = new CheckedListBox();
            checkedListBox.FormattingEnabled = true;
            checkedListBox.CheckOnClick = true;
            checkedListBox.Width = 150;

            // Add items to the CheckedListBox
            string[] items = { "Option 1", "Option 2", "Option 3", "Option 4" };
            checkedListBox.Items.AddRange(items);

            // Add event handler for item check state change
            checkedListBox.ItemCheck += CheckedListBox_ItemCheck;

            // Add the CheckedListBox to the form
            Controls.Add(checkedListBox);
        }

        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Handle item check state change
            CheckedListBox checkedListBox = (CheckedListBox)sender;
            string selectedItem = checkedListBox.Items[e.Index].ToString();
            bool isChecked = (e.NewValue == CheckState.Checked);

            // Perform actions based on item check state
            if (isChecked)
            {
                MessageBox.Show("Item '" + selectedItem + "' checked.");
            }
            else
            {
                MessageBox.Show("Item '" + selectedItem + "' unchecked.");
            }
        }

        private void FactoryEfficiencyForm_Load(object sender, EventArgs e)
        {
            Summary_FiscalYearDropdown.Text = DateTime.Now.Year.ToString();

            SummTab_FiscalYearDropdown.Text = DateTime.Now.Year.ToString();

            AddYearLists(); //Add years in summary FY dropdown list

            //Set default text to signed in user department
            //Summary_DepartmentDropdown.Text = Dashboard.DepartmentText;
            //FESimulation_DepartmentDropdown.Text = Dashboard.DepartmentText;

            //Set default text to signed in user section
            //Summary_SectionDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            //FESimulation_SectionDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");

            // Customize the header styles
            foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
            {
                if (column.HeaderText.IndexOf("target", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    column.HeaderCell.Style.BackColor = Color.LightBlue;
                    column.HeaderCell.Style.ForeColor = Color.White; // Text color for better contrast
                }
            }

            // Subscribe to the CellFormatting event
            FESummaryDatagrid.CellFormatting += FESummaryDatagrid_CellFormatting;
            
            if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
            {
                WorkingDaysHoursBtn.Visible = true;
            }
            else
            {
                WorkingDaysHoursBtn.Visible = false;
            }
        }


        private void Summary_FiscalYearDropdown_DropDown(object sender, EventArgs e)
        {
            
        }


        private void AddYearLists()
        {
            var currentYear = DateTime.Today.Year;

            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                Summary_FiscalYearDropdown.Items.Add((currentYear - i).ToString()); //Summary FY dropdown list

                SummTab_FiscalYearDropdown.Items.Add((currentYear - i).ToString()); //FE Simulation dropdown list
            }
        }

        private void Summary_SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection_Summary();
        }

        private void FESimulation_SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection_FESimulation();
        }

        private void Summary_DepartmentDropdown_DropDown(object sender, EventArgs e)
        {
            LoadDepartment();
        }

        private void FESimulation_DepartmentDropdown_DropDown(object sender, EventArgs e)
        {
            LoadDepartment();
        }

        public void LoadSection_Summary()
        {
            
            con.Open();
            // -> SQL query to select User Account
            SqlCommand SelectFESection = new SqlCommand("SP_Select_FE_Section", con);
            SelectFESection.CommandType = CommandType.StoredProcedure;
            SelectFESection.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectFESection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            SelectFESection.ExecuteNonQuery();
            con.Close();

            Summary_SectionDropdown.DataSource = ds.Tables[0];
            Summary_SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();

            //FESimulation_SectionDropdown.DataSource = ds.Tables[0];
            //FESimulation_SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            //SectionDropdown.ValueMember = "";
        }

        public void LoadSection_FESimulation()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand SelectFESection = new SqlCommand("SP_Select_FE_Section", con);
            SelectFESection.CommandType = CommandType.StoredProcedure;
            SelectFESection.Parameters.AddWithValue("@Department", SummTab_DepartmentDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectFESection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            SelectFESection.ExecuteNonQuery();
            con.Close();

            //Summary_SectionDropdown.DataSource = ds.Tables[0];
            //Summary_SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();

            SummTab_SectionDropdown.DataSource = ds.Tables[0];
            SummTab_SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            //SectionDropdown.ValueMember = "";
        }

        public void LoadDepartment()
        {

            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // SQL query to select User Account
            SqlCommand SelectFEDepartment = new SqlCommand("SP_Select_FE_Department", con);
            SelectFEDepartment.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(SelectFEDepartment);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            // Close the connection after fetching the data
            SelectFEDepartment.ExecuteNonQuery();
            con.Close();

            // Create a list to hold both dynamic and static items
            BindingList<string> departmentList = new BindingList<string>();

            // Add static items to the list
            departmentList.Add("All");
            departmentList.Add("None");

            // Add dynamic items from the database
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                departmentList.Add(row["Department"].ToString());
            }

            // Bind the department list to the ComboBox
            Summary_DepartmentDropdown.DataSource = departmentList;
            SummTab_DepartmentDropdown.DataSource = departmentList;


        }
        // Method to add static items to the ComboBox
        private void AddStaticItems(ComboBox comboBox)
        {
            // Add "All" as the first item
            comboBox.Items.Insert(0, "All");

            comboBox.SelectedIndex = 0; // Select "All" by default (optional)
        }

        private void FESimulationUpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateFactoryEfficiencyForm UpdateFEForm = new UpdateFactoryEfficiencyForm();
            UpdateFEForm.ShowDialog();
        }

        private void FEUpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateFactoryEfficiencyForm UpdateFEForm = new UpdateFactoryEfficiencyForm();
            UpdateFEForm.ShowDialog();
        }

        private void AdjustmentBtn_Click(object sender, EventArgs e)
        {
            AdjustmentForm AdjForm = new AdjustmentForm();
            AdjForm.ShowDialog();
        }

        private void FESimulation_DepartmentDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSection_FESimulation();
        }

        private void Summary_DepartmentDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadSection_Summary();
            if (Summary_DepartmentDropdown.Text == "All")
            {
                Summary_SectionDropdown.Enabled = false;
            }
            else
            {
                Summary_SectionDropdown.Enabled = true;
            }
        }

        private void SummaryGenerateBtn_Click(object sender, EventArgs e)
        {
            if (Summary_CategoryDropdown.Text == "-- Select --")
            {
                MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Summary_ResultTypeDropdown.Text == "-- Select --")
            {
                MessageBox.Show("Please select result type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (Summary_CategoryDropdown.Text == "BIPH FE Details")
                {
                    SelectBIPHFEResult();
                }

                if (Summary_CategoryDropdown.Text == "Manhour")
                {
                    if (Summary_DepartmentDropdown.Text == "-- Select --")
                    {
                        MessageBox.Show("Please select department.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        SelectManhour();
                    }
                    
                }

                if (Summary_CategoryDropdown.Text == "Manhour Ratio")
                {
                    if (Summary_DepartmentDropdown.Text == "-- Select --")
                    {
                        MessageBox.Show("Please select department.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        SelectFEMHRatio();
                    }
                    
                }

                if (Summary_CategoryDropdown.Text == "Standard Manhour")
                {
                    SelectStandardManhour();
                }
            }
        }

        private void SelectBIPHFEResult()
        {
            FESummaryDatagrid.DataSource = null;

            con.Open();
            SqlCommand SelectManhour = new SqlCommand("SP_SelectBIPHFEResult", con);
            SelectManhour.CommandType = CommandType.StoredProcedure;
            SelectManhour.Parameters.AddWithValue("@FiscalYear", Summary_FiscalYearDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Month", Summary_MonthDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Category", Summary_CategoryDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@ResultType", Summary_ResultTypeDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectManhour);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FESummaryDatagrid.DataSource = dt;
            con.Close();

            CheckStatusAndFormatHeaderText();

            if (Summary_MonthDropdown.Text == "All")
            {
                if (Summary_ResultTypeDropdown.Text == "Actual + Forecast")
                {
                    FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else if (Summary_ResultTypeDropdown.Text == "Cumulative Data")
                {
                    FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    Summary_MonthDropdown.Enabled = false;
                    Summary_MonthDropdown.Text = "All";
                }
                //HideEmptyColumns(FESummaryDatagrid); //Hide empty colums
            }
            else
            {
                FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void SelectManhour()
        {
            FESummaryDatagrid.DataSource = null;

            con.Open();
            SqlCommand SelectManhour = new SqlCommand("SP_SelectManhour", con);
            SelectManhour.CommandType = CommandType.StoredProcedure;
            SelectManhour.Parameters.AddWithValue("@FiscalYear", Summary_FiscalYearDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Month", Summary_MonthDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Category", Summary_CategoryDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@ResultType", Summary_ResultTypeDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectManhour);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FESummaryDatagrid.DataSource = dt;
            con.Close();

            //formatColums();
            CheckStatusAndFormatHeaderText();

            if (Summary_MonthDropdown.Text == "All")
            {
                FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                //HideEmptyColumns(FESummaryDatagrid); //Hide empty colums
            }
            else
            {
                FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void SelectStandardManhour()
        {
            FESummaryDatagrid.DataSource = null;

            con.Open();
            SqlCommand SelectManhour = new SqlCommand("SP_SelectStandardManhour", con);
            SelectManhour.CommandType = CommandType.StoredProcedure;
            SelectManhour.Parameters.AddWithValue("@FiscalYear", Summary_FiscalYearDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Month", Summary_MonthDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Category", Summary_CategoryDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@ResultType", Summary_ResultTypeDropdown.Text);
            SelectManhour.Parameters.AddWithValue("@Section", Summary_SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectManhour);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FESummaryDatagrid.DataSource = dt;
            con.Close();

            CheckStatusAndFormatHeaderText();

            if (Summary_MonthDropdown.Text == "All")
            {
                FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                //HideEmptyColumns(FESummaryDatagrid); //Hide empty colums
            }
            else
            {
                FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        string ActualForecast = "";
        string Month = "";

        private void SelectFEMHRatio()
        {
            
            FESummaryDatagrid.DataSource = null;

            FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            con.Open();
            SqlCommand SelectFEMHRatio = new SqlCommand("SP_SelectFEMHRatio", con);
            SelectFEMHRatio.CommandType = CommandType.StoredProcedure;
            SelectFEMHRatio.Parameters.AddWithValue("@FiscalYear", Summary_FiscalYearDropdown.Text);
            SelectFEMHRatio.Parameters.AddWithValue("@Month", Summary_MonthDropdown.Text);
            SelectFEMHRatio.Parameters.AddWithValue("@Category", Summary_CategoryDropdown.Text);
            SelectFEMHRatio.Parameters.AddWithValue("@ResultType", Summary_ResultTypeDropdown.Text);
            SelectFEMHRatio.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectFEMHRatio);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FESummaryDatagrid.DataSource = dt;
            con.Close();

            CheckStatusAndFormatHeaderText();

            if (Summary_MonthDropdown.Text == "All")
            {
                if (Summary_ResultTypeDropdown.Text == "Actual + Forecast")
                {
                    FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else if (Summary_ResultTypeDropdown.Text == "Cumulative Data")
                {
                    FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    Summary_MonthDropdown.Enabled = false;
                    Summary_MonthDropdown.Text = "All";
                }
            }
            else
            {
                FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }


        private void SelectFEMHRatio_SummTab()
        {
            //ReadFEActualForescast();

            if (SummTab_CategoryDropdown.Text == "Manhour Ratio")
            {
                FEMonthlyDatagrid.DataSource = null;

                FEMonthlyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                con.Open();
                SqlCommand SelectFEMHRatioSummary = new SqlCommand("SP_SelectFEMHRatio_Summary", con);
                SelectFEMHRatioSummary.CommandType = CommandType.StoredProcedure;
                SelectFEMHRatioSummary.Parameters.AddWithValue("@FiscalYear", SummTab_FiscalYearDropdown.Text);
                SelectFEMHRatioSummary.Parameters.AddWithValue("@Month", DateTime.Now.ToString("MMM"));
                SelectFEMHRatioSummary.Parameters.AddWithValue("@Category", SummTab_CategoryDropdown.Text);
                SelectFEMHRatioSummary.Parameters.AddWithValue("@ResultType", SummTab_ResultTypeDropdown.Text);
                //SelectFEMHRatio.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectFEMHRatioSummary);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                FEMonthlyDatagrid.DataSource = dt;
                con.Close();

                //SelectCommentPerDepartment();
                SelectVariancePerDepartment();
            }

            // Freeze the second column
            FEMonthlyDatagrid.Columns[1].Frozen = true;


            //formatColums();

            //if (Summary_MonthDropdown.Text == "All")
            //{
            //    if (Summary_ResultTypeDropdown.Text == "Actual + Forecast")
            //    {
            //        FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //    }
            //    else if (Summary_ResultTypeDropdown.Text == "Cumulative Data")
            //    {
            //        FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //        Summary_MonthDropdown.Enabled = false;
            //        Summary_MonthDropdown.Text = "All";
            //    }
            //}
            //else
            //{
            //    FESummaryDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //}
        }

        private void SelectFEMHRatio_Section_SummTab()
        {
            //ReadFEActualForescast();

            if (SummTab_CategoryDropdown.Text == "Manhour Ratio")
            {
                SectionResultDatagrid.DataSource = null;

                SectionResultDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                con.Open();
                SqlCommand SelectFEMHRatioSummary = new SqlCommand("SP_SelectFEMHRatio_Section_Summary", con);
                SelectFEMHRatioSummary.CommandType = CommandType.StoredProcedure;
                SelectFEMHRatioSummary.Parameters.AddWithValue("@FiscalYear", SummTab_FiscalYearDropdown.Text);
                SelectFEMHRatioSummary.Parameters.AddWithValue("@Month", DateTime.Now.ToString("MMM"));
                SelectFEMHRatioSummary.Parameters.AddWithValue("@Category", SummTab_CategoryDropdown.Text);
                SelectFEMHRatioSummary.Parameters.AddWithValue("@ResultType", SummTab_ResultTypeDropdown.Text);
                //SelectFEMHRatio.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectFEMHRatioSummary);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SectionResultDatagrid.DataSource = dt;
                con.Close();

                //SelectCommentPerSection();
                SelectVariancePerSection();
            }

            // Freeze the second column
            SectionResultDatagrid.Columns[1].Frozen = true;
            SectionResultDatagrid.Columns[2].Frozen = true;

        }

        //private void SelectCommentPerDepartment()
        //{
        //    //CommentDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //    con.Open();
        //    SqlCommand SelectFEMHRatioSummary = new SqlCommand("SP_SelectFEMHRatioDepartmentComment", con);
        //    SelectFEMHRatioSummary.CommandType = CommandType.StoredProcedure;
        //    SelectFEMHRatioSummary.Parameters.AddWithValue("@FiscalYear", SummTab_FiscalYearDropdown.Text);
        //    SelectFEMHRatioSummary.Parameters.AddWithValue("@Month", DateTime.Now.AddMonths(-1).ToString("MMM"));
        //    //SelectFEMHRatioSummary.Parameters.AddWithValue("@Category", SummTab_CategoryDropdown.Text);
        //    SelectFEMHRatioSummary.Parameters.AddWithValue("@ResultType", SummTab_ResultTypeDropdown.Text);
        //    //SelectFEMHRatio.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
        //    SqlDataAdapter sda = new SqlDataAdapter(SelectFEMHRatioSummary);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    CommentDatagrid.DataSource = dt;
        //    con.Close();
        //}

        //private void SelectCommentPerSection()
        //{
        //    //CommentDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //    con.Open();
        //    SqlCommand SelectFEMHRatioSummary = new SqlCommand("SP_SelectFEMHRatioSectionComment", con);
        //    SelectFEMHRatioSummary.CommandType = CommandType.StoredProcedure;
        //    SelectFEMHRatioSummary.Parameters.AddWithValue("@FiscalYear", SummTab_FiscalYearDropdown.Text);
        //    SelectFEMHRatioSummary.Parameters.AddWithValue("@Month", DateTime.Now.AddMonths(-1).ToString("MMM"));
        //    //SelectFEMHRatioSummary.Parameters.AddWithValue("@Department", SummTab_CategoryDropdown.Text);
        //    SelectFEMHRatioSummary.Parameters.AddWithValue("@ResultType", SummTab_ResultTypeDropdown.Text);
        //    //SelectFEMHRatio.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
        //    SqlDataAdapter sda = new SqlDataAdapter(SelectFEMHRatioSummary);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    SectionCommentDataGrid.DataSource = dt;
        //    con.Close();
        //}

        private void SelectVariancePerDepartment()
        {
            //CommentDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            con.Open();
            SqlCommand SelectVariancePerDepartment = new SqlCommand("SP_SelectVariancePerDepartment", con);
            SelectVariancePerDepartment.CommandType = CommandType.StoredProcedure;
            SelectVariancePerDepartment.Parameters.AddWithValue("@FiscalYear", SummTab_FiscalYearDropdown.Text);
            SelectVariancePerDepartment.Parameters.AddWithValue("@Month", DateTime.Now.AddMonths(-1).ToString("MMM"));
            //SelectFEMHRatioSummary.Parameters.AddWithValue("@Department", SummTab_CategoryDropdown.Text);
            SelectVariancePerDepartment.Parameters.AddWithValue("@ResultType", SummTab_ResultTypeDropdown.Text);
            //SelectFEMHRatio.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectVariancePerDepartment);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DeptVarianceDataGrid.DataSource = dt;
            con.Close();

            // Freeze the first column
            DeptVarianceDataGrid.Columns[0].Frozen = true;

        }

        private void SelectVariancePerSection()
        {
            //CommentDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            con.Open();
            SqlCommand SelectVariancePerSection = new SqlCommand("SP_SelectVariancePerSection", con);
            SelectVariancePerSection.CommandType = CommandType.StoredProcedure;
            SelectVariancePerSection.Parameters.AddWithValue("@FiscalYear", SummTab_FiscalYearDropdown.Text);
            SelectVariancePerSection.Parameters.AddWithValue("@Month", DateTime.Now.AddMonths(-1).ToString("MMM"));
            //SelectFEMHRatioSummary.Parameters.AddWithValue("@Department", SummTab_CategoryDropdown.Text);
            SelectVariancePerSection.Parameters.AddWithValue("@ResultType", SummTab_ResultTypeDropdown.Text);
            //SelectFEMHRatio.Parameters.AddWithValue("@Department", Summary_DepartmentDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectVariancePerSection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SectionVarianceDataGrid.DataSource = dt;
            con.Close();

            // Freeze the first column
            SectionVarianceDataGrid.Columns[0].Frozen = true;

        }


        private void CheckStatusAndFormatHeaderText()
        {
            con.Open();
            SqlCommand ReadFEActualForescast = new SqlCommand("SP_ReadFEActualForescast", con);
            ReadFEActualForescast.CommandType = CommandType.StoredProcedure;
            ReadFEActualForescast.Parameters.AddWithValue("@FiscalYear", Summary_FiscalYearDropdown.Text);
            SqlDataAdapter sda2 = new SqlDataAdapter(ReadFEActualForescast);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            con.Close();

            // Now, you can call a method to update headers based on the data in dt2
            UpdateMonthHeaders(dt2);
        }

        private void UpdateMonthHeaders(DataTable dt2)
        {
            // Create a dictionary to store month and status mapping from the database
            Dictionary<string, string> monthStatus = new Dictionary<string, string>();

            // Fill the dictionary with the data from the DataTable
            foreach (DataRow row in dt2.Rows)
            {
                string month = row["Month"].ToString();
                string actualForecast = row["Actual_Forecast"].ToString();
                monthStatus[month] = actualForecast;
            }

            // Loop through each column in the DataGrid (assuming column names are the months: "Jan", "Feb", etc.)
            for (int i = 0; i < FESummaryDatagrid.Columns.Count; i++)
            {
                // Get the column name (e.g., "Jan", "Feb", etc.)
                string columnName = FESummaryDatagrid.Columns[i].HeaderText.ToString();

                // Check if the status for that month is "ACT" or "FCST"
                if (monthStatus.ContainsKey(columnName))
                {
                    string status = monthStatus[columnName];

                    // Update the header text based on the status
                    if (status == "ACT")
                    {
                        FESummaryDatagrid.Columns[i].HeaderText = columnName + " ACT";
                    }
                    else if (status == "FCST")
                    {
                        FESummaryDatagrid.Columns[i].HeaderText = columnName + " FCST";
                    }
                }
            }
        }



        private void FESummaryDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in FESummaryDatagrid.Rows)
            {
                row.Height = 40; // Set the height of each row to 50 pixels
            }

            foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

               

            }

            // Get the column name (header text) for the current column
            string columnName = FESummaryDatagrid.Columns[e.ColumnIndex].HeaderText;

            if (columnName.Contains("Target"))
            {
                e.CellStyle.BackColor = Color.FromArgb(242, 242, 242);
                //column.HeaderCell.Style.ForeColor = Color.White; // Text color for better contrast

                // Change the header background color to gray
                FESummaryDatagrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.FromArgb(242, 242, 242);
            }
            else if (columnName.Contains("ACT"))
            {
                // Change the cell's background color to blue
                e.CellStyle.BackColor = Color.Blue;
                e.CellStyle.ForeColor = Color.White; // Set the text color to white for contrast

                // Change the header background color to blue
                FESummaryDatagrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Blue;
                FESummaryDatagrid.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
            }
            else
            {
                // Reset to default cell style if the header text does not contain "ACT"
                e.CellStyle.BackColor = FESummaryDatagrid.DefaultCellStyle.BackColor;
                e.CellStyle.ForeColor = FESummaryDatagrid.DefaultCellStyle.ForeColor;
            }


            // Set the background color for cells in last column
            if (e.ColumnIndex == FESummaryDatagrid.ColumnCount - 1)
            {
                // Set the background color for cells in the desired column
                e.CellStyle.BackColor = Color.FromArgb(0, 0, 153);
                e.CellStyle.ForeColor = Color.FromArgb(255, 255, 0);

            }

        
        }

        private void FESummaryDatagrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            /*HideEmptyColumns(FESummaryDatagrid);*/ //Hide Empty columns


            

           
        }

        private void formatColums()
        {
            if (Summary_CategoryDropdown.Text == "Manhour Ratio" && Summary_ResultTypeDropdown.Text == "Actual + Forecast")
            {


                foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
                {

                    if (column.HeaderText == "Department")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else if (column.HeaderText == "Section")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else if (column.HeaderText == "Annual")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else
                    {
                        if (Month == column.HeaderText && ActualForecast == "ACT")
                        {
                            column.HeaderText = column.HeaderText + " ACT";
                        }
                        else
                        {
                            column.HeaderText = column.HeaderText + " FCST";
                        }
                    }
                }


            }
            else if (Summary_CategoryDropdown.Text == "Manhour Ratio" && Summary_ResultTypeDropdown.Text == "Cumulative Data")
            {
                foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
                {
                    if (column.HeaderText == "Department")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else if (column.HeaderText == "Section")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else
                    {
                        if (!column.HeaderText.Contains("Target"))
                        {
                            if (Month == column.HeaderText && ActualForecast == "ACT")
                            {
                                column.HeaderText = column.HeaderText + " ACT";
                            }
                            else
                            {
                                column.HeaderText = column.HeaderText + " FCST";
                            }
                        }

                    }
                }
            }

            if (Summary_CategoryDropdown.Text == "Manhour" && Summary_ResultTypeDropdown.Text == "Actual + Forecast")
            {
                foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
                {
                    if (column.HeaderText == "Department")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else if (column.HeaderText == "Section")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else
                    {
                        if (!column.HeaderText.Contains("Target"))
                        {
                            if (Month == column.HeaderText && ActualForecast == "ACT")
                            {
                                column.HeaderText = column.HeaderText + " ACT";
                            }
                            else
                            {
                                column.HeaderText = column.HeaderText + " FCST";
                            }
                        }


                    }
                }
            }
            else if (Summary_CategoryDropdown.Text == "Manhour" && Summary_ResultTypeDropdown.Text == "Cumulative Data")
            {
                foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
                {
                    if (column.HeaderText == "Section")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else
                    {
                        if (!column.HeaderText.Contains("Target"))
                        {
                            if (Month == column.HeaderText && ActualForecast == "ACT")
                            {
                                column.HeaderText = column.HeaderText + " ACT";
                            }
                            else
                            {
                                column.HeaderText = column.HeaderText + " FCST";
                            }
                        }


                    }
                }
            }

            if (Summary_CategoryDropdown.Text == "Standard Manhour" && Summary_ResultTypeDropdown.Text == "Actual + Forecast")
            {
                foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
                {
                    if (column.HeaderText == "Section")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else if (column.HeaderText == "Costcenter")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else
                    {
                        if (!column.HeaderText.Contains("Target"))
                        {
                            if (Month == column.HeaderText && ActualForecast == "ACT")
                            {
                                column.HeaderText = column.HeaderText + " ACT";
                            }
                            else
                            {
                                column.HeaderText = column.HeaderText + " FCST";
                            }
                        }


                    }
                }
            }
            else if (Summary_CategoryDropdown.Text == "Standard Manhour" && Summary_ResultTypeDropdown.Text == "Cumulative Data")
            {
                foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
                {
                    if (column.HeaderText == "Section")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else if (column.HeaderText == "Costcenter")
                    {
                        column.HeaderText = column.HeaderText;
                    }
                    else
                    {
                        if (!column.HeaderText.Contains("Target"))
                        {
                            if (Month == column.HeaderText && ActualForecast == "ACT")
                            {
                                column.HeaderText = column.HeaderText + " ACT";
                            }
                            else
                            {
                                column.HeaderText = column.HeaderText + " FCST";
                            }
                        }
                    }
                }
            }

            if (Summary_CategoryDropdown.Text == "BIPH FE Details" && Summary_ResultTypeDropdown.Text == "Actual + Forecast")
            {
                foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
                {
                    if (!column.HeaderText.Contains("Target"))
                    {
                        if (Month == column.HeaderText && ActualForecast == "ACT")
                        {
                            column.HeaderText = column.HeaderText + " ACT";
                        }
                        else
                        {
                            column.HeaderText = column.HeaderText + " FCST";
                        }
                    }
                }
            }
            else if (Summary_CategoryDropdown.Text == "BIPH FE Details" && Summary_ResultTypeDropdown.Text == "Cumulative Data")
            {
                foreach (DataGridViewColumn column in FESummaryDatagrid.Columns)
                {
                    if (!column.HeaderText.Contains("Target"))
                    {
                        if (Month == column.HeaderText && ActualForecast == "ACT")
                        {
                            column.HeaderText = column.HeaderText + " ACT";
                        }
                        else
                        {
                            column.HeaderText = column.HeaderText + " FCST";
                        }
                    }
                }
            }
        }

        //private void IsColumnEmpty(DataGridView dataGridView)
        //{
        //    foreach (DataGridViewColumn column in dataGridView.Columns)
        //    {
        //        foreach (DataGridViewRow row in dataGridView.Rows)
        //        {
        //            if (row.Cells[column.Index].Value != null && !string.IsNullOrWhiteSpace(row.Cells[column.Index].Value.ToString()))
        //            {
        //                if (!column.HeaderText.Contains("Target"))
        //                {
        //                    column.HeaderText = column.HeaderText + " ACT";
        //                }
        //            }
        //        }

        //        column.HeaderText = column.HeaderText + " FCST";
        //    }
        //}

        private void HideEmptyColumns(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                bool hasData = false;

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[column.Index].Value != null && !string.IsNullOrWhiteSpace(row.Cells[column.Index].Value.ToString()))
                    {
                        hasData = true;
                        break;
                    }
                }

                column.Visible = hasData;
            }
        }

        private void copyAlltoClipboardsss()
        {

            FESummaryDatagrid.SelectAll();

            //Copy to clipboard
            FESummaryDatagrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = FESummaryDatagrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void SummaryExportBtn_Click(object sender, EventArgs e)
        {
            if (FESummaryDatagrid.DataSource == null)
            {
                MessageBox.Show("No data to export, please generate data first.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";
                System.IO.Directory.CreateDirectory(pathsss);

                copyAlltoClipboardsss();
                Microsoft.Office.Interop.Excel.Application xlexcel;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlexcel = new Microsoft.Office.Interop.Excel.Application();
                xlexcel.Visible = true;
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.Cells.NumberFormat = "@";

                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                xlWorkSheet.Columns.AutoFit();

                MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void Summary_CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Summary_CategoryDropdown.Text == "Standard Manhour" || Summary_CategoryDropdown.Text == "BIPH FE Details")
            {
                Summary_DepartmentDropdown.Enabled = false;
                Summary_SectionDropdown.Enabled = false;
            }
            else
            {
                Summary_DepartmentDropdown.Enabled = true;
                Summary_SectionDropdown.Enabled = true;
            }
        }

        private void FESummGenBtn_Click(object sender, EventArgs e)
        {
            if (SummTab_CategoryDropdown.Text == "-- Select --")
            {
                MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (SummTab_ResultTypeDropdown.Text == "-- Select --")
            {
                MessageBox.Show("Please select result type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (SummTab_CategoryDropdown.Text == "All")
                {
                   
                }

                if (SummTab_CategoryDropdown.Text == "Manhour")
                {
                    if (Summary_DepartmentDropdown.Text == "-- Select --")
                    {
                        MessageBox.Show("Please select department.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        SelectManhour();
                    }
                }

                if (SummTab_CategoryDropdown.Text == "Manhour Ratio")
                {
                    SelectFEMHRatio_SummTab();
                    SelectFEMHRatio_Section_SummTab();
                }

            }
        }

        private void FEMonthlyDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in FEMonthlyDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            foreach (DataGridViewRow row in FEMonthlyDatagrid.Rows)
            {
                row.Height = 40; // Set the height of each row to 50 pixels
            }

            // Get the header text of the column
            string headerText = FEMonthlyDatagrid.Columns[e.ColumnIndex].HeaderText;

            // Check if the header text contains the word "target" (case-insensitive)
            if (headerText.IndexOf("target", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                e.CellStyle.BackColor = Color.FromArgb(242, 242, 242);
            }

            // Ensure that the column being formatted is the 'actual' column (e.g., 'Apr', 'May', etc.)
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                // Get the name of the column (e.g., "Apr", "May", etc.)
                string columnName = FEMonthlyDatagrid.Columns[e.ColumnIndex].Name;

                // Check if we have a column name in the "Apr", "May" format (column names for actual data)
                if (columnName.Length == 3)  // assuming column names like "Apr", "May", etc.
                {
                    // Construct the target column name (e.g., "Apr Target", "May Target")
                    string targetColumnName = columnName + " Target";


                    // Ensure there is a corresponding 'Target' column
                    if (FEMonthlyDatagrid.Columns.Contains(targetColumnName))
                    {
                        // Get the values of the target column and the actual column
                        var targetValue = FEMonthlyDatagrid.Rows[e.RowIndex].Cells[targetColumnName].Value;
                        var actualValue = FEMonthlyDatagrid.Rows[e.RowIndex].Cells[columnName].Value;

                        // Check if the values are not DBNull
                        if (targetValue != DBNull.Value && actualValue != DBNull.Value)
                        {
                            // Convert both values to strings and remove the '%' character
                            string targetValueStr = targetValue.ToString().Replace("%", "").Trim();
                            string actualValueStr = actualValue.ToString().Replace("%", "").Trim();

                            // Try to convert the cleaned strings to decimal
                            if (decimal.TryParse(targetValueStr, out decimal targetDecimal) &&
                                decimal.TryParse(actualValueStr, out decimal actualDecimal))
                            {
                                // Check if the actual value is less than the target value
                                if (actualDecimal <= targetDecimal)
                                {
                                    // Reset the background color to default
                                    e.CellStyle.BackColor = Color.Blue;
                                    e.CellStyle.ForeColor = Color.White;
                                }
                                else
                                {
                                    // Change the background color of the actual value cell
                                    e.CellStyle.BackColor = Color.Red;  // You can use any color here
                                    e.CellStyle.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            FactoryEfficiencyMGRCommentForm FactoryEfficiencyMGRCommentForm = new FactoryEfficiencyMGRCommentForm();
            FactoryEfficiencyMGRCommentForm.ShowDialog();
        }

        private void FEMonthlyDatagrid_DoubleClick(object sender, EventArgs e)
        {

        }

        public static string Dept = "";
        public static string Section = "";
        public static string ResultType = "";
        public static string FiscalYear = "";

        private void FEMonthlyDatagrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Ensure the click is on a valid button cell
            if (e.RowIndex >= 0 && e.ColumnIndex == FEMonthlyDatagrid.Columns["Comment"].Index)
            {
                Dept = FEMonthlyDatagrid.Rows[e.RowIndex].Cells["Department"].Value.ToString();
                ResultType = SummTab_ResultTypeDropdown.Text;
                FiscalYear = SummTab_FiscalYearDropdown.Text;

                FactoryEfficiencyMGRCommentForm FactoryEfficiencyMGRCommentForm = new FactoryEfficiencyMGRCommentForm();
                FactoryEfficiencyMGRCommentForm.ShowDialog();
            }
        }

        //private void CommentDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    foreach (DataGridViewColumn column in CommentDatagrid.Columns)
        //    {
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //    }

        //    foreach (DataGridViewRow row in CommentDatagrid.Rows)
        //    {
        //        row.Height = 40; // Set the height of each row to 50 pixels
        //    }
        //}

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        public static bool IsCommentSave = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsCommentSave == true)
            {
                if (isSectionComment == true)
                {
                    //SelectCommentPerSection();
                    SelectVariancePerSection();
                    isSectionComment = false;
                }
                else
                {
                    //SelectCommentPerDepartment();
                    SelectVariancePerDepartment();
                }
                
                IsCommentSave = false;
            }
            else { }
        }

        private void FESummaryDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void copyAlltoClipboardssss()
        {

            FEMonthlyDatagrid.SelectAll();

            //Copy to clipboard
            FEMonthlyDatagrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = FEMonthlyDatagrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void FESummaryExportBtn_Click(object sender, EventArgs e)
        {
            if (FEMonthlyDatagrid.DataSource == null)
            {
                MessageBox.Show("No data to export, please generate data first.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";
                System.IO.Directory.CreateDirectory(pathsss);

                copyAlltoClipboardssss();
                Microsoft.Office.Interop.Excel.Application xlexcel;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlexcel = new Microsoft.Office.Interop.Excel.Application();
                xlexcel.Visible = true;
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.Cells.NumberFormat = "@";

                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                xlWorkSheet.Columns.AutoFit();

                MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private static bool isSectionComment = false;
        private void SectionResultDatagrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Ensure the click is on a valid button cell
            if (e.RowIndex >= 0 && e.ColumnIndex == FEMonthlyDatagrid.Columns["Comment"].Index)
            {
                Dept = SectionResultDatagrid.Rows[e.RowIndex].Cells["Department"].Value.ToString();
                Section = SectionResultDatagrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                ResultType = SummTab_ResultTypeDropdown.Text;
                isSectionComment = true;
                FiscalYear = SummTab_FiscalYearDropdown.Text;

                FactoryEfficiencyMGRCommentForm FactoryEfficiencyMGRCommentForm = new FactoryEfficiencyMGRCommentForm();
                FactoryEfficiencyMGRCommentForm.ShowDialog();
            }
        }

        private void WorkingDaysHoursBtn_Click(object sender, EventArgs e)
        {
            UpdateWorkingHours UpdateWorkingHours = new UpdateWorkingHours();
            UpdateWorkingHours.ShowDialog();
        }

        private void DeptVarianceDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in DeptVarianceDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in DeptVarianceDataGrid.Rows)
            {
                row.Height = 40; // Set the height of each row to 50 pixels
            }

            // Ensure the row and column are valid
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the value of the cell (ensure it can be converted to decimal)
                var cellValue = DeptVarianceDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                // Check if the cell contains a valid numeric value
                if (cellValue != DBNull.Value && decimal.TryParse(cellValue.ToString(), out decimal numericValue))
                {
                    // If the value is negative, set the background color to blue
                    if (numericValue < 0)
                    {
                        e.CellStyle.BackColor = Color.Blue;
                        e.CellStyle.ForeColor = Color.White; // Optionally change text color for better contrast
                    }
                    else
                    {
                        // If the value is not negative, set the background color to red
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White; // Optionally change text color
                    }
                }
            }
        }

        private void SectionResultDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in SectionResultDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in SectionResultDatagrid.Rows)
            {
                row.Height = 40; // Set the height of each row to 50 pixels
            }

            // Get the header text of the column
            string headerText = SectionResultDatagrid.Columns[e.ColumnIndex].HeaderText;

            // Check if the header text contains the word "target" (case-insensitive)
            if (headerText.IndexOf("target", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                e.CellStyle.BackColor = Color.FromArgb(242, 242, 242);
            }

            // Ensure that the column being formatted is the 'actual' column (e.g., 'Apr', 'May', etc.)
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                // Get the name of the column (e.g., "Apr", "May", etc.)
                string columnName = SectionResultDatagrid.Columns[e.ColumnIndex].Name;

                // Check if we have a column name in the "Apr", "May" format (column names for actual data)
                if (columnName.Length == 3)  // assuming column names like "Apr", "May", etc.
                {
                    // Construct the target column name (e.g., "Apr Target", "May Target")
                    string targetColumnName = columnName + " Target";


                    // Ensure there is a corresponding 'Target' column
                    if (SectionResultDatagrid.Columns.Contains(targetColumnName))
                    {
                        // Get the values of the target column and the actual column
                        var targetValue = SectionResultDatagrid.Rows[e.RowIndex].Cells[targetColumnName].Value;
                        var actualValue = SectionResultDatagrid.Rows[e.RowIndex].Cells[columnName].Value;

                        // Check if the values are not DBNull
                        if (targetValue != DBNull.Value && actualValue != DBNull.Value)
                        {
                            // Convert both values to strings and remove the '%' character
                            string targetValueStr = targetValue.ToString().Replace("%", "").Trim();
                            string actualValueStr = actualValue.ToString().Replace("%", "").Trim();

                            // Try to convert the cleaned strings to decimal
                            if (decimal.TryParse(targetValueStr, out decimal targetDecimal) &&
                                decimal.TryParse(actualValueStr, out decimal actualDecimal))
                            {
                                // Check if the actual value is less than the target value
                                if (actualDecimal <= targetDecimal)
                                {
                                    // Reset the background color to default
                                    e.CellStyle.BackColor = Color.Blue;
                                    e.CellStyle.ForeColor = Color.White;
                                }
                                else
                                {
                                    // Change the background color of the actual value cell
                                    e.CellStyle.BackColor = Color.Red;  // You can use any color here
                                    e.CellStyle.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SectionVarianceDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in SectionVarianceDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow row in SectionVarianceDataGrid.Rows)
            {
                row.Height = 40; // Set the height of each row to 40 pixels
            }

            // Ensure the row and column are valid
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the value of the cell (ensure it can be converted to decimal)
                var cellValue = SectionVarianceDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                // Check if the cell contains a valid numeric value
                if (cellValue != DBNull.Value && decimal.TryParse(cellValue.ToString(), out decimal numericValue))
                {
                    // If the value is negative, set the background color to blue
                    if (numericValue < 0)
                    {
                        e.CellStyle.BackColor = Color.Blue;
                        e.CellStyle.ForeColor = Color.White; // Optionally change text color for better contrast
                    }
                    else
                    {
                        // If the value is not negative, set the background color to red
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White; // Optionally change text color
                    }
                }
            }
        }

        private void SimulationGraphBtn_Click(object sender, EventArgs e)
        {
            FactoryEfficiencyGraphForm FactoryEfficiencyGraphForm = new FactoryEfficiencyGraphForm();
            FactoryEfficiencyGraphForm.ShowDialog();

        }

        private void SummaryGraphBtn_Click(object sender, EventArgs e)
        {

        }

        private void SummTab_CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SummTab_ResultTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SectionVarianceDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
