using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ElGamal
{
    // Message encrypted by ElGamal
    // Message blocks by 3 characters, Russian text inside)
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex("[0-9]+", RegexOptions.Compiled);

            using (StreamReader sr = new StreamReader(args[0]))
            {
                long p = long.Parse(regex.Match(sr.ReadLine()).Value);
                long g = long.Parse(regex.Match(sr.ReadLine()).Value);
                long y = long.Parse(regex.Match(sr.ReadLine()).Value);

                sr.ReadLine();
                // Find private key
                long x;
                long? tmp = BabyStepGiantStep(g, y, p);
                if (tmp.HasValue)
                    x = tmp.Value;
                else
                    throw new Exception("Private key can not be found");

                using (StreamWriter sw = new StreamWriter("decrypted.txt", false, Encoding.GetEncoding("Windows-1251")))
                {
                    sw.WriteLine($"x = {x}");

                    byte[] c = new byte[3];
                    string line;
                    while (!string.IsNullOrWhiteSpace(line = sr.ReadLine()))
                    {
                        MatchCollection matches = regex.Matches(line);
                        if (matches.Count == 2)
                        {
                            long a = long.Parse(matches[0].Value);
                            long b = long.Parse(matches[1].Value);

                            long M = (b * ModularPower(a, p - 1 - x, p)) % p;

                            c[2] = (byte)(M & 255);
                            M >>= 8;
                            c[1] = (byte)(M & 255);
                            M >>= 8;
                            c[0] = (byte)(M & 255);

                            sw.Write(ASCIIEncoding.Default.GetString(c));
                        }
                    }
                }
            }
        }

        static long? BabyStepGiantStep(long a, long b, long mod)
        {
            long m = (long)Math.Ceiling(Math.Sqrt(mod));
            long e = 1;

            Dictionary<long, long> table = new Dictionary<long, long>((int)m);

            for (long i = 0; i < m; i++)
            {
                table.Add(e, i);
                e = (e * a) % mod;
            }

            long factor = ModularPower(a, mod - m - 1, mod);
            e = b;

            for (long i = 0; i < m; i++)
            {
                if (table.TryGetValue(e, out long value))
                {
                    return i * m + value;
                }
                e = (e * factor) % mod;
            }

            return null;
        }

        static long ModularPower(long a, long exp, long mod)
        {
            long res = 1;

            a %= mod;

            while (exp > 0)
            {
                if ((exp & 1) == 1)
                    res = (res * a) % mod;

                exp >>= 1;
                a = (a * a) % mod;
            }
            return res;
        }
    }
}
