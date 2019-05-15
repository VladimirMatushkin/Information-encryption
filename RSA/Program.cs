using System;
using System.IO;
using System.Text;
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
                //𝑑 = 𝑒^(−1) mod 𝜙(𝑛)
                long d = InverseModulo(e, phi);

                Console.WriteLine($"p = {p}\nq = {q}");
                Console.WriteLine($"phi = {phi}\nd = {d}");

                sr.ReadLine();

                using (StreamWriter sw = new StreamWriter("decrypted.txt", false, Encoding.GetEncoding("Windows-1251")))
                {
                    sw.WriteLine($"p = {p}\r\nq = {q}");
                    sw.WriteLine($"phi = {phi}\r\nd = {d}");

                    byte[] c = new byte[3];
                    long M;
                    foreach (string number in sr.ReadToEnd().Split(' '))
                    {
                        if (long.TryParse(number, out M))
                        {
                            long C = ModularPower(M, d, n);

                            c[2] = (byte)(C & 255);
                            C >>= 8;
                            c[1] = (byte)(C & 255);
                            C >>= 8;
                            c[0] = (byte)(C & 255);

                            sw.Write(ASCIIEncoding.Default.GetString(c));
                        }
                    }
                }
            }
        }

        static long InverseModulo(long a, long m)
        {
            long x, y;
            long d = ExtendedGCD(a, m, out x, out y);

            if (d == 1) //return x;
                return (x % m + m) % m;
            return 0;
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

        // calculates a*x + b*y = gcd(a, b)
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
