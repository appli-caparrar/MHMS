using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using MHMS.Connection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DataTable = System.Data.DataTable;
using System.IO;

namespace MHMS.Forms
{
    public partial class OverallMonitoring : Form
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public OverallMonitoring()
        {
            InitializeComponent();
        }

        private async void OverallMonitoring_Load(object sender, EventArgs e)
        {
            DateTo();
            DateFrom();

            LoadSection();
            LoadResponsibleSection();
            LoadStatusDropdown();
            LoadPriorityLevels();

            GraphDropdownList.Items.Add("High Category Linestop");
            GraphDropdownList.Items.Add("Sum of Linestop Time Per Month");
            GraphDropdownList.Items.Add("Count of Linestop Per Month");
            GraphDropdownList.Items.Add("Count of Linestop Per Applying Section PIC");
            GraphDropdownList.Items.Add("Count of Linestop Per Responsible Section PIC");
            GraphDropdownList.Items.Add("Sum of Linestop Per Section Category");

            // Ensure WebView2 is properly initialized.
            await webView21.EnsureCoreWebView2Async(null);

            LoadingForm loadingForm = new LoadingForm();
            loadingForm.Show();

            try
            {
                await Task.Delay(1000);

                await RefreshData(SectionDropdown.Text, ResponsibleSectionDropdown.Text, LineStopCategoryDropdown.Text, StatusDropdown.Text, FromDateTimePicker.Value, ToDateTimePicker.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingForm.Close();
            }

        }

        private void DateTo()
        {
            ToDateTimePicker.Value = DateTime.Now;
        }

        private void DateFrom()
        {
            FromDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        private void LoadStatusDropdown()
        {
            string query = "SELECT DISTINCT Status FROM View_OverallMonitoring";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
            DataTable dataTable = new DataTable();

            try
            {
                // Fill the DataTable with the data from the query
                dataAdapter.Fill(dataTable);

                // Add the "All" option as the first row in the DataTable
                DataRow allRow = dataTable.NewRow();
                allRow["Status"] = "All";  // Add a new row with "All" value
                dataTable.Rows.InsertAt(allRow, 0); // Insert it at the beginning

                // Set the ComboBox's DataSource to the data table's Status column
                StatusDropdown.DataSource = dataTable;
                StatusDropdown.DisplayMember = "Status";  // Display the Status values
                StatusDropdown.ValueMember = "Status";    // The value will also be the Status (or use a different column if needed)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPriorityLevels()
        {
            string query = "SELECT DISTINCT [Priority Level] FROM View_OverallMonitoring";


            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
            DataTable dataTable = new DataTable();

            try
            {
                // Fill the DataTable with the data from the query
                dataAdapter.Fill(dataTable);

                // Optionally, add an "All" option at the top of the list
                DataRow allRow = dataTable.NewRow();
                allRow["Priority Level"] = "All";  // Set the value for the "All" option
                dataTable.Rows.InsertAt(allRow, 0); // Insert it at the beginning

                // Set the ComboBox's DataSource to the data table's PriorityLevel column
                LineStopCategoryDropdown.DataSource = dataTable;
                LineStopCategoryDropdown.DisplayMember = "Priority Level";  // Display the PriorityLevel values
                LineStopCategoryDropdown.ValueMember = "Priority Level";    // The value will also be the PriorityLevel

                // Optionally, set the "All" option to be selected by default
                LineStopCategoryDropdown.SelectedIndex = 0; // Default to "All"
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

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

                        // Add a row for the "All" option at the beginning
                        DataRow newRow = ds.Tables[0].NewRow();
                        newRow[0] = "All";  // Assuming the first column is what you want to display in the dropdown
                        ds.Tables[0].Rows.InsertAt(newRow, 0);

                        // Bind data to the dropdown
                        SectionDropdown.DataSource = ds.Tables[0];
                        SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString(); // Set the column to display
                        SectionDropdown.ValueMember = ds.Tables[0].Columns[0].ToString();  // Set the value for the selection

                        // Set the default selection to "All"
                        SectionDropdown.SelectedIndex = 0;
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

        public void LoadResponsibleSection()
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
                    loadSectionCmd.Parameters.AddWithValue("@Procedure", "SelectAllSectionsExceptBPS");

                    using (SqlDataAdapter sda = new SqlDataAdapter(loadSectionCmd))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        // Add a row for the "All" option at the beginning
                        DataRow newRow = ds.Tables[0].NewRow();
                        newRow[0] = "All";  // Assuming the first column is what you want to display in the dropdown
                        ds.Tables[0].Rows.InsertAt(newRow, 0);

                        // Bind data to the dropdown
                        ResponsibleSectionDropdown.DataSource = ds.Tables[0];
                        ResponsibleSectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString(); // Set the column to display
                        ResponsibleSectionDropdown.ValueMember = ds.Tables[0].Columns[0].ToString();  // Set the value for the selection

                        // Set the default selection to "All"
                        SectionDropdown.SelectedIndex = 0;
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

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            UpdateInfo.Visible = true;

            // Get filter values
            string section = SectionDropdown.Text;
            string responsibleSection = ResponsibleSectionDropdown.Text;
            string lineStopCategory = LineStopCategoryDropdown.Text;
            string status = StatusDropdown.Text;

            // Get the date range from DateTimePickers
            DateTime dateFrom = FromDateTimePicker.Value.Date;
            DateTime dateTo = ToDateTimePicker.Value.Date;

            LoadingForm loadingForm = new LoadingForm();
            loadingForm.Show();

            try
            {
                if (DropdownEntriesValue.Text == "All" && section == "All" && responsibleSection == "All" && lineStopCategory == "All" && status == "All")
                {
                    DialogResult result = MessageBox.Show(
                        "Processing all data may take a long time and could cause the application to become unresponsive. To ensure smooth performance, please apply filters before proceeding.",
                        "Warning: Large Data Processing",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Warning
                     );

                    if (result == DialogResult.Yes)
                    {
                        await RefreshData(section, responsibleSection, lineStopCategory, status, dateFrom, dateTo);
                    }
                }
                else
                {
                    await RefreshData(section, responsibleSection, lineStopCategory, status, dateFrom, dateTo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingForm.Close();
                UpdateInfo.Visible = false;
            }
        }

        private async Task RefreshData(string section, string responsibleSection, string lineStopCategory, string status, DateTime dateFrom, DateTime dateTo)
        {
            // Check if all filters are set to "All" or if specific filters need to be applied
            if (section == "All" && responsibleSection == "All" && lineStopCategory == "All" && status == "All")
            {
                // Select all data based on date only
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, null, null, null, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection == "All" && lineStopCategory == "All" && status == "All")
            {
                // Select all data based on section and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, section, null, null, null, dateFrom, dateTo);
            }
            else if (section == "All" && responsibleSection != "All" && lineStopCategory == "All" && status == "All")
            {
                // Select all data based on responsible section and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, null, responsibleSection, null, dateFrom, dateTo);
            }
            else if (section == "All" && responsibleSection == "All" && lineStopCategory != "All" && status == "All")
            {
                // Select all data based on line stop category and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, lineStopCategory, null, null, dateFrom, dateTo);
            }
            else if (section == "All" && responsibleSection == "All" && lineStopCategory == "All" && status != "All")
            {
                // Select all data based on status and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, null, null, status, dateFrom, dateTo);
            }
            else if (section == "All" && responsibleSection == "All" && lineStopCategory != "All" && status != "All")
            {
                // Select all data based on section and responsible section and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, null, lineStopCategory, status, dateFrom, dateTo);
            }
            else if (section == "All" && responsibleSection != "All" && lineStopCategory == "All" && status != "All")
            {
                // Select all data based on section and responsible section and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, null, responsibleSection, status, dateFrom, dateTo);
            }
            else if (section == "All" && responsibleSection != "All" && lineStopCategory != "All" && status == "All")
            {
                // Select all data based on section and responsible section and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, lineStopCategory, responsibleSection, null, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection != "All" && lineStopCategory == "All" && status == "All")
            {
                // Select all data based on section and responsible section and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, section, null, responsibleSection, null, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection == "All" && lineStopCategory != "All" && status == "All")
            {
                // Select all data based on section and line stop category and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, section, lineStopCategory, null, null, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection == "All" && lineStopCategory == "All" && status != "All")
            {
                // Select all data based on section and status and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, section, null, null, status, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection == "All" && lineStopCategory != "All" && status == "All")
            {
                // Select all data based on responsible section and line stop category and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, lineStopCategory, responsibleSection, null, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection == "All" && lineStopCategory == "All" && status != "All")
            {
                // Select all data based on responsible section and status and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, null, responsibleSection, status, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection != "All" && lineStopCategory != "All" && status == "All")
            {
                // Select all data based on section, responsible section, and line stop category and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, section, lineStopCategory, responsibleSection, null, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection == "All" && lineStopCategory != "All" && status != "All")
            {
                // Select all data based on section, responsible section, and line stop category and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, section, lineStopCategory, null, status, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection != "All" && lineStopCategory == "All" && status != "All")
            {
                // Select all data based on section, responsible section, and line stop category and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, section, null, responsibleSection, status, dateFrom, dateTo);
            }
            else if (section == "All" && responsibleSection != "All" && lineStopCategory != "All" && status != "All")
            {
                // Select all data based on section, responsible section, and status and date
                await LoadDataWithFilters(DropdownEntriesValue.Text, null, lineStopCategory, responsibleSection, status, dateFrom, dateTo);
            }
            else if (section != "All" && responsibleSection != "All" && lineStopCategory != "All" && status != "All")
            {
                // Select data based on all the filters
                await LoadDataWithFilters(DropdownEntriesValue.Text, section, lineStopCategory, responsibleSection, status, dateFrom, dateTo);
            }
        }

        private async Task LoadDataWithFilters(string entries, string section, string priorityLevel, string responsibleSection, string status, DateTime dateFrom, DateTime dateTo)
        {
            // SQL query to call the stored procedure
            string query = "SP_GetOverallMonitoring";  // Name of the stored procedure

            // Create a new DataTable to hold the result of the query
            DataTable dataTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;  // Specify that it's a stored procedure

                // Add parameters for stored procedure
                cmd.Parameters.AddWithValue("@Section", section != "All" ? section : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PriorityLevel", priorityLevel != "All" ? priorityLevel : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ResponsibleSection", responsibleSection != "All" ? responsibleSection : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", status != "All" ? status : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DateFrom", dateFrom); // Add the dateFrom parameter
                cmd.Parameters.AddWithValue("@DateTo", dateTo); // Add the dateTo parameter


                if (entries == "All")
                {
                    cmd.Parameters.AddWithValue("@Procedure", "SelectAll");
                    cmd.Parameters.AddWithValue("@Entries", DBNull.Value);  // Set Entries as null if "All"
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Procedure", "SelectByEntries");
                    cmd.Parameters.AddWithValue("@Entries", entries);  // Set Entries to the selected value
                }

                try
                {
                    // Open connection and execute the query
                    await con.OpenAsync();

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        // Fill DataTable asynchronously
                        await Task.Run(() => dataAdapter.Fill(dataTable));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();  // Ensure the connection is closed even if there is an error
                }
            }

            // Bind the DataTable to a DataGridView
            MHLossDataGridView.DataSource = dataTable;
        }


        private void ViewGraphBtn_Click(object sender, EventArgs e)
        {
            string selectedItem = GraphDropdownList.Text;

            if (selectedItem == "High Category Linestop")
            {
                // Perform action for "High Category Linestop"
                webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/PC-OverallMonitoringGraph/HighCategoryLinestopOn-TimeRatio");
            }
            else if (selectedItem == "Sum of Linestop Time Per Month")
            {
                // Perform action for "Sum of Linestop Time Per Month"
                webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/PC-OverallMonitoringGraph/SumofLinestopPerMonth");
            }
            else if (selectedItem == "Count of Linestop Per Month")
            {
                // Perform action for "Count of Linestop Per Month"
                webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/PC-OverallMonitoringGraph/CountofLinestopPerMonth");
            }
            else if (selectedItem == "Count of Linestop Per Applying Section PIC")
            {
                // Perform action for "Count of Linestop Per Applying Section PIC"
                webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/PC-OverallMonitoringGraph/CountofLinestopPerApplyingSectionPIC");
            }
            else if (selectedItem == "Count of Linestop Per Responsible Section PIC")
            {
                // Perform action for "Count of Linestop Per Responsible Section PIC"
                webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/PC-OverallMonitoringGraph/CountofLinestopPerReceivingSectionPIC");
            }
            else if (selectedItem == "Sum of Linestop Per Section Category")
            {
                // Perform action for "Sum of Linestop Per Section Category"
                webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/PC-OverallMonitoringGraph/SumofLinestopPerSectionCategory");
            }
        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Search keyword: section, responsible section, linestop detail, status, linestop category
            if (e.KeyChar == (char)Keys.Enter) // Check if Enter is pressed
            {
                e.Handled = true; // Prevent the "ding" sound on Enter key press

                string searchQuery = SearchBox.Text.Trim();

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    // Call the search function asynchronously
                    await PerformSearch(searchQuery);
                }
            }
        }

        private async Task PerformSearch(string searchQuery)
        {
            UpdateInfo.Visible = true;

            LoadingForm loadingForm = new LoadingForm();
            loadingForm.Show();

            try
            {
                await Task.Delay(1000); // Small delay to improve UI responsiveness

                // Define the query parameters
                string query = "SP_SearchOverallMonitoring";  // Name of the stored procedure
                DataTable dataTable = new DataTable();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;  // Specify stored procedure

                    // Add parameters for filtering
                    cmd.Parameters.AddWithValue("@SearchTerm", SearchBox.Text);

                    // Open the connection asynchronously
                    await con.OpenAsync();

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        await Task.Run(() => dataAdapter.Fill(dataTable));  // Run in background to prevent UI freeze
                    }
                }

                // Bind the DataTable to DataGridView
                MHLossDataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
                loadingForm.Close(); // Close the loading animation

                UpdateInfo.Visible = false;
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (MHLossDataGridView.DataSource == null)
            {
                MessageBox.Show("No data found! Please generate data first.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //ExportMHData();
                ExportToExcel_OpenXML();
            }
        }


        //private void copyAlltoClipboardsss()
        //{

        //    MHLossDataGridView.SelectAll();
        //    //Copy to clipboard
        //    MHLossDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
        //    DataObject dataObj = MHLossDataGridView.GetClipboardContent();
        //    if (dataObj != null)
        //        Clipboard.SetDataObject(dataObj);
        //}

        //private void ExportMHData()
        //{
        //    string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";

        //    System.IO.Directory.CreateDirectory(pathsss);

        //    copyAlltoClipboardsss();
        //    Microsoft.Office.Interop.Excel.Application xlexcel;
        //    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        //    object misValue = System.Reflection.Missing.Value;
        //    xlexcel = new Microsoft.Office.Interop.Excel.Application();
        //    xlexcel.Visible = true;
        //    xlWorkBook = xlexcel.Workbooks.Add(misValue);
        //    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

        //    Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
        //    // xlWorkSheet.Cells[3, "XL"].Cells.NumberFormat = "@";
        //    CR.Select();
        //    xlWorkSheet.Cells.NumberFormat = "@";

        //    xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
        //    xlWorkSheet.Columns.AutoFit();

        //}

        private void ExportToExcel_OpenXML()
        {
            try
            {
                if (MHLossDataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export!", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string baseFileName = "COPQ_Exported_Data_OpenXML.xlsx";
                string filePath = Path.Combine(downloadPath, baseFileName);

                // Check if file exists and add a count
                int count = 1;
                while (File.Exists(filePath))
                {
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(baseFileName);
                    string fileExt = Path.GetExtension(baseFileName);
                    filePath = Path.Combine(downloadPath, $"{fileNameWithoutExt} ({count}){fileExt}");
                    count++;
                }

                using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet(new SheetData());

                    Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
                    Sheet sheet = new Sheet()
                    {
                        Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "ExportedData"
                    };
                    sheets.Append(sheet);

                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    // Column Headers
                    Row headerRow = new Row();
                    foreach (DataGridViewColumn column in MHLossDataGridView.Columns)
                    {
                        Cell cell = new Cell() { DataType = CellValues.String, CellValue = new CellValue(column.HeaderText) };
                        headerRow.Append(cell);
                    }
                    sheetData.AppendChild(headerRow);

                    // Row Data
                    foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                    {
                        Row newRow = new Row();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            Cell newCell = new Cell()
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(cell.Value?.ToString() ?? "")
                            };
                            newRow.Append(newCell);
                        }
                        sheetData.AppendChild(newRow);
                    }
                }

                MessageBox.Show($"Data exported successfully to: {filePath}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddLeadtimeBtn_Click(object sender, EventArgs e)
        {
            AddLeadTime AddLeadTime = new AddLeadTime();
            AddLeadTime.ShowDialog();
        }
    }
}
