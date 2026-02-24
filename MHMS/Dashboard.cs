using MHMS.Connection;
using MHMS.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MHMS
{
    public partial class Dashboard : Form
    {

        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        //Fields
        private Button currentBtn;
        /*private Panel leftBorderBtn;*/ // --> this is use to border color of side bar button
        private Form currentChildForm; // --> initialized variable

        private bool ReportisCollapsed; // --> this is use as variable for report dropdown button
        private bool SettingIsCollapsed; // --> this is use as variable for setting dropdown button
        private bool ApplicationIsCollapsed; // --> this is use as variable for setting dropdown button

        public Dashboard()
        {
            InitializeComponent();

            //leftBorderBtn = new Panel();
            //leftBorderBtn.Size = new Size(7, 43);
            //SideBarPanel.Controls.Add(leftBorderBtn);

            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea; //use to maximize form base on screen size od computer
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(198, 46, 74);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(150, 46, 198);
            public static Color color5 = Color.FromArgb(171, 198, 46);
            public static Color color6 = Color.FromArgb(46, 198, 196);
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Methods
        private void ActivateButton(Object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                //Button
                currentBtn = (Button)senderBtn;
                currentBtn.BackColor = Color.FromArgb(46, 198, 196);
                currentBtn.ForeColor = Color.White;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Disable button higlighted
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(4, 41, 56);
                currentBtn.ForeColor = Color.FromArgb(213, 241, 252);
                //currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                //currentBtn.IconColor = Color.FromArgb(213, 241, 252);
                //currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                //currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //Open only form
                currentChildForm.Close();
            }

            currentChildForm = childForm;

            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(childForm);
            MainPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            TitleChildForm.Text = childForm.Text;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ReportButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            ReportDropdownTimer.Start();
            Icon.Image = ReportButton.Image;
            ApprovalButton.BackColor = Color.Transparent;

            //IsReportFormOpen = true;

            //if (IsReportFormOpen == true)
            //{
            //    ActivateButton(sender, RGBColors.color6);
            //    ReportDropdownTimer.Start();
            //    Icon.Image = ReportButton.Image;
            //    ApprovalButton.BackColor = Color.Transparent;
            //}
            //else
            //{
            //    ApplicationIsCollapsed = false;
            //    ReportDropdownTimer.Stop();
            //}

           
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ReportDropdownTimer_Tick(object sender, EventArgs e)
        {
            if (ReportisCollapsed)
            {
                ReportButtonPanel.Height += 10;
                if (ReportButtonPanel.Size == ReportButtonPanel.MaximumSize)
                {
                    ReportDropdownTimer.Stop();
                    ReportisCollapsed = false;
                }
            }
            else
            {
                ReportButtonPanel.Height -= 10;
                if (ReportButtonPanel.Size == ReportButtonPanel.MinimumSize)
                {
                    ReportDropdownTimer.Stop();
                    ReportisCollapsed = true;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            SettingDropdownTimer.Start();
            Icon.Image = SettingsButton.Image;
            ApprovalButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SettingDropdownTimer_Tick(object sender, EventArgs e)
        {
            if (SettingIsCollapsed)
            {
                SettingButtonPanel.Height += 10;
                if (SettingButtonPanel.Size == SettingButtonPanel.MaximumSize)
                {
                    SettingDropdownTimer.Stop();
                    SettingIsCollapsed = false;
                }
            }
            else
            {
                SettingButtonPanel.Height -= 10;
                if (SettingButtonPanel.Size == SettingButtonPanel.MinimumSize)
                {
                    SettingDropdownTimer.Stop();
                    SettingIsCollapsed = true;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            //OpenChildForm(new Forms.DashboardForm());
            OpenChildForm(new Forms.DashboardForm3());
            Icon.Image = DashboardButton.Image;
            ApprovalButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        public static bool IsApplicationFormOpen = false;
        public static bool IsReportFormOpen = false;

        private void ApplicationFormButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            //OpenChildForm(new Forms.ApplicationForm());
            Icon.Image = ApplicationFormButton.Image;
            ApplicationDropdownTimer.Start();

            //IsApplicationFormOpen = true;

            //if (IsApplicationFormOpen == true)
            //{
            //    ActivateButton(sender, RGBColors.color6);
            //    //OpenChildForm(new Forms.ApplicationForm());
            //    Icon.Image = ApplicationFormButton.Image;
            //    ApplicationDropdownTimer.Start();
            //}
            //else
            //{
            //    ReportisCollapsed = false;
            //    ApplicationDropdownTimer.Stop();
            //}
            
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApplicationDropdownTimer_Tick(object sender, EventArgs e)
        {
            if (ApplicationIsCollapsed)
            {
                ApplicationFormPanel.Height += 10;
                if (ApplicationFormPanel.Size == ApplicationFormPanel.MaximumSize)
                {
                    ApplicationDropdownTimer.Stop();
                    ApplicationIsCollapsed = false;
                }
            }
            else
            {
                ApplicationFormPanel.Height -= 10;
                if (ApplicationFormPanel.Size == ApplicationFormPanel.MinimumSize)
                {
                    ApplicationDropdownTimer.Stop();
                    ApplicationIsCollapsed = true;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ManpowerForecastButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.ManpowerForecastForm());
            Icon.Image = ManpowerForecastButton.Image;
            ManpowerForecastButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DPRButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.DPRForm());
            Icon.Image = DPRButton.Image;
            ApprovalButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void COPQPartLossButton_Click(object sender, EventArgs e)
        {
            string UserSection = SectionLabel.Text.Replace("BIPH-", "");

            ActivateButton(sender, RGBColors.color6);
            Icon.Image = COPQPartLossButton.Image;

            if (UserSection == "BPS" || UserSection == "Tape Cassette" || UserSection == "Ink Cartridge" || UserSection == "Ink Head" || UserSection == "Printer" || UserSection == "P-Touch")
            {
                OpenChildForm(new Forms.COPQPartsLossForm());
            }
            else
            {
                MessageBox.Show("Sorry, you are not allowed to access this module", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            

        }

        //====================================================================================================================>>>>>>>>>>>>

        private void COPQManhourLossButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.COPQManhourLossForm());
            Icon.Image = COPQManhourLossButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void EfficiencyButton_Click(object sender, EventArgs e)
        {
            //if (SectionText.Replace("BIPH-", "") == "BPS")
            //{
                ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ProductionEfficiencyForm());
                Icon.Image = ProductionEfficiencyButton.Image;
            //}
            //else
            //{
            //    MessageBox.Show("Sorry, this page is currently unavailable.", "Ongoing development!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApproverSettingButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.ApproverSettingForm());
            Icon.Image = ApproverSettingButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void TargetSettingButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.TargetSettingForm());
            Icon.Image = TargetSettingButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else { }
        }

        //====================================================================================================================>>>>>>>>>>>>

        public static bool MaximizeIsClicked = false;

        private void MaximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void MinimizedButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Drag Form ------------------>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void TopBarPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void HeaderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }// <---------------------------

        //====================================================================================================================>>>>>>>>>>>>

        private void Logo_Click(object sender, EventArgs e)
        {
            if (TitleChildForm.Text != "Home")
            {
                currentChildForm.Close();
                Reset();
            }
            else
            {
                //Stay in current form
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void Reset()
        {
            DisableButton();
            //leftBorderBtn.Visible = false;
            TitleChildForm.Text = "Home";
            //Icon.Image = MHMS.Properties.Resources.house_32;
        }

        //====================================================================================================================>>>>>>>>>>>>

        public static string SectionText;
        public static string AccountType;
        public static string DepartmentText;
        public static string EEAssignedSection;
           
        private async void Dashboard_Load(object sender, EventArgs e)
        {
            LoadingForm loadingForm = new LoadingForm();
            loadingForm.Show();

            try
            {
                //Code that will adjust Time Format h:mm tt 1:43 pm
                RegistryKey keyShortTime = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                keyShortTime.SetValue("sShortTime", "hh:mm tt");

                RegistryKey keyLongTime = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                keyLongTime.SetValue("sTimeFormat", "hh:mm:ss tt");

                //Code that will change the regional Format
                //RegistryKey keyLocalName = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                //keyLocalName.SetValue("LocaleName", "en-PH");

                //Code that will adjust Date Format MM/dd/yyyy 05/03/2023
                RegistryKey keyShortDate = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                keyShortDate.SetValue("sShortDate", "MM/dd/yyyy");

                //Long date
                RegistryKey keyLongDate = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                keyLongDate.SetValue("sLongDate", "dddd, d MMMM yyyy");

                if (LoginForm.isSingleSectionAccess == true)
                {
                    SectionLabel.Text = LoginForm.SectionName;
                    UserSection.Text = LoginForm.UserSection;
                    SectionText = SectionLabel.Text;
                    DepartmentText = LoginForm.Department;
                    AccountType = AccountTypeLabel.Text;
                    LoggedIn.Text = "Logged in: " + LoginForm.LoggedIn;
                    SectionMenuForm.isMultiSectionAccess = false;
                    EEAssignedSection = LoginForm.EESection;
                }

                if (SectionMenuForm.isMultiSectionAccess == true)
                {
                    SectionLabel.Text = SectionMenuForm.SectionName;
                    UserSection.Text = SectionMenuForm.UserSection;
                    SectionText = SectionLabel.Text;
                    DepartmentText = LoginForm.Department;
                    AccountType = AccountTypeLabel.Text;
                    LoggedIn.Text = "Logged in: " + LoginForm.LoggedIn;
                    LoginForm.isSingleSectionAccess = false;
                    EEAssignedSection = SectionMenuForm.EESection;
                }

                UserName.Text = "Welcome " + LoginForm.FirstName + "!";
                UserLoginName.Text = LoginForm.FirstName + " " + LoginForm.LastName;
                AccountTypeLabel.Text = LoginForm.AccountType;

                //NotifCount.Text = (Convert.ToInt32(NotificationForm.ApplyingCount) + Convert.ToUInt32(NotificationForm.ReceivingCount)).ToString();

                if (AccountTypeLabel.Text == "ADMIN")
                {
                    SettingsButton.Enabled = true;
                    SettingsButton.BackColor = Color.FromArgb(21, 35, 53);
                }
                else if (AccountTypeLabel.Text == "ADMIN")
                {
                    SettingsButton.Enabled = true;
                    SettingsButton.BackColor = Color.FromArgb(21, 35, 53);
                }
                else
                {
                    SettingButtonPanel.Visible = false;
                }

                // ---> Get Fullname of user
                InitialNameButton.Text = (LoginForm.FirstName.Substring(0, 1) + LoginForm.LastName.Substring(0, 1)).ToUpper();
                UserPicture.Text = (LoginForm.FirstName.Substring(0, 1) + LoginForm.LastName.Substring(0, 1)).ToUpper();

                //set version info
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                this.SystemVersion.Text = String.Format(this.SystemVersion.Text, version.Major, version.Minor, version.Build, version.Revision);

                //SelectForApprovalCount();

                SelectApplyingForApprovalRequest();
                SelectReceivingForApprovalRequest();

                if ((Convert.ToUInt32(ApplyingCount) + Convert.ToUInt32(ReceivingCount)) > 9)
                {
                    NotifCount.Text = "9+";
                }
                else
                {
                    NotifCount.Text = (Convert.ToUInt32(ApplyingCount) + Convert.ToUInt32(ReceivingCount)).ToString();
                }

                loadingForm.Show();

                // Ensure WebView2 is properly initialized.
                await webView21.EnsureCoreWebView2Async(null);

                MHvsCOPQPerProdBtn.BackColor = SelectedColor;
               
                // Perform action for "Per Production Section"
                webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/MHvsCOPQLineStopComparison/MHvsCOPQLineStopComparisonPerProductionSection");

                await Task.Delay(5000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                loadingForm.Close();
            }
        }

       
        //====================================================================================================================>>>>>>>>>>>>

        private void SelectForApprovalCount()
        {
            //ApprovalCount.Visible = true;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectApprovalCount = new SqlCommand("SP_SelectForApprovalRequestCount", con);
            SelectApprovalCount.CommandType = CommandType.StoredProcedure;
            SelectApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SelectApprovalCount.Parameters.AddWithValue("@Procedure", "SelecForApprovalCount");
            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalCount);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                SqlDataReader reader = SelectApprovalCount.ExecuteReader();
                while (reader.Read())
                {
                    int a = Convert.ToInt32(reader["ForApprovalCount"].ToString()) + Convert.ToInt32(reader["ForApprovalCount"].ToString());
                    NotifCount.Text = a.ToString();
                }
            }

            con.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DateAndTime_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateTimeLabel.Text = dateTime.ToString("dddd , MMM dd, yyyy hh : mm : ss tt");
        }

        //====================================================================================================================>>>>>>>>>>>>

        public static string sections = "";

        private void SectionLabel_TextChanged(object sender, EventArgs e)
        {
            sections = SectionLabel.Text;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void InitialNameButton_Click(object sender, EventArgs e)
        {
            if (ChangePasswordPanel.Visible == false)
            {
                ChangePasswordPanel.Visible = true;
            }
            else
            {
                ChangePasswordPanel.Visible = false;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            ChangePasswordPanel.Visible = false;
            var changePassword = new ChangePassword();
            changePassword.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApplicationButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.ApplicationForm());
            Icon.Image = ApplicationButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApprovalButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.ApprovalForm());
            Icon.Image = ApprovalButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void NotificationBellButton_Click(object sender, EventArgs e)
        {
            NotificationForm2 Notif = new NotificationForm2();
            Notif.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void TruobleChecksheetButton_Click(object sender, EventArgs e)
        {
            //Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\System Trouble Checksheet.xlsx");
            Process.Start(@"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\01_Hashira Activity\IT Hashira\FY2023\09_Manhour Management System\12_Problem Management List_MHMS COPQ and MH Approval.xlsm");
        }

        //====================================================================================================================>>>>>>>>>>>>
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else { }
        }

        //Notification form
        public static bool SeeAllIsClicked = false;

        //Approval form category
        //public static bool STCategoryIsSelected = false;

        //MH Approval
        public static bool ProceedBtnIsClicked = false; 

        //Dashboard
        public static bool COPQAcceptanceIsClicked = false;
        public static bool STIsClicked = false;
        public static bool WCCCIsClicked = false;
        public static bool ApplicationIsSubmitted = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Notification - see all button action
            if (SeeAllIsClicked == true)
            {
                ApplicationDropdownTimer.Start();
                ApprovalButton.BackColor = Color.FromArgb(46, 198, 196);
                DashboardButton.BackColor = Color.Transparent;
                //OpenChildForm(new Forms.ApplicationForm());
                Icon.Image = ApplicationFormButton.Image;
                //ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ApprovalForm());
                Icon.Image = ApprovalButton.Image;

                SeeAllIsClicked = false;
            }

            if (ApplicationIsSubmitted == true)
            {
                //ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ApplicationForm());
                Icon.Image = ApplicationButton.Image;

                ApplicationIsSubmitted = false;
            }

            //if (STCategoryIsSelected == true)
            //{
            //    ApplicationDropdownTimer.Start();
            //    ApprovalButton.BackColor = Color.FromArgb(46, 198, 196);
            //    DashboardButton.BackColor = Color.Transparent;
            //    OpenChildForm(new Forms.OtherApproval());
            //    Icon.Image = OtherApprovalButton.Image;

            //    STCategoryIsSelected = false;
            //}

            if (COPQAcceptanceIsClicked == true)
            {
                ApplicationDropdownTimer.Start();
                ApprovalButton.BackColor = Color.FromArgb(46, 198, 196);
                DashboardButton.BackColor = Color.Transparent;
                //OpenChildForm(new Forms.ApplicationForm());
                Icon.Image = ApplicationFormButton.Image;
                //ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ApprovalForm());
                Icon.Image = ApprovalButton.Image;

                COPQAcceptanceIsClicked = false;
            }

            if (STIsClicked == true)
            {
                //ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ApplicationForm());
                Icon.Image = ApprovalButton.Image;

                STIsClicked = false;
            }


            if (WCCCIsClicked == true)
            {
                //ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ApplicationForm());
                Icon.Image = ApprovalButton.Image;

                WCCCIsClicked = false;
            }

            if (ProceedBtnIsClicked == true)
            {
                ApplicationDropdownTimer.Start();
                MHApprovalButton.BackColor = Color.FromArgb(46, 198, 196);
                DashboardButton.BackColor = Color.Transparent;
                OpenChildForm(new Forms.MHApproval());
                Icon.Image = MHApprovalButton.Image;

                ProceedBtnIsClicked = false;
            }
        }

        private void SwitchAccountButton_Click(object sender, EventArgs e)
        {
            //Hide();
            //LoginForm loginForm = new LoginForm();
            //loginForm.ShowDialog();
            //this.Close();

            Application.Exit();
            Process.Start(@"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\Installer\BPS Centralized Login\setup.exe");
        }

        private void UpdateSystemButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to update and restart the system?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
                Process.Start(@"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe");
            }
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        //============================================================================>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        int COPQPICCount;
        int COPQProcessInChargeCount;
        int SPVCount;
        int MGRCount;

        public static string ApplyingCount;
        public static string ReceivingCount;

        private void SelectApplyingForApprovalRequest()
        {
            if (LoginForm.COPQPIC == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //Count For approval per section pic
                SqlCommand SelectApplyingApprovalCount = new SqlCommand("SP_SelectForApprovalRequestCount", con);
                SelectApplyingApprovalCount.CommandType = CommandType.StoredProcedure;
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQPIC");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", "");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Type", "Applying");
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        COPQPICCount = Convert.ToInt32(reader2["ForApprovalCount"]);
                    }
                }

                con.Close();

            }


            if (LoginForm.SectionSPV == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //Count For approval per section pic
                SqlCommand SelectApplyingApprovalCount = new SqlCommand("SP_SelectForApprovalRequestCount", con);
                SelectApplyingApprovalCount.CommandType = CommandType.StoredProcedure;
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Procedure", "SelectForApprovalbySPV");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Type", "Applying");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", "");
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        SPVCount = Convert.ToInt32(reader2["ForApprovalCount"]);
                    }
                }

                con.Close();

            }

            if (LoginForm.SectionMGR == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //Count For approval per section pic
                SqlCommand SelectApplyingApprovalCount = new SqlCommand("SP_SelectForApprovalRequestCount", con);
                SelectApplyingApprovalCount.CommandType = CommandType.StoredProcedure;
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyMGR");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Type", "Applying");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", "");
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        MGRCount = Convert.ToInt32(reader2["ForApprovalCount"]);
                    }
                }

                con.Close();

            }

            ApplyingCount = (COPQPICCount + SPVCount + MGRCount).ToString();

        }

        //============================================================================>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void SelectReceivingForApprovalRequest()
        {

            if (LoginForm.COPQPIC == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                //Count For approval per section pic
                SqlCommand SelectApplyingApprovalCount = new SqlCommand("SP_SelectForApprovalRequestCount", con);
                SelectApplyingApprovalCount.CommandType = CommandType.StoredProcedure;
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQPIC");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Type", "Receiving");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        COPQPICCount = Convert.ToInt32(reader2["ForApprovalCount"]);
                    }
                }

                con.Close();

                //COPQPICButton.Visible = true;
                //MGRButton.Visible = false;
                //COPQProcessInchargeButton.Visible = false;
                //SPVButton.Visible = false;
            }

            if (LoginForm.ProcessInCharge == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //Count For approval per section pic
                SqlCommand SelectApplyingApprovalCount = new SqlCommand("SP_SelectForApprovalRequestCount", con);
                SelectApplyingApprovalCount.CommandType = CommandType.StoredProcedure;
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQProcessInCharge");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Type", "Receiving");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        COPQProcessInChargeCount = Convert.ToInt32(reader2["ForApprovalCount"]);
                    }
                }

                con.Close();
                
            }


            if (LoginForm.SectionSPV == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //Count For approval per section pic
                SqlCommand SelectApplyingApprovalCount = new SqlCommand("SP_SelectForApprovalRequestCount", con);
                SelectApplyingApprovalCount.CommandType = CommandType.StoredProcedure;
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Procedure", "SelectForApprovalbySPV");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Type", "Receiving");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        SPVCount = Convert.ToInt32(reader2["ForApprovalCount"]);
                    }
                }

                con.Close();

                //SPVButton.Visible = true;
                //MGRButton.Visible = false;
                //COPQPICButton.Visible = false;
                //COPQProcessInchargeButton.Visible = false;
            }

            if (LoginForm.SectionMGR == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                //Count For approval per section pic
                SqlCommand SelectApplyingApprovalCount = new SqlCommand("SP_SelectForApprovalRequestCount", con);
                SelectApplyingApprovalCount.CommandType = CommandType.StoredProcedure;
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyMGR");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Type", "Receiving");
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        MGRCount = Convert.ToInt32(reader2["ForApprovalCount"]);
                    }
                }

                con.Close();

                //MGRButton.Visible = true;
                //SPVButton.Visible = false;
                //COPQPICButton.Visible = false;
                //COPQProcessInchargeButton.Visible = false;
            }

            ReceivingCount = (COPQPICCount + COPQProcessInChargeCount + SPVCount + MGRCount).ToString();

        }



        private void OperationalManualButton_Click(object sender, EventArgs e)
        {
            //Operational manual link
            Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Manual\COPQ Operational Manual.pdf");
        }

        private void MHApprovalButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.MHApproval());
            Icon.Image = MHApprovalButton.Image;
        }

        private void FactoryEfficiencyButton_Click(object sender, EventArgs e)
        {
            if (LoginForm.FEPIC == "✔️" || SectionMenuForm.FEPIC == "✔️")
            {
                ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.FactoryEfficiencyForm());
                Icon.Image = MHApprovalButton.Image;
            }
            else
            {
                MessageBox.Show("You don't have permission to access this module.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OverallMonitoringBtn_Click(object sender, EventArgs e)
        {
            if (SectionText.Replace("BIPH-","") == "Production Control" || SectionText.Replace("BIPH-", "") == "BPS")
            {
                ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.OverallMonitoring());
                Icon.Image = MHApprovalButton.Image;
            }
            else
            {
                MessageBox.Show("Only the PC section is authorized to access this module.", "MHMS Information.",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            
            //Process.Start("http://apbiphbpsts02:8080/mhms-overall-monitoring/");
        }

        private void PartsRegsBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.PartsRegistrationForm());
            Icon.Image = PartsRegsBtn.Image;
        }


        Color DefaultColor = Color.AliceBlue;
        Color DefaultTextColor = Color.Black;
        Color SelectedColor = Color.FromArgb(47, 69, 180);
        Color SelectedTextColor = Color.White;
        private void MHvsCOPQPerProdBtn_Click(object sender, EventArgs e)
        {
            MHvsCOPQPerProdBtn.BackColor = SelectedColor;
            MHvsCOPQPerProdBtn.ForeColor = SelectedTextColor;
            MHvsCOPQPerLossFactorBtn.BackColor = DefaultColor;
            MHvsCOPQPerLossFactorBtn.ForeColor = DefaultTextColor;

            // Perform action for "Per Production Section"
            webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/MHvsCOPQLineStopComparison/MHvsCOPQLineStopComparisonPerProductionSection");
        }

        private void MHvsCOPQPerLossFactorBtn_Click(object sender, EventArgs e)
        {
            MHvsCOPQPerLossFactorBtn.BackColor = SelectedColor;
            MHvsCOPQPerLossFactorBtn.ForeColor = SelectedTextColor;
            MHvsCOPQPerProdBtn.BackColor = DefaultColor;
            MHvsCOPQPerProdBtn.ForeColor = DefaultTextColor;

            // Perform action for "Per Loss Factor"
            webView21.CoreWebView2.Navigate("https://bi.datalake.brother.co.jp/#/site/biph/views/MHvsCOPQLineStopComparison/MHvsCOPQLineStopComparisonPerLossFactor");
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        //=======================================================================================================>>>>>>>>>>>>
    }
}
