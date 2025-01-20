
using System;
using System.Linq;

using MedicineTracking.Table;


namespace MedicineTracking.Query
{
    internal static class Common
    {


        private const char ListSeparator = ',';


        public static decimal GetDosageOfDay(DateTime day, DateTime validFrom, PatientDosage.DosageType type, decimal dosageValue, string param)
        {
            decimal result = 0;

            switch (type)
            {
                case PatientDosage.DosageType.daily:

                    result = dosageValue;
                    break;


                case PatientDosage.DosageType.every_other_day:

                    if (day.Subtract(validFrom).TotalDays == 0 || day.Subtract(validFrom).TotalDays % 2 == 0)
                    {
                        result = dosageValue;
                    }
                    break;


                case PatientDosage.DosageType.on_even_days:

                    if (IsDayEvenDay(day))
                    {
                        result = dosageValue;
                    }
                    break;


                case PatientDosage.DosageType.on_odd_days:

                    if (!IsDayEvenDay(day))
                    {
                        result = dosageValue;
                    }
                    break;


                case PatientDosage.DosageType.weekly:

                    string[] weekDays = param.Split(ListSeparator);
                    string dayOfWeek = ((int)day.DayOfWeek + 1).ToString();
                    if (weekDays.Contains(dayOfWeek))
                    {
                        result = dosageValue;
                    }
                    break;


                default:
                    throw new ArgumentException();
            }

            return result;
        }

        private static bool IsDayEvenDay(DateTime day)
        {
            return (day.Day % 2) == 0;
        }
    }
}
