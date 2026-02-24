using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class
{
    class OpenMH_LineStop_LossMH_LossFactor_Class
    {
        public string ReferenceNo { get; set; }
        public string ApplicationFormNo { get; set; }
        public string No { get; set; }
        public string Section { get; set; }

        public string Date { get; set; }
        public string Shift { get; set; }
        public string CostCenterCode { get; set; }
        public string WorkCenterCode { get; set; }
        public string LineStopContentDetail_Old { get; set; }
        public string LossFactor_Old { get; set; }
        public string StopTime_Old { get; set; }
        public string DirectOperator_Old { get; set; }
        public string SemiDirectEmployee_Old { get; set; }
        public string LossManhour_Old { get; set; }
        public string LineStopContentDetail_New { get; set; }
        public string LossFactor_New { get; set; }
        public string StopTime_New { get; set; }
        public string DirectOperator_New { get; set; }
        public string SemiDirectEmployee_New { get; set; }
        public string LossManhour_New { get; set; }

        public string ReasonOfRevision { get; set; }
        public string DateApplied { get; set; }
        public string AppliedBy { get; set; }
    }
}
