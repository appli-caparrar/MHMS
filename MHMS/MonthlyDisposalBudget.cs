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

namespace MHMS
{
    public partial class MonthlyDisposalBudget : Form
    {

        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public MonthlyDisposalBudget()
        {
            InitializeComponent();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void MonthlyDisposalBudget_Load(object sender, EventArgs e)
        {
            FiscalYearDropdown.Text = DateTime.Now.Year.ToString();
            SectionLabel.Text = Dashboard.SectionText;

            LoadDisposalBudget();

            AddYears();

            //Hide column ID from datagrid
            DisposalBudgetDataGrid.Columns["ID"].Visible = false;

            //Change specific column name in datagrid view
            DisposalBudgetDataGrid.Columns[3].HeaderText = "Updated By";
        }

        //====================================================================================================================>>>>>>>>>>>>

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

        //===================================================================================================================>>>>>>>>>>>>
        private void LoadDisposalBudget()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

       
            SqlCommand SelectDisposalBudgetData = new SqlCommand("SP_SelectDisposalBudget", con);
            SelectDisposalBudgetData.CommandType = CommandType.StoredProcedure;
            SelectDisposalBudgetData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SelectDisposalBudgetData.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectDisposalBudgetData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DisposalBudgetDataGrid.DataSource = dt;
            con.Close();
        }

        //===================================================================================================================>>>>>>>>>>>>

        string ID;
        private void DisposalBudgetDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = DisposalBudgetDataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            Month.Text = DisposalBudgetDataGrid.Rows[e.RowIndex].Cells["Month"].Value.ToString();
            Budget.Text = DisposalBudgetDataGrid.Rows[e.RowIndex].Cells["Budget"].Value.ToString();
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void DisposalBudgetDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //disable sorting a column in datagrid
            foreach (DataGridViewColumn column in DisposalBudgetDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Set specific width on datagrid column
            DisposalBudgetDataGrid.Columns["Month"].Width = 120;
            DisposalBudgetDataGrid.Columns["Budget"].Width = 100;

            //Set column name
            this.DisposalBudgetDataGrid.Columns[3].HeaderText = "Updated By";

        }

        //==================================================================================================================>>>>>>>>>>>>

        private void SaveButton_Click(object sender, EventArgs e)
        {
            UpdateDisposalBudget();
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void UpdateDisposalBudget()
        {

            if (Month.Text == "")
            {
                MessageBox.Show("Please select month", "Remiders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Budget.Text == "")
            {
                MessageBox.Show("Please type monthly budget", "Remiders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // ---> Update query
                SqlCommand UpdateDisposalBudget = new SqlCommand("SP_UpdateDisposalBudget", con);
                UpdateDisposalBudget.CommandType = CommandType.StoredProcedure;
                UpdateDisposalBudget.Parameters.AddWithValue("@ID", ID);
                UpdateDisposalBudget.Parameters.AddWithValue("@Budget", Budget.Text);
                UpdateDisposalBudget.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                UpdateDisposalBudget.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                UpdateDisposalBudget.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Disposal budget for the month of " + Month.Text + " was updated successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDisposalBudget();

                Month.Clear();
                Budget.Clear();

            }
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void MonthlyDisposalBudgetTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void FiscalYearDropdown_TextChanged(object sender, EventArgs e)
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectDisposalBudgetBasedOnFilcalYear = new SqlCommand("SP_SelectDisposalBudgetBasedOnFilcalYear", con);
            SelectDisposalBudgetBasedOnFilcalYear.CommandType = CommandType.StoredProcedure;
            SelectDisposalBudgetBasedOnFilcalYear.Parameters.AddWithValue("@Section", SectionLabel.Text.Replace("BIPH-", ""));
            SelectDisposalBudgetBasedOnFilcalYear.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectDisposalBudgetBasedOnFilcalYear);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DisposalBudgetDataGrid.DataSource = dt;
            con.Close();
        }

        private void FiscalYearDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //====================================================================================================================>>>>>>>>>>>>
    }
}
