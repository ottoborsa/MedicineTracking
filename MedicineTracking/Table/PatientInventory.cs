
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
    internal static class PatientInventory
    {

        private const char FileNameSeparator = '_';

        private const char DynamicColumnNameSeparator = '_';



        public const string medicine_id = nameof(medicine_id);

        public const string medicine_name = nameof(medicine_name);

        public const string inventory_prefix = "inventory";

        public const string increment_prefix = "increment";




        public static List<Model.PatientInventory> Parse(List<FileRecord> files)
        {
            List<Model.PatientInventory> result = new();

            int fileCounter = 0;
            foreach (FileRecord fileRecord in files)
            {
                // ProgressBar: 1st event (of total 3); 25% range of whole
                fileCounter++;
                int progressPercentage = (int)Math.Round((decimal)(fileCounter) / (decimal)(files.Count) * 100);
                int progress = 0 + progressPercentage / 4;
                ApplicationInterface.SetProgressBarValue(progress);


                List<PatientInventoryRecord> list = new();
                string fileName = String.Empty;
                string medicineId = String.Empty;

                try
                {
                    string filePath = fileRecord.Path;
                    string[] path = filePath.Split('\\');
                    string fileContent = fileRecord.Content;
                    fileName = path[path.Length - 1];

                    string patientName = fileName.Split(FileNameSeparator)[0].Trim();
                    string patientId = fileName.Split(FileNameSeparator)[1].Split('.')[0].Trim();

                    if (String.IsNullOrEmpty(patientId))
                    {
                        throw new SerializedException("InvalidPatientId");
                    }
                    if (result.Where(element => element.PatientId == patientId).Count() > 0)
                    {
                        throw new SerializedException("DuplicateOfPatient");
                    }

                    Matrix inventoryMatrix = CsvParser.Parse(fileContent);

                    string[] signature = inventoryMatrix.Signature.Select(element => element.Trim()).ToArray();
                    string lastInventoryColumn = String.Empty;
                    List<string> incrementColumns = new List<string>();
                    foreach (string columnName in signature)
                    {
                        if (columnName.StartsWith(inventory_prefix))
                        {
                            lastInventoryColumn = columnName;
                        }
                        if (columnName.StartsWith(increment_prefix))
                        {
                            incrementColumns.Add(columnName);
                        }
                    }

                    if (String.IsNullOrEmpty(lastInventoryColumn))
                    {
                        throw new SerializedException("MissingInventory");
                    }

                    for (int i = 0; i < inventoryMatrix.GetSize(); i++)
                    {
                        medicineId = inventoryMatrix.GetValue(medicine_id, i).Trim();

                        if (String.IsNullOrEmpty(medicineId))
                        {
                            throw new SerializedException("InvalidMedicineId");
                        }
                        if (list.Where(element => element.MedicineId == medicineId).Count() > 0)
                        {
                            throw new SerializedException("DuplicateOfMedicine");
                        }

                        string medicineName = inventoryMatrix.GetValue(medicine_name, i).Trim();

                        DateTime inventoryDate = DateTime.Parse(lastInventoryColumn.Split(DynamicColumnNameSeparator)[1]);

                        if (inventoryDate > DateTools.GetToday())
                        {
                            throw new SerializedException("FutureInventoryDate");
                        }

                        decimal medicineCount = Decimal.Parse(inventoryMatrix.GetValue(lastInventoryColumn, i).Trim(), CultureInfo.InvariantCulture);

                        Dictionary<DateTime, decimal> incrementations = new Dictionary<DateTime, decimal>();

                        for (int j = 0; j < incrementColumns.Count; j++)
                        {
                            DateTime incrementDate = DateTime.Parse(incrementColumns[j].Split(DynamicColumnNameSeparator)[1]);

                            if (incrementDate > DateTools.GetToday())
                            {
                                throw new SerializedException("FutureIncrementDate");
                            }

                            if (incrementDate >= inventoryDate)
                            {
                                string incrementValueString = inventoryMatrix.GetValue(incrementColumns[j], i).Trim();
                                decimal incrementValue = String.IsNullOrEmpty(incrementValueString) ? 0 : Decimal.Parse(incrementValueString, CultureInfo.InvariantCulture);

                                if (incrementValue != 0)
                                {
                                    incrementations.Add(incrementDate, incrementValue);
                                }
                            }
                        }

                        list.Add(new PatientInventoryRecord(medicineId, medicineName, inventoryDate, medicineCount, incrementations));
                    }

                    Model.PatientInventory patient = new Model.PatientInventory(patientId, patientName, list);
                    result.Add(patient);
                }
                catch (SerializedException ex)
                {
                    throw new SerializedException(
                        new Translation(
                            $"{nameof(Table)}.{nameof(PatientInventory)}",
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
                            $"{nameof(Table)}.{nameof(PatientInventory)}",
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
