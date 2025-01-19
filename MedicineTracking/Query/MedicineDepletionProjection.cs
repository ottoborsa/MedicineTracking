
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

        static string depletion_date = nameof(depletion_date);

        public static string[] Signature { get; private set; } = new string[]
        {
            patient_id,
            patient_name,
            Table.PatientInventory.medicine_id,
            Table.PatientInventory.medicine_name,
            depletion_date
        };



        // Tehát szeretném azt látni, mikor fogy el, naptári napokon



        public static Matrix GetResult(List<PatientInventory> patientInventories, List<MedicineDosage> medicineDosages)
        {
            Matrix result = new Matrix(Signature);


            // iterate on patients
            foreach (PatientInventory patient in patientInventories)
            {

                // iterate on inventory records
                foreach (PatientInventoryRecord inventoryRecord in patient.PatientInventoryRecords)
                {
                    decimal sum = 0;


                    // iterate on days
                    foreach (DateTime day in DateTools.EachDay(inventoryRecord.InventoryDate, DateTime.Parse(DateTools.ForeverDateString)))
                    {


                        // iterate in incrementations
                        foreach (KeyValuePair<DateTime, decimal> incrementation in inventoryRecord.Incrementations.Where(i => i.Key.Day == day.Day).ToList())
                        {
                            sum += incrementation.Value;
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
                                        sum += value;
                                    }
                                }
                            }
                        }

                        ;

                    }


                    result.AddRow(new string[]
                    {
                        patient.PatientId,
                        patient.PatientName,
                        inventoryRecord.MedicineId,
                        inventoryRecord.MedicineName,
                        sum.ToString().Replace(',', '.')
                    });
                }
            }

            return result;
        }
    }
}
