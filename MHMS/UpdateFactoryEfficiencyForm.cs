using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using System.Data.SqlClient;
using MHMS.Connection;
using System.Diagnostics;
using System.Data.OleDb;

namespace MHMS
{
    public partial class UpdateFactoryEfficiencyForm : Form
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);
        SqlConnection con2 = new SqlConnection(SQLControl.MHMS_Conn);


        public UpdateFactoryEfficiencyForm()
        {
            InitializeComponent();
        }


        string fileName = string.Empty;
        string fileNameWithExt = string.Empty;
        string fileExt = string.Empty;
        string filePath = string.Empty;

        //Table collection
        DataTableCollection tableCollection;

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //string filePath = string.Empty;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;//get the path of the file
                    fileName = Path.GetFileNameWithoutExtension(filePath); // get the file name without extension
                    fileNameWithExt = Path.GetFileName(filePath);
                    fileExt = Path.GetExtension(filePath);//get the file extension
                    FilePath.Text = filePath;


                    FilePath.Text = openFileDialog.FileName;
                    try
                    {
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });

                                tableCollection = result.Tables;
                                SheetDropdownList.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                    SheetDropdownList.Items.Add(table.TableName);

                                SheetDropdownList.Select();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please close the Excel File!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FilePath.Text = "";
                    }

                }
            }


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
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [" + SheetDropdownList.Text + "$]", con);//here we read data from sheet1
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception)
                {
                    MessageBox.Show("Make sure the sheet name should be same as category name!", "Reminders");
                }
            }

            return dtexcel;

        }// end ReadExcel

        private void SheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtExcel = new DataTable();
                dtExcel = ReadExcel(filePath, fileExt);//read excel file
                UploadFactoryEfficiencyDatagrid.Visible = true;
                UploadFactoryEfficiencyDatagrid.DataSource = dtExcel;

                UploadFactoryEfficiencyDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void UpdateFactoryEfficiencyForm_Load(object sender, EventArgs e)
        {
            AddYears();
        }

        private void AddYears()
        {

            var currentYear = DateTime.Today.Year;
            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                //YearDropdownlist.Items.Add((currentYear - i).ToString());
                YearDropdown.Items.Add((currentYear - i).ToString());
            }
        }

        private void DownloadTemplateButton_Click(object sender, EventArgs e)
        {
            Process.Start("input template link here");
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (CategoryDropdown.Text == "")
            {
                MessageBox.Show("Please select category", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CategoryDropdown.Select();
            }
            else if (MonthDropdown.Text == "")
            {
                MessageBox.Show("Please select month", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MonthDropdown.Select();
            }
            else if (YearDropdown.Text == "")
            {
                MessageBox.Show("Please select year", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                YearDropdown.Select();
            }
            else if (SheetDropdownList.Text == "")
            {
                MessageBox.Show("Please select sheet", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SheetDropdownList.Select();
            }
            else
            {
                InsertFactoryEfficiencyRawData();
            }
        }

        private void InsertFactoryEfficiencyRawData()
        {
            DeletePreviousUploadBaseOnFiscalYear();

            if (CategoryDropdown.Text == "MH Monthly Actual Forecast")
            {
               
                foreach (DataGridViewRow row in UploadFactoryEfficiencyDatagrid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        con.Open();
                        SqlCommand InsertFEMHMonthly = new SqlCommand("SP_Insert_FE_Monthly", con);
                        InsertFEMHMonthly.CommandType = CommandType.StoredProcedure;
                        InsertFEMHMonthly.Parameters.AddWithValue("@Procedure", "MH Monthly");
                        InsertFEMHMonthly.Parameters.AddWithValue("@Department", row.Cells["Department"].Value);
                        InsertFEMHMonthly.Parameters.AddWithValue("@Costcenter", "");
                        InsertFEMHMonthly.Parameters.AddWithValue("@Section", row.Cells["Section Detail"].Value);
                        InsertFEMHMonthly.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                        InsertFEMHMonthly.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                        InsertFEMHMonthly.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                        InsertFEMHMonthly.Parameters.AddWithValue("@Total", row.Cells["Total"].Value);
                        InsertFEMHMonthly.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                        InsertFEMHMonthly.ExecuteNonQuery();
                        con.Close();
                    }
                }

                MessageBox.Show("MH monthly forecast updated successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CategoryDropdown.Text == "ST Monthly Actual Forecast")
            {

                foreach (DataGridViewRow row in UploadFactoryEfficiencyDatagrid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        con.Open();
                        SqlCommand InsertFESTMonthly = new SqlCommand("SP_Insert_FE_Monthly", con);
                        InsertFESTMonthly.CommandType = CommandType.StoredProcedure;
                        InsertFESTMonthly.Parameters.AddWithValue("@Procedure", "ST Monthly");
                        InsertFESTMonthly.Parameters.AddWithValue("@Department", "");
                        InsertFESTMonthly.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
                        InsertFESTMonthly.Parameters.AddWithValue("@Costcenter", row.Cells["Cost Center"].Value);
                        InsertFESTMonthly.Parameters.AddWithValue("@Month", row.Cells["Month"].Value);
                        InsertFESTMonthly.Parameters.AddWithValue("@Value", row.Cells["Value"].Value);
                        InsertFESTMonthly.Parameters.AddWithValue("@Actual_Forecast", row.Cells["Actual / Forecast"].Value);
                        InsertFESTMonthly.Parameters.AddWithValue("@Total", row.Cells["Total"].Value);
                        InsertFESTMonthly.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                        InsertFESTMonthly.ExecuteNonQuery();
                        con.Close();
                    }
                }

                MessageBox.Show("ST monthly forecast updated successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
               
        }
    




        private void DeletePreviousUploadBaseOnFiscalYear()
        {
            if (CategoryDropdown.Text == "MH Monthly Actual Forecast")
            {
                con.Open();
                SqlCommand DeletePreviousUploadBaseOnFiscalYear = new SqlCommand("SP_DeletePreviousUploadBaseOnFiscalYear", con);
                DeletePreviousUploadBaseOnFiscalYear.CommandType = CommandType.StoredProcedure;
                DeletePreviousUploadBaseOnFiscalYear.Parameters.AddWithValue("@Procedure", "MH Monthly");
                DeletePreviousUploadBaseOnFiscalYear.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                DeletePreviousUploadBaseOnFiscalYear.ExecuteNonQuery();
                con.Close();
            }
            else if (CategoryDropdown.Text == "ST Monthly Actual Forecast")
            {
                con.Open();
                SqlCommand DeletePreviousUploadBaseOnFiscalYear = new SqlCommand("SP_DeletePreviousUploadBaseOnFiscalYear", con);
                DeletePreviousUploadBaseOnFiscalYear.CommandType = CommandType.StoredProcedure;
                DeletePreviousUploadBaseOnFiscalYear.Parameters.AddWithValue("@Procedure", "ST Monthly");
                DeletePreviousUploadBaseOnFiscalYear.Parameters.AddWithValue("@FiscalYear", YearDropdown.Text);
                DeletePreviousUploadBaseOnFiscalYear.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
