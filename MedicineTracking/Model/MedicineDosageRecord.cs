
using System;

namespace MedicineTracking.Model
{
    internal class MedicineDosageRecord
    {
        public string MedicineId { get; private set; }

        public int DailyDosage { get; private set; }

        public DateTime ValidFrom { get; private set; }

        public DateTime ValidTo { get; private set; }


        public MedicineDosageRecord(string medicineId, int dosage, DateTime from, DateTime to)
        {
            MedicineId = medicineId;
            DailyDosage = dosage;
            ValidFrom = from;
            ValidTo = to;
        }
    }
}
