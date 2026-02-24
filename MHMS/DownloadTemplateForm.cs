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
    public partial class DownloadTemplateForm : Form
    {
        public DownloadTemplateForm()
        {
            InitializeComponent();
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (TypeDropdown.Text == "")
            {
                MessageBox.Show("Please select the type of template.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {  
                if (TypeDropdown.Text == "Disposal Budget")
                {
                    Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Other Template\Disposal Budget Template - Updated.xlsx");
                }
                else if (TypeDropdown.Text == "Production Standard Manhour")
                {
                    Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Other Template\Standard MH - Updated.xlsx");
                }
                else if (TypeDropdown.Text == "COPQ Manpower Rate")
                {
                    Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\Other Template\COPQ Manpower Rate Template.xlsx");
                }
                else if (TypeDropdown.Text == "MH Loss Rate Target")
                {

                }
                else if (TypeDropdown.Text == "Production Efficiency")
                {

                }
                else if (TypeDropdown.Text == "Production Efficiency Target")
                {
                    
                }
                else if (TypeDropdown.Text == "Annual Monthly Factory Efficiency")
                {

                }
            }
        }

        private void DownloadTemplateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
