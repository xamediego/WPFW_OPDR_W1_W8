using System;
using System.Globalization;

namespace ConsoleApplication1.Map
{
    public static class Float
    {

        public static string MetSuffixen(this float f)
        {

            f *= 1000;
            
            string output;
            
            if (f >= 1000000000)
            {
                f /= 1000000000;
                output = Math.Round(f, 1).ToString(CultureInfo.InvariantCulture) + "B";
            }
            else
            {
                if (f >= 1000000)
                {
                    f /= 1000000;
                    output = Math.Round(f, 1).ToString(CultureInfo.InvariantCulture) + "M";
                }
                else
                {
                    if (f >= 1000)
                    {
                        f /= 1000;
                        output = Math.Round(f, 1).ToString("F1") + "K";
                    }
                    else
                    {
                        output = f.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
            
            return output;
        }
        
    }
}