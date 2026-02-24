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
    public partial class NotificationForm2 : Form
    {
        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public NotificationForm2()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void ApplyingBtn_Click(object sender, EventArgs e)
        {
            ApproverBtnPanel.Visible = true;

            Recv_COPQPICBtn.Visible = false;
            Recv_COPQProcessInchargeBtnsda.Visible = false;
            Recv_SPVBtn.Visible = false;
            Recv_MGRBtn.Visible = false;


            App_COPQPICBtn.Visible = true;
            App_SPVBtn.Visible = true;
            App_MGRBtn.Visible = true;

            App_MGRBtn.BackColor = DefaultBackColor;
            App_MGRBtn.ForeColor = DefaultForeColor;

            App_COPQPICBtn.BackColor = DefaultBackColor;
            App_COPQPICBtn.ForeColor = DefaultForeColor;

            App_SPVBtn.BackColor = DefaultBackColor;
            App_SPVBtn.ForeColor = DefaultForeColor;

            NotificationDataGridView.DataSource = null;
        }

        private void ReceivingBtn_Click(object sender, EventArgs e)
        {
            ApproverBtnPanel.Visible = true;

            App_COPQPICBtn.Visible = false;
            App_SPVBtn.Visible = false;
            App_MGRBtn.Visible = false;

            Recv_COPQPICBtn.Visible = true;
            Recv_COPQProcessInchargeBtnsda.Visible = true;
            Recv_SPVBtn.Visible = true;
            Recv_MGRBtn.Visible = true;

            Recv_COPQPICBtn.BackColor = DefaultBackColor;
            Recv_COPQPICBtn.ForeColor = DefaultForeColor;

            Recv_COPQProcessInchargeBtnsda.BackColor = DefaultBackColor;
            Recv_COPQProcessInchargeBtnsda.ForeColor = DefaultForeColor;

            Recv_SPVBtn.BackColor = DefaultBackColor;
            Recv_SPVBtn.ForeColor = DefaultForeColor;

            Recv_MGRBtn.BackColor = DefaultBackColor;
            Recv_MGRBtn.ForeColor = DefaultForeColor;

            NotificationDataGridView.DataSource = null;
        }

        private void SeeAllForApprovalButton_Click(object sender, EventArgs e)
        {
            Dashboard.SeeAllIsClicked = true;
            this.Close();
        }

        private void NotificationForm2_Load(object sender, EventArgs e)
        {
            SelectApplyingForApprovalRequestCount();
            SelectReceivingForApprovalRequestCount();

            ApplyingBtn.Text = "APPLYING (" + (App_COPQPICCount + App_SPVCount + App_MGRCount).ToString() + ")";
            ReceivingBtn.Text = "RECEIVING (" + (Recv_COPQPICCount + Recv_COPQProcessInChargeCount + Recv_SPVCount + Recv_MGRCount).ToString() + ")";

        }

        int App_COPQPICCount;
        int App_SPVCount;
        int App_MGRCount;

        int Recv_COPQPICCount;
        int Recv_COPQProcessInChargeCount;
        int Recv_SPVCount;
        int Recv_MGRCount;

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
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", "");
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        App_COPQPICCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        App_COPQPICBtn.Text = "COPQ PIC (" + App_COPQPICCount.ToString() + ")";
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
                        App_SPVCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        App_SPVBtn.Text = "SPV (" + App_SPVCount.ToString() + ")";
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
                        App_MGRCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        App_MGRBtn.Text = "MGR (" + App_MGRCount.ToString() + ")";
                    }
                }

                con.Close();

            }

            ApplyingCount = "Applying (" + (App_COPQPICCount + App_SPVCount + App_MGRCount) + ")";
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
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        Recv_COPQPICCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        Recv_COPQPICBtn.Text = "COPQ PIC (" + Recv_COPQPICCount.ToString() + ")";
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
                        Recv_COPQProcessInChargeCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        Recv_COPQProcessInchargeBtnsda.Text = "COPQ Process In-Charge (" + Recv_COPQProcessInChargeCount.ToString() + ")";
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
                SelectApplyingApprovalCount.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectApplyingApprovalCount);
                DataTable dataTable = new DataTable();
                sda2.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    SqlDataReader reader2 = SelectApplyingApprovalCount.ExecuteReader();
                    while (reader2.Read())
                    {
                        Recv_SPVCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        Recv_SPVBtn.Text = "SPV (" + Recv_SPVCount.ToString() + ")";
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
                        Recv_MGRCount = Convert.ToInt32(reader2["ForApprovalCount"].ToString());
                        Recv_MGRBtn.Text = "MGR (" + Recv_MGRCount.ToString() + ")";
                    }
                }

                con.Close();

                //MGRButton.Visible = true;
                //SPVButton.Visible = false;
                //COPQPICButton.Visible = false;
                //COPQProcessInchargeButton.Visible = false;
            }

            ReceivingCount = "Receiving (" + (Recv_COPQPICCount + Recv_COPQProcessInChargeCount + Recv_SPVCount + Recv_MGRCount) + ")";

        }

        Color DefaultForeColor = Color.FromArgb(47, 69, 180);
        Color DefaultBackColor = Color.FromArgb(223, 237, 255);
        Color SelectedColor = Color.FromArgb(78, 122, 199);

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
                LoadNotification.Parameters.AddWithValue("@AssignedSection", "");
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
                LoadNotification.Parameters.AddWithValue("@AssignedSection", "");
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
                LoadNotification.Parameters.AddWithValue("@AssignedSection", "");
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
                LoadNotification.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
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
                LoadNotification.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
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
                LoadNotification.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
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
                LoadNotification.Parameters.AddWithValue("@AssignedSection", LoginForm.EESection);
                SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                NotificationDataGridView.DataSource = dTable;
            }
        }

        private void App_COPQPICBtn_Click(object sender, EventArgs e)
        {
            App_COPQPICBtn.BackColor = SelectedColor;
            App_COPQPICBtn.ForeColor = Color.White;

            //COPQProcessInchargeButton.BackColor = DefaultBackColor;
            //COPQProcessInchargeButton.ForeColor = DefaultForeColor;

            App_SPVBtn.BackColor = DefaultBackColor;
            App_SPVBtn.ForeColor = DefaultForeColor;

            App_MGRBtn.BackColor = DefaultBackColor;
            App_MGRBtn.ForeColor = DefaultForeColor;

            SelectApplyingCOPQPICForApprovalRequest();
        }

        private void App_SPVBtn_Click(object sender, EventArgs e)
        {
            App_SPVBtn.BackColor = SelectedColor;
            App_SPVBtn.ForeColor = Color.White;

            App_COPQPICBtn.BackColor = DefaultBackColor;
            App_COPQPICBtn.ForeColor = DefaultForeColor;

            //COPQProcessInchargeButton.BackColor = DefaultBackColor;
            //COPQProcessInchargeButton.ForeColor = DefaultForeColor;

            App_MGRBtn.BackColor = DefaultBackColor;
            App_MGRBtn.ForeColor = DefaultForeColor;

            SelectApplyingSPVForApprovalRequest();
        }

        private void App_MGRBtn_Click(object sender, EventArgs e)
        {
            App_MGRBtn.BackColor = SelectedColor;
            App_MGRBtn.ForeColor = Color.White;

            App_COPQPICBtn.BackColor = DefaultBackColor;
            App_COPQPICBtn.ForeColor = DefaultForeColor;

            //COPQProcessInchargeButton.BackColor = DefaultBackColor;
            //COPQProcessInchargeButton.ForeColor = DefaultForeColor;

            App_SPVBtn.BackColor = DefaultBackColor;
            App_SPVBtn.ForeColor = DefaultForeColor;

            SelectApplyingMGRForApprovalRequest();
        }

        private void Recv_COPQPICBtn_Click(object sender, EventArgs e)
        {
            Recv_COPQPICBtn.BackColor = SelectedColor;
            Recv_COPQPICBtn.ForeColor = Color.White;

            Recv_COPQProcessInchargeBtnsda.BackColor = DefaultBackColor;
            Recv_COPQProcessInchargeBtnsda.ForeColor = DefaultForeColor;

            Recv_SPVBtn.BackColor = DefaultBackColor;
            Recv_SPVBtn.ForeColor = DefaultForeColor;

            Recv_MGRBtn.BackColor = DefaultBackColor;
            Recv_MGRBtn.ForeColor = DefaultForeColor;

            SelectReceivingCOPQPICForApprovalRequest();
        }

        private void Recv_COPQProcessInchargeBtn_Click(object sender, EventArgs e)
        {
            Recv_COPQProcessInchargeBtnsda.BackColor = SelectedColor;
            Recv_COPQProcessInchargeBtnsda.ForeColor = Color.White;

            Recv_COPQPICBtn.BackColor = DefaultBackColor;
            Recv_COPQPICBtn.ForeColor = DefaultForeColor;

            Recv_SPVBtn.BackColor = DefaultBackColor;
            Recv_SPVBtn.ForeColor = DefaultForeColor;

            Recv_MGRBtn.BackColor = DefaultBackColor;
            Recv_MGRBtn.ForeColor = DefaultForeColor;

            SelectReceivingCOPQProcessInChargeForApprovalRequest();
        }

        private void Recv_SPVBtn_Click(object sender, EventArgs e)
        {
            Recv_SPVBtn.BackColor = SelectedColor;
            Recv_SPVBtn.ForeColor = Color.White;

            Recv_COPQPICBtn.BackColor = DefaultBackColor;
            Recv_COPQPICBtn.ForeColor = DefaultForeColor;

            Recv_COPQProcessInchargeBtnsda.BackColor = DefaultBackColor;
            Recv_COPQProcessInchargeBtnsda.ForeColor = DefaultForeColor;

            Recv_MGRBtn.BackColor = DefaultBackColor;
            Recv_MGRBtn.ForeColor = DefaultForeColor;

            SelectReceivingSPVForApprovalRequest();
        }

        private void Recv_MGRBtn_Click(object sender, EventArgs e)
        {
            Recv_MGRBtn.BackColor = SelectedColor;
            Recv_MGRBtn.ForeColor = Color.White;

            Recv_COPQPICBtn.BackColor = DefaultBackColor;
            Recv_COPQPICBtn.ForeColor = DefaultForeColor;

            Recv_COPQProcessInchargeBtnsda.BackColor = DefaultBackColor;
            Recv_COPQProcessInchargeBtnsda.ForeColor = DefaultForeColor;

            Recv_SPVBtn.BackColor = DefaultBackColor;
            Recv_SPVBtn.ForeColor = DefaultForeColor;

            SelectReceivingMGRForApprovalRequest();
        }
    }
}
