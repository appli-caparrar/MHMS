using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class
{
    class STMasterData_Class
    {
        public string Section { get; set; }
        public string MassProduction { get; set; }
        public string Plant { get; set; }
        public string ItemCodeSAP { get; set; }
        public string ItemNameSAP { get; set; }
        public string SAPBeforeST { get; set; }
        public string SAPAfterST { get; set; }
        public string SAPBeforeTT { get; set; }
        public string SAPAfterTT { get; set; }
        public string ItemCodeMH { get; set; }
        public string ItemNameMH { get; set; }
        public string MHBeforeST { get; set; }
        public string MHAfterST { get; set; }
        public string MHBeforeTT { get; set; }
        public string MHAfterTT { get; set; }
    }
}
