using MHMS.Connection;
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
    public partial class EditApprovalCategory : Form
    {
        // SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        public EditApprovalCategory()
        {
            InitializeComponent();
        }

        private void EditApprovalCategory_Load(object sender, EventArgs e)
        {

            LoadSelectedUser();

        }

        private void LoadSelectedUser()
        {
            if (UserSetting.MHPIC_Value == "✔️")
            {
                MHPICCheckBox.Checked = true;
            }
            else
            {
                MHPICCheckBox.Checked = false;
            }

            if (UserSetting.COPQPIC_Value == "✔️")
            {
                COPQPICCheckBox.Checked = true;
            }
            else
            {
                COPQPICCheckBox.Checked = false;
            }

            if (UserSetting.PCPIC_Value == "✔️")
            {
                PCPICCheckBox.Checked = true;
            }
            else
            {
                PCPICCheckBox.Checked = false;
            }

            if (UserSetting.FactoryEfficiency_Value == "✔️")
            {
                FactoryEfficiencyCheckBox.Checked = true;
            }
            else
            {
                FactoryEfficiencyCheckBox.Checked = false;
            }

            if (UserSetting.Supervisor_Value == "✔️")
            {
                SupervisorCheckBox.Checked = true;
            }
            else
            {
                SupervisorCheckBox.Checked = false;
            }

            if (UserSetting.Manager_Value == "✔️")
            {
                ManagerCheckBox.Checked = true;
            }
            else
            {
                ManagerCheckBox.Checked = false;
            }

            if (UserSetting.GeneralManager_Value == "✔️")
            {
                GeneralMangerCheckBox.Checked = true;
            }
            else
            {
                GeneralMangerCheckBox.Checked = false;
            }

            if (UserSetting.BILSupport_Value == "✔️")
            {
                BILSupportCheckBox.Checked = true;
            }
            else
            {
                BILSupportCheckBox.Checked = false;
            }

            if (UserSetting.COPQProcessInCharge_Value == "✔️")
            {
                COPQProcessInChargeCheckBox.Checked = true;
            }
            else
            {
                COPQProcessInChargeCheckBox.Checked = false;
            }

        }

        string _MHPIC_Value = "";
        string _COPQPIC_Value = "";
        string _PCPIC_Value = "";
        string _Supervisor_Value = "";
        string _Manager_Value = "";
        string _GeneralManager_Value = "";
        string _BILSupport_Value = "";
        string _COPQProcessInCharge_Value = "";
        string _FactoryEfficiency_Value = "";

        private void SaveUpdateBtn_Click(object sender, EventArgs e)
        {
            if (MHPICCheckBox.Checked)
            {
                _MHPIC_Value = "✔️";
            }
            else
            {
                _MHPIC_Value = "";
            }

            if (COPQPICCheckBox.Checked)
            {
                _COPQPIC_Value = "✔️";
            }
            else
            {
                _COPQPIC_Value = "";
            }

            if (PCPICCheckBox.Checked)
            {
                _PCPIC_Value = "✔️";
            }
            else
            {
                _PCPIC_Value = "";
            }

            if (SupervisorCheckBox.Checked)
            {
                _Supervisor_Value = "✔️";
            }
            else
            {
                _Supervisor_Value = "";
            }

            if (ManagerCheckBox.Checked)
            {
                _Manager_Value = "✔️";
            }
            else
            {
                _Manager_Value = "";
            }

            if (GeneralMangerCheckBox.Checked)
            {
                _GeneralManager_Value = "✔️";
            }
            else
            {
                _GeneralManager_Value = "";
            }

            if (BILSupportCheckBox.Checked)
            {
                _BILSupport_Value = "✔️";
            }
            else
            {
                _BILSupport_Value = "";
            }

            if (COPQProcessInChargeCheckBox.Checked)
            {
                _COPQProcessInCharge_Value = "✔️";
            }
            else
            {
                _COPQProcessInCharge_Value = "";
            }

            if (FactoryEfficiencyCheckBox.Checked)
            {
                _FactoryEfficiency_Value = "✔️";
            }
            else
            {
                _FactoryEfficiency_Value = "";
            }

            // SQL query to update a record
            string updateQuery = "UPDATE [UserAccount] SET [MH PIC] = @MHPIC, [COPQ PIC] = @COPQPIC, [PC PIC] = @PCPIC, [FE PIC] = @FEPIC, [Supervisor] = @SPV, [Manager] = @MGR,[General Manager] = @GEN_MGR, [BIL Support] = @BILSupport, [COPQ Process In-charge] = @COPQProcessIncharge WHERE ADID = @ADID AND Section = @Section";

            try
            {
                con.Open();

                // Create a command with the SQL query and connection
                using (SqlCommand command = new SqlCommand(updateQuery, con))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@ADID", UserSetting.UserID);
                    command.Parameters.AddWithValue("@Section", UserSetting._Section);
                    command.Parameters.AddWithValue("@MHPIC", _MHPIC_Value);
                    command.Parameters.AddWithValue("@COPQPIC", _COPQPIC_Value);
                    command.Parameters.AddWithValue("@PCPIC", _PCPIC_Value);
                    command.Parameters.AddWithValue("@FEPIC", _FactoryEfficiency_Value);
                    command.Parameters.AddWithValue("@SPV", _Supervisor_Value);
                    command.Parameters.AddWithValue("@MGR", _Manager_Value);
                    command.Parameters.AddWithValue("@GEN_MGR", _GeneralManager_Value);
                    command.Parameters.AddWithValue("@BILSupport", _BILSupport_Value);
                    command.Parameters.AddWithValue("@COPQProcessIncharge", _COPQProcessInCharge_Value);

                    // Execute the update query
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check how many rows were updated
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update successful!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        UserSetting.DoneEditing = true;

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                MessageBox.Show("An error occurred: " + ex.Message);

            }


        }
    }
}
