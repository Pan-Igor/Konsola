using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pytong
{
    class PytongEngine
    {
        int speed;
        bool exit = false;

        int xDelta = 4;
        int yDelta = 2;

        ConsoleKey[] controlKeys = new ConsoleKey[5];
        ConsoleColor pytongColor;
        int pointsMultiplier;

        int mapWidth = 15;
        int mapHeight = 20;

        List<CzenscWenza> pytong = new List<CzenscWenza>();
        int dlugosc = 3;

        int xdir = 0;
        int ydir = 0;

        int japkox;
        int japkoy;

        int applesEaten;
        void controls()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey k = Console.ReadKey().Key;
                if (k == controlKeys[0])
                {
                    if (ydir != -1)
                    {
                        ydir = 1;
                        xdir = 0;
                    }
                }
                if (k == controlKeys[1])
                {
                    if (ydir != 1)
                    {
                        ydir = -1;
                        xdir = 0;
                    }
                }
                if (k == controlKeys[2])
                {
                    if (xdir != 1)
                    {
                        xdir = -1;
                        ydir = 0;
                    }
                }
                if (k == controlKeys[3])
                {
                    if (xdir != -1)
                    {
                        xdir = 1;
                        ydir = 0;
                    }
                }


            }/*
            EasyInput input = new EasyInput();
            */
        }

        ConsoleKey[] defaultkeys = new ConsoleKey[5]
        {
            ConsoleKey.W,
            ConsoleKey.S,
            ConsoleKey.A,
            ConsoleKey.D,
            ConsoleKey.Enter
        };

        public PytongEngine()
        {
            if (!File.Exists(Program.PytongControllsFilePath))
            {
                PytongControls.GenerateDefaultFile();
            }
            using (StreamReader sr = new StreamReader(Program.PytongControllsFilePath))
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
                sr.Close();
            }
            using (StreamReader sr = new StreamReader(Program.PytongSettingsFilePath))
            {
                int v;

                if (Int32.TryParse(sr.ReadLine(), out v))
                {
                    speed = v * 3;
                }
                else
                {
                    speed = 10;
                }

                if (Int32.TryParse(sr.ReadLine(), out v))
                {
                    pytongColor = (ConsoleColor)v;
                }
                else
                {
                    pytongColor = ConsoleColor.Yellow;
                }
                sr.Close();
            }
        }

        void drawBorders()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorSize = 2;
            for (int x = 0; x < mapWidth + 2; x++)
            {
                Console.SetCursorPosition(xDelta + 2 * x, yDelta);
                Console.Write("  ");
            }
            for (int x = 0; x < mapWidth + 2; x++)
            {
                Console.SetCursorPosition(xDelta + 2 * x, yDelta + 1 + mapHeight);
                Console.Write("  ");
            }
            for (int y = 0; y < mapHeight + 1; y++)
            {
                Console.SetCursorPosition(xDelta, yDelta + y);
                Console.Write("  ");
            }
            for (int y = 0; y < mapHeight + 1; y++)
            {
                Console.SetCursorPosition(xDelta + 2 + mapWidth * 2, yDelta + y);
                Console.Write("  ");
            }
        }
        void render()
        {
            foreach (CzenscWenza cz in pytong)
            {
                int x = xDelta + 2 + cz.x * 2;
                int y = yDelta + mapHeight - cz.y;
                Console.BackgroundColor = pytongColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(cz.model);
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(japkox * 2 + xDelta + 2, mapHeight - japkoy + yDelta);
            Console.Write("  ");
            Console.SetCursorPosition(mapWidth, mapHeight + yDelta + 2);
            Console.Write("SCORE :" + applesEaten * pointsMultiplier);
        }

        public void GameLoop()
        {
            pointsMultiplier = speed;

            Console.CursorVisible = false;
            Console.Clear();
            applesEaten = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            int xpos = (int)mapWidth / 2;
            int ypos = (int)mapHeight / 2;

            Random rnd = new Random();
            int position;
            int yp = 0;
            int xp = 0;
            position = rnd.Next(0, mapWidth * mapHeight - dlugosc);
            for (int i = 1; i <= position; i++)
            {
                xp = i % mapWidth;
                yp = (i - xp) / mapWidth;
                if (pytong.Where(x => x.x == xp && x.y == yp).ToList().Count > 0)
                    position++;
            }
            japkox = xp;
            japkoy = yp;

            while (!exit)
            {
                drawBorders();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Black;
                controls();

                xpos += xdir;
                ypos += ydir;
                if (xpos < 0)
                    xpos = mapWidth - 1;
                if (xpos >= mapWidth)
                    xpos = 0;
                if (ypos < 0)
                    ypos = mapHeight - 1;
                if (ypos >= mapHeight)
                    ypos = 0;

                if (pytong.Where(x => x.x == xpos && x.y == ypos).ToList().Count > 0 && (xdir != 0 || ydir != 0))
                {
                    exit = true;
                    continue;
                }
                if (xpos == japkox && ypos == japkoy)
                {
                    dlugosc++;
                    position = rnd.Next(0, (mapWidth * mapHeight - dlugosc));
                    for (int i = 1; i <= position; i++)
                    {
                        xp = i % mapWidth;
                        yp = (i - xp) / mapWidth;
                        if (pytong.Where(x => x.x == xp && x.y == yp).ToList().Count > 0)
                            position++;
                    }
                    japkox = xp;
                    japkoy = yp;
                    applesEaten++;
                }

                bool lookingUp = (ydir != 0) ? true : false;
                int dir = (lookingUp) ? ydir : xdir;
                pytong.Add(new CzenscWenza(xpos, ypos, lookingUp, dir));
                CzenscWenza doUsuniecia = null;
                foreach (CzenscWenza cw in pytong)
                {
                    cw.UpdatePart();
                    if (cw.wiek > dlugosc)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(cw.x * 2 + xDelta + 2, mapHeight - cw.y + yDelta);
                        Console.Write("  ");
                        doUsuniecia = cw;
                    }
                }
                if (doUsuniecia != null)
                    pytong.Remove(doUsuniecia);
                render();
                Thread.Sleep(1000 / speed);

            }
            Thread.Sleep(500);
            int j = -3;
            for (int i = 0; i < mapHeight + 2 + 2 + 2 + 3 + 4; i++)
            {
                if (i < mapHeight + 2 + 2 + 2)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;

                    Console.SetCursorPosition(xDelta, yDelta + i);
                    for (int x = 0; x < mapWidth * 2 + 4 + 4; x++)
                    {
                        Console.Write(" ");
                    }
                }
                if (j >= 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(xDelta, yDelta + j);
                    for (int x = 0; x < mapWidth * 2 + xDelta + 4; x++)
                    {
                        Console.Write(" ");
                    }
                }
                Thread.Sleep(100);
                j++;
            }
            Console.SetCursorPosition(0, 5);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("               Przegrałeś\n           Pożarłeś " + applesEaten + " jabłek\n              " + applesEaten * pointsMultiplier + " Punktów"); Console.ForegroundColor = ConsoleColor.Black;
            while (Console.KeyAvailable)
                Console.ReadKey(false);
            Console.ReadKey();
            Console.Clear();
            Console.ResetColor();
        }
    }
}