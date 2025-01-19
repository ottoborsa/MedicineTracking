
using System;
using System.Linq;

using MedicineTracking.Table;


namespace MedicineTracking.Query
{
    internal static class Common
    {

        static char ListSeparator = ',';


        public static float GetDosageOfDay(DateTime day, DateTime validFrom, PatientDosage.DosageType type, float dosageValue, string param)
        {
            float result = 0;


            switch (type)
            {
                case PatientDosage.DosageType.daily:

                    result = dosageValue;
                    break;


                case PatientDosage.DosageType.every_other_day:

                    bool isCurrentDayEven = IsDayEvenDay(day);
                    bool isValidFromDayEven = IsDayEvenDay(validFrom);
                    if ((isValidFromDayEven && isCurrentDayEven) || (!isValidFromDayEven && !isCurrentDayEven))
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
                    string dayOfWeek = ((int)day.DayOfWeek).ToString();
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
