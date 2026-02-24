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
    public partial class MHReportLogsForm : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public MHReportLogsForm()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please select section!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SectionDropdown.Focus();
            }
            else
            {
                //SelectedSection = SectionDropdown.Text;

                FilterDataBySelectedRangeOfDate();

                SearchBox.Clear();
            }
        }

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
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllProdSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            //SectionDropdown.ValueMember = "";
        }

       

        public void FilterDataBySelectedRangeOfDate()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
           
            if (ExcludeCheckBox.Checked == true)
            {
                SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "ShowLogsBySectionExcludeEE");
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", "Applying");
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", "");
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                MHReportLogsDatagrid.DataSource = dt;
                con.Close();
            }
            else
            {
                SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "ShowLogsBySection");
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", "Applying");
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", "");
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                MHReportLogsDatagrid.DataSource = dt;
                con.Close();
            }
           
          
            
        }

        private void SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection();
        }

        private void copyAlltoClipboardsss()
        {

            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            MHReportLogsDatagrid.SelectAll();
            //Copy to clipboard
            MHReportLogsDatagrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = MHReportLogsDatagrid.GetClipboardContent();
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
            //string DateNowVal = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            //string folderPath = "C:\\Users\\manalojo\\Desktop\\Export\\";
            //    xlWorkBook.SaveAs(folderPath + "ViewExport_ " + DateNowVal + ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
            //false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ExportMHData();
        }

        private void DateFrom()
        {
            DateTime now = DateTime.Now;
            FromDateTimePicker.Value = new DateTime(now.Year, now.Month, 1);
        }

        private void DateTo()
        {
            DateTime datenow = DateTime.Now;
            ToDateTimePicker.Value = datenow;
        }

        private void MHReportLogsForm_Load(object sender, EventArgs e)
        {
            DateFrom(); //Set date for datefrom datetime picker

            DateTo(); //Set date for dateTo datetime picker
        }

        private void ExcludeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FilterDataBySelectedRangeOfDate();
        }


        private void SearchMHLossData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SearchMHLossData = new SqlCommand("SP_SearchMHLossLogsData", con);
            SearchMHLossData.CommandType = CommandType.StoredProcedure;
            SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
            SearchMHLossData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MHReportLogsDatagrid.DataSource = dt;
            con.Close();

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            /* FormatHeaderText();*/ // Format header text
                                     //SearchBox.Clear(); // Clear text box

        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchMHLossData();
            }
        }

        private void MHReportLogsDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in MHReportLogsDatagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            MHReportLogsDatagrid.Columns["Applying COPQ PIC"].HeaderCell.Style.BackColor = Color.FromArgb(239, 196, 140);
            MHReportLogsDatagrid.Columns["Applying SPV"].HeaderCell.Style.BackColor = Color.FromArgb(239, 196, 140);
            MHReportLogsDatagrid.Columns["Applying MGR"].HeaderCell.Style.BackColor = Color.FromArgb(239, 196, 140);
            MHReportLogsDatagrid.Columns["Receiving COPQ PIC"].HeaderCell.Style.BackColor = Color.FromArgb(239, 196, 140);
            MHReportLogsDatagrid.Columns["COPQ Process In-Charge"].HeaderCell.Style.BackColor = Color.FromArgb(239, 196, 140);
            MHReportLogsDatagrid.Columns["Receiving SPV"].HeaderCell.Style.BackColor = Color.FromArgb(239, 196, 140);
            MHReportLogsDatagrid.Columns["Receiving MGR"].HeaderCell.Style.BackColor = Color.FromArgb(239, 196, 140);
            MHReportLogsDatagrid.EnableHeadersVisualStyles = false;
        }

        //=======================================================<END>=============================================//
    }
}
