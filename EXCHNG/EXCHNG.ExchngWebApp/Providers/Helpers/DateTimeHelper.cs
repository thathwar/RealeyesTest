using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXCHNG.ExchngWebApp.Providers.Helpers
{
    public class DateTimeHelper
    {
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }
    }
}