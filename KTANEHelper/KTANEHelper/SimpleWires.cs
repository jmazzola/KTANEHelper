using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

// SimpleWires - Handles everything about Simple Wires (3-5 wires with colors)

namespace KTANEHelper
{
    class SimpleWires
    {
        enum WireColor : int
        {
            Red = 0,
            Blue,
            Yellow,
            White,
            Black,
        };

        GlobalInformation info;

        string[] wires;
        int[] colorCount;

        public SimpleWires(ref GlobalInformation i)
        {
            info = i;
        }

        public void CountWireColors()
        {
            colorCount = new int[5];

            int i = 0;
            foreach(string w in wires)
            {
                wires[i] = wires[i].ToLower();

                if (w == "red")
                    colorCount[(int)WireColor.Red]++;
                else if (w == "blue")
                    colorCount[(int)WireColor.Blue]++;
                else if (w == "yellow")
                    colorCount[(int)WireColor.Yellow]++;
                else if (w == "white")
                    colorCount[(int)WireColor.White]++;
                else if (w == "black")
                    colorCount[(int)WireColor.Black]++;

                i++;
            }
        }

        public string SolveThreeWires()
        {

            // 3 Wire Logic

            if(colorCount[(int)WireColor.Red] == 0)
                return "Cut the second wire.";
            else if(wires[2] == "white")
                return "Cut the last wire.";
            else if(colorCount[(int)WireColor.Blue] > 1)
                return "Cut the last blue wire.";
            else
                return "Cut the last wire.";
        }

        public string SolveFourWires()
        {
            // 4 Wire Logic

            if ((colorCount[(int)WireColor.Red] > 1) && info.LastSerialDigitOdd)
                return "Cut the last red wire.";
            else if (wires[3] == "yellow" && colorCount[(int)WireColor.Red] == 0)
                return "Cut the first wire.";
            else if (colorCount[(int)WireColor.Blue] == 1)
                return "Cut the first wire.";
            else if (colorCount[(int)WireColor.Yellow] > 1)
                return "Cut the last wire.";
            else
                return "Cut the second wire.";
        }

        public string SolveFiveWires()
        {
            // 5 Wire Logic

            if (wires[4] == "black" && info.LastSerialDigitOdd)
                return "Cut the fourth wire.";
            else if ((colorCount[(int)WireColor.Red] == 1) && (colorCount[(int)WireColor.Yellow] > 1))
                return "Cut the first wire.";
            else if ((colorCount[(int)WireColor.Black] == 0))
                return "Cut the second wire.";
            else
                return "Cut the first wire.";

        }

        public string SolveSixWires()
        {
            // 6 Wire Logic

            if (colorCount[(int)WireColor.Yellow] == 0 && info.LastSerialDigitOdd)
                return "Cut the third wire.";
            else if ((colorCount[(int)WireColor.Yellow] == 1) && (colorCount[(int)WireColor.White] > 1))
                return "Cut the fourth wire";
            else if (colorCount[(int)WireColor.Red] == 0)
                return "Cut the last wire.";
            else
                return "Cut the fourth wire.";
        }

        public string ParseSolution(string[] wiresList)
        {
            wires = wiresList;

            // Count our wire colors
            CountWireColors();

            switch(wires.Count())
            {
                case 3:
                    return SolveThreeWires();

                case 4:
                    return SolveFourWires();

                case 5:
                    return SolveFiveWires();

                case 6:
                    return SolveSixWires();

                default:
                    break;
            }

            return "You fucked up the number of wires.";
        }
    }
}
