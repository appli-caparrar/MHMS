using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class OverallMonitoring_web : Form
    {
        public OverallMonitoring_web()
        {
            InitializeComponent();
        }

        private async void OverallMonitoring_web_Load(object sender, EventArgs e)
        {
            // Ensure WebView2 is properly initialized.
            await webView21.EnsureCoreWebView2Async(null);

            // Handle NavigationCompleted event
            //webView21.CoreWebView2.NavigationCompleted += (s, args) =>
            //{
            //    if (args.IsSuccess)
            //    {
            //        MessageBox.Show("Navigation successful!");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Navigation failed.");
            //    }
            //};

            // Navigate to a webpage
            webView21.CoreWebView2.Navigate("http://apbiphbpsts02:8080/mhms-overall-monitoring");
        }
    }
}
