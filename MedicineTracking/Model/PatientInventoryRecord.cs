
using System;

namespace MedicineTracking
{
    internal class PatientInventoryRecord
    {

        public string MedicineId { get; private set; }

        public string MedicineName { get; private set; }

        public DateTime Date { get; private set; }

        public int MedicineCount { get; private set; }


        public PatientInventoryRecord(string medicineId, string medicineName, DateTime date, int medicineCount)
        {
            MedicineId = medicineId;
            MedicineName = medicineName;
            Date = date;
            MedicineCount = medicineCount;
        }
    }
}
