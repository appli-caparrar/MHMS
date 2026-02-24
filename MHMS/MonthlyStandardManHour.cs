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
    public partial class MonthlyStandardManHour : Form
    {

        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public MonthlyStandardManHour()
        {
            InitializeComponent();
        }

        private void MonthlyStandardMHTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        private void MonthlyStandardManHour_Load(object sender, EventArgs e)
        {
            SectionLabel.Text = COPQManhourLossForm.SelectedSection;

            LoadMHLossRateData();

            AddYears();

            //Hide column ID from datagrid
            StandardMHDataGrid.Columns["ID"].Visible = false;

            //Change specific column name in datagrid view
            StandardMHDataGrid.Columns[3].HeaderText = "Updated By";

            //Change column header name 
            StandardMHDataGrid.Columns["Man-Hour"].HeaderText = "Standard MH";
        }

        //Load Section in combobox
        //public void LoadSection()
        //{
        //    // Check Connection status -> Open the connection if the connection is closed
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    // -> SQL query to select User Account
        //    SqlCommand LoadSection = new SqlCommand("SP_LoadSection", con);
        //    LoadSection.CommandType = CommandType.StoredProcedure;
        //    LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllProdSections");
        //    SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);
        //    LoadSection.ExecuteNonQuery();
        //    con.Close();

        //    SectionDropdown.DataSource = ds.Tables[0];
        //    SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
        //    SectionDropdown.ValueMember = "Section";
        //}// <---- end


        private void LoadMHLossRateData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select ink cartridge parts loss data
            SqlCommand SelectMHLossRateData = new SqlCommand("SP_SelectMHLossRateData", con);
            SelectMHLossRateData.CommandType = CommandType.StoredProcedure;
            //SelectMHLossRateData.Parameters.AddWithValue("@Section", SectionLabel.Text);
            SelectMHLossRateData.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossRateData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StandardMHDataGrid.DataSource = dt;
            con.Close();
        }

        private void AddYears()
        {
            var currentYear = DateTime.Today.Year;
            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                FiscalYearDropdown.Items.Add((currentYear - i).ToString());
            }

            //var currentYear = DateTime.Today.Year - 1;

            //for (int i = 1; i <= 78; i++)
            //{
            //    // Now just add an entry that's the current year minus the counter
            //    FiscalYearDropdown.Items.Add((currentYear + i).ToString());
            //}
        }

        private void StandardMHDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //disable sorting a column in datagrid
            foreach (DataGridViewColumn column in StandardMHDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Set specific width on datagrid column
            StandardMHDataGrid.Columns["Month"].Width = 120;
            StandardMHDataGrid.Columns["Man-Hour"].Width = 100;

            //Set column name
            this.StandardMHDataGrid.Columns[3].HeaderText = "Updated By";
        }

        string ID;
        private void StandardMHDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = StandardMHDataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            Month.Text = StandardMHDataGrid.Rows[e.RowIndex].Cells["Month"].Value.ToString();
            Manhour.Text = StandardMHDataGrid.Rows[e.RowIndex].Cells["Man-Hour"].Value.ToString();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Month.Text == "")
            {
                MessageBox.Show("Please select month", "Remiders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Manhour.Text == "")
            {
                MessageBox.Show("Please type manhour", "Remiders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // ---> Update query
                SqlCommand UpdateStandardMH = new SqlCommand("SP_UpdateMonthlyStandardMH", con);
                UpdateStandardMH.CommandType = CommandType.StoredProcedure;
                UpdateStandardMH.Parameters.AddWithValue("@Month", Month.Text);
                UpdateStandardMH.Parameters.AddWithValue("@Manhour", Manhour.Text);
                UpdateStandardMH.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                UpdateStandardMH.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                UpdateStandardMH.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
                UpdateStandardMH.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Standard MH for the month of " + Month.Text + " was updated successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMHLossRateData();

                Month.Clear();
                Manhour.Clear();

            }
        }

        private void FiscalYearDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectDisposalBudgetDataBasedOnFiscalYear = new SqlCommand("SP_SelectStandardMHBasedOnFilcalYear", con);
            SelectDisposalBudgetDataBasedOnFiscalYear.CommandType = CommandType.StoredProcedure;
            SelectDisposalBudgetDataBasedOnFiscalYear.Parameters.AddWithValue("@Section", SectionLabel.Text.Replace("BIPH-", ""));
            SelectDisposalBudgetDataBasedOnFiscalYear.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectDisposalBudgetDataBasedOnFiscalYear);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StandardMHDataGrid.DataSource = dt;
            con.Close();
        }
    }
}
