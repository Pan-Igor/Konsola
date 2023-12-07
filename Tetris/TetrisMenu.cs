using Pytong;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsola.Tetris
{
    class TetrisMenu : UIelement
    {
        bool exit = false;

        public TetrisMenu()
        {
            options.Add(new Option("Graj w Tetris KOLESZKO"));
            options.Add(new Option("Wyjdź"));
        }

        protected override void Accept()
        {
            switch (selectedIndex)
            {
                case 0:
                    {
                        TetrisEngine tetris = new TetrisEngine();
                        tetris.tetrisLoop();
                        break;
                    }
                case 1:
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
                " #####  ####  #####  ###    #   ##  ",
                "   #    #       #    #  #   #  #  # ",
                "   #    ###     #    #  #   #   #   ",
                "   #    #       #    ###    #    #  ",
                "   #    #       #    #  #   #  #  # ",
                "   #    ####    #    #   #  #   ##  "
            };

            while (!exit)
            {
                drawTitle();

                Console.SetCursorPosition(0, Console.CursorTop + 4);
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
                        Console.WriteLine(options[i].GetOptionText() + "        ");
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
