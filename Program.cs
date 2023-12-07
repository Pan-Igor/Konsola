using System;

namespace Pytong
{
    class Program
    {
        public static string PytongControllsFilePath = @"pytongControlls.pref";
        public static string PytongSettingsFilePath = @"pytongSettings.pref";

        static void Main(string[] args)
        {
            Console.Clear();

            Console.ResetColor();
            Konsola konsola = new Konsola();
            konsola.ConsoleLoop();
        }
    }
}
