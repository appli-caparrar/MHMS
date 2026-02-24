using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class
{
    public class MHData_Class
    {
        public string DistinctionCode { get; set; }
        public string ReferenceNo { get; set; }
        public string Section { get; set; }
        public string DateEncountered { get; set; }
        public string Plant { get; set; }
        public string WorkCenter { get; set; }
        public string Day_Night { get; set; }
        public string CostCenter { get; set; }
        public string ModelName { get; set; }
        public string ItemCode { get; set; }
        public string ItemText { get; set; }
        public string StopTime { get; set; }
        public string LineStopDetail { get; set; }
        public string LossFactor { get; set; }
        public string DirectMP { get; set; }
        public string SemiDirectMP { get; set; }
        public string LossManhour { get; set; }
        public string UploadDate { get; set; }
        public string COPQAmount { get; set; }
        public string ApplyingApprovalStatus { get; set; }
        public string ReceivingApprovalStatus { get; set; }
        public string OverAllStatus { get; set; }
        public string QIConfirmation { get; set; }
        public string LossMH_ForCOPQAmount { get; set; }
        public string Remarks { get; set; }
        public string FiscalYear { get; set; }
        public string ApplyingCOPQPIC { get; set; }
        public string Mark { get; set; }
        public string LeadTime { get; set; }
        public string TargetClosedDate { get; set; }

        public int RowCounter { get; set; } // Add this property for the row counter
    }
}
