using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using MHMS.Connection;

namespace MHMS
{
    public partial class UploadTemplateForm : Form
    {
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;


        public UploadTemplateForm()
        {
            InitializeComponent();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void UploadTemplateForm_Load(object sender, EventArgs e)
        {
            
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            //string filePath = string.Empty;
            //string fileExt = string.Empty;

            //OpenFileDialog file = new OpenFileDialog();//open dialog to choose file
            //if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)//if there is a file choosen by the user
            //{
            //    filePath = file.FileName;//get the path of the file
            //    fileExt = Path.GetExtension(filePath);//get the file extension
            //    FilePath.Text = filePath + fileExt;
            //    if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0 || fileExt.CompareTo(".xlsm") == 0)
            //    {
            //        try
            //        {
            //            DataTable dtExcel = new DataTable();
            //            dtExcel = ReadExcel(filePath, fileExt);//read excel file
            //            UploadedUserDataGrid.Visible = true;
            //            UploadedUserDataGrid.DataSource = dtExcel;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message.ToString());
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);//custom messageBox to show error
            //    }
            //}
        }

        //====================================================================================================================>>>>>>>>>>>>

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0) //compare the extension of the file
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';"; //for above excel 2007
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con);//here we read data from sheet1
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return dtexcel;
        }// end ReadExcel

        //====================================================================================================================>>>>>>>>>>>>

        private void UploadUserButton_Click(object sender, EventArgs e)
        {
            if (FilePath.Text == "")
            {
                MessageBox.Show("File not found! Please select file to upload!");
            }
            else
            {
                try
                {
                    //int intRow = 0;

                    foreach (DataGridViewRow row in UploadedUserDataGrid.Rows)
                    {
                        // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                        if (!row.IsNewRow)
                        {
                            //intRow = intRow + 1;

                            // -> SQL query to insert Section to Approver setting
                            SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand InsertUsers = new SqlCommand("SP_InsertUser", con);
                            InsertUsers.CommandType = CommandType.StoredProcedure;
                            InsertUsers.Parameters.AddWithValue("@FirstName", row.Cells["First Name"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@LastName", row.Cells["Last Name"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@ADID", row.Cells["ADID"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@Email", row.Cells["Email"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@Password", row.Cells["Password"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@Section", row.Cells["Section"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@AccountType", row.Cells["Account Type"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@Status", "ACTIVE");
                            InsertUsers.Parameters.AddWithValue("@MHPIC", row.Cells["MH PIC"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@COPQPIC", row.Cells["COPQ PIC"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@PCPIC", row.Cells["PC PIC"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@Supervisor", row.Cells["Supervisor"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@Manager", row.Cells["Manager"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@GeneralManager", row.Cells["General Manager"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@BILSupport", row.Cells["BIL Support"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@COPQProcessInCharge", row.Cells["COPQ Process In-Charge"].Value.ToString());
                            InsertUsers.Parameters.AddWithValue("@DateCreated", DateTime.Now.ToString());
                            InsertUsers.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    MessageBox.Show("Users Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FilePath.Clear();
                    UploadedUserDataGrid.DataSource = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;

            OpenFileDialog file = new OpenFileDialog();//open dialog to choose file
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)//if there is a file choosen by the user
            {
                filePath = file.FileName;//get the path of the file
                fileExt = Path.GetExtension(filePath);//get the file extension
                FilePath.Text = filePath + fileExt;
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0 || fileExt.CompareTo(".xlsm") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        UploadedUserDataGrid.Visible = true;
                        UploadedUserDataGrid.DataSource = dtExcel;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);//custom messageBox to show error
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>
    }
}
