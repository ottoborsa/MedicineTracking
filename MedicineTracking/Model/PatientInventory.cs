
using System.Collections.Generic;

namespace MedicineTracking.Model
{
    internal class PatientInventory
    {
        public string PatientId { get; private set; }

        public string PatientName { get; private set; }

        public List<PatientInventoryRecord> PatientInventoryRecords { get; private set; }

        public PatientInventory(string patientId, string patientName, List<PatientInventoryRecord> patientInventoryRecords)
        {
            PatientId = patientId;
            PatientName = patientName;
            PatientInventoryRecords = patientInventoryRecords;
        }
    }
}
