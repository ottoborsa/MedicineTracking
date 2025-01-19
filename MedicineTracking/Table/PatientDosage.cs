
using System.Collections.Generic;
using System;

using MedicineTracking.Model;

namespace MedicineTracking.Table
{


    internal static class PatientDosage
    {

        public enum DosageType
        {
            daily,           // dosage type parameter: number value (double) per day
            every_other_day,
            on_even_days,
            on_odd_days,
            weekly           // dosage type parameter: list of weekdays (1, 2, 3, 4, 5, 6, 7)
        }




        private static char FileNameSeparator = '_';




        public const string medicine_id = nameof(medicine_id);

        public const string medicine_name = nameof(medicine_name);

        public const string dosage_type_code = nameof(dosage_type_code);

        public const string dosage_type_parameter = nameof(dosage_type_parameter);

        public const string valid_from = nameof(valid_from);

        public const string valid_to = nameof(valid_to);





        public static List<MedicineDosage> Parse(Dictionary<string, string> files)
        {
            List<MedicineDosage> result = new List<MedicineDosage>();

            foreach (KeyValuePair<string, string> file in files)
            {
                List<MedicineDosageRecord> list = new List<MedicineDosageRecord>();

                string filePath = file.Key;
                string[] path = filePath.Split('\\');
                string fileName = path[path.Length - 1];
                string fileContent = file.Value;

                string patientName = fileName.Split(FileNameSeparator)[0];
                string patientId = fileName.Split(FileNameSeparator)[1];

                CsvParser.Matrix dosageMatrix = CsvParser.CsvParser.Parse(fileContent);

                for (int i = 0; i < dosageMatrix.GetSize(); i++)
                {
                    string medicineId = dosageMatrix.GetValue(medicine_id, i);
                    DosageType dosageType = (DosageType)Enum.Parse(typeof(DosageType), dosageMatrix.GetValue(dosage_type_code, i));
                    string dosageParam = dosageMatrix.GetValue(dosage_type_parameter, i);
                    DateTime validFrom = DateTime.Parse(dosageMatrix.GetValue(valid_from, i));
                    DateTime validTo = DateTime.Parse(String.IsNullOrEmpty(dosageMatrix.GetValue(valid_to, i)) ? "2100-12-31" : dosageMatrix.GetValue(valid_to, i));

                    list.Add(new MedicineDosageRecord(medicineId, dosageType, dosageParam, validFrom, validTo));
                }

                MedicineDosage patient = new MedicineDosage(patientId, patientName, list);
                result.Add(patient);
            }

            return result;
        }
    }
}
