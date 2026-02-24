using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class
{
    class CC_Revision_Class
    {
        public string ReferenceNo { get; set; }
        public string ApplicationFormNo { get; set; }
        public string No { get; set; }
        //public string WorkCenterCode_Old { get; set; }
        //public string WorkCenterName_Old { get; set; }
        public string CostCenterCode_Old { get; set; }
        public string CostCenterName_Old { get; set; }
        public string Shift_Old { get; set; }
        public string Plant_Old { get; set; }
        public string CostCenterGrouping_Old { get; set; }
        //public string WorkCenterCode_New { get; set; }
        //public string WorkCenterName_New { get; set; }
        public string CostCenterCode_New { get; set; }
        public string CostCenterName_New { get; set; }
        public string Shift_New { get; set; }
        public string Plant_New { get; set; }
        public string CostCenterGrouping_New { get; set; }
        public string ReasonOfApplication { get; set; }
        public string Effectivity { get; set; }
        public string Remarks { get; set; }
        public string Section { get; set; }
        public string DateApplied { get; set; }
        public string AppliedBy { get; set; }
    }
}
