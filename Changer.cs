using Pytong;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsola
{
    class Changer : Option
    {
        public int value;
        public int minValue;
        public int maxValue;
        public Changer(string optiontext, int value, int minvalue, int maxvalue) : base(optiontext)
        {
            this.value = value;
            this.minValue = minvalue;
            this.maxValue = maxvalue;
        }
        public void updateValue(int val)
        {
            this.value += val;
            if (value < minValue) 
                value = maxValue;
            if (value > maxValue)
                value = minValue;
        }
    }
}
