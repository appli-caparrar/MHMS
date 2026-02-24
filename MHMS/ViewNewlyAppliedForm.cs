using MHMS.Connection;
using MHMS.Forms;
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
    public partial class ViewNewlyAppliedForm : Form
    {
        //Connection String
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public ViewNewlyAppliedForm()
        {
            InitializeComponent();
        }

        private void ViewNewlyAppliedForm_Load(object sender, EventArgs e)
        {
            ApplicationTypeLabel.Text = "Newly Applied " + ApplicationForm.ApplicationFormType + "-" + ApplicationForm.Category + " Application";
            AppCategoryLabel.Text = ApplicationForm.Category;

            SelectNewlyAppliedMHApplication();
        }


        string ApplicationFormNo;
        string ReferenceNo;
        private void SelectNewlyAppliedMHApplication()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //select no.
            SqlCommand SelectApplicationFormNo = new SqlCommand("SP_SelectApplicationFormNo", con);
            SelectApplicationFormNo.CommandType = CommandType.StoredProcedure;
            SelectApplicationFormNo.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
            SelectApplicationFormNo.Parameters.AddWithValue("@Category", ApplicationForm.Category);
            SelectApplicationFormNo.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormNo);
            DataTable dTable = new DataTable();
            da.Fill(dTable);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                con.Open();
                SqlDataReader reader = SelectApplicationFormNo.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationFormNo = reader["ApplicationFormNo"].ToString(); //Application no column
                    ReferenceNo = reader["ReferenceNo"].ToString(); //ReferenceNo column

                    reader.Close();
                }

                SqlCommand SelectApplicationFormByCategory = new SqlCommand("SP_SelectNewlyAppliedMHApplication", con);
                SelectApplicationFormByCategory.CommandType = CommandType.StoredProcedure;
                SelectApplicationFormByCategory.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplicationFormByCategory.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                SelectApplicationFormByCategory.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                SelectApplicationFormByCategory.Parameters.AddWithValue("@ApplicationNo", ApplicationFormNo);
                SqlDataAdapter sda = new SqlDataAdapter(SelectApplicationFormByCategory);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ViewNewlyAppliedDataGrid.DataSource = dt;
                con.Close();

                ViewNewlyAppliedDataGrid.Columns["ApplicationFormNo"].Visible = false;
                ViewNewlyAppliedDataGrid.Columns["ReferenceNo"].Visible = false;
            }

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
            ViewNewlyAppliedDataGrid.SelectAll();
            //Copy to clipboard
            ViewNewlyAppliedDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = ViewNewlyAppliedDataGrid.GetClipboardContent();
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

        private void CancelApplicationBtn_Click(object sender, EventArgs e)
        {
            //Delete application here...
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?", "MHMS Infornation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //Deletion of Application per category
                SqlCommand DeleteApplicationForm = new SqlCommand("SP_DeleteApplicationFormPerCategory", con);
                DeleteApplicationForm.CommandType = CommandType.StoredProcedure;
                DeleteApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                DeleteApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                DeleteApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNo);
                DeleteApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                DeleteApplicationForm.ExecuteNonQuery();
                con.Close();


                //Deletion of Application in approval table
                con.Open();
                SqlCommand DeleteForApprovalApplicationForm = new SqlCommand("SP_DeleteForApprovalApplicationForm", con);
                DeleteForApprovalApplicationForm.CommandType = CommandType.StoredProcedure;
                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@ReferneceNo", ReferenceNo);
                DeleteForApprovalApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                DeleteForApprovalApplicationForm.ExecuteNonQuery();
                con.Close();

                ViewNewlyAppliedDataGrid.DataSource = null;

                //SelectForApprovalPerApplicationForm();

                MessageBox.Show("Application was cancelled successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Dashboard.ApplicationIsSubmitted = true; //Refresh application form
            }
        }
    }
}
