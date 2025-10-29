using System;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace FractionCalculator
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();// делает кнопки и элементы «современными» (как в Windows 10/11).
            Application.SetCompatibleTextRenderingDefault(false);//Устанавливает по умолчанию для всех новых элементов управления  использование GDI, а не GDI+.
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);//приложение корректно масштабируется на экранах с высоким DPI
            Application.Run(new Form1());// запускает главное окно (Form1).
        }
    }
}
