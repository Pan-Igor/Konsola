using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Pytong
{
    class UIelement
    {
        protected string[] tytul;

        ConsoleKey[] controlKeys = new ConsoleKey[5];
        ConsoleKey[] defaultkeys = new ConsoleKey[5]
        {
            ConsoleKey.W,
            ConsoleKey.S,
            ConsoleKey.A,
            ConsoleKey.D,
            ConsoleKey.Enter
        };

        protected void drawTitle()
        {
            int y = 0;
            foreach (string s in tytul)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '#')
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(i * 2 + 2, y + 2);
                        Console.Write("  ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                y++;
            }
        }

        public UIelement()
        {
            if(!File.Exists("settings.pref"))
            {
                File.WriteAllText("settings.pref", defaultkeys[0] + "\n" + defaultkeys[1] + "\n" + defaultkeys[2] + "\n" + defaultkeys[3] + "\n" + defaultkeys[4] + "\n");
            }
            using (StreamReader sr = new StreamReader("settings.pref"))
            {
                int v;
                for (int i = 0; i < 5; i++)
                {
                    if (Int32.TryParse(sr.ReadLine(), out v))
                    {
                        controlKeys[i] = (ConsoleKey)v;
                    }
                    else
                    {
                        controlKeys[i] = defaultkeys[i];
                    }
                }
            }
        }

        public struct int2
        {
            public int x;
            public int y;
            public int2(int xx, int yy)
            {
                x = xx;
                y = yy;
            }
        }

        protected List<Option> options = new List<Option>();
        protected int selectedIndex;

        protected virtual int2 UIcontrols()
        {
            ConsoleKey key = Console.ReadKey().Key;
            int w = (key == ConsoleKey.W) ? 1 : 0;
            int s = (key == ConsoleKey.S) ? -1 : 0;
            int a = (key == ConsoleKey.A) ? -1 : 0;
            int d = (key == ConsoleKey.D) ? 1 : 0;
            int enter = (key == ConsoleKey.Enter) ? 20 : 0;
            int xinput = a + d + enter;
            int yinput = w + s + enter;

            int2 returninput = new int2(xinput, yinput);
            return returninput;
        }

        protected virtual void Accept()
        {

        }
    }
}
