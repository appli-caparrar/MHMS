using MHMS.Connection;
using MHMS.Forms;
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
    public partial class FactoryEfficiencyMGRCommentForm : Form
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);

        public FactoryEfficiencyMGRCommentForm()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveCommentBtn_Click(object sender, EventArgs e)
        {
            if (ReasonText.Text == "")
            {
                MessageBox.Show("Please input the reason.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (CountermeasureText.Text == "")
            {
                MessageBox.Show("Please input the countermeasure or improvement activities.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand InsertFEComment = new SqlCommand("SP_InsertFEComment", con);
                    InsertFEComment.CommandType = CommandType.StoredProcedure;
                    InsertFEComment.Parameters.AddWithValue("@Department", FactoryEfficiencyForm.Dept);
                    InsertFEComment.Parameters.AddWithValue("@Section", FactoryEfficiencyForm.Section);
                    InsertFEComment.Parameters.AddWithValue("@Month", DateTime.Now.AddMonths(-1).ToString("MMM")); //previous month
                    InsertFEComment.Parameters.AddWithValue("@FiscalYear", FactoryEfficiencyForm.FiscalYear);
                    InsertFEComment.Parameters.AddWithValue("@ResultType", FactoryEfficiencyForm.ResultType);
                    InsertFEComment.Parameters.AddWithValue("@Reason", ReasonText.Text);
                    InsertFEComment.Parameters.AddWithValue("@Countermeasure", CountermeasureText.Text);

                    InsertFEComment.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Saved successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FactoryEfficiencyForm.IsCommentSave = true;

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FactoryEfficiencyMGRCommentForm_Load(object sender, EventArgs e)
        {
          
        }

        
    }
}
