using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Connection
{
    class SQLControl
    {
        //COPQ
        public static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS_ACTUAL"].ConnectionString;

        //MH Application / Efficiency
        public static string MHMS2_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2_ACTUAL"].ConnectionString;

        //I-portal
        public static string CentralizedLogin = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.CentralizedLogin"].ConnectionString;

        //BIPH Calendar
        public static string biph_calendar_conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.biph_calendar"].ConnectionString;
        
    }
}
