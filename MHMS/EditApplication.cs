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
    public partial class EditApplication : Form
    {
        //Connection String
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public EditApplication()
        {
            InitializeComponent();
        }

        string Category;
        private void EditApplication_Load(object sender, EventArgs e)
        {
            ReferenceNoLabel.Text = "<" + ApplicationForm.ReferenceNumber +">";

            Category = ApplicationForm.Category;

            SelectApplicationFormByReference();

            EditApplicationDataGrid.Columns["ApplicationFormNo"].Visible = false;

            if (ApplicationForm.ApplicationFormType == "ST")
            {
                if (Category == "MH New ST Model List Form")
                {
                    EditApplicationDataGrid.Columns[1].Frozen = false;
                    EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    EditApplicationDataGrid.Columns[1].Frozen = true;
                    EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
            else if (ApplicationForm.ApplicationFormType == "WC/CC")
            {
                EditApplicationDataGrid.Columns[1].Frozen = false;
                EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }



        private void SelectApplicationFormByReference()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectApplicationFormByReference = new SqlCommand("SP_SelectApplicationFormByReference", con);
            SelectApplicationFormByReference.CommandType = CommandType.StoredProcedure;
            SelectApplicationFormByReference.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
            SelectApplicationFormByReference.Parameters.AddWithValue("@Category", ApplicationForm.Category);
            SelectApplicationFormByReference.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
            SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormByReference);
            DataTable dt = new DataTable();
            da.Fill(dt);
            EditApplicationDataGrid.DataSource = dt;
            con.Close();

            if (ApplicationForm.Category == "Manpower/Man-hour")
            {
                EditApplicationDataGrid.Columns[1].Frozen = true;
                EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            else if (ApplicationForm.Category == "Standard Time (ST mins)")
            {
                EditApplicationDataGrid.Columns[1].Frozen = false;
                EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if (ApplicationForm.Category == "Linestop/Loss Man-hour/Loss Factor")
            {
                EditApplicationDataGrid.Columns[1].Frozen = true;
                EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }

        }

        private void SelectApplicationFormByReference_Refresh()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectApplicationFormByReference = new SqlCommand("SP_SelectApplicationFormByReference", con);
            SelectApplicationFormByReference.CommandType = CommandType.StoredProcedure;
            SelectApplicationFormByReference.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
            SelectApplicationFormByReference.Parameters.AddWithValue("@Category", ApplicationForm.Category);
            SelectApplicationFormByReference.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
            SqlDataAdapter da = new SqlDataAdapter(SelectApplicationFormByReference);
            DataTable dt = new DataTable();
            da.Fill(dt);
            EditApplicationDataGrid.DataSource = dt;
            con.Close();
        }

        private void EditApplicationDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in EditApplicationDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (ApplicationForm.ApplicationFormType == "ST")
            {
                EditApplicationDataGrid.Columns[1].Frozen = true; //Fixed column
                EditApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //align column content to center

                if (Category == "MH New ST Model List Form")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;


                    //Align centerenter column content
                    EditApplicationDataGrid.Columns["SAP ST(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["SAP TT(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["MH ST(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["MH TT(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    //ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                    //}

                }
                else
                {
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = true;

                    EditApplicationDataGrid.Columns["SAP Before ST(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["SAP Before TT(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["SAP After ST(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["SAP After TT(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["MH Before ST(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["MH Before TT(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["MH After ST(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["MH After TT(min)"].DefaultCellStyle.Format = "N3";

                    //Align centerenter column content
                    EditApplicationDataGrid.Columns["SAP Before ST(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["SAP Before TT(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["SAP After ST(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["SAP After TT(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["MH Before ST(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["MH Before TT(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["MH After ST(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    EditApplicationDataGrid.Columns["MH After TT(min)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                    //    EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                    //    EditApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                    //}
                }
            }
            else if (ApplicationForm.ApplicationFormType == "WC/CC")
            {
                EditApplicationDataGrid.Columns[1].Width = 50;
                EditApplicationDataGrid.Columns[1].Frozen = true; //Fixed column
                EditApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //align column content to center

                if (ApplicationForm.Category == "Work Center New")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;

                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}
                }
                else if (ApplicationForm.Category == "Cost Center New")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;

                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}
                }
                else if (ApplicationForm.Category == "Work Center Revision")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;

                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}


                }
                else if (ApplicationForm.Category == "Cost Center Revision")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;

                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}

                }
                else if (ApplicationForm.Category == "Work Center Deletion")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;


                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}

                }
                else if (ApplicationForm.Category == "Cost Center Deletion")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;


                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}

                }
            }
            else if (ApplicationForm.ApplicationFormType == "Open MH System")
            {
                EditApplicationDataGrid.Columns[1].Width = 50;
                EditApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (ApplicationForm.Category == "Manpower/Man-hour")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[16].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[17].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[18].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[19].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[20].ReadOnly = false;

                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[20].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}
                }
                else if (ApplicationForm.Category == "Standard Time (ST mins)")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;

                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}
                }
                else if (ApplicationForm.Category == "Linestop/Loss Man-hour/Loss Factor")
                {
                    //ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[16].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[17].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[18].ReadOnly = false;
                    EditApplicationDataGrid.Rows[e.RowIndex].Cells[19].ReadOnly = false;

                    ////Change back color of particular cell in datagrid
                    //for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    //{
                    //    EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                    //    EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //    EditApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    //}
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        private void UploadApplicationBtn_Click(object sender, EventArgs e)
        {
            

            //foreach (DataGridViewRow row in EditApplicationDataGrid.Rows)
            //{
            //    if (ApplicationForm.ApplicationFormType == "ST")
            //    {
            //        if (Category == "MH New ST Model List Form")
            //        {
            //            //if (con.State == ConnectionState.Closed)
            //            //{
            //            //    con.Open();
            //            //}
            //            //SqlCommand UpdateApplicationForm = new SqlCommand("SP_UpdateSTApplicationForm", con);
            //            //UpdateApplicationForm.CommandType = CommandType.StoredProcedure;
            //            //UpdateApplicationForm.Parameters.AddWithValue("@Category", Category);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ApplicationFormNo", row.Cells["ApplicationFormNo"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@No", row.Cells["No."].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MassProduction", row.Cells["Mass Production (Month Start)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@Plant", row.Cells["Plant"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ItemCodeSAP", row.Cells["Item Code (SAP)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ItemNameSAP", row.Cells["Item Name (SAP)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@SAPBeforeST", row.Cells["SAP Before ST(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@SAPBeforeTT", row.Cells["SAP Before TT(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@SAPAfterST", row.Cells["SAP After ST(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@SAPAfterTT", row.Cells["SAP After TT(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@SAPST", "");
            //            //UpdateApplicationForm.Parameters.AddWithValue("@SAPTT", "");
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ItemCodeMH", row.Cells["Item Code (MH)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ItemNameMH", row.Cells["Item Name (MH)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MHBeforeST", row.Cells["MH Before ST(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MHBeforeTT", row.Cells["MH Before TT(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MHAfterST", row.Cells["MH After ST(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MHAfterTT", row.Cells["MH After TT(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MHST", "");
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MHTT", "");
            //            //UpdateApplicationForm.Parameters.AddWithValue("@EffectivityDate", row.Cells["Effectivity Date"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@Reason", row.Cells["Reason"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@Remarks", row.Cells["Remarks"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
            //            //UpdateApplicationForm.Parameters.AddWithValue("@EditBy", LoginForm.FirstName + " " + LoginForm.LastName);
            //            //UpdateApplicationForm.ExecuteNonQuery();
            //            //con.Close();
            //        }
            //        else
            //        {
            //            if (con.State == ConnectionState.Closed)
            //            {
            //                con.Open();
            //            }

            //            SqlCommand UpdateApplicationForm = new SqlCommand("SP_UpdateSTApplicationForm", con);
            //            UpdateApplicationForm.CommandType = CommandType.StoredProcedure;
            //            UpdateApplicationForm.Parameters.AddWithValue("@Category", Category);
            //            UpdateApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
            //            UpdateApplicationForm.Parameters.AddWithValue("@ApplicationFormNo", row.Cells["ApplicationFormNo"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@No", row.Cells["No."].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@Section", row.Cells["Section"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MassProduction", row.Cells["Mass Production (Month Start)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@Plant", row.Cells["Plant"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ItemCodeSAP", row.Cells["Item Code (SAP)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ItemNameSAP", row.Cells["Item Name (SAP)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@SAPBeforeST", row.Cells["SAP Before ST(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@SAPBeforeTT", row.Cells["SAP Before TT(min)"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@SAPAfterST", row.Cells["SAP After ST(min)"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@SAPAfterTT", row.Cells["SAP After TT(min)"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@SAPST", "");
            //            UpdateApplicationForm.Parameters.AddWithValue("@SAPTT", "");
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ItemCodeMH", row.Cells["Item Code (MH)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@ItemNameMH", row.Cells["Item Name (MH)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MHBeforeST", row.Cells["MH Before ST(min)"].Value);
            //            //UpdateApplicationForm.Parameters.AddWithValue("@MHBeforeTT", row.Cells["MH Before TT(min)"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@MHAfterST", row.Cells["MH After ST(min)"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@MHAfterTT", row.Cells["MH After TT(min)"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@MHST", "");
            //            UpdateApplicationForm.Parameters.AddWithValue("@MHTT", "");
            //            UpdateApplicationForm.Parameters.AddWithValue("@EffectivityDate", row.Cells["Effectivity Date"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@Reason", row.Cells["Reason"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@Remarks", row.Cells["Remarks"].Value);
            //            UpdateApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
            //            UpdateApplicationForm.Parameters.AddWithValue("@EditBy", LoginForm.FirstName + " " + LoginForm.LastName);
            //            UpdateApplicationForm.ExecuteNonQuery();
            //            con.Close();

            //            SelectApplicationFormByReference();
            //        }
            //    }

            //}
        }

        //ST
        string ApplicationFormNo;
        string No;
        string Section;
        string MassProduction;
        string Plant;
        string ItemCodeSAP;
        string ItemNameSAP;
        string SAPST;
        string SAPTT;
        string SAPAfterST;
        string SAPAfterTT;
        string ItemCodeMH;
        string ItemNameMH;
        string MHST;
        string MHTT;
        string MHAfterST;
        string MHAfterTT;
        string EffectivityDate;
        string Reason;
        string Remarks;

        //WC/CC
        string WorkCenterCode;
        string WorkCenterName;
        string Shift;
        string CostCenterCode;
        string CostCenterName;
        string CostCenterGrouping;
        string Effectivity;

        string WorkCenterCode_Old;
        string WorkCenterName_Old;

        string CostCenterCode_Old;
        string CostCenterName_Old;

        string Shift_Old;
        string Plant_Old;
        string CostCenterGrouping_Old;
        string CostcenterGrouping_A;
        string CostcenterGrouping_B;

        string WorkCenterCode_New;
        string WorkCenterName_New;

        string CostCenterCode_New;
        string CostCenterName_New;

        string Shift_New;
        string Plant_New;
        string CostCenterGrouping_New;

        //Open MH - MP/MH
        string Date;
        decimal OperationTimeOld;
        decimal DirectOperatorOld;
        string DirectOperator_Old;
        decimal SemiDirectOperatorOld;
        decimal SemiIndirectOperatorOld;
        decimal TotalManpowerOld;
        decimal TotalManhourOld;
        decimal OperationTimeNew;
        decimal DirectOperatorNew;
        string DirectOperator_New;
        decimal SemiDirectOperatorNew;
        decimal SemiIndirectOperatorNew;
        decimal TotalManpowerNew;
        decimal TotalManhourNew;
        string ReasonOfRevision;
        //Open MH - Standard Time
        double OldST;
        double NewST;
        double Difference;
        //Open MH - Line Stop/ Loss MH / Loss factor
        string LinestopContentDetailOld;
        string LossFactorOld;
        string StopTimeOld;
        string SemiDirectEmployeeOld;
        string LossManhourOld;
        string LinestopContentDetailNew;
        string LossFactorNew;
        string StopTimeNew;
        string SemiDirectEmployeeNew;
        string LossManhourNew;
        string ItemCode;
        string ItemName;
        string SAPBeforeST;
        string SAPBeforeTT;
        string MHBeforeST;
        string MHBeforeTT;

        private void EditApplicationDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            if (ApplicationForm.ApplicationFormType == "ST")
            {
                if (ApplicationForm.Category == "MH New ST Model List Form")
                {
                    //Type code here...
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                    Section = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                    MassProduction = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Mass Production (Month Start)"].Value.ToString();
                    Plant = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                    ItemCodeSAP = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Item Code (SAP)"].Value.ToString();
                    ItemNameSAP = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Item Name (SAP)"].Value.ToString();
                    SAPST = EditApplicationDataGrid.Rows[e.RowIndex].Cells["SAP ST(min)"].Value.ToString();
                    SAPTT = EditApplicationDataGrid.Rows[e.RowIndex].Cells["SAP TT(min)"].Value.ToString();
                    ItemCodeMH = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Item Code (MH)"].Value.ToString();
                    ItemNameMH = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Item Name (MH)"].Value.ToString();
                    MHST = EditApplicationDataGrid.Rows[e.RowIndex].Cells["MH ST(min)"].Value.ToString();
                    MHTT = EditApplicationDataGrid.Rows[e.RowIndex].Cells["MH TT(min)"].Value.ToString();
                    EffectivityDate = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity Date"].Value.ToString();
                    Reason = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Reason"].Value.ToString();
                    Remarks = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                    //Update application
                    con.Open();
                    SqlCommand EditApplication = new SqlCommand("SP_UpdateApplicationFormData", con);
                    EditApplication.CommandType = CommandType.StoredProcedure;
                    EditApplication.Parameters.AddWithValue("@Procedure", "");
                    EditApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                    EditApplication.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                    EditApplication.Parameters.AddWithValue("@Reference", ApplicationForm.ReferenceNumber);
                    EditApplication.Parameters.AddWithValue("@ApplicationFormNo", ApplicationFormNo);
                    EditApplication.Parameters.AddWithValue("@No", No);
                    EditApplication.Parameters.AddWithValue("@Section", Section);
                    EditApplication.Parameters.AddWithValue("@MassProduction", MassProduction);
                    EditApplication.Parameters.AddWithValue("@Plant", Plant);
                    EditApplication.Parameters.AddWithValue("@ItemCodeSAP", ItemCodeSAP);
                    EditApplication.Parameters.AddWithValue("@ItemNameSAP", ItemNameSAP);
                    EditApplication.Parameters.AddWithValue("@SAPBeforeST", "");//not use, blank parameter
                    EditApplication.Parameters.AddWithValue("@SAPBeforeTT", "");//not use, blank parameter
                    EditApplication.Parameters.AddWithValue("@SAPAfterST", "");//not use, blank parameter
                    EditApplication.Parameters.AddWithValue("@SAPAfterTT", "");//not use, blank parameter
                    EditApplication.Parameters.AddWithValue("@SAPST", SAPST);
                    EditApplication.Parameters.AddWithValue("@SAPTT", SAPTT);
                    EditApplication.Parameters.AddWithValue("@ItemCodeMH", ItemCodeMH);
                    EditApplication.Parameters.AddWithValue("@ItemNameMH", ItemNameMH);
                    EditApplication.Parameters.AddWithValue("@MHBeforeST", ""); //not use, blank parameter
                    EditApplication.Parameters.AddWithValue("@MHBeforeTT", ""); //not use, blank parameter
                    EditApplication.Parameters.AddWithValue("@MHAfterST", "");//not use, blank parameter
                    EditApplication.Parameters.AddWithValue("@MHAfterTT", "");//not use, blank parameter
                    EditApplication.Parameters.AddWithValue("@MHST", MHST);
                    EditApplication.Parameters.AddWithValue("@MHTT", MHTT);
                    EditApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDate);
                    EditApplication.Parameters.AddWithValue("@Reason", Reason);
                    EditApplication.Parameters.AddWithValue("@Remarks", Remarks);
                    EditApplication.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                    EditApplication.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    EditApplication.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                    Section = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                    ItemCodeSAP = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Item Code (SAP)"].Value.ToString();
                    SAPAfterST = EditApplicationDataGrid.Rows[e.RowIndex].Cells["SAP After ST(min)"].Value.ToString();
                    SAPAfterTT = EditApplicationDataGrid.Rows[e.RowIndex].Cells["SAP After TT(min)"].Value.ToString();
                    ItemCodeMH = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Item Code (MH)"].Value.ToString();
                    MHAfterST = EditApplicationDataGrid.Rows[e.RowIndex].Cells["MH After ST(min)"].Value.ToString();
                    MHAfterTT = EditApplicationDataGrid.Rows[e.RowIndex].Cells["MH After TT(min)"].Value.ToString();
                    EffectivityDate = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity Date"].Value.ToString();
                    Reason = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Reason"].Value.ToString();
                    Remarks = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                    if (ItemCodeSAP != "")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //select SAP ST from SAP master data
                        SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
                        SelectSTItemCode.CommandType = CommandType.StoredProcedure;
                        SelectSTItemCode.Parameters.AddWithValue("@Procedure", "SAP");
                        SelectSTItemCode.Parameters.AddWithValue("@ItemCode", ItemCodeSAP);
                        SelectSTItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da2 = new SqlDataAdapter(SelectSTItemCode);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        con.Close();

                        if (dt2.Rows.Count > 0)
                        {
                            con.Open();

                            SqlDataReader reader = SelectSTItemCode.ExecuteReader();
                            if (reader.Read())
                            {
                                ItemCode = reader["ItemCodeSAP"].ToString();
                                ItemName = reader["ItemNameSAP"].ToString();
                                Plant = reader["Plant"].ToString();
                                Section = reader["Section"].ToString();
                                SAPBeforeST = reader["SAPBeforeST"].ToString();
                                SAPBeforeTT = reader["SAPBeforeTT"].ToString();
                                MassProduction = reader["MassProduction"].ToString();

                                reader.Close();
                            }

                            //Update application
                            con.Close();
                            con.Open();
                            SqlCommand EditApplication = new SqlCommand("SP_UpdateApplicationFormData", con);
                            EditApplication.CommandType = CommandType.StoredProcedure;
                            EditApplication.Parameters.AddWithValue("@Procedure", "Update SAP");
                            EditApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            EditApplication.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                            EditApplication.Parameters.AddWithValue("@Reference", ApplicationForm.ReferenceNumber);
                            EditApplication.Parameters.AddWithValue("@ApplicationFormNo", ApplicationFormNo);
                            EditApplication.Parameters.AddWithValue("@No", No);
                            EditApplication.Parameters.AddWithValue("@Section", Section);
                            EditApplication.Parameters.AddWithValue("@MassProduction", MassProduction); 
                            EditApplication.Parameters.AddWithValue("@Plant", Plant); 
                            EditApplication.Parameters.AddWithValue("@ItemCodeSAP", ItemCodeSAP);
                            EditApplication.Parameters.AddWithValue("@ItemNameSAP", ItemName);
                            EditApplication.Parameters.AddWithValue("@SAPBeforeST", SAPBeforeST);
                            EditApplication.Parameters.AddWithValue("@SAPBeforeTT", SAPBeforeTT);
                            EditApplication.Parameters.AddWithValue("@SAPAfterST", SAPAfterST);
                            EditApplication.Parameters.AddWithValue("@SAPAfterTT", SAPAfterTT);
                            EditApplication.Parameters.AddWithValue("@SAPST", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@SAPTT", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@ItemCodeMH", ItemCodeMH);
                            EditApplication.Parameters.AddWithValue("@ItemNameMH", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@MHBeforeST", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@MHBeforeTT", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@MHAfterST", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@MHAfterTT", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@MHST", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@MHTT", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDate);
                            EditApplication.Parameters.AddWithValue("@Reason", Reason);
                            EditApplication.Parameters.AddWithValue("@Remarks", Remarks);
                            EditApplication.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                            EditApplication.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            EditApplication.ExecuteNonQuery();
                            con.Close();

                        }
                        else
                        {
                            MessageBox.Show("Some of the item codes (SAP) do not belong to your section, Please check your file before uploading again.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }


                    if (ItemCodeMH != "")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //select MH ST from SAP master data
                        SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
                        SelectSTItemCode.CommandType = CommandType.StoredProcedure;
                        SelectSTItemCode.Parameters.AddWithValue("@Procedure", "MH");
                        SelectSTItemCode.Parameters.AddWithValue("@ItemCode", ItemCodeMH);
                        SelectSTItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da3 = new SqlDataAdapter(SelectSTItemCode);
                        DataTable dt3 = new DataTable();
                        da3.Fill(dt3);
                        con.Close();

                        if (dt3.Rows.Count > 0)
                        {
                            con.Open();

                            SqlDataReader reader = SelectSTItemCode.ExecuteReader();
                            if (reader.Read())
                            {
                                ItemCode = reader["ItemCodeMH"].ToString();
                                ItemName = reader["ItemNameMH"].ToString();
                                Plant = reader["Plant"].ToString();
                                Section = reader["Section"].ToString();
                                MHBeforeST = reader["MHBeforeST"].ToString();
                                MHBeforeTT = reader["MHBeforeTT"].ToString();
                                MassProduction = reader["MassProduction"].ToString();

                                reader.Close();
                            }

                            //Update application
                            con.Close();
                            con.Open();
                            SqlCommand EditApplication = new SqlCommand("SP_UpdateApplicationFormData", con);
                            EditApplication.CommandType = CommandType.StoredProcedure;
                            EditApplication.Parameters.AddWithValue("@Procedure", "Update MH");
                            EditApplication.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                            EditApplication.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                            EditApplication.Parameters.AddWithValue("@Reference", ApplicationForm.ReferenceNumber);
                            EditApplication.Parameters.AddWithValue("@ApplicationFormNo", ApplicationFormNo);
                            EditApplication.Parameters.AddWithValue("@No", No);
                            EditApplication.Parameters.AddWithValue("@Section", Section);
                            EditApplication.Parameters.AddWithValue("@MassProduction", MassProduction);
                            EditApplication.Parameters.AddWithValue("@Plant", Plant); 
                            EditApplication.Parameters.AddWithValue("@ItemCodeSAP", ItemCodeSAP);
                            EditApplication.Parameters.AddWithValue("@ItemNameSAP", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@SAPBeforeST", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@SAPBeforeTT", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@SAPAfterST", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@SAPAfterTT", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@SAPST", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@SAPTT", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@ItemCodeMH", ItemCodeMH);
                            EditApplication.Parameters.AddWithValue("@ItemNameMH", ItemName); 
                            EditApplication.Parameters.AddWithValue("@MHBeforeST", MHBeforeST); 
                            EditApplication.Parameters.AddWithValue("@MHBeforeTT", MHBeforeTT); 
                            EditApplication.Parameters.AddWithValue("@MHAfterST", MHAfterST);
                            EditApplication.Parameters.AddWithValue("@MHAfterTT", MHAfterTT);
                            EditApplication.Parameters.AddWithValue("@MHST", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@MHTT", ""); //not use, blank parameter
                            EditApplication.Parameters.AddWithValue("@EffectivityDate", EffectivityDate);
                            EditApplication.Parameters.AddWithValue("@Reason", Reason);
                            EditApplication.Parameters.AddWithValue("@Remarks", Remarks);
                            EditApplication.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                            EditApplication.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            EditApplication.ExecuteNonQuery();
                            con.Close();


                        }
                        else
                        {
                            MessageBox.Show("Some of the item codes (SAP) do not belong to your section, Please check your file before uploading again.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }

                }
            }
            else if (ApplicationForm.ApplicationFormType == "WC/CC")
            {
                if (ApplicationForm.Category == "Work Center New")
                {
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                    Section = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                    WorkCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code"].Value.ToString();
                    WorkCenterName = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name"].Value.ToString();
                    Shift = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"].Value.ToString();
                    CostCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code"].Value.ToString();
                    CostCenterName = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name"].Value.ToString();
                    CostCenterGrouping = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping"].Value.ToString();
                    Plant = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                    Effectivity = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                    Remarks = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                    //Update application
                    con.Open();
                    SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCCCNewApplicationForm", con);
                    FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit WC New");
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostCenterGrouping);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                    FillOutWCCCApplicationForm.ExecuteNonQuery();
                    con.Close();

                }
                else if (ApplicationForm.Category == "Cost Center New")
                {
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                    Section = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                    CostCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code"].Value.ToString();
                    CostCenterName = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name"].Value.ToString();
                    Plant = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                    WorkCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code"].Value.ToString();
                    WorkCenterName = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name"].Value.ToString();
                    Shift = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"].Value.ToString();
                    CostCenterGrouping = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping"].Value.ToString();
                    Effectivity = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                    Remarks = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                    //Update application
                    con.Open();
                    SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCCCNewApplicationForm", con);
                    FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit WC New");
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostCenterGrouping);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                    FillOutWCCCApplicationForm.ExecuteNonQuery();
                    con.Close();
                }
                else if (ApplicationForm.Category == "Work Center Revision")
                {

                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                    Section = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();

                    WorkCenterCode_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code (Old)"].Value.ToString();
                    WorkCenterName_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name (Old)"].Value.ToString();
                    Shift_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift (Old)"].Value.ToString();
                    Plant_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant (Old)"].Value.ToString();
                    CostCenterGrouping_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping (Old)"].Value.ToString();

                    WorkCenterCode_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code (New)"].Value.ToString();
                    WorkCenterName_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name (New)"].Value.ToString();
                    Shift_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift (New)"].Value.ToString();
                    Plant_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant (New)"].Value.ToString();
                    CostCenterGrouping_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping (New)"].Value.ToString();

                    Effectivity = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                    Remarks = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                    if (WorkCenterCode_Old != "" && Shift_Old != "")
                    {
                        if ((Shift_Old != "B") && (Shift_Old != "Y"))
                        {
                            MessageBox.Show("Shift must be letter 'B' or 'Y' only.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select SAP ST from SAP master data
                            SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectWorkCenterCodeFromWCMasterData", con);
                            SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                            SelectWorkcenter.Parameters.AddWithValue("@Procedure", "WorkCenter");
                            SelectWorkcenter.Parameters.AddWithValue("@WorcenterCode", WorkCenterCode_Old);
                            SelectWorkcenter.Parameters.AddWithValue("@Shift", Shift_Old);
                            SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            con.Close();

                            if (dt2.Rows.Count > 0)
                            {
                                con.Open();

                                SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                                if (reader.Read())
                                {
                                    WorkCenterCode_Old = reader["WorkCenterCode"].ToString();
                                    WorkCenterName_Old = reader["WorkCenterName"].ToString();
                                    Plant_Old = reader["Plant"].ToString();
                                    CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                    CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                    reader.Close();
                                }


                                //Update application
                                con.Close();
                                con.Open();
                                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCRevisionApplicationForm", con);
                                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit WC Revision");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode_Old", WorkCenterCode_Old);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName_Old", WorkCenterName_Old);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_Old", Shift_Old);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_Old", Plant_Old);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_Old", CostcenterGrouping_A + " " + CostcenterGrouping_B);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode_New", WorkCenterCode_New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName_New", WorkCenterName_New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_New", Shift_New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_New", Plant_New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_New", CostCenterGrouping_New);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                                FillOutWCCCApplicationForm.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Workcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                }
                else if (ApplicationForm.Category == "Cost Center Revision")
                {
                    
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                    CostCenterCode_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code (Old)"].Value.ToString();
                    CostCenterName_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name (Old)"].Value.ToString();

                    Shift_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift (Old)"].Value.ToString();
                    Plant_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant (Old)"].Value.ToString();
                    CostCenterGrouping_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping (Old)"].Value.ToString();

                    CostCenterCode_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code (New)"].Value.ToString();
                    CostCenterName_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name (New)"].Value.ToString();
                    Shift_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift (New)"].Value.ToString();
                    Plant_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant (New)"].Value.ToString();
                    CostCenterGrouping_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping (New)"].Value.ToString();

                    Effectivity = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                    Remarks = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();

                    if (CostCenterCode_Old != "" && Shift_Old != "")
                    {
                        if ((Shift_Old != "B") && (Shift_Old != "Y"))
                        {
                            MessageBox.Show("Shift must be letter 'B' or 'Y' only.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select SAP ST from SAP master data
                            SqlCommand SelectCostcenter = new SqlCommand("SP_SelectCostCenterCodeFromWCMasterData", con);
                            SelectCostcenter.CommandType = CommandType.StoredProcedure;
                            SelectCostcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                            SelectCostcenter.Parameters.AddWithValue("@CostcenterCode", CostCenterCode_Old);
                            //SelectCostcenter.Parameters.AddWithValue("@Shift", ShiftOld);
                            SelectCostcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da2 = new SqlDataAdapter(SelectCostcenter);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            con.Close();

                            if (dt2.Rows.Count > 0)
                            {
                                con.Open();

                                SqlDataReader reader = SelectCostcenter.ExecuteReader();
                                if (reader.Read())
                                {
                                    CostCenterCode_Old = reader["CostCenterCode"].ToString();
                                    CostCenterName_Old = reader["CostCenterName"].ToString();
                                    Plant_Old = reader["Plant"].ToString();
                                    CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                    CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                    reader.Close();
                                }

                                //Update application
                                con.Close();
                                con.Open();
                                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateCostCenterRevisionApplicationForm", con);
                                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit CC Revision");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode_Old", CostCenterCode_Old);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName_Old", CostCenterName_Old);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_Old", Shift_Old);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_Old", Plant_Old);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_Old", CostcenterGrouping_A + " and " + CostcenterGrouping_B);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode_New", CostCenterCode_New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName_New", CostCenterName_New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift_New", Shift_New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant_New", Plant_New);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping_New", CostCenterGrouping_New);

                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                                FillOutWCCCApplicationForm.ExecuteNonQuery();
                                con.Close();

                            }
                            else
                            {
                                MessageBox.Show("Costcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                }
                else if (ApplicationForm.Category == "Work Center Deletion")
                {
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                    Section = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Section"].Value.ToString();
                    WorkCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code"].Value.ToString();
                    WorkCenterName = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Name"].Value.ToString();
                    Shift = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"].Value.ToString();
                    Plant = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                    CostCenterGrouping = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping"].Value.ToString();
                    Effectivity = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                    Remarks = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();


                    if (WorkCenterCode != "" && Shift != "")
                    {
                        if ((Shift != "B") && (Shift != "Y"))
                        {
                            MessageBox.Show("Shift must be letter 'B' or 'Y' only.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            //select SAP ST from SAP master data
                            SqlCommand SelectWorkcenter = new SqlCommand("SP_SelectWorkCenterCodeFromWCMasterData", con);
                            SelectWorkcenter.CommandType = CommandType.StoredProcedure;
                            SelectWorkcenter.Parameters.AddWithValue("@Procedure", "WorkCenter");
                            SelectWorkcenter.Parameters.AddWithValue("@WorcenterCode", WorkCenterCode);
                            SelectWorkcenter.Parameters.AddWithValue("@Shift", Shift);
                            SelectWorkcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da2 = new SqlDataAdapter(SelectWorkcenter);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            con.Close();

                            if (dt2.Rows.Count > 0)
                            {
                                con.Open();

                                SqlDataReader reader = SelectWorkcenter.ExecuteReader();
                                if (reader.Read())
                                {
                                    WorkCenterName = reader["WorkCenterName"].ToString();
                                    Shift = reader["Shift"].ToString();
                                    Plant = reader["Plant"].ToString();
                                    CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                    CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                    reader.Close();
                                }

                                //Update application
                                con.Close();
                                con.Open();
                                SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateWCDeletionApplicationForm", con);
                                FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit WC Deletion");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterName", WorkCenterName);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostcenterGrouping_A + " and " + CostcenterGrouping_B);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                                FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                                FillOutWCCCApplicationForm.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Workcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    
                }
                else if (ApplicationForm.Category == "Cost Center Deletion")
                {
                    //Type code here...
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();
                    CostCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code"].Value.ToString();
                    CostCenterName = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Name"].Value.ToString();
                    CostCenterGrouping = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Grouping"].Value.ToString();
                    Plant = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Plant"].Value.ToString();
                    Effectivity = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Effectivity"].Value.ToString();
                    Remarks = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();


                    if (CostCenterCode != "")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //select SAP ST from SAP master data
                        SqlCommand SelectCostcenter = new SqlCommand("SP_SelectCostCenterCodeFromWCMasterData", con);
                        SelectCostcenter.CommandType = CommandType.StoredProcedure;
                        SelectCostcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                        SelectCostcenter.Parameters.AddWithValue("@CostcenterCode", CostCenterCode);
                        SelectCostcenter.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da2 = new SqlDataAdapter(SelectCostcenter);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        con.Close();

                        if (dt2.Rows.Count > 0)
                        {
                            con.Open();

                            SqlDataReader reader = SelectCostcenter.ExecuteReader();
                            if (reader.Read())
                            {
                                CostCenterName = reader["CostCenterName"].ToString();
                                Plant = reader["Plant"].ToString();
                                CostcenterGrouping_A = reader["CostCenterGrouping_A"].ToString();
                                CostcenterGrouping_B = reader["CostCenterGrouping_B"].ToString();

                                reader.Close();
                            }

                            //Update application
                            con.Close();
                            con.Open();
                            SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateCostCenterDeletionApplicationForm", con);
                            FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit CC Deletion");
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterName", CostCenterName);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Plant", Plant);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterGrouping", CostcenterGrouping_A + " and " + CostcenterGrouping_B);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Effectivity", Effectivity);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Remarks", Remarks);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                            FillOutWCCCApplicationForm.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Costcenter code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                }

            }
            else if (ApplicationForm.ApplicationFormType == "Open MH System")
            {
                if (ApplicationForm.Category == "Manpower/Man-hour")
                {
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();

                    Date = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Date"].Value.ToString();
                    Category = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString();
                    WorkCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Work Center Code"].Value.ToString();
                    CostCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Cost Center Code"].Value.ToString();
                    Shift = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"].Value.ToString();

                    OperationTimeOld = Convert.ToDecimal(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Operation Time (Old)"].Value.ToString());
                    DirectOperatorOld = Convert.ToDecimal(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (Old)"].Value.ToString());
                    SemiDirectOperatorOld = Convert.ToDecimal(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Operator (Old)"].Value.ToString());
                    SemiIndirectOperatorOld = Convert.ToDecimal(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-Indirect Operator (Old)"].Value.ToString());
                    TotalManpowerOld = DirectOperatorOld + SemiDirectOperatorOld + SemiIndirectOperatorOld;
                    TotalManhourOld = Math.Round(Convert.ToDecimal((OperationTimeOld / 60) * TotalManpowerOld), 2);

                    OperationTimeNew = Convert.ToDecimal(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Operation Time (New)"].Value.ToString());
                    DirectOperatorNew = Convert.ToDecimal(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (New)"].Value.ToString());
                    SemiDirectOperatorNew = Convert.ToDecimal(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Operator (New)"].Value.ToString());
                    SemiIndirectOperatorNew = Convert.ToDecimal(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-Indirect Operator (New)"].Value.ToString());
                    TotalManpowerNew =  DirectOperatorNew + SemiDirectOperatorNew + SemiIndirectOperatorNew;
                    TotalManhourNew = Math.Round(Convert.ToDecimal((OperationTimeNew / 60) * TotalManpowerNew), 2);

                    ReasonOfRevision = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Reason of Revision"].Value.ToString();

                    //Update application
                    con.Open();
                    SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateManpowerManhourApplicationForm", con);
                    FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit ManpowerManhour");
                    //FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Date", Date);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", Category);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@OperationTimeOld", OperationTimeOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DirectOperatorOld", DirectOperatorOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiDirectOperatorOld", SemiDirectOperatorOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiIndirectOperatorOld", SemiIndirectOperatorOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@TotalManpowerOld", TotalManpowerOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@TotalManhourOld", TotalManhourOld);

                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@OperationTimeNew", OperationTimeNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DirectOperatorNew", DirectOperatorNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiDirectOperatorNew", SemiDirectOperatorNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiIndirectOperatorNew", SemiIndirectOperatorNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@TotalManpowerNew", TotalManpowerNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@TotalManhourNew", TotalManhourNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReasonOfRevision", ReasonOfRevision);

                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                    FillOutWCCCApplicationForm.ExecuteNonQuery();
                    con.Close();
                }
                else if (ApplicationForm.Category == "Standard Time (ST mins)")
                {
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();

                    Date = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Date"].Value.ToString();
                    WorkCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Work Center"].Value.ToString();
                    CostCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Cost Center"].Value.ToString();
                    Shift = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"].Value.ToString();

                    ItemCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Item Code"].Value.ToString();

                    if (EditApplicationDataGrid.Rows[e.RowIndex].Cells["Old"].Value.ToString() == "")
                    {
                        OldST = 0;
                    }
                    else
                    {
                        OldST = Convert.ToDouble(EditApplicationDataGrid.Rows[e.RowIndex].Cells["Old"].Value);
                    }

                    if (EditApplicationDataGrid.Rows[e.RowIndex].Cells["New"].Value.ToString() == "")
                    {
                        NewST = 0;
                    }
                    else
                    {
                        NewST = Convert.ToDouble(EditApplicationDataGrid.Rows[e.RowIndex].Cells["New"].Value);
                    }

                    ReasonOfRevision = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Reason of Revision"].Value.ToString();


                    if (ItemCode != "")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //select SAP ST from SAP master data
                        SqlCommand SelectItemCode = new SqlCommand("SP_SelectItemCodeFromOpemMHMasterData", con);
                        SelectItemCode.CommandType = CommandType.StoredProcedure;
                        //SelectCostcenter.Parameters.AddWithValue("@Procedure", "CostCenter");
                        SelectItemCode.Parameters.AddWithValue("@ItemCode", ItemCode);
                        SelectItemCode.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da2 = new SqlDataAdapter(SelectItemCode);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        con.Close();

                        if (dt2.Rows.Count > 0)
                        {
                            con.Open();

                            SqlDataReader reader = SelectItemCode.ExecuteReader();
                            if (reader.Read())
                            {
                                //ItemCode = reader["ItemCode"].ToString();
                                OldST = Convert.ToDouble(reader["OldST"].ToString());

                                reader.Close();
                            }

                            Difference = OldST - NewST; //Get Difference

                            //Update application
                            con.Close();
                            con.Open();
                            SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateStandardTimeApplicationForm", con);
                            FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit Standard Time");
                            //FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Date", Date);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@ItemCode", ItemCode);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Old", OldST);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@New", NewST);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@Difference", Difference);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReasonOfRevision", ReasonOfRevision);

                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                            FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                            FillOutWCCCApplicationForm.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Item code is not existing in master data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                   
                }
                else if (ApplicationForm.Category == "Linestop/Loss Man-hour/Loss Factor")
                {
                    ApplicationFormNo = EditApplicationDataGrid.Rows[e.RowIndex].Cells["ApplicationFormNo"].Value.ToString();
                    No = EditApplicationDataGrid.Rows[e.RowIndex].Cells["No."].Value.ToString();

                    Date = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Date"].Value.ToString();
                    WorkCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Workcenter Code"].Value.ToString();
                    CostCenterCode = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Costcenter Code"].Value.ToString();
                    Shift = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Shift"].Value.ToString();

                    LinestopContentDetailOld = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Line Stop Content Detail (Old)"].Value.ToString();
                    LossFactorOld = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Loss Factor (Old)"].Value.ToString();
                    StopTimeOld = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Stop Time (Old)"].Value.ToString();
                    DirectOperator_Old = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (Old)"].Value.ToString();
                    SemiDirectEmployeeOld = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Employee (Old)"].Value.ToString();
                    LossManhourOld = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Loss Manhour (Old)"].Value.ToString();

                    LinestopContentDetailNew = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Line Stop Content Detail (New)"].Value.ToString();
                    LossFactorNew = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Loss Factor (New)"].Value.ToString();
                    StopTimeNew = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Stop Time (New)"].Value.ToString();
                    DirectOperator_New = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Direct Operator (New)"].Value.ToString();
                    SemiDirectEmployeeNew = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Semi-direct Employee (New)"].Value.ToString();
                    LossManhourNew = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Loss Manhour (New)"].Value.ToString();

                    ReasonOfRevision = EditApplicationDataGrid.Rows[e.RowIndex].Cells["Reason of Revision"].Value.ToString();


                    //Update application
                    con.Open();
                    SqlCommand FillOutWCCCApplicationForm = new SqlCommand("SP_UpdateLinestop_LossManhour_LossFactorApplicationForm", con);
                    FillOutWCCCApplicationForm.CommandType = CommandType.StoredProcedure;
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Procedure", "Edit Linestop_LossManhour_LossFactor");
                    //FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", ApplicationForm.Category);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ApplicationformNo", ApplicationFormNo);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@No", No);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Date", Date);
                    //FillOutWCCCApplicationForm.Parameters.AddWithValue("@Category", Category);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@CostCenterCode", CostCenterCode);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@WorkCenterCode", WorkCenterCode);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@Shift", Shift);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@LinestopContentDetailOld", LinestopContentDetailOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@LossFactorOld", LossFactorOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@StopTimeOld", StopTimeOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DirectOperatorOld", DirectOperator_Old);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiDirectEmployeeOld", SemiDirectEmployeeOld);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@LossManhourOld", LossManhourOld);

                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@LinestopContentDetailNew", LinestopContentDetailNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@LossFactorNew", LossFactorNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@StopTimeNew", StopTimeNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DirectOperatorNew", DirectOperator_New);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@SemiDirectEmployeeNew", SemiDirectEmployeeNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@LossManhourNew", LossManhourNew);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@ReasonOfRevision", ReasonOfRevision);

                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateEdited", DateTime.Now.ToString());
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@EditedBy", LoginForm.FirstName + " " + LoginForm.LastName);
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@DateApplied", "");
                    FillOutWCCCApplicationForm.Parameters.AddWithValue("@AppliedBy", "");
                    FillOutWCCCApplicationForm.ExecuteNonQuery();
                    con.Close();
                }
            }


        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            //SelectApplicationFormByReference_Refresh();

            SelectApplicationFormByReference();

            EditApplicationDataGrid.Columns["ApplicationFormNo"].Visible = false;

            if (ApplicationForm.ApplicationFormType == "ST")
            {
                if (Category == "MH New ST Model List Form")
                {
                    EditApplicationDataGrid.Columns[1].Frozen = false;
                    EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    EditApplicationDataGrid.Columns[1].Frozen = true;
                    EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
            else if (ApplicationForm.ApplicationFormType == "WC/CC")
            {
                EditApplicationDataGrid.Columns[1].Frozen = false;
                EditApplicationDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            SearchBox.Clear();
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SearchApplicationFormByReference = new SqlCommand("SP_SearchApplicationFormByReference", con);
                SearchApplicationFormByReference.CommandType = CommandType.StoredProcedure;
                SearchApplicationFormByReference.Parameters.AddWithValue("@ApplicationFormType", ApplicationForm.ApplicationFormType);
                SearchApplicationFormByReference.Parameters.AddWithValue("@Category", Category);
                SearchApplicationFormByReference.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                SearchApplicationFormByReference.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter da = new SqlDataAdapter(SearchApplicationFormByReference);
                DataTable dt = new DataTable();
                da.Fill(dt);
                EditApplicationDataGrid.DataSource = dt;
                con.Close();

                EditApplicationDataGrid.Columns[0].Visible = false;

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void EditApplicationDataGrid_MouseEnter(object sender, EventArgs e)
        {
             Cursor = Cursors.Hand;
        }

        private void EditApplicationDataGrid_MouseLeave(object sender, EventArgs e)
        {
             Cursor = Cursors.Default;
        }

        private void EditApplicationDataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (ApplicationForm.ApplicationFormType == "ST")
            {
                EditApplicationDataGrid.Columns[1].Frozen = true; //Fixed column
                EditApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //align column content to center

                if (Category == "MH New ST Model List Form")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;

                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        //ApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                    }




                }
                else
                {
                    EditApplicationDataGrid.Columns["SAP Before ST(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["SAP Before TT(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["SAP After ST(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["SAP After TT(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["MH Before ST(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["MH Before TT(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["MH After ST(min)"].DefaultCellStyle.Format = "N3";
                    EditApplicationDataGrid.Columns["MH After TT(min)"].DefaultCellStyle.Format = "N3";


                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = true;


                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                        EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(230, 230, 230); //gray

                        EditApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow

                    }


                }

            }
            else if (ApplicationForm.ApplicationFormType == "WC/CC")
            {
                EditApplicationDataGrid.Columns[1].Width = 50;
                EditApplicationDataGrid.Columns[1].Frozen = true; //Fixed column
                EditApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //align column content to center

                if (ApplicationForm.Category == "Work Center New")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;

                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }
                }
                else if (ApplicationForm.Category == "Cost Center New")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;

                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }
                }
                else if (ApplicationForm.Category == "Work Center Revision")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;

                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }


                }
                else if (ApplicationForm.Category == "Cost Center Revision")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;

                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }

                }
                else if (ApplicationForm.Category == "Work Center Deletion")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;


                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }

                }
                else if (ApplicationForm.Category == "Cost Center Deletion")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;


                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }

                }
            }
            else if (ApplicationForm.ApplicationFormType == "Open MH System")
            {
                EditApplicationDataGrid.Columns[1].Width = 50;
                EditApplicationDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (ApplicationForm.Category == "Manpower/Man-hour")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[16].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[17].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[18].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[19].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[20].ReadOnly = false;

                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[20].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }
                }
                else if (ApplicationForm.Category == "Standard Time (ST mins)")
                {
                    //////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;

                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }
                }
                else if (ApplicationForm.Category == "Linestop/Loss Man-hour/Loss Factor")
                {
                    ////ApplicationDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[15].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[16].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[17].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[18].ReadOnly = false;
                    //EditApplicationDataGrid.Rows[e.RowIndex].Cells[19].ReadOnly = false;

                    //Change back color of particular cell in datagrid
                    for (int i = 0; i < EditApplicationDataGrid.Rows.Count; i++)
                    {
                        EditApplicationDataGrid.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(230, 230, 230); //gray
                        EditApplicationDataGrid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[6].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[8].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[9].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[10].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[11].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[12].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[13].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[14].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[15].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[17].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                        EditApplicationDataGrid.Rows[i].Cells[19].Style.BackColor = Color.FromArgb(255, 242, 198); //Light Yellow
                    }
                }
            }

        }














    }
}
