
using System;
using MedicineTracking.Messaging;

namespace MedicineTracking.Utility
{
    internal static class CsvParser
    {

        public static string LineSeparator { get; private set; } = Environment.NewLine;

        public const char CulumnSeparator = ';';




        public static Matrix Parse(string fileContent)
        {
            try
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
            catch (Exception ex)
            {
                throw GeneralSystemError.Exception(nameof(CsvParser), ex);
            }
        }

        public static string FromMatrix(Matrix matrix)
        {
            string fileContent = String.Join(CulumnSeparator.ToString(), matrix.Signature) + LineSeparator;

            for (int i = 0; i < matrix.GetSize(); i++)
            {
                fileContent += String.Join(CulumnSeparator.ToString(), matrix.GetRow(i)) + LineSeparator;
            }

            return fileContent;
        }
    }
}
