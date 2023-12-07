using System;
using System.Collections.Generic;
using System.Text;

namespace Pytong
{
    class EasyInput
    {
        public ConsoleKey GetKey()
        {
            if (!Console.KeyAvailable) return ConsoleKey.NoName;

            return Console.ReadKey().Key;
        }
    }
}
