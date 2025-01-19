



using MedicineTracking.Model;
using System.Collections.Generic;

namespace MedicineTracking.Query
{
    internal static class MedicineDecrement
    {


        public static string[] Signature = new string[]
        {
            "medicine_id",
            "medicine_name",
            "quantity_decrement"
        };



        // Tehát azt szeretném tudni, hogy az egyes gyógyszerfajtákból mennyi fogyott egy hónapban.


        public static CsvParser.Matrix GetResult(List<PatientInventory> patientInventories, List<MedicineDosage> medicineDosages)
        {




            return new CsvParser.Matrix(Signature);
        }





    }
}
