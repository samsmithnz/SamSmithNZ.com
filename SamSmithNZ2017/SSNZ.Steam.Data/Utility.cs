using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam.Data
{
    public static class Utility
    {
        public static string ConvertMinutesToFriendlyTime(long minutes)
        {
            string result = "";

            long hh = minutes / (long)60;
            result = hh + " hrs";

            return result;
        }
    }
}
