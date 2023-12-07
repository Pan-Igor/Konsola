using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Konsola.Tetris
{
    class TetrisEngine
    {
        int mapWidth;
        int mapHeight;
        char[,] map;

        int mapDeltaX = 4;
        int mapDeltaY = 2;

        int msSinceLevelLoad = 0;
        int lastBlockmove = 0;

        TetrisBlock actualBlock;

        char[,] BlockModel1 = new char[3, 2]
            {
                {'#', '#'},
                {'#', ' '},
                {'#', ' '}
            };
        char[,] BlockModel2 = new char[3, 2]
            {
                {'#', '#'},
                {' ', '#'},
                {' ', '#'}
            };
        char[,] BlockModel3 = new char[3, 2]
            {
                {' ', '#'},
                {'#', '#'},
                {'#', ' '}
            };
        char[,] BlockModel4 = new char[3, 2]
            {
                {'#', ' '},
                {'#', '#'},
                {' ', '#'}
            };
        char[,] BlockModel5 = new char[2, 2]
            {
                {'#', '#'},
                {'#', '#'}
            };
        char[,] BlockModel6 = new char[4, 1]
            {
                {'#'},
                {'#'},
                {'#'},
                {'#'}
            };

        public TetrisEngine()
        {
            mapWidth = 10;
            mapHeight = 20;
            map = new char[mapWidth, mapHeight];
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    map[x, y] = ' ';
                }
            }
        }

        void drawBorders()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorSize = 2;
            for (int x = 0; x < mapWidth + 2; x++)
            {
                Console.SetCursorPosition(mapDeltaX + 2* x, mapDeltaY);
                Console.Write("  ");
            }
            for (int x = 0; x < mapWidth + 2; x++)
            {
                Console.SetCursorPosition(mapDeltaX + 2* x, mapDeltaY+1 + mapHeight);
                Console.Write("  ");
            }
            for (int y = 0; y < mapHeight + 1; y++)
            {
                Console.SetCursorPosition(mapDeltaX, mapDeltaY + y);
                Console.Write("  ");
            }
            for (int y = 0; y < mapHeight + 1; y++)
            {
                Console.SetCursorPosition(mapDeltaX + 2 + mapWidth * 2, mapDeltaY + y);
                Console.Write("  ");
            }
            Console.ResetColor();
        }

        void Render()
        {
            drawBorders();
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if(map[x, y] == ' ')
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        
                        Console.SetCursorPosition(mapDeltaX + 2 + x*2, mapDeltaY + mapHeight - y);
                        Console.Write("  ");
                    }
                }
            }

            actualBlock.drawBlock(mapDeltaX, mapHeight+mapDeltaY, ConsoleColor.Yellow);
            
        }


        public void tetrisLoop()
        {
            Console.Clear();
            bool isAlive = true;

            actualBlock = new TetrisBlock(BlockModel2, mapWidth/2-BlockModel2.GetLength(0)/2, mapHeight);

            while(isAlive)
            {
                if(Console.KeyAvailable)
                {
                    if(Console.ReadKey().Key == ConsoleKey.UpArrow)
                    {
                        actualBlock.TemporaryRotateBlockRight();
                        actualBlock.applyRotation();
                    }
                    while(Console.KeyAvailable)
                    {
                        Console.ReadKey(false);
                    }
                }
                if(msSinceLevelLoad - lastBlockmove > 5)
                {
                    lastBlockmove = msSinceLevelLoad;
                    actualBlock.updateBlock();
                }

                Render();
                Thread.Sleep(100);
                msSinceLevelLoad++;
            }
        }
    }
}
