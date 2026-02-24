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
using System.Diagnostics;
using MHMS.Connection;
using OfficeOpenXml;
using ClosedXML.Excel;

namespace MHMS.Forms
{
    public partial class COPQManhourLossForm : Form
    {

        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public COPQManhourLossForm()
        {
            InitializeComponent();
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void AddCOPQButton_Click(object sender, EventArgs e)
        {

        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DateFrom()
        {
            DateTime now = DateTime.Now;
            FromDateTimePicker.Value = new DateTime(now.Year, now.Month, 1);
        }

        private void DateTo()
        {
            DateTime datenow = DateTime.Now;
            ToDateTimePicker.Value = datenow;
        }// <---- end

        //====================================================================================================================>>>>>>>>>>>>
        public static string SelectedSection;
        private void COPQManhourLossForm_Load(object sender, EventArgs e)
        {

            MHLossDataGridView.EnableHeadersVisualStyles = false;
            MHLossDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(86, 119, 157);
            MHLossDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            

            //Hide Row header in datagrid
            MHLossDataGridView.RowHeadersVisible = false;

            //Hide Send mail button to othet section only PE section can see
            if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
            {
                SendEmailButton.Visible = true;
                ExportPreviousDataBtn.Visible = true;
                LogsButton.Visible = true; //show logs button
                LogArrow.Visible = true;
                CheckDuplicatedLineStopBtn.Visible = true;
                RejectedMHLossBtn.Visible = true;
            }
            else
            {
                SendEmailButton.Visible = false;
                ExportPreviousDataBtn.Visible = false;
                LogsButton.Visible = false; //Hide logs button
                LogArrow.Visible = false;
                CheckDuplicatedLineStopBtn.Visible = false;
                RejectedMHLossBtn.Visible = false;
            }

           

            DateFrom(); // Call out the function for Date From and show when the form is loaded

            DateTo();


            if (LoginForm.isSingleSectionAccess == true)
            {
                if (LoginForm.UserSection == "BPS")
                {
                    //LoadSection();
                    SelectedSection = SectionDropdown.Text;
                    SectionDropdown.Text = LoginForm.UserSection;
                }
                else if (LoginForm.UserSection == "Quality Innovation")
                {
                    SelectedSection = SectionDropdown.Text;
                    SectionDropdown.Text = "";
                }
                else
                {
                    SectionDropdown.Text = LoginForm.UserSection;
                    SectionDropdown.Enabled = false;
                }

                SectionMenuForm.isMultiSectionAccess = false;
            }

            if (SectionMenuForm.isMultiSectionAccess == true)
            {
                if (SectionMenuForm.UserSection == "BPS")
                {
                    //LoadSection();
                    SelectedSection = SectionDropdown.Text;
                    SectionDropdown.Text = LoginForm.UserSection;
                }
                else if (LoginForm.UserSection == "Quality Innovation")
                {
                    SelectedSection = SectionDropdown.Text;
                    SectionDropdown.Text = "";
                }
                else
                {
                    SectionDropdown.Text = SectionMenuForm.UserSection;
                    SectionDropdown.Enabled = false;
                }
                
                LoginForm.isSingleSectionAccess = false;
            }

            //LoadMHLossData(); // Load all MH loss data to datagrid view

            /* FormatHeaderText();*/ // Format datagridview column header text

            //SelectMHDataBaseOnDropdownEntries();

            HideMonthlyStandardMHButton();

            RenameExportPreviousBtn();

            SynchStandarMH();
        }

        private void SynchStandarMH()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            //Count For approval per section pic
            SqlCommand SelectStandardMH = new SqlCommand("SP_SelectMonthlyStandardMH", con);
            SelectStandardMH.CommandType = CommandType.StoredProcedure;
            SelectStandardMH.Parameters.AddWithValue("@Month", DateTime.Now.ToString("MMMM"));

            if (DateTime.Now.ToString("MMMM") == "January")
            {
                SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
            }
            else if (DateTime.Now.ToString("MMMM") == "February")
            {
                SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
            }
            else if (DateTime.Now.ToString("MMMM") == "March")
            {
                SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
            }
            else
            {
                SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.ToString("yyyy"));
            }

            SqlDataAdapter sda2 = new SqlDataAdapter(SelectStandardMH);
            DataTable dataTable = new DataTable();
            sda2.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                SqlDataReader reader2 = SelectStandardMH.ExecuteReader();
                while (reader2.Read())
                {
                    StandardMHTextBox.Text = reader2["StandardMH"].ToString();
                }
            }
            else
            {
                
                StandardMHTextBox.Text = "No Standard MH";
                StandardMHTextBox.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            }

            con.Close();
        }

        private void RenameExportPreviousBtn()
        {
            if (DateTime.Now.Month.ToString() == "1")
            {
                ExportPreviousDataBtn.Text = "EXPORT DECEMBER DATA";
            }
            else if (DateTime.Now.Month.ToString() == "2")
            {
                ExportPreviousDataBtn.Text = "EXPORT JANUARY DATA";
            }
            else if (DateTime.Now.Month.ToString() == "3")
            {
                ExportPreviousDataBtn.Text = "EXPORT FEBRUARY DATA";
            }
            else if (DateTime.Now.Month.ToString() == "4")
            {
                ExportPreviousDataBtn.Text = "EXPORT MARCH DATA";
            }
            else if (DateTime.Now.Month.ToString() == "5")
            {
                ExportPreviousDataBtn.Text = "EXPORT APRIL DATA";
            }
            else if (DateTime.Now.Month.ToString() == "6")
            {
                ExportPreviousDataBtn.Text = "EXPORT MAY DATA";
            }
            else if (DateTime.Now.Month.ToString() == "7")
            {
                ExportPreviousDataBtn.Text = "EXPORT JUNE DATA";
            }
            else if (DateTime.Now.Month.ToString() == "8")
            {
                ExportPreviousDataBtn.Text = "EXPORT JULY DATA";
            }
            else if (DateTime.Now.Month.ToString() == "9")
            {
                ExportPreviousDataBtn.Text = "EXPORT AUGUST DATA";
            }
            else if (DateTime.Now.Month.ToString() == "10")
            {
                ExportPreviousDataBtn.Text = "EXPORT SEPTEMBER DATA";
            }
            else if (DateTime.Now.Month.ToString() == "11")
            {
                ExportPreviousDataBtn.Text = "EXPORT OCTOBER DATA";
            }
            else if (DateTime.Now.Month.ToString() == "12")
            {
                ExportPreviousDataBtn.Text = "EXPORT NOVEMBER DATA";
            }
           
        }
        //==================================================================================================================>>>>>>>>>>>>

        private void HideMonthlyStandardMHButton()
        {
            if (LoginForm.UserSection == "BPS")
            {
                StandardMHButton.Visible = true;
            }
            else
            {
                StandardMHButton.Visible = false;
            }
        }

        //==================================================================================================================>>>>>>>>>>>>

        public void LoadSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            try
            {
                // SQL query to select User Account
                using (SqlCommand loadSectionCmd = new SqlCommand("SP_LoadSection", con))
                {
                    loadSectionCmd.CommandType = CommandType.StoredProcedure;
                    loadSectionCmd.Parameters.AddWithValue("@Procedure", "SelectAllProdSections");

                    using (SqlDataAdapter sda = new SqlDataAdapter(loadSectionCmd))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        // Bind data to the dropdown
                        SectionDropdown.DataSource = ds.Tables[0];
                        SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString(); // Set the column to display
                        SectionDropdown.ValueMember = ds.Tables[0].Columns[0].ToString();  // Set the value for the selection
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions here
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close(); // Ensure connection is closed
            }
        }


        //==================================================================================================================>>>>>>>>>>>>

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (Dashboard.SectionText == "BIPH-BPS")
            {
                UpdateDataButton.Enabled = true;
                UpdateMHLoss2 UpdateData = new UpdateMHLoss2();
                UpdateData.ShowDialog();
                //UpdateDataButton.BackColor = Color.FromArgb(21, 35, 53);
            }
            else
            {
                //UpdateDataButton.Enabled = false;
                MessageBox.Show("Sorry, Only admin can update MH data!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DisableSectionDropdown()
        {
            SectionDropdown.Enabled = false;
            SectionDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LoadMHLossData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (LoginForm.UserSection == "BPS")
            {
                SelectMHLossData();

                //// -> SQL query to select MH loss data
                //SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                //SelectMHLossData.CommandType = CommandType.StoredProcedure;
                //SelectMHLossData.Parameters.AddWithValue("@Procedure", "ViewAllMHLossData");
                //SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                //SelectMHLossData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                //SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                //MHLossDataGridView.DataSource = dt;
                //con.Close();
            }
            else
            {
                DisableSectionDropdown();

                // -> SQL query to select MH loss data
                SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                SelectMHLossData.CommandType = CommandType.StoredProcedure;
                SelectMHLossData.Parameters.AddWithValue("@Procedure", "ViewMHLossDataBySection");
                SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SelectMHLossData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                MHLossDataGridView.DataSource = dt;
                con.Close();
            }

           
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchMHLossData();
            }
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void FormatHeaderText()
        {
            MHLossDataGridView.Columns["DateEncountered"].HeaderText = "Date Encountered";
            MHLossDataGridView.Columns["Section"].HeaderText = "Section";
            MHLossDataGridView.Columns["CostCenter"].HeaderText = "Cost Center";
            MHLossDataGridView.Columns["ModelName"].HeaderText = "Model Name";
            MHLossDataGridView.Columns["LossFactor"].HeaderText = "Loss Factor";
            MHLossDataGridView.Columns["ResponsibleSection"].HeaderText = "Responsible Section";
            MHLossDataGridView.Columns["LineStopDetail"].HeaderText = "Reason (Line Stop Detail)";
            MHLossDataGridView.Columns["StopTime"].HeaderText = "Stop Time";
            MHLossDataGridView.Columns["DirectMP"].HeaderText = "Direct MP";
            MHLossDataGridView.Columns["SemiDirectMP"].HeaderText = "Semi-Direct MP";
            MHLossDataGridView.Columns["LossManhour"].HeaderText = "Loss Manhour";
            MHLossDataGridView.Columns["Reason"].HeaderText = "Reason";
            MHLossDataGridView.Columns["TypeOfLoss"].HeaderText = "Type of Loss";
            //MHLossDataGridView.Columns["COPQAmount"].HeaderText = "COPQ Amount";
            MHLossDataGridView.Columns["DateIssued"].HeaderText = "Date Issued";
            MHLossDataGridView.Columns["Cause"].HeaderText = "Cause";
            MHLossDataGridView.Columns["Countermeasure"].HeaderText = "Countermeasure (if accepted) / Reason (if rejected)";
            MHLossDataGridView.Columns["ApplyingStatus"].HeaderText = "Applying Status";
            MHLossDataGridView.Columns["ReceivingStatus"].HeaderText = "Receiving Status";
            MHLossDataGridView.Columns["QIConfirmation"].HeaderText = "QI Confirmation";
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void SearchMHLossData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SearchMHLossData = new SqlCommand("SP_SearchMHLossData", con);
            SearchMHLossData.CommandType = CommandType.StoredProcedure;
            SearchMHLossData.Parameters.AddWithValue("@Type", TypeDropdown.Text);
            SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
            SearchMHLossData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MHLossDataGridView.DataSource = dt;
            con.Close();
            
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            

            /* FormatHeaderText();*/ // Format header text
            //SearchBox.Clear(); // Clear text box

        }

        //==================================================================================================================>>>>>>>>>>>>

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (MHLossDataGridView.DataSource == null)
            {
                MessageBox.Show("No data found! Please generate data first.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ExportMHData();
            }
            
        }

        private void copyAlltoClipboardsss()
        {

            MHLossDataGridView.SelectAll();
            //Copy to clipboard
            MHLossDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = MHLossDataGridView.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ExportMHData()
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
            // xlWorkSheet.Cells[3, "XL"].Cells.NumberFormat = "@";
            CR.Select();
            xlWorkSheet.Cells.NumberFormat = "@";

            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

           
        }
        //==================================================================================================================>>>>>>>>>>>>

        private void SelectMHDataBaseOnDropdownEntries()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (LoginForm.UserSection == "BPS")
            {
                SelectMHLossData();

                //if (DropdownEntriesValue.Text == "All")
                //{

                //    SqlCommand SelectMHData = new SqlCommand("SP_SelectMHDataBaseOnDropdownEntries", con);
                //    SelectMHData.CommandType = CommandType.StoredProcedure;
                //    SelectMHData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                //    SelectMHData.Parameters.AddWithValue("@SubProcedure", "SelectAll");
                //    SelectMHData.Parameters.AddWithValue("@Value", "");
                //    SelectMHData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHData);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    MHLossDataGridView.DataSource = dt;
                //    con.Close();
                //}
                //else
                //{
                    
                //    SqlCommand SelectMHData = new SqlCommand("SP_SelectMHDataBaseOnDropdownEntries", con);
                //    SelectMHData.CommandType = CommandType.StoredProcedure;
                //    SelectMHData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                //    SelectMHData.Parameters.AddWithValue("@SubProcedure", "SelectBasedOnEntriesValue");
                //    SelectMHData.Parameters.AddWithValue("@Value", DropdownEntriesValue.Text);
                //    SelectMHData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHData);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    MHLossDataGridView.DataSource = dt;
                //    con.Close();
                //}
            }
            else
            {
                if (DropdownEntriesValue.Text == "All")
                {
                    
                    SqlCommand SelectMHData = new SqlCommand("SP_SelectMHDataBaseOnDropdownEntries", con);
                    SelectMHData.CommandType = CommandType.StoredProcedure;
                    SelectMHData.Parameters.AddWithValue("@Procedure", "SelectMHLossDataBySection");
                    SelectMHData.Parameters.AddWithValue("@SubProcedure", "SelectAll");
                    SelectMHData.Parameters.AddWithValue("@Value", "");
                    SelectMHData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SelectMHData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectMHData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(SelectMHData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MHLossDataGridView.DataSource = dt;
                    con.Close();
                }
                else
                {
                  
                    SqlCommand SelectMHData = new SqlCommand("SP_SelectMHDataBaseOnDropdownEntries", con);
                    SelectMHData.CommandType = CommandType.StoredProcedure;
                    SelectMHData.Parameters.AddWithValue("@Procedure", "SelectMHLossDataBySection");
                    SelectMHData.Parameters.AddWithValue("@SubProcedure", "SelectBasedOnEntriesValue");
                    SelectMHData.Parameters.AddWithValue("@Value", DropdownEntriesValue.Text);
                    SelectMHData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SelectMHData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectMHData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(SelectMHData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MHLossDataGridView.DataSource = dt;
                    con.Close();
                }
            }
           
        }

        //==================================================================================================================>>>>>>>>>>>>

        private async void DropdownEntriesValue_TextChanged(object sender, EventArgs e)
        {
            if (TypeDropdown.Text == "")
            {
                MessageBox.Show("Please select the type.", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TypeDropdown.Focus();
            }
            else if (DropdownEntriesValue.Text != "")
            {
                //SelectMHDataBaseOnDropdownEntries();
                await FilterDataBySelectedRangeOfDate();
                DropdownEntriesValue.ForeColor = Color.FromArgb(21, 35, 53);
            }
            else
            { }
            
        }

        //==================================================================================================================>>>>>>>>>>>>
        public static bool IsGenerateBtnClick = false;
        private async void GenerateButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please select section!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SectionDropdown.Focus();
            }
            else if (TypeDropdown.Text == "")
            {
                MessageBox.Show("Please select the type!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TypeDropdown.Focus();
            }
            else
            {
                IsGenerateBtnClick = true;

                LoadingForm LoadingForm = new LoadingForm();
                LoadingForm.Show();

                // Disable the button or other controls if needed to prevent user interaction
                GenerateButton.Enabled = false;

                try
                {
                    await Task.Delay(5000);

                    if (TypeDropdown.Text == "Receiving")
                    {
                        LossRatePanel.Height = 150;
                        LossRateDataGrid.Visible = true;
                        ViewGraphButton.Visible = false;
                        LossRateDataGrid.Dock = DockStyle.Fill;

                        //LossRateDropdownList.Visible = true;

                        await SelectLossRateData(); //Show loss rate data

                        //Hide Column in loss rate data grid view
                        //LossRateDataGrid.Columns["ID"].Visible = false;
                        //LossRateDataGrid.Columns["Fiscal Year"].Visible = false;
                        //LossRateDataGrid.Columns["UploadBy"].Visible = false;
                        //LossRateDataGrid.Columns["UploadDate"].Visible = false;

                        //Change column header back color
                        LossRateDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(189, 225, 255);
                        LossRateDataGrid.Columns["Target Rate"].HeaderCell.Style.BackColor = Color.FromArgb(242, 213, 157);
                        LossRateDataGrid.Columns["Actual Rate"].HeaderCell.Style.BackColor = Color.FromArgb(87, 119, 255);
                        LossRateDataGrid.Columns["Actual Rate"].HeaderCell.Style.ForeColor = Color.White;

                        LossRateDataGrid.EnableHeadersVisualStyles = false;


                    }
                    else
                    {
                        ViewGraphButton.Visible = true;
                        LossRatePanel.Height = MinimumSize.Height;
                        LossRateDataGrid.Visible = false;
                        ViewGraphButton.Visible = true;

                    }

                    SelectedSection = SectionDropdown.Text;

                    await FilterDataBySelectedRangeOfDate();

                    //Change column name of "Part Code"
                    MHLossDataGridView.Columns["Part Code"].HeaderText = "Item Code";



                    SearchBox.Clear();
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the async operation
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                {
                    // Hide the loading image when the operation is complete
                    LoadingForm.Close();

                    // Re-enable the button or controls
                    GenerateButton.Enabled = true;
                }




            }
            
        }

        //==================================================================================================================>>>>>>>>>>>>

        private async Task SelectLossRateData()
        {
            await con.OpenAsync();
            try
            {
                SqlCommand SelectLossRateData = new SqlCommand("SP_SelectLossRateData", con);
                SelectLossRateData.CommandType = CommandType.StoredProcedure;
                //SelectLossRateData.Parameters.AddWithValue("@DopdownItem", LossRateDropdownList.Text);
                SelectLossRateData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectLossRateData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                LossRateDataGrid.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
           
        }


        //==================================================================================================================>>>>>>>>>>>>

        public async Task FilterDataBySelectedRangeOfDate()
        {
            await con.OpenAsync();

            try
            {
                if (DropdownEntriesValue.Text == "All")
                {
                    if (TypeDropdown.Text == "Applying")
                    {
                        // -> SQL query to select MH data base on selected entries
                        SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                        SelectMHDataBaseOnSelectedDetails.CommandTimeout = 100; // Set the command timeout here - Set timeout to 100 seconds
                        SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "FilterBySectionAndDateAll");
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", "");
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;

                    }
                    else if (TypeDropdown.Text == "Receiving")
                    {
                        // -> SQL query to select MH data base on selected entries
                        SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                        SelectMHDataBaseOnSelectedDetails.CommandTimeout = 100; // Set the command timeout here - Set timeout to 100 seconds
                        SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "FilterBySectionAndDateAll");
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", "");
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;

                    }
                }
                else
                {
                    if (TypeDropdown.Text == "Applying")
                    {
                        // -> SQL query to select MH data base on selected entries
                        SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                        SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "FilterBySectionAndDateRange");
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", Convert.ToInt32(DropdownEntriesValue.Text));
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;

                    }
                    else if (TypeDropdown.Text == "Receiving")
                    {
                        // -> SQL query to select MH data base on selected entries
                        SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                        SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "FilterBySectionAndDateRange");
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", Convert.ToInt32(DropdownEntriesValue.Text));
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally 
            {
                con.Close();
            }

        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SectionDropdown_TextChanged(object sender, EventArgs e)
        {
            //SelectMHLossData();

            //// Check Connection status -> Open connection if the connection is closed
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            //if (SectionDropdown.Text == "Ink Cartridge")
            //{
            //    // -> SQL query to select Ink cartridge MH loss data
            //    SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
            //    SelectMHLossData.CommandType = CommandType.StoredProcedure;
            //    SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInCartridgeMHLoss");
            //    SelectMHLossData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            //    SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
            //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    MHLossDataGridView.DataSource = dt;
            //    con.Close();
            //}
            //else if (SectionDropdown.Text == "Ink Head")
            //{
            //    // -> SQL query to select Ink Head MH loss data
            //    SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
            //    SelectMHLossData.CommandType = CommandType.StoredProcedure;
            //    SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInCartridgeMHLoss");
            //    SelectMHLossData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            //    SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
            //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    MHLossDataGridView.DataSource = dt;
            //    con.Close();
            //}

        }

        private void SelectMHLossData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (SectionDropdown.Text == "Ink Cartridge")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Cartridge");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Cartridge");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }

                }
                else if (SectionDropdown.Text == "Tape Cassette")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Tape Cassette");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select tape cassette parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Tape Cassette");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Ink Head")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Head");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select ink head parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInkHeadMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Head");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Molding Production")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Molding");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select molding parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectMoldingMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Molding");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "PCBA")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "PCBA");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select PCBA parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPCBAMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "PCBA");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Printer")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Printer");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select Printer parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPrinterMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Printer");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "BPS")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "BPS");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select Production Engineering parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectBPSMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "BPS");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "P-Touch")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "P-Touch");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select P-touch parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPTouchMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "P-Touch");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
            }
            else if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (SectionDropdown.Text == "Ink Cartridge")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Cartridge");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Cartridge");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }

                }
                else if (SectionDropdown.Text == "Tape Cassette")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Tape Cassette");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select tape cassette parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Tape Cassette");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Ink Head")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Head");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select ink head parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInkHeadMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Head");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Molding Production")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Molding");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select molding parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectMoldingMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Molding");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "PCBA")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "PCBA");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select PCBA parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPCBAMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "PCBA");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Printer")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Printer");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select Printer parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPrinterMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Printer");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "BPS")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "BPS");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select Production Engineering parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectBPSMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "BPS");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "P-Touch")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "P-Touch");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select P-touch parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPTouchMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "P-Touch");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void MHLossDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in MHLossDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateMHLoss2 sample = new UpdateMHLoss2();
            sample.ShowDialog();
        }

        private void LossRateButton_Click(object sender, EventArgs e)
        {
            MonthlyStandardManHour StandardMH = new MonthlyStandardManHour();
            StandardMH.ShowDialog();
        }

        public static bool HaveNewUploadedData = false;
        private void RefreshDatagridTimer_Tick(object sender, EventArgs e)
        {
            if (HaveNewUploadedData == true)
            {
                LoadMHLossData();

                HaveNewUploadedData = false;
            }
        }

        private void ViewGraphButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://bi.datalake.brother.co.jp/t/biph/views/ManhourLossReport/IssuedLossRate?:origin=card_share_link&:embed=n");
        }

        private void SectionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FilterDataBySelectedRangeOfDate();
        }

        private void LossRateDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            LossRateDataGrid.Columns["Section"].Width = 150;

            foreach (DataGridViewColumn column in LossRateDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection();
        }

        private void LossRateDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectLossRateData(); //Show loss rate data
        }

        private void ApprovedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ApprovedCheckBox.Checked == true)
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "Approved")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(87, 222, 155);
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "Approved")
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void ForApprovalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ForApprovalCheckBox.Checked == true)
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "For Approval")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(222, 217, 87);
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "For Approval")
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void RejectedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RejectedCheckBox.Checked == true)
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "Rejected")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(222, 87, 109);
                    }
                }

            }
            else
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "Rejected")
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void SendEmailButton_Click(object sender, EventArgs e)
        {
            ExportCurrenMonthData();

        }

        private void ExportCurrenMonthData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_SelectMHLossDataOfCurrentMonth", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 100;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                // Get previous month name and year
                // Get current month name and year
                DateTime currentMonth = DateTime.Now;
                string currentMonthName = currentMonth.ToString("MMMM yyyy");
                string currentMonthFile = currentMonth.ToString("yyyyMM");  


                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Excel Workbook|*.xlsx",
                    Title = $"MH Loss Data - {currentMonthName}",
                    FileName = $"MH_LossData_{currentMonthFile}.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "MH Loss Data");
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No data found to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LogsButton_Click(object sender, EventArgs e)
        {
            MHReportLogsForm logsForm = new MHReportLogsForm();
            logsForm.ShowDialog();
        }

        private void ExportPreviousDataBtn_Click(object sender, EventArgs e)
        {
            ExportPreviousData();
        }

        private void ExportPreviousData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_SelectMHLossDataOfPreviousMonth", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 100;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                // Get previous month name and year
                DateTime prevMonth = DateTime.Now.AddMonths(-1);
                string prevMonthName = prevMonth.ToString("MMMM yyyy"); 
                string prevMonthFile = prevMonth.ToString("yyyyMM");

                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Excel Workbook|*.xlsx",
                    Title = $"MH Loss Data - {prevMonthName}",
                    FileName = $"MH_LossData_{prevMonthFile}.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "MH Loss Data");
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No data found to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //private void ExportPreviousData()
        //{
        //    con.Open();
        //    // -> SQL query to select MH data base on selected entries
        //    SqlCommand SelectMHLossDataOfPreviousMonth = new SqlCommand("SP_SelectMHLossDataOfPreviousMonth", con);
        //    SelectMHLossDataOfPreviousMonth.CommandTimeout = 100; // Set the command timeout here - Set timeout to 120 seconds
        //    SelectMHLossDataOfPreviousMonth.CommandType = CommandType.StoredProcedure;
        //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossDataOfPreviousMonth);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    MHLossDataGridView.DataSource = dt;
        //    con.Close();

        //    ExportMHData(); // Export all data in datagrid

        //    MHLossDataGridView.DataSource = null; //Clear datagrid source
        //}


        private void ToDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (FromDateTimePicker.Value.ToString("MMMM") == ToDateTimePicker.Value.ToString("MMMM"))
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                //Count For approval per section pic
                SqlCommand SelectStandardMH = new SqlCommand("SP_SelectMonthlyStandardMH", con);
                SelectStandardMH.CommandType = CommandType.StoredProcedure;
                SelectStandardMH.Parameters.AddWithValue("@Month", ToDateTimePicker.Value.ToString("MMMM"));
                if (DateTime.Now.ToString("MMMM") == "January")
                {
                    SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
                }
                else if (DateTime.Now.ToString("MMMM") == "February")
                {
                    SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
                }
                else if (DateTime.Now.ToString("MMMM") == "March")
                {
                    SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
                }
                else
                {
                    SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.ToString("yyyy"));
                }

                SqlDataAdapter sda2 = new SqlDataAdapter(SelectStandardMH);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectStandardMH.ExecuteReader();
                    while (reader2.Read())
                    {
                        StandardMHTextBox.Text = reader2["StandardMH"].ToString();
                    }
                }
                else
                {
                    SqlDataReader reader2 = SelectStandardMH.ExecuteReader();
                    while (reader2.Read())
                    {
                        StandardMHTextBox.Text = "No Standard MH";
                    }
                }

                con.Close();
            }
            else
            {

            }
        }

        private void FromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (FromDateTimePicker.Value.ToString("MMMM") == ToDateTimePicker.Value.ToString("MMMM"))
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectStandardMH = new SqlCommand("SP_SelectMonthlyStandardMH", con);
                SelectStandardMH.CommandType = CommandType.StoredProcedure;
                SelectStandardMH.Parameters.AddWithValue("@Month", ToDateTimePicker.Value.ToString("MMMM"));

                if (DateTime.Now.ToString("MMMM") == "January")
                {
                    SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
                }
                else if (DateTime.Now.ToString("MMMM") == "February")
                {
                    SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
                }
                else if (DateTime.Now.ToString("MMMM") == "March")
                {
                    SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.AddYears(-1).ToString("yyyy"));
                }
                else
                {
                    SelectStandardMH.Parameters.AddWithValue("@FiscalYear", DateTime.Now.Year.ToString());
                }

                SqlDataAdapter sda2 = new SqlDataAdapter(SelectStandardMH);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectStandardMH.ExecuteReader();
                    while (reader2.Read())
                    {
                        StandardMHTextBox.Text = reader2["StandardMH"].ToString();
                    }
                }

                con.Close();
            }
            else
            {

            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchMHLossData();
        }

        private void CheckDuplicatedLineStopBtn_Click(object sender, EventArgs e)
        {
            CheckLineStopDataForm checkLineStopDataForm = new CheckLineStopDataForm();
            checkLineStopDataForm.ShowDialog();
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RejectedMHLossBtn_Click(object sender, EventArgs e)
        {
            ViewRejectedMHLoss viewRjectedMHLoss = new ViewRejectedMHLoss();
            viewRjectedMHLoss.ShowDialog();
        }

        private void DataSheetButton_Click(object sender, EventArgs e)
        {

        }



        //==================================================================================================================>>>>>>>>>>>>
    }
}
