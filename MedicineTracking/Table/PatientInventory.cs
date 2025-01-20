
using System.Collections.Generic;
using System;
using System.Globalization;

using MedicineTracking.Utility;
using MedicineTracking.Messaging;
using MedicineTracking.Messaging.Const;



namespace MedicineTracking.Table
{
    internal static class PatientInventory
    {

        private const char FileNameSeparator = '_';

        private const char FileExtensionSeparator = '.';

        private const char DynamicColumnNameSeparator = '_';



        public const string medicine_id = nameof(medicine_id);

        public const string medicine_name = nameof(medicine_name);

        public const string inventory_prefix = "inventory";

        public const string increment_prefix = "increment";




        public static List<Model.PatientInventory> Parse(Dictionary<string, string> files)
        {
            try
            {
                List<Model.PatientInventory> result = new List<Model.PatientInventory>();

                foreach (KeyValuePair<string, string> file in files)
                {
                    List<PatientInventoryRecord> list = new List<PatientInventoryRecord>();

                    string filePath = file.Key;
                    string[] path = filePath.Split('\\');
                    string fileName = path[path.Length - 1];
                    string fileContent = file.Value;

                    string patientName = fileName.Split(FileNameSeparator)[0];
                    string patientId = fileName.Split(FileNameSeparator)[1].Split(FileExtensionSeparator)[0];

                    Matrix inventoryMatrix = CsvParser.Parse(fileContent);


                    string[] signature = inventoryMatrix.Signature;
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


                    for (int i = 0; i < inventoryMatrix.GetSize(); i++)
                    {
                        string medicineId = inventoryMatrix.GetValue(medicine_id, i);
                        string medicineName = inventoryMatrix.GetValue(medicine_name, i);

                        DateTime inventoryDate = DateTime.Parse(lastInventoryColumn.Split(DynamicColumnNameSeparator)[1]);
                        decimal medicineCount = Decimal.Parse(inventoryMatrix.GetValue(lastInventoryColumn, i), CultureInfo.InvariantCulture);

                        Dictionary<DateTime, decimal> incrementations = new Dictionary<DateTime, decimal>();

                        for (int j = 0; j < incrementColumns.Count; j++)
                        {
                            DateTime incrementDate = DateTime.Parse(incrementColumns[j].Split(DynamicColumnNameSeparator)[1]);

                            if (incrementDate >= inventoryDate)
                            {
                                string incrementValueString = inventoryMatrix.GetValue(incrementColumns[j], i);
                                decimal incrementValue = Decimal.Parse(String.IsNullOrEmpty(incrementValueString) ? "0" : incrementValueString, CultureInfo.InvariantCulture);

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

                return result;
            }
            catch (Exception ex)
            {
                throw GeneralSystemError.Exception(
                    $"{nameof(Table)}.{nameof(PatientInventory)}.{nameof(Parse)}",
                    ex,
                    new() { { Keys.Description, "pina" } }
                );
            }
        }
    }
}
