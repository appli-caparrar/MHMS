using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class
{
    class OpenMH_MH_MPCategory_Class
    {
        public string ReferenceNo { get; set; }
        public string ApplicationFormNo { get; set; }
        public string No { get; set; }
        public string Section { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string CostCenterCode { get; set; }
        public string WorkCenterCode { get; set; }
        public string Shift { get; set; }
        public string OperationTime_Old { get; set; }
        public string DirectOperator_Old { get; set; }
        public string SemiDirectOperator_Old { get; set; }
        public string SemiIndirectOperator_Old { get; set; }
        public string TotalManpower_Old { get; set; }
        public string TotalManhour_Old { get; set; }
        public string OperationTime_New { get; set; }
        public string DirectOperator_New { get; set; }
        public string SemiDirectOperator_New { get; set; }
        public string SemiIndirectOperator_New { get; set; }
        public string TotalManpower_New { get; set; }
        public string TotalManhour_New { get; set; }
        public string ReasonOfRevision { get; set; }
        public string DateApplied { get; set; }
        public string AppliedBy { get; set; }
    }
}
