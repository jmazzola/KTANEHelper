using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANEHelper
{
    class TheButton
    {

        GlobalInformation info;

        public TheButton(ref GlobalInformation i)
        {
            info = i;
        }

        public string GetNumberToReleaseOn(string color)
        {
            switch(color)
            {
                case "blue":
                    return "Release when there's a 4 in any position";
                case "white":
                    return "Release when there's a 1 in any position";
                case "yellow":
                    return "Release when there's a 5 in any position";
                case "red":
                    return "Release when there's a 1 in any position";
            }

            return "You fucked up the color";
        }

        public string ParseSolution(string[] input)
        {
            string color = input[0];
            string label = input[1];

            if( (color == "blue") && (label == "abort"))
                return "Hold the button. What color is the strip?";
            
            else if(info.NumBatteries > 1 && label == "detonate")
                return "Press and immediately release.";

            else if(color == "white" && info.CARLit)
                return "Hold the button. What color is the strip?";

            else if(info.NumBatteries > 2 && info.FRKLit)
                return "Press and immediately release";

            else if(color == "yellow")
                return "Hold the button. What color is the strip?";
            
            else if(color == "red" && label == "hold")
                return "Press and immediately release";
            else
                return "Hold the button. What color is the strip?";

        }
    }
}
