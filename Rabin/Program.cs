using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Rabin
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Regex regex = new Regex("[0-9]+");

            using (StreamReader sr = new StreamReader(args[0]))
            {
                long n = long.Parse(regex.Match(sr.ReadLine()).Value);
                long p = PollardRho(n);
                long q = n / p;

                long yp, yq;
                ExtendedGCD(p, q, out yp, out yq);

                long[] M = new long[4];

                sr.ReadLine();


            }
        }

        static long ExtendedGCD(long a, long b, out long x, out long y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            long q, r;
            long x1 = 0, x2 = 1;
            long y1 = 1, y2 = 0;

            while (b > 0)
            {
                q = a / b;
                r = a - q * b;
                x = x2 - q * x1;
                y = y2 - q * y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }

            x = x2;
            y = y2;
            return a;
        }

        static long PollardRho(long n)
        {
            long x = (long)(random.NextDouble() * Int64.MaxValue) % n;
            long y = 1;
            long i = 0;
            long stage = 2;

            while (GCD(n, Math.Abs(x - y)) == 1)
            {
                if (i == stage)
                {
                    y = x;
                    stage *= 2;
                }
                x = (x * x - 1) % n;
                i++;
            }

            return GCD(n, Math.Abs(x - y));
        }

        static long GCD(long a, long b)
        {
            long r;
            while (b != 0)
            {
                r = a % b;
                a = b;
                b = r;
            }
            return a;
        }
    }
}
