using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using ClosedXML.Excel;
using MHMS.Connection;

namespace MHMS
{
    public partial class UploadPCData : Form
    {
        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public UploadPCData()
        {
            InitializeComponent();
        }

        private void UploadPartsDetail_Load(object sender, EventArgs e)
        {

        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx;*.xls;*.xlsm)|*.xlsx;*.xls;*.xlsm";
            openFileDialog.Title = "Select an Excel File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                FilePath.Text = filePath;

                // Load sheet names into ComboBox
                LoadSheetNames(filePath);

                // Initialize the DataGridView or clear it
                PartsDetailDatagrid.DataSource = null;
                rowCount.Text = "Count: 0";
            }
        }

        private void LoadSheetNames(string filePath)
        {
            try
            {
                // Clear the ComboBox before adding new items
                SheetComboBox.Items.Clear();

                using (var workbook = new XLWorkbook(filePath))
                {
                    // Loop through all worksheets and add their names to the ComboBox
                    foreach (var worksheet in workbook.Worksheets)
                    {
                        SheetComboBox.Items.Add(worksheet.Name);
                    }

                    // If there are sheets, set the first one as the default selected sheet
                    if (SheetComboBox.Items.Count > 0)
                    {
                        SheetComboBox.SelectedIndex = 0; // Select the first sheet by default
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sheet names: {ex.Message}");
            }
        }

        private void SheetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SheetComboBox.SelectedItem != null)
            {
                string selectedSheetName = SheetComboBox.SelectedItem.ToString();
                string filePath = FilePath.Text;

                // Load and display the selected sheet in the DataGridView
                DataTable dt = ReadExcelSheet(filePath, selectedSheetName);
                PartsDetailDatagrid.DataSource = dt;

                rowCount.Text = "Count: " + PartsDetailDatagrid.Rows.Count.ToString();
            }
        }

        // This method will read the selected sheet from the Excel file and return a DataTable
        private DataTable ReadExcelSheet(string filePath, string sheetName)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(sheetName); // Read the selected sheet
                    var rows = worksheet.RowsUsed();

                    // Adding columns to DataTable
                    foreach (var cell in rows.First().Cells())
                    {
                        dt.Columns.Add(cell.Value.ToString());
                    }

                    // Adding data rows
                    foreach (var row in rows.Skip(1)) // Skip the first row (header)
                    {
                        DataRow dataRow = dt.NewRow();
                        int i = 0;
                        foreach (var cell in row.Cells())
                        {
                            dataRow[i] = cell.Value.ToString();
                            i++;
                        }

                        dt.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading Excel sheet: {ex.Message}");
            }

            return dt;
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (FilePath.Text == "")
            {
                MessageBox.Show("Please select file.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Retrieve the DataTable from the DataGridView's DataSource
                DataTable dt = PartsDetailDatagrid.DataSource as DataTable;

                if (dt != null)
                {
                    try
                    {
                        // Check the sheet name selected in the ComboBox
                        string selectedSheetName = SheetComboBox.SelectedItem?.ToString();

                        if (string.IsNullOrEmpty(selectedSheetName))
                        {
                            MessageBox.Show("Please select a valid sheet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Determine which method to call based on the sheet name
                        if (selectedSheetName.Contains("Work Center"))
                        {
                            // If it's the "Work Center" sheet, call InsertWorkCenter
                            BulkInsertWorkCenter(dt);
                        }
                        else
                        {
                            // Otherwise, perform bulk insert for PartsRegistration
                            BulkInsertPartsRegistration(dt);
                        }

                        // Insert history record after the insert operation
                        string uploadBy = LoginForm.FirstName + " " + LoginForm.LastName;
                        InsertPartsRegistrationHistory(FilePath.Text, DateTime.Now, DateTime.Now.ToString("HH:mm:ss"), uploadBy);

                        MessageBox.Show("Data successfully uploaded.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error during upload: {ex.Message}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No data available for upload.", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        // Method to perform bulk insert into PartsRegistration
        private void BulkInsertPartsRegistration(DataTable dt)
        {
            // Make sure the connection is open
            con.Open();

            // Use SqlBulkCopy for efficient bulk insertion
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
            {
                // Map columns between DataTable and the database table
                bulkCopy.DestinationTableName = "PartsRegistration";

                // Map columns from the DataTable to the destination table columns
                bulkCopy.ColumnMappings.Add("VENDOR", "Vendor");
                bulkCopy.ColumnMappings.Add("MATERIAL", "Material");
                bulkCopy.ColumnMappings.Add("PLANT", "Plant");
                bulkCopy.ColumnMappings.Add("DESCRIPTION (EN)", "Description");
                bulkCopy.ColumnMappings.Add("VENDOR NAME", "VendorName");

                // Write data from DataTable to the SQL Server table
                bulkCopy.WriteToServer(dt);
            }

            con.Close();
        }

        private void BulkInsertWorkCenter(DataTable dt)
        {
            // Make sure the connection is open
            con.Open();

            // Use SqlBulkCopy for efficient bulk insertion
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
            {
                // Map columns between DataTable and the database table
                bulkCopy.DestinationTableName = "WorkCenter";

                // Map columns from the DataTable to the destination table columns
                bulkCopy.ColumnMappings.Add("Section", "Section");
                bulkCopy.ColumnMappings.Add("Work Center", "WorkCenter");
                bulkCopy.ColumnMappings.Add("Plant", "Plant");
                bulkCopy.ColumnMappings.Add("Model", "Model");
                bulkCopy.ColumnMappings.Add("Line", "Line");

                // Write data from DataTable to the SQL Server table
                bulkCopy.WriteToServer(dt);
            }

            con.Close();
        }

        // Method to insert data into PartsRegistrationHistory table
        private void InsertPartsRegistrationHistory(string fileName, DateTime uploadDate, string uploadTime, string uploadedBy)
        {
            try
            {
                string query = @"
                INSERT INTO PartsRegistrationHistory (FileName, Date, Time, UploadedByPIC)
                VALUES (@FileName, @UploadDate, @UploadTime, @UploadBy)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the SQL query
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@UploadDate", uploadDate);
                    cmd.Parameters.AddWithValue("@UploadTime", uploadTime);
                    cmd.Parameters.AddWithValue("@UploadBy", uploadedBy); // Pass the actual user name

                    // Open connection and execute the insert query
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting into PartsRegistrationHistory: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

       
    }
}
