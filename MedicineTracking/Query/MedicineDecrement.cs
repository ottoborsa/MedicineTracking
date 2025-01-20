

using System;
using System.Collections.Generic;
using System.Globalization;
using MedicineTracking.Core;
using MedicineTracking.Model;
using MedicineTracking.Utility;


namespace MedicineTracking.Query
{
    internal static class MedicineDecrement
    {


        public const string quantity_decrement = nameof(quantity_decrement);

        public static string[] Signature { get; private set; } = new string[]
        {
            Table.PatientInventory.medicine_id,
            Table.PatientInventory.medicine_name,
            quantity_decrement
        };



        public static Matrix GetResult(DataBase db, DateTime dateFrom, DateTime dateTo)
        {
            List<PatientInventory> patientInventories = db.Table_PatientInventory;
            List<MedicineDosage> medicineDosages = db.Table_PatientDosage;

            Matrix result = new Matrix(Signature);

            foreach (PatientInventory patient in patientInventories)
            {
                foreach (PatientInventoryRecord inventoryRecord in patient.PatientInventoryRecords)
                {
                    if (result.GetIndex(Table.PatientInventory.medicine_id, inventoryRecord.MedicineId) == -1)
                    {
                        result.AddRow(new string[] { inventoryRecord.MedicineId, inventoryRecord.MedicineName, "0" });
                    }
                }
            }

            // iterate on result Matrix (medicine records)
            for (int i = 0; i < result.GetSize(); i++)
            {

                int progressPercentage = (int)Math.Round((decimal)(i + 1) / (decimal)(result.GetSize()) * 100);
                int progress = 50 + progressPercentage / 2;
                Core.ApplicationInterface.SetProgressBarValue(progress);

                string medicineId = result.GetValue(Table.PatientInventory.medicine_id, i);

                // iterate on patients
                foreach (MedicineDosage patient in medicineDosages)
                {

                    // iterate on days of the query
                    foreach (DateTime day in DateTools.EachDay(dateFrom, dateTo))
                    {

                        // iterate on dosage records
                        foreach (MedicineDosageRecord dosageRecord in patient.MedicineDosageRecords)
                        {

                            if (
                                dosageRecord.MedicineId == medicineId &&
                                dosageRecord.ValidFrom <= day &&
                                dosageRecord.ValidTo >= day
                               )
                            {
                                decimal value = Common.GetDosageOfDay(
                                    day,
                                    dosageRecord.ValidFrom,
                                    dosageRecord.DosageType,
                                    dosageRecord.DosageValue,
                                    dosageRecord.DosageTypeParameter
                                );

                                if (value != 0)
                                {
                                    decimal currentValue = Decimal.Parse(result.GetValue(quantity_decrement, i), CultureInfo.InvariantCulture);

                                    result.SetValue(quantity_decrement, i, (currentValue + value).ToString().Replace(',', '.'));
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
