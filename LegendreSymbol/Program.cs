using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LegendreSymbol
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex r = new Regex(@"L\(([0-9]+),([0-9]+)\)");

            using (StreamReader sr = new StreamReader(args[0]))
            {
                string line;

                int i = 0;
                while((line = sr.ReadLine()) != null)
                {
                    Match m = r.Match(line);

                    if(m.Success)
                    {
                        Console.WriteLine(Legendre(long.Parse(m.Groups[1].Value), long.Parse(m.Groups[2].Value)));
                        i++;
                    }
                    if (i > 10) break;
                }
            }
        }

        static long Legendre(long a, long p)
        {
            if (p < 2)  // prime test is expensive.
                throw new ArgumentOutOfRangeException("p", "p must not be < 2");
            if (a == 0)
            {
                return 0;
            }
            if (a == 1)
            {
                return 1;
            }
            long result;
            if (a % 2 == 0)
            {
                result = Legendre(a / 2, p);
                if (((p * p - 1) / 8) != 0) // instead of dividing by 8, shift the mask bit
                {
                    result = -result;
                }
            }
            else
            {
                result = Legendre(p % a, a);
                if (((a - 1) * (p - 1) / 4) != 0) // instead of dividing by 4, shift the mask bit
                {
                    result = -result;
                }
            }
            return result;
        }
    }
}