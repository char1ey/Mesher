using System;
using System.Windows.Forms;
using Mesher.Core.Test;

namespace Mesher.Core
{
    static class Program
    {
        public static MesherApplication MesherApplication { get; set; }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MesherApplication = new MesherApplication();

            Application.Run(MesherApplication.MainWindow);
        }
    }
}
