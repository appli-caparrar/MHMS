using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using CheckBox = System.Windows.Forms.CheckBox;

namespace MHMS
{
    public partial class UserSetting : Form
    {
        // Connection string
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn); 
        SqlConnection Conn_CentralizedLogin = new SqlConnection(SQLControl.CentralizedLogin);
        public UserSetting()
        {
            InitializeComponent();
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Drag Form ------------------>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void TopPanel_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        // <---------------------------

        //====================================================================================================================>>>>>>>>>>>>

        private void UserSetting_Load(object sender, EventArgs e)
        {
            this.AcceptButton = null;

            FirstName.Focus();

            Section.Text = Forms.ApproverSettingForm.userSection;

            LoadAllUsers(); // ---> Calling function to load all users

            // ---> Disable Sort of MH PIC Data Grid View
            foreach (DataGridViewColumn column in UsersDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (Section.Text == "Equipment Engineering")
            {
                EEDropdownList.Visible = true;
            }
            else {
                EEDropdownList.Visible = false;
            }

            //-------------------------------------------------------//
            if (Dashboard.SectionText.Replace("BIPH-","") == "BPS" && LoginForm.AccountType == "ADMIN")
            {
                AdminBtn.Visible = true;
                UserBtn.Visible = true;
            }
            else
            {
                AdminBtn.Visible = false;
                UserBtn.Visible = false;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        //====================================================================================================================>>>>>>>>>>>>

        private void LoadAllUsers()
        {
            // -> SQL query to select User Account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand LoadUsersPIC = new SqlCommand("SP_LoadUsersPIC", con);
            LoadUsersPIC.CommandType = CommandType.StoredProcedure;
            LoadUsersPIC.Parameters.AddWithValue("@Procedure", "SelectUserAccount");
            LoadUsersPIC.Parameters.AddWithValue("@Section", Section.Text.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(LoadUsersPIC);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UsersDataGrid.DataSource = dt;
            con.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void AddUserToPortalSystemApproverList()
        {
            try
            {
                //Insert user to approver list in I-portal
                Conn_CentralizedLogin.Open();
                SqlCommand InsertUSer = new SqlCommand("SP_InsertUserInSystemApproverList", Conn_CentralizedLogin);
                InsertUSer.CommandType = CommandType.StoredProcedure;
                InsertUSer.Parameters.AddWithValue("@SystemID", "11");
                InsertUSer.Parameters.AddWithValue("@SystemName", "Manhour Management System");
                InsertUSer.Parameters.AddWithValue("@ApproverNumber", "0");
                InsertUSer.Parameters.AddWithValue("@FullName", FirstName.Text + " " + LastName.Text);
                InsertUSer.Parameters.AddWithValue("@EmailAdd", Email.Text);
                InsertUSer.Parameters.AddWithValue("@Section", Section.Text);
                InsertUSer.Parameters.AddWithValue("@Position", Position);
                InsertUSer.Parameters.AddWithValue("@ADID", ADID.Text);
                InsertUSer.Parameters.AddWithValue("@EmployeeNumber", IDNumber);
                InsertUSer.ExecuteNonQuery();
                Conn_CentralizedLogin.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
        //====================================================================================================================>>>>>>>>>>>>

        public static string MHPIC_Value = "";
        public static string COPQPIC_Value = "";
        public static string PCPIC_Value = "";
        public static string Supervisor_Value = "";
        public static string Manager_Value = "";
        public static string GeneralManager_Value = "";
        public static string BILSupport_Value = "";
        public static string COPQProcessInCharge_Value = "";
        public static string FactoryEfficiency_Value = "";

        public static string CurrentPassword = "";

        // Helper method for checkbox values
        private string GetCheckValue(CheckBox checkBox) => checkBox.Checked ? "✔️" : "";

        // Helper method to validate email
        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        // Main method
        private void SaveUser()
        {
            // Trim inputs
            string adid = ADID.Text.Trim();
            string email = Email.Text.Trim();
            string fname = FirstName.Text.Trim();
            string lname = LastName.Text.Trim();
            string section = Section.Text.Trim();
            string assignedSection = AssignedSectionDropdown.Text.Trim();

            // Validate required fields
            if (string.IsNullOrEmpty(adid) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) ||
                (section == "Equipment Engineering" && string.IsNullOrEmpty(assignedSection)))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Validate email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email address.");
                return;
            }

            // Validate at least one role selected
            var roles = new[]
            {
                MHPICCheckBox, COPQPICCheckBox, PCPICCheckBox,
                SupervisorCheckBox, ManagerCheckBox, GeneralMangerCheckBox,
                BILSupportCheckBox, COPQProcessInChargeCheckBox, FactoryEfficiencyCheckBox
            };

            if (!roles.Any(cb => cb.Checked))
            {
                MessageBox.Show("Please select at least one category.");
                return;
            }

            // Prepare values
            MHPIC_Value = GetCheckValue(MHPICCheckBox);
            COPQPIC_Value = GetCheckValue(COPQPICCheckBox);
            PCPIC_Value = GetCheckValue(PCPICCheckBox);
            Supervisor_Value = GetCheckValue(SupervisorCheckBox);
            Manager_Value = GetCheckValue(ManagerCheckBox);
            GeneralManager_Value = GetCheckValue(GeneralMangerCheckBox);
            BILSupport_Value = GetCheckValue(BILSupportCheckBox);
            COPQProcessInCharge_Value = GetCheckValue(COPQProcessInChargeCheckBox);
            FactoryEfficiency_Value = GetCheckValue(FactoryEfficiencyCheckBox);

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                // Check if user exists
                using (var cmd = new SqlCommand("SP_SelectUserAccount", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Procedure", "SelectUserAccount3");
                    cmd.Parameters.AddWithValue("@ADID", adid);
                    cmd.Parameters.AddWithValue("@Section", section);
                    cmd.Parameters.AddWithValue("@Password", "");

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            MessageBox.Show("The user already exists.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                // Insert new user
                using (var cmd = new SqlCommand("SP_InsertUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", fname);
                    cmd.Parameters.AddWithValue("@LastName", lname);
                    cmd.Parameters.AddWithValue("@ADID", adid);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", adid);
                    cmd.Parameters.AddWithValue("@Section", section);
                    cmd.Parameters.AddWithValue("@EESection", section == "Equipment Engineering" ? assignedSection : "");
                    cmd.Parameters.AddWithValue("@AccountType", "USER");
                    cmd.Parameters.AddWithValue("@Status", "ACTIVE");
                    cmd.Parameters.AddWithValue("@MHPIC", MHPIC_Value);
                    cmd.Parameters.AddWithValue("@COPQPIC", COPQPIC_Value);
                    cmd.Parameters.AddWithValue("@PCPIC", PCPIC_Value);
                    cmd.Parameters.AddWithValue("@Supervisor", Supervisor_Value);
                    cmd.Parameters.AddWithValue("@Manager", Manager_Value);
                    cmd.Parameters.AddWithValue("@GeneralManager", GeneralManager_Value);
                    cmd.Parameters.AddWithValue("@BILSupport", BILSupport_Value);
                    cmd.Parameters.AddWithValue("@COPQProcessInCharge", COPQProcessInCharge_Value);
                    cmd.Parameters.AddWithValue("@FEPIC", FactoryEfficiency_Value);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }

                // Update password (reuse stored procedure logic)
                using (var cmd = new SqlCommand("SP_UpdateUserPassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ADID", adid);
                    cmd.Parameters.AddWithValue("@Password", adid);
                    cmd.ExecuteNonQuery();
                }

                // Add to i-Portal Approver List
                AddUserToPortalSystemApproverList();

                MessageBox.Show("User Successfully Added!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields
                ADID.Clear();
                Email.Clear();
                FirstName.Clear();
                LastName.Clear();
                AssignedSectionDropdown.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Unchecked all selected checkboxes
        private void UncheckedSelectedUserType()
        {
            MHPICCheckBox.Checked = false;
            COPQPICCheckBox.Checked = false;
            PCPICCheckBox.Checked = false;
            SupervisorCheckBox.Checked = false;
            ManagerCheckBox.Checked = false;
            GeneralMangerCheckBox.Checked = false;
            BILSupportCheckBox.Checked = false;
            COPQProcessInChargeCheckBox.Checked = false;
            FactoryEfficiencyCheckBox.Checked = false;
        }// <---

        //====================================================================================================================>>>>>>>>>>>>
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            SaveUser();
            UncheckedSelectedUserType();
            LoadAllUsers();
        }

        //====================================================================================================================>>>>>>>>>>>>

        //initialized variable use to store data from datagrid
        string ID = "";
        string First_Name = "";
        string Last_Name = ""; 
        string UserSection = "";

        private void UsersDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = UsersDataGrid.Rows[e.RowIndex].Cells["ADID"].Value.ToString();
            First_Name = UsersDataGrid.Rows[e.RowIndex].Cells["First Name"].Value.ToString();
            Last_Name = UsersDataGrid.Rows[e.RowIndex].Cells["Last Name"].Value.ToString();
            UserSection = UsersDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Delete user function
        private void DeleteUser()
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure do you want to delete user {First_Name} {Last_Name}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                // -> SQL query to delete Section to user setting
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand InsertUSer = new SqlCommand("SP_DeleteUser", con);
                InsertUSer.CommandType = CommandType.StoredProcedure;
                InsertUSer.Parameters.AddWithValue("@ADID", ID);
                InsertUSer.Parameters.AddWithValue("@Section", UserSection);
                InsertUSer.ExecuteNonQuery();
                con.Close(); 

                MessageBox.Show("User Successfuly Deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else { }


        } // --> End of Function

        //====================================================================================================================>>>>>>>>>>>>

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            DeleteUser();
            LoadAllUsers();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void FirstName_TextChanged(object sender, EventArgs e)
        {
            Email.Text = (FirstName.Text).ToLower().Replace(" ", "") + "." + (LastName.Text).ToLower().Replace(" ", "") + "@brother-biph.com.ph";
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LastName_TextChanged(object sender, EventArgs e)
        {
            Email.Text = (FirstName.Text).ToLower().Replace(" ", "") + "." + (LastName.Text).ToLower().Replace(" ", "") + "@brother-biph.com.ph";
        }


        private void copyAlltoClipboardsss()
        {

            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            UsersDataGrid.SelectAll();
            //Copy to clipboard
            UsersDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = UsersDataGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ExportData()
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

            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            xlWorkSheet.Columns.AutoFit();

            MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportUsersButton_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        public static string UserID = string.Empty;
        public static string _Section = string.Empty;

        private void UsersDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the row and column indixes are valid
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    if (UsersDataGrid.Columns[e.ColumnIndex].HeaderText == "Edit Category")
                    {
                        // Get the user ADID from the clicked cell
                        UserID = UsersDataGrid.Rows[e.RowIndex].Cells["ADID"].Value.ToString();
                        _Section = UsersDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();

                        MHPIC_Value = UsersDataGrid.Rows[e.RowIndex].Cells["MH PIC"].Value.ToString();
                        COPQPIC_Value = UsersDataGrid.Rows[e.RowIndex].Cells["COPQ PIC"].Value.ToString();
                        PCPIC_Value = UsersDataGrid.Rows[e.RowIndex].Cells["PC PIC"].Value.ToString();
                        Supervisor_Value = UsersDataGrid.Rows[e.RowIndex].Cells["Supervisor"].Value.ToString();
                        Manager_Value = UsersDataGrid.Rows[e.RowIndex].Cells["Manager"].Value.ToString();
                        GeneralManager_Value = UsersDataGrid.Rows[e.RowIndex].Cells["General Manager"].Value.ToString();
                        BILSupport_Value = UsersDataGrid.Rows[e.RowIndex].Cells["BIL Support"].Value.ToString();
                        COPQProcessInCharge_Value = UsersDataGrid.Rows[e.RowIndex].Cells["COPQ Process In-charge"].Value.ToString();
                        FactoryEfficiency_Value = UsersDataGrid.Rows[e.RowIndex].Cells["FE PIC"].Value.ToString();


                        EditApprovalCategory EditApprovalCategory = new EditApprovalCategory();
                        EditApprovalCategory.ShowDialog();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }


        public static string UserADID = "";
        public static string UserFirstName = "";
        public static string UserLastName = "";
        public static string UserEmail = "";
        public static string IDNumber = "";
        public static string User_Section = "";
        public static string Position = "";

        private void ADID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand SelectUserAccount = new SqlCommand("SP_SelectUserAcountFromEmpData", con);
                SelectUserAccount.CommandType = CommandType.StoredProcedure;
                SelectUserAccount.Parameters.AddWithValue("@ADID", ADID.Text);
                SqlDataAdapter da = new SqlDataAdapter(SelectUserAccount);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count >= 1)
                {
                    SqlDataReader reader = SelectUserAccount.ExecuteReader();

                    if (reader.Read())
                    {
                        UserADID = reader["ADID"].ToString();
                        UserFirstName = reader["First_Name"].ToString();
                        UserLastName = reader["Last_Name"].ToString();
                        UserEmail = reader["Email"].ToString();
                        IDNumber = reader["EmpNo"].ToString();
                        User_Section = reader["SECTION"].ToString();
                        Position = reader["POSITION"].ToString();

                        FirstName.Text = UserFirstName;
                        LastName.Text = UserLastName;
                        Email.Text = UserEmail;

                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show("ADID does not exist!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("ADID does not exist, Add user manually.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void ClearInputsBtn_Click(object sender, EventArgs e)
        {
            ADID.Clear();
            FirstName.Clear();
            LastName.Clear();
            AssignedSectionDropdown.Text = "";
            Email.Clear();
            UncheckedSelectedUserType();

        }

        private void AdminBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update the account type from User to Admin?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand UpdateAccountType = new SqlCommand("SP_UpdateAccountType", con);
                UpdateAccountType.CommandType = CommandType.StoredProcedure;
                UpdateAccountType.Parameters.AddWithValue("@Procedure", "UpdateToAdmin");
                UpdateAccountType.Parameters.AddWithValue("@ADID", ID);
                UpdateAccountType.Parameters.AddWithValue("@Section", UserSection);
                UpdateAccountType.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Account type updated successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAllUsers();
            }
            else { }
            
        }

        private void UserBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update the account type from Admin to User?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand UpdateAccountType = new SqlCommand("SP_UpdateAccountType", con);
                UpdateAccountType.CommandType = CommandType.StoredProcedure;
                UpdateAccountType.Parameters.AddWithValue("@Procedure", "UpdateToUser");
                UpdateAccountType.Parameters.AddWithValue("@ADID", ID);
                UpdateAccountType.Parameters.AddWithValue("@Section", UserSection);
                UpdateAccountType.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Account type updated successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAllUsers();
            }
            else { }
        }

        private void ExportAllBtn_Click(object sender, EventArgs e)
        {
            // -> SQL query to select User Account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand LoadUsersPIC = new SqlCommand("SP_LoadUsersPIC", con);
            LoadUsersPIC.CommandType = CommandType.StoredProcedure;
            LoadUsersPIC.Parameters.AddWithValue("@Procedure", "SelectAllUserAccount");
            LoadUsersPIC.Parameters.AddWithValue("@Section", "");
            SqlDataAdapter sda = new SqlDataAdapter(LoadUsersPIC);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UsersDataGrid.DataSource = dt;
            con.Close();

            ExportData(); //Export data from datagrid

        }

        private void ADID_TextChanged(object sender, EventArgs e)
        {

        }

        public static bool DoneEditing = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DoneEditing == true)
            {
                LoadAllUsers();

                DoneEditing = false;
            }
            else
            {
                DoneEditing = false;
            }
            
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void PerformSearch(string keyword)
        {
            string query = @"
            SELECT * FROM UserAccount 
            WHERE (ADID LIKE @keyword 
                OR [First Name] LIKE @keyword 
                OR [Last Name] LIKE @keyword)
              AND (Section = @section)";

            using (SqlConnection conn = new SqlConnection(SQLControl.MHMS_Conn))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                cmd.Parameters.AddWithValue("@section", Section.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                UsersDataGrid.DataSource = dt;
            }
        }
    
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Stops the Enter key from propagating
                e.Handled = true;
                PerformSearch(SearchTextBox.Text.Trim());
            }
        }

        private void SearchText_Click(object sender, EventArgs e)
        {
            PerformSearch(SearchTextBox.Text.Trim());
        }


        //==================================================================================================================>>>>>>>>>>>>

    }
}
