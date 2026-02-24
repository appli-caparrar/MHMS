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
    public partial class OpenGraphForm : Form
    {
        public OpenGraphForm()
        {
            InitializeComponent();
        }

        private void COPQDisposalPartsCostBtn_MouseEnter(object sender, EventArgs e)
        {
            COPQDisposalPartsCostBtn.Size = new Size(150, 150);
            GraphName.Visible = true;
            GraphName.Text = "COPQ Parts Disposal Cost (Cumulative)";
        }

        private void COPQDisposalPartsCostBtn_MouseLeave(object sender, EventArgs e)
        {
            COPQDisposalPartsCostBtn.Size = new Size(137, 112);
            GraphName.Visible = false;
        }

        private void Top5DisposalCostBtn_MouseEnter(object sender, EventArgs e)
        {
            Top5DisposalCostBtn.Size = new Size(150, 150);
            GraphName.Visible = true;
            GraphName.Text = "Top 5 Monthly Disposal Cost";
        }

        private void Top5DisposalCostBtn_MouseLeave(object sender, EventArgs e)
        {
            Top5DisposalCostBtn.Size = new Size(137, 112);
            GraphName.Visible = false;
        }

        private void Top5DefectRecurrenceBtn_MouseEnter(object sender, EventArgs e)
        {
            Top5DefectRecurrenceBtn.Size = new Size(150, 150);
            GraphName.Visible = true;
            GraphName.Text = "Top 5 Defect Recurrence";
        }

        private void Top5DefectRecurrenceBtn_MouseLeave(object sender, EventArgs e)
        {
            Top5DefectRecurrenceBtn.Size = new Size(137, 112);
            GraphName.Visible = false;
        }

        private void COPQDisposalPartsCostBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/COPQDisposalPartsCost/CombineGraph?:origin=card_share_link&:embed=n");
        }

        private void Top5DisposalCostBtn_Click(object sender, EventArgs e)
        {
            if (Dashboard.SectionText.Replace("BIPH-", "") == "BPS")
            {
                SectionPanel.Visible = true;
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Printer")
            {
                SectionPanel.Visible = false;
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/Printer?:origin=card_share_link&:embed=n");
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Cartridge")
            {
                SectionPanel.Visible = false;
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/InkCartridgeDashboard?:origin=card_share_link&:embed=n");
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette")
            {
                SectionPanel.Visible = false;
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/TapeCassette?:origin=card_share_link&:embed=n");
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "Ink Head")
            {
                SectionPanel.Visible = false;
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/InkHead?:origin=card_share_link&:embed=n");
            }
            else if (Dashboard.SectionText.Replace("BIPH-", "") == "P-Touch")
            {
                SectionPanel.Visible = false;
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/P-Touch?:origin=card_share_link&:embed=n");
            }
        }

        private void Top5DefectRecurrenceBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DefectRecurrence/Top5DefectRecurrence?:origin=card_share_link&:embed=n");
        }

        private void OpenGraphForm_Load(object sender, EventArgs e)
        {
            if ((Dashboard.SectionText.Replace("BIPH-", "") == "Tape Cassette") || (Dashboard.SectionText.Replace("BIPH-", "") == "BPS"))
            {
                Top5DefectRecurrenceBtn.Location = new Point(390, 101);
                Top5DisposalCostBtn.Location = new Point(213, 101);
                COPQDisposalPartsCostBtn.Location = new Point(38, 101);
            }
            else
            {
                Top5DefectRecurrenceBtn.Visible = false;
                Top5DisposalCostBtn.Location = new Point(327, 101);
                COPQDisposalPartsCostBtn.Location = new Point(103, 101);
            }

            //if ((Dashboard.SectionText.Replace("BIPH-", "") != "Production Engineering"))
            //{
            //    Top5DefectRecurrenceBtn.Visible = false;
            //    Top5DisposalCostBtn.Location = new Point(327, 101);
            //    COPQDisposalPartsCostBtn.Location = new Point(103, 101);
            //}
            //else
            //{
            //    Top5DefectRecurrenceBtn.Visible = true;
            //    Top5DisposalCostBtn.Location = new Point(213, 101);
            //    COPQDisposalPartsCostBtn.Location = new Point(38, 101);
            //}
        }

        private void SectionDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SectionDropdownList.Text == "Ink Cartridge")
            {
                Process.Start(@"https://bi.datalake.brother.co.jp/#/site/biph/views/Top5DisposalPartsCost/InkCartridgeDashboard?:iid=2");
            }
            else if (SectionDropdownList.Text == "Ink Head")
            {
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/InkHead?:origin=card_share_link&:embed=n");
            }
            else if (SectionDropdownList.Text == "Printer")
            {
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/Printer?:origin=card_share_link&:embed=n");
            }
            else if (SectionDropdownList.Text == "P-Touch")
            {
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/P-Touch?:origin=card_share_link&:embed=n");
            }
            else if (SectionDropdownList.Text == "Tape Cassette")
            {
                Process.Start(@"https://bi.datalake.brother.co.jp/t/biph/views/Top5DisposalPartsCost/TapeCassette?:origin=card_share_link&:embed=n");
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            SectionPanel.Visible = false;
        }


        //===================================================<end>=====================================================//
    }
}
