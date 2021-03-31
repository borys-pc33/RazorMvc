using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorMvc.Utilities
{
    public class DateTimeConvertor
    {
        public static DateTime ConvertEpochToDateTime(long ticks)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(ticks);
            DateTime dateTime = dateTimeOffset.UtcDateTime;
            return dateTime;
        }
    }
}
