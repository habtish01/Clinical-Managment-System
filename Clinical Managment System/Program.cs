using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinical_Managment_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(GlobalExceptionHandler);

            Application.Run(new ClinicalManagmentSystemForm());
            //Application.Run(new HomeClinicalSystem());
            //Application.Run(new InPatient_DashboardForm());

           
        }

        // Global exception handler
        private static void GlobalExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
          
            MessageBox.Show($"An unexpected error occurred: {e.Exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
