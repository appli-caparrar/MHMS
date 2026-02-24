using MHMS.Connection;
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
    public partial class ProductionEfficiencyForm : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);


        public ProductionEfficiencyForm()
        {
            InitializeComponent();
        }

        private void EfficiencyForm_Load(object sender, EventArgs e)
        {
            LoadSection();

            TTEfficiencyDateFrom();
            TTEfficiencyDateTo();

            DirectEfficiencyDateFrom();
            DirectEfficiencyDateTo();

            SemiDirectRate_DateFrom();
            SemiDirectRate_DateTo();

            TotalLossRate_DateFrom();
            TotalLossRate_DateTo();

            ////Load Efficiency Summary Data
            //SelectTotalEfficiencyData();
            //SelectDirectEfficiencyData();
            //SelectSemiDirectRateData();
            //SelectTotalLossRateData();

            //This codition is to disabled the printer text when user section is not printer
            if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                TotalEfficiencyPrinterDropdown.Enabled = true;
                DirectEfficiencyPrinterDropdown.Enabled = true;
                SemiDirectPrinterDropdown.Enabled = true;
                TotalLossRatePrinterDropdown.Enabled = true;
            }
            else
            {
                TotalEfficiencyPrinterDropdown.Enabled = false;
                DirectEfficiencyPrinterDropdown.Enabled = false;
                SemiDirectPrinterDropdown.Enabled = false;
                TotalLossRatePrinterDropdown.Enabled = false;
            }

            AddSection(); //Add item lists to section/dept dropdown

            TotalEfficiencyPrinterDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");


            //This category dropdown selection enable property set to false as default
            TotalEfficiencyCategoryDropdown.Enabled = false;
            DirectEfficiencyCategoryDropdown.Enabled = false;
            SemiDirectCatedoryDropdown.Enabled = false;
            TotalLossRateCategory.Enabled = false;

            Page1.Dock = DockStyle.Fill;
            Page1.Visible = true;
            Page2.Dock = DockStyle.Fill;
            Page2.Visible = false;

            
        }

        private void AddSection()
        {
            if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                //Total Efficiency
                TE_SectionDeptDropdown.Items.Add(Dashboard.SectionText.Replace("BIPH-", ""));
                //TE_SectionDeptDropdown.Items.Add("PRT Mini");
                //TE_SectionDeptDropdown.Items.Add("PRT A3");
                TE_SectionDeptDropdown.Items.Add("Production Department");
                TE_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
               
                //Direct Efficiency
                //---------------------------------------------------------------------------
                DE_SectionDeptDropdown.Items.Add(Dashboard.SectionText.Replace("BIPH-", ""));
                //DE_SectionDeptDropdown.Items.Add("PRT Mini");
                //DE_SectionDeptDropdown.Items.Add("PRT A3");
                DE_SectionDeptDropdown.Items.Add("Production Department");
                DE_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");

                //Semi-direct Rate
                //---------------------------------------------------------------------------
                SDR_SectionDeptDropdown.Items.Add(Dashboard.SectionText.Replace("BIPH-", ""));
                //SDR_SectionDeptDropdown.Items.Add("PRT Mini");
                //SDR_SectionDeptDropdown.Items.Add("PRT A3");
                SDR_SectionDeptDropdown.Items.Add("Production Department");
                SDR_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");

                //Total Loss Rate
                //---------------------------------------------------------------------------
                TLR_SectionDeptDropdown.Items.Add(Dashboard.SectionText.Replace("BIPH-", ""));
                //TLR_SectionDeptDropdown.Items.Add("PRT Mini");
                //TLR_SectionDeptDropdown.Items.Add("PRT A3");
                TLR_SectionDeptDropdown.Items.Add("Production Department");
                TLR_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            }
            else
            {
                //Total Efficiency
                TE_SectionDeptDropdown.Items.Add(Dashboard.SectionText.Replace("BIPH-", ""));
                TE_SectionDeptDropdown.Items.Add("Production Department");
                TE_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");

                //Direct Efficiency
                //---------------------------------------------------------------------------
                DE_SectionDeptDropdown.Items.Add(Dashboard.SectionText.Replace("BIPH-", ""));
                DE_SectionDeptDropdown.Items.Add("Production Department");
                DE_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");

                //Semi-direct Rate
                //---------------------------------------------------------------------------
                SDR_SectionDeptDropdown.Items.Add(Dashboard.SectionText.Replace("BIPH-", ""));
                SDR_SectionDeptDropdown.Items.Add("Production Department");
                SDR_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");

                //Total Loss Rate
                //---------------------------------------------------------------------------
                TLR_SectionDeptDropdown.Items.Add(Dashboard.SectionText.Replace("BIPH-", ""));
                TLR_SectionDeptDropdown.Items.Add("Production Department");
                TLR_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            }
          


        }

        // ---> Set the datetime picker value to first day of the current month
        private void TTEfficiencyDateFrom()
        {
            DateTime now = DateTime.Now;
            TotalEffDateFrom.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        private void TTEfficiencyDateTo()
        {
            DateTime datenow = DateTime.Now;
            TotalEffDateTo.Value = datenow;
        }// <---- end

        private void DirectEfficiencyDateFrom()
        {
            DateTime now = DateTime.Now;
            DirectEffDateFrom.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        private void DirectEfficiencyDateTo()
        {
            DateTime datenow = DateTime.Now;
            DirectEffDateTo.Value = datenow;
        }// <---- end

        private void SemiDirectRate_DateFrom()
        {
            DateTime now = DateTime.Now;
            SemiDirectRateDateFrom.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        private void SemiDirectRate_DateTo()
        {
            DateTime datenow = DateTime.Now;
            SemiDirectRateDateTo.Value = datenow;
        }// <---- end

        private void TotalLossRate_DateFrom()
        {
            DateTime now = DateTime.Now;
            TotalLossRateDateFrom.Value = new DateTime(now.Year, now.Month, 1);
        }// <---- end

        private void TotalLossRate_DateTo()
        {
            DateTime datenow = DateTime.Now;
            TotalLossRateDateTo.Value = datenow;
        }// <---- end


        private void SelectTotalEfficiencyData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectTotalEfficiencyData = new SqlCommand("SP_SelectEfficiencyDataSummary", con);
            SelectTotalEfficiencyData.CommandType = CommandType.StoredProcedure;
            SelectTotalEfficiencyData.Parameters.AddWithValue("@Procedure", "Total Efficiency");
            SelectTotalEfficiencyData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectTotalEfficiencyData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TotalEffDatagrid.DataSource = dt;
            con.Close();
        }

        private void SelectDirectEfficiencyData()
        {

            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectDirectEfficiencyData = new SqlCommand("SP_SelectEfficiencyDataSummary", con);
            SelectDirectEfficiencyData.CommandType = CommandType.StoredProcedure;
            SelectDirectEfficiencyData.Parameters.AddWithValue("@Procedure", "Direct Efficiency");
            SelectDirectEfficiencyData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectDirectEfficiencyData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DirectEffDatagrid.DataSource = dt;
            con.Close();

        }

        private void SelectSemiDirectRateData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectSemiDirectRateData = new SqlCommand("SP_SelectEfficiencyDataSummary", con);
            SelectSemiDirectRateData.CommandType = CommandType.StoredProcedure;
            SelectSemiDirectRateData.Parameters.AddWithValue("@Procedure", "Semi-Direct Rate");
            SelectSemiDirectRateData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectSemiDirectRateData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SemiDirectRateDatagrid.DataSource = dt;
            con.Close();
        }

        private void SelectTotalLossRateData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectTotalLossRateData = new SqlCommand("SP_SelectEfficiencyDataSummary", con);
            SelectTotalLossRateData.CommandType = CommandType.StoredProcedure;
            SelectTotalLossRateData.Parameters.AddWithValue("@Procedure", "Total Loss Rate");
            SelectTotalLossRateData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectTotalLossRateData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TTLossRateDatagrid.DataSource = dt;
            con.Close();

        }

        private void TotalEffDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in TotalEffDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

                foreach (DataGridViewRow row in TotalEffDatagrid.Rows)
                {
                    row.Height = 40; // Set the height of each row to 50 pixels
                }
            }


            TotalEffDatagrid.Columns[0].HeaderCell.Style.BackColor = Color.Blue;
            TotalEffDatagrid.Columns[0].HeaderCell.Style.ForeColor = Color.White;

            // Get the current month
            string currentMonth = DateTime.Now.ToString("MMM");

            // List of months
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            // Iterate over each month and apply style
            foreach (string month in months)
            {
                if (currentMonth == month)
                {
                    // Apply Navy background and white text color for the current month
                    TotalEffDatagrid.Columns[month].HeaderCell.Style.BackColor = Color.Navy;
                    TotalEffDatagrid.Columns[month].HeaderCell.Style.ForeColor = Color.White;
                }
                else
                {
                    // Reset the other months to transparent background and black text color
                    TotalEffDatagrid.Columns[month].HeaderCell.Style.BackColor = Color.Transparent;
                    TotalEffDatagrid.Columns[month].HeaderCell.Style.ForeColor = Color.Black;
                }
            }


            TotalEffDatagrid.EnableHeadersVisualStyles = false;

        }

        private void DirectEffDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            foreach (DataGridViewColumn column in DirectEffDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //------------------------------------------------------------------

            DirectEffDatagrid.Columns[0].HeaderCell.Style.BackColor = Color.Blue;
            DirectEffDatagrid.Columns[0].HeaderCell.Style.ForeColor = Color.White;

            // Get the current month
            string currentMonth = DateTime.Now.ToString("MMM");

            // List of months
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            // Iterate over each month and apply style
            foreach (string month in months)
            {
                if (currentMonth == month)
                {
                    // Apply Navy background and white text color for the current month
                    DirectEffDatagrid.Columns[month].HeaderCell.Style.BackColor = Color.Navy;
                    DirectEffDatagrid.Columns[month].HeaderCell.Style.ForeColor = Color.White;
                }
                else
                {
                    // Reset the other months to transparent background and black text color
                    DirectEffDatagrid.Columns[month].HeaderCell.Style.BackColor = Color.Transparent;
                    DirectEffDatagrid.Columns[month].HeaderCell.Style.ForeColor = Color.Black;
                }
            }

            DirectEffDatagrid.EnableHeadersVisualStyles = false;
        }

        private void SemiDirectRateDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            foreach (DataGridViewColumn column in SemiDirectRateDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            SemiDirectRateDatagrid.Columns[0].HeaderCell.Style.BackColor = Color.Blue;
            SemiDirectRateDatagrid.Columns[0].HeaderCell.Style.ForeColor = Color.White;

            // Get the current month
            string currentMonth = DateTime.Now.ToString("MMM");

            // List of months
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            // Iterate over each month and apply style
            foreach (string month in months)
            {
                if (currentMonth == month)
                {
                    // Apply Navy background and white text color for the current month
                    SemiDirectRateDatagrid.Columns[month].HeaderCell.Style.BackColor = Color.Navy;
                    SemiDirectRateDatagrid.Columns[month].HeaderCell.Style.ForeColor = Color.White;
                }
                else
                {
                    // Reset the other months to transparent background and black text color
                    SemiDirectRateDatagrid.Columns[month].HeaderCell.Style.BackColor = Color.Transparent;
                    SemiDirectRateDatagrid.Columns[month].HeaderCell.Style.ForeColor = Color.Black;
                }
            }

            SemiDirectRateDatagrid.EnableHeadersVisualStyles = false;

        }


        private void TTLossRateDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in TTLossRateDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            TTLossRateDatagrid.EnableHeadersVisualStyles = false;
            TTLossRateDatagrid.Columns[0].HeaderCell.Style.BackColor = Color.Blue;
            TTLossRateDatagrid.Columns[0].HeaderCell.Style.ForeColor = Color.White;

            // Get the current month
            string currentMonth = DateTime.Now.ToString("MMM");

            // List of months
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            // Iterate over each month and apply style
            foreach (string month in months)
            {
                if (currentMonth == month)
                {
                    // Apply Navy background and white text color for the current month
                    TTLossRateDatagrid.Columns[month].HeaderCell.Style.BackColor = Color.Navy;
                    TTLossRateDatagrid.Columns[month].HeaderCell.Style.ForeColor = Color.White;
                }
                else
                {
                    // Reset the other months to transparent background and black text color
                    TTLossRateDatagrid.Columns[month].HeaderCell.Style.BackColor = Color.Transparent;
                    TTLossRateDatagrid.Columns[month].HeaderCell.Style.ForeColor = Color.Black;
                }
            }
        }

        // Create a method to handle the comparison and cell color change for any column
        private void ChangeCellColor(DataGridView dataGridName, int rowIndex1, int rowIndex2, int rowIndex3, int rowToUpdate, int columnIndex)
        {
            // Extract and convert the values to decimal
            decimal value1 = Convert.ToDecimal(dataGridName.Rows[rowIndex1].Cells[columnIndex].Value.ToString().Replace("%", ""));
            decimal value2 = Convert.ToDecimal(dataGridName.Rows[rowIndex2].Cells[columnIndex].Value.ToString().Replace("%", ""));
            decimal valueToUpdate = Convert.ToDecimal(dataGridName.Rows[rowToUpdate].Cells[columnIndex].Value.ToString().Replace("%", ""));

            if (dataGridName == TotalEffDatagrid || dataGridName == DirectEffDatagrid)
            {
                // Apply colors based on comparison
                if (valueToUpdate > value1 && valueToUpdate > value2)
                {
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.BackColor = Color.Blue;
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.ForeColor = Color.White;
                }
                else if (valueToUpdate > value1)
                {
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.BackColor = Color.Green;
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.ForeColor = Color.White;
                }
                else if (valueToUpdate < value1 || valueToUpdate < value2)
                {
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.BackColor = Color.Red;
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.ForeColor = Color.White;
                }
            }
            else
            {
                // Apply colors based on comparison
                if (valueToUpdate < value1 && valueToUpdate < value2)
                {
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.BackColor = Color.Blue;
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.ForeColor = Color.White;
                }
                else if (valueToUpdate < value1)
                {
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.BackColor = Color.Green;
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.ForeColor = Color.White;
                }
                else if (valueToUpdate > value1 || valueToUpdate > value2)
                {
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.BackColor = Color.Red;
                    dataGridName.Rows[rowToUpdate].Cells[columnIndex].Style.ForeColor = Color.White;
                }
            }

        }

        private void ChangeCellColor_AprResult(DataGridView dataGridName)
        {
            // Use the method for both row comparisons
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 1); //rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 1);
        }

        private void ChangeCellColor_MayResult(DataGridView dataGridName)
        {
            // For column index 2 (May)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 2);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 2);
        }

        private void ChangeCellColor_JunResult(DataGridView dataGridName)
        {
            // For column index 3 (June)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 3);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 3);
        }

        private void ChangeCellColor_JulResult(DataGridView dataGridName)
        {
            // For column index 4 (Jul)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 4);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 4);
        }

        private void ChangeCellColor_AugResult(DataGridView dataGridName)
        {
            // For column index 5 (Aug)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 5);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 5);
        }


        private void ChangeCellColor_SepResult(DataGridView dataGridName)
        {
            // For column index 6 (Sep)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 6);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 6);
        }

        private void ChangeCellColor_OctResult(DataGridView dataGridName)
        {
            // For column index 7 (Oct)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 7);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 7);
        }

        private void ChangeCellColor_NovResult(DataGridView dataGridName)
        {
            // For column index 8 (Nov)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 8);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 8);

        }

        private void ChangeCellColor_DecResult(DataGridView dataGridName)
        {
            // For column index 9 (Dec)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 9);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 9);
        }

        private void ChangeCellColor_JanResult(DataGridView dataGridName)
        {
            // For column index 10 (Jan)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 10);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 10);

        }


        private void ChangeCellColor_FebResult(DataGridView dataGridName)
        {
            // For column index 11 (Feb)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 11);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 11);
        }

        private void ChangeCellColor_MarResult(DataGridView dataGridName)
        {
            // For column index 12 (Mar)
            ChangeCellColor(dataGridName, 0, 1, 2, 2, 12);//rowIndex1,rowIndex2,rowIndex3, rowToUpdateColor, columnIndex 
            ChangeCellColor(dataGridName, 3, 4, 6, 6, 12);
        }


        private void TotalEffDatagrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            TotalEffDatagrid.Columns[0].Width = 200;
            //TotalEffDatagrid.Rows[0].Cells[0].Style.BackColor = Color.Blue; //Change color of specific row column cell

            ChangeCellColor_AprResult(TotalEffDatagrid);
            ChangeCellColor_MayResult(TotalEffDatagrid);
            ChangeCellColor_JunResult(TotalEffDatagrid);
            ChangeCellColor_JulResult(TotalEffDatagrid);
            ChangeCellColor_AugResult(TotalEffDatagrid);
            ChangeCellColor_SepResult(TotalEffDatagrid);
            ChangeCellColor_OctResult(TotalEffDatagrid);
            ChangeCellColor_NovResult(TotalEffDatagrid);
            ChangeCellColor_DecResult(TotalEffDatagrid);
            ChangeCellColor_JanResult(TotalEffDatagrid);
            ChangeCellColor_FebResult(TotalEffDatagrid);
            ChangeCellColor_MarResult(TotalEffDatagrid);

            ChangeHeaderTextBaseOnFiscalYear(TotalEffDatagrid);
        }

        private void ChangeHeaderTextBaseOnFiscalYear(DataGridView dataGridView)
        {
            int currentYear = DateTime.Now.Year;
            int fiscalYearStart = currentYear;
            int nextFiscalYear = currentYear + 1;

            // If the current month is between January and March, adjust fiscal years
            if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 3)
            {
                fiscalYearStart = currentYear - 1;  // For Jan-Mar, set the fiscal year to the previous year (e.g., Apr-Dec 2024)
                nextFiscalYear = currentYear;        // For Jan-Mar, set next fiscal year to current year (e.g., Jan-Mar 2025)
            }
            else
            {
                fiscalYearStart = currentYear;      // For Apr-Dec, set fiscal year to current year
                nextFiscalYear = currentYear + 1;   // For Jan-Mar, use next fiscal year
            }

            // Set headers for months April to December for the fiscal year (fiscalYearStart)
            dataGridView.Columns["Apr"].HeaderText = "Apr-" + fiscalYearStart;
            dataGridView.Columns["May"].HeaderText = "May-" + fiscalYearStart;
            dataGridView.Columns["Jun"].HeaderText = "Jun-" + fiscalYearStart;
            dataGridView.Columns["Jul"].HeaderText = "Jul-" + fiscalYearStart;
            dataGridView.Columns["Aug"].HeaderText = "Aug-" + fiscalYearStart;
            dataGridView.Columns["Sep"].HeaderText = "Sep-" + fiscalYearStart;
            dataGridView.Columns["Oct"].HeaderText = "Oct-" + fiscalYearStart;
            dataGridView.Columns["Nov"].HeaderText = "Nov-" + fiscalYearStart;
            dataGridView.Columns["Dec"].HeaderText = "Dec-" + fiscalYearStart;

            // Set headers for months January to March for the next fiscal year (nextFiscalYear)
            dataGridView.Columns["Jan"].HeaderText = "Jan-" + nextFiscalYear;
            dataGridView.Columns["Feb"].HeaderText = "Feb-" + nextFiscalYear;
            dataGridView.Columns["Mar"].HeaderText = "Mar-" + nextFiscalYear;


        }


        private void DirectEffDatagrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DirectEffDatagrid.Columns[0].Width = 200;

            ChangeCellColor_AprResult(DirectEffDatagrid);
            ChangeCellColor_MayResult(DirectEffDatagrid);
            ChangeCellColor_JunResult(DirectEffDatagrid);
            ChangeCellColor_JulResult(DirectEffDatagrid);
            ChangeCellColor_AugResult(DirectEffDatagrid);
            ChangeCellColor_SepResult(DirectEffDatagrid);
            ChangeCellColor_OctResult(DirectEffDatagrid);
            ChangeCellColor_NovResult(DirectEffDatagrid);
            ChangeCellColor_DecResult(DirectEffDatagrid);
            ChangeCellColor_JanResult(DirectEffDatagrid);
            ChangeCellColor_FebResult(DirectEffDatagrid);
            ChangeCellColor_MarResult(DirectEffDatagrid);

            ChangeHeaderTextBaseOnFiscalYear(DirectEffDatagrid);

        }


        private void ChangeSemiDirectEffCellColor_AprResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[1].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[1].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[1].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[1].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[1].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[1].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[1].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[1].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[1].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[1].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[1].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[1].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[1].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[1].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[1].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[1].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[1].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[1].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[1].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[1].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[1].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[1].Style.ForeColor = Color.White;
            }

        }

        private void ChangeSemiDirectEffCellColor_MayResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[2].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[2].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[2].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[2].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[2].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[2].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[2].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[2].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[2].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[2].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[2].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[2].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[2].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[2].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[2].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[2].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[2].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[2].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[2].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[2].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[2].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[2].Style.ForeColor = Color.White;
            }

        }

        private void ChangeSemiDirectEffCellColor_JunResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[3].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[3].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[3].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[3].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[3].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[3].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[3].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[3].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[3].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[3].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[3].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[3].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[3].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[3].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[3].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[3].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[3].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[3].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[3].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[3].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[3].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[3].Style.ForeColor = Color.White;
            }

        }

        private void ChangeSemiDirectEffCellColor_JulResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[4].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[4].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[4].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[4].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[4].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[4].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[4].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[4].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[4].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[4].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[4].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[4].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[4].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[4].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[4].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[4].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[4].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[4].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[4].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[4].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[4].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[4].Style.ForeColor = Color.White;
            }

        }

        private void ChangeSemiDirectEffCellColor_AugResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[5].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[5].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[5].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[5].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[5].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[5].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[5].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[5].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[5].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[5].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[5].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[5].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[5].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[5].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[5].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[5].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[5].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[5].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[5].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[5].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[5].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[5].Style.ForeColor = Color.White;
            }

        }


        private void ChangeSemiDirectEffCellColor_SepResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[6].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[6].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[6].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[6].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[6].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[6].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[6].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[6].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[6].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[6].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[6].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[6].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[6].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[6].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[6].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[6].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[6].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[6].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[6].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[6].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[6].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[6].Style.ForeColor = Color.White;
            }
        }

        private void ChangeSemiDirectEffCellColor_OctResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[7].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[7].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[7].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[7].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[7].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[7].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[7].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[7].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[7].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[7].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[7].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[7].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[7].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[7].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[7].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[7].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[7].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[7].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[7].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[7].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[7].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[7].Style.ForeColor = Color.White;
            }
        }

        private void ChangeSemiDirectEffCellColor_NovResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[8].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[8].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[8].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[8].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[8].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[8].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[8].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[8].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[8].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[8].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[8].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[8].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[8].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[8].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[8].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[8].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[8].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[8].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[8].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[8].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[8].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[8].Style.ForeColor = Color.White;
            }
        }

        private void ChangeSemiDirectEffCellColor_DecResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[9].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[9].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[9].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[9].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[9].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[9].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[9].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[9].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[9].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[9].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[9].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[9].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[9].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[9].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[9].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[9].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[9].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[9].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[9].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[9].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[9].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[9].Style.ForeColor = Color.White;
            }
        }

        private void ChangeSemiDirectEffCellColor_JanResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[10].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[10].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[10].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[10].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[10].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[10].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[10].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[10].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[10].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[10].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[10].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[10].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[10].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[10].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[10].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[10].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[10].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[10].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[10].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[10].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[10].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[10].Style.ForeColor = Color.White;
            }
        }


        private void ChangeSemiDirectEffCellColor_FebResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[11].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[11].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[11].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[11].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[11].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[11].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[11].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[11].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[11].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[11].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[11].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[11].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[11].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[11].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[11].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[11].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[11].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[11].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[11].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[11].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[11].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[11].Style.ForeColor = Color.White;
            }
        }

        private void ChangeSemiDirectEffCellColor_MarResult()
        {
            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[12].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[12].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[12].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[2].Cells[12].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(DirectEffDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[12].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[12].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[2].Cells[12].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[1].Cells[12].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[12].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[2].Cells[12].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[2].Cells[12].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[12].Value.ToString().Replace("%", "")) && Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[12].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[12].Style.BackColor = Color.Blue;
                SemiDirectRateDatagrid.Rows[6].Cells[12].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[12].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[12].Style.BackColor = Color.Green;
                SemiDirectRateDatagrid.Rows[6].Cells[12].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[12].Value.ToString().Replace("%", "")) || Convert.ToDecimal(SemiDirectRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[3].Cells[12].Value.ToString().Replace("%", "")))
            {
                SemiDirectRateDatagrid.Rows[6].Cells[12].Style.BackColor = Color.Red;
                SemiDirectRateDatagrid.Rows[6].Cells[12].Style.ForeColor = Color.White;
            }
        }

        private void SemiDirectRateDatagrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SemiDirectRateDatagrid.Columns[0].Width = 200;

            ChangeCellColor_AprResult(SemiDirectRateDatagrid);
            ChangeCellColor_MayResult(SemiDirectRateDatagrid);
            ChangeCellColor_JunResult(SemiDirectRateDatagrid);
            ChangeCellColor_JulResult(SemiDirectRateDatagrid);
            ChangeCellColor_AugResult(SemiDirectRateDatagrid);
            ChangeCellColor_SepResult(SemiDirectRateDatagrid);
            ChangeCellColor_OctResult(SemiDirectRateDatagrid);
            ChangeCellColor_NovResult(SemiDirectRateDatagrid);
            ChangeCellColor_DecResult(SemiDirectRateDatagrid);
            ChangeCellColor_JanResult(SemiDirectRateDatagrid);
            ChangeCellColor_FebResult(SemiDirectRateDatagrid);
            ChangeCellColor_MarResult(SemiDirectRateDatagrid);

            ChangeHeaderTextBaseOnFiscalYear(SemiDirectRateDatagrid);

        }

        private void ChangeTotalLossRateCellColor_AprResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[1].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[1].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[1].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[1].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[1].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[1].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[1].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[1].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[1].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[1].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[1].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[1].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[1].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[1].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[1].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[1].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[1].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[1].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[1].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[1].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[1].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[1].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[1].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[1].Style.ForeColor = Color.White;
            }

        }

        private void ChangeTotalLossRateCellColor_MayResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[2].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[2].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[2].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[2].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[2].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[2].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[2].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[2].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[2].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[2].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[2].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[2].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[2].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[2].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[2].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[2].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[2].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[2].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[2].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[2].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[2].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[2].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[2].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[2].Style.ForeColor = Color.White;
            }

        }

        private void ChangeTotalLossRateCellColor_JunResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[3].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[3].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[3].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[3].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[3].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[3].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[3].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[3].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[3].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[3].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[3].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[3].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[3].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[3].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[3].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[3].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[3].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[3].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[3].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[3].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[3].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[3].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[3].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[3].Style.ForeColor = Color.White;
            }

        }

        private void ChangeTotalLossRateCellColor_JulResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[4].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[4].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[4].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[4].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[4].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[4].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[4].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[4].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[4].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[0].Cells[4].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[4].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[4].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[4].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", ""))  < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[4].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[4].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[4].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[4].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[4].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[4].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[4].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[4].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[4].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[4].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[4].Style.ForeColor = Color.White;
            }

        }

        private void ChangeTotalLossRateCellColor_AugResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[5].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[5].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[5].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[5].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[5].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[5].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[5].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[5].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[5].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[5].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[5].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[5].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[5].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[5].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[5].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[5].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[5].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[5].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[5].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) > Convert.ToDecimal(SemiDirectRateDatagrid.Rows[4].Cells[5].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[5].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[5].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[5].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[5].Style.ForeColor = Color.White;
            }

        }


        private void ChangeTotalLossRateCellColor_SepResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[6].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[6].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[6].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[6].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[6].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[6].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[6].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[6].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[6].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[6].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[6].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[6].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[6].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[6].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[6].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[6].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[6].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[6].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[6].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[6].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[6].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[6].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[6].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[6].Style.ForeColor = Color.White;
            }
        }

        private void ChangeTotalLossRateCellColor_OctResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[7].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[7].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[7].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[7].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[7].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[7].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[7].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[7].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[7].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[7].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[7].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[7].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[7].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[7].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[7].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[7].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[7].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[7].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[7].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[7].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[7].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[7].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[7].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[7].Style.ForeColor = Color.White;
            }
        }

        private void ChangeTotalLossRateCellColor_NovResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[8].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[8].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[8].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[8].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[8].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[8].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[8].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[8].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[8].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[8].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[8].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[8].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[8].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[8].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[8].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[8].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[8].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[8].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[8].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[8].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[8].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[8].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[8].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[8].Style.ForeColor = Color.White;
            }
        }

        private void ChangeTotalLossRateCellColor_DecResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[9].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[9].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[9].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[9].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[9].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[9].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[9].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[9].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[9].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[9].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[9].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[9].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[9].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[9].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[9].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[9].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[9].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[9].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[9].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[9].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[9].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[9].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[9].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[9].Style.ForeColor = Color.White;
            }
        }

        private void ChangeTotalLossRateCellColor_JanResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[10].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[10].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[10].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[10].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[10].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[10].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[10].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[10].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[10].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[10].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[10].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[10].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[10].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[10].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[10].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[10].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[10].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[10].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[10].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[10].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[10].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[10].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[10].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[10].Style.ForeColor = Color.White;
            }
        }


        private void ChangeTotalLossRateCellColor_FebResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[11].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[11].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[11].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[11].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[11].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[11].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[11].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[11].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[11].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[11].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[11].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[11].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[11].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[11].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[11].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[11].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[11].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[11].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[11].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[11].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[11].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[11].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[11].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[11].Style.ForeColor = Color.White;
            }
        }

        private void ChangeTotalLossRateCellColor_MarResult()
        {
            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[12].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[12].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[12].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[2].Cells[12].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[12].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[12].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[2].Cells[12].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[1].Cells[12].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[2].Cells[12].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[0].Cells[12].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[2].Cells[12].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[2].Cells[12].Style.ForeColor = Color.White;
            }

            //----------------------------------------------------------------------

            if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[12].Value.ToString().Replace("%", "")) && Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[12].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[12].Style.BackColor = Color.Blue;
                TTLossRateDatagrid.Rows[6].Cells[12].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) < Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[12].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[12].Style.BackColor = Color.Green;
                TTLossRateDatagrid.Rows[6].Cells[12].Style.ForeColor = Color.White;
            }
            else if (Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[4].Cells[12].Value.ToString().Replace("%", "")) || Convert.ToDecimal(TTLossRateDatagrid.Rows[6].Cells[12].Value.ToString().Replace("%", "")) > Convert.ToDecimal(TTLossRateDatagrid.Rows[3].Cells[12].Value.ToString().Replace("%", "")))
            {
                TTLossRateDatagrid.Rows[6].Cells[12].Style.BackColor = Color.Red;
                TTLossRateDatagrid.Rows[6].Cells[12].Style.ForeColor = Color.White;
            }
        }

        private void TTLossRateDatagrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            TTLossRateDatagrid.Columns[0].Width = 200;

            ChangeCellColor_AprResult(TTLossRateDatagrid);
            ChangeCellColor_MayResult(TTLossRateDatagrid);
            ChangeCellColor_JunResult(TTLossRateDatagrid);
            ChangeCellColor_JulResult(TTLossRateDatagrid);
            ChangeCellColor_AugResult(TTLossRateDatagrid);
            ChangeCellColor_SepResult(TTLossRateDatagrid);
            ChangeCellColor_OctResult(TTLossRateDatagrid);
            ChangeCellColor_NovResult(TTLossRateDatagrid);
            ChangeCellColor_DecResult(TTLossRateDatagrid);
            ChangeCellColor_JanResult(TTLossRateDatagrid);
            ChangeCellColor_FebResult(TTLossRateDatagrid);
            ChangeCellColor_MarResult(TTLossRateDatagrid);


            ChangeHeaderTextBaseOnFiscalYear(TTLossRateDatagrid);


        }

        private void ProductionEfficiencyForm_Scroll(object sender, ScrollEventArgs e)
        {
            ////Load Efficiency Summary Data
            //SelectTotalEfficiencyData();
            //SelectDirectEfficiencyData();
            //SelectSemiDirectRateData();
            //SelectTotalLossRateData();
        }

        private void TotalEfficiencyTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TotalEfficiencyTypeDropdown.Text == "Overall Result")
            {
                TotalEfficiencyCategoryDropdown.Enabled = false;
                TotalEfficiencyCategoryDropdown.Text = "";
            }
            else
            {
                TotalEfficiencyCategoryDropdown.Enabled = true;
                TE_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            }
        }

        private void TotalEfficiencyGenerateButton_Click(object sender, EventArgs e)
        {
            if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                if (TotalEfficiencyTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TotalEfficiencyTypeDropdown.Select();
                }
                //else if (TotalEfficiencyPrinterDropdown.Text == "")
                //{
                //    MessageBox.Show("Please select printer type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                else
                {
                    if (TotalEfficiencyTypeDropdown.Text == "Overall Result")
                    {
                        GetTotalEfficiencyDailyResult();
                        GetTotalEfficiencyMonthlyResult();
                    }
                    else if (TotalEfficiencyTypeDropdown.Text == "Contributor Result")
                    {
                        if (TotalEfficiencyCategoryDropdown.Text == "")
                        {
                            MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TotalEfficiencyCategoryDropdown.Select();
                        }
                        else if (TotalEfficiencyPrinterDropdown.Text == "")
                        {
                            MessageBox.Show("Please select printer type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TotalEfficiencyPrinterDropdown.Select();
                        }
                        else
                        {
                            GetTotalEfficiencyDailyResult();
                            GetTotalEfficiencyMonthlyResult();

                            //if (TotalEfficiencyCategoryDropdown.Text == "Per Cost Center")
                            //{
                            //    GetTotalEfficiencyDailyResult();
                            //    GetTotalEfficiencyMonthlyResult();
                            //}
                            //else if (TotalEfficiencyCategoryDropdown.Text == "Per Work Center")
                            //{
                            //    GetTotalEfficiencyDailyResult();
                            //    GetTotalEfficiencyMonthlyResult();
                            //}
                            //else if (TotalEfficiencyCategoryDropdown.Text == "Per Process")
                            //{
                            //    GetTotalEfficiencyDailyResult();
                            //    GetTotalEfficiencyMonthlyResult();
                            //}
                            //else {  }
                        }
                    }
                }
            }
            else
            {
                if (TotalEfficiencyTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TotalEfficiencyTypeDropdown.Select();
                }
                else
                {
                    if (TotalEfficiencyTypeDropdown.Text == "Overall Result")
                    {
                        GetTotalEfficiencyDailyResult();
                        GetTotalEfficiencyMonthlyResult();
                    }
                    else
                    {
                        if (TotalEfficiencyCategoryDropdown.Text == "")
                        {
                            MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TotalEfficiencyCategoryDropdown.Select();
                        }
                        else
                        {
                            GetTotalEfficiencyDailyResult();
                            GetTotalEfficiencyMonthlyResult();
                        }
                    }
                }
            }
        }

        private void GetTotalEfficiencyDailyResult()
        {
            if (TotalEfficiencyDropdownEntriesValue.Text == "All")
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectTotalEfficiencyDailyResult = new SqlCommand("SP_SelectTotalEfficiencyDailyResult", con);
                SelectTotalEfficiencyDailyResult.CommandType = CommandType.StoredProcedure;
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Section", TE_SectionDeptDropdown.Text);
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@DateFrom", TotalEffDateFrom.Value.ToString());
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@DateTo", TotalEffDateTo.Value.ToString());
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Type", TotalEfficiencyTypeDropdown.Text);
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Category", TotalEfficiencyCategoryDropdown.Text);
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@PrinterType", TotalEfficiencyPrinterDropdown.Text);
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectTotalEfficiencyDailyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TotalEfficiencyDataGird.DataSource = dt;
                con.Close();
            }
            else
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectTotalEfficiencyDailyResult = new SqlCommand("SP_SelectTotalEfficiencyDailyResult", con);
                SelectTotalEfficiencyDailyResult.CommandType = CommandType.StoredProcedure;
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Section", TE_SectionDeptDropdown.Text);
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@DateFrom", TotalEffDateFrom.Value.ToString());
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@DateTo", TotalEffDateTo.Value.ToString());
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Type", TotalEfficiencyTypeDropdown.Text);
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Category", TotalEfficiencyCategoryDropdown.Text);
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@PrinterType", TotalEfficiencyPrinterDropdown.Text);
                SelectTotalEfficiencyDailyResult.Parameters.AddWithValue("@Entries", Convert.ToInt32(TotalEfficiencyDropdownEntriesValue.Text));
                SqlDataAdapter sda = new SqlDataAdapter(SelectTotalEfficiencyDailyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TotalEfficiencyDataGird.DataSource = dt;
                con.Close();
            }
           
        }



        private void GetTotalEfficiencyMonthlyResult()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectTotalEfficiencyMonthlyResult = new SqlCommand("SP_SelectTotalEfficiencyMonthlyResult", con);
            SelectTotalEfficiencyMonthlyResult.CommandType = CommandType.StoredProcedure;
            SelectTotalEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SelectTotalEfficiencyMonthlyResult.Parameters.AddWithValue("@DateFrom", TotalEffDateFrom.Value.ToString());
            SelectTotalEfficiencyMonthlyResult.Parameters.AddWithValue("@DateTo", TotalEffDateTo.Value.ToString());
            SelectTotalEfficiencyMonthlyResult.Parameters.AddWithValue("@Type", TotalEfficiencyTypeDropdown.Text);
            SelectTotalEfficiencyMonthlyResult.Parameters.AddWithValue("@Category", TotalEfficiencyCategoryDropdown.Text);
            SelectTotalEfficiencyMonthlyResult.Parameters.AddWithValue("@PrinterType", TotalEfficiencyPrinterDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectTotalEfficiencyMonthlyResult);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TotalEfficiencyDataGird_Top.DataSource = dt;
            con.Close();
        }

        private void TotalEfficiencyDropdownEntriesValue_TextChanged(object sender, EventArgs e)
        {
            if (TotalEfficiencyDropdownEntriesValue.Text == "")
            {
                MessageBox.Show("Please select or type number of entries.", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TotalEfficiencyDropdownEntriesValue.Focus();
            }
            else
            {
                GetTotalEfficiencyDailyResult();
                GetTotalEfficiencyMonthlyResult();
            }
        }

        private void TotalEfficiencyDataGird_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in TotalEfficiencyDataGird.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            TotalEfficiencyDataGird.EnableHeadersVisualStyles = false;
            TotalEfficiencyDataGird.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            TotalEfficiencyDataGird.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void TotalEfficiencyDataGird_Top_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in TotalEfficiencyDataGird_Top.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            TotalEfficiencyDataGird_Top.EnableHeadersVisualStyles = false;
            TotalEfficiencyDataGird_Top.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            TotalEfficiencyDataGird_Top.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }


        private void copyAlltoClipboard_TE_DailyResult()
        {

            TotalEfficiencyDataGird.SelectAll();
            //Copy to clipboard
            TotalEfficiencyDataGird.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = TotalEfficiencyDataGird.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void copyAlltoClipboard_TE_MonthlyResult()
        {

            TotalEfficiencyDataGird_Top.SelectAll();
            //Copy to clipboard
            TotalEfficiencyDataGird_Top.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj2 = TotalEfficiencyDataGird_Top.GetClipboardContent();
            if (dataObj2 != null)
                Clipboard.SetDataObject(dataObj2);
        }

        private void ExportMHData()
        {
            string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";
            System.IO.Directory.CreateDirectory(pathsss);

            copyAlltoClipboard_TE_DailyResult();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //This is to export the daily result
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            xlWorkSheet = xlWorkBook.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["A1", "G1"];
            oRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 242, 204));
            oRange.Font.Bold = true;

            //This is to export the monthly result
            copyAlltoClipboard_TE_MonthlyResult();
            Microsoft.Office.Interop.Excel.Range CR2 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 8];
            CR2.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR2, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            Microsoft.Office.Interop.Excel.Range oRange2 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["H1", "K1"];
            oRange2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(214,220,228));
            oRange2.Font.Bold = true;
        }

       
        private void TotalEfficiencyExportBtn_Click(object sender, EventArgs e)
        {
            ExportMHData();
        }


        private void TotalEfficiencySearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (SearchDropdownList.Text == "--Select--")
                {
                    MessageBox.Show("Please select search category first before enter the keywords.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SearchTotalEfficiencyData();
                }
                
            }
        }

        private void SearchTotalEfficiencyData()
        {

            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SearchEfficiencyData = new SqlCommand("SP_SearchProdEfficiencyData", con);
            SearchEfficiencyData.CommandType = CommandType.StoredProcedure;
            SearchEfficiencyData.Parameters.AddWithValue("@Procedure", "Search_TotalEfficiencyData");
            SearchEfficiencyData.Parameters.AddWithValue("@Category", SearchDropdownList.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@Section", TE_SectionDeptDropdown.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@Search", TotalEfficiencySearchBox.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@DateFrom", TotalEffDateFrom.Value.ToString());
            SearchEfficiencyData.Parameters.AddWithValue("@DateTo", TotalEffDateTo.Value.ToString());
            //SearchEfficiencyData.Parameters.AddWithValue("@Entries", "");
            SqlDataAdapter sda = new SqlDataAdapter(SearchEfficiencyData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TotalEfficiencyDataGird.DataSource = dt;
            con.Close();
        }

        private void TE_SectionDeptDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TE_SectionDeptDropdown.Text == "Production Department")
            {
                //TotalEfficiencyTypeDropdown.Enabled = false;
                TotalEfficiencyTypeDropdown.Text = "Overall Result";
            }
            else
            {
                TotalEfficiencyTypeDropdown.Enabled = true;
                TotalEfficiencyTypeDropdown.Text = "";
            }
        }

        private void DE_SectionDeptDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DE_SectionDeptDropdown.Text == "Production Department")
            {
                //DE_SectionDeptDropdown.Enabled = false;
                DirectEfficiencyTypeDropdown.Text = "Overall Result";
            }
            else
            {
                DirectEfficiencyTypeDropdown.Enabled = true;
                DirectEfficiencyTypeDropdown.Text = "";
            }
        }

        private void SemiDirectPrinterDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SemiDirectPrinterDropdown.Text == "Production Department")
            {
                SemiDirectPrinterDropdown.Enabled = false;
                SemiDirectPrinterDropdown.Text = "Overall Result";
            }
            else
            {
                SemiDirectPrinterDropdown.Enabled = true;
                SemiDirectPrinterDropdown.Text = "";
            }
        }


        private void TLR_SectionDeptDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TLR_SectionDeptDropdown.Text == "Production Department")
            {
                //DE_SectionDeptDropdown.Enabled = false;
                TotalLossRateType.Text = "Overall Result";
            }
            else
            {
                TotalLossRateType.Enabled = true;
                TotalLossRateType.Text = "";
            }
        }

        private void DirectEfficiencyGenerateBtn_Click(object sender, EventArgs e)
        {
            if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                if (DirectEfficiencyTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DirectEfficiencyTypeDropdown.Select();
                }
                //else if (TotalEfficiencyPrinterDropdown.Text == "")
                //{
                //    MessageBox.Show("Please select printer type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                else
                {
                    if (DirectEfficiencyTypeDropdown.Text == "Overall Result")
                    {
                        GetDirectEfficiencyDailyResult();
                        GetDirectEfficiencyMonthlyResult();
                        GetDirectEfficiencyContributorResult();
                    }
                    else if (DirectEfficiencyTypeDropdown.Text == "Contributor Result")
                    {

                        if (DirectEfficiencyCategoryDropdown.Text == "")
                        {
                            MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            DirectEfficiencyCategoryDropdown.Select();
                        }                             
                        else if (DirectEfficiencyPrinterDropdown.Text == "")
                        {
                            MessageBox.Show("Please select printer type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            DirectEfficiencyPrinterDropdown.Select();
                        }
                        else
                        {
                            GetDirectEfficiencyDailyResult();
                            GetDirectEfficiencyMonthlyResult();
                            GetDirectEfficiencyContributorResult();
                        }                                           
                    }
                }
            }
            else
            {
                if (DirectEfficiencyTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DirectEfficiencyTypeDropdown.Select();
                }
                else
                {
                    if (DirectEfficiencyTypeDropdown.Text == "Overall Result")
                    {
                        GetDirectEfficiencyDailyResult();
                        GetDirectEfficiencyMonthlyResult();
                        GetDirectEfficiencyContributorResult();
                    }
                    else if (DirectEfficiencyTypeDropdown.Text == "Contributor Result")
                    {
                        if (DirectEfficiencyCategoryDropdown.Text == "")
                        {
                            MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            DirectEfficiencyCategoryDropdown.Select();
                        }
                        else 
                        {
                            GetDirectEfficiencyDailyResult();
                            GetDirectEfficiencyMonthlyResult();
                            GetDirectEfficiencyContributorResult();
                        }
                    }
                }
            }
        }


        private void GetDirectEfficiencyDailyResult()
        {
            if (DirectEfficiencyDropdownEntries.Text == "All")
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectDirectEfficiencyDailyResult = new SqlCommand("SP_SelectDirectEfficiencyDailyResult", con);
                SelectDirectEfficiencyDailyResult.CommandType = CommandType.StoredProcedure;
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@DateFrom", DirectEffDateFrom.Value.ToString());
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@DateTo", DirectEffDateTo.Value.ToString());
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Type", DirectEfficiencyTypeDropdown.Text);
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Category", DirectEfficiencyCategoryDropdown.Text);
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@PrinterType", DirectEfficiencyPrinterDropdown.Text);
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectDirectEfficiencyDailyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DirectEfficiencyDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectDirectEfficiencyDailyResult = new SqlCommand("SP_SelectDirectEfficiencyDailyResult", con);
                SelectDirectEfficiencyDailyResult.CommandType = CommandType.StoredProcedure;
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@DateFrom", DirectEffDateFrom.Value.ToString());
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@DateTo", DirectEffDateTo.Value.ToString());
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Type", DirectEfficiencyTypeDropdown.Text);
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Category", DirectEfficiencyCategoryDropdown.Text);
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@PrinterType", DirectEfficiencyPrinterDropdown.Text);
                SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Entries", Convert.ToInt32(DirectEfficiencyDropdownEntries.Text));
                SqlDataAdapter sda = new SqlDataAdapter(SelectDirectEfficiencyDailyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DirectEfficiencyDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void GetDirectEfficiencyMonthlyResult()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectDirectEfficiencyMonthlyResult = new SqlCommand("SP_SelectDirectEfficiencyMonthlyResult", con);
            SelectDirectEfficiencyMonthlyResult.CommandType = CommandType.StoredProcedure;
            SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            //SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
            SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateFrom", DirectEffDateFrom.Value.ToString());
            SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateTo", DirectEffDateTo.Value.ToString());
            SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Type", DirectEfficiencyTypeDropdown.Text);
            SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Category", DirectEfficiencyCategoryDropdown.Text);
            SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@PrinterType", DirectEfficiencyPrinterDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectDirectEfficiencyMonthlyResult);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DirectEfficiencyDataGrid_Top.DataSource = dt;
            con.Close();
        }

        private void GetDirectEfficiencyContributorResult()
        {
            if (DirectEfficiencyDropdownEntries.Text == "All")
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectDirectEfficiencyMonthlyResult = new SqlCommand("SP_SelectDirectEfficiencyContributorResult", con);
                SelectDirectEfficiencyMonthlyResult.CommandType = CommandType.StoredProcedure;
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateFrom", DirectEffDateFrom.Value.ToString());
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateTo", DirectEffDateTo.Value.ToString());
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Type", DirectEfficiencyTypeDropdown.Text);
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Category", DirectEfficiencyCategoryDropdown.Text);
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@PrinterType", DirectEfficiencyPrinterDropdown.Text);
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectDirectEfficiencyMonthlyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DirectEfficiencyContributorDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectDirectEfficiencyMonthlyResult = new SqlCommand("SP_SelectDirectEfficiencyContributorResult", con);
                SelectDirectEfficiencyMonthlyResult.CommandType = CommandType.StoredProcedure;
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SelectDirectEfficiencyDailyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateFrom", DirectEffDateFrom.Value.ToString());
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateTo", DirectEffDateTo.Value.ToString());
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Type", DirectEfficiencyTypeDropdown.Text);
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Category", DirectEfficiencyCategoryDropdown.Text);
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@PrinterType", DirectEfficiencyPrinterDropdown.Text);
                SelectDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Entries", Convert.ToInt32(DirectEfficiencyDropdownEntries.Text));
                SqlDataAdapter sda = new SqlDataAdapter(SelectDirectEfficiencyMonthlyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DirectEfficiencyContributorDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void DirectEfficiencyTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DirectEfficiencyTypeDropdown.Text == "Overall Result")
            {
                DirectEfficiencyCategoryDropdown.Enabled = false;
                DirectEfficiencyCategoryDropdown.Text = "";
            }
            else
            {
                DirectEfficiencyCategoryDropdown.Enabled = true;
                DE_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            }
        }

        private void DirectEfficiencyDataGrid_Top_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in DirectEfficiencyDataGrid_Top.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DirectEfficiencyDataGrid_Top.EnableHeadersVisualStyles = false;
            DirectEfficiencyDataGrid_Top.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            DirectEfficiencyDataGrid_Top.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void DirectEfficiencyDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in DirectEfficiencyDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DirectEfficiencyDataGrid.EnableHeadersVisualStyles = false;
            DirectEfficiencyDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            DirectEfficiencyDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void DirectEfficiencyDropdownEntries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TotalEfficiencyDropdownEntriesValue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DirectEfficiencyDropdownEntries_TextChanged(object sender, EventArgs e)
        {
            if (DirectEfficiencyDropdownEntries.Text == "")
            {
                MessageBox.Show("Please select or type number of entries.", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DirectEfficiencyDropdownEntries.Focus();
            }
            else
            {
                GetDirectEfficiencyDailyResult();
                GetDirectEfficiencyMonthlyResult();
                GetDirectEfficiencyContributorResult();
            }
        }


        private void copyAlltoClipboard_DE_DailyResult()
        {

            DirectEfficiencyDataGrid.SelectAll();
            //Copy to clipboard
            DirectEfficiencyDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = DirectEfficiencyDataGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void copyAlltoClipboard_DE_Contributor()
        {

            DirectEfficiencyContributorDataGrid.SelectAll();
            //Copy to clipboard
            DirectEfficiencyContributorDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj2 = DirectEfficiencyContributorDataGrid.GetClipboardContent();
            if (dataObj2 != null)
                Clipboard.SetDataObject(dataObj2);
        }
      
        private void copyAlltoClipboard_DE_MonthlyResult()
        {

            DirectEfficiencyDataGrid_Top.SelectAll();
            //Copy to clipboard
            DirectEfficiencyDataGrid_Top.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj3 = DirectEfficiencyDataGrid_Top.GetClipboardContent();
            if (dataObj3 != null)
                Clipboard.SetDataObject(dataObj3);
        }

        private void ExportMHData_DE()
        {
            string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";
            System.IO.Directory.CreateDirectory(pathsss);

            copyAlltoClipboard_DE_DailyResult();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //This is to export the daily result -------------------------------------------------
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            xlWorkSheet = xlWorkBook.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["A1", "D1"];
            oRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 242, 204));
            oRange.Font.Bold = true;

            //This is to export the monthly result --------------------------------------------------
            copyAlltoClipboard_DE_Contributor();
            Microsoft.Office.Interop.Excel.Range CR2 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 6];
            CR2.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR2, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            Microsoft.Office.Interop.Excel.Range oRange2 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["F1", "H1"];
            oRange2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(214, 220, 228));
            oRange2.Font.Bold = true;

            //This is to export the monthly result------------------------------------------------------
            copyAlltoClipboard_DE_MonthlyResult();
            Microsoft.Office.Interop.Excel.Range CR3 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 10];
            CR3.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR3, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            Microsoft.Office.Interop.Excel.Range oRange3 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["J1", "M1"];
            oRange3.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(214, 220, 228));
            oRange3.Font.Bold = true;
        }

        private void DirectEfficiencyExportBtn_Click(object sender, EventArgs e)
        {
            ExportMHData_DE();
        }

        private void DirectEfficiencyContributorDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in DirectEfficiencyContributorDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DirectEfficiencyContributorDataGrid.EnableHeadersVisualStyles = false;
            DirectEfficiencyContributorDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            DirectEfficiencyContributorDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }


       

        private void SemiDirectTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SemiDirectTypeDropdown.Text == "Overall Result")
            {
                SemiDirectCatedoryDropdown.Enabled = false;
                SemiDirectCatedoryDropdown.Text = "";
            }
            else
            {
                SemiDirectCatedoryDropdown.Enabled = true;
                SDR_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            }
        }

        private void SDR_SectionDeptDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SDR_SectionDeptDropdown.Text == "Production Department")
            {
                //DE_SectionDeptDropdown.Enabled = false;
                SemiDirectTypeDropdown.Text = "Overall Result";
            }
            else
            {
                SemiDirectTypeDropdown.Enabled = true;
                SemiDirectTypeDropdown.Text = "";
            }
        }

        private void SemiDirectRateBtn_Click(object sender, EventArgs e)
        {
            if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                if (SemiDirectTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SemiDirectTypeDropdown.Select();
                }
                //else if (TotalEfficiencyPrinterDropdown.Text == "")
                //{
                //    MessageBox.Show("Please select printer type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                else
                {
                    if (SemiDirectTypeDropdown.Text == "Overall Result")
                    {
                        GetSemiDirectEfficiencyDailyResult();
                        GetSemiDirectEfficiencyMonthlyResult();
                        GetSemiDirectEfficiencyContributorResult();
                    }
                    else if (SemiDirectTypeDropdown.Text == "Contributor Result")
                    {

                        if (SemiDirectCatedoryDropdown.Text == "")
                        {
                            MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            SemiDirectCatedoryDropdown.Select();
                        }
                        else if (SemiDirectPrinterDropdown.Text == "")
                        {
                            MessageBox.Show("Please select printer type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            SemiDirectPrinterDropdown.Select();
                        }
                        else
                        {
                            GetSemiDirectEfficiencyDailyResult();
                            GetSemiDirectEfficiencyMonthlyResult();
                            GetSemiDirectEfficiencyContributorResult();
                        }
                    }
                }
            }
            else
            {
                if (SemiDirectTypeDropdown.Text == "")
                {
                    MessageBox.Show("Please select type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SemiDirectTypeDropdown.Select();
                }
                else
                {
                    if (SemiDirectTypeDropdown.Text == "Overall Result")
                    {
                        GetSemiDirectEfficiencyDailyResult();
                        GetSemiDirectEfficiencyMonthlyResult();
                        GetSemiDirectEfficiencyContributorResult();
                    }
                    else if (SemiDirectTypeDropdown.Text == "Contributor Result")
                    {
                        if (SemiDirectCatedoryDropdown.Text == "")
                        {
                            MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            SemiDirectCatedoryDropdown.Select();
                        }
                        else
                        {
                            GetSemiDirectEfficiencyDailyResult();
                            GetSemiDirectEfficiencyMonthlyResult();
                            GetSemiDirectEfficiencyContributorResult();
                        }
                    }
                }
            }
        }


        private void GetSemiDirectEfficiencyDailyResult()
        {
            if (SemiDirectDropdownEntries.Text == "All")
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectSemiDirectEfficiencyDailyResult = new SqlCommand("SP_SelectSemiDirectEfficiencyDailyResult", con);
                SelectSemiDirectEfficiencyDailyResult.CommandType = CommandType.StoredProcedure;
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Section", SDR_SectionDeptDropdown.Text);
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@DateFrom", SemiDirectRateDateFrom.Value.ToString());
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@DateTo", SemiDirectRateDateTo.Value.ToString());
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Type", SemiDirectTypeDropdown.Text);
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Category", SemiDirectCatedoryDropdown.Text);
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@PrinterType", SemiDirectPrinterDropdown.Text);
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectSemiDirectEfficiencyDailyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SemiDirectEfficiencyDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectSemiDirectEfficiencyDailyResult = new SqlCommand("SP_SelectSemiDirectEfficiencyDailyResult", con);
                SelectSemiDirectEfficiencyDailyResult.CommandType = CommandType.StoredProcedure;
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Section", SDR_SectionDeptDropdown.Text);
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@DateFrom", SemiDirectRateDateFrom.Value.ToString());
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@DateTo", SemiDirectRateDateTo.Value.ToString());
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Type", SemiDirectTypeDropdown.Text);
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Category", SemiDirectCatedoryDropdown.Text);
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@PrinterType", SemiDirectPrinterDropdown.Text);
                SelectSemiDirectEfficiencyDailyResult.Parameters.AddWithValue("@Entries", Convert.ToInt32(SemiDirectDropdownEntries.Text));
                SqlDataAdapter sda = new SqlDataAdapter(SelectSemiDirectEfficiencyDailyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SemiDirectEfficiencyDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void GetSemiDirectEfficiencyMonthlyResult()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectSemiDirectEfficiencyMonthlyResult = new SqlCommand("SP_SelectSemiDirectEfficiencyMonthlyResult", con);
            SelectSemiDirectEfficiencyMonthlyResult.CommandType = CommandType.StoredProcedure;
            SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            //SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
            SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateFrom", SemiDirectRateDateFrom.Value.ToString());
            SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateTo", SemiDirectRateDateTo.Value.ToString());
            SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Type", SemiDirectTypeDropdown.Text);
            SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Category", SemiDirectCatedoryDropdown.Text);
            SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@PrinterType", SemiDirectPrinterDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectSemiDirectEfficiencyMonthlyResult);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SemiDirectEfficiencyDataGrid_Top.DataSource = dt;
            con.Close();
        }

        private void GetSemiDirectEfficiencyContributorResult()
        {
            if (SemiDirectDropdownEntries.Text == "All")
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectSemiDirectEfficiencyMonthlyResult = new SqlCommand("SP_SelectSemiDirectEfficiencyContributorResult", con);
                SelectSemiDirectEfficiencyMonthlyResult.CommandType = CommandType.StoredProcedure;
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateFrom", SemiDirectRateDateFrom.Value.ToString());
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateTo", SemiDirectRateDateTo.Value.ToString());
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Type", SemiDirectTypeDropdown.Text);
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Category", SemiDirectCatedoryDropdown.Text);
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@PrinterType", SemiDirectPrinterDropdown.Text);
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectSemiDirectEfficiencyMonthlyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SemiDirectEfficiencyContributorDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectSemiDirectEfficiencyMonthlyResult = new SqlCommand("SP_SelectSemiDirectEfficiencyContributorResult", con);
                SelectSemiDirectEfficiencyMonthlyResult.CommandType = CommandType.StoredProcedure;
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateFrom", SemiDirectRateDateFrom.Value.ToString());
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@DateTo", SemiDirectRateDateTo.Value.ToString());
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Type", SemiDirectTypeDropdown.Text);
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Category", SemiDirectCatedoryDropdown.Text);
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@PrinterType", SemiDirectPrinterDropdown.Text);
                SelectSemiDirectEfficiencyMonthlyResult.Parameters.AddWithValue("@Entries", Convert.ToInt32(SemiDirectDropdownEntries.Text));
                SqlDataAdapter sda = new SqlDataAdapter(SelectSemiDirectEfficiencyMonthlyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SemiDirectEfficiencyContributorDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void SemiDirectDropdownEntries_TextChanged(object sender, EventArgs e)
        {
            if (SemiDirectDropdownEntries.Text == "")
            {
                MessageBox.Show("Please select or type number of entries.", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SemiDirectDropdownEntries.Focus();
            }
            else
            {
                GetSemiDirectEfficiencyDailyResult();
                GetSemiDirectEfficiencyMonthlyResult();
                GetSemiDirectEfficiencyContributorResult();
            }
        }



        private void copyAlltoClipboard_SDE_DailyResult()
        {

            SemiDirectEfficiencyDataGrid.SelectAll();
            //Copy to clipboard
            SemiDirectEfficiencyDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = SemiDirectEfficiencyDataGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void copyAlltoClipboard_SDE_Contributor()
        {

            SemiDirectEfficiencyContributorDataGrid.SelectAll();
            //Copy to clipboard
            SemiDirectEfficiencyContributorDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj2 = SemiDirectEfficiencyContributorDataGrid.GetClipboardContent();
            if (dataObj2 != null)
                Clipboard.SetDataObject(dataObj2);
        }

        private void copyAlltoClipboard_SDE_MonthlyResult()
        {

            SemiDirectEfficiencyDataGrid_Top.SelectAll();
            //Copy to clipboard
            SemiDirectEfficiencyDataGrid_Top.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj3 = SemiDirectEfficiencyDataGrid_Top.GetClipboardContent();
            if (dataObj3 != null)
                Clipboard.SetDataObject(dataObj3);
        }

        private void ExportMHData_SDE()
        {
            string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";
            System.IO.Directory.CreateDirectory(pathsss);

            copyAlltoClipboard_SDE_DailyResult();

            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //This is to export the daily result -------------------------------------------------
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            xlWorkSheet = xlWorkBook.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["A1", "D1"];
            oRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 242, 204));
            oRange.Font.Bold = true;

            //This is to export the monthly result --------------------------------------------------
            copyAlltoClipboard_SDE_Contributor();

            Microsoft.Office.Interop.Excel.Range CR2 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 6];
            CR2.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR2, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            Microsoft.Office.Interop.Excel.Range oRange2 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["F1", "H1"];
            oRange2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(214, 220, 228));
            oRange2.Font.Bold = true;

            //This is to export the monthly result------------------------------------------------------
            copyAlltoClipboard_SDE_MonthlyResult();
            Microsoft.Office.Interop.Excel.Range CR3 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 10];
            CR3.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR3, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            Microsoft.Office.Interop.Excel.Range oRange3 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["J1", "M1"];
            oRange3.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(214, 220, 228));
            oRange3.Font.Bold = true;
        }

        private void SemiDirectExportBtn_Click(object sender, EventArgs e)
        {
            ExportMHData_SDE();
        }

        private void TotalLossRateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TotalLossRateType.Text == "Overall Result")
            {
                TotalLossRateCategory.Enabled = false;
                TotalLossRateCategory.Text = "";
            }
            else
            {
                TotalLossRateCategory.Enabled = true;
                TLR_SectionDeptDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
            }
        }

        private void TotalLossRateGenerateBtn_Click(object sender, EventArgs e)
        {
            if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                if (TotalLossRateType.Text == "")
                {
                    MessageBox.Show("Please select type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TotalLossRateType.Select();
                }
                //else if (TotalEfficiencyPrinterDropdown.Text == "")
                //{
                //    MessageBox.Show("Please select printer type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                else
                {
                    if (TotalLossRateType.Text == "Overall Result")
                    {
                        GetTotalLossRateDailyResult();
                        GetTotalLossRateMonthlyResult();
                        GetTotalLossRateContributorResult();
                    }
                    else if (TotalLossRateType.Text == "Contributor Result")
                    {

                        if (TotalLossRateCategory.Text == "")
                        {
                            MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TotalLossRateCategory.Select();
                        }
                        else if (TotalLossRatePrinterDropdown.Text == "")
                        {
                            MessageBox.Show("Please select printer type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TotalLossRatePrinterDropdown.Select();
                        }
                        else
                        {
                            GetTotalLossRateDailyResult();
                            GetTotalLossRateMonthlyResult();
                            GetTotalLossRateContributorResult();
                        }
                    }
                }
            }
            else
            {
                if (TotalLossRateType.Text == "")
                {
                    MessageBox.Show("Please select type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TotalLossRateType.Select();
                }
                else
                {
                    if (TotalLossRateType.Text == "Overall Result")
                    {
                        GetTotalLossRateDailyResult();
                        GetTotalLossRateMonthlyResult();
                        GetTotalLossRateContributorResult();
                    }
                    else if (TotalLossRateType.Text == "Contributor Result")
                    {
                        if (TotalLossRateCategory.Text == "")
                        {
                            MessageBox.Show("Please select category.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TotalLossRateCategory.Select();
                        }
                        else
                        {
                            GetTotalLossRateDailyResult();
                            GetTotalLossRateMonthlyResult();
                            GetTotalLossRateContributorResult();
                        }
                    }
                }
            }
        }

        private void GetTotalLossRateDailyResult()
        {
            if (TotalLossRateDropdownEntries.Text == "All")
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectTotalLossRateDailyResult = new SqlCommand("SP_SelectTotalLossRateDailyResult", con);
                SelectTotalLossRateDailyResult.CommandType = CommandType.StoredProcedure;
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Section", TLR_SectionDeptDropdown.Text);
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@DateFrom", TotalLossRateDateFrom.Value.ToString());
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@DateTo", TotalLossRateDateTo.Value.ToString());
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Type", TotalLossRateType.Text);
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Category", TotalLossRateCategory.Text);
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@PrinterType", TotalLossRatePrinterDropdown.Text);
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectTotalLossRateDailyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TotalLossRateDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectTotalLossRateDailyResult = new SqlCommand("SP_SelectTotalLossRateDailyResult", con);
                SelectTotalLossRateDailyResult.CommandType = CommandType.StoredProcedure;
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Section", TLR_SectionDeptDropdown.Text);
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@DateFrom", TotalLossRateDateFrom.Value.ToString());
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@DateTo", TotalLossRateDateTo.Value.ToString());
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Type", TotalLossRateType.Text);
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Category", TotalLossRateCategory.Text);
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@PrinterType", TotalLossRatePrinterDropdown.Text);
                SelectTotalLossRateDailyResult.Parameters.AddWithValue("@Entries", Convert.ToInt32(TotalLossRateDropdownEntries.Text));
                SqlDataAdapter sda = new SqlDataAdapter(SelectTotalLossRateDailyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TotalLossRateDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void GetTotalLossRateMonthlyResult()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectLossRateMonthlyResult = new SqlCommand("SP_SelectTotalLossRateMonthlyResult", con);
            SelectLossRateMonthlyResult.CommandType = CommandType.StoredProcedure;
            SelectLossRateMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            //SelectLossRateMonthlyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
            SelectLossRateMonthlyResult.Parameters.AddWithValue("@DateFrom", TotalLossRateDateFrom.Value.ToString());
            SelectLossRateMonthlyResult.Parameters.AddWithValue("@DateTo", TotalLossRateDateTo.Value.ToString());
            SelectLossRateMonthlyResult.Parameters.AddWithValue("@Type", TotalLossRateType.Text);
            SelectLossRateMonthlyResult.Parameters.AddWithValue("@Category", TotalLossRateCategory.Text);
            SelectLossRateMonthlyResult.Parameters.AddWithValue("@PrinterType", TotalLossRatePrinterDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectLossRateMonthlyResult);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TotalLossRateDataGrid_Top.DataSource = dt;
            con.Close();
        }

        private void GetTotalLossRateContributorResult()
        {
            if (TotalLossRateDropdownEntries.Text == "All")
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectLossRateMonthlyResult = new SqlCommand("SP_SelectTotalLossRateContributorResult", con);
                SelectLossRateMonthlyResult.CommandType = CommandType.StoredProcedure;
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Procedure", "SelectAll");
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SelectLossRateMonthlyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@DateFrom", TotalLossRateDateFrom.Value.ToString());
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@DateTo", TotalLossRateDateTo.Value.ToString());
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Type", TotalLossRateType.Text);
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Category", TotalLossRateCategory.Text);
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@PrinterType", TotalLossRatePrinterDropdown.Text);
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Entries", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectLossRateMonthlyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TotalLossRateContributorDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                // Check Connection status -> Open connection if the current connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectLossRateMonthlyResult = new SqlCommand("SP_SelectTotalLossRateContributorResult", con);
                SelectLossRateMonthlyResult.CommandType = CommandType.StoredProcedure;
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SelectLossRateMonthlyResult.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@DateFrom", TotalLossRateDateFrom.Value.ToString());
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@DateTo", TotalLossRateDateTo.Value.ToString());
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Type", TotalLossRateType.Text);
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Category", TotalLossRateCategory.Text);
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@PrinterType", TotalLossRatePrinterDropdown.Text);
                SelectLossRateMonthlyResult.Parameters.AddWithValue("@Entries", Convert.ToInt32(TotalLossRateDropdownEntries.Text));
                SqlDataAdapter sda = new SqlDataAdapter(SelectLossRateMonthlyResult);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TotalLossRateContributorDataGrid.DataSource = dt;
                con.Close();
            }
        }


        private void TotalLossRateDropdownEntries_TextChanged(object sender, EventArgs e)
        {
            if (TotalLossRateDropdownEntries.Text == "")
            {
                MessageBox.Show("Please select or type number of entries.", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TotalLossRateDropdownEntries.Focus();
            }
            else
            {
                GetTotalLossRateDailyResult();
                GetTotalLossRateMonthlyResult();
                GetTotalLossRateContributorResult();
            }
        }



        private void copyAlltoClipboard_TLR_DailyResult()
        {

            TotalLossRateDataGrid.SelectAll();
            //Copy to clipboard
            TotalLossRateDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = TotalLossRateDataGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void copyAlltoClipboard_TLR_Contributor()
        {

            TotalLossRateContributorDataGrid.SelectAll();
            //Copy to clipboard
            TotalLossRateContributorDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj2 = TotalLossRateContributorDataGrid.GetClipboardContent();
            if (dataObj2 != null)
                Clipboard.SetDataObject(dataObj2);
        }

        private void copyAlltoClipboard_TLR_MonthlyResult()
        {

            TotalLossRateDataGrid_Top.SelectAll();
            //Copy to clipboard
            TotalLossRateDataGrid_Top.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj3 = TotalLossRateDataGrid_Top.GetClipboardContent();
            if (dataObj3 != null)
                Clipboard.SetDataObject(dataObj3);
        }

        private void ExportMHData_TLR()
        {
            string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";
            System.IO.Directory.CreateDirectory(pathsss);

            copyAlltoClipboard_TLR_DailyResult();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //This is to export the daily result -------------------------------------------------
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            xlWorkSheet = xlWorkBook.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["A1", "D1"];
            oRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 242, 204));
            oRange.Font.Bold = true;

            //This is to export the monthly result --------------------------------------------------
            copyAlltoClipboard_TLR_Contributor();
            Microsoft.Office.Interop.Excel.Range CR2 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 6];
            CR2.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR2, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            Microsoft.Office.Interop.Excel.Range oRange2 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["F1", "H1"];
            oRange2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(214, 220, 228));
            oRange2.Font.Bold = true;

            //This is to export the monthly result------------------------------------------------------
            copyAlltoClipboard_TLR_MonthlyResult();
            Microsoft.Office.Interop.Excel.Range CR3 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 10];
            CR3.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR3, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            Microsoft.Office.Interop.Excel.Range oRange3 = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Range["J1", "M1"];
            oRange3.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(214, 220, 228));
            oRange3.Font.Bold = true;
        }

        private void TotalLossRateExportBtn_Click(object sender, EventArgs e)
        {
            ExportMHData_TLR();
        }

        private void TopContributorBtn_Click(object sender, EventArgs e)
        {
            TopContributorForm TopContributorForm = new TopContributorForm();
            TopContributorForm.ShowDialog();
        }

        private void DirectEfficiencySearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (DE_SearchDropdownList.Text == "--Select--")
                {
                    MessageBox.Show("Please select search category first before press enter.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SearchDirectEfficiencyData();
                }

            }
        }

        private void SearchDirectEfficiencyData()
        {

            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SearchEfficiencyData = new SqlCommand("SP_SearchProdEfficiencyData", con);
            SearchEfficiencyData.CommandType = CommandType.StoredProcedure;
            SearchEfficiencyData.Parameters.AddWithValue("@Procedure", "Search_DirectEfficiencyData");
            SearchEfficiencyData.Parameters.AddWithValue("@Category", DE_SearchDropdownList.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@Section", DE_SectionDeptDropdown.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@Search", DirectEfficiencySearch.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@DateFrom", DirectEffDateFrom.Value.ToString());
            SearchEfficiencyData.Parameters.AddWithValue("@DateTo", DirectEffDateTo.Value.ToString());
            //SearchEfficiencyData.Parameters.AddWithValue("@Entries", "");
            SqlDataAdapter sda = new SqlDataAdapter(SearchEfficiencyData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DirectEfficiencyDataGrid.DataSource = dt;
            con.Close();

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("No data Found!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel74_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SemiDirectSearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (SDE_SearchDropdownList.Text == "--Select--")
                {
                    MessageBox.Show("Please select search category first before press enter.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SearchSemiDirectData();
                }

            }
        }

        private void SearchSemiDirectData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SearchEfficiencyData = new SqlCommand("SP_SearchProdEfficiencyData", con);
            SearchEfficiencyData.CommandType = CommandType.StoredProcedure;
            SearchEfficiencyData.Parameters.AddWithValue("@Procedure", "Search_SemiDirectData");
            SearchEfficiencyData.Parameters.AddWithValue("@Category", SDE_SearchDropdownList.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@Section", SDR_SectionDeptDropdown.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@Search", SemiDirectSearchBox.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@DateFrom", SemiDirectRateDateFrom.Value.ToString());
            SearchEfficiencyData.Parameters.AddWithValue("@DateTo", SemiDirectRateDateTo.Value.ToString());
            //SearchEfficiencyData.Parameters.AddWithValue("@Entries", "");
            SqlDataAdapter sda = new SqlDataAdapter(SearchEfficiencyData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SemiDirectEfficiencyDataGrid.DataSource = dt;
            con.Close();

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("No data Found!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TotalLossRateSearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (TLR_SearchDropdownList.Text == "--Select--")
                {
                    MessageBox.Show("Please select search category first before press enter.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SearchTotalLossRateData();
                }

            }
        }
        private void SearchTotalLossRateData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SearchEfficiencyData = new SqlCommand("SP_SearchProdEfficiencyData", con);
            SearchEfficiencyData.CommandType = CommandType.StoredProcedure;
            SearchEfficiencyData.Parameters.AddWithValue("@Procedure", "Search_TotalLossRateData");
            SearchEfficiencyData.Parameters.AddWithValue("@Category", TLR_SearchDropdownList.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@Section", TLR_SectionDeptDropdown.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@Search", TotalLossRateSearchBox.Text);
            SearchEfficiencyData.Parameters.AddWithValue("@DateFrom", TotalLossRateDateFrom.Value.ToString());
            SearchEfficiencyData.Parameters.AddWithValue("@DateTo", TotalLossRateDateTo.Value.ToString());
            //SearchEfficiencyData.Parameters.AddWithValue("@Entries", "");
            SqlDataAdapter sda = new SqlDataAdapter(SearchEfficiencyData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TotalLossRateDataGrid.DataSource = dt;
            con.Close();

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("No data Found!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void TotalEfficiencySearchBox_MouseEnter(object sender, EventArgs e)
        {
            TotalEfficiencySearchBox.Text = "";
        }

        private void TotalEfficiencySearchBox_MouseLeave(object sender, EventArgs e)
        {
            TotalEfficiencySearchBox.Text = "Search...";
        }

        private void DirectEfficiencySearch_MouseEnter(object sender, EventArgs e)
        {
            DirectEfficiencySearch.Text = "";
        }

        private void DirectEfficiencySearch_MouseLeave(object sender, EventArgs e)
        {
            DirectEfficiencySearch.Text = "Search...";
        }

        private void SemiDirectSearchBox_MouseEnter(object sender, EventArgs e)
        {
            SemiDirectSearchBox.Text = "";
        }

        private void SemiDirectSearchBox_MouseLeave(object sender, EventArgs e)
        {
            SemiDirectSearchBox.Text = "Search...";
        }

        private void TotalLossRateSearchBox_MouseEnter(object sender, EventArgs e)
        {
            TotalLossRateSearchBox.Text = "";
        }

        private void TotalLossRateSearchBox_MouseLeave(object sender, EventArgs e)
        {
            TotalLossRateSearchBox.Text = "Search...";
        }

        private void ViewTotalEfficientcyGraph_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_Overall_TotalEfficiencyResult/OverallTotalEfficiencyResult?:iid=4");
        }

        public static bool isOverallResult = false;
        private void DirectEfficiencyViewGraphBtn_Click(object sender, EventArgs e)
        {
            isOverallResult = true;

            DirectEfficiencyGraphForm DirectEfficiencyGraphForm = new DirectEfficiencyGraphForm();
            DirectEfficiencyGraphForm.ShowDialog();
        }

        private void SemiDirectViewGraphButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_Overall_Semi-directResult/OverallSemi-directEfficiencyResult?:iid=3");
        }

        private void TotalLossRateViewGraphBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_Overall_TotalLossRateResult/OverallTotalLossRateResult?:iid=2");
        }

        private void ViewGraphSummryButton_Click(object sender, EventArgs e)
        {
            ProdEfficiencyGraphForm ProdEfficiencyGraphForm = new ProdEfficiencyGraphForm();
            ProdEfficiencyGraphForm.ShowDialog();
        }

        private void SemiDirectEfficiencyDataGrid_Top_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in SemiDirectEfficiencyDataGrid_Top.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            SemiDirectEfficiencyDataGrid_Top.EnableHeadersVisualStyles = false;
            SemiDirectEfficiencyDataGrid_Top.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            SemiDirectEfficiencyDataGrid_Top.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void SemiDirectEfficiencyContributorDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in SemiDirectEfficiencyContributorDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            SemiDirectEfficiencyContributorDataGrid.EnableHeadersVisualStyles = false;
            SemiDirectEfficiencyContributorDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            SemiDirectEfficiencyContributorDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void SemiDirectEfficiencyDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in SemiDirectEfficiencyDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            SemiDirectEfficiencyDataGrid.EnableHeadersVisualStyles = false;
            SemiDirectEfficiencyDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            SemiDirectEfficiencyDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void TotalLossRateDataGrid_Top_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in TotalLossRateDataGrid_Top.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            TotalLossRateDataGrid_Top.EnableHeadersVisualStyles = false;
            TotalLossRateDataGrid_Top.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            TotalLossRateDataGrid_Top.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void TotalLossRateContributorDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in TotalLossRateContributorDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            TotalLossRateContributorDataGrid.EnableHeadersVisualStyles = false;
            TotalLossRateContributorDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            TotalLossRateContributorDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void TotalLossRateDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in TotalLossRateDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            TotalLossRateDataGrid.EnableHeadersVisualStyles = false;
            TotalLossRateDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Navy;
            TotalLossRateDataGrid.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = Color.White;
        }

        private void SemiDirectCatedoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TTLossRateDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

        private void NextBtn_Click(object sender, EventArgs e)
        {
            Page1.Dock = DockStyle.Fill;
            Page1.Visible = false;

            Page2.Dock = DockStyle.Fill;
            Page2.Visible = true;

            //Load Efficiency Summary Data
            SelectTotalEfficiencyData();
            SelectDirectEfficiencyData();
            SelectSemiDirectRateData();
            SelectTotalLossRateData();

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Page2.Dock = DockStyle.Fill;
            Page2.Visible = false;

            Page1.Dock = DockStyle.Fill;
            Page1.Visible = true;
           

            //Load Efficiency Summary Data
            SelectTotalEfficiencyData();
            SelectDirectEfficiencyData();
            SelectSemiDirectRateData();
            SelectTotalLossRateData();
        }


        private void ProductionEfficiencyForm_ResizeEnd(object sender, EventArgs e)
        {
            Page1.Dock = DockStyle.Fill;
            Page1.Dock = DockStyle.Fill;

            //Load Efficiency Summary Data
            SelectTotalEfficiencyData();
            SelectDirectEfficiencyData();
            SelectSemiDirectRateData();
            SelectTotalLossRateData();
        }

        private void SectionDropsown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load Efficiency Summary Data
            SelectTotalEfficiencyData();
            SelectDirectEfficiencyData();
            SelectSemiDirectRateData();
            SelectTotalLossRateData();
        }

        private void LoadSection()
        {
            con.Open();
            SqlCommand LoadSection = new SqlCommand("SP_LoadProductionSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            SectionDropdown.ValueMember = "Section";
        }

        private void SectionDropdown_DropDownClosed(object sender, EventArgs e)
        {
           
        }

        private void SectionDropdown_DragEnter(object sender, EventArgs e)
        {
           
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == TotalEffGraphTab)
            {
                // Ensure WebView2 is properly initialized.
                await webView21.EnsureCoreWebView2Async(null);

                // Perform action for "Per Production Section"
                webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_Overall_TotalEfficiencyResult/OverallTotalEfficiencyResult?:iid=4");

            }
            else if (tabControl1.SelectedTab == SemiDirectGraphTab)
            {
                // Ensure WebView2 is properly initialized.
                await webView22.EnsureCoreWebView2Async(null);

                // Perform action for "Per Production Section"
                webView22.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_Overall_Semi-directResult/OverallSemi-directEfficiencyResult?:iid=3");

            }
            else if (tabControl1.SelectedTab == TotalLossRateGraphTab)
            {
                // Ensure WebView2 is properly initialized.
                await webView23.EnsureCoreWebView2Async(null);

                // Perform action for "Per Production Section"
                webView23.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_Overall_TotalLossRateResult/OverallTotalLossRateResult?:iid=2");

            }
        }
    }
}
