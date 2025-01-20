
using System;
using System.Windows.Forms;

namespace MedicineTracking
{
    internal static class Program
    {
        public static Form1 MainForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new Form1();

            Application.Run(MainForm);
        }
    }
}
