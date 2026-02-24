using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class LoginRemindersForm : Form
    {
        public LoginRemindersForm()
        {
            InitializeComponent();
        }

        private void OpenPortalBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\Installer\BPS Centralized Login\setup.exe");
            this.Close();
        }


        private void LoginRemindersForm_Load(object sender, EventArgs e)
        {
            IPAddressLabel.Text = GetLocalIPAddress();
        }

        //Get local IP Address of current computer
        public static string GetLocalIPAddress()
        {
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void BILTempLoginBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
