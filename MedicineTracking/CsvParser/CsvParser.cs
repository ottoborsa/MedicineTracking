
using System;

namespace MedicineTracking.CsvParser
{
    internal static class CsvParser
    {

        public static string LineSeparator { get; private set; } = Environment.NewLine;

        public static char CulumnSeparator { get; private set; } = ';';




        public static Matrix Parse(string fileContent)
        {
            string[] lines = fileContent.Trim().Split(new string[] { LineSeparator }, StringSplitOptions.None);
            string[] signature = lines[0].Split(CulumnSeparator);

            for (int i = 0; i < signature.Length; i++)
            {
                signature[i] = signature[i].Trim();
            }

            Matrix result = new Matrix(signature);


            for (int i = 1; i < lines.Length; i++)
            {
                string[] row = lines[i].Split(CulumnSeparator);

                for (int j = 0; j < row.Length; j++)
                {
                    row[j] = row[j].Trim();
                }

                result.AddRow(row);
            }

            return result;
        }
    }
}
