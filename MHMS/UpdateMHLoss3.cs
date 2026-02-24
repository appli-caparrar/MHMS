using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using MHMS.Class;
using MHMS.Connection;

namespace MHMS
{
    public partial class UpdateMHLoss3 : Form
    {
        // SQL Connection
        SqlConnection conn = new SqlConnection(SQLControl.MHMS_Conn);

        public UpdateMHLoss3()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx",
                Title = "Select an Excel File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                MessageBox.Show("You selected: " + filePath);

                // Call method to read or upload the file here
                var dataTable = ReadExcel(filePath);
                BulkInsertToSqlServer(dataTable, SQLControl.MHMS_Conn, "YourTableName");
            }
        }

        // Read Excel to DataTable
        public DataTable ReadExcel(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                return result.Tables[0];
            }
        }

        public void BulkInsertToSqlServer(DataTable table, string connectionString, string destinationTableName)
        {
            //using (var connection = new SqlConnection(SQLControl.MHMS_Conn))
            //{
            //    connection.Open();

            //    using (var bulkCopy = new SqlBulkCopy(connection))
            //    {
            //        bulkCopy.DestinationTableName = destinationTableName;
            //        bulkCopy.BatchSize = 1000;
            //        bulkCopy.BulkCopyTimeout = 60;

            //        // Optional: map columns if they don't match exactly
            //        // bulkCopy.ColumnMappings.Add("ExcelColumn", "SQLColumn");

            //        bulkCopy.WriteToServer(dataTable); // 'dataTable' is the DataTable you're uploading
            //    }
            //}
        }

        public async Task<int> GetLeadTimeFromOverallMonitoringAsync(SqlConnection con, string LossFactor)
        {
            // Close the connection (if it's not already closed) and ensure it is opened asynchronously
            con.Close();
            await con.OpenAsync();

            // Query to fetch the LeadTime from the LossFactor table based on LossFactor
            string query = "SELECT LeadTime FROM LossFactor WHERE [Loss Factor] = @LossFactor";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LossFactor", LossFactor);

            // Execute the query asynchronously and get the result
            var result = await cmd.ExecuteScalarAsync();

            // Check if the result is DBNull (indicating a null value from the database)
            if (result == DBNull.Value)
            {
                return 0;  // Return 0 or any default value when the result is DBNull
            }

            // Otherwise, return the LeadTime value (cast it safely to int)
            return Convert.ToInt32(result);
        }

        public async Task<DateTime> GetTargetClosedDateAsync(SqlConnection con, DateTime dateEncountered, int leadTime)
        {
            DateTime targetDate = dateEncountered;
            int workingDaysCount = 0;

            // Loop until we reach the desired number of working days
            while (workingDaysCount < leadTime)
            {
                targetDate = targetDate.AddDays(1);  // Move to the next day

                // Check if the targetDate is a working day asynchronously
                if (await IsNonWorkingDayAsync(con, targetDate))  // If it's a working day
                {
                    workingDaysCount++;  // Increment the working day counter
                }
            }

            return targetDate;  // Return the calculated target date
        }


        public async Task<bool> IsNonWorkingDayAsync(SqlConnection con, DateTime date)
        {
            con.Close();
            await con.OpenAsync();  // Ensure connection is opened asynchronously

            // Query to check if the date is a non-working day from 'view_biph_working_days' table
            string query = "SELECT COUNT(*) FROM view_biph_working_days " +
                           "WHERE working_date = @date AND is_no_working_day = 'No'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@date", date);
            int result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            con.Close();

            return result > 0;  // Return true if it's a non-working day, false otherwise


        }

        private void UpdateMHLoss3_Load(object sender, EventArgs e)
        {
            MHLossUploadDatagrid.AutoGenerateColumns = true;
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog
            //{
            //    Filter = "Excel Files|*.xls;*.xlsx;"
            //};

            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    string filePath = ofd.FileName;
            //    var dt = ReadExcel(filePath);
            //    BulkInsertToSqlServer(dt, yourConnectionString, "YourTableName");
            //    MessageBox.Show("Upload successful!");
            //}
        }
    }
}
