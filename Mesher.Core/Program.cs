using System;
using System.Windows.Forms;
using Mesher.Core.Test;

namespace Mesher.Core
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GCoreTest());
        }
    }
}
