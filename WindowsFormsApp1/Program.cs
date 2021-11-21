using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // При изменении данных в БД данные во время Debug сохранятся не будут, для сохранения данных лучше использовать Release
            // Также не забыть поменять стандартные значения (LocalAppData) название папки
#if DEBUG == false
            String dbPathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            String dbPath = Path.Combine(dbPathAppData, "LocalAppData");
            AppDomain.CurrentDomain.SetData("DataDirectory", dbPath);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
