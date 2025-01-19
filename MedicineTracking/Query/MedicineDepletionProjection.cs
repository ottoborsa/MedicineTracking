
using System;
using System.Collections.Generic;
using System.Linq;

using MedicineTracking.Utility;
using MedicineTracking.Model;


namespace MedicineTracking.Query
{
    internal static class MedicineDepletionProjection
    {


        static string patient_id = nameof(patient_id);

        static string patient_name = nameof(patient_name);

        static string last_positive_amount_day = nameof(last_positive_amount_day);

        public static string[] Signature { get; private set; } = new string[]
        {
            patient_id,
            patient_name,
            Table.PatientInventory.medicine_id,
            Table.PatientInventory.medicine_name,
            last_positive_amount_day
        };




        public static Matrix GetResult(List<PatientInventory> patientInventories, List<MedicineDosage> medicineDosages)
        {
            Matrix result = new Matrix(Signature);


            // iterate on patients
            foreach (PatientInventory patient in patientInventories)
            {

                // iterate on inventory records
                foreach (PatientInventoryRecord inventoryRecord in patient.PatientInventoryRecords)
                {

                    DateTime lastPositiveAmountDay = DateTime.Parse(DateTools.ForeverDateString);
                    decimal amount = inventoryRecord.MedicineCount;

                    // iterate on days
                    foreach (DateTime day in DateTools.EachDay(inventoryRecord.InventoryDate, DateTime.Parse(DateTools.ForeverDateString)))
                    {

                        // iterate in incrementations
                        foreach (KeyValuePair<DateTime, decimal> incrementation in inventoryRecord.Incrementations.Where(i => i.Key.Day == day.Day).ToList())
                        {
                            amount += incrementation.Value;
                        }

                        // (only one step of iteration for the given patient)
                        foreach (MedicineDosage dosage in medicineDosages.Where(d => d.PatientId == patient.PatientId).ToList())
                        {

                            // iterate on dosage records
                            foreach (MedicineDosageRecord dosageRecord in dosage.MedicineDosageRecords)
                            {

                                if (
                                    dosageRecord.MedicineId == inventoryRecord.MedicineId &&
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
                                        amount -= value;
                                    }
                                }
                            }
                        }

                        if (amount <= 0)
                        {
                            lastPositiveAmountDay = day.AddDays(-1);
                            break;
                        }
                    }

                    result.AddRow(new string[]
                    {
                        patient.PatientId,
                        patient.PatientName,
                        inventoryRecord.MedicineId,
                        inventoryRecord.MedicineName,
                        lastPositiveAmountDay.ToString(DateTools.DayPattern),
                    });
                }
            }

            return result;
        }
    }
}
