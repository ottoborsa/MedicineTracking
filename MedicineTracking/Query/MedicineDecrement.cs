﻿

using System;
using System.Collections.Generic;
using System.Globalization;

using MedicineTracking.Core;
using MedicineTracking.Messaging;
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
            if (dateFrom > dateTo || dateTo.Subtract(dateFrom).TotalDays > 365)
            {
                throw new SerializedException("InvalidDateRange");
            }

            List<PatientInventory> patientInventories = db.Table_PatientInventory;
            List<MedicineDosage> medicineDosages = db.Table_PatientDosage;

            if (patientInventories.Count == 0 || medicineDosages.Count == 0)
            {
                throw new SerializedException("InsufficientDataBase");
            }

            Matrix result = new Matrix(Signature);

            foreach (PatientInventory patient in patientInventories)
            {
                foreach (PatientInventoryRecord inventoryRecord in patient.PatientInventoryRecords)
                {
                    if (result.GetIndex(Table.PatientInventory.medicine_id, inventoryRecord.MedicineId) == -1)
                    {
                        if (inventoryRecord.InventoryDate > dateFrom)
                        {
                            throw new SerializedException("NoInventoryAvailableForDateRange");
                        }

                        result.AddRow(new string[] { inventoryRecord.MedicineId, inventoryRecord.MedicineName, "0" });
                    }
                }
            }

            // iterate on result Matrix (medicine records)
            for (int i = 0; i < result.GetSize(); i++)
            {

                // ProgressBar: 3rd event (of total 3); 50% range of whole
                int progressPercentage = (int)Math.Round((decimal)(i + 1) / (decimal)(result.GetSize()) * 100);
                int progress = 50 + progressPercentage / 2;
                ApplicationInterface.SetProgressBarValue(progress);

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
                                    string sum = ((currentValue + value) / 1.000000000000000000000000000000000m).ToString().Replace(',', '.');

                                    result.SetValue(quantity_decrement, i, sum);
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
