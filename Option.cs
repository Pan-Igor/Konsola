using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Pytong
{
    class Option
    {
        public string optionText;
        public string GetOptionText()
        {
            string returnText = "    " + optionText;
            return returnText;
        }
        public string GetSelectedOptionText()
        {
            string returnText = " -> " + optionText + " <- ";
            
            return returnText;
        }
        public Option(string optiontext)
        {
            optionText = optiontext;
        }
    }
}
