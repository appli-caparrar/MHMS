using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using ExcelDataReader;
using MHMS.Alert;
using MHMS.Class;
using MHMS.Connection;
using MHMS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using Z.Dapper.Plus;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MHMS
{
    public partial class UpdateMHLoss2 : Form
    {
        // Connection string
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public UpdateMHLoss2()
        {
            InitializeComponent();
        }

        //Table collection
        DataTableCollection tableCollection;

        //=================================================================================================================>>>>>>>>>>>>>

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx;*.xlsm" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
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
                                ExcelSheetDropdownList.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                    ExcelSheetDropdownList.Items.Add(table.TableName);
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

            //using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx;*.xlsm" })
            //{
            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        FilePath.Text = openFileDialog.FileName;
            //        try
            //        {
            //            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
            //            using (var reader = ExcelReaderFactory.CreateReader(stream))
            //            {
            //                var config = new ExcelDataSetConfiguration
            //                {
            //                    ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
            //                };

            //                var result = reader.AsDataSet(config);
            //                var table = result.Tables[0]; // or use dropdown to pick table

            //                 ⚡ Super fast direct bind
            //                MHLossUploadDatagrid.DataSource = table;

            //                rowCount.Text = $"{table.Rows.Count} rows";
            //                LabelTimeElapsed.Text = "Loaded instantly ⚡";

            //                Optionally populate dropdown if multiple sheets
            //                ExcelSheetDropdownList.Items.Clear();
            //                foreach (DataTable sheet in result.Tables)
            //                    ExcelSheetDropdownList.Items.Add(sheet.TableName);

            //                tableCollection = result.Tables; // still store it if needed later
            //            }
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("Please close the Excel File!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            FilePath.Text = "";
            //        }
            //    }
            //}

            //using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //{
            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        FilePath.Text = openFileDialog.FileName;
            //        try
            //        {
            //            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
            //            {
            //                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            //                {
            //                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
            //                    {
            //                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
            //                    });
            //                    tableCollection = result.Tables;
            //                    ExcelSheetDropdownList.Items.Clear();
            //                    foreach (DataTable table in tableCollection)
            //                        ExcelSheetDropdownList.Items.Add(table.TableName);
            //                }
            //            }
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("Please close the Excel File!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            FilePath.Text = "";
            //        }
            //    }
            //}
        }

        //===================================================================================================================>>>>>>>>>>>>>

        int addOne = 0;
        private async void ExcelSheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcelSheetDropdownList.Enabled = false;
            UploadButton.Enabled = false;

            UpdateInfo.Visible = true;
            ReadyToUpload.Image = Properties.Resources.loading1;
            infoText.Text = "Reading data...";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                DataTable dt = tableCollection[ExcelSheetDropdownList.SelectedItem.ToString()];
                if (dt == null) return;

                List<MHData_Class> list = new List<MHData_Class>();
                int rowCounter = 1;

                using (SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn))
                {
                    await con.OpenAsync();

                    // ✅ 1. Normalize Loss Factor Dictionary
                    Dictionary<string, int> leadTimeMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    using (SqlCommand cmd = new SqlCommand("SELECT [Loss Factor], LeadTime FROM LossFactor", con))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string factor = NormalizeLossFactor(reader["Loss Factor"].ToString());
                            if (reader["LeadTime"] != DBNull.Value)
                            {
                                int lead = Convert.ToInt32(reader["LeadTime"]);
                                leadTimeMap[factor] = lead + 2; // Add 2 buffer days
                            }
                        }
                    }

                    // ✅ 2. Load Working Days
                    HashSet<DateTime> workingDays = new HashSet<DateTime>();
                    using (SqlCommand cmd = new SqlCommand("SELECT working_date FROM view_biph_working_days WHERE is_no_working_day = 'No'", con))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            workingDays.Add(Convert.ToDateTime(reader["working_date"]).Date);
                        }
                    }

                    // ✅ 3. Process Excel Rows
                    foreach (DataRow row in dt.Rows)
                    {
                        MHData_Class obj = new MHData_Class();
                        obj.RowCounter = rowCounter++;

                        // ==========================
                        // ✅ SAFE DATE PARSING
                        // ==========================
                        DateTime date = DateTime.MinValue;      // safe initial value
                        string rawDate = row["Date"]?.ToString()?.Trim();
                        bool parsed = false;

                        // 1) If numeric → treat as OADate
                        if (!string.IsNullOrWhiteSpace(rawDate) && double.TryParse(rawDate, out double oaVal))
                        {
                            try
                            {
                                date = DateTime.FromOADate(oaVal);
                                parsed = true;
                            }
                            catch
                            {
                                parsed = false;
                            }
                        }

                        // 2) If not parsed → try normal datetime
                        if (!parsed)
                        {
                            if (!DateTime.TryParse(rawDate, out date))
                            {
                                MessageBox.Show(
                                    $"Invalid date found in Excel row {rowCounter}. Value: \"{rawDate}\"\n\nThis row was skipped.",
                                    "Invalid Date",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                continue;   // ⛔ Skip this row to avoid errors downstream
                            }
                        }
                        // ==========================


                        // ==========================
                        // LOSS FACTOR NORMALIZATION
                        // ==========================
                        string rawLossFactor = row["Loss factor(EN)"]?.ToString() ?? "";
                        string lossFactor = NormalizeLossFactor(rawLossFactor);


                        // ==========================
                        // BUILD MODEL OBJECT
                        // ==========================
                        obj.DistinctionCode =
                            $"{row["Section"]}-{date:MM/dd/yyyy}-{row["Cost center/Model"]}-{row["Work center"]}-{row["Item code"]}-{row["Stop time(min)"]}-{row["Direct employee"]}-{row["Semi-direct employee"]}-{row["Loss man-hour"]}-{row["Line stop content detail"]}";

                        obj.ReferenceNo = $"{row["Section"]}-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}-{addOne++}";
                        obj.Section = row["Section"]?.ToString();
                        obj.DateEncountered = date.ToString("MM/dd/yyyy");
                        obj.Plant = row["Plant"]?.ToString();
                        obj.WorkCenter = row["Work center"]?.ToString();
                        obj.Day_Night = row["Day/Night"]?.ToString();
                        obj.CostCenter = row["Cost center/Model"]?.ToString();
                        obj.ModelName = row["Cost center/Model name"]?.ToString();
                        obj.ItemCode = row["Item code"]?.ToString();
                        obj.ItemText = row["Item text"]?.ToString();
                        obj.StopTime = row["Stop time(min)"]?.ToString();
                        obj.LineStopDetail = row["Line stop content detail"]?.ToString();
                        obj.LossFactor = rawLossFactor;
                        obj.DirectMP = row["Direct employee"]?.ToString();
                        obj.SemiDirectMP = row["Semi-direct employee"]?.ToString();
                        obj.LossManhour = row["Loss man-hour"]?.ToString();
                        obj.LossMH_ForCOPQAmount = obj.LossManhour;

                        obj.ApplyingApprovalStatus = "For Approval by COPQ PIC";
                        obj.ReceivingApprovalStatus = "---";
                        obj.OverAllStatus = "For Approval";
                        obj.QIConfirmation = "---";
                        obj.ApplyingCOPQPIC = "Pending Approval";
                        obj.UploadDate = DateTime.Now.ToString("MM/dd/yyyy");
                        obj.Mark = "0";


                        // ==========================
                        // SAFE FISCAL YEAR LOGIC
                        // ==========================
                        obj.FiscalYear = (date.Month <= 3)
                            ? date.AddYears(-1).Year.ToString()
                            : date.Year.ToString();


                        // ==========================
                        // SAFE LEAD TIME TARGET DATE
                        // ==========================
                        if (leadTimeMap.TryGetValue(lossFactor, out int leadTime))
                        {
                            obj.LeadTime = leadTime.ToString();

                            DateTime targetDate = date;
                            int count = 0;

                            // Safety: prevent overflow
                            while (count < leadTime)
                            {
                                if (targetDate >= DateTime.MaxValue.AddDays(-1))
                                {
                                    obj.TargetClosedDate = "Invalid Date";
                                    break;
                                }

                                targetDate = targetDate.AddDays(1);

                                if (workingDays.Contains(targetDate.Date))
                                    count++;
                            }

                            if (obj.TargetClosedDate != "Invalid Date")
                                obj.TargetClosedDate = targetDate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            obj.LeadTime = null;
                            obj.TargetClosedDate = null;
                        }

                            list.Add(obj);
                        }
                    }

                    // ✅ 5. Bind to DataGrid
                    MHLossUploadDatagrid.Columns.Clear();
                    MHLossUploadDatagrid.DataSource = list;

                    // ✅ 6. Hide for Delete Action
                    if (ActionDropdownList.Text == "Delete")
                    {
                        string[] columnsToHide = {
                        "ReferenceNo", "LossMH_ForCOPQAmount", "ApplyingApprovalStatus",
                        "ReceivingApprovalStatus", "OverAllStatus", "QIConfirmation",
                        "UploadDate", "ApplyingCOPQPIC"
                    };

                    foreach (string col in columnsToHide)
                    {
                        if (MHLossUploadDatagrid.Columns.Contains(col))
                            MHLossUploadDatagrid.Columns[col].Visible = false;
                    }
                }

                stopwatch.Stop();
                MessageBox.Show($"Processing completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds.", "Ready For Upload!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                stopwatch.Stop();
                rowCount.Text = $"{MHLossUploadDatagrid.RowCount} rows";
                LabelTimeElapsed.Text = $"{stopwatch.Elapsed.TotalSeconds:F2} seconds";
                ExcelSheetDropdownList.Enabled = true;
                UploadButton.Enabled = true;
                UpdateInfo.Visible = true;
                ReadyToUpload.Image = Properties.Resources.check_mark_verified;
                infoText.Text = "Ready to upload.";
            }
        }

        private string NormalizeLossFactor(string input)
        {
            return input?.Trim()
                         .Replace("\u00A0", "") // non-breaking space
                         .Replace("\r", "")
                         .Replace("\n", "")
                         .ToLowerInvariant();
        }

        //private async void ExcelSheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    // Disable UI elements before processing
        //    ExcelSheetDropdownList.Enabled = false;
        //    UploadButton.Enabled = false;

        //    UpdateInfo.Visible = true;
        //    ReadyToUpload.Image = Properties.Resources.loading1;
        //    infoText.Text = "Reading data...";


        //    // Initialize Stopwatch
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();  // Start tracking execution time

        //    try
        //    {
        //        DataTable dt = tableCollection[ExcelSheetDropdownList.SelectedItem.ToString()];

        //        if (dt != null)
        //        {
        //            List<MHData_Class> list = new List<MHData_Class>();
        //            int rowCounter = 1;  // Initialize rowCounter here

        //            // Define columns for DataGridView (ensure this is done before adding any rows)
        //            MHLossUploadDatagrid.Columns.Clear(); // Clear any existing columns
        //            MHLossUploadDatagrid.Columns.Add("RowCounter", "Row");

        //            MHLossUploadDatagrid.Columns.Add("DistinctionCode", "DistinctionCode");
        //            MHLossUploadDatagrid.Columns.Add("ReferenceNo", "ReferenceNo");
        //            MHLossUploadDatagrid.Columns.Add("Section", "Section");
        //            MHLossUploadDatagrid.Columns.Add("DateEncountered", "DateEncountered");
        //            MHLossUploadDatagrid.Columns.Add("Plant", "Plant");
        //            MHLossUploadDatagrid.Columns.Add("ModelName", "ModelName");
        //            MHLossUploadDatagrid.Columns.Add("WorkCenter", "WorkCenter");
        //            MHLossUploadDatagrid.Columns.Add("Day_Night", "Day_Night");
        //            MHLossUploadDatagrid.Columns.Add("CostCenter", "CostCenter");
        //            MHLossUploadDatagrid.Columns.Add("ItemCode", "ItemCode");
        //            MHLossUploadDatagrid.Columns.Add("ItemText", "ItemText");
        //            MHLossUploadDatagrid.Columns.Add("StopTime", "StopTime");
        //            MHLossUploadDatagrid.Columns.Add("LineStopDetail", "LineStopDetail");
        //            MHLossUploadDatagrid.Columns.Add("LossFactor", "LossFactor");
        //            MHLossUploadDatagrid.Columns.Add("DirectMP", "DirectMP");
        //            MHLossUploadDatagrid.Columns.Add("SemiDirectMP", "SemiDirectMP");
        //            MHLossUploadDatagrid.Columns.Add("LossManhour", "LossManhour");
        //            MHLossUploadDatagrid.Columns.Add("LossMH_ForCOPQAmount", "LossMH_ForCOPQAmount");
        //            MHLossUploadDatagrid.Columns.Add("ApplyingApprovalStatus", "ApplyingApprovalStatus");
        //            MHLossUploadDatagrid.Columns.Add("ReceivingApprovalStatus", "ReceivingApprovalStatus");
        //            MHLossUploadDatagrid.Columns.Add("OverAllStatus", "OverAllStatus");
        //            MHLossUploadDatagrid.Columns.Add("QIConfirmation", "QIConfirmation");
        //            MHLossUploadDatagrid.Columns.Add("Mark", "Mark");
        //            MHLossUploadDatagrid.Columns.Add("UploadDate", "UploadDate");
        //            MHLossUploadDatagrid.Columns.Add("FiscalYear", "FiscalYear");
        //            MHLossUploadDatagrid.Columns.Add("LeadTime", "LeadTime");
        //            MHLossUploadDatagrid.Columns.Add("TargetClosedDate", "TargetClosedDate");


        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                MHData_Class obj = new MHData_Class();

        //                // Assign the row counter to the object
        //                obj.RowCounter = rowCounter++; // Increment the row counter

        //                //DistinctionCode = section + date + costcenter + workcenter + itemcode + stoptime + direct emp + semi-direct emp + loassmh + linestop
        //                obj.DistinctionCode = dt.Rows[i]["Section"].ToString() + "-" + Convert.ToDateTime(dt.Rows[i]["Date"]).ToShortDateString().ToString() + "-" + dt.Rows[i]["Cost center/Model"].ToString() + "-" + dt.Rows[i]["Work center"].ToString() + "-" + dt.Rows[i]["Item code"].ToString() + "-" + dt.Rows[i]["Stop time(min)"].ToString() + "-" + dt.Rows[i]["Direct employee"].ToString() + "-" + dt.Rows[i]["Semi-direct employee"].ToString() + "-" + dt.Rows[i]["Loss man-hour"].ToString() + "-" + dt.Rows[i]["Line stop content detail"].ToString();

        //                obj.ReferenceNo = dt.Rows[i]["Section"].ToString() + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "-" + (addOne += 1);
        //                obj.Section = dt.Rows[i]["Section"].ToString();
        //                obj.DateEncountered = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToShortDateString();
        //                obj.Plant = dt.Rows[i]["Plant"].ToString(); // no need to display
        //                obj.WorkCenter = dt.Rows[i]["Work center"].ToString(); // no need to display
        //                obj.Day_Night = dt.Rows[i]["Day/Night"].ToString(); // no need to display
        //                obj.CostCenter = dt.Rows[i]["Cost center/Model"].ToString();
        //                obj.ModelName = dt.Rows[i]["Cost center/Model name"].ToString();
        //                obj.ItemCode = dt.Rows[i]["Item code"].ToString(); // display as part code
        //                obj.ItemText = dt.Rows[i]["Item text"].ToString(); // no need to display
        //                obj.StopTime = dt.Rows[i]["Stop time(min)"].ToString();
        //                obj.LineStopDetail = dt.Rows[i]["Line stop content detail"].ToString();
        //                obj.LossFactor = dt.Rows[i]["Loss factor(EN)"].ToString();
        //                obj.DirectMP = dt.Rows[i]["Direct employee"].ToString();
        //                obj.SemiDirectMP = dt.Rows[i]["Semi-direct employee"].ToString();
        //                obj.LossManhour = dt.Rows[i]["Loss man-hour"].ToString();
        //                obj.LossMH_ForCOPQAmount = dt.Rows[i]["Loss man-hour"].ToString();
        //                obj.ApplyingApprovalStatus = "For Approval by COPQ PIC";
        //                obj.ReceivingApprovalStatus = "---";
        //                obj.OverAllStatus = "For Approval";
        //                obj.QIConfirmation = "---";
        //                obj.ApplyingCOPQPIC = "Pending Approval";
        //                obj.UploadDate = DateTime.Now.ToString("MM/dd/yyyy");
        //                obj.Mark = "0";


        //                // Retrieve LeadTime from the OverallMonitoring table based on DateEncountered
        //                DateTime dateEncountered = Convert.ToDateTime(dt.Rows[i]["Date"]);

        //                int leadTime = await GetLeadTimeFromOverallMonitoringAsync(con, dt.Rows[i]["Loss factor(EN)"].ToString()) + 2;  // Function to retrieve LeadTime
        //                obj.LeadTime = leadTime.ToString();


        //                // Calculate the TargetClosedDate and format it as a string
        //                DateTime targetDate = await GetTargetClosedDateAsync(con, dateEncountered, leadTime);
        //                obj.TargetClosedDate = targetDate.ToString("MM/dd/yyyy");


        //                if (Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("MMMM") == "January")
        //                {
        //                    obj.FiscalYear = Convert.ToDateTime(dt.Rows[i]["Date"]).AddYears(-1).ToString("yyyy"); //Subtract 1 year to current year
        //                }
        //                else if (Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("MMMM") == "February")
        //                {
        //                    obj.FiscalYear = Convert.ToDateTime(dt.Rows[i]["Date"]).AddYears(-1).ToString("yyyy"); //Subtract 1 year to current year
        //                }
        //                else if (Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("MMMM") == "March")
        //                {
        //                    obj.FiscalYear = Convert.ToDateTime(dt.Rows[i]["Date"]).AddYears(-1).ToString("yyyy"); //Subtract 1 year to current year
        //                }
        //                else
        //                {
        //                    obj.FiscalYear = Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("yyyy");
        //                }

        //                list.Add(obj);

        //                // Update the UI after processing each row

        //                // Add the object to the DataGridView row by row
        //                MHLossUploadDatagrid.Rows.Add(
        //                    obj.RowCounter,
        //                    obj.DistinctionCode,
        //                    obj.ReferenceNo,
        //                    obj.Section,
        //                    obj.DateEncountered,
        //                    obj.Plant,
        //                    obj.ModelName,
        //                    obj.WorkCenter,
        //                    obj.Day_Night,
        //                    obj.CostCenter,
        //                    obj.ItemCode,
        //                    obj.ItemText,
        //                    obj.StopTime,
        //                    obj.LineStopDetail,
        //                    obj.LossFactor,
        //                    obj.DirectMP,
        //                    obj.SemiDirectMP,
        //                    obj.LossManhour,
        //                    obj.LossMH_ForCOPQAmount,
        //                    obj.ApplyingApprovalStatus,
        //                    obj.ReceivingApprovalStatus,
        //                    obj.OverAllStatus,
        //                    obj.QIConfirmation,
        //                    obj.Mark,
        //                    obj.UploadDate,
        //                    obj.FiscalYear,
        //                    obj.LeadTime,
        //                    obj.TargetClosedDate

        //                );

        //                // Update the Label with the elapsed time (in seconds)
        //                LabelTimeElapsed.Text = $"Time Elapsed: {stopwatch.Elapsed.TotalSeconds:F2} seconds";

        //                // Force the UI to update by processing events
        //                Application.DoEvents();  // This updates the UI after each row
        //            }

        //            // After finishing processing all rows, stop the stopwatch
        //            stopwatch.Stop();

        //            // Display a final message indicating completion
        //            MessageBox.Show($"Processing completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds.", "Ready For Upload!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            //MHLossUploadDatagrid.DataSource = list;

        //            if (ActionDropdownList.Text == "Delete")
        //            {
        //                MHLossUploadDatagrid.Columns["ReferenceNo"].Visible = false;
        //                MHLossUploadDatagrid.Columns["LossMH_ForCOPQAmount"].Visible = false;
        //                MHLossUploadDatagrid.Columns["ApplyingApprovalStatus"].Visible = false;
        //                MHLossUploadDatagrid.Columns["ReceivingApprovalStatus"].Visible = false;
        //                MHLossUploadDatagrid.Columns["OverAllStatus"].Visible = false;
        //                MHLossUploadDatagrid.Columns["QIConfirmation"].Visible = false;
        //                MHLossUploadDatagrid.Columns["UploadDate"].Visible = false;
        //                MHLossUploadDatagrid.Columns["ApplyingCOPQPIC"].Visible = false;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        stopwatch.Stop();  // Stop Stopwatch

        //        // Update UI elements
        //        rowCount.Text = $"{MHLossUploadDatagrid.RowCount} rows";
        //        LabelTimeElapsed.Text = $"{stopwatch.Elapsed.TotalSeconds:F2} seconds"; // Display accurate time
        //        ExcelSheetDropdownList.Enabled = true;
        //        UploadButton.Enabled = true;

        //        UpdateInfo.Visible = true;
        //        ReadyToUpload.Image = Properties.Resources.check_mark_verified;
        //        infoText.Text = "Ready to upload.";
        //        ReadyToUpload.Image = Properties.Resources.check_mark_verified;
        //    }
        //}

        private async void test()
        {
            //LoadingForm LoadingForm = new LoadingForm();
            //LoadingForm.Show();

            ExcelSheetDropdownList.Enabled = false;
            UploadButton.Enabled = false;
            ReadyToUpload.Visible = false;


            // Initialize the Stopwatch to track time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();  // Start the stopwatch immediately

            try
            {

                await Task.Delay(5000);  // Simulating a delay of 5 seconds

                DataTable dt = tableCollection[ExcelSheetDropdownList.SelectedItem.ToString()];

                if (dt != null)
                {
                    List<MHData_Class> list = new List<MHData_Class>();
                    int rowCounter = 1;  // Initialize rowCounter here

                    // Define columns for DataGridView (ensure this is done before adding any rows)
                    MHLossUploadDatagrid.Columns.Clear(); // Clear any existing columns
                    MHLossUploadDatagrid.Columns.Add("RowCounter", "Row");

                    MHLossUploadDatagrid.Columns.Add("DistinctionCode", "DistinctionCode");
                    MHLossUploadDatagrid.Columns.Add("ReferenceNo", "ReferenceNo");
                    MHLossUploadDatagrid.Columns.Add("Section", "Section");
                    MHLossUploadDatagrid.Columns.Add("DateEncountered", "DateEncountered");
                    MHLossUploadDatagrid.Columns.Add("Plant", "Plant");
                    MHLossUploadDatagrid.Columns.Add("ModelName", "ModelName");
                    MHLossUploadDatagrid.Columns.Add("WorkCenter", "WorkCenter");
                    MHLossUploadDatagrid.Columns.Add("Day_Night", "Day_Night");
                    MHLossUploadDatagrid.Columns.Add("CostCenter", "CostCenter");
                    MHLossUploadDatagrid.Columns.Add("ItemCode", "ItemCode");
                    MHLossUploadDatagrid.Columns.Add("ItemText", "ItemText");
                    MHLossUploadDatagrid.Columns.Add("StopTime", "StopTime");
                    MHLossUploadDatagrid.Columns.Add("LineStopDetail", "LineStopDetail");
                    MHLossUploadDatagrid.Columns.Add("LossFactor", "LossFactor");
                    MHLossUploadDatagrid.Columns.Add("DirectMP", "DirectMP");
                    MHLossUploadDatagrid.Columns.Add("SemiDirectMP", "SemiDirectMP");
                    MHLossUploadDatagrid.Columns.Add("LossManhour", "LossManhour");
                    MHLossUploadDatagrid.Columns.Add("LossMH_ForCOPQAmount", "LossMH_ForCOPQAmount");
                    MHLossUploadDatagrid.Columns.Add("ApplyingApprovalStatus", "ApplyingApprovalStatus");
                    MHLossUploadDatagrid.Columns.Add("ReceivingApprovalStatus", "ReceivingApprovalStatus");
                    MHLossUploadDatagrid.Columns.Add("OverAllStatus", "OverAllStatus");
                    MHLossUploadDatagrid.Columns.Add("QIConfirmation", "QIConfirmation");
                    MHLossUploadDatagrid.Columns.Add("Mark", "Mark");
                    MHLossUploadDatagrid.Columns.Add("UploadDate", "UploadDate");
                    MHLossUploadDatagrid.Columns.Add("FiscalYear", "FiscalYear");
                    MHLossUploadDatagrid.Columns.Add("LeadTime", "LeadTime");
                    MHLossUploadDatagrid.Columns.Add("TargetClosedDate", "TargetClosedDate");






                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MHData_Class obj = new MHData_Class();

                        // Assign the row counter to the object
                        obj.RowCounter = rowCounter++; // Increment the row counter

                        obj.DistinctionCode = dt.Rows[i]["Section"].ToString() + "-" + Convert.ToDateTime(dt.Rows[i]["Date"]).ToShortDateString().ToString() + "-" + dt.Rows[i]["Cost center/Model"].ToString() + "-" + dt.Rows[i]["Work center"].ToString() + "-" + dt.Rows[i]["Item code"].ToString() + "-" + dt.Rows[i]["Stop time(min)"].ToString() + "-" + dt.Rows[i]["Direct employee"].ToString() + "-" + dt.Rows[i]["Semi-direct employee"].ToString() + "-" + dt.Rows[i]["Loss man-hour"].ToString() + "-" + dt.Rows[i]["Line stop content detail"].ToString();

                        obj.ReferenceNo = dt.Rows[i]["Section"].ToString() + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "-" + (addOne += 1);
                        obj.Section = dt.Rows[i]["Section"].ToString();
                        obj.DateEncountered = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToShortDateString();
                        obj.Plant = dt.Rows[i]["Plant"].ToString(); // no need to display
                        obj.WorkCenter = dt.Rows[i]["Work center"].ToString(); // no need to display
                        obj.Day_Night = dt.Rows[i]["Day/Night"].ToString(); // no need to display
                        obj.CostCenter = dt.Rows[i]["Cost center/Model"].ToString();
                        obj.ModelName = dt.Rows[i]["Cost center/Model name"].ToString();
                        obj.ItemCode = dt.Rows[i]["Item code"].ToString(); // display as part code
                        obj.ItemText = dt.Rows[i]["Item text"].ToString(); // no need to display
                        obj.StopTime = dt.Rows[i]["Stop time(min)"].ToString();
                        obj.LineStopDetail = dt.Rows[i]["Line stop content detail"].ToString();
                        obj.LossFactor = dt.Rows[i]["Loss factor(EN)"].ToString();
                        obj.DirectMP = dt.Rows[i]["Direct employee"].ToString();
                        obj.SemiDirectMP = dt.Rows[i]["Semi-direct employee"].ToString();
                        obj.LossManhour = dt.Rows[i]["Loss man-hour"].ToString();
                        obj.LossMH_ForCOPQAmount = dt.Rows[i]["Loss man-hour"].ToString();
                        obj.ApplyingApprovalStatus = "For Approval by COPQ PIC";
                        obj.ReceivingApprovalStatus = "---";
                        obj.OverAllStatus = "For Approval";
                        obj.QIConfirmation = "---";
                        obj.UploadDate = DateTime.Now.ToString("MM/dd/yyyy");
                        obj.Mark = "0";

                        //obj.ApplyingApprovalStatus = "For Approval by COPQ PIC";
                        //obj.ReceivingApprovalStatus = "---";
                        //obj.OverAllStatus = "For Approval";
                        //obj.QIConfirmation = "---";
                        //obj.UploadDate = DateTime.Now.ToString("MM/dd/yyyy");

                        //obj.ApplyingCOPQPIC = "Pending Approval";




                        // Retrieve LeadTime from the OverallMonitoring table based on DateEncountered
                        DateTime dateEncountered = Convert.ToDateTime(dt.Rows[i]["Date"]);
                        int leadTime = await GetLeadTimeFromOverallMonitoringAsync(con, dt.Rows[i]["Loss factor(EN)"].ToString()) + 2;  // Function to retrieve LeadTime
                        obj.LeadTime = leadTime.ToString();

                        // Calculate the TargetClosedDate and format it as a string
                        DateTime targetDate = await GetTargetClosedDateAsync(con, dateEncountered, leadTime);
                        obj.TargetClosedDate = targetDate.ToString("MM/dd/yyyy");


                        if (Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("MMMM") == "January")
                        {
                            obj.FiscalYear = Convert.ToDateTime(dt.Rows[i]["Date"]).AddYears(-1).ToString("yyyy"); //Subtract 1 year to current year
                        }
                        else if (Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("MMMM") == "February")
                        {
                            obj.FiscalYear = Convert.ToDateTime(dt.Rows[i]["Date"]).AddYears(-1).ToString("yyyy"); //Subtract 1 year to current year
                        }
                        else if (Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("MMMM") == "March")
                        {
                            obj.FiscalYear = Convert.ToDateTime(dt.Rows[i]["Date"]).AddYears(-1).ToString("yyyy"); //Subtract 1 year to current year
                        }
                        else
                        {
                            obj.FiscalYear = Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("yyyy");
                        }

                        list.Add(obj);

                        // Update the UI after processing each row

                        // Add the object to the DataGridView row by row
                        MHLossUploadDatagrid.Rows.Add(
                            obj.RowCounter,
                            obj.DistinctionCode,
                            obj.ReferenceNo,
                            obj.Section,
                            obj.DateEncountered,
                            obj.Plant,
                            obj.ModelName,
                            obj.WorkCenter,
                            obj.Day_Night,
                            obj.CostCenter,
                            obj.ItemCode,
                            obj.ItemText,
                            obj.StopTime,
                            obj.LineStopDetail,
                            obj.LossFactor,
                            obj.DirectMP,
                            obj.SemiDirectMP,
                            obj.LossManhour,
                            obj.LossMH_ForCOPQAmount,
                            obj.ApplyingApprovalStatus,
                            obj.ReceivingApprovalStatus,
                            obj.OverAllStatus,
                            obj.QIConfirmation,
                            obj.Mark,
                            obj.UploadDate,
                            obj.FiscalYear,
                            obj.LeadTime,
                            obj.TargetClosedDate

                        );

                        // Update the Label with the elapsed time (in seconds)
                        LabelTimeElapsed.Text = $"Time Elapsed: {stopwatch.Elapsed.TotalSeconds:F2} seconds";

                        // Force the UI to update by processing events
                        Application.DoEvents();  // This updates the UI after each row
                    }

                    // After finishing processing all rows, stop the stopwatch
                    stopwatch.Stop();

                    // Display a final message indicating completion
                    MessageBox.Show($"Processing completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds.", "Ready For Upload!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //MHLossUploadDatagrid.DataSource = list;

                    if (ActionDropdownList.Text == "Delete")
                    {
                        MHLossUploadDatagrid.Columns["ReferenceNo"].Visible = false;
                        MHLossUploadDatagrid.Columns["LossMH_ForCOPQAmount"].Visible = false;
                        MHLossUploadDatagrid.Columns["ApplyingApprovalStatus"].Visible = false;
                        MHLossUploadDatagrid.Columns["ReceivingApprovalStatus"].Visible = false;
                        MHLossUploadDatagrid.Columns["OverAllStatus"].Visible = false;
                        MHLossUploadDatagrid.Columns["QIConfirmation"].Visible = false;
                        MHLossUploadDatagrid.Columns["UploadDate"].Visible = false;
                        MHLossUploadDatagrid.Columns["ApplyingCOPQPIC"].Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //LoadingForm.Close();

                rowCount.Text = MHLossUploadDatagrid.RowCount.ToString() + "rows";
                LabelTimeElapsed.Text = (stopwatch.Elapsed.TotalSeconds / 60).ToString("F2") + " minutes.";
                // Re-enable the dropdown
                ExcelSheetDropdownList.Enabled = true;
                UploadButton.Enabled = true;
                ReadyToUpload.Visible = true;
            }
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



        //===================================================================================================================>>>>>>>>>>>>>

        private void UpdateMHLoss2_Load(object sender, EventArgs e)
        {
            SelectMHLossLastUpdated(); //--> Show last update date from DB to label

            UpdateInfo.Visible = false;
        }

        //===================================================================================================================>>>>>>>>>>>>>

        //private void InsertManhourLossData_TEST()
        //{
        //    DapperPlusManager.Entity<MHData_Class>().Table("ManhourLossData_TEST");
        //    List<MHData_Class> MHLossData_TEST = manhourLossData2BindingSource2.DataSource as List<MHData_Class>;
        //    if (MHLossData_TEST != null)
        //    {
        //        using (IDbConnection db = new SqlConnection("Server=apbiph1131;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
        //        //using (IDbConnection db = con)
        //        {
        //            db.BulkInsert(MHLossData_TEST);
        //        }
        //    }

        //    MessageBox.Show("MH Data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    //Clear fields after upload
        //    FilePath.Clear();
        //    ExcelSheetDropdownList.Text = "";
        //    MHLossUploadDatagrid.DataSource = null;
        //    this.Close();
        //}

        //==================================================================================================================>>>>>>>>>>>>>

        private void InsertManhourLossData()
        {
            //DapperPlusManager.Entity<MHData_Class>().Table("ManhourLossData2");
            //List<MHData_Class> LossMHData = MHLossUploadDatagrid.DataSource as List<MHData_Class>;
            //if (LossMHData != null)
            //{
            //    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
            //    {
            //        db.BulkInsert(LossMHData);
            //    }
            //}

            ////Clear fields after upload
            //FilePath.Clear();
            //ExcelSheetDropdownList.Text = "";
            //MHLossUploadDatagrid.DataSource = null;
            //this.Close();
        }

        //==================================================================================================================>>>>>>>>>>>>>


        string ApplyingApprovalStatus = "";
        string ReceivingApprovalStatus = "";
        string OverallApproval = "";

        string ApplyingCOPQPIC = "";
        string ApplyingSPV = "";
        string ApplyingMGR = "";

        string ReceivingCOPQPIC = "";
        string ProcessInCharge = "";
        string ReceivingSPV = "";
        string ReceivingMGR = "";

        //private async Task InsertAndUpdateMHLoss()
        //{
        //    try
        //    {
        //        con.Close();
        //        await con.OpenAsync();

        //        // Create DataTables for bulk insert and batch update
        //        DataTable insertTable = new DataTable();
        //        DataTable updateTable = new DataTable();

        //        // Define table structure
        //        DefineInsertTable(insertTable);
        //        DefineUpdateTable(updateTable);

        //        foreach (DataGridViewRow row in MHLossUploadDatagrid.Rows)
        //        {
        //            if (row.IsNewRow) continue;

        //            SqlCommand selectCmd = new SqlCommand("SP_SelectMHLoss", con)
        //            {
        //                CommandType = CommandType.StoredProcedure
        //            };
        //            selectCmd.Parameters.AddWithValue("@DistinctionCode", row.Cells["DistinctionCode"].Value.ToString());

        //            SqlDataAdapter da = new SqlDataAdapter(selectCmd);
        //            DataTable dt = new DataTable();
        //            await Task.Run(() => da.Fill(dt));

        //            if (dt.Rows.Count > 0)
        //            {
        //                // Add row to update DataTable
        //                AddToUpdateTable(row, dt.Rows[0], updateTable);
        //            }
        //            else
        //            {
        //                // Add row to insert DataTable
        //                AddToInsertTable(row, insertTable);
        //            }
        //        }

        //        // Perform batch update
        //        if (updateTable.Rows.Count > 0)
        //        {
        //            await UpdateMHLossBatch(updateTable);
        //        }

        //        // Perform bulk insert
        //        if (insertTable.Rows.Count > 0)
        //        {
        //            await BulkInsertMHLoss(insertTable);
        //        }

        //        con.Close();
        //        MHLossUploadDatagrid.DataSource = null;
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //    }
        //}

        //private void DefineInsertTable(DataTable table)
        //{
        //    table.Columns.Add("DistinctionCode", typeof(string));
        //    table.Columns.Add("ReferenceNo", typeof(string));
        //    table.Columns.Add("Section", typeof(string));
        //    table.Columns.Add("DateEncountered", typeof(string));
        //    table.Columns.Add("WorkCenter", typeof(string));
        //    table.Columns.Add("CostCenter", typeof(string));
        //    table.Columns.Add("Day_Night", typeof(string));
        //    table.Columns.Add("ModelName", typeof(string));
        //    table.Columns.Add("ItemCode", typeof(string));
        //    table.Columns.Add("ItemText", typeof(string));
        //    table.Columns.Add("StopTime", typeof(string));
        //    table.Columns.Add("LineStopDetail", typeof(string));
        //    table.Columns.Add("LossFactor", typeof(string));
        //    table.Columns.Add("DirectMP", typeof(string));
        //    table.Columns.Add("SemiDirectMP", typeof(string));
        //    table.Columns.Add("LossManhour", typeof(string));
        //    table.Columns.Add("LossMH_ForCOPQAmount", typeof(string));
        //    table.Columns.Add("ApplyingApprovalStatus", typeof(string));
        //    table.Columns.Add("ReceivingApprovalStatus", typeof(string));
        //    table.Columns.Add("OverAllStatus", typeof(string));
        //    table.Columns.Add("QIConfirmation", typeof(string));
        //    table.Columns.Add("TargetClosedDate", typeof(string));
        //    table.Columns.Add("UploadDate", typeof(string));
        //}

        //private void DefineUpdateTable(DataTable table)
        //{
        //    table.Columns.Add("DistinctionCode", typeof(string));
        //    table.Columns.Add("ApplyingApprovalStatus", typeof(string));
        //    table.Columns.Add("ReceivingApprovalStatus", typeof(string));
        //    table.Columns.Add("OverallStatus", typeof(string));
        //    table.Columns.Add("ApplyingCOPQPIC", typeof(string));
        //    table.Columns.Add("ApplyingSPV", typeof(string));
        //    table.Columns.Add("ApplyingMGR", typeof(string));
        //    table.Columns.Add("ReceivingCOPQPIC", typeof(string));
        //    table.Columns.Add("COPQProcessInCharge", typeof(string));
        //    table.Columns.Add("ReceivingSPV", typeof(string));
        //    table.Columns.Add("ReceivingMGR", typeof(string));
        //    table.Columns.Add("TargetClosedDate", typeof(string));
        //    table.Columns.Add("Day_Night", typeof(string));
        //}

        //private void AddToInsertTable(DataGridViewRow row, DataTable insertTable)
        //{
        //    insertTable.Rows.Add(
        //        row.Cells["DistinctionCode"].Value.ToString(),
        //        row.Cells["Section"].Value.ToString() + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"),
        //        row.Cells["Section"].Value.ToString(),
        //        DateTime.Parse(row.Cells["DateEncountered"].Value.ToString()),
        //        row.Cells["WorkCenter"].Value.ToString(),
        //        row.Cells["CostCenter"].Value.ToString(),
        //        row.Cells["Day_Night"].Value.ToString(),
        //        row.Cells["ModelName"].Value.ToString(),
        //        row.Cells["ItemCode"].Value.ToString(),
        //        row.Cells["ItemText"].Value.ToString(),
        //        row.Cells["StopTime"].Value.ToString(),
        //        row.Cells["LineStopDetail"].Value.ToString(),
        //        row.Cells["LossFactor"].Value.ToString(),
        //        row.Cells["DirectMP"].Value.ToString(),
        //        row.Cells["SemiDirectMP"].Value.ToString(),
        //        row.Cells["LossManhour"].Value.ToString(),
        //        row.Cells["LossManhour"].Value.ToString(),
        //        "For Approval by COPQ PIC",
        //        "---",
        //        "For Approval",
        //        "---",
        //        row.Cells["TargetClosedDate"].Value.ToString(),
        //        DateTime.Now
        //    );
        //}

        //private void AddToUpdateTable(DataGridViewRow row, DataRow existingRow, DataTable updateTable)
        //{
        //    updateTable.Rows.Add(
        //        row.Cells["DistinctionCode"].Value.ToString(),
        //        existingRow["ApplyingApprovalStatus"].ToString(),
        //        existingRow["ReceivingApprovalStatus"].ToString(),
        //        existingRow["OverAllStatus"].ToString(),
        //        existingRow["ApplyingCOPQPIC"].ToString(),
        //        existingRow["ApplyingSPV"].ToString(),
        //        existingRow["ApplyingMGR"].ToString(),
        //        existingRow["ReceivingCOPQPIC"].ToString(),
        //        existingRow["COPQProcessInCharge"].ToString(),
        //        existingRow["ReceivingSPV"].ToString(),
        //        existingRow["ReceivingMGR"].ToString(),
        //        row.Cells["TargetClosedDate"].Value.ToString(),
        //        existingRow["Day_Night"].ToString()
        //    );
        //}

        //private async Task BulkInsertMHLoss(DataTable insertTable)
        //{
        //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
        //    {
        //        bulkCopy.DestinationTableName = "COPQApprovalData2";
        //        await bulkCopy.WriteToServerAsync(insertTable);
        //    }
        //}

        //private async Task UpdateMHLossBatch(DataTable updateTable)
        //{
        //    using (SqlCommand cmd = new SqlCommand("SP_UpdateCOPQMHLossStatus", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlParameter param = cmd.Parameters.AddWithValue("@UpdateTable", updateTable);
        //        param.SqlDbType = SqlDbType.Structured;
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}


        private async Task InsertAndUpdateMHLoss()
        {
            con.Close();

            try
            {
                // Open the connection asynchronously before starting the loop
                await con.OpenAsync();

                foreach (DataGridViewRow row in MHLossUploadDatagrid.Rows)
                {
                    if (row.IsNewRow) continue; // Skip new empty rows in the DataGridView

                    SqlCommand SelectMHLoss = new SqlCommand("SP_SelectMHLoss", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 1800 // Set command timeout to 30 minutes
                    };

                    SelectMHLoss.Parameters.AddWithValue("@DistinctionCode", row.Cells["DistinctionCode"].Value.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(SelectMHLoss);
                    DataTable dt = new DataTable();

                    // Fill the DataTable asynchronously
                    await Task.Run(() => da.Fill(dt));


                    if (dt.Rows.Count > 0)
                    {
                        //// Data exists, so perform update
                        //var dataRow = dt.Rows[0];

                        //// Get values from the fetched data row
                        //string ApplyingApprovalStatus = dataRow["ApplyingApprovalStatus"].ToString();
                        //string ReceivingApprovalStatus = dataRow["ReceivingApprovalStatus"].ToString();
                        //string OverallApproval = dataRow["OverAllStatus"].ToString();
                        //string ApplyingCOPQPIC = dataRow["ApplyingCOPQPIC"].ToString();
                        //string ApplyingSPV = dataRow["ApplyingSPV"].ToString();
                        //string ApplyingMGR = dataRow["ApplyingMGR"].ToString();
                        //string ReceivingCOPQPIC = dataRow["ReceivingCOPQPIC"].ToString();
                        //string ProcessInCharge = dataRow["COPQProcessInCharge"].ToString();
                        //string ReceivingSPV = dataRow["ReceivingSPV"].ToString();
                        //string ReceivingMGR = dataRow["ReceivingMGR"].ToString();

                        //SqlCommand UpdateMHLossData = new SqlCommand("SP_UpdateCOPQMHLossStatus", con)
                        //{
                        //    CommandType = CommandType.StoredProcedure
                        //};
                        //UpdateMHLossData.Parameters.AddWithValue("@DistinctionCode", row.Cells["DistinctionCode"].Value.ToString());
                        //UpdateMHLossData.Parameters.AddWithValue("@ApplyingApprovalStatus", ApplyingApprovalStatus);
                        //UpdateMHLossData.Parameters.AddWithValue("@ReceivingApprovalStatus", ReceivingApprovalStatus);
                        //UpdateMHLossData.Parameters.AddWithValue("@OverallStatus", OverallApproval);
                        //UpdateMHLossData.Parameters.AddWithValue("@ApplyingCOPQPIC", ApplyingCOPQPIC);
                        //UpdateMHLossData.Parameters.AddWithValue("@ApplyingSPV", ApplyingSPV);
                        //UpdateMHLossData.Parameters.AddWithValue("@ApplyingMGR", ApplyingMGR);
                        //UpdateMHLossData.Parameters.AddWithValue("@ReceivingCOPQPIC", ReceivingCOPQPIC);
                        //UpdateMHLossData.Parameters.AddWithValue("@COPQProcessInCharge", ProcessInCharge);
                        //UpdateMHLossData.Parameters.AddWithValue("@ReceivingSPV", ReceivingSPV);
                        //UpdateMHLossData.Parameters.AddWithValue("@ReceivingMGR", ReceivingMGR);
                        //UpdateMHLossData.Parameters.AddWithValue("@TargetClosedDate", row.Cells["TargetClosedDate"].Value.ToString());
                        //await UpdateMHLossData.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        // Data doesn't exist, so perform insert
                        SqlCommand InsertUSer = new SqlCommand("SP_InsertCOPQMHLossData", con)
                        {
                            CommandType = CommandType.StoredProcedure,
                            CommandTimeout = 1800 // Set command timeout to 30 minutes
                        };

                        InsertUSer.Parameters.AddWithValue("@DistinctionCode", row.Cells["DistinctionCode"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@ReferenceNo", row.Cells["Section"].Value.ToString() + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "-" + (addOne += 1));
                        InsertUSer.Parameters.AddWithValue("@Section", row.Cells["Section"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@DateEncountered", row.Cells["DateEncountered"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@WorkCenter", row.Cells["WorkCenter"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@CostCenter", row.Cells["CostCenter"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@DayNight", row.Cells["Day_Night"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@ModelName", row.Cells["ModelName"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@ItemCode", row.Cells["ItemCode"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@ItemText", row.Cells["ItemText"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@StopTime", row.Cells["StopTime"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@LineStopDetail", row.Cells["LineStopDetail"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@LossFactor", row.Cells["LossFactor"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@DirectMP", row.Cells["DirectMP"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@SemiDirectMP", row.Cells["SemiDirectMP"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@LossManhour", row.Cells["LossManhour"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@LossMH_ForCOPQAmount", row.Cells["LossManhour"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@ApplyingApprovalStatus", "For Approval by COPQ PIC");
                        InsertUSer.Parameters.AddWithValue("@ReceivingApprovalStatus", "---");
                        InsertUSer.Parameters.AddWithValue("@OverAllStatus", "For Approval");
                        InsertUSer.Parameters.AddWithValue("@QIConfirmation", "---");
                        InsertUSer.Parameters.AddWithValue("@TargetClosedDate", row.Cells["TargetClosedDate"].Value.ToString());
                        InsertUSer.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("MM/dd/yyyy"));

                        // Execute the insert command asynchronously
                        await InsertUSer.ExecuteNonQueryAsync();
                    }
                }

                con.Close();

                // Clear UI elements after the loop
                FilePath.Clear();
                ExcelSheetDropdownList.Items.Clear();
                ActionDropdownList.Items.Clear();
                MHLossUploadDatagrid.DataSource = null;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close connection safely
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        string Email = string.Empty;
        int NoOfCOPQPendingApproval;
        string innerString;

        private void EmailToApplyingSectionCOPQPIC()
        {
        
            
        }

        private void InsertCOPQApprovalData()
        {
            DapperPlusManager.Entity<MHData_Class>().Table("COPQApprovalData2");

            List<MHData_Class> COPQApprovalData = MHLossUploadDatagrid.DataSource as List<MHData_Class>;

            if (COPQApprovalData != null)
            {
                using (IDbConnection db2 = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                {
                    db2.BulkInsert(COPQApprovalData);
                }
            }


            DapperPlusManager.Entity<MHData_Class>().Table("COPQApprovalDataStatus");

            List<MHData_Class> COPQApprovalDataStatus = MHLossUploadDatagrid.DataSource as List<MHData_Class>;

            if (COPQApprovalDataStatus != null)
            {
                using (IDbConnection db3 = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                {
                    db3.BulkInsert(COPQApprovalDataStatus);
                }
            }

            //--------------------------------------------------------------------------------------------

            //DapperPlusManager.Entity<MHData_Class>().Table("COPQApprovalDataStatus");

            //List<MHData_Class> COPQApprovalDataStatus = MHLossUploadDatagrid.DataSource as List<MHData_Class>;

            //if (COPQApprovalDataStatus != null)
            //{
            //    using (IDbConnection db3 = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
            //    {
            //        db3.BulkInsert(COPQApprovalDataStatus);
            //    }
            //}


            //DapperPlusManager.Entity<MHData_Class>().Table("ManhourLossData2");

            //List<MHData_Class> LossMHData = MHLossUploadDatagrid.DataSource as List<MHData_Class>;

            //if (LossMHData != null)
            //{
            //    using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
            //    {
            //        db.BulkInsert(LossMHData);
            //    }
            //}


            /* NOTE:
             Under COPQApprovalData and ManhourLossData2 Table if you expand it you may see the Trigger folder,  
             open it to see the trigger function that automatically update the approval status
             if the responsible section of data uploaded are equipment engineering. 
            */

        }

        //==================================================================================================================>>>>>>>>>>>>>


        private void InsertRequestForApprovalData()
        {
            DapperPlusManager.Entity<MHData_Class>().Table("COPQApprovalData");
            List<MHData_Class> COPQApprovalData = MHLossUploadDatagrid.DataSource as List<MHData_Class>;
            if (COPQApprovalData != null)
            {
                using (IDbConnection db = new SqlConnection("Server=APBIPHBPSDB01;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                //using (IDbConnection db2 = con)
                {
                    db.BulkInsert(COPQApprovalData);
                }
            }


            /* NOTE:
             Inside COPQApprovalData Table 
             has a trigger function that can automatically aupdate the approval status
             when the responsible section of datas uploaded are equipment engineering. 
            */
        }

        private void RemovedDeletedMHLoss()
        {
            try
            {

                foreach (DataGridViewRow row in MHLossUploadDatagrid.Rows)
                {
                    // FUNCTION FOR CHECKING IF MH LOSS ALREADY EXIST IN DB.

                    SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand SelectMHLoss = new SqlCommand("SP_SelectMHLoss", con);
                    SelectMHLoss.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = SelectMHLoss.ExecuteReader();
                    reader.Read();


                    if (reader.HasRows == true)
                    {

                        //Update the status of existing data in COPQ Aprroval table
                        SqlCommand UpdateMHLossStatusInCOPQApproval = new SqlCommand("SP_UpdateMHLossStatusInCOPQApproval", con);
                        UpdateMHLossStatusInCOPQApproval.CommandType = CommandType.StoredProcedure;
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@DateEncountered", row.Cells["DateEncountered"].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@Section", row.Cells["Section"].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@WorkCenter", row.Cells["WorkCenter"].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@CostCenter", row.Cells["CostCenter"].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@LineStopDetail", row.Cells["LineStopDetail"].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@StopTime", row.Cells["StopTime"].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@DirectMP", row.Cells["DirectMP"].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@SemiDirectMP", row.Cells["SemiDirectMP"].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@LossManhour", row.Cells["LossManhour"].Value.ToString());
                        reader.Close();
                        UpdateMHLossStatusInCOPQApproval.ExecuteNonQuery();
                       
                    }


                    con.Close();
                }

                MessageBox.Show("MH loss data cancelled successfully.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FilePath.Clear();
                ExcelSheetDropdownList.Items.Clear();
                ActionDropdownList.Items.Clear();
                MHLossUploadDatagrid.DataSource = null;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private async void UploadButton_Click(object sender, EventArgs e)
        {
            if (MHLossUploadDatagrid.Rows.Count == 0)
            {
                MessageBox.Show("No data to upload.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UploadButton.Enabled = false;
            ReadyToUpload.Image = Properties.Resources.upload;
            infoText.Text = "Uploading...";
            var loadingForm = new LoadingForm();
            loadingForm.Show();

            try
            {
                // Prepare schema
                var dataTable = new DataTable();
                string[] columns = {
                "DistinctionCode", "ReferenceNo", "Section", "CostCenter", "WorkCenter", "DateEncountered",
                "LineStopDetail", "LossFactor", "ItemCode", "DirectMP", "SemiDirectMP", "StopTime",
                "LossManhour", "LossMH_ForCOPQAmount", "ModelName", "ApplyingApprovalStatus",
                "ReceivingApprovalStatus", "OverAllStatus", "QIConfirmation", "ApplyingCOPQPIC",
                "UploadDate", "TargetClosedDate", "Day_Night"
            };

                foreach (string col in columns)
                dataTable.Columns.Add(col, typeof(string));

                var distinctionCodes = new List<string>();
                foreach (DataGridViewRow gridRow in MHLossUploadDatagrid.Rows)
                {
                    if (gridRow.IsNewRow) continue;

                    var newRow = dataTable.NewRow();
                    foreach (string col in columns)
                    {
                        string val = gridRow.Cells[col]?.Value?.ToString()?.Trim() ?? "";
                        newRow[col] = val;
                    }

                    // Normalize DistinctionCode (trim, remove special characters)
                    string normCode = NormalizeCode(newRow["DistinctionCode"].ToString());
                    newRow["DistinctionCode"] = normCode;
                    dataTable.Rows.Add(newRow);

                    distinctionCodes.Add($"'{normCode.Replace("'", "''")}'");
                }

                using (var con = new SqlConnection(SQLControl.MHMS_Conn))
                {
                    await con.OpenAsync();

                    // Get existing DistinctionCodes
                    var existingCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    if (distinctionCodes.Count > 0)
                    {
                        string inClause = string.Join(",", distinctionCodes);
                        var checkCmd = new SqlCommand($"SELECT DistinctionCode FROM COPQApprovalData2 WHERE DistinctionCode IN ({inClause})", con);
                        using (var reader = await checkCmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                existingCodes.Add(NormalizeCode(reader.GetString(0)));
                            }
                        }
                    }

                    // Filter out duplicates
                    var filteredRows = dataTable.AsEnumerable()
                        .Where(row => !existingCodes.Contains(NormalizeCode(row["DistinctionCode"].ToString())))
                        .ToList();

                    if (filteredRows.Count == 0)
                    {
                        MessageBox.Show("No new data to upload (all records are duplicates).", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var filteredTable = filteredRows.CopyToDataTable();

                    try
                    {
                        using (var bulkCopy = new SqlBulkCopy(
                            con,
                            SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints,
                            null))
                        {
                            bulkCopy.DestinationTableName = "COPQApprovalData2";
                            bulkCopy.BatchSize = 1000;
                            bulkCopy.BulkCopyTimeout = 300;

                            foreach (DataColumn col in filteredTable.Columns)
                                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);

                            await bulkCopy.WriteToServerAsync(filteredTable);
                        }

                        // Only reaches here if no exception occurred above
                        if (filteredTable.Rows.Count > 0)
                        {
                            // Log uploader
                            string uploader = LoginForm.FirstName + " " + LoginForm.LastName;

                            using (var logCmd = new SqlCommand())
                            {
                                logCmd.Connection = con;
                                foreach (DataRow row in filteredTable.Rows)
                                {
                                    string logQuery = @"INSERT INTO COPQ_ChangeLog (DistinctionCode, Uploader) 
                                    VALUES (@DistinctionCode, @Uploader)";

                                    logCmd.CommandText = logQuery;
                                    logCmd.Parameters.Clear();
                                    logCmd.Parameters.AddWithValue("@DistinctionCode", row["DistinctionCode"]?.ToString() ?? "");
                                    logCmd.Parameters.AddWithValue("@Uploader", uploader);

                                    await logCmd.ExecuteNonQueryAsync();
                                }
                            }

                            // Send notification
                            COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover();
                            await COPQ_SendEmail.SendEmailToCOPQPIC();
                        }

                        //MessageBox.Show("Upload and email notification completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Upload failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                MessageBox.Show("Upload successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upload failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingForm.Close();
                UploadButton.Enabled = true;
                ReadyToUpload.Image = Properties.Resources.check_mark_verified;
                infoText.Text = "Upload complete.";
            }

            //if (FilePath.Text == "")
            //{
            //    MessageBox.Show("Please select File!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    FilePath.Select();
            //}
            ////else if (ActionDropdownList.Text == "")
            ////{
            ////    MessageBox.Show("Please select data type!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////}
            //else if (ExcelSheetDropdownList.Text == "")
            //{
            //    MessageBox.Show("Please select Sheet!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    ExcelSheetDropdownList.Select();
            //}
            //else
            //{

            //    // Disable the button to prevent multiple clicks during the operation
            //    UploadButton.Enabled = false;

            //    ReadyToUpload.Image = Properties.Resources.upload;
            //    infoText.Text = "Data uploading...";

            //    LoadingForm LoadingForm = new LoadingForm();
            //    LoadingForm.Show();

            //    try
            //    {

            //        // Simulate a long-running operation

            //        await InsertAndUpdateMHLoss();


            //        //Trigger to reload data table
            //        COPQManhourLossForm.HaveNewUploadedData = true;

            //        //Send email to COPQ PIC
            //        COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover();
            //        await COPQ_SendEmail.SendEmailToCOPQPIC();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    finally
            //    {
            //        LoadingForm.Close();

            //        UpdateInfo.Visible = false;

            //        // Re-enable the button
            //        UploadButton.Enabled = true;

            //        MessageBox.Show("MH loss data inserted successfully", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }

            //}


        }

        // 🧹 Utility: Normalize DistinctionCode
        private string NormalizeCode(string input)
        {
            return (input ?? "")
                .Trim()
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("\t", "")
                .ToLowerInvariant(); // use lowercase for consistent matching
        }



        private void UpdateMonhtlyMHLossRate()
        {
            // -> SQL query to update approval status
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand UpdateMHLossRate = new SqlCommand("SP_Update_Monthly_MHLossRate", con);
            UpdateMHLossRate.CommandType = CommandType.StoredProcedure;
            UpdateMHLossRate.ExecuteNonQuery();
            con.Close();
        }

        private void UpdateYearlyMHLossRate()
        {
            // -> SQL query to update approval status
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand UpdateMHLossRate = new SqlCommand("SP_Update_Yearly_MHLossRate", con);
            UpdateMHLossRate.CommandType = CommandType.StoredProcedure;
            UpdateMHLossRate.ExecuteNonQuery();
            con.Close();
        }

        private void UpdateQuarterlyMHLossRate()
        {
            // -> SQL query to update approval status
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (DateTime.Now.ToString("MMMM") == "April" || DateTime.Now.ToString("MMMM") == "May" || DateTime.Now.ToString("MMMM") == "June")
            {
                SqlCommand UpdateMHLossRate = new SqlCommand("SP_Update_Quarterly_MHLossRate", con);
                UpdateMHLossRate.CommandType = CommandType.StoredProcedure;
                UpdateMHLossRate.Parameters.AddWithValue("@Quarter", "Q1");
                UpdateMHLossRate.ExecuteNonQuery();
                con.Close();
            }
            else if (DateTime.Now.ToString("MMMM") == "July" || DateTime.Now.ToString("MMMM") == "August" || DateTime.Now.ToString("MMMM") == "September")
            {
                SqlCommand UpdateMHLossRate = new SqlCommand("SP_Update_Quarterly_MHLossRate", con);
                UpdateMHLossRate.CommandType = CommandType.StoredProcedure;
                UpdateMHLossRate.Parameters.AddWithValue("@Quarter", "Q2");
                UpdateMHLossRate.ExecuteNonQuery();
                con.Close();
            }
            else if (DateTime.Now.ToString("MMMM") == "October" || DateTime.Now.ToString("MMMM") == "November" || DateTime.Now.ToString("MMMM") == "December")
            {
                SqlCommand UpdateMHLossRate = new SqlCommand("SP_Update_Quarterly_MHLossRate", con);
                UpdateMHLossRate.CommandType = CommandType.StoredProcedure;
                UpdateMHLossRate.Parameters.AddWithValue("@Quarter", "Q3");
                UpdateMHLossRate.ExecuteNonQuery();
                con.Close();
            }
            else if (DateTime.Now.ToString("MMMM") == "January" || DateTime.Now.ToString("MMMM") == "February" || DateTime.Now.ToString("MMMM") == "March")
            {
                SqlCommand UpdateMHLossRate = new SqlCommand("SP_Update_Quarterly_MHLossRate", con);
                UpdateMHLossRate.CommandType = CommandType.StoredProcedure;
                UpdateMHLossRate.Parameters.AddWithValue("@Quarter", "Q4");
                UpdateMHLossRate.ExecuteNonQuery();
                con.Close();
            }
          
        }

        //===================================================================================================================>>>>>>>>>>>>>

        private void SelectMHLossLastUpdated()
        {
            // -> SQL query to select User Account
            SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectMHLossLastUpdated = new SqlCommand("SP_SelectMHLossLastUpdated", con);
            SelectMHLossLastUpdated.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(SelectMHLossLastUpdated);
            DataTable dt = new DataTable();
            da.Fill(dt);

            SqlDataReader reader = SelectMHLossLastUpdated.ExecuteReader();

            while (reader.Read())
            {
                MHLossLastUpdateLabel.Text = reader["UploadDate"].ToString();
            }
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void UpdateDataTimer_Tick(object sender, EventArgs e)
        {

        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void ExportButton_Click(object sender, EventArgs e)
        {
            //Export all data from last upload

            //Send email to Applying section SPV
            COPQ_SendEmailToApprover COPQ_SendEmail = new COPQ_SendEmailToApprover(); //class instance
            COPQ_SendEmail.SendEmailToReceivingCOPQPIC("General Affairs");

        }

        private void MHLossUploadDatagrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            rowCount.Text = "Rows:" + MHLossUploadDatagrid.Rows.Count.ToString();
        }

     





        //==================================================================================================================>>>>>>>>>>>>>
    }
}
