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
    public partial class MHMSUpdateInformationForm : Form
    {
        public MHMSUpdateInformationForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\Other Files\How to uninstall MHMS.docx");
        }

        private void InstallBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe");
        }

        private void MHMSUpdateInformationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
