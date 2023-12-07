using Konsola.Pytong;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pytong
{
    class pytongMenu : UIelement
    {
        bool exit = false;


        public pytongMenu()
        {
            options.Add(new Option("Start"));
            options.Add(new Option("Sterowanie"));
            options.Add(new Option("Ustawienia gry"));
            options.Add(new Option("Opcja 3"));
            options.Add(new Option("Wyjście"));
            
        }

        protected override int2 UIcontrols()
        {
            return base.UIcontrols();
        }

        protected override void Accept()
        {
            switch(selectedIndex)
            {
                case 0:
                    {
                        PytongEngine pytong= new PytongEngine();
                        pytong.GameLoop();

                        break;
                    }
                case 1:
                    {
                        PytongControls settings = new PytongControls();
                        settings.settingsLoop(); ;

                        break;
                    }
                case 2:
                    {
                        PytongGameplaySettings settings = new PytongGameplaySettings();
                        settings.settingsLoop(); ;

                        break;
                    }
                case 4:
                    {
                        exit = true;
                        break;
                    }
            }
        }

        public void menuLoop()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            tytul = new string[6]
            {
                " ####   #     #  #####   ###   #    #   ### ",
                " #   #   #   #     #    #   #  ##   #  #   #",
                " #   #    # #      #    #   #  # #  #  #    ",
                " ####      #       #    #   #  #  # #  #  ##",
                " #         #       #    #   #  #   ##  #   #",
                " #         #       #     ###   #    #   ### "
            };

            selectedIndex = 0;
            
            while (!exit)
            {
                drawTitle();

                Console.SetCursorPosition(0, Console.CursorTop+4);
                for (int i = 0; i < options.Count; i++)
                {
                    Console.Write("     ");
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(options[i].GetSelectedOptionText());
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.WriteLine(options[i].GetOptionText()+ "        ");
                    }
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
            Console.Clear();
        }
    }
}
