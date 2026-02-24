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
    public partial class ItemCodeForm : Form
    {
        //Connection String
        static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS2_Conn);

        public ItemCodeForm()
        {
            InitializeComponent();
        }

        private void ItemCodeForm_Load(object sender, EventArgs e)
        {
            ApplicationNoLabel.Text = "Application No. " + Forms.ApplicationForm.No;
            ItemCodeType.Text = ApplicationForm.ItemCodeType;
            ItemCodeTextBox.Select();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string ItemCode;
        string Plant;
        private void AutoFillBtn_Click(object sender, EventArgs e)
        {
            if (ItemCodeTextBox.Text == "")
            {
                MessageBox.Show("Please input the item code.", "Notification!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // -> SQL query to select parts loss data setting
                SqlCommand SelectSTItemCode = new SqlCommand("SP_SelectSTItemCodeFromMasterData", con);
                SelectSTItemCode.CommandType = CommandType.StoredProcedure;
                SelectSTItemCode.Parameters.AddWithValue("@ItemCode", ItemCodeTextBox.Text);
                SqlDataAdapter da = new SqlDataAdapter(SelectSTItemCode);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    SqlDataReader reader = SelectSTItemCode.ExecuteReader();
                    if (reader.Read())
                    {
                        ItemCode = reader["ItemCode"].ToString();
                        Plant = reader["Plant"].ToString();

                        reader.Close();
                    }
                }

                SqlCommand AutoFillSTApplication = new SqlCommand("SP_UpdateSTApplication", con);
                AutoFillSTApplication.CommandType = CommandType.StoredProcedure;
                AutoFillSTApplication.Parameters.AddWithValue("@STcategory", ApplicationForm.STCategory);
                AutoFillSTApplication.Parameters.AddWithValue("@ItemCode", ItemCode);
                AutoFillSTApplication.Parameters.AddWithValue("@Plant", Plant);
                AutoFillSTApplication.Parameters.AddWithValue("@No", ApplicationForm.No);
                AutoFillSTApplication.ExecuteNonQuery();
                con.Close();


                COPQPartsLossForm.HaveNewUploadedData = true;

                this.Close();
            }

           

        }

        private void DownloadUploadPanel_Click(object sender, EventArgs e)
        {

        }
    }
}
