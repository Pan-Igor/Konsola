using Pytong;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Konsola.Pytong
{
    class PytongGameplaySettings : UIelement
    {
        bool finished;

        Changer colorChanger, speedChanger;

        int[] settings = new int[2];
        static int[] defaultSettings = new int[2]
        {
            10,
            (int)ConsoleColor.Yellow
        };
        ConsoleColor[] awableColors = new ConsoleColor[6]
        {
            ConsoleColor.Yellow,
            ConsoleColor.Blue,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.White,
        };
        public PytongGameplaySettings()
        {
            if(!File.Exists(Program.PytongSettingsFilePath))
            {
                GenerateDefaultSettings();
            }
            using (StreamReader sw = new StreamReader(Program.PytongSettingsFilePath))
            {
                string value;
                for(int i = 0; i < 2; i++)
                {
                    value = sw.ReadLine();
                    int v;
                    if (Int32.TryParse(value, out v))
                    {
                        settings[i] = v;
                    }
                    else
                    {
                        settings[i] = defaultSettings[i];
                    }
                }
                sw.Close();
            }

            speedChanger = new Changer("Prędkość węża", settings[0], 1, 10);
            colorChanger = new Changer("Kolor węża", Array.IndexOf(awableColors, (ConsoleColor)settings[1]), 0, awableColors.Length-1);
            if (!awableColors.Contains((ConsoleColor)settings[1]))
            {
                settings[1] = Array.IndexOf(awableColors, (ConsoleColor)defaultSettings[1]);
            }
            else
            {
                settings[1] = Array.IndexOf(awableColors, (ConsoleColor)settings[1]);
            }

            options.Add(new Option("Zapisz"));
            options.Add(new Option("Powrót"));
        }

        void saveSettings()
        {
            //using (StreamWriter sw = new StreamWriter(Program.PytongSettingsFilePath))
            //{
            /*
                szybkość
                kolor węża
            */
            string SavedSettings = "";
            SavedSettings += settings[0].ToString() + "\n";
            SavedSettings += ((int)awableColors[settings[1]]).ToString() + "\n";
            File.WriteAllText(Program.PytongSettingsFilePath, SavedSettings);
            //    sw.Close();
            //}
        }

        protected override void Accept()
        {
            switch (selectedIndex)
            {
                case 2:
                    {
                        saveSettings();
                        finished = true;
                        Console.Clear();
                        break;
                    }
                case 3:
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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            selectedIndex = 0;
            tytul = new string[6]
            {
                " #       #  #     #   ###   #      ####    ###  ",
                " #       #   #   #   #   #  #     #    #   #  # ",
                " #   #   #    # #    #      #     #    #   #  # ",
                " #   #   #     #     #  ##  #     ######   #  # ",
                " #   #   #     #     #   #  #     #    #   #  # ",
                "  ### ###      #      ###   ####  #    ##  ###  "
            };
            while (!finished)
            {
                drawTitle();

                Console.SetCursorPosition(0, Console.CursorTop + 4);
                Console.Write("     ");
                if (selectedIndex == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("  < " + speedChanger.optionText + " >  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.WriteLine("    " + speedChanger.optionText + "    ");
                }
                Console.WriteLine("         " + settings[0] + "    ");
                Console.WriteLine("");
                Console.Write("     ");
                if (selectedIndex == 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("  < " + colorChanger.optionText + " >  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.WriteLine("    " + colorChanger.optionText + "    ");
                }

                string color = "";
                Console.Write("         ");
                Console.BackgroundColor = awableColors[settings[1]];
                for (int i = 0; i < colorChanger.optionText.Length-1; i++)
                {
                    color += ' ';
                }
                color += ':';
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(color);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("");
                for (int i = 0; i < options.Count; i++)
                {
                    Console.Write("     ");
                    if (i+2 == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(options[i].GetSelectedOptionText());
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.WriteLine(options[i].GetOptionText() + "        ");
                    }
                }

                Console.ForegroundColor = ConsoleColor.Black;
                int2 consoleInput = UIcontrols();
                if (selectedIndex == 0 && consoleInput.x != 20)
                {
                    speedChanger.updateValue(consoleInput.x);
                    settings[0] = speedChanger.value;
                }
                else if (selectedIndex == 1 && consoleInput.x != 20)
                {
                    colorChanger.updateValue(consoleInput.x);
                    settings[1] = colorChanger.value;
                }

                if (consoleInput.x == 20 && consoleInput.y == 20)
                    Accept();
                else
                    selectedIndex -= consoleInput.y;

                if (selectedIndex < 0)
                    selectedIndex = options.Count - 1;
                if (selectedIndex >= options.Count+2)
                    selectedIndex = 0;
            }
        }

        public static void GenerateDefaultSettings()
        {
            File.WriteAllText(Program.PytongSettingsFilePath, defaultSettings[0].ToString() + "\n" + defaultSettings[1].ToString() + "\n");
        }
    }
}
