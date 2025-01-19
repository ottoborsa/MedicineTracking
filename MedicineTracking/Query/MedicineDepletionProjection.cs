
using System;
using System.Collections.Generic;

using MedicineTracking.Utility;
using MedicineTracking.Model;


namespace MedicineTracking.Query
{
    internal static class MedicineDepletionProjection
    {


        public static string[] Signature { get; private set; } = new string[]
        {
            "patient_id",
            "patient_name",
            Table.PatientInventory.medicine_id,
            Table.PatientInventory.medicine_name,
            "zero_quantity_threshold_date"
        };



        // Tehát szeretném azt látni, mikor fogy el, naptári napokon



        public static Matrix GetResult(DateTime dateFrom, DateTime dateTo, List<PatientInventory> patientInventories, List<MedicineDosage> medicineDosages)
        {




            return new Matrix(Signature);
        }
    }
}
