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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class NotificationForm : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public NotificationForm()
        {
            InitializeComponent();
        }

        //================================================================================================================>>>>>>>>>>>>>>

        private void NotificationForm_Load(object sender, EventArgs e)
        {

            if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
            {
                COPQPICButton.Visible = true;
                SPVButton.Visible = true;
                MGRButton.Visible = true;
                COPQProcessInchargeButton.Visible = false;

                SelectApplyingForApprovalRequestCount();


            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
            {

                COPQPICButton.Visible = true;
                SPVButton.Visible = true;
                MGRButton.Visible = true;
                COPQProcessInchargeButton.Visible = false;

                SelectApplyingForApprovalRequestCount();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
            {

                COPQPICButton.Visible = true;
                SPVButton.Visible = true;
                MGRButton.Visible = true;
                COPQProcessInchargeButton.Visible = false;

                SelectApplyingForApprovalRequestCount();

            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                COPQPICButton.Visible = true;
                SPVButton.Visible = true;
                MGRButton.Visible = true;
                COPQProcessInchargeButton.Visible = false;

                SelectApplyingForApprovalRequestCount();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
            {
                COPQPICButton.Visible = true;
                SPVButton.Visible = true;
                MGRButton.Visible = true;
                COPQProcessInchargeButton.Visible = false;

                SelectApplyingForApprovalRequestCount();
            }
            else
            {
                COPQPICButton.Visible = true;
                SPVButton.Visible = true;
                MGRButton.Visible = true;
                COPQProcessInchargeButton.Visible = true;

                SelectReceivingForApprovalRequestCount();
            }

            SelectApplyingForApprovalRequestCount();
            SelectReceivingForApprovalRequestCount();
        }

      

        //================================================================================================================>>>>>>>>>>>>>>

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //================================================================================================================>>>>>>>>>>>>>>

        //Drag Form ------------------>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void NotificationTopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        // <---------------------------

        //================================================================================================================>>>>>>>>>>>>>>

        int COPQPICCount;
        int COPQProcessInChargeCount;
        int SPVCount;
        int MGRCount;

        public static string ApplyingCount;
        public static string ReceivingCount;

        private void SelectApplyingForApprovalRequestCount()
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
                SelectApplyingApprovalCount.Parameters.AddWithValue("@Type", "Applying");
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        COPQPICCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        COPQPICButton.Text = "COPQ PIC (" + COPQPICCount.ToString() + ")";
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
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        SPVCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        SPVButton.Text = "SPV (" + SPVCount.ToString() + ")";
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
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        MGRCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        MGRButton.Text = "MGR (" + MGRCount.ToString() + ")";
                    }
                }

                con.Close();
             
            }

            ApplyingCount = "Applying (" + (COPQPICCount + SPVCount + MGRCount) + ")";
        }


        private void SelectReceivingForApprovalRequestCount()
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
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        COPQPICCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        COPQPICButton.Text = "COPQ PIC (" + COPQPICCount.ToString() + ")";
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
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        COPQProcessInChargeCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        COPQProcessInchargeButton.Text = "COPQ Process In-Charge (" + COPQProcessInChargeCount.ToString() + ")";
                    }
                }

                con.Close();

                //COPQProcessInchargeButton.Visible = true;
                //MGRButton.Visible = false;
                //COPQPICButton.Visible = false;
                //SPVButton.Visible = false;
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
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        SPVCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        SPVButton.Text = "SPV (" + SPVCount.ToString() + ")";
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
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        MGRCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        MGRButton.Text = "MGR (" + MGRCount.ToString() + ")";
                    }
                }

                con.Close();

                //MGRButton.Visible = true;
                //SPVButton.Visible = false;
                //COPQPICButton.Visible = false;
                //COPQProcessInchargeButton.Visible = false;
            }

            ReceivingCount = "Receiving (" + (COPQPICCount + COPQProcessInChargeCount + SPVCount + MGRCount) + ")";

        }

        private void SeeAllForApprovalButton_Click(object sender, EventArgs e)
        {
            Dashboard.SeeAllIsClicked = true;
            //this.Close();
        }

        private void SelectApplyingForApprovalRequest()
        {

            if (LoginForm.COPQPIC == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQPIC");
                LoadNotification.Parameters.AddWithValue("@Type", "Applying");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }


            if (LoginForm.SectionSPV == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbySPV");
                LoadNotification.Parameters.AddWithValue("@Type", "Applying");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }

            if (LoginForm.SectionMGR == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyMGR");
                LoadNotification.Parameters.AddWithValue("@Type", "Applying");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }

        }

        private void SelectReceivingForApprovalRequest()
        {

            if (LoginForm.COPQPIC == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQPIC");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }

            if (LoginForm.ProcessInCharge == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQProcessInCharge");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }


            if (LoginForm.SectionSPV == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbySPV");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }

            if (LoginForm.SectionMGR == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyMGR");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }

        }

        Color DefaultForeColor = Color.FromArgb(47, 69, 180);
        Color DefaultBackColor = Color.FromArgb(223, 237, 255);
        Color SelectedColor = Color.FromArgb(78, 122, 199);

        private void COPQPICButton_Click(object sender, EventArgs e)
        {

            COPQPICButton.BackColor = SelectedColor;
            COPQPICButton.ForeColor = Color.White;

            COPQProcessInchargeButton.BackColor = DefaultBackColor;
            COPQProcessInchargeButton.ForeColor = DefaultForeColor;

            SPVButton.BackColor = DefaultBackColor;
            SPVButton.ForeColor = DefaultForeColor;

            MGRButton.BackColor = DefaultBackColor;
            MGRButton.ForeColor = DefaultForeColor;

            if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
            {
                SelectApplyingCOPQPICForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
            {
                SelectApplyingCOPQPICForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
            {
                SelectApplyingCOPQPICForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                SelectApplyingCOPQPICForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
            {
                SelectApplyingCOPQPICForApprovalRequest();
            }
            else
            {
                SelectReceivingCOPQPICForApprovalRequest();
            }


        }

        private void COPQProcessInchargeButton_Click(object sender, EventArgs e)
        {
            COPQProcessInchargeButton.BackColor = SelectedColor;
            COPQProcessInchargeButton.ForeColor = Color.White;

            COPQPICButton.BackColor = DefaultBackColor;
            COPQPICButton.ForeColor = DefaultForeColor;

            SPVButton.BackColor = DefaultBackColor;
            SPVButton.ForeColor = DefaultForeColor;

            MGRButton.BackColor = DefaultBackColor;
            MGRButton.ForeColor = DefaultForeColor;

            if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
            {
                SelectApplyingForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
            {
                SelectApplyingForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
            {
                SelectApplyingForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                SelectApplyingForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
            {
                SelectApplyingForApprovalRequest();
            }
            else
            {
                SelectReceivingCOPQProcessInChargeForApprovalRequest();
            }
        }

        private void SPVButton_Click(object sender, EventArgs e)
        {
            SPVButton.BackColor = SelectedColor;
            SPVButton.ForeColor = Color.White;

            COPQPICButton.BackColor = DefaultBackColor;
            COPQPICButton.ForeColor = DefaultForeColor;

            COPQProcessInchargeButton.BackColor = DefaultBackColor;
            COPQProcessInchargeButton.ForeColor = DefaultForeColor;

            MGRButton.BackColor = DefaultBackColor;
            MGRButton.ForeColor = DefaultForeColor;

            if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
            {
                SelectApplyingSPVForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
            {
                SelectApplyingSPVForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
            {
                SelectApplyingSPVForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                SelectApplyingSPVForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
            {
                SelectApplyingSPVForApprovalRequest();
            }
            else
            {
                SelectReceivingSPVForApprovalRequest();
            }
        }

        private void MGRButton_Click(object sender, EventArgs e)
        {
            MGRButton.BackColor = SelectedColor;
            MGRButton.ForeColor = Color.White;

            COPQPICButton.BackColor = DefaultBackColor;
            COPQPICButton.ForeColor = DefaultForeColor;

            COPQProcessInchargeButton.BackColor = DefaultBackColor;
            COPQProcessInchargeButton.ForeColor = DefaultForeColor;

            SPVButton.BackColor = DefaultBackColor;
            SPVButton.ForeColor = DefaultForeColor;

            if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
            {
                SelectApplyingMGRForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
            {
                SelectApplyingMGRForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
            {
                SelectApplyingMGRForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                SelectApplyingMGRForApprovalRequest();
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
            {
                SelectApplyingMGRForApprovalRequest();
            }
            else
            {
                SelectReceivingMGRForApprovalRequest();
            }

        }

        private void SelectApplyingCOPQPICForApprovalRequest()
        {
            if (LoginForm.COPQPIC == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQPIC");
                LoadNotification.Parameters.AddWithValue("@Type", "Applying");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }
        }

        private void SelectApplyingSPVForApprovalRequest()
        {
            if (LoginForm.SectionSPV == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbySPV");
                LoadNotification.Parameters.AddWithValue("@Type", "Applying");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }

        }

        private void SelectApplyingMGRForApprovalRequest()
        {
            if (LoginForm.SectionMGR == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyMGR");
                LoadNotification.Parameters.AddWithValue("@Type", "Applying");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }
        }

        private void SelectReceivingCOPQPICForApprovalRequest()
        {

            if (LoginForm.COPQPIC == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQPIC");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }
        }


        private void SelectReceivingCOPQProcessInChargeForApprovalRequest()
        {

            if (LoginForm.ProcessInCharge == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyCOPQProcessInCharge");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }


            if (LoginForm.SectionSPV == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbySPV");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }

            if (LoginForm.SectionMGR == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyMGR");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;

            }
        }

        private void SelectReceivingSPVForApprovalRequest()
        {
            if (LoginForm.SectionSPV == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbySPV");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;
            }
        }

        private void SelectReceivingMGRForApprovalRequest()
        {
            if (LoginForm.SectionMGR == "✔️")
            {
                // Check Connection status -> Open connection if the connection is closed
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand LoadNotification = new SqlCommand("SP_SelectForApprovalRequest", con);
                LoadNotification.CommandType = CommandType.StoredProcedure;
                LoadNotification.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                LoadNotification.Parameters.AddWithValue("@Procedure", "SelectForApprovalbyMGR");
                LoadNotification.Parameters.AddWithValue("@Type", "Receiving");
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;
            }
        }

        private void NotificationDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in NotificationDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        //================================================================================================================>>>>>>>>>>>>>>

    }
}
