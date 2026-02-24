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

namespace MHMS.Forms
{
    public partial class OtherApproval : Form
    {
        //Connection String
        static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS2_Conn);


        public OtherApproval()
        {
            InitializeComponent();
        }

        private void OtherApproval_Load(object sender, EventArgs e)
        {
            AddCheckedBoxColumn();
            ApprovalDataGrid.Columns[0].Width = 80;
        }

        private void AddCheckedBoxColumn()
        {
            // Add checkbox column in datagrid
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = "Select";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.FillWeight = 50; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            ApprovalDataGrid.Columns.Add(checkColumn);
            checkColumn.DisplayIndex = 0;
            checkColumn.Frozen = true;
            // <<----------
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            SelectForApprovalApplication();
        }

        private void SelectForApprovalApplication()
        {
            if (ApplicationTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               

                if (ApplicationTypeDropdown.Text == "ST Application")
                {
                    if (StatusDropdown.Text == "For Approval")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                        SelectForApproval.CommandType = CommandType.StoredProcedure;
                        SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                        SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                    else if (StatusDropdown.Text == "Approved")
                    {

                    }
                    else if (StatusDropdown.Text == "Rejected")
                    {

                    }
                }
                else if (ApplicationTypeDropdown.Text == "WC/CC Application")
                {
                    //Type code here...
                }
            }
        }

        private void ApprovalDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in ApprovalDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (e.ColumnIndex == 1 && e.Value != null)
            {
                //e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                e.CellStyle.ForeColor = Color.FromArgb(27, 88, 245);
                //ApprovalDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ApprovalDataGrid.Columns[1].DefaultCellStyle.Font = new Font(ApprovalDataGrid.DefaultCellStyle.Font, FontStyle.Underline);
            }

            ApprovalDataGrid.Columns[0].Frozen = true; //Fixed column
            ApprovalDataGrid.Columns[0].Width = 80;

            ApprovalDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ExportMHData();
        }

        private void copyAlltoClipboardsss()
        {

            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            ApprovalDataGrid.SelectAll();
            //Copy to clipboard
            ApprovalDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = ApprovalDataGrid.GetClipboardContent();
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

            //MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public static string ReferenceNumber;
        public static string ApplicationFormType;
        public static string Category;

        private void ApprovalDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ReferenceNumber = ApprovalDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString();
            ApplicationFormType = ApplicationTypeDropdown.Text;

            if (ApplicationTypeDropdown.Text == "ST Application")
            {
                Category = ApprovalDataGrid.Rows[e.RowIndex].Cells["ST Application Category"].Value.ToString();
            }
            else if (ApplicationTypeDropdown.Text == "WC/CC Application")
            {
                Category = ApprovalDataGrid.Rows[e.RowIndex].Cells["WC/CC Application Category"].Value.ToString();
            }
            else if (ApplicationTypeDropdown.Text == "Open MH System")
            {
                Category = ApprovalDataGrid.Rows[e.RowIndex].Cells["Open MH System Application Category"].Value.ToString();
            }
            else if (ApplicationTypeDropdown.Text == "Manpower  Forecast")
            {
                Category = ApprovalDataGrid.Rows[e.RowIndex].Cells["Manpower  Forecast Application Category"].Value.ToString();
            }


            if (ApprovalDataGrid.CurrentCell.ColumnIndex.Equals(1) && e.RowIndex != -1)
            {
                ViewApplicationForm viewApplication = new ViewApplicationForm();
                viewApplication.ShowDialog();
            }

        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SearchApplicationForm = new SqlCommand("SP_SearchForApprovalApplicationForm", con);
                SearchApplicationForm.CommandType = CommandType.StoredProcedure;
                SearchApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SearchApplicationForm.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text); 
                SearchApplicationForm.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SearchApplicationForm.Parameters.AddWithValue("@Category", Category);
                //SearchApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                SearchApplicationForm.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter da = new SqlDataAdapter(SearchApplicationForm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
