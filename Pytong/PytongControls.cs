using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;

namespace Pytong
{
    class PytongControls : UIelement
    {
        bool finished = false;


        ConsoleKey[] keys = new ConsoleKey[4];
        ConsoleKey[] defaultkeys = new ConsoleKey[4]
        {
            ConsoleKey.W,
            ConsoleKey.S,
            ConsoleKey.A,
            ConsoleKey.D
        };


        void readNewKey(int keyIndex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(4, 10);
            Console.WriteLine("Wciśnij nowy klawisz, by zamienić " + keys[keyIndex].ToString());

            Console.SetCursorPosition(10, 11);
            ConsoleKey key = Console.ReadKey().Key;
            if (keys.Where(x => x == key).ToList().Count > 0 && keys[keyIndex] != key)
            {
                Console.SetCursorPosition(4, 12);
                Console.WriteLine("Błąd! klawisz jest już wykożystywany");
                Thread.Sleep(1000);
            }
            else
                keys[keyIndex] = key;

            Console.Clear();
        }

        public PytongControls()
        {
            if (!File.Exists(Program.PytongControllsFilePath))
            {
                GenerateDefaultFile();
            }
            using (StreamReader sr = new StreamReader(Program.PytongControllsFilePath))
            {
                int v;
                bool error = false;
                for (int i = 0; i < 4; i++)
                {
                    if (Int32.TryParse(sr.ReadLine(), out v))
                    {
                        keys[i] = (ConsoleKey)v;
                    }
                    else
                    {
                        keys[i] = defaultkeys[i];
                        error = true;
                    }
                }
                sr.Close();
                if (error == true)
                {
                    saveSettings();
                }
            }


            options.Add(new Option("Góra"));
            options.Add(new Option("Dół"));
            options.Add(new Option("Lewo"));
            options.Add(new Option("Prawo"));
            options.Add(new Option("Zapisz zmiany"));
            options.Add(new Option("Powrót"));   
        }

        void saveSettings()
        {
            //using (StreamWriter sw = new StreamWriter(Program.PytongControllsFilePath))
            //{
            /*
                góra
                dół
                lewo
                prawo
                wybór
            */
            string SavedSettings = "";
            SavedSettings += ((int)keys[0]).ToString() + "\n";
            SavedSettings += ((int)keys[1]).ToString() + "\n";
            SavedSettings += ((int)keys[2]).ToString() + "\n";
            SavedSettings += ((int)keys[3]).ToString() + "\n";
            //SavedSettings += ((int)keys[4]).ToString() + "\n";

            File.WriteAllText(Program.PytongControllsFilePath, SavedSettings);
            //sw.Close();
            //}
        }

        protected override void Accept()
        {
            switch (selectedIndex)
            {
                case 0:
                    {
                        readNewKey(0);
                        break;
                    }
                case 1:
                    {
                        readNewKey(1);
                        break;
                    }
                case 2:
                    {
                        readNewKey(2);
                        break;
                    }
                case 3:
                    {
                        readNewKey(3);
                        break;
                    }
                case 4:
                    {
                        saveSettings();
                        finished = true;
                        Console.Clear();
                        break;
                    }
                case 5:
                    {
                        finished = true;
                        Console.Clear();
                        break;
                    }
            }
        }

        public void settingsLoop()
        {
            Console.Clear();
            tytul = new string[6]
            {
                "  ##   #####  ####  ###     #       #  ####  #######  ###  ",
                " #  #    #    #     #  #    #       #  #          #  #   # ",
                "  #      #    ###   #  #    #       #  ###       #   #   # ",
                "   #     #    #     ##      #   #   #  #        #    ##### ",
                " #  #    #    #     # #     #   #   #  #   #   #     #   # ",
                "  ##     #    ####  #  #     ### ###   #####  ###### #   # "
            };
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            selectedIndex = 0;   

            while (!finished)
            {
                drawTitle();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(0, Console.CursorTop + 4);
                for (int i = 0; i < options.Count; i++)
                {
                    Console.Write(" ");
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(options[i].GetSelectedOptionText());
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.Write(options[i].GetOptionText() + "    ");
                    }
                    if (i < 4)
                        Console.WriteLine(" Klawisz: " + keys[i]);
                    Console.WriteLine("");
                }
                Console.ForegroundColor = ConsoleColor.Black;
                int2 consoleInput = UIcontrols();

                if (consoleInput.x == 20 && consoleInput.y == 20)
                    Accept();
                else
                    selectedIndex -= consoleInput.y;

                if (selectedIndex < 0)
                    selectedIndex = options.Count - 1;
                if (selectedIndex >= options.Count)
                    selectedIndex = 0;
            }
        }
        public static void GenerateDefaultFile()
        {
            //using (StreamWriter sw = new StreamWriter(Program.PytongControllsFilePath))
            //{
            /*
                góra
                dół
                lewo
                prawo
                wybór
            */
            string defaultSettings = "";
            defaultSettings += ((int)ConsoleKey.W).ToString() + "\n";
            defaultSettings += ((int)ConsoleKey.S).ToString() + "\n";
            defaultSettings += ((int)ConsoleKey.A).ToString() + "\n";
            defaultSettings += ((int)ConsoleKey.D).ToString() + "\n";
            defaultSettings += ((int)ConsoleKey.Enter).ToString() + "\n";

            File.WriteAllText(Program.PytongControllsFilePath, defaultSettings);
            //sw.Close();
            //}
        }
    }
}
