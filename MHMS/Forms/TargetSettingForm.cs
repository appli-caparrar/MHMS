using ExcelDataReader;
using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class TargetSettingForm : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        //int ScrollVal;
        //DataSet ds;
        //SqlDataAdapter sda;
        //DataTable Dtable;
        //SqlCommandBuilder SQLBuilder;

        public TargetSettingForm()
        {
            InitializeComponent();

        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void TargetSettingForm_Load(object sender, EventArgs e)
        {
            FiscalYearDropdown.Text = DateTime.Now.ToString("yyyy");

            LoadSection();

            AddYears(); // Load the list of fiscal year in combobox

            //LoadSection(); //Load all section

            LoadTargetSetting(); // load all data from target setting table

            // Prevent the data grid column to sort
            foreach (DataGridViewColumn column in HistoryDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


        }

        //=================================================================================================================>>>>>>>>>>>>>>>


        //Load Section in combobox
        public void LoadSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand LoadSection = new SqlCommand("SP_LoadSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdownList.DataSource = ds.Tables[0];
            SectionDropdownList.DisplayMember = ds.Tables[0].Columns[0].ToString();
            //SectionDropdown.ValueMember = "";
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void AddYears()
        {

            var currentYear = DateTime.Today.Year;
            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                FiscalYearDropdown.Items.Add((currentYear - i).ToString());
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void LoadTargetSetting()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select target setting
            SqlCommand SelectTargetSetting = new SqlCommand("SP_SelectTargetSetting", con);
            SelectTargetSetting.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(SelectTargetSetting);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TargetSettingDataGrid.DataSource = dt;
            con.Close();
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void TargetSettingDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.Value != null)
            {
                e.CellStyle.BackColor = Color.FromArgb(71, 208, 197);
                e.CellStyle.ForeColor = Color.White;
            }
            //Set specific width on datagrid column
            TargetSettingDataGrid.Columns["History"].Width = 150;
            TargetSettingDataGrid.Columns["Last Update"].Width = 220;

            foreach (DataGridViewColumn column in TargetSettingDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        // Get all data in Efficiency Files table
        private void GetEfficiencyFilesHistory()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (FiscalYearDropdown.Text == "")
            {
                
                // -> SQL query to select/get Efficiency Files
                SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFiles", con);
                SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectProdEfficiencyFiles");
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();

            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {
                SqlCommand SelectEfficiencyFilesBaseOnDate = new SqlCommand("SP_SelectTargetFilesBaseOnDate", con);
                SelectEfficiencyFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectEfficiencyFilesBaseOnDate.Parameters.AddWithValue("@Procedure", "SelectProdEfficiencyFilesBaseOnDate");
                SelectEfficiencyFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        // Get all data in MH loss rate files table
        private void GetMHLossRateFilesHistory()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select/get MH loss rate files
            if (FiscalYearDropdown.Text == "")
            {
                SqlCommand SelectMHLossRateFiles = new SqlCommand("SP_SelectTargetFiles", con);
                SelectMHLossRateFiles.CommandType = CommandType.StoredProcedure;
                SelectMHLossRateFiles.Parameters.AddWithValue("@Procedure", "SelectMHLossRateFiles");
                SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossRateFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {
                SqlCommand SelectMHLossRateFilesBaseOnDate = new SqlCommand("SP_SelectTargetFilesBaseOnDate", con);
                SelectMHLossRateFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectMHLossRateFilesBaseOnDate.Parameters.AddWithValue("@Procedure", "SelectMHLossRateFilesBaseOnDate");
                SelectMHLossRateFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossRateFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        // Get all data in Parts loss rate files table
        private void GetPartsLossRateFilesHistory()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select/get Parts loss rate files
            if (FiscalYearDropdown.Text == "")
            {
                SqlCommand SelectPartsLossRateFiles = new SqlCommand("SP_SelectTargetFiles", con);
                SelectPartsLossRateFiles.CommandType = CommandType.StoredProcedure;
                SelectPartsLossRateFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossRateFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {

                SqlCommand SelectPartsLossRateFilesBaseOnDate = new SqlCommand("SP_SelectTargetFilesBaseOnDate", con);
                SelectPartsLossRateFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossRateFilesBaseOnDate.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFilesBaseOnDate");
                SelectPartsLossRateFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossRateFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        // Get all data in COPQ Manpower rate files table
        private void GetCOPQManpowerRateFilesHistory()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select/get COPQ Manpower rate files
            if (FiscalYearDropdown.Text == "")
            {
                SqlCommand SelectCOPQManpowerRateFiles = new SqlCommand("SP_SelectTargetFiles", con);
                SelectCOPQManpowerRateFiles.CommandType = CommandType.StoredProcedure;
                SelectCOPQManpowerRateFiles.Parameters.AddWithValue("@Procedure", "SelectCOPQManpowerRateFiles");
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQManpowerRateFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {
                SqlCommand SelectCOPQManpowerRateFilesBaseOnDate = new SqlCommand("SP_SelectTargetFilesBaseOnDate", con);
                SelectCOPQManpowerRateFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectCOPQManpowerRateFilesBaseOnDate.Parameters.AddWithValue("@Procedure", "SelectCOPQManpowerRateFilesBaseOnDate");
                SelectCOPQManpowerRateFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQManpowerRateFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        // Get all data in disposal budget files table
        private void GetDisposalBudgetFilesHistory()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select/get COPQ Manpower rate files
            if (FiscalYearDropdown.Text == "")
            {
                SqlCommand SelectDisposalBudgetFiles = new SqlCommand("SP_SelectTargetFiles", con);
                SelectDisposalBudgetFiles.CommandType = CommandType.StoredProcedure;
                SelectDisposalBudgetFiles.Parameters.AddWithValue("@Procedure", "SelectDisposalBudgetFiles");
                SqlDataAdapter sda = new SqlDataAdapter(SelectDisposalBudgetFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {
                SqlCommand SelectDisposalBudgetFilesBaseOnDate = new SqlCommand("SP_SelectTargetFilesBaseOnDate", con);
                SelectDisposalBudgetFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectDisposalBudgetFilesBaseOnDate.Parameters.AddWithValue("@Procedure", "SelectDisposalBudgetFilesBaseOnDate");
                SelectDisposalBudgetFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectDisposalBudgetFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        //private void ShowNextAndPreviousButton()
        //{
        //    PreviousButton.Visible = true;
        //    NextButton.Visible = true;
        //}

        private void TargetSettingDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //try
            //{
                if (TargetSettingDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "HISTORY")
                {
                    if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "Production Efficiency")
                    {
                        HistoryDataGrid.Visible = true;
                        HistoryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        GetEfficiencyFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";

                        SelectTargetFilesByEntries();
                    }
                    if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "Factory Efficiency")
                    {
                        HistoryDataGrid.Visible = true;
                        HistoryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        GetEfficiencyFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";

                        SelectTargetFilesByEntries();
                    }
                    else if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "MH Loss Rate")
                    {
                        HistoryDataGrid.Visible = true;
                        HistoryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        GetMHLossRateFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";

                        SelectTargetFilesByEntries();
                    }
                    else if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "Parts Loss Rate")
                    {
                        HistoryDataGrid.Visible = true;
                        HistoryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        GetPartsLossRateFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";

                        SelectTargetFilesByEntries();
                    }
                    else if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "COPQ Manpower Rate")
                    {
                        HistoryDataGrid.Visible = true;
                        HistoryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        GetCOPQManpowerRateFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";

                        SelectTargetFilesByEntries();
                    }
                    else if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "Disposal Budget")
                    {
                        HistoryDataGrid.Visible = true;
                        HistoryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        GetDisposalBudgetFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";

                        SelectTargetFilesByEntries();
                    }

                    //ShowNextAndPreviousButton();
                }
                else 
                {}
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        string fileName = string.Empty;
        string fileNameWithExt = string.Empty;
        string fileExt = string.Empty;

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {

        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;

            DataTable dtexcel = new DataTable();

            if (fileExt.CompareTo(".xls") == 0)//compare the extension of the file
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";//for below excel 2007
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";//for above excel 2007
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [" + CategoryDropdown.Text + "$]", con);//here we read data from sheet1
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception)
                {
                    MessageBox.Show("Make sure the sheet name should be same as category name!", "Reminders");
                }
            }

            return dtexcel;

        }// end ReadExcel

        //=================================================================================================================>>>>>>>>>>>>>>>

        //private void SaveFileToFolder()
        //{
        //    string SaveDirectory = @"\\apbiphwb02\Release\COPQ Files\Target History\";

        //    if (!Directory.Exists(SaveDirectory))
        //    {
        //        Directory.CreateDirectory(SaveDirectory);
        //    }

        //    if (File.Exists(SaveDirectory + fileNameWithExt))
        //    {
        //        NewFileName = fileName + " rev" + (count++) + fileExt; // Create new filename if already exist
        //        string newFileSavePath = Path.Combine(SaveDirectory, NewFileName); // combine the path of new folder to filename
        //        File.Copy(FilePath.Text, newFileSavePath, true);
        //        MessageBox.Show("File already exist in directory folder, please rename your file!");
        //    }
        //    else
        //    {
        //        string fileSavePath = Path.Combine(SaveDirectory, fileNameWithExt); // combine the path of new folder to filename
        //        File.Copy(FilePath.Text, fileSavePath, true);
        //    }

        //}

        //=================================================================================================================>>>>>>>>>>>>>>>

        // ---> Generate Code
        private void GenerateCode()
        {
            int maxNum = 10000;
            int minNum = 1000;
            Random random = new Random();
            int randomNum = random.Next(minNum, maxNum);
        }

        //=================================================================================================================>>>>>>>>>>>>>>>
        //int count = 2;
        string NewFileName;
        string SaveDirectory;

        // ---> Insert the revised file name
        private void InsertRevisedFiles()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to insert files in Efficiency files table
            if (CategoryDropdown.Text == "Production Efficiency")
            {
                SqlCommand InsertEfficiencyFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                InsertEfficiencyFiles.Parameters.AddWithValue("@Procedure", "InsertEfficiencyFiles");
                InsertEfficiencyFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertEfficiencyFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertEfficiencyFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Production Efficiency File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (CategoryDropdown.Text == "MH Loss Rate")
            {
                SqlCommand InsertMHLossRateFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertMHLossRateFiles.CommandType = CommandType.StoredProcedure;
                InsertMHLossRateFiles.Parameters.AddWithValue("@Procedure", "InsertMHLossRateFiles");
                InsertMHLossRateFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertMHLossRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertMHLossRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertMHLossRateFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertMHLossRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("MH Loss File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                InsertManhourLossRateData(); // insert standard MH loss rate data into DB
            }
            else if (CategoryDropdown.Text == "Parts Loss Rate")
            {
                SqlCommand InsertPartsLossRateFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertPartsLossRateFiles.CommandType = CommandType.StoredProcedure;
                InsertPartsLossRateFiles.Parameters.AddWithValue("@Procedure", "InsertPartsLossRateFiles");
                InsertPartsLossRateFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertPartsLossRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertPartsLossRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertPartsLossRateFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertPartsLossRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Parts Loss File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CategoryDropdown.Text == "COPQ Manpower Rate")
            {
                SqlCommand InsertCOPQManpowerRateFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertCOPQManpowerRateFiles.CommandType = CommandType.StoredProcedure;
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@Procedure", "InsertCOPQManpowerRateFiles");
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertCOPQManpowerRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("COPQ Manpower Rate File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                InsertCOPQManpowerRateData(); // insert COPQ manpower rate data to DB
            }
            else if (CategoryDropdown.Text == "Disposal Budget")
            {
                if (CategoryDropdown.Text == "Disposal Budget")
                {
                    if (SectionDropdownList.Text == "")
                    {
                        MessageBox.Show("Please select the section!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SectionDropdownList.Focus();
                    }
                    else
                    {
                        SqlCommand InsertDisposalBudgetFiles = new SqlCommand("SP_InsertTargetFiles", con);
                        InsertDisposalBudgetFiles.CommandType = CommandType.StoredProcedure;
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@Procedure", "InsertDisposalBudgetFiles");
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                        InsertDisposalBudgetFiles.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Disposal Budget File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        InsertDisposalBudget(); //Insert disposal budget data to DB

                    }
                }
            }
            else if (CategoryDropdown.Text == "Standard Man-Hour")
            {

                SqlCommand InsertDisposalBudgetFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertDisposalBudgetFiles.CommandType = CommandType.StoredProcedure;
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@Procedure", "InsertStandardMHFiles");
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertDisposalBudgetFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Standard Man-Hour File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                InsertStandardManhourData(); //Insert disposal budget data to DB

            }

        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        // ---> Insert the new files
        private void InsertFiles()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to insert files in Efficiency files table
            if (CategoryDropdown.Text == "Production Efficiency")
            {
                SqlCommand InsertEfficiencyFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                InsertEfficiencyFiles.Parameters.AddWithValue("@Procedure", "InsertEfficiencyFiles");
                InsertEfficiencyFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertEfficiencyFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertEfficiencyFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Production Efficiency File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (CategoryDropdown.Text == "MH Loss Rate")
            {
                
                SqlCommand InsertMHLossRateFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertMHLossRateFiles.CommandType = CommandType.StoredProcedure;
                InsertMHLossRateFiles.Parameters.AddWithValue("@Procedure", "InsertMHLossRateFiles");
                InsertMHLossRateFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertMHLossRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertMHLossRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertMHLossRateFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertMHLossRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("MH Loss File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                InsertManhourLossRateData();
                
            }
            else if (CategoryDropdown.Text == "Parts Loss Rate")
            {
                SqlCommand InsertPartsLossRateFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertPartsLossRateFiles.CommandType = CommandType.StoredProcedure;
                InsertPartsLossRateFiles.Parameters.AddWithValue("@Procedure", "InsertPartsLossRateFiles");
                InsertPartsLossRateFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertPartsLossRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertPartsLossRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertPartsLossRateFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertPartsLossRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Parts Loss File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CategoryDropdown.Text == "COPQ Manpower Rate")
            {
                SqlCommand InsertCOPQManpowerRateFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertCOPQManpowerRateFiles.CommandType = CommandType.StoredProcedure;
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@Procedure", "InsertCOPQManpowerRateFiles");
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertCOPQManpowerRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("COPQ Manpower File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                InsertCOPQManpowerRateData();
            }
            else if (CategoryDropdown.Text == "Disposal Budget")
            {
                if (CategoryDropdown.Text == "Disposal Budget")
                {
                    if (SectionDropdownList.Text == "")
                    {
                        MessageBox.Show("Please select the section!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SectionDropdownList.Focus();
                    }
                    else
                    {
                        SqlCommand InsertDisposalBudgetFiles = new SqlCommand("SP_InsertTargetFiles", con);
                        InsertDisposalBudgetFiles.CommandType = CommandType.StoredProcedure;
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@Procedure", "InsertDisposalBudgetFiles");
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        InsertDisposalBudgetFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                        InsertDisposalBudgetFiles.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Disposal Budget File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        InsertDisposalBudget(); //Insert disposal budget data to DB

                    }
                }
            }
            else if (CategoryDropdown.Text == "Standard Man-Hour")
            {
            
                SqlCommand InsertDisposalBudgetFiles = new SqlCommand("SP_InsertTargetFiles", con);
                InsertDisposalBudgetFiles.CommandType = CommandType.StoredProcedure;
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@Procedure", "InsertStandardMHFiles");
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertDisposalBudgetFiles.Parameters.AddWithValue("@FilePath", SaveDirectory + fileNameWithExt);
                InsertDisposalBudgetFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Standard Man-Hour File Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                InsertStandardManhourData(); //Insert disposal budget data to DB

            }
        }

        //================================================================================================================>>>>>>>>>>>>>>>

        private void InsertDisposalBudget()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectDisposalBudgetData = new SqlCommand("SP_SelectDisposalBudget", con);
            SelectDisposalBudgetData.CommandType = CommandType.StoredProcedure;
            SelectDisposalBudgetData.Parameters.AddWithValue("@Section", SectionDropdownList.Text);
            SelectDisposalBudgetData.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
            SqlDataReader reader = SelectDisposalBudgetData.ExecuteReader();
            reader.Read();

            if (reader.HasRows)
            {
                
                foreach (DataGridViewRow row in HistoryDataGrid.Rows)
                {
                    // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                    if (!row.IsNewRow)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        reader.Close();
                        SqlCommand UpdateAnnualDisposalBudgetBySection = new SqlCommand("SP_UpdateAnnualDisposalBudgetBySection", con);
                        UpdateAnnualDisposalBudgetBySection.CommandType = CommandType.StoredProcedure;
                        UpdateAnnualDisposalBudgetBySection.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                        UpdateAnnualDisposalBudgetBySection.Parameters.AddWithValue("@Budget", row.Cells["Budget"].Value);
                        UpdateAnnualDisposalBudgetBySection.Parameters.AddWithValue("@Section", SectionDropdownList.Text);
                        UpdateAnnualDisposalBudgetBySection.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        UpdateAnnualDisposalBudgetBySection.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                        UpdateAnnualDisposalBudgetBySection.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
                        UpdateAnnualDisposalBudgetBySection.ExecuteNonQuery();
                        con.Close();
                    }
                }

                reader.Close();

                MessageBox.Show("Disposal Budget Data Updated successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                foreach (DataGridViewRow row in HistoryDataGrid.Rows)
                {
                    // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                    if (!row.IsNewRow)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        reader.Close();
                        SqlCommand InsertDisposalBudget = new SqlCommand("SP_InsertDisposalBudget", con);
                        InsertDisposalBudget.CommandType = CommandType.StoredProcedure;
                        InsertDisposalBudget.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                        InsertDisposalBudget.Parameters.AddWithValue("@Budget", row.Cells["Budget"].Value);
                        InsertDisposalBudget.Parameters.AddWithValue("@Section", SectionDropdownList.Text);
                        InsertDisposalBudget.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                        InsertDisposalBudget.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                        InsertDisposalBudget.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
                        InsertDisposalBudget.ExecuteNonQuery();
                        con.Close();
                    }
                }

                MessageBox.Show("Disposal Budget Data Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //===============================================================================================================>>>>>>>>>>>>>>>

        private void InsertManhourLossRateData()
        {
            //int intRow = 0;

            foreach (DataGridViewRow row in HistoryDataGrid.Rows)
            {
                // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                if (!row.IsNewRow)
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand InsertMHLossRateData = new SqlCommand("SP_InsertMHLossRateData", con);
                    InsertMHLossRateData.CommandType = CommandType.StoredProcedure;
                    InsertMHLossRateData.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                    InsertMHLossRateData.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                    InsertMHLossRateData.Parameters.AddWithValue("@Target", row.Cells["Target Rate"].Value);
                    InsertMHLossRateData.Parameters.AddWithValue("@Legend", row.Cells["Legend"].Value);
                    InsertMHLossRateData.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
                    InsertMHLossRateData.Parameters.AddWithValue("@UploadBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    InsertMHLossRateData.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString());
                    InsertMHLossRateData.ExecuteNonQuery();
                    con.Close();
                }
            }

            MessageBox.Show("MH Loss Rate Data Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //CategoryDropdown.Text = "";
            //FilePath.Clear();
            //HistoryDataGrid.DataSource = null;
        }

        //=================================================================================================================>>>>>>>>>>>>>>>
        private void DeleteCOPQManpowerRatePreviousUpload()
        {
            // -> SQL query to delete factor loss
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand DeletePartsLoss = new SqlCommand("SP_DeleteCOPQManpowerRatePreviousUpload", con);
            DeletePartsLoss.CommandType = CommandType.StoredProcedure;
            DeletePartsLoss.ExecuteNonQuery();
            con.Close();
        }

        private void InsertCOPQManpowerRateData()
        {
            DeleteCOPQManpowerRatePreviousUpload(); // delete all previous upload

            foreach (DataGridViewRow row in HistoryDataGrid.Rows)
            {
                
                if (!row.IsNewRow)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand InsertCOPQManpowerRateData = new SqlCommand("SP_InsertCOPQManpowerRateData", con);
                    InsertCOPQManpowerRateData.CommandType = CommandType.StoredProcedure;
                    InsertCOPQManpowerRateData.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                    InsertCOPQManpowerRateData.Parameters.AddWithValue("@COPQManpowerRate", row.Cells["COPQ Manpower Rate"].Value);
                    InsertCOPQManpowerRateData.ExecuteNonQuery();
                    con.Close();

                }
            }

            MessageBox.Show("COPQ Manpower Rate Data Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //CategoryDropdown.Text = "";
            //FilePath.Clear();
            //HistoryDataGrid.DataSource = null;
        }

        private void InsertStandardManhourData()
        {
            //int intRow = 0;

            foreach (DataGridViewRow row in HistoryDataGrid.Rows)
            {
                // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                if (!row.IsNewRow)
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand InsertDisposalBudget = new SqlCommand("SP_InsertStandardManhourData", con);
                    InsertDisposalBudget.CommandType = CommandType.StoredProcedure;
                    InsertDisposalBudget.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                    InsertDisposalBudget.Parameters.AddWithValue("@Manhour", row.Cells["Man-Hour"].Value);
                    //InsertDisposalBudget.Parameters.AddWithValue("@Section", SectionDropdownList.Text);
                    InsertDisposalBudget.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    InsertDisposalBudget.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                    InsertDisposalBudget.Parameters.AddWithValue("@FiscalYear", FiscalYearDropdown.Text);
                    InsertDisposalBudget.ExecuteNonQuery();
                    con.Close();

                }
            }

            MessageBox.Show("Standard MH Data Uploaded successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //CategoryDropdown.Text = "";
            //FilePath.Clear();
            //HistoryDataGrid.DataSource = null;
        }

        //============================================================================================================>>>>>>>>>>>>>>>


        // ---> Update the date of last upload
        public void UpdateDateUpload()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // ---> Update query
            SqlCommand UpdateDateOfLastUpload = new SqlCommand("SP_UpdateDateOfLastUpload", con);
            UpdateDateOfLastUpload.CommandType = CommandType.StoredProcedure;
            UpdateDateOfLastUpload.Parameters.AddWithValue("@Category", CategoryDropdown.Text);
            UpdateDateOfLastUpload.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
            UpdateDateOfLastUpload.ExecuteNonQuery();
            con.Close();
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void UploadButton_Click(object sender, EventArgs e)
        {
            // Change the default datagrid columns size to auto fill
            //HistoryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //File destination
            SaveDirectory = @"\\apbiphbpswb01\RELEASE\COPQ Files\Target History\";

            if (FilePath.Text == "")
            {
                MessageBox.Show("File not found, Please select file to upload!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (CategoryDropdown.Text == "")
            {
                MessageBox.Show("Please select category!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //try
                //{
                //int maxNum = 100000000;
                //int minNum = 1000;
                //Random random = new Random();
                //int randomNum = random.Next(minNum, maxNum);

                TitleCategory.Text = CategoryDropdown.Text; // Category title will change depends on selected item in category lists

                    if (!Directory.Exists(SaveDirectory))
                    {
                        Directory.CreateDirectory(SaveDirectory);
                    }

                    if (File.Exists(SaveDirectory + fileNameWithExt)) // if the file to upload is already exists this function will rename the file and insert the new file to the database
                    {
                        //NewFileName = fileName + "_" + DateTime.Now.ToString("mm/dd/yyyy") + fileExt; // Create new filename if already exist
                       
                        NewFileName = fileName + "_Copy_" + DateTime.Now.ToString("MMddyyyyhhmmss") + fileExt; // Create new filename if already exist
                        string newFileSavePath = Path.Combine(SaveDirectory, NewFileName); // combine the path of new folder to filename
                        File.Copy(FilePath.Text, newFileSavePath, true);

                        InsertRevisedFiles(); // ---> insert the revised files
                        UpdateDateUpload(); // ---> Update the date of last upload
                        LoadTargetSetting(); // load all data from target setting table

                        //MessageBox.Show("File already exist in directory folder, please rename your file!");
                    }
                    else
                    {
                        string FileDestination = Path.Combine(SaveDirectory, fileNameWithExt); // combine the path of new folder and filename
                        File.Copy(FilePath.Text, FileDestination, true);

                        InsertFiles(); // Insert the not existing file in database
                        UpdateDateUpload(); // ---> Update the date of last upload
                        LoadTargetSetting(); // load all data from target setting table
                    }

                    FilePath.Clear();
                    CategoryDropdown.Text = "";
                    HistoryDataGrid.DataSource = null;

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        // ---> Filter the data based on fiscal year when the FY dropdown was change
        private void FiscalYearDropdown_TextChanged(object sender, EventArgs e)
        {
            if (TitleCategory.Text == "Production Efficiency History")
            {
                GetEfficiencyFilesHistory(); // Call out the function to execute
            }
            else if (TitleCategory.Text == "MH Loss Rate History")
            {
                GetMHLossRateFilesHistory(); // Call out the function to execute
            }
            else if (TitleCategory.Text == "Parts Loss Rate History")
            {
                GetPartsLossRateFilesHistory(); // Call out the function to execute
            }
            else if (TitleCategory.Text == "COPQ Manpower Rate History")
            {
                GetCOPQManpowerRateFilesHistory(); // Call out the function to execute
            }
        }


        //===========================================================================================================>>>>>>>>>>>>>>>

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            //ScrollVal = ScrollVal - 10;
            //if (ScrollVal <= 0)
            //{
            //    ScrollVal = 0;
            //}

            //if (ScrollVal <= TotalCount)
            //{
            //    NextButton.Enabled = true;
            //}

            //if (TitleCategory.Text == "Efficiency History")
            //{
            //    ds.Clear();
            //    sda.Fill(ds, ScrollVal, 10, "EfficiencyFiles");
            //}
            //else if (TitleCategory.Text == "MH Loss Rate History")
            //{
            //    ds.Clear();
            //    sda.Fill(ds, ScrollVal, 10, "MHLossRateFiles");
            //}

        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        int TotalCount;
        //NOT IN USE
        private void NextButton_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select/get Efficiency Files
            SqlCommand SelectTotalFilesCount = new SqlCommand("SP_SelectTotalFilesCount", con);
            SelectTotalFilesCount.CommandType = CommandType.StoredProcedure;
            SelectTotalFilesCount.Parameters.AddWithValue("@Procedure", "CountEfficiencyFiles");

            SqlDataReader reader = SelectTotalFilesCount.ExecuteReader();
            if (reader.Read())
            {
                TotalCount = int.Parse(reader["TotalCount"].ToString());
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            //Set the datagrid column size to none
            HistoryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            string filePath = string.Empty;

            OpenFileDialog file = new OpenFileDialog();//open dialog to choose file

            if (file.ShowDialog() == DialogResult.OK)//if there is a file choosen by the user
            {
                filePath = file.FileName;//get the path of the file
                fileName = Path.GetFileNameWithoutExtension(filePath); // get the file name without extension
                fileNameWithExt = Path.GetFileName(filePath);
                fileExt = Path.GetExtension(filePath);//get the file extension
                FilePath.Text = filePath;

                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0 || fileExt.CompareTo(".xlsm") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        HistoryDataGrid.Visible = true;
                        HistoryDataGrid.DataSource = dtExcel;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);//custom messageBox to show error
                }
            }

        }


        //=================================================================================================================>>>>>>>>>>>>>>>

        private void SelectTargetFilesByEntries()
        {
            if (TitleCategory.Text == "Production Efficiency History")
            {
                // -> Select top 10 data of target files
                SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectProductionEfficiencyFiles");
                //SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top10"); 
                SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", DropdownValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();

                //if (DropdownValue.Text == "10")
                //{
                //    // -> Select top 10 data of target files
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectEfficiencyFiles");
                //    //SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top10"); 
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", DropdownValue.Text);
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();

                //    //ds = new DataSet();
                //    //sda.Fill(ds, ScrollVal, 10, "EfficiencyFiles");
                //    //con.Close();
                //    //Dtable = ds.Tables["EfficiencyFiles"];
                //    //HistoryDataGrid.DataSource = Dtable;
                //}
                //else if (DropdownValue.Text == "50")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectEfficiencyFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top50");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
                //else if (DropdownValue.Text == "100")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectEfficiencyFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top100");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
                //else if (DropdownValue.Text == "All")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectEfficiencyFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "All");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}

            }
            else if (TitleCategory.Text == "Factory Efficiency History")
            {
                // -> Select top 10 data of target files
                SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectFactoryEfficiencyFiles");
                //SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top10"); 
                SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", DropdownValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();

            }
            else if (TitleCategory.Text == "MH Loss Rate History")
            {
                // -> Select top 10 data of target files
                SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectMHLossRateFiles");
                SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", DropdownValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();

                //if (DropdownValue.Text == "10")
                //{
                //    // -> Select top 10 data of target files
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectMHLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", DropdownValue.Text);
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();

                //    //ds = new DataSet();
                //    //sda.Fill(ds, ScrollVal, 10, "EfficiencyFiles");
                //    //con.Close();
                //    //Dtable = ds.Tables["EfficiencyFiles"];
                //    //HistoryDataGrid.DataSource = Dtable;
                //}
                //else if (DropdownValue.Text == "50")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectMHLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top50");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
                //else if (DropdownValue.Text == "100")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectMHLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top100");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
                //else if (DropdownValue.Text == "All")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectMHLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "All");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
            }
            else if (TitleCategory.Text == "Parts Loss Rate History")
            {
                // -> Select top 10 data of target files
                SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", DropdownValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();

                //if (DropdownValue.Text == "10")
                //{
                //    // -> Select top 10 data of target files
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top10");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();

                //    //ds = new DataSet();
                //    //sda.Fill(ds, ScrollVal, 10, "EfficiencyFiles");
                //    //con.Close();
                //    //Dtable = ds.Tables["EfficiencyFiles"];
                //    //HistoryDataGrid.DataSource = Dtable;
                //}
                //else if (DropdownValue.Text == "50")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top50");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
                //else if (DropdownValue.Text == "100")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top100");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
                //else if (DropdownValue.Text == "All")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "All");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
            }
            else if (TitleCategory.Text == "COPQ Manpower Rate History")
            {
                // -> Select top 10 data of target files
                SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectCOPQManpowerRateFiles");
                SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", DropdownValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();

                //if (DropdownValue.Text == "10")
                //{
                //    // -> Select top 10 data of target files
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectCOPQManpowerRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", DropdownValue.Text);
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();

                //    //ds = new DataSet();
                //    //sda.Fill(ds, ScrollVal, 10, "EfficiencyFiles");
                //    //con.Close();
                //    //Dtable = ds.Tables["EfficiencyFiles"];
                //    //HistoryDataGrid.DataSource = Dtable;
                //}
                //else if (DropdownValue.Text == "50")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top50");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
                //else if (DropdownValue.Text == "100")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "Top100");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
                //else if (DropdownValue.Text == "All")
                //{
                //    // -> Select top data of target files base on entries
                //    SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectTargetFilesByEntries", con);
                //    SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Procedure", "SelectPartsLossRateFiles");
                //    SelectEfficiencyFiles.Parameters.AddWithValue("@Entries", "All");
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    HistoryDataGrid.DataSource = dt;
                //    con.Close();
                //}
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void DropdownValue_TextChanged(object sender, EventArgs e)
        {
            SelectTargetFilesByEntries();
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void HistoryDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in HistoryDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //=================================================================================================================>>>>>>>>>>>>>>>

        private void TargetSettingDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("Please dont click table header to avoid showing of this message. Thank you!", "");
        }

        //=================================================================================================================>>>>>>>>>>>>>>>
      
        private void CategoryDropdown_TextChanged(object sender, EventArgs e)
        {
            if (CategoryDropdown.Text == "Disposal Budget")
            {
                SectionDropdownPanel.Visible = true;
            }
            else
            {
                SectionDropdownPanel.Visible = false;
            }
        }

        private void DisposalBudgetTemplateButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Other Template\Disposal Budget Template - Updated.xlsx");
        }

        private void ManhourTemplateButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Other Template\Standard MH - Updated.xlsx");
        }

        private void DownloadTemplateBtn_Click(object sender, EventArgs e)
        {
            DownloadTemplateForm downloadTemplateForm = new DownloadTemplateForm();
            downloadTemplateForm.ShowDialog();
        }

        public static string Category;
        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category = CategoryDropdown.Text;

            UploadProdEfficiencyTemplate UploadProdEfficiencyTemplate = new UploadProdEfficiencyTemplate();
            UploadProdEfficiencyTemplate.ShowDialog();
        }

        public static bool LoadTargetSettings = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (LoadTargetSettings == true)
            {
                LoadTargetSetting(); // load all data from target setting table

                LoadTargetSettings = false;
            }
        }

        private void SectionDropdownList_DropDown(object sender, EventArgs e)
        {
           
        }

        private void FiscalYearDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //=================================================================================================================>>>>>>>>>>>>>>>

    }
}
