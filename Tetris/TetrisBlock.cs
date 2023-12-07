using System;
using System.Collections.Generic;
using System.Text;

namespace Konsola.Tetris
{
    class TetrisBlock
    {
        private int blockWidth;
        private int blockHeight;

        public int xpos;
        public int ypos;

        public int BLOCKWIDTH
        {
            get { return blockWidth; }
            private set { blockWidth = value; }
        }
        public int BLOCKHEIGHT
        {
            get { return blockHeight; }
            private set { blockHeight = value; }
        }
        private char[,] tempBlock;
        public char[,] TEMPBLOCK
        {
            get { return tempBlock; }
            private set { tempBlock = value; }
        }
        public char[,] BlockModel;

        void SetWidthHeight()
        {
            BLOCKWIDTH = this.BlockModel.GetLength(0);
            BLOCKHEIGHT = this.BlockModel.GetLength(1);
        }

        public TetrisBlock(char[,] BlockModel, int xpos, int ypos)
        {
            this.BlockModel = BlockModel;
            SetWidthHeight();

            this.xpos = xpos;
            this.ypos = ypos;
        }

        public void TemporaryRotateBlockRight()
        {
            TEMPBLOCK = new char[BLOCKHEIGHT, BLOCKWIDTH];
            for(int x = 0; x < BLOCKWIDTH; x++)
            {
                for (int y = 0; y < BLOCKHEIGHT; y++)
                {
                    TEMPBLOCK[y, x] = BlockModel[blockWidth - 1 - x, /*BLOCKHEIGHT - 1 - */y];
                }
            }
        }
        public void applyRotation()
        {
            if (TEMPBLOCK.GetLength(0) == 0) return;

            BlockModel = TEMPBLOCK;
            SetWidthHeight();
            TEMPBLOCK = new char[0, 0];
        }
        public void drawBlock(int mapLeftCornersX, int mapBottomCornersY, ConsoleColor blockColor)
        {
            for(int x = 0; x < BlockModel.GetLength(0); x++)
            {
                for (int y = 0; y < BlockModel.GetLength(1); y++)
                {
                    if (BlockModel[x, y] == '#')
                    {
                        Console.BackgroundColor = blockColor;
                        Console.SetCursorPosition(xpos*2 + mapLeftCornersX + 2 + 2 * x, mapBottomCornersY + 2 - y-ypos);
                        Console.Write("  ");
                        Console.ResetColor();
                    } 
                }
            }
        }

        public void updateBlock()
        {
            ypos -= 1;
        }
    }
}
