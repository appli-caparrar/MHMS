using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class_Efficiency
{
    class TotalEfficiency_PRT_Class
    {
        public string Section { get; set; }

        //------------------------------------------------------
        public string PRT_DailyKPITarget { get; set; }
        public string PRT_DailyChallengeTarget { get; set; }
        public string PRT_OverallResult { get; set; }
        public string PRT_DirectContributor { get; set; }
        public string PRT_SemiDirectContributor { get; set; }
        public string PRT_LossManhourContributor { get; set; }

        //------------------------------------------------------
        public string PRT_Mini_DailyKPITarget { get; set; }
        public string PRT_Mini_DailyChallengeTarget { get; set; }
        public string PRT_Mini_OverallResult { get; set; }
        public string PRT_Mini_DirectContributor { get; set; }
        public string PRT_Mini_SemiDirectContributor { get; set; }
        public string PRT_Mini_LossManhourContributor { get; set; }

        //------------------------------------------------------
        public string PRT_A3_DailyKPITarget { get; set; }
        public string PRT_A3_DailyChallengeTarget { get; set; }
        public string PRT_A3_OverallResult { get; set; }
        public string PRT_A3_DirectContributor { get; set; }
        public string PRT_A3_SemiDirectContributor { get; set; }
        public string PRT_A3_LossManhourContributor { get; set; }

        public string Date { get; set; }

        //------------------------------------------------------
        public string PRT_Section { get; set; }
        public string MonthlyKPITarget { get; set; }
        public string MonthlyChallengeTarget { get; set; }
        public string MonthlyOverallResult { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

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

        //------------------------------------------------------

        //public string DailyKPITarget { get; set; }
        //public string DailyChallengeTarget { get; set; }
        //public string OverallResult { get; set; }
        //public string DirectContributor { get; set; }
        //public string SemiDirectContributor { get; set; }
        //public string LossManhourContributor { get; set; }
       

        //------------------------------------------------------
        //public string PerWorkCenter { get; set; }
        //public string DailyResult { get; set; }
        //public string DailyContributor { get; set; }


    }
}
