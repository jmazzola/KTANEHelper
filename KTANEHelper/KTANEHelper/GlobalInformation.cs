using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// GlobalInformation - Bomb information such as serial number, battery count, ports, etc.

namespace KTANEHelper
{
    class GlobalInformation
    {
        // Is the last digit in the serial number odd? 
        bool lastSerialDigitOdd;

        // Does the serial contain a vowel?
        bool serialHasVowel;


        public bool LastSerialDigitOdd
        {
            get { return lastSerialDigitOdd; }
            set { lastSerialDigitOdd = value; }
        }

        public bool SerialHasVowel
        {
            get { return serialHasVowel; }
            set { serialHasVowel = value; }
        }

        bool BContains(string text, string token)
        {
            return text.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public void DetermineLastDigit(string digit)
        {
            if (BContains(digit, "one") || BContains(digit, "three") || BContains(digit, "five") || BContains(digit, "seven") || BContains(digit, "nine"))
                lastSerialDigitOdd = true;
            else if (BContains(digit, "zero") || BContains(digit, "o") || BContains(digit, "two") || BContains(digit, "four") || BContains(digit, "six") || BContains(digit, "eight"))
                lastSerialDigitOdd = false;
        }
    }
}
