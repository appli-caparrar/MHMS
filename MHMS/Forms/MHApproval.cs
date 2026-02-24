using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using MHMS.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace MHMS.Forms
{
    public partial class MHApproval : Form
    {
        //Connection String
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;
        //static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn);


        public MHApproval()
        {
            InitializeComponent();
        }



        private void MHApproval_Load(object sender, EventArgs e)
        {
            AddCheckedBoxColumn();

            ApprovalDataGrid.Columns[0].Width = 80;

            if (Dashboard.AccountType == "ADMIN")
            {
                AllPendingAppBtn.Visible = true;
            }

            AddYears();
        }

        private void AddYears()
        {
            var currentYear = DateTime.Today.Year;
            for (int i = 3; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                yearCbx.Items.Add((currentYear - i).ToString());
            }
        }

        private void AddApproverDropdownList()
        {
            if (ApplicationTypeDropdown.Text == "ST")
            {

                if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                {
                    if ((LoginForm.SectionMGR == "✔️" && LoginForm.SectionGeneralMGR != "✔️") || (SectionMenuForm.SectionMGR == "✔️" && SectionMenuForm.SectionGeneralMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("BPS MGR");

                        
                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("BPS MGR");

                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR == "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR == "✔️"))
                    {

                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "";

                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR != "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "Section GM";
                    }
                    //else
                    //{
                    //    ApproverDropDown.Items.Add("Section MGR");
                    //    ApproverDropDown.Text = "Section MGR";
                    //}
                }
                else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                {
                    if ((LoginForm.SectionMGR == "✔️" && LoginForm.SectionGeneralMGR != "✔️") || (SectionMenuForm.SectionMGR == "✔️" && SectionMenuForm.SectionGeneralMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("PProd MGR");

                        ApproverDropDown.Items.Add("PProd MGR");
                        ApproverDropDown.Items.Add("Section MGR");
                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR == "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR == "✔️"))
                    {

                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "";

                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR != "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "Section GM";
                    }
                    //else
                    //{
                    //    ApproverDropDown.Items.Add("Section MGR");
                    //    ApproverDropDown.Text = "Section MGR";
                    //}
                }
                else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                {
                    if ((LoginForm.SectionMGR == "✔️" && LoginForm.SectionGeneralMGR != "✔️") || (SectionMenuForm.SectionMGR == "✔️" && SectionMenuForm.SectionGeneralMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("PProd MGR");
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("PProd MGR");
                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("Section GM");
                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR == "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR == "✔️"))
                    {

                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "";

                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR != "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "Section GM";
                    }
                    //else
                    //{
                    //    ApproverDropDown.Items.Add("Section MGR");
                    //    ApproverDropDown.Text = "Section MGR";
                    //}
                }
                else if (Dashboard.SectionText.Replace("BIPH-", "") == "Production Control")
                {
                    if ((LoginForm.SectionMGR == "✔️" && LoginForm.SectionGeneralMGR != "✔️") || (SectionMenuForm.SectionMGR == "✔️" && SectionMenuForm.SectionGeneralMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("PC MGR");

                        ApproverDropDown.Items.Add("PC MGR");
                        ApproverDropDown.Items.Add("Section MGR");
                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR == "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR == "✔️"))
                    {

                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "";

                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR != "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "Section GM";
                    }
                    //else 
                    //{
                    //    ApproverDropDown.Items.Add("Section MGR");
                    //    ApproverDropDown.Text = "Section MGR";
                    //}
                }
                else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                {
                    if ((LoginForm.SectionMGR == "✔️" && LoginForm.SectionGeneralMGR != "✔️") || (SectionMenuForm.SectionMGR == "✔️" && SectionMenuForm.SectionGeneralMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("PC MGR (Temp)");
                        ApproverDropDown.Items.Remove("Section MGR");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Text = "Section MGR";

                        ApproverDropDown.Items.Add("PC MGR (Temp)");
                        ApproverDropDown.Text = "PC MGR (Temp)";
                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR == "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR == "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "";

                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR != "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "Section GM";
                    }
                    //else
                    //{
                    //    ApproverDropDown.Items.Add("Section MGR");
                    //    ApproverDropDown.Text = "Section MGR";
                    //}
                }
                else
                {
                    if ((LoginForm.SectionMGR == "✔️" && LoginForm.SectionGeneralMGR != "✔️") || (SectionMenuForm.SectionMGR == "✔️" && SectionMenuForm.SectionGeneralMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section MGR");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Text = "Section MGR";
                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR == "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR == "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "";

                    }
                    else if ((LoginForm.SectionGeneralMGR == "✔️" && LoginForm.SectionMGR != "✔️") || (SectionMenuForm.SectionGeneralMGR == "✔️" && SectionMenuForm.SectionMGR != "✔️"))
                    {
                        ApproverDropDown.Items.Remove("Section GM");

                        ApproverDropDown.Items.Add("Section GM");
                        ApproverDropDown.Text = "Section GM";
                    }
                    //else
                    //{
                    //    ApproverDropDown.Items.Add("Section MGR");
                    //    ApproverDropDown.Text = "Section MGR";
                    //}
                }


                //------------------------------------------------------------------

                if (LoginForm.BILSupport == "✔️" || SectionMenuForm.BILSupport == "✔️")
                {
                    ApproverDropDown.Items.Add("BIL PIC");
                    ApproverDropDown.Text = "BIL PIC";
                }
                //else
                //{
                //    ApproverDropDown.Items.Add("Section MGR");
                //    ApproverDropDown.Text = "Section MGR";
                //}

                //------------------------------------------------------------------

                if (LoginForm.MHPIC == "✔️" || SectionMenuForm.MHPIC == "✔️")
                {
                    if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                    {
                        ApproverDropDown.Items.Add("BPS MH PIC");
                        ApproverDropDown.Text = "BPS MH PIC";
                    }
                    else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA" || Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                    {
                        ApproverDropDown.Items.Add("PProd PIC");
                        ApproverDropDown.Text = "PProd PIC";
                    }
                    //else if (Dashboard.SectionText.Replace("BIPH-", "") == "Production Control")
                    //{
                    //    ApproverDropDown.Items.Add("PC PIC");
                    //    ApproverDropDown.Text = "PC PIC";
                    //}
                    //else
                    //{
                    //    ApproverDropDown.Items.Add("Not Applicable");
                    //    ApproverDropDown.Text = "Not Applicable";
                    //    ApproverDropDown.Enabled = false;
                    //}
                }

                if (LoginForm.PCPIC == "✔️" || SectionMenuForm.PCPIC == "✔️")
                {
                    ApproverDropDown.Items.Add("PC PIC");
                    ApproverDropDown.Text = "PC PIC";
                }

                //------------------------------------------------------------------

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                    {
                        ApproverDropDown.Items.Add("PC PIC (Temp)");
                        ApproverDropDown.Text = "PC PIC (Temp)";
                    }
                    //if (Dashboard.SectionText.Replace("BIPH-", "") == "Production Control")
                    //{
                    //    ApproverDropDown.Items.Add("PC PIC");
                    //    ApproverDropDown.Text = "PC PIC";
                    //}
                    //else
                    //{
                    //    ApproverDropDown.Items.Add("Not Applicable");
                    //    ApproverDropDown.Text = "Not Applicable";
                    //    ApproverDropDown.Enabled = false;
                    //}
                }

                //------------------------------------------------------------------

                if (LoginForm.SectionSPV != "✔️" && LoginForm.SectionMGR != "✔️" && LoginForm.SectionGeneralMGR != "✔️" && LoginForm.MHPIC != "✔️" && LoginForm.BILSupport != "✔️" && LoginForm.SectionMGR != "✔️" && LoginForm.PCPIC != "✔️" && SectionMenuForm.SectionSPV != "✔️" && SectionMenuForm.SectionMGR != "✔️" && SectionMenuForm.SectionGeneralMGR != "✔️" && SectionMenuForm.MHPIC != "✔️" && SectionMenuForm.BILSupport != "✔️" && SectionMenuForm.SectionMGR != "✔️" && SectionMenuForm.PCPIC != "✔️")
                {
                    ApproverDropDown.Items.Add("Not Applicable");
                    ApproverDropDown.Text = "Not Applicable";
                    ApproverDropDown.Enabled = false;
                }

                //------------------------------------------------------------------
            }
            else if (ApplicationTypeDropdown.Text == "WC/CC")
            {
                if (LoginForm.SectionSPV == "✔️")
                {
                    ApproverDropDown.Items.Remove("Section SPV");

                    ApproverDropDown.Items.Add("Section SPV");
                    ApproverDropDown.Text = "Section SPV";
                }
                else if (LoginForm.SectionMGR == "✔️")
                {
                    if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                    {
                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("BPS MGR");

                        
                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("BPS MGR");
                    }
                    else
                    {
                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Text = "Section MGR";
                    }
                }
                else if (SectionMenuForm.SectionMGR == "✔️")
                {
                    if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                    {
                        ApproverDropDown.Items.Remove("Section MGR");
                        ApproverDropDown.Items.Remove("BPS MGR");
                        
                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Items.Add("BPS MGR");
                    }
                    else
                    {
                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Text = "Section MGR";
                    }
                }
                else if (LoginForm.MHPIC == "✔️" && Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                {
                    ApproverDropDown.Items.Add("BPS MH PIC");
                    ApproverDropDown.Text = "BPS MH PIC";
                }
                else
                {
                    ApproverDropDown.Items.Add("Not Applicable");
                    ApproverDropDown.Text = "Not Applicable";
                    ApproverDropDown.Enabled = false;
                }
            }
            else if (ApplicationTypeDropdown.Text == "Open MH System")
            {
                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                    {
                        ApproverDropDown.Items.Remove("BPS MGR");
                        ApproverDropDown.Items.Remove("Section MGR");

                        ApproverDropDown.Items.Add("BPS MGR");
                        ApproverDropDown.Items.Add("Section MGR");
                    }
                    else
                    {
                        ApproverDropDown.Items.Remove("Section MGR");

                        ApproverDropDown.Items.Add("Section MGR");
                        ApproverDropDown.Text = "Section MGR";
                    }
                }
                else if (LoginForm.MHPIC == "✔️" && Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                {
                    ApproverDropDown.Items.Add("BPS MH PIC Registration");
                    ApproverDropDown.Items.Add("BPS MH PIC Confirmation");
                }
                else if (LoginForm.MHPIC == "✔️" && Dashboard.SectionText.Replace("BIPH-", "") != "BPS")
                {
                    ApproverDropDown.Items.Add("Section MH PIC");
                    ApproverDropDown.Text = "Section MH PIC";
                }
                else
                {
                    ApproverDropDown.Items.Add("Not Applicable");
                    ApproverDropDown.Text = "Not Applicable";
                    ApproverDropDown.Enabled = false;
                }

                if (LoginForm.SectionGeneralMGR == "✔️" || SectionMenuForm.SectionGeneralMGR == "✔️")
                {
                    ApproverDropDown.Items.Add("Section GM");
                    ApproverDropDown.Text = "Section GM";
                }

            }
        }




        private void AddCheckedBoxColumn()
        {
            // Add checkbox column in datagrid
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = "Select";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.FillWeight = 50; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            ApprovalDataGrid.Columns.Add(checkColumn);
            checkColumn.DisplayIndex = 0;
            checkColumn.Frozen = true;
            // <<----------




            //// Creating checkbox without panel
            //CheckBox checkbox = new CheckBox();
            //checkbox.Size = new System.Drawing.Size(20, 20);
            //checkbox.BackColor = Color.Transparent;

            //// Reset properties
            //checkbox.Padding = new Padding(0);
            //checkbox.Margin = new Padding(0);
            //checkbox.Text = "";

            //// Add checkbox to datagrid cell
            //ApprovalDataGrid.Controls.Add(checkbox);
            //DataGridViewHeaderCell header = ApprovalDataGrid.Columns[0].HeaderCell;
            //checkbox.Location = new Point(
            //    header.ContentBounds.Left + (header.ContentBounds.Right - header.ContentBounds.Left + checkbox.Size.Width) / 2,
            //    header.ContentBounds.Top + (header.ContentBounds.Bottom - header.ContentBounds.Top + checkbox.Size.Height) / 2
            //);


        }

        public static bool IsGenerateBtnClick = false;
        private void GenerateButton_Click(object sender, EventArgs e)
        {

            IsGenerateBtnClick = true;

            //if (ApproverDropDown.Text == "Not Applicable")
            //{
            //    SelectAllCheckbox.Visible = false;
            //    ApprovalDataGrid.Columns["Select"].Visible = false;
            //}
            //else
            //{
            //    SelectAllCheckbox.Visible = true;
            //    ApprovalDataGrid.Columns["Select"].Visible = true;
            //}

            SelectAllCheckbox.Visible = true;

            SelectForApprovalApplication();
        }

        private void SelectForApprovalApplication()
        {
            if (ApplicationTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select application type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                bottomPanel.Visible = true;

                if (ApplicationTypeDropdown.Text == "ST")
                {
                    if (StatusDropdown.Text == "For Approval")
                    {
                        if (ApproverDropDown.Text == "")
                        {
                            MessageBox.Show("Please select approver type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            //if (LoginForm.MHPIC == "✔️")
                            //{
                            if (ApproverDropDown.Text == "BPS MH PIC")
                            {

                                if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByBPSMHPIC");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    //ApprovalDataGrid.Columns["Section"].Visible = false;
                                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = true;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }
                                }
                                else if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByPProdMHPIC");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    //ApprovalDataGrid.Columns["Section"].Visible = false;
                                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = true;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }
                                }
                                else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByPProdMHPIC");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    //ApprovalDataGrid.Columns["Section"].Visible = false;
                                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = true;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }
                                }
                                else if (Dashboard.SectionText.Replace("BIPH-", "") == "Production Control")
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByPCMHPIC");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    //ApprovalDataGrid.Columns["Section"].Visible = false;
                                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = true;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }
                                }
                                else
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "SectionRequestForApproval");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    //ApprovalDataGrid.Columns["Section"].Visible = false;
                                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = false;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }
                                }

                            }
                            else if (ApproverDropDown.Text == "PC PIC (Temp)") //Add this function temporary
                            {
                                if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByPCMHPIC_IH");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    //ApprovalDataGrid.Columns["Section"].Visible = false;
                                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = true;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }
                                }
                            }
                            else if (ApproverDropDown.Text == "PC PIC")
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByPCMHPIC");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                //ApprovalDataGrid.Columns["Section"].Visible = false;
                                ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                            else if (ApproverDropDown.Text == "BPS MGR")
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByBPSMGR");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                //ApprovalDataGrid.Columns["Section"].Visible = false;
                                ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                            else if (ApproverDropDown.Text == "PProd MGR")
                            {


                                if (Dashboard.SectionText.Replace("BIPH-", "") == "PCBA")
                                {

                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByPCBAMGR");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    //ApprovalDataGrid.Columns["Section"].Visible = false;
                                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = true;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }

                                }
                                else if (Dashboard.SectionText.Replace("BIPH-", "") == "Molding Production")
                                {
                                    if (ApproverDropDown.Text == "PProd MGR")
                                    {
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }

                                        SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                        SelectForApproval.CommandType = CommandType.StoredProcedure;
                                        SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByMoldingMGR");
                                        SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                        SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                        SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                        SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                        //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                        SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                        DataTable dt = new DataTable();
                                        sda.Fill(dt);
                                        ApprovalDataGrid.DataSource = dt;

                                        //ApprovalDataGrid.Columns["Section"].Visible = false;
                                        ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                        ApprovalDataGrid.Columns["Select"].Visible = true;
                                        ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                        if (IsGenerateBtnClick == true)
                                        {
                                            if (dt.Rows.Count < 1)
                                            {
                                                MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }

                                            IsGenerateBtnClick = false;
                                        }
                                    }
                                }
                            }
                            else if (ApproverDropDown.Text == "PC MGR")
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByPCMGR");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                //ApprovalDataGrid.Columns["Section"].Visible = false;
                                ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                            else if (ApproverDropDown.Text == "PC MGR (Temp)")
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByPCMGR");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                //ApprovalDataGrid.Columns["Section"].Visible = false;
                                ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                            else if (ApproverDropDown.Text == "Section MGR")
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalBySectionMGR");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                //ApprovalDataGrid.Columns["Section"].Visible = false;
                                ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }

                            }
                            else if (ApproverDropDown.Text == "Section GM")
                            {

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalBySectionGeneralMGR");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                //ApprovalDataGrid.Columns["Section"].Visible = false;
                                ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }

                            }
                            else if (ApproverDropDown.Text == "BIL PIC")
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalBySectionBILPIC");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                //ApprovalDataGrid.Columns["Section"].Visible = false;
                                ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                            else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "AllForApproval");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", "");
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                //ApprovalDataGrid.Columns["Section"].Visible = false;
                                ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }
                        }
                        //else
                        //{
                        //    if (con.State == ConnectionState.Closed)
                        //    {
                        //        con.Open();
                        //    }

                        //    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                        //    SelectForApproval.CommandType = CommandType.StoredProcedure;
                        //    SelectForApproval.Parameters.AddWithValue("@Procedure", "SectionRequestForApproval");
                        //    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                        //    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                        //    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        //    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        //    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        //    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                        //    DataTable dt = new DataTable();
                        //    sda.Fill(dt);
                        //    ApprovalDataGrid.DataSource = dt;

                        //    ApprovalDataGrid.Columns["Section"].Visible = false;
                        //    ApprovalDataGrid.Columns["WithSAP"].Visible = false;

                        //    ApprovalDataGrid.Columns["Select"].Visible = false;
                        //    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                        //    if (IsGenerateBtnClick == true)
                        //    {
                        //        if (dt.Rows.Count < 1)
                        //        {
                        //            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        }

                        //        IsGenerateBtnClick = false;
                        //    }
                        //}

                    }
                    else if (StatusDropdown.Text == "Approved")
                    {

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                        SelectForApproval.CommandType = CommandType.StoredProcedure;
                        SelectForApproval.Parameters.AddWithValue("@Procedure", "ApprovedApplication");
                        SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                        SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                        SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;

                        ApprovalDataGrid.Columns["Select"].Visible = false;
                        ApprovalDataGrid.Columns["Reference No."].Visible = true;

                        if (IsGenerateBtnClick == true)
                        {
                            if (dt.Rows.Count < 1)
                            {
                                MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ApprovalDataGrid.Columns["Section"].Visible = false;
                            }

                            IsGenerateBtnClick = false;
                        }
                    }
                    else if (StatusDropdown.Text == "Rejected")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                        SelectForApproval.CommandType = CommandType.StoredProcedure;
                        SelectForApproval.Parameters.AddWithValue("@Procedure", "RejectedApplication");
                        SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                        SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                        SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;

                        ApprovalDataGrid.Columns["Select"].Visible = false;
                        ApprovalDataGrid.Columns["Reference No."].Visible = true;

                        if (IsGenerateBtnClick == true)
                        {
                            if (dt.Rows.Count < 1)
                            {
                                MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ApprovalDataGrid.Columns["Section"].Visible = false;
                            }

                            IsGenerateBtnClick = false;
                        }
                    }

                    ApprovalDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else if (ApplicationTypeDropdown.Text == "WC/CC")
                {
                    //Type code here...
                    if (StatusDropdown.Text == "For Approval")
                    {

                        if (LoginForm.SectionSPV == "✔️")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                            SelectForApproval.CommandType = CommandType.StoredProcedure;
                            SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalBySectionSPV");
                            SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                            SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                            SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                            //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                            SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;

                            ApprovalDataGrid.Columns["Section"].Visible = false;

                            ApprovalDataGrid.Columns["Select"].Visible = true;
                            ApprovalDataGrid.Columns["Reference No."].Visible = true;

                            if (IsGenerateBtnClick == true)
                            {
                                if (dt.Rows.Count < 1)
                                {
                                    MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                IsGenerateBtnClick = false;
                            }

                        }
                        else if (LoginForm.MHPIC == "✔️")
                        {
                            if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByBPSMHPIC");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                ApprovalDataGrid.Columns["Section"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }

                            }
                            else
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "SectionRequestForApproval");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                ApprovalDataGrid.Columns["Section"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = false;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }

                        }
                        else if (LoginForm.SectionMGR == "✔️")
                        {
                            if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                            {
                                if (ApproverDropDown.Text == "BPS MGR")
                                {

                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByBPSMGR");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    ApprovalDataGrid.Columns["Section"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = true;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }
                                }
                                else if (ApproverDropDown.Text == "Section MGR")
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                    SelectForApproval.CommandType = CommandType.StoredProcedure;
                                    SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalBySectionMGR");
                                    SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                    SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                    SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                    //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                    SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    ApprovalDataGrid.DataSource = dt;

                                    ApprovalDataGrid.Columns["Section"].Visible = false;

                                    ApprovalDataGrid.Columns["Select"].Visible = true;
                                    ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                    if (IsGenerateBtnClick == true)
                                    {
                                        if (dt.Rows.Count < 1)
                                        {
                                            MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        IsGenerateBtnClick = false;
                                    }
                                }

                            }
                            else
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                                SelectForApproval.CommandType = CommandType.StoredProcedure;
                                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalBySectionMGR");
                                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                ApprovalDataGrid.DataSource = dt;

                                ApprovalDataGrid.Columns["Section"].Visible = false;

                                ApprovalDataGrid.Columns["Select"].Visible = true;
                                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                                if (IsGenerateBtnClick == true)
                                {
                                    if (dt.Rows.Count < 1)
                                    {
                                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    IsGenerateBtnClick = false;
                                }
                            }

                        }
                        else if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                            SelectForApproval.CommandType = CommandType.StoredProcedure;
                            SelectForApproval.Parameters.AddWithValue("@Procedure", "AllForApproval");
                            SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                            SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                            SelectForApproval.Parameters.AddWithValue("@Section", "");
                            SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                            //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                            SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;

                            ApprovalDataGrid.Columns["Section"].Visible = false;

                            ApprovalDataGrid.Columns["Select"].Visible = true;
                            ApprovalDataGrid.Columns["Reference No."].Visible = true;

                            if (IsGenerateBtnClick == true)
                            {
                                if (dt.Rows.Count < 1)
                                {
                                    MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                IsGenerateBtnClick = false;
                            }
                        }
                        else
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                            SelectForApproval.CommandType = CommandType.StoredProcedure;
                            SelectForApproval.Parameters.AddWithValue("@Procedure", "SectionRequestForApproval");
                            SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                            SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                            SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                            //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                            SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;

                            ApprovalDataGrid.Columns["Section"].Visible = false;

                            if (ApproverDropDown.Text == "Section MGR")
                            {
                                ApprovalDataGrid.Columns["Select"].Visible = true;
                            }
                            else
                            {
                                ApprovalDataGrid.Columns["Select"].Visible = false;
                            }

                            ApprovalDataGrid.Columns["Reference No."].Visible = true;

                            if (IsGenerateBtnClick == true)
                            {
                                if (dt.Rows.Count < 1)
                                {
                                    MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                IsGenerateBtnClick = false;
                            }
                        }

                    }
                    else if (StatusDropdown.Text == "Approved")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                        SelectForApproval.CommandType = CommandType.StoredProcedure;
                        SelectForApproval.Parameters.AddWithValue("@Procedure", "ApprovedApplication");
                        SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                        SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                        SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;


                        ApprovalDataGrid.Columns["Select"].Visible = false;

                        if (IsGenerateBtnClick == true)
                        {
                            if (dt.Rows.Count < 1)
                            {
                                MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ApprovalDataGrid.Columns["Section"].Visible = false;
                            }

                            IsGenerateBtnClick = false;
                        }
                    }
                    else if (StatusDropdown.Text == "Rejected")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                        SelectForApproval.CommandType = CommandType.StoredProcedure;
                        SelectForApproval.Parameters.AddWithValue("@Procedure", "RejectedApplication");
                        SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                        SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                        SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;


                        ApprovalDataGrid.Columns["Select"].Visible = false;

                        if (IsGenerateBtnClick == true)
                        {
                            if (dt.Rows.Count < 1)
                            {
                                MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ApprovalDataGrid.Columns["Section"].Visible = false;
                            }

                            IsGenerateBtnClick = false;
                        }
                    }

                    ApprovalDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else if (ApplicationTypeDropdown.Text == "Open MH System")
                {
                    if (StatusDropdown.Text == "For Approval")
                    {

                        OpenMHForApproval();

                    }
                    else if (StatusDropdown.Text == "Approved")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                        SelectForApproval.CommandType = CommandType.StoredProcedure;
                        SelectForApproval.Parameters.AddWithValue("@Procedure", "ApprovedApplication");
                        SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                        SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                        SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;

                        ApprovalDataGrid.Columns["Select"].Visible = false;

                        if (IsGenerateBtnClick == true)
                        {
                            if (dt.Rows.Count < 1)
                            {
                                MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ApprovalDataGrid.Columns["Section"].Visible = false;

                            }

                            IsGenerateBtnClick = false;
                        }
                    }
                    else if (StatusDropdown.Text == "Rejected")
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                        SelectForApproval.CommandType = CommandType.StoredProcedure;
                        SelectForApproval.Parameters.AddWithValue("@Procedure", "RejectedApplication");
                        SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                        SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                        SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                        //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;

                        ApprovalDataGrid.Columns["Select"].Visible = false;

                        if (IsGenerateBtnClick == true)
                        {
                            if (dt.Rows.Count < 1)
                            {
                                MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ApprovalDataGrid.Columns["Section"].Visible = false;

                            }

                            IsGenerateBtnClick = false;
                        }
                    }

                    ApprovalDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }

        private void OpenMHForApproval()
        {

            if (ApproverDropDown.Text == "BPS MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                SelectForApproval.CommandType = CommandType.StoredProcedure;
                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByBPSMGR");
                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;

                if (ApprovalDataGrid.Columns.Contains("Section"))
                {
                    ApprovalDataGrid.Columns["Section"].Visible = false;
                }

                if (ApprovalDataGrid.Columns.Contains("Reference No."))
                {
                    ApprovalDataGrid.Columns["Reference No."].Visible = true;
                }

                ApprovalDataGrid.Columns["Select"].Visible = true;

                if (ApprovalDataGrid.Columns.Contains("Month to Open"))
                {
                    ApprovalDataGrid.Columns["Month to Open"].Visible = false;
                }

              

                if (IsGenerateBtnClick == true)
                {
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    IsGenerateBtnClick = false;
                }
            }
            else if (ApproverDropDown.Text == "Section MGR")
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                SelectForApproval.CommandType = CommandType.StoredProcedure;
                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalBySectionMGR");
                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;

                ApprovalDataGrid.Columns["Section"].Visible = false;

                ApprovalDataGrid.Columns["Select"].Visible = true;
                ApprovalDataGrid.Columns["Reference No."].Visible = true;
                ApprovalDataGrid.Columns["Month to Open"].Visible = false;

                if (IsGenerateBtnClick == true)
                {
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    IsGenerateBtnClick = false;
                }
            }
            else if (ApproverDropDown.Text == "Section GM")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                SelectForApproval.CommandType = CommandType.StoredProcedure;
                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalBySectionGeneralMGR");
                SelectForApproval.Parameters.AddWithValue("@Approver", "");
                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;

                //SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                //SelectForApproval.CommandType = CommandType.StoredProcedure;
                //SelectForApproval.Parameters.AddWithValue("@Procedure", "SectionRequestForApproval");
                //SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                //SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                //SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SelectForApproval.Parameters.AddWithValue("@Status", "For Approval");
                ////SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                //SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                //ApprovalDataGrid.DataSource = dt;

                ApprovalDataGrid.Columns["Section"].Visible = false;

                ApprovalDataGrid.Columns["Select"].Visible = true;
                ApprovalDataGrid.Columns["Reference No."].Visible = true;
                ApprovalDataGrid.Columns["Month to Open"].Visible = false;

                if (IsGenerateBtnClick == true)
                {
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    IsGenerateBtnClick = false;
                }
            }
            else if (ApproverDropDown.Text == "BPS MH PIC Registration")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                SelectForApproval.CommandType = CommandType.StoredProcedure;
                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByBPSMHPIC");
                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;

                ApprovalDataGrid.Columns["Section"].Visible = false;

                ApprovalDataGrid.Columns["Select"].Visible = true;
                ApprovalDataGrid.Columns["Reference No."].Visible = true;
                ApprovalDataGrid.Columns["Month to Open"].Visible = false;

                if (IsGenerateBtnClick == true)
                {
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    IsGenerateBtnClick = false;
                }
            }
            else if (ApproverDropDown.Text == "BPS MH PIC Confirmation")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                SelectForApproval.CommandType = CommandType.StoredProcedure;
                SelectForApproval.Parameters.AddWithValue("@Procedure", "ForApprovalByBPSMHPICConfirmation");
                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;

                ApprovalDataGrid.Columns["Section"].Visible = false;

                ApprovalDataGrid.Columns["Select"].Visible = true;
                ApprovalDataGrid.Columns["Reference No."].Visible = true;
                ApprovalDataGrid.Columns["Month to Open"].Visible = false;

                if (IsGenerateBtnClick == true)
                {
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    IsGenerateBtnClick = false;
                }
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                SelectForApproval.CommandType = CommandType.StoredProcedure;
                SelectForApproval.Parameters.AddWithValue("@Procedure", "SectionRequestForApproval");
                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;

                ApprovalDataGrid.Columns["Section"].Visible = true;

                ApprovalDataGrid.Columns["Select"].Visible = false;
                ApprovalDataGrid.Columns["Reference No."].Visible = true;
                ApprovalDataGrid.Columns["Month to Open"].Visible = false;

                if (IsGenerateBtnClick == true)
                {
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    IsGenerateBtnClick = false;
                }
            }


        }

        private void ApprovalDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in ApprovalDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (e.ColumnIndex == 2 && e.Value != null)
            {
                //e.CellStyle.BackColor = Color.FromArgb(65, 137, 218);
                e.CellStyle.ForeColor = Color.FromArgb(27, 88, 245);
                //ApprovalDataGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ApprovalDataGrid.Columns[2].DefaultCellStyle.Font = new Font(ApprovalDataGrid.DefaultCellStyle.Font, FontStyle.Underline);
            }

            ApprovalDataGrid.Columns[0].Frozen = true; //Fixed column
            ApprovalDataGrid.Columns[0].Width = 80;

            ApprovalDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[2].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[4].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[5].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[6].ReadOnly = true;
            ApprovalDataGrid.Rows[e.RowIndex].Cells[7].ReadOnly = true;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (ApprovalDataGrid.DataSource == null)
            {
                MessageBox.Show("No data found, Please generate the data before clicking export button!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ExportCurrenMonthData();
            }

        }

        private void ExportCurrenMonthData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 100;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Procedure", "AllForApproval");
                cmd.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                cmd.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                cmd.Parameters.AddWithValue("@Section", "");
                cmd.Parameters.AddWithValue("@Status", StatusDropdown.Text);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                DateTime currentMonth = DateTime.Now;
                string currentMonthName = currentMonth.ToString("MMMM yyyy");
                string currentMonthFile = currentMonth.ToString("yyyyMM");

                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Excel Workbook|*.xlsx",
                    Title = $"{ApplicationTypeDropdown.Text} - {currentMonthName}",
                    FileName = $"{ApplicationTypeDropdown.Text}_{currentMonthFile}.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string sheetName = ApplicationTypeDropdown.Text;

                        // Replace invalid characters
                        foreach (char c in new[] { '\\', '/', '?', '*', '[', ']', ':' })
                        {
                            sheetName = sheetName.Replace(c, '-');
                        }

                        // Limit to 31 characters (Excel max)
                        if (sheetName.Length > 31)
                            sheetName = sheetName.Substring(0, 31);

                        // Ensure not empty
                        if (string.IsNullOrWhiteSpace(sheetName))
                            sheetName = "Sheet1";

                        using (XLWorkbook wb = new XLWorkbook())
                        {                          
                            wb.Worksheets.Add(dt, sheetName); // ✅ use sanitized sheet name here
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No data found to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void copyAlltoClipboardsss()
        {

            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            ApprovalDataGrid.SelectAll();
            //Copy to clipboard
            ApprovalDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = ApprovalDataGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ExportMHData()
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

            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            //MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public static string ReferenceNumber;
        public static string ApplicationFormType;
        public static string Approver;
        public static string Category;
        public static string DateTimeApplied;
        public static string AppliedBy;
        public static string Section;
        public static string WithSAP;
        public static string MonthToOpen;

        private void ApprovalDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ApprovalDataGrid.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
            {

                if (ApplicationTypeDropdown.Text == "ST")
                {
                    ApplicationFormType = ApplicationTypeDropdown.Text;
                    Section = ApprovalDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString(); //Section column
                    ReferenceNumber = ApprovalDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString(); //Reference no. column
                    Category = ApprovalDataGrid.Rows[e.RowIndex].Cells[3].Value.ToString(); //Category column
                    DateTimeApplied = ApprovalDataGrid.Rows[e.RowIndex].Cells[5].Value.ToString(); //Date time applied column
                    AppliedBy = ApprovalDataGrid.Rows[e.RowIndex].Cells[6].Value.ToString(); //Applied by column
                    Approver = ApproverDropDown.Text;



                    if (StatusDropdown.Text == "For Approval")
                    {
                        WithSAP = ApprovalDataGrid.Rows[e.RowIndex].Cells["WithSAP"].Value.ToString();
                    }

                    ViewApplicationForm viewApplication = new ViewApplicationForm();
                    viewApplication.ShowDialog();
                }
                else if (ApplicationTypeDropdown.Text == "WC/CC")
                {
                    ApplicationFormType = ApplicationTypeDropdown.Text;
                    Section = ApprovalDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString(); //Section column
                    ReferenceNumber = ApprovalDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString(); //Reference no. column
                    Category = ApprovalDataGrid.Rows[e.RowIndex].Cells[3].Value.ToString(); //Category column
                    DateTimeApplied = ApprovalDataGrid.Rows[e.RowIndex].Cells[5].Value.ToString(); //Date time applied column
                    AppliedBy = ApprovalDataGrid.Rows[e.RowIndex].Cells[6].Value.ToString(); //Applied by column
                    Approver = ApproverDropDown.Text;

                    ViewApplicationForm viewApplication = new ViewApplicationForm();
                    viewApplication.ShowDialog();
                }
                else if (ApplicationTypeDropdown.Text == "Open MH System")
                {
                    ApplicationFormType = ApplicationTypeDropdown.Text;
                    Section = ApprovalDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString(); //Section column
                    ReferenceNumber = ApprovalDataGrid.Rows[e.RowIndex].Cells["Reference No."].Value.ToString(); //Reference no. column
                    Category = ApprovalDataGrid.Rows[e.RowIndex].Cells["Open MH Application Category"].Value.ToString(); //Category column
                    DateTimeApplied = ApprovalDataGrid.Rows[e.RowIndex].Cells[6].Value.ToString(); //Date time applied column
                    AppliedBy = ApprovalDataGrid.Rows[e.RowIndex].Cells[7].Value.ToString(); //Applied by column
                    Approver = ApproverDropDown.Text;

                    ViewApplicationForm viewApplication = new ViewApplicationForm();
                    viewApplication.ShowDialog();
                }




                //if (ApplicationTypeDropdown.Text == "ST Application")
                //{
                //    Category = ApprovalDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                //}
                //else if (ApplicationTypeDropdown.Text == "WC/CC Application")
                //{
                //    Category = ApprovalDataGrid.Rows[e.RowIndex].Cells["WC/CC Application Category"].Value.ToString();

                //}
                //else if (ApplicationTypeDropdown.Text == "Open MH System")
                //{
                //    Category = ApprovalDataGrid.Rows[e.RowIndex].Cells["Open MH System Application Category"].Value.ToString();

                //}
                //else if (ApplicationTypeDropdown.Text == "Manpower  Forecast")
                //{
                //    Category = ApprovalDataGrid.Rows[e.RowIndex].Cells["Manpower  Forecast Application Category"].Value.ToString();
                //}

            }

        }


        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SearchApplicationForm = new SqlCommand("SP_SearchForApprovalApplicationForm", con);
                SearchApplicationForm.CommandType = CommandType.StoredProcedure;
                SearchApplicationForm.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                SearchApplicationForm.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SearchApplicationForm.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SearchApplicationForm.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SearchApplicationForm.Parameters.AddWithValue("@Category", Category);
                //SearchApplicationForm.Parameters.AddWithValue("@ReferenceNo", ApplicationForm.ReferenceNumber);
                SearchApplicationForm.Parameters.AddWithValue("@Search", SearchBox.Text);
                SqlDataAdapter da = new SqlDataAdapter(SearchApplicationForm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();

                //ApprovalDataGrid.Columns["Section"].Visible = false;

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }



        private void ApprovalDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void SelectAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectAllCheckbox.Checked == true)
            {
                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                {
                    row.Cells["Select"].Value = true;
                }
            }
            else if (SelectAllCheckbox.Checked == false)
            {
                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                {
                    row.Cells["Select"].Value = false;
                }
            }
        }


        string ReferenceNo;

        private void AcceptButton_Click(object sender, EventArgs e)
        {

            List<DataGridViewRow> selectedRows = (from row in ApprovalDataGrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();

            if (selectedRows.Count < 1 || selectedRows.Count == 0)
            {
                MessageBox.Show("No selected item, Please select at least one to proceed.", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (ApplicationTypeDropdown.Text == "ST")
                {
                    if (ApproverDropDown.Text == "Section MGR")
                    {
                        try
                        {
                            foreach (DataGridViewRow row in selectedRows)
                            {
                                ReferenceNo = row.Cells["Reference No."].Value.ToString();
                                Section = row.Cells["Section"].Value.ToString();
                                Category = row.Cells["ST Application Category"].Value.ToString();
                                WithSAP = row.Cells["WithSAP"].Value.ToString();

                                using (SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn))
                                {
                                    con.Open();

                                    using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                                    {
                                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                        UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");

                                        if (Category == "MH New ST Model List Form")
                                        {
                                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "GM Approval Not Required");
                                        }
                                        else
                                        {
                                            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "2nd Approval --> Section GM");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Section GM");
                                            UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                        }

                                        UpdateApprovalStatus.Parameters.AddWithValue(
                                            "@ApproverName",
                                            "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
                                        );

                                        UpdateApprovalStatus.ExecuteNonQuery();
                                    }
                                }

                               
                            }

                            // ✅ Send email PER row
                            if (Category == "MH New ST Model List Form")
                                STApplicationEmailMessage_BPSMHPIC();
                            else
                                STApplicationEmailMessage_SectionGM();

                            MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SelectForApprovalApplication();
                            SelectAllCheckbox.Checked = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Approval Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (ApproverDropDown.Text == "Section GM")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        // Flags to track if we need to send email to each recipient
                        bool sendToBILPIC = false;
                        bool sendToBPSMHPIC = false;

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            string ReferenceNo = row.Cells["Reference No."].Value?.ToString() ?? "";
                            string Category = row.Cells["ST Application Category"].Value?.ToString() ?? "";

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                    $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                                if (Category == "MH Change ST Model List Form")
                                {
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionGM->BILPIC");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "3rd Approval --> BIL PIC");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BIL PIC");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");

                                    sendToBILPIC = true; // mark to send email later
                                }
                                else
                                {
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionGM->BPSMHPIC");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");

                                    sendToBPSMHPIC = true; // mark to send email later
                                }

                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        // Send emails once per recipient
                        if (sendToBILPIC)
                            STApplicationEmailMessage_BILPIC();

                        if (sendToBPSMHPIC)
                            STApplicationEmailMessage_BPSMHPIC();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "BIL PIC")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        // Flag to track if email should be sent
                        bool sendEmail = false;

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            string ReferenceNo = row.Cells["Reference No."].Value?.ToString() ?? "";
                            string Section = row.Cells["Section"].Value?.ToString() ?? "";
                            string Category = row.Cells["ST Application Category"].Value?.ToString() ?? "";
                            string WithSAP = row.Cells["WithSAP"].Value?.ToString() ?? "";

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBILPIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                    $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                                UpdateApprovalStatus.ExecuteNonQuery();
                            }

                            sendEmail = true; // Mark that we need to send email once
                        }

                        // Send email only once
                        if (sendEmail)
                            STApplicationEmailMessage_BPSMHPIC();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "BPS MH PIC")
                    {
                        try
                        {
                            if (con.State == ConnectionState.Closed)
                                con.Open();

                            bool sendEmailToBPSMGR = false; // Track if we need to send email

                            foreach (DataGridViewRow row in selectedRows)
                            {
                                ReferenceNo = row.Cells["Reference No."].Value.ToString();
                                Section = row.Cells["Section"].Value.ToString();
                                Category = row.Cells["ST Application Category"].Value.ToString();
                                WithSAP = row.Cells["WithSAP"].Value.ToString();

                                using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                                {
                                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                    UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMHPIC");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration Confirmation --> BPS MGR");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MGR");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                        "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));

                                    UpdateApprovalStatus.ExecuteNonQuery();
                                }

                                sendEmailToBPSMGR = true; // Mark to send email once
                            }

                            // Send email only once after processing all rows
                            if (sendEmailToBPSMGR)
                                STApplicationEmailMessage_BPSMGR();

                            MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            SelectForApprovalApplication();
                            SelectAllCheckbox.Checked = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Approval Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                                con.Close();
                        }

                    }
                    else if (ApproverDropDown.Text == "BPS MGR")
                    {
                        bool sendEmailToPProdPIC = false;
                        bool sendEmailToPCPIC = false;
                        bool sendEmailToRequestor = false;

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            Category = row.Cells["ST Application Category"].Value.ToString();
                            WithSAP = row.Cells["WithSAP"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                    $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                                if (WithSAP == "Yes")
                                {
                                    if (Section == "PCBA" || Section == "Molding Production")
                                    {
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration --> PProd PIC");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PProd PIC");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");

                                        sendEmailToPProdPIC = true; // mark to send email later
                                    }
                                    else
                                    {
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration --> PC PIC");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PC PIC");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");

                                        sendEmailToPCPIC = true; // mark to send email later
                                    }
                                }
                                else
                                {
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR->APPROVED");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                    UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");

                                    sendEmailToRequestor = true; // mark to send email later
                                }

                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        // Send emails only once per recipient
                        if (sendEmailToPProdPIC)
                            STApplicationEmailMessage_PProdPIC();

                        if (sendEmailToPCPIC)
                            STApplicationEmailMessage_PCPIC();

                        if (sendEmailToRequestor)
                            STApplicationEmailMessage_SendToRequestor();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "PC PIC")
                    {
                        bool sendEmailToPCMgr = false; // Track if email should be sent

                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            Category = row.Cells["ST Application Category"].Value.ToString();
                            WithSAP = row.Cells["WithSAP"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPCPIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration Confirmation --> PC MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PC MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                    $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                                UpdateApprovalStatus.ExecuteNonQuery();
                            }

                            sendEmailToPCMgr = true; // mark to send email later
                        }

                        // Send email only once after processing all rows
                        if (sendEmailToPCMgr)
                            STApplicationEmailMessage_PCMGR();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "PC PIC (Temp)") //Temporary added this function - 12/11/23
                    {
                        // Flag to send email once
                        bool sendEmailToPCMgr = false;

                        // For PC PIC
                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            Category = row.Cells["ST Application Category"].Value.ToString();
                            WithSAP = row.Cells["WithSAP"].Value.ToString();

                            if (con.State == ConnectionState.Closed)
                                con.Open();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPCPIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration Confirmation --> PC MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PC MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                    $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                                UpdateApprovalStatus.ExecuteNonQuery();
                            }

                            sendEmailToPCMgr = true; // mark to send email once
                            con.Close();
                        }

                        // Send email only once after all rows are processed
                        if (sendEmailToPCMgr)
                            STApplicationEmailMessage_PCMGR();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "PProd PIC")
                    {
                        // Flag to send email once
                        bool sendEmailToPProdMgr = false;

                        // For PProd PIC
                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            Category = row.Cells["ST Application Category"].Value.ToString();
                            WithSAP = row.Cells["WithSAP"].Value.ToString();

                            if (con.State == ConnectionState.Closed)
                                con.Open();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPProdPIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "SAP Registration Confirmation --> PProd MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "PProd MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                    $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                                UpdateApprovalStatus.ExecuteNonQuery();
                            }

                            sendEmailToPProdMgr = true; // mark to send email once
                            con.Close();
                        }

                        // Send email only once after all rows are processed
                        if (sendEmailToPProdMgr)
                            STApplicationEmailMessage_PProdMGR();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;

                    }
                    else if (ApproverDropDown.Text == "PC MGR")
                    {
                        // Flag to send email once
                        bool sendEmailToRequestor = false;

                        // For PC MGR
                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            Category = row.Cells["ST Application Category"].Value.ToString();
                            WithSAP = row.Cells["WithSAP"].Value.ToString();

                            if (con.State == ConnectionState.Closed)
                                con.Open();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPCMGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                    $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                                UpdateApprovalStatus.ExecuteNonQuery();
                            }

                            sendEmailToRequestor = true; // mark to send email once
                            con.Close();
                        }

                        // Send email only once after all rows are processed
                        if (sendEmailToRequestor)
                            STApplicationEmailMessage_SendToRequestor();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;

                    }
                    else if (ApproverDropDown.Text == "PC MGR (Temp)") //Tempory added this function - 12/11/23
                    {
                        //For PC MGR
                        foreach (DataGridViewRow row in selectedRows)
                        {
                            //if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                            //{
                                ReferenceNo = row.Cells["Reference No."].Value.ToString();
                                Section = row.Cells["Section"].Value.ToString();
                                Category = row.Cells["ST Application Category"].Value.ToString();
                                WithSAP = row.Cells["WithSAP"].Value.ToString();


                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPCMGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", ""); //This parameter is not applicable for this query
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", "Approved by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                UpdateApprovalStatus.ExecuteNonQuery();
                                con.Close();

                                //Send email
                                //STApplicationEmailMessage_SendToRequestor();

                            //}
                        }

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();

                        SelectAllCheckbox.Checked = false;

                    }
                    else if (ApproverDropDown.Text == "PProd MGR")
                    {
                        bool sendEmailToRequestor = false;

                        // For PProd MGR
                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            Category = row.Cells["ST Application Category"].Value.ToString();
                            WithSAP = row.Cells["WithSAP"].Value.ToString();

                            if (con.State == ConnectionState.Closed)
                                con.Open();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByPProdMGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName",
                                    $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                                UpdateApprovalStatus.ExecuteNonQuery();
                            }

                            sendEmailToRequestor = true; // mark to send email once
                            con.Close();
                        }

                        // Send email only once after all rows are processed
                        if (sendEmailToRequestor)
                            STApplicationEmailMessage_SendToRequestor();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                }
                else if (ApplicationTypeDropdown.Text == "WC/CC")
                {
                    if (ApproverDropDown.Text == "Section SPV")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["WC/CC Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "2nd Approval --> Section MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Section MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        //Send email once after all rows are updated
                        SendWCCCApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "Section MGR")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["WC/CC Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        //Send email once after all rows are updated
                        SendWCCCApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "BPS MH PIC")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["WC/CC Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMHPIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration Confirmation --> BPS MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        //Send email once after all rows are updated
                        SendWCCCApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "BPS MGR")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["WC/CC Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", $"Approved {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        //Send email once after all rows are updated
                        SendWCCCApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                }
                else if (ApplicationTypeDropdown.Text == "Open MH System")
                {

                    if (ApproverDropDown.Text == "Section MGR")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["Open MH Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "2nd Approval --> Section GM");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Section GM");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        //Send email once after all rows are updated
                        OpenMHApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "Section GM")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            Category = row.Cells["Open MH Application Category"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionGM");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration --> BPS MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        //Send email once for all updated rows
                        OpenMHApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "BPS MH PIC Registration")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["Open MH Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMHPIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "MH System Registration Confirmation --> BPS MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        // Send email once after all rows are processed
                        OpenMHApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "BPS MGR")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["Open MH Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            MonthToOpen = row.Cells["Month to Open"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMGR");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Closing Information --> Section MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "Section MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        // Send email once after all rows are processed
                        OpenMHApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "Section MH PIC")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["Open MH Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();
                            MonthToOpen = row.Cells["Month to Open"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMHPIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Closing Confirmation --> BPS MH PIC");
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "For Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "BPS MH PIC Confirmation");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "Pending Approval");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        // Send email once after all rows are processed
                        OpenMHApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                    else if (ApproverDropDown.Text == "BPS MH PIC Confirmation")
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        foreach (DataGridViewRow row in selectedRows)
                        {
                            ReferenceNo = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["Open MH Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            using (SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con))
                            {
                                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApplicationFormType", ApplicationTypeDropdown.Text);
                                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedByBPSMHPICConfirmation");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                                UpdateApprovalStatus.Parameters.AddWithValue("@ReasonOfRejection", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                UpdateApprovalStatus.Parameters.AddWithValue("@OverallStatus", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@Approver", "APPROVED");
                                UpdateApprovalStatus.Parameters.AddWithValue("@CurrentApprover", "");
                                UpdateApprovalStatus.Parameters.AddWithValue("@ApproverName", $"Approved by {LoginForm.FirstName} {LoginForm.LastName} {DateTime.Now:MM/dd/yyyy hh:mm tt}");
                                UpdateApprovalStatus.ExecuteNonQuery();
                            }
                        }

                        // Send email once after all rows are processed
                        OpenMHApplicationEmailMessage();

                        con.Close();

                        MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SelectForApprovalApplication();
                        SelectAllCheckbox.Checked = false;
                    }
                }
            }
        }

        private void ApplicationTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApprovalDataGrid.DataSource = null;
            AddApproverDropdownList();
        }

        public static bool IsApproveSuccess = false;
        public static bool IsRejectClicked = false;
        public static bool IsProceedClicked = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsApproveSuccess == true)
            {
                SelectForApprovalApplication();

                IsApproveSuccess = false;
            }

            //if (IsRejectClicked == true)
            //{
            //    SelectForApprovalApplication();

            //    IsRejectClicked = false;
            //}

            if (IsProceedClicked == true)
            {
                ApplicationTypeDropdown.Text = DashboardForm3.ApplicationFormType;

                IsProceedClicked = false;
            }
        }

        string innerString;
        string FirstName;
        string LastName;
        string Email;
        //string Addresses;

        private void STApplicationEmailMessage_SectionGM()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionGM");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();

                    builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "") + " Section GM,");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                      
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }

                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] FY." + CurrentYear + ": " + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}
            }
        }

        private void STApplicationEmailMessage_BILPIC()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BILPIC");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();


            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear " + Section + " BIL PIC,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "FY." + CurrentYear + ":" + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        private void STApplicationEmailMessage_BPSMHPIC()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();


            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear BPS MH PICs,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }

                    }

                    //====================>>>>>>>>

                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");

                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        //client.Host = "smtp.brother.co.jp";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        //Insert data to trigger table for sending email

                        MessageBox.Show(ex.Message);
                    }


                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }


        private void STApplicationEmailMessage_BPSMGR()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();


            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();


            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear BPS MGR,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }

                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                          
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer 1")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        if (Section == "Printer 2")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }

                    }

                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("Registration in MH system is already done");
                    builder.Append("<br>");
                    builder.Append("<font color=blue>MHシステムへの登録は完了済です。</font>");
                    builder.Append("<br><br>");

                    if (WithSAP == "No")
                    {
                        builder.Append("For your checking and approval");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>確認と承認依頼の連絡になります。</font>");
                        builder.Append("<br><br>");
                    }


                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        //mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        private void STApplicationEmailMessage_SendToRequestor()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionRequestor");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Section);
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();

            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                //CC to approver -->> Ongoing
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                //foreach (DataRow row in dTable.Rows)
                //{
                //    FirstName = row["First Name"].ToString();
                //    LastName = row["Last Name"].ToString();
                //    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear " + Section + " MH PIC,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");
                    builder.Append("Registration in MH system is already done");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>MHシステム登録が完了しました</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");
                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        //CC to approver - ongoing
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Host = "10.113.10.1";
                        client.Port = 25;
                        client.UseDefaultCredentials = false;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                //}

            }

        }

        private void STApplicationEmailMessage_PProdPIC()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PProdPIC");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                          
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For SAP system registration");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>SAPシステム登録用</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    
                    //}

                }

            }

        }

        private void STApplicationEmailMessage_PCPIC()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PCPIC");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                          
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For SAP system registration");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>SAPシステム登録用</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                }

            }

        }

        private void STApplicationEmailMessage_PCMGR()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PCMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();


            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();


                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                         
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }



                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                          
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                          
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                }

            }

        }

        private void STApplicationEmailMessage_PProdMGR()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PProdMGR");
            SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
            DataTable dTable = new DataTable();
            sda.Fill(dTable);
            con.Close();

            SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
            SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount2.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
            DataTable dTable2 = new DataTable();
            sda2.Fill(dTable2);
            con.Close();

            //Select PE MH PIC
            SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
            SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
            SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
            DataTable dTable3 = new DataTable();
            sda3.Fill(dTable3);
            con.Close();

            //Select Section SPV
            SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
            SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
            DataTable dTable4 = new DataTable();
            sda4.Fill(dTable4);
            con.Close();

            //Select Section MGR
            SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
            SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
            SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
            SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
            SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
            SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
            DataTable dTable5 = new DataTable();
            sda5.Fill(dTable5);
            con.Close();



            if (dTable.Rows.Count > 0)
            {
                string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                foreach (DataRow row in dTable.Rows)
                {
                    FirstName = row["First Name"].ToString();
                    LastName = row["Last Name"].ToString();
                    Email = row["Email"].ToString();

                    //Email body start ====>>>
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();



                    builder.Append("Dear All,");
                    builder.Append("<br>");
                    builder.Append("<br>");


                    builder.Append("Good day!");
                    builder.Append("<br>");
                    builder.Append("<br>");

                    //====================>>>>>>>>

                    if (MHApproval.Category == "Annual ST Change")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP－タッチ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Annual Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の年計ST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form - No BIL Approval")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH Change ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにプリンター課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにP－タッチ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクカートリッジ課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> Change ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済のST変更機種一覧申請をご覧下さい。</font>");
                        }


                    }
                    else if (MHApproval.Category == "MH New ST Model List Form")
                    {
                        if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにプリンター課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてP－タッチ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクカートリッジ課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>下記のリンクにてインクヘッド課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて生産技術課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                           
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + Section + " section's <font color=green>Approved</font> New ST Model List  Application form");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課の承認済の新規ST機種一覧申請をご覧下さい。</font>");
                        }


                    }


                    //====================>>>>>>>>


                    builder.Append("<br>");

                    builder.Append("For your checking and approval");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>確認と承認をお願いします</font>");
                    builder.Append("<br><br>");

                    builder.Append("Reference No. (整 理 番 号)：");
                    builder.Append("<br>");


                    builder.Append(ReferenceNo);
                    builder.Append("<br><br><br>");
                    builder.Append("Link (リンク)：");
                    builder.Append("<br>");

                    //======================>>>>>>

                    builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                    //======================>>>>>>

                    builder.Append("<br><br>");
                    builder.Append("<hr>");
                    builder.Append("<br>");

                    builder.Append("Thanks and Best Regards.");
                    builder.Append("<br>");

                    builder.Append("<b>[This is an automatic generated e-mail]</b>");
                    builder.Append("<br>");

                    builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                    innerString = builder.ToString();
                    //Email body end ====>>>

                    //if (dt.Rows.Count > 0)
                    //{
                    try
                    {
                        string CurrentYear = DateTime.Now.ToString("yyyy");
                        string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                        MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                        mail.CC.Add(EmailListCC);
                        mail.CC.Add(EmailListCC_SPV);
                        mail.CC.Add(EmailListCC_MGR);
                        mail.Bcc.Add(EmailListBCC);
                        mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "10.113.10.1";
                        mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ":" + Section + " section's " + MHApproval.Category + " Application form.";
                        mail.Priority = MailPriority.High;
                        mail.Body = innerString;
                        mail.IsBodyHtml = true;
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //}
                    //else if (dt.Rows.Count < 0)
                    //{
                    //    MessageBox.Show("Walang aplikasyon ngayong araw. salamat!");
                    //}

                }

            }

        }
        //===================================================<break>======================================================//

        private void SendWCCCApplicationEmailMessage()
        {
            if (ApproverDropDown.Text == "Section SPV")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH-", "").Replace("BIPH-", "") + " Section MGR,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        if (MHApproval.Category == "Work Center New")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                              
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Work Center Revision")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>"); 
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Work Center Deletion")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center New")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center Revision")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center Deletion")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }


                        builder.Append("<b>For your approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>承認待ち</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>申請ファイルに問題が生じた場合は、下記のメーリングリストに連絡下さい</font>");
                        builder.Append("<br><br>");

                        builder.Append("<b>PM Group Mailing List</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                        builder.Append("<br>");
                        builder.Append("Bautista, Princess (BIPH-PE) <princess.bautista@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Tan, Lina (BIPH-PE) <lina.tan@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Mateo, Bradly (BIPH-PE) <bradly.mateo@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Dimayuga, Jeancy (BIPH-PE) <jeancy.dimayuga@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Estacio, Dianelle Yasdane (BIPH-PE) <dianelleyasdane.estacio@brother-biph.com.ph>");
                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>ご尽力頂きありがとうございます。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Section + " section's " + Category + " Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                }
            }
            else if (ApproverDropDown.Text == "Section MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();


                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear BPS MH PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        if (MHApproval.Category == "Work Center New")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Work Center Revision")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Work Center Deletion")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                      
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center New")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center Revision")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Cost Center Deletion")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }

                        builder.Append("<b>For your approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>承認をお願い致します。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>申請ファイルに不具合が発生した場合には、下記のメーリングリストにご連絡下さい。</font>");
                        builder.Append("<br><br>");

                        builder.Append("<b>PM Group Mailing List</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                        builder.Append("<br>");
                        builder.Append("Bautista, Princess (BIPH-PE) <princess.bautista@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Tan, Lina (BIPH-PE) <lina.tan@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Mateo, Bradly (BIPH-PE) <bradly.mateo@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Dimayuga, Jeancy (BIPH-PE) <jeancy.dimayuga@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Estacio, Dianelle Yasdane (BIPH-PE) <dianelleyasdane.estacio@brother-biph.com.ph>");
                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<font color=blue>宜しくお願い致します。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動配信されています]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Section + " section's " + Category + " Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                }
            }
            else if (ApproverDropDown.Text == "BPS MH PIC")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMGR");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string FirstNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["First Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear BPS MGR,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        if (MHApproval.Category == "Work Center New")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                     
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); //
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Work Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規ワークセンター登録申請が承認された事をお知らせします。</font>"); 
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Work Center Revision")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                       
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Work Center Deletion")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                       
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Work Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のワークセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center New")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the New Cost Center Registration for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課の新規コストセンター登録申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }
                        else if (MHApproval.Category == "Cost Center Revision")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                      
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Revision of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの改訂申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }

                        }
                        else if (MHApproval.Category == "Cost Center Deletion")
                        {
                            if (Section == "Ink Cartridge")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクカートリッジ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Ink Head")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>インクヘッド課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");

                            }
                            else if (Section == "P-Touch")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>P-タッチ課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "BPS")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                            }
                            else if (Section == "Printer")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>プリンター課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Tape Cassette")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>テープカセット課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "PCBA")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>基板組立課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Molding Production")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                builder.Append("<br>");
                            }
                            else if (Section == "Toner")
                            {
                                builder.Append("This is to inform you that the Deletion of Cost Center for " + Section + " section has been approved.");
                                builder.Append("<br>");
                                //builder.Append("<font color=blue>成形課のコストセンターの削除申請が承認された事をお知らせします。</font>");
                                //builder.Append("<br>");
                            }


                        }

                        builder.Append("<b>For your approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>承認をお願い致します。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>申請ファイルに不具合が発生した場合には、下記のメーリングリストにご連絡下さい。</font>");
                        builder.Append("<br><br>");

                        builder.Append("<b>PM Group Mailing List</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                        builder.Append("<br>");
                        builder.Append("Bautista, Princess (BIPH-PE) <princess.bautista@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Tan, Lina (BIPH-PE) <lina.tan@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Mateo, Bradly (BIPH-PE) <bradly.mateo@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Dimayuga, Jeancy (BIPH-PE) <jeancy.dimayuga@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Estacio, Dianelle Yasdane (BIPH-PE) <dianelleyasdane.estacio@brother-biph.com.ph>");
                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<font color=blue>宜しくお願い致します。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動配信されています]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Section + " section's " + Category + " Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else if (ApproverDropDown.Text == "BPS MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                //SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                //SelectUsersAccount.Parameters.AddWithValue("@Procedure", "PEMGR");
                //SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                //SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                //SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                //DataTable dTable = new DataTable();
                //sda.Fill(dTable);
                //con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();

                if (dTable2.Rows.Count > 0)
                {
                    //string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    //string EmailListTo = String.Join("; ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable2.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear All");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Registration in MH system is already done");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>MHシステム登録が完了しました</font>");
                        builder.Append("<br>");
                        builder.Append("<b>For your checking/verification.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>ご確認・検証お願い致します。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("In case a problem occurred in the application file, kindly inform the mailing list below.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>申請ファイルに不具合が発生した場合には、下記のメーリングリストにご連絡下さい。</font>");
                        builder.Append("<br><br>");

                        builder.Append("<b>PM Group Mailing List</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>PM Gメーリングリスト</font>");
                        builder.Append("<br>");
                        builder.Append("Bautista, Princess (BIPH-PE) <princess.bautista@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Tan, Lina (BIPH-PE) <lina.tan@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Mateo, Bradly (BIPH-PE) <bradly.mateo@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Dimayuga, Jeancy (BIPH-PE) <jeancy.dimayuga@brother-biph.com.ph>");
                        builder.Append("<br>");
                        builder.Append("Estacio, Dianelle Yasdane (BIPH-PE) <dianelleyasdane.estacio@brother-biph.com.ph>");
                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<font color=blue>宜しくお願い致します。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動配信されています]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + Section + " section's " + Category + " Application form.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void OpenMHApplicationEmailMessage()
        {
            if (ApproverDropDown.Text == "Section MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionGM");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear " + Dashboard.SectionText.Replace("BIPH - ", "").Replace("BIPH-", "") + " Section GM,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジセクションの承認済みOpen MHシステムリクエストフォームについては、以下のリンクを参照してください。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。/font>");
                            builder.Append("<br>");

                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP-タッチ課のMHシステム編集解除許可（OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
             
                        }
                        else if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてプリンター課のMHシステム編集解除許可 (OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }

                        builder.Append("<b>For your checking and approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>確認・承認待ち</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");


                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                }
            }
            else if (ApproverDropDown.Text == "Section GM")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();
                 
                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear BPS MH PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジセクションの承認済みOpen MHシステムリクエストフォームについては、以下のリンクを参照してください。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。/font>");
                            builder.Append("<br>");

                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP-タッチ課のMHシステム編集解除許可（OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                        }
                        else if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてプリンター課のMHシステム編集解除許可 (OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }


                        builder.Append("<b>Kindly proceed to Open MH system.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>MHシステムを開き、編集に進んで下さい。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");


                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                }
            }
            else if (ApproverDropDown.Text == "BPS MH PIC Registration")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMGR");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();


                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear BPS MGR,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        if (Section == "Ink Cartridge")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>インクカートリッジセクションの承認済みOpen MHシステムリクエストフォームについては、以下のリンクを参照してください。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Ink Head")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてインクヘッド課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。/font>");
                            builder.Append("<br>");

                        }
                        else if (Section == "P-Touch")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてP-タッチ課のMHシステム編集解除許可（OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "BPS")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                       
                        }
                        else if (Section == "Printer")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてプリンター課のMHシステム編集解除許可 (OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Tape Cassette")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにてテープカセット課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "PCBA")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて基板組立課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Molding Production")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            builder.Append("<br>");
                        }
                        else if (Section == "Toner")
                        {
                            builder.Append("Please see link below for " + MHApproval.Section + " section's approved Open MH system request form.");
                            builder.Append("<br>");
                            //builder.Append("<font color=blue>以下のリンクにて成形課のMHシステム編集解除許可(OPEN MH)申請をご覧下さい。</font>");
                            //builder.Append("<br>");
                        }


                        builder.Append("<b>For your checking and approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>確認・承認待ち</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");


                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else if (ApproverDropDown.Text == "BPS MGR")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                //SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                //SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                //SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                //SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                //SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                //SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                //DataTable dTable2 = new DataTable();
                //sda2.Fill(dTable2);
                //con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();

                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    //string EmailListCC = String.Join("; ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear " + MHApproval.Section + " MH PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("MH system for the month of " + MonthToOpen + " is already open.");
                        builder.Append("<br>");
                        //builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>For your checking and approval.</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>確認・承認待ち</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else if (ApproverDropDown.Text == "Section MH PIC")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                DataTable dTable2 = new DataTable();
                sda2.Fill(dTable2);
                con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC = String.Join(", ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear BPS MH PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        builder.Append("Revision in MH system is already done.");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>MHシステムでの改訂が完了しました。</font>");
                        builder.Append("<br>");

                        builder.Append("<b><font color=green>MH system can now be close.</font></b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>MHシステムは編集締結（クローズ）可能です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            mail.CC.Add(EmailListCC);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                }
            }
            else if (ApproverDropDown.Text == "BPS MH PIC Confirmation")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUsersAccount = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                SelectUsersAccount.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectUsersAccount);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                con.Close();

                //SqlCommand SelectUsersAccount2 = new SqlCommand("SP_SelectUsersAccount", con);
                //SelectUsersAccount2.CommandType = CommandType.StoredProcedure;
                //SelectUsersAccount2.Parameters.AddWithValue("@Procedure", "SectionMHPIC");
                //SelectUsersAccount2.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                //SelectUsersAccount2.Parameters.AddWithValue("@Section", Section);
                //SqlDataAdapter sda2 = new SqlDataAdapter(SelectUsersAccount2);
                //DataTable dTable2 = new DataTable();
                //sda2.Fill(dTable2);
                //con.Close();

                //Select PE MH PIC
                SqlCommand SelectUsersAccount3 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount3.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount3.Parameters.AddWithValue("@Procedure", "BPSMHPIC");
                SelectUsersAccount3.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount3.Parameters.AddWithValue("@Section", "BPS");
                SqlDataAdapter sda3 = new SqlDataAdapter(SelectUsersAccount3);
                DataTable dTable3 = new DataTable();
                sda3.Fill(dTable3);
                con.Close();

                //Select Section SPV
                SqlCommand SelectUsersAccount4 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount4.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount4.Parameters.AddWithValue("@Procedure", "SectionSPV");
                SelectUsersAccount4.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount4.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda4 = new SqlDataAdapter(SelectUsersAccount4);
                DataTable dTable4 = new DataTable();
                sda4.Fill(dTable4);
                con.Close();

                //Select Section MGR
                SqlCommand SelectUsersAccount5 = new SqlCommand("SP_SelectUsersAccount", con);
                SelectUsersAccount5.CommandType = CommandType.StoredProcedure;
                SelectUsersAccount5.Parameters.AddWithValue("@Procedure", "SectionMGR");
                SelectUsersAccount5.Parameters.AddWithValue("@ApplicationformType", ApplicationTypeDropdown.Text);
                SelectUsersAccount5.Parameters.AddWithValue("@Section", MHApproval.Section);
                SqlDataAdapter sda5 = new SqlDataAdapter(SelectUsersAccount5);
                DataTable dTable5 = new DataTable();
                sda5.Fill(dTable5);
                con.Close();


                if (dTable.Rows.Count > 0)
                {
                    string LastNameList = String.Join(" san, ", dTable.AsEnumerable().Select(row => row["Last Name"]).ToArray());
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());
                    //string EmailListCC = String.Join("; ", dTable2.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListBCC = String.Join(", ", dTable3.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_SPV = String.Join(", ", dTable4.AsEnumerable().Select(row => row["Email"]).ToArray());
                    string EmailListCC_MGR = String.Join(", ", dTable5.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        FirstName = row["First Name"].ToString();
                        LastName = row["Last Name"].ToString();
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();

                        builder.Append("Dear All,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>お疲れ様です。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");


                        builder.Append("MH system for the month of " + MonthToOpen + " is already closed.");
                        builder.Append("<br>");
                        //builder.Append("<font color=blue>インクカートリッジ課の新規ワークセンター登録申請が承認された事をお知らせします。</font>");
                        builder.Append("<br>");

                        builder.Append("<b>Kindly start to revise your input</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>入力の改訂を開始して下さい。</font>");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>Reference No. (整 理 番 号)：</b>");
                        builder.Append("<br>");
                        builder.Append("<b><i>" + ReferenceNo + "</i></b>");
                        builder.Append("<br><br><br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");
                        builder.Append("<hr>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");
                        builder.Append("<br>");
                        builder.Append("<font color=blue>[本メールは自動で送信されています。]</font>");
                        innerString = builder.ToString();
                        //Email body end ====>>>

                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //mail.cc.add(emaillistcc);
                            mail.CC.Add(EmailListCC_SPV);
                            mail.CC.Add(EmailListCC_MGR);
                            mail.Bcc.Add(EmailListBCC);
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": " + MHApproval.Section + " section's Open MH request.";
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            client.Send(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = (from row in ApprovalDataGrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();

            if (selectedRows.Count < 1 || selectedRows.Count == 0)
            {
                MessageBox.Show("Please select the item you want to reject!", "Reminders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (selectedRows.Count > 1)
            {
                MessageBox.Show("Cannot process multiple selected data, Please select one item to reject request!", "Reminders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (ApplicationTypeDropdown.Text == "ST")
                {
                    foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                    {
                        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                        {
                            ApplicationFormType = ApplicationTypeDropdown.Text;
                            ReferenceNumber = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["ST Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            if (MessageBox.Show("Are you sure you want to reject this application?", "MHMS Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                MHReasonOfRejection rejectionForm = new MHReasonOfRejection();
                                rejectionForm.ShowDialog();
                            }
                        }
                    }
                }
                else if (ApplicationTypeDropdown.Text == "WC/CC")
                {
                    foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                    {
                        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                        {
                            ApplicationFormType = ApplicationTypeDropdown.Text;
                            ReferenceNumber = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["WC/CC Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            if (MessageBox.Show("Are you sure you want to reject this application?", "MHMS Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                MHReasonOfRejection rejectionForm = new MHReasonOfRejection();
                                rejectionForm.ShowDialog();
                            }
                        }
                    }
                }
                else if (ApplicationTypeDropdown.Text == "Open MH System")
                {
                    foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                    {
                        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                        {
                            ApplicationFormType = ApplicationTypeDropdown.Text;
                            ReferenceNumber = row.Cells["Reference No."].Value.ToString();
                            Category = row.Cells["Open MH Application Category"].Value.ToString();
                            Section = row.Cells["Section"].Value.ToString();

                            if (MessageBox.Show("Are you sure you want to reject this application?", "MHMS Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                MHReasonOfRejection rejectionForm = new MHReasonOfRejection();
                                rejectionForm.ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        private void OverAllForApprovalBtn_Click(object sender, EventArgs e)
        {
            if (ApplicationTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select application type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                SelectAllCheckbox.Visible = false;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                SelectForApproval.CommandType = CommandType.StoredProcedure;
                SelectForApproval.Parameters.AddWithValue("@Procedure", "SectionRequestForApproval");
                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectForApproval.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SelectForApproval.Parameters.AddWithValue("@Status", "For Approval");
                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;

                if (ApplicationTypeDropdown.Text == "")
                {
                    ApprovalDataGrid.Columns["Select"].Visible = false;
                    ApprovalDataGrid.Columns["Section"].Visible = false;
                    ApprovalDataGrid.Columns["Reference No."].Visible = false;
                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;
                    ApprovalDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                else
                {
                    //ApprovalDataGrid.Columns["Section"].Visible = false;
                    ApprovalDataGrid.Columns["Select"].Visible = false;
                    ApprovalDataGrid.Columns["Reference No."].Visible = true;
                    ApprovalDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }

        private void ApprovalDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ApproverDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AllPendingAppBtn_Click(object sender, EventArgs e)
        {
            if (ApplicationTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select application type.", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                SelectAllCheckbox.Visible = false;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectForApproval = new SqlCommand("SP_SelectForApprovalPerApplicationType", con);
                SelectForApproval.CommandType = CommandType.StoredProcedure;
                SelectForApproval.Parameters.AddWithValue("@Procedure", "AllForApproval");
                SelectForApproval.Parameters.AddWithValue("@Approver", ApproverDropDown.Text);
                SelectForApproval.Parameters.AddWithValue("@ApplicationType", ApplicationTypeDropdown.Text);
                SelectForApproval.Parameters.AddWithValue("@Section", "");
                SelectForApproval.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                //SelectForApproval.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectForApproval);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;

                //ApprovalDataGrid.Columns["Section"].Visible = false;
                if (ApplicationTypeDropdown.Text == "ST")
                {
                    ApprovalDataGrid.Columns["WithSAP"].Visible = false;
                }

                ApprovalDataGrid.Columns["Select"].Visible = true;
                ApprovalDataGrid.Columns["Reference No."].Visible = true;

                if (IsGenerateBtnClick == true)
                {
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("No data has been generated!", "MHMS Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    IsGenerateBtnClick = false;
                }
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DownloadApprovedAppBtn_Click(object sender, EventArgs e)
        {
            if (monthCbx.Text == "-- Select month --")
            {
                MessageBox.Show("Please select month.", "Required.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                monthCbx.Select();
            }
            else if (yearCbx.Text == "-- Select year --")
            {
                MessageBox.Show("Please select year.", "Required.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                yearCbx.Select();
            }
            else
            {
                string selectedAppType = ApplicationTypeDropdown.Text;

                // Example: ComboBoxes/TextBoxes hold text values
                string selectedMonthText = monthCbx.Text; // e.g. "09" or "September"
                string selectedYearText = yearCbx.Text;  // e.g. "2025"

                // Convert text to int (safe parsing)
                int selectedMonth;
                if (!int.TryParse(selectedMonthText, out selectedMonth))
                {
                    // If the month is full text like "September", convert it
                    if (DateTime.TryParseExact(selectedMonthText, "MMMM",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                    {
                        selectedMonth = parsedDate.Month;
                    }
                    else
                    {
                        MessageBox.Show("Invalid month value.");
                        return;
                    }
                }

                int selectedYear;
                if (!int.TryParse(selectedYearText, out selectedYear))
                {
                    MessageBox.Show("Invalid year value.");
                    return;
                }

                // Pick the table name
                string tableName = "";
                if (selectedAppType == "ST")
                    tableName = "TBL_STApplicationApproval";
                else if (selectedAppType == "WC/CC")
                    tableName = "TBL_WCCCApplicationApproval";
                else if (selectedAppType == "Open MH System")
                    tableName = "TBL_OpenMHApplicationApproval";
                else
                {
                    MessageBox.Show("Invalid Application Type selected.");
                    return;
                }

                // Get data
                DataTable dt = GetDataByMonthYear(tableName, selectedMonth, selectedYear);

                if (dt.Rows.Count == 0)
                {
                    ApprovalDataGrid.DataSource = null; // clear grid
                    MessageBox.Show("No record found for the selected month and year.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ApprovalDataGrid.DataSource = dt;

                // Hide ID column
                if (ApprovalDataGrid.Columns.Contains("ID"))
                {
                    ApprovalDataGrid.Columns["ID"].Visible = false;
                }

                if (ApprovalDataGrid.Columns.Contains("Select"))
                {
                    ApprovalDataGrid.Columns["Select"].Visible = false;
                }

                if (ApprovalDataGrid.Columns.Contains("Action"))
                {
                    ApprovalDataGrid.Columns["Action"].Visible = false;
                }

                if (ApprovalDataGrid.Columns.Contains("ReferenceNo"))
                {
                    ApprovalDataGrid.Columns["ReferenceNo"].Visible = false;
                }
            }
        }

        private DataTable GetDataByMonthYear(string tableName, int month, int year)
        {
            DataTable dt = new DataTable();

            string query = $@"
            SELECT *
            FROM {tableName}
            WHERE MONTH(CONVERT(date, DateAndTimeApplied, 101)) = @Month
              AND YEAR(CONVERT(date, DateAndTimeApplied, 101)) = @Year AND Section = @Section;";

            using (SqlConnection con = new SqlConnection(SQLControl.MHMS2_Conn))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        //================================================================<BreakLine>======================================================>>> 

    }
}
