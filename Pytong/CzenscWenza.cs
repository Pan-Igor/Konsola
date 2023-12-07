using System;
using System.Collections.Generic;
using System.Text;

namespace Pytong
{
    class CzenscWenza
    {
        public int x;
        public int y;
        public int wiek;
        public bool isLookingVertical;
        public int dir;
        public string model;
        public CzenscWenza(int xpos, int ypos, bool up, int direction)
        {
            x = xpos;
            y = ypos;
            dir = direction;
            isLookingVertical = up;
            wiek = 0;
            if (isLookingVertical)
            {
                if (dir > 0)
                    model = "˙˙";
                else
                    model = "..";
            }
            else
            {
                if (dir > 0)
                    model = " :";
                else
                    model = ": ";
            }
        }

        public void UpdatePart()
        {
            if(wiek != 0)
                model = "  ";
            wiek++;
        }
    }
}
