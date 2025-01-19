﻿
using System;
using System.Collections.Generic;

using MedicineTracking.Utility;
using MedicineTracking.Model;


namespace MedicineTracking.Query
{
    internal static class MedicineDepletionProjection
    {


        static string patient_id = nameof(patient_id);

        static string patient_name = nameof(patient_name);

        static string zero_quantity_threshold_date = nameof(zero_quantity_threshold_date);

        public static string[] Signature { get; private set; } = new string[]
        {
            patient_id,
            patient_name,
            Table.PatientInventory.medicine_id,
            Table.PatientInventory.medicine_name,
            zero_quantity_threshold_date
        };



        // Tehát szeretném azt látni, mikor fogy el, naptári napokon



        public static Matrix GetResult(List<PatientInventory> patientInventories, List<MedicineDosage> medicineDosages)
        {
            Matrix result = new Matrix(Signature);


            // iterate on patients
            foreach (PatientInventory patient in patientInventories)
            {

                // iterate on inventory records
                foreach (PatientInventoryRecord record in patient.PatientInventoryRecords)
                {
                    string depletionDate = "";



                    foreach (DateTime day in DateTools.EachDay(record.InventoryDate, DateTime.Parse(DateTools.ForeverDateString)))
                    {

                    }











                    result.AddRow(new string[]
                    {
                        patient.PatientId,
                        patient.PatientName,
                        record.MedicineId,
                        record.MedicineName,
                        depletionDate
                    });
                }
            }

            return result;
        }
    }
}
