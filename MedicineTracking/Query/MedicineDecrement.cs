

using System;
using System.Collections.Generic;

using MedicineTracking.Model;
using MedicineTracking.Utility;


namespace MedicineTracking.Query
{
    internal static class MedicineDecrement
    {


        public static string[] Signature = new string[]
        {
            Table.PatientInventory.medicine_id,
            Table.PatientInventory.medicine_name,
            "quantity_decrement"
        };



        // Tehát azt szeretném tudni, hogy az egyes gyógyszerfajtákból mennyi fogyott egy hónapban.


        public static Matrix GetResult(DateTime dateFrom, DateTime dateTo, List<PatientInventory> patientInventories, List<MedicineDosage> medicineDosages)
        {

            Matrix result = new Matrix(Signature);

            foreach (PatientInventory patient in patientInventories)
            {
                foreach (PatientInventoryRecord inventoryRecord in patient.PatientInventoryRecords)
                {
                    if (result.GetIndex(Table.PatientInventory.medicine_id, inventoryRecord.MedicineId) != -1)
                    {
                        result.AddRow(new string[] { inventoryRecord.MedicineId, inventoryRecord.MedicineName, "" });
                    }
                }
            }


            // iterate on result (medicines)
            for (int i = 0; i < result.GetSize(); i++)
            {
                string medicineId = result.GetValue(Table.PatientInventory.medicine_id, i);

                // iterate on dosage of patients
                foreach (MedicineDosage dosage in medicineDosages)
                {

                    // iterate on days of the query
                    foreach (DateTime day in DateTools.EachDay(dateFrom, dateTo))
                    {

                        // iterate on dosage records
                        foreach (MedicineDosageRecord dosageRecord in dosage.MedicineDosageRecords)
                        {

                            if (
                                dosageRecord.MedicineId == medicineId &&
                                dosageRecord.ValidFrom <= day &&
                                dosageRecord.ValidTo >= day
                               )
                            {

                            }
                        }
                    }
                }
            }



            return result;
        }
    }
}
