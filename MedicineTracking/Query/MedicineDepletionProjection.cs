﻿
using System;
using System.Collections.Generic;
using System.Linq;

using MedicineTracking.Utility;
using MedicineTracking.Model;
using MedicineTracking.Core;
using MedicineTracking.Messaging;




namespace MedicineTracking.Query
{
    internal static class MedicineDepletionProjection
    {


        public const string patient_id = nameof(patient_id);

        public const string patient_name = nameof(patient_name);

        public const string depletion_day = nameof(depletion_day);

        public const string remark = nameof(remark);


        private const string ResultCode_NoDepletion = "-";

        private const string ResultCode_NoCurrentValidity = "no_current_validity";

        private const string ResultCode_DepletedMedicine = "depleted";


        public static string[] Signature { get; private set; } = new string[]
        {
            patient_id,
            patient_name,
            Table.PatientInventory.medicine_id,
            Table.PatientInventory.medicine_name,
            depletion_day,
            remark
        };




        public static Matrix GetResult(DataBase db)
        {
            List<PatientInventory> patientInventories = db.Table_PatientInventory;
            List<MedicineDosage> medicineDosages = db.Table_PatientDosage;

            if (patientInventories.Count == 0 || medicineDosages.Count == 0)
            {
                throw new SerializedException("InsufficientDataBase");
            }

            Matrix result = new Matrix(Signature);

            int progressBarSum = 0;
            int progressBarCounter = 0;
            foreach (PatientInventory inventory in patientInventories)
            {
                progressBarSum += inventory.PatientInventoryRecords.Count;
            }

            // iterate on patients
            foreach (PatientInventory patient in patientInventories)
            {

                // iterate on inventory records
                foreach (PatientInventoryRecord inventoryRecord in patient.PatientInventoryRecords)
                {
                    // ProgressBar: 3rd event (of total 3); 50% range of whole
                    progressBarCounter++;
                    int progressPercentage = (int)Math.Round((decimal)(progressBarCounter) / (decimal)(progressBarSum) * 100);
                    int progress = 50 + progressPercentage / 2;
                    ApplicationInterface.SetProgressBarValue(progress);

                    DateTime? depletionDay = null;
                    decimal amount = inventoryRecord.MedicineCount;

                    // iterate on days
                    foreach (DateTime day in DateTools.EachDay(inventoryRecord.InventoryDate, DateTime.Parse(DateTools.ForeverDateString)))
                    {

                        // get incrementation of the day if there is any
                        foreach (KeyValuePair<DateTime, decimal> incrementation in inventoryRecord.Incrementations.Where(i => i.Key == day).ToList())
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
                                        amount = amount < 0 ? 0 : amount;
                                    }
                                }
                            }
                        }

                        if (amount == 0)
                        {
                            depletionDay = day.AddDays(-1);

                            if (
                                inventoryRecord.Incrementations.Keys.Count == 0 ||
                                day >= inventoryRecord.Incrementations.Keys.OrderByDescending(date => date).First()
                               )
                            {
                                break;
                            }
                        }
                    }

                    string remarkField = String.Empty;

                    int currentValidRangeCount =
                        medicineDosages
                            .Where(medicineDosage => medicineDosage.PatientId == patient.PatientId).First()
                            .MedicineDosageRecords
                                .Where(
                                       dosageRecord => dosageRecord.MedicineId == inventoryRecord.MedicineId &&
                                                       dosageRecord.ValidFrom <= DateTools.GetToday() &&
                                                       dosageRecord.ValidTo >= DateTools.GetToday()
                                      ).Count();

                    if (currentValidRangeCount == 0)
                    {
                        remarkField = ResultCode_NoCurrentValidity;
                    }
                    else if (currentValidRangeCount > 0 && depletionDay != null && depletionDay <= DateTools.GetToday())
                    {
                        remarkField = ResultCode_DepletedMedicine;
                    }

                    result.AddRow(new string[]
                    {
                        patient.PatientId,
                        patient.PatientName,
                        inventoryRecord.MedicineId,
                        inventoryRecord.MedicineName,
                        depletionDay != null ? depletionDay?.ToString(DateTools.DayPattern) : ResultCode_NoDepletion,
                        remarkField
                    });
                }
            }

            return result;
        }
    }
}
