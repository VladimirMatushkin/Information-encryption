using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LegendreSymbol
{
    class Program
    {
        const int AmountOfLegendreSymbols = 240;
        const int TextLength = AmountOfLegendreSymbols / 8;

        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder(TextLength);
            Regex r = new Regex(@"L\(([0-9]+),([0-9]+)\)");

            using (StreamWriter sw = new StreamWriter("legendre.txt", false))
            using (StreamReader sr = new StreamReader(args[0]))
            {
                string line;
                int bit = 1;
                int symbol = 0;
                int legendreSymbol = 0;

                sw.WriteLine("Legendre symbols");
                while ((line = sr.ReadLine()) != null)
                {
                    Match m = r.Match(line);

                    if (m.Success)
                    {
                        legendreSymbol = Legendre(long.Parse(m.Groups[1].Value), long.Parse(m.Groups[2].Value));
                        sw.Write(legendreSymbol + ",");
                        if (bit > 128)
                        {
                            bit = 1;
                            if (symbol != 32)
                                symbol += 848;
                            sb.Append((char)symbol);
                            symbol = 0;
                        }
                        symbol += legendreSymbol * bit;
                        bit <<= 1;
                    }
                }
                if (bit > 128)
                {
                    if (symbol != 32)
                        symbol += 848;
                    sb.Append((char)symbol);
                }
                sw.WriteLine("\n\nText");
                sw.Write(sb.ToString());
            }
        }

        static int Legendre(long a, long p)
        {
            if (a % p == 0) return 0;
            return ModularPower(a, (p - 1) / 2, p) == 1 ? 1 : 0;
        }

        static long ModularPower(long a, long p, long n)
        {
            long res = 1;

            a %= n;

            while (p > 0)
            {
                if ((p & 1) == 1)
                    res = (res * a) % n;

                p >>= 1;
                a = (a * a) % n;
            }
            return res;
        }
    }
}