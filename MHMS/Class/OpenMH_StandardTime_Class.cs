using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class
{
    class OpenMH_StandardTime_Class
    {
        public string ReferenceNo { get; set; }
        public string ApplicationFormNo { get; set; }
        public string No { get; set; }
        public string Section { get; set; }

        public string Date { get; set; }
        public string CostCenter { get; set; }
        public string WorkCenter { get; set; }
        public string Shift { get; set; }
        public string ItemCode { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
        public string Difference { get; set; }
        public string ReasonOfRevision { get; set; }

        public string DateApplied { get; set; }
        public string AppliedBy { get; set; }
    }
}
