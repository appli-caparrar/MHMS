using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MHMS.Class_Efficiency
{
    class DirectEfficiency_Class
    {
        public string Section { get; set; }

        //------------------------------------------------------
        public string DailyKPITarget { get; set; }
        public string DailyChallengeTarget { get; set; }
        public string OverallResult { get; set; }

        //public string SupportContributor { get; set; }
        //public string FinalPackingContributor { get; set; }
        //public string IFOldContributor { get; set; }
        //public string FWOldContributor { get; set; }
        //public string HTContributor { get; set; }
        //public string OthersContributor { get; set; }
        //public string IF_BH17_Contributor { get; set; }
        //public string FW_BH17_Contributor { get; set; }
        //public string SIM17_Contributor { get; set; }
        //public string FWSIM_Contributor { get; set; }
        //public string SIM19_Contributor { get; set; }
        //public string REG19_Contributor { get; set; }
        //public string Blossom19_Contributor { get; set; }

        //------------------------------------------------------
        //public string Monthly_Section { get; set; }
        public string MonthlyKPITarget { get; set; }
        public string MonthlyChallengeTarget { get; set; }
        public string MonthlyOverallResult { get; set; }
        //public string Month { get; set; }
        //public string Year { get; set; }

        public string Date { get; set; }

        //------------------------------------------------------

        public string Contributor_Section { get; set; }
        public string Contributor_ProcessItem { get; set; }
        public string Contributor_Rate { get; set; }
        public string Contributor_ProcessDate { get; set; }

        //------------------------------------------------------
        public string WC_Section { get; set; }
        public string Workcenter { get; set; }
        public string WC_DailyResult { get; set; }
        public string WC_Date { get; set; }
        public string WC_OverallResult { get; set; }
        public string WC_Month { get; set; }

        //------------------------------------------------------
        public string CC_Section { get; set; }
        public string Costcenter { get; set; }
        public string CC_DailyResult { get; set; }
        public string CC_Date { get; set; }
        public string CC_OverallResult { get; set; }
        public string CC_Month { get; set; }

        //------------------------------------------------------
        public string Process_Section { get; set; }
        public string Process_Item { get; set; }
        public string Process_DailyResult { get; set; }
        public string Process_Date { get; set; }
        public string Process_OverallResult { get; set; }
        public string Process_Month { get; set; }

        public string UploadDate { get; set; }
    }
}
