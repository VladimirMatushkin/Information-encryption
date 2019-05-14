using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.RegularExpressions;

namespace RSA
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
                long e = long.Parse(regex.Match(sr.ReadLine()).Value);

                long p = PollardRho(n);
                long q = n / p;
                long phi = (p - 1) * (q - 1);

                //𝑑 = 𝑒−1(mod 𝜙(𝑛))

                Console.WriteLine($"p = {p}\nq = {q}");

                sr.ReadLine();
            }
        }

        static long PollardRho(long n)
        {
            long x = (long)(random.NextDouble() * Int64.MaxValue) % n;
            long y = 1;
            long i = 0;
            long stage = 2;

            while(GCD(n, Math.Abs(x-y)) == 1)
            {
                if(i == stage)
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
