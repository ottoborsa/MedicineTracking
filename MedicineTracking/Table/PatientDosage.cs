
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

using MedicineTracking.Model;
using MedicineTracking.Utility;
using MedicineTracking.Messaging;
using MedicineTracking.Messaging.Const;
using MedicineTracking.Core;


namespace MedicineTracking.Table
{


    internal static class PatientDosage
    {

        public enum DosageType
        {
            daily,
            every_other_day,
            on_even_days,
            on_odd_days,
            weekly           // dosage type parameter: list of weekdays (1, 2, 3, 4, 5, 6, 7)
        }




        private const char FileNameSeparator = '_';




        public const string medicine_id = nameof(medicine_id);

        public const string medicine_name = nameof(medicine_name);

        public const string dosage_type_code = nameof(dosage_type_code);

        public const string dosage_value = nameof(dosage_value);

        public const string dosage_type_parameter = nameof(dosage_type_parameter);

        public const string valid_from = nameof(valid_from);

        public const string valid_to = nameof(valid_to);





        public static List<MedicineDosage> Parse(List<FileRecord> files)
        {
            List<MedicineDosage> result = new();

            int fileCounter = 0;
            foreach (FileRecord fileRecord in files)
            {
                // ProgressBar: 2nd event (of total 3); 25% range of whole
                fileCounter++;
                int progressPercentage = (int)Math.Round((decimal)(fileCounter) / (decimal)(files.Count) * 100);
                int progress = 25 + progressPercentage / 4;
                ApplicationInterface.SetProgressBarValue(progress);


                List<MedicineDosageRecord> list = new();
                string fileName = String.Empty;
                string medicineId = String.Empty;

                try
                {
                    string filePath = fileRecord.FilePath;
                    string[] path = filePath.Split('\\');
                    string fileContent = fileRecord.Content;
                    fileName = path[path.Length - 1];

                    string patientName = fileName.Split(FileNameSeparator)[0];
                    string patientId = fileName.Split(FileNameSeparator)[1].Split(Core.DataBase.FileExtensionSeparator)[0];

                    if (result.Where(element => element.PatientId == patientId).Count() > 0)
                    {
                        throw new SerializedException("DuplicateOfPatient");
                    }

                    Matrix dosageMatrix = CsvParser.Parse(fileContent);

                    for (int i = 0; i < dosageMatrix.GetSize(); i++)
                    {
                        medicineId = dosageMatrix.GetValue(medicine_id, i);

                        DosageType dosageType = (DosageType)Enum.Parse(typeof(DosageType), dosageMatrix.GetValue(dosage_type_code, i));
                        string dosageValueString = dosageMatrix.GetValue(dosage_value, i);
                        decimal dosageValue = String.IsNullOrEmpty(dosageValueString) ? 0 : Decimal.Parse(dosageValueString, CultureInfo.InvariantCulture);
                        string dosageParam = dosageMatrix.GetValue(dosage_type_parameter, i);
                        DateTime validFrom = DateTime.Parse(dosageMatrix.GetValue(valid_from, i));
                        DateTime validTo = DateTime.Parse(String.IsNullOrEmpty(dosageMatrix.GetValue(valid_to, i)) ? DateTools.ForeverDateString : dosageMatrix.GetValue(valid_to, i));

                        if (validFrom > validTo)
                        {
                            throw new SerializedException("InvalidValidityRange");
                        }

                        list.Add(new MedicineDosageRecord(medicineId, dosageType, dosageValue, dosageParam, validFrom, validTo));
                    }

                    MedicineDosage patient = new MedicineDosage(patientId, patientName, list);
                    result.Add(patient);
                }
                catch (SerializedException ex)
                {
                    throw new SerializedException(
                        new Translation(
                            $"{nameof(Table)}.{nameof(PatientDosage)}",
                            new()
                            {
                                { Keys.FileName, fileName },
                                { Keys.MedicineId, medicineId },
                                { Keys.ErrorMessage, JsonConvert.DeserializeObject<SystemError>(SystemError.ParseException(ex).ErrorMessage).ErrorMessage }
                            }
                        )
                    );
                }
                catch (Exception ex)
                {
                    throw new SerializedException(
                        new Translation(
                            $"{nameof(Table)}.{nameof(PatientDosage)}",
                            new()
                            {
                                { Keys.FileName, fileName },
                                { Keys.MedicineId, medicineId },
                                { Keys.ErrorMessage, ex.Message }
                            }
                        )
                    );
                }
            }

            return result;
        }
    }
}
