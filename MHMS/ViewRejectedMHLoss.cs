using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class ViewRejectedMHLoss : Form
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public ViewRejectedMHLoss()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdownList.Text == "Select Section")
            {
                MessageBox.Show("Please select section.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SelectDuplicateLinestop();
            }
            
        }

        private void SelectDuplicateLinestop()
        {
            con.Open();
            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectRejectedMHLoss", con);
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
                MessageBox.Show("No data has been generated!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ViewRejectedMHLoss_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateFrom.Value = new DateTime(now.Year, now.Month, 1);//Date picker value is set to first day of the month

            DateTo.Value = DateTime.Now; //Date picker value is set to present date

            AddCheckedBoxColumn();
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

        public static string DistinctionCode = string.Empty;
        private void ReApplyBtn_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = (from row in LineStopDatagrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();

            if (selectedRows.Count <= 0)
            {
                MessageBox.Show("Please select the data you want to reapply!", "CMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                foreach (DataGridViewRow row in LineStopDatagrid.Rows)
                {
                    DistinctionCode = row.Cells["DistinctionCode"].Value.ToString();

                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                    {
                        ChangeMHLossStatus();
                    }
                }

            }
        }


        private void ChangeMHLossStatus()
        {
            con.Open();
            SqlCommand UpdateStatusOfRejectedMHLoss = new SqlCommand("SP_UpdateStatusOfRejectedMHLoss", con);
            UpdateStatusOfRejectedMHLoss.CommandType = CommandType.StoredProcedure;
            UpdateStatusOfRejectedMHLoss.Parameters.AddWithValue("@DistinctionCode", DistinctionCode);
            UpdateStatusOfRejectedMHLoss.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("The selected MH Loss reapplied successfully.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (LineStopDatagrid.DataSource == null)
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


    }
}
