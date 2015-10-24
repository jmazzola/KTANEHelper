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
        bool lastSerialDigitOdd = false;

        // Does the serial contain a vowel?
        bool serialHasVowel = false;

        // How many batteries does the bomb have?
        int numBatteries = -1;

        // Is the FRK indicator lit?
        bool frkLit = false;

        // Is the CAR indicator lit?
        bool carLit = false;

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

        public int NumBatteries
        {
            get { return numBatteries; }
            set { numBatteries = value; }
        }

        public bool FRKLit
        {
            get { return frkLit; }
            set { frkLit = value; }
        }

        public bool CARLit
        {
            get { return carLit; }
            set { carLit = value; }
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

        public void DetermineBatteryCount(string count)
        {
            if (BContains(count, "one"))
                numBatteries = 1;
            else if (BContains(count, "two"))
                numBatteries = 2;
            else if (BContains(count, "a lot") || BContains(count, "shit ton"))
                numBatteries = 1337;
            else
                numBatteries = 69;
        }
    }
}
