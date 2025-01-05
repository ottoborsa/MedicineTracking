
using System.Collections.Generic;

namespace MedicineTracking.Model
{
    internal class MedicineDosage
    {
        public string PatientId { get; private set; }

        public string PatientName { get; private set; }

        public List<MedicineDosageRecord> MedicineDosageRecords { get; private set; }

        public MedicineDosage(string patientId, string patientName, List<MedicineDosageRecord> medicineDosageRecords)
        {
            PatientId = patientId;
            PatientName = patientName;
            MedicineDosageRecords = medicineDosageRecords;
        }
    }
}
