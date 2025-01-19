
using System;
using System.Collections.Generic;

namespace MedicineTracking.Utility
{
    internal static class DateTools
    {
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }
    }
}
