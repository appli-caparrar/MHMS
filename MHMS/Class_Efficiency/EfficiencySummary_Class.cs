using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class_Efficiency
{
    class EfficiencySummary_Class
    {
        //public string TotalEfficiency { get; set; }
        //public string MonthlyKPITarget { get; set; }
        //public string MonthlyChallengeTarget { get; set; }
        //public string MonthlyCumulativeKPITarget { get; set; }
        //public string MonthlyCumulativeChallengeTarget { get; set; }
        //public string AnnualRecoveryTarget { get; set; }
        //public string MonthlyCumulativeActual { get; set; }
        //public string YearlyResult { get; set; }
        //public string MonthlyDate { get; set; }
        //public string Yearly { get; set; }

        //TOTAL EFFICIENCY --------------------------------------->>>>>>>>>
        public string TotalEfficiency { get; set; }
        public string TE_Apr { get; set; }
        public string TE_May { get; set; }
        public string TE_Jun { get; set; }
        public string TE_Jul { get; set; }
        public string TE_Aug { get; set; }
        public string TE_Sep { get; set; }
        public string TE_Oct { get; set; }
        public string TE_Nov { get; set; }
        public string TE_Dec { get; set; }
        public string TE_Jan { get; set; }
        public string TE_Feb { get; set; }
        public string TE_Mar { get; set; }
        public string TE_Yearly { get; set; }

        //Common column
        public string Section { get; set; }
        public string YearDate { get; set; }
        public string UploadDate { get; set; }
        public string Year { get; set; }
        public string UploadCount { get; set; }


        //DIRECT EFFICIENCY --------------------------------------->>>>>>>>>
        public string DirectEfficiency { get; set; }
        public string DE_Apr { get; set; }
        public string DE_May { get; set; }
        public string DE_Jun { get; set; }
        public string DE_Jul { get; set; }
        public string DE_Aug { get; set; }
        public string DE_Sep { get; set; }
        public string DE_Oct { get; set; }
        public string DE_Nov { get; set; }
        public string DE_Dec { get; set; }
        public string DE_Jan { get; set; }
        public string DE_Feb { get; set; }
        public string DE_Mar { get; set; }
        public string DE_Yearly { get; set; }

        //SEMI-DIRECT RATE --------------------------------------->>>>>>>>>
        public string SemiDirectRate { get; set; }
        public string SDR_Apr { get; set; }
        public string SDR_May { get; set; }
        public string SDR_Jun { get; set; }
        public string SDR_Jul { get; set; }
        public string SDR_Aug { get; set; }
        public string SDR_Sep { get; set; }
        public string SDR_Oct { get; set; }
        public string SDR_Nov { get; set; }
        public string SDR_Dec { get; set; }
        public string SDR_Jan { get; set; }
        public string SDR_Feb { get; set; }
        public string SDR_Mar { get; set; }
        public string SDR_Yearly { get; set; }

        //TOTAL LOSS RATE --------------------------------------->>>>>>>>>
        public string TotalLossRate { get; set; }
        public string TLR_Apr { get; set; }
        public string TLR_May { get; set; }
        public string TLR_Jun { get; set; }
        public string TLR_Jul { get; set; }
        public string TLR_Aug { get; set; }
        public string TLR_Sep { get; set; }
        public string TLR_Oct { get; set; }
        public string TLR_Nov { get; set; }
        public string TLR_Dec { get; set; }
        public string TLR_Jan { get; set; }
        public string TLR_Feb { get; set; }
        public string TLR_Mar { get; set; }
        public string TLR_Yearly { get; set; }


    }

}
