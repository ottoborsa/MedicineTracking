
using System;
using System.Collections.Generic;

namespace MedicineTracking
{
    internal class PatientInventoryRecord
    {

        public string MedicineId { get; private set; }

        public string MedicineName { get; private set; }

        public DateTime InventoryDate { get; private set; }

        public decimal MedicineCount { get; private set; }

        public Dictionary<DateTime, decimal> Incrementations { get; private set; }


        public PatientInventoryRecord(
            string medicineId,
            string medicineName,
            DateTime inventoryDate,
            decimal medicineCount,
            Dictionary<DateTime, decimal> incrementations
        )
        {
            MedicineId = medicineId;
            MedicineName = medicineName;
            InventoryDate = inventoryDate;
            MedicineCount = medicineCount;
            Incrementations = incrementations;
        }
    }
}
