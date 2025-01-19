
using System;

namespace MedicineTracking.Model
{
    internal class MedicineDosageRecord
    {
        public string MedicineId { get; private set; }

        public Table.PatientDosage.DosageType DosageType { get; private set; }

        public float DosageValue { get; private set; }

        public string DosageTypeParameter { get; private set; }

        public DateTime ValidFrom { get; private set; }

        public DateTime ValidTo { get; private set; }


        public MedicineDosageRecord(
            string medicineId,
            Table.PatientDosage.DosageType dosageType,
            float dosageValue,
            string dosageTypeParameter,
            DateTime validFrom,
            DateTime validTo
        )
        {
            MedicineId = medicineId;
            DosageType = dosageType;
            DosageValue = dosageValue;
            DosageTypeParameter = dosageTypeParameter;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }
    }
}
