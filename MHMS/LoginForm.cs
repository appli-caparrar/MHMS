using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MHMS.Connection;

namespace MHMS
{
    public partial class LoginForm : Form
    {
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
       
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
      

        public LoginForm()
        {
            InitializeComponent();
        }

        // Variable use for handling data from database
        public static string UserADID = "";
        public static string UserPassword = "";
        public static string UserSection = "";
        public static string EESection = "";
        public static string FirstName = "";
        public static string LastName = "";
        public static string EmailAddress = "";
        public static string SectionName = "";
        public static string Department = "";
        public static string AccountType = "";
        public static string COPQPIC = "";
        public static string SectionSPV = "";
        public static string SectionMGR = "";
        public static string ProcessInCharge = "";
        public static string SectionGeneralMGR = "";
        public static string BILSupport = "";
        public static string MHPIC = "";
        public static string PCPIC = "";
        public static string PEMGR = "";
        public static string FEPIC = "";
        public static string LoggedIn = "";

        //public static string QIConfirmation = "";

        //====================================================================================================================>>>>>>>>>>>>
        public static bool isSingleSectionAccess;

        private void LoginUser_2()
        {
            ////-> textbox validation
            //if (ADID.Text == "" && Password.Text == "")
            //{
            //    MessageBox.Show("Please type your ADID and Password", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else if (ADID.Text == "")
            //{
            //    MessageBox.Show("Please type your ADID", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else if (Password.Text == "")
            //{
            //    MessageBox.Show("Please type your Password", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
                RecentLoggedIn();

                SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select User Account
                SqlCommand SelectUserAccount = new SqlCommand("SP_SelectUserAccount", con);
                SelectUserAccount.CommandType = CommandType.StoredProcedure;
                SelectUserAccount.Parameters.AddWithValue("@Procedure", "SelectUserAcount_New");
                SelectUserAccount.Parameters.AddWithValue("@ADID", UserADID);
                SelectUserAccount.Parameters.AddWithValue("@Password", "");
                SelectUserAccount.Parameters.AddWithValue("@Section", "");
                SqlDataAdapter da = new SqlDataAdapter(SelectUserAccount);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count == 1)
                {
                    RecentLoggedIn();

                    SqlDataReader reader = SelectUserAccount.ExecuteReader();

                    if (reader.Read())
                    {
                        UserADID = reader["ADID"].ToString();
                        UserSection = reader["Section"].ToString();
                        EESection = reader["EE_AssignedSection"].ToString();
                        FirstName = reader["First Name"].ToString();
                        LastName = reader["Last Name"].ToString();
                        EmailAddress = reader["Email"].ToString();
                        SectionName = "BIPH-" + reader["Section"].ToString();
                        Department = reader["Department"].ToString();
                        AccountType = reader["Account Type"].ToString();

                        COPQPIC = reader["COPQ PIC"].ToString();
                        ProcessInCharge = reader["COPQ Process In-charge"].ToString();
                        SectionSPV = reader["Supervisor"].ToString();
                        SectionMGR = reader["Manager"].ToString();
                        SectionGeneralMGR = reader["General Manager"].ToString();
                        BILSupport = reader["BIL Support"].ToString();
                        MHPIC = reader["MH PIC"].ToString();
                        PCPIC = reader["PC PIC"].ToString();
                        FEPIC = reader["FE PIC"].ToString();
                        LoggedIn = reader["RecentLoggedIn"].ToString();

                        isSingleSectionAccess = true;

                        RecentLoggedIn();

                        // -> this code is use to Redirect  from this form to main form
                        this.Hide();
                        Dashboard formDasboard = new Dashboard();
                        formDasboard.ShowDialog();
                        this.Close();

                    }
                    else
                    {
                        //this.Show();
                        //MessageBox.Show("Incorrect ADID or password", "Access Denied", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                else if (dt.Rows.Count > 1)
                {
                    RecentLoggedIn();

                    SqlDataReader reader2 = SelectUserAccount.ExecuteReader();

                    if (reader2.Read())
                    {
                        UserADID = reader2["ADID"].ToString();
                        UserPassword = Password.Text;
                        UserSection = reader2["Section"].ToString();
                        EESection = reader2["EE_AssignedSection"].ToString();
                        FirstName = reader2["First Name"].ToString();
                        LastName = reader2["Last Name"].ToString();
                        EmailAddress = reader2["Email"].ToString();
                        SectionName = "BIPH-" + reader2["Section"].ToString();
                        Department = reader2["Department"].ToString();
                        AccountType = reader2["Account Type"].ToString();

                        COPQPIC = reader2["COPQ PIC"].ToString();
                        ProcessInCharge = reader2["COPQ Process In-charge"].ToString();
                        SectionSPV = reader2["Supervisor"].ToString();
                        SectionMGR = reader2["Manager"].ToString();
                        SectionGeneralMGR = reader2["General Manager"].ToString();
                        BILSupport = reader2["BIL Support"].ToString();
                        MHPIC = reader2["MH PIC"].ToString();
                        PCPIC = reader2["PC PIC"].ToString();
                        FEPIC = reader2["FE PIC"].ToString();
                        LoggedIn = reader2["RecentLoggedIn"].ToString();

                        this.Hide();
                        SectionMenuForm sectionMenuForm = new SectionMenuForm();
                        sectionMenuForm.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    //this Message show when the user account is not exist in database
                    this.Hide();
                    LoginRemindersForm loginRemindersForm = new LoginRemindersForm();
                    loginRemindersForm.ShowDialog();
                    this.Close();

                //MessageBox.Show("No login request found in I-Portal, But you can direct login to MHMS temporarily by clicking  \"OK\".", "MHMS Information!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            //}
        }  //***** End of login fuction *****
            
        private void LoginUser()
        {
            //-> textbox validation
            if (ADID.Text == "" && Password.Text == "")
            {
                MessageBox.Show("Please type your ADID and Password", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ADID.Text == "")
            {
                MessageBox.Show("Please type your ADID", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Password.Text == "")
            {
                MessageBox.Show("Please type your Password", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                RecentLoggedIn();

                SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select User Account
                SqlCommand SelectUserAccount = new SqlCommand("SP_SelectUserAccount", con);
                SelectUserAccount.CommandType = CommandType.StoredProcedure;
                SelectUserAccount.Parameters.AddWithValue("@Procedure", "SelectUserAcount");
                SelectUserAccount.Parameters.AddWithValue("@ADID", ADID.Text);
                SelectUserAccount.Parameters.AddWithValue("@Password", Password.Text);
                SelectUserAccount.Parameters.AddWithValue("@Section", "");
                SqlDataAdapter da = new SqlDataAdapter(SelectUserAccount);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count == 1)
                {
                    RecentLoggedIn();

                    SqlDataReader reader = SelectUserAccount.ExecuteReader();

                    if (reader.Read())
                    {
                        UserADID = reader["ADID"].ToString();
                        UserSection = reader["Section"].ToString();
                        EESection = reader["EE_AssignedSection"].ToString();
                        FirstName = reader["First Name"].ToString();
                        LastName = reader["Last Name"].ToString();
                        EmailAddress = reader["Email"].ToString();
                        SectionName = "BIPH-" + reader["Section"].ToString();
                        Department = reader["Department"].ToString();
                        AccountType = reader["Account Type"].ToString();

                        COPQPIC = reader["COPQ PIC"].ToString();
                        ProcessInCharge = reader["COPQ Process In-charge"].ToString();
                        SectionSPV = reader["Supervisor"].ToString();
                        SectionMGR = reader["Manager"].ToString();
                        SectionGeneralMGR = reader["General Manager"].ToString();
                        BILSupport = reader["BIL Support"].ToString();
                        MHPIC = reader["MH PIC"].ToString();
                        PCPIC = reader["PC PIC"].ToString();
                        FEPIC = reader["FE PIC"].ToString();
                        LoggedIn = reader["RecentLoggedIn"].ToString();

                        isSingleSectionAccess = true;

                        RecentLoggedIn();

                        // -> this code is use to Redirect  from this form to main form
                        this.Hide();
                        Dashboard formDasboard = new Dashboard();
                        formDasboard.ShowDialog();
                        this.Close();


                    }
                    else
                    {
                         MessageBox.Show("Incorrect ADID or password", "Access Denied", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                else if (dt.Rows.Count > 1)
                {
                    RecentLoggedIn();

                    SqlDataReader reader2 = SelectUserAccount.ExecuteReader();

                    if (reader2.Read())
                    {
                        UserADID = reader2["ADID"].ToString();
                        UserPassword = Password.Text;
                        UserSection = reader2["Section"].ToString();
                        EESection = reader2["EE_AssignedSection"].ToString();
                        FirstName = reader2["First Name"].ToString();
                        LastName = reader2["Last Name"].ToString();
                        EmailAddress = reader2["Email"].ToString();
                        SectionName = "BIPH-" + reader2["Section"].ToString();
                        Department = reader2["Department"].ToString();
                        AccountType = reader2["Account Type"].ToString();

                        COPQPIC = reader2["COPQ PIC"].ToString();
                        ProcessInCharge = reader2["COPQ Process In-charge"].ToString();
                        SectionSPV = reader2["Supervisor"].ToString();
                        SectionMGR = reader2["Manager"].ToString();
                        SectionGeneralMGR = reader2["General Manager"].ToString();
                        BILSupport = reader2["BIL Support"].ToString();
                        MHPIC = reader2["MH PIC"].ToString();
                        PCPIC = reader2["PC PIC"].ToString();
                        FEPIC = reader2["FE PIC"].ToString();
                        LoggedIn = reader2["RecentLoggedIn"].ToString();


                        this.Hide();
                        SectionMenuForm sectionMenuForm = new SectionMenuForm();
                        sectionMenuForm.ShowDialog();
                        this.Close();
                        
                    }
                }
                else
                {
                    // -> this Message show when the user account is not existing in database
                    MessageBox.Show("User not found!", "Access Denied", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }  //***** End of login fuction *****

        private void RecentLoggedIn()
        {
            SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

            con.Open();
            SqlCommand InsertLoggedInTime = new SqlCommand("SP_InsertLoggedInTime", con);
            InsertLoggedInTime.CommandType = CommandType.StoredProcedure;
            InsertLoggedInTime.Parameters.AddWithValue("@ADID", LoginForm.UserADID);
            InsertLoggedInTime.ExecuteNonQuery();
            con.Close();
        }


        //===================================================================================================================>>>>>>>>>>>>

        // ---> Hide the character inputed in password box
        private void HidePasswordEyeButton_Click(object sender, EventArgs e)
        {
            if (Password.PasswordChar == '•')
            {
                Password.PasswordChar = '\0';
            }
            else
            {
                Password.PasswordChar = '•';
            }

            ShowPasswordEyeButton.BringToFront();
            HidePasswordEyeButton.SendToBack();
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Show the character inputed in password box
        private void ShowPasswordEyeButton_Click(object sender, EventArgs e)
        {
            if (Password.PasswordChar == '•')
            {
                Password.PasswordChar = '\0';
            }
            else
            {
                Password.PasswordChar = '•';
            }

            ShowPasswordEyeButton.SendToBack();
            HidePasswordEyeButton.BringToFront();
        }

        //====================================================================================================================>>>>>>>>>>>>

       
        private void SignInButton_Click(object sender, EventArgs e)
        {
             LoginUser(); // call out the login user function
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Show change password form when click the forgot password button
        private void ForgotPassword_Click(object sender, EventArgs e)
        {
            Hide();
            ChangePassword changePassword = new ChangePassword();
            changePassword.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Focus to password text box when hit enter key
        private void ADID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Password.Focus();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Login user when hit the enter key
        private void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                LoginUser(); // call out the login user function
            }
        }

        //====================================================================================================================>>>>>>>>>>>>
        //Get local IP Address of current computer
        public static string GetLocalIPAddress()
        {
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        
        
        private void GetIPAddressFromCentralizedLogin()
        {
            SqlConnection CentralizedLogin = new SqlConnection(SQLControl.CentralizedLogin);

            CentralizedLogin.Open();
            SqlCommand SelectUserAccount = new SqlCommand("SP_SelectLoginRequestFromCentralizedLogin", CentralizedLogin);
            SelectUserAccount.CommandType = CommandType.StoredProcedure;
            SelectUserAccount.Parameters.AddWithValue("@IPAddress", GetLocalIPAddress());
            SelectUserAccount.Parameters.AddWithValue("@SystemID", "11");
            SqlDataAdapter da = new SqlDataAdapter(SelectUserAccount);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                SqlDataReader reader = SelectUserAccount.ExecuteReader();

                if (reader.Read())
                {
                    UserADID = reader["ADID"].ToString();
                }                                                               
            }
            else
            {
                //MessageBox.Show("No data found!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static string localIP;
        public static string SystemID; 
        public static string SystemName;
        public static string ApproverNumber;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            ////ADID.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "");

            //ADID.Text = UserADID;

            //localIP = GetLocalIPAddress();

            //Get local ip of user PC
            GetIPAddressFromCentralizedLogin();

            //Process login
            LoginUser_2();

            SystemID = "11";
            SystemName = "Manhour Management System";
            ApproverNumber = "0";
        }

        private void SignInButton_MouseEnter(object sender, EventArgs e)
        {
            SignInButton.BackColor = Color.FromArgb(211, 240, 254);
            SignInButton.ForeColor = Color.FromArgb(21, 35, 53);
        }

        private void SignInButton_MouseLeave(object sender, EventArgs e)
        {
            SignInButton.BackColor = Color.FromArgb(21, 35, 53);
            SignInButton.ForeColor = Color.FromArgb(211, 240, 254);
        }

        //====================================================================================================================>>>>>>>>>>>>
    }
}
