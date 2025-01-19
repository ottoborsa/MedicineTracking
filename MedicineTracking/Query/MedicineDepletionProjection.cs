


using MedicineTracking.Model;
using System;
using System.Collections.Generic;

namespace MedicineTracking.Query
{
    internal static class MedicineDepletionProjection
    {


        public static string[] Signature { get; private set; } = new string[]
        {
            "patient_id",
            "patient_name",
            "medicine_id",
            "medicine_name",
            "zero_quantity_threshold_date"
        };



        // Tehát szeretném azt látni, mikor fogy el, naptári napokon



        public static CsvParser.Matrix GetResult(DateTime dateFrom, DateTime dateTo, List<PatientInventory> patientInventories, List<MedicineDosage> medicineDosages)
        {




            return new CsvParser.Matrix(Signature);
        }
    }
}
