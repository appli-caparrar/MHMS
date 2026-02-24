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
    public partial class CheckLineStopDataForm : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public CheckLineStopDataForm()
        {
            InitializeComponent();
        }

        private void CheckLineStopDataForm_Load(object sender, EventArgs e)
        {
            //AddCheckedBoxColumn();

            DateTime now = DateTime.Now;
            DateFrom.Value = new DateTime(now.Year, now.Month, 1);//Date picker value is set to first day of the month

            DateTo.Value = DateTime.Now; //Date picker value is set to present date

            //MessageBox.Show(DateTime.Now.ToString());
       
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
            LineStopDatagrid.Columns.Add(checkColumn);
            checkColumn.DisplayIndex = 0;
            checkColumn.Frozen = true;
            // <<----------
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdownList.Text == "Select Section")
            {
                MessageBox.Show("Please select section.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SelectDuplicateLinestop();
            }

            
        }


        private void SelectDuplicateLinestop()
        {
            con.Open();
            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectLineStopDetail", con);
            SelectApprovalData.CommandType = CommandType.StoredProcedure;
            SelectApprovalData.Parameters.AddWithValue("@Section", SectionDropdownList.Text);
            SelectApprovalData.Parameters.AddWithValue("@DateFrom", DateFrom.Value.ToString());
            SelectApprovalData.Parameters.AddWithValue("@DateTo", DateTo.Value.ToString());
            //SelectApprovalData.Parameters.AddWithValue("@LineStopDetail", LineStopTextBox.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            LineStopDatagrid.DataSource = dataTable;
            con.Close();

            //LineStopDatagrid.Columns["ID"].Visible = false;

            if (dataTable.Rows.Count < 1)
            {
                MessageBox.Show("No duplicates data has been generated!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

    

        private void DeleteLineStopBtn_Click(object sender, EventArgs e)
        {

            DeleteDuplicate();



            //List<DataGridViewRow> selectedRows = (from row in LineStopDatagrid.Rows.Cast<DataGridViewRow>()
            //                                      where Convert.ToBoolean(row.Cells["Select"].Value) == true
            //                                      select row).ToList();

            //if (selectedRows.Count < 1 || selectedRows.Count == 0)
            //{
            //    MessageBox.Show("Please select data you want to delete!", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else
            //{
            //    if (MessageBox.Show(string.Format("Do you want to delete {0} item?", selectedRows.Count), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        foreach (DataGridViewRow row in selectedRows)
            //        {

            //            //Cancel linestop data in COPQApproval table
            //            //con.Open();
            //            //SqlCommand CancelLineStopData = new SqlCommand("SP_UpdateLineStopStatusInCOPQApprovalData", con);
            //            //CancelLineStopData.CommandType = CommandType.StoredProcedure;
            //            //CancelLineStopData.Parameters.AddWithValue("@ID", row.Cells["ID"].Value.ToString());
            //            //CancelLineStopData.Parameters.AddWithValue("@CancelledBy", LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString());
            //            //CancelLineStopData.ExecuteNonQuery();
            //            //con.Close();


            //            con.Open();
            //            SqlCommand DeleteLinestop = new SqlCommand("SP_DeleteDuplicateLinestop", con);
            //            DeleteLinestop.CommandType = CommandType.StoredProcedure;
            //            DeleteLinestop.Parameters.AddWithValue("@Section", SectionDropdownList.Text);
            //            DeleteLinestop.Parameters.AddWithValue("@DateFrom", DateFrom.Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@DateTo", DateTo.Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@DateEncountered", row.Cells["Date Encountered"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@CostCenter", row.Cells["Cost Center"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@WorkCenter", row.Cells["Work Center"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@PartCode", row.Cells["Part Code"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@LineStopDetail", row.Cells["LineStop Detail"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@StopTime", row.Cells["Stop Time"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@DirectMP", row.Cells["Direct MP"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@SemiDirectMP", row.Cells["SemiDirect MP"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@LossManhour", row.Cells["Loss Manhour"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@Reason", row.Cells["Reason"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@TypeOfLoss", row.Cells["Type of Loss"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@ApplyingApprovalStatus", row.Cells["Applying Approval Status"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@ReceivingApprovalStatus", row.Cells["Receiving Approval Status"].Value.ToString());
            //            DeleteLinestop.Parameters.AddWithValue("@OverAllStatus", row.Cells["Over All Status"].Value.ToString());
            //            DeleteLinestop.ExecuteNonQuery();
            //            con.Close();

            //        }

                    MessageBox.Show("Deleted Successfully!", "DONE");

                    SelectDuplicateLinestop();

                    SectionDropdown.Text = "";
                    LineStopTextBox.Clear();
                //}
            //}
        }

        private void DeleteDuplicate()
        {

            con.Open();
            SqlCommand DeleteLinestop = new SqlCommand("SP_DeleteDuplicateLinestop", con);
            DeleteLinestop.CommandType = CommandType.StoredProcedure;
            DeleteLinestop.Parameters.AddWithValue("@Section", SectionDropdownList.Text);
            DeleteLinestop.Parameters.AddWithValue("@DateFrom", DateFrom.Value.ToString());
            DeleteLinestop.Parameters.AddWithValue("@DateTo", DateTo.Value.ToString());
            DeleteLinestop.ExecuteNonQuery();
            con.Close();
        }

        private void ExportBtn_Click_1(object sender, EventArgs e)
        {
            ExportMHData();
        }

        private void copyAlltoClipboardsss()
        {
            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            LineStopDatagrid.SelectAll();
            //Copy to clipboard
            LineStopDatagrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = LineStopDatagrid.GetClipboardContent();
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


        private void LineStopTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (LineStopTextBox.Text == "Type or Paste Linestop")
            {
                LineStopTextBox.Text = "";
            }
            else
            {
                LineStopTextBox.Text = LineStopTextBox.Text;
            }
            
        }

        private void LineStopTextBox_MouseLeave(object sender, EventArgs e)
        {
            if (LineStopTextBox.Text == "")
            {
                LineStopTextBox.Text = "Type or Paste Linestop"; 
            }
            else
            {
                LineStopTextBox.Text = LineStopTextBox.Text;
            }
             
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LineStopDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //CheckForDuplicates();
        }

        //private void CheckForDuplicates()
        //{
        //    var valueSet = new HashSet<string>();

        //    foreach (DataGridViewRow row in LineStopDatagrid.Rows)
        //    {
        //        if (row.IsNewRow) continue; // Skip the new row placeholder

        //        string DateEncountered = row.Cells["DateEncountered"].Value.ToString();
        //        string Section = row.Cells["Section"].Value.ToString();
        //        string CostCenter = row.Cells["CostCenter"].Value.ToString();
        //        string WorkCenter = row.Cells["WorkCenter"].Value.ToString();
        //        string LineStopDetail = row.Cells["LineStopDetail"].Value.ToString();
        //        string MHLossType = row.Cells["MHLossType"].Value.ToString();
        //        string StopTime = row.Cells["StopTime"].Value.ToString();
        //        string DirectMP = row.Cells["DirectMP"].Value.ToString();
        //        string SemiDirectMP = row.Cells["SemiDirectMP"].Value.ToString();
        //        string LossManhour = row.Cells["LossManhour"].Value.ToString();

        //        string uniqueKey = $"{DateEncountered}|{Section}|{CostCenter}|{WorkCenter}|{LineStopDetail}|{MHLossType}|{StopTime}|{DirectMP}|{SemiDirectMP}|{LossManhour}"; // Combine columns to create a unique key

        //        if (valueSet.Contains(uniqueKey))
        //        {
        //            row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow; // Highlight duplicate
        //        }
        //        //else
        //        //{
        //        //    valueSet.Add(uniqueKey);
        //        //}
        //    }
        //}

        //==================================================================================================================>>>>>>>>>>>>
    }
}
