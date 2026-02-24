using MHMS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class DirectEfficiencyGraphForm : Form
    {
        public DirectEfficiencyGraphForm()
        {
            InitializeComponent();
        }

        private void DirectEfficiencyGraphForm_Load(object sender, EventArgs e)
        {
            SectionDropdownList.Items.Add("Ink Cartridge");
            SectionDropdownList.Items.Add("Ink Head");
            SectionDropdownList.Items.Add("Printer 1");
            SectionDropdownList.Items.Add("Printer 2");
            SectionDropdownList.Items.Add("P-Touch");
            SectionDropdownList.Items.Add("Tape Cassette");
            SectionDropdownList.Items.Add("Toner");
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            if (ProductionEfficiencyForm.isOverallResult == true)
            {
                if (SectionDropdownList.Text == "Ink Cartridge")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/t/biph/views/MHMS_DirectEfficiencyIC/InkCartridgeDirectEfficiencyOverallResult?:origin=card_share_link&:embed=n");
                }
                else if (SectionDropdownList.Text == "Ink Head")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyIH/InkHeadDirectEfficiencyOverallResult");
                }
                else if (SectionDropdownList.Text == "Printer 1")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyPR1/Printer1DirectEfficiencyOverallResult?:iid=1");
                }
                else if (SectionDropdownList.Text == "Printer 2")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyPR2/Printer2DirectEfficiencyOverallResult?:iid=5");
                }
                else if (SectionDropdownList.Text == "P-Touch")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyPT/P-TouchDirectEfficiencyOverallResult?:iid=4");
                }
                else if (SectionDropdownList.Text == "Tape Cassette")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyTC/TapeCasseteDirectEfficiencyOverallResult?:iid=5");
                }
                else if (SectionDropdownList.Text == "Toner")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyTN/TonerDirectEfficiencyOverallResult?:iid=1");
                }

                ProductionEfficiencyForm.isOverallResult = false;
            }
            else 
            {
                if (SectionDropdownList.Text == "Ink Cartridge")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyIC/InkCartridgeDirectEfficencyMonthly");
                }
                else if (SectionDropdownList.Text == "Ink Head")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyIH/InkHeadDirectEfficiency");
                }
                else if (SectionDropdownList.Text == "Printer 1")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyPR1/Printer1DirectEfficiencyMonthly?:iid=6");
                }
                else if (SectionDropdownList.Text == "Printer 2")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyPR2/Printer2DirectEfficiencyMonthly?:iid=6");
                }
                else if (SectionDropdownList.Text == "P-Touch")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyPT/P-TouchDirectEfficiencyMonthly?:iid=3");
                }
                else if (SectionDropdownList.Text == "Tape Cassette")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyTC/TapeCassetteDirectEfficiency?:iid=8");
                }
                else if (SectionDropdownList.Text == "Toner")
                {
                    Process.Start("https://bi.datalake.brother.co.jp/#/site/biph/views/MHMS_DirectEfficiencyTN/TonerDirectEfficencyMonthly?:iid=1");
                }
            }
        }

    }
}
