
using System;
using System.Collections.Generic;

namespace MedicineTracking.Utility
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

        public void SetValue(string columnName, int rowNr, string value)
        {
            Rows[rowNr][Array.FindIndex(Signature, name => name == columnName)] = value;
        }

        public int GetIndex(string columnName, string value)
        {
            int result = -1;

            for (int i = 0; i < Rows.Count; i++)
            {
                if (GetValue(columnName, i) == value)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
    }
}
