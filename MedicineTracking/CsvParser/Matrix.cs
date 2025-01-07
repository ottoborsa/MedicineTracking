
using System;
using System.Collections.Generic;

namespace MedicineTracking.CsvParser
{
    internal class Matrix
    {

        public string[] Signature { get; private set; }

        private Dictionary<int, string[]> Rows;


        public Matrix(string[] signature)
        {
            Signature = signature;
            Rows = new Dictionary<int, string[]>();
        }


        public int GetSize()
        {
            return Rows.Count;
        }

        public void AddRow(string[] row)
        {
            Rows.Add(Rows.Count, row);
        }

        public string GetValue(string columnName, int rowNr)
        {
            return Rows[rowNr][Array.FindIndex(Signature, name => name == columnName)];
        }
    }
}
