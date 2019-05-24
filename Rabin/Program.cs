using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Rabin
{
    // Message encrypted by Rabin
    // Message blocks by 3 characters, Russian text inside)
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

                sr.ReadLine();

                using (StreamWriter sw = new StreamWriter("decrypted.txt", false, Encoding.GetEncoding("Windows-1251")))
                {
                    sw.WriteLine($"p = {p}\r\nq = {q}");
                    //sw.WriteLine($"yp = {yp}\r\nyq = {yq}");

                    long encryptedMessage;
                    long[] M = new long[4];
                    byte[] decryptedMessage = new byte[1000];

                    int messageLength = 0;
                    foreach (string number in Regex.Split(sr.ReadToEnd(), "[ \r\n]+"))
                        if (long.TryParse(number, out encryptedMessage))
                        {
                            //long C = ModularPower(M, d, n);
                            long mp = encryptedMessage % p;
                            long mq = encryptedMessage % q;
                            M = Decrypt(encryptedMessage, p, q);

                            for (int i = 0; i < M.Length; i++)
                            {
                                byte c1 = (byte)(M[i] & 255);
                                if (c1 == 32 || (c1 >= 192 && c1 <= 223))
                                {
                                    M[i] >>= 8;
                                    byte c2 = (byte)(M[i] & 255);
                                    if (c2 == 32 || (c2 >= 192 && c2 <= 223))
                                    {
                                        M[i] >>= 8;
                                        byte c3 = (byte)(M[i] & 255);
                                        if (c3 == 32 || (c3 >= 192 && c3 <= 223))
                                        {
                                            decryptedMessage[messageLength++] = c3;
                                            decryptedMessage[messageLength++] = c2;
                                            decryptedMessage[messageLength++] = c1;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    sw.WriteLine("Decrypted message");
                    sw.WriteLine(ASCIIEncoding.Default.GetString(decryptedMessage, 0, messageLength));
                }
            }
        }

        public static long[] Decrypt(long c, long p, long q)
        {
            long N = p * q;
            long m_p1 = ModularPower(c, (p + 1) / 4, p);
            long m_p2 = p - m_p1;
            long m_q1 = ModularPower(c, (q + 1) / 4, q);
            long m_q2 = q - m_q1;

            long y_p;
            long y_q;
            ExtendedGCD(p, q, out y_p, out y_q);

            //y_p*p*m_q + y_q*q*m_p (mod n)
            long d1 = Mod((y_p * p * m_q1 + y_q * q * m_p1), N);
            long d2 = Mod((y_p * p * m_q2 + y_q * q * m_p1), N);
            long d3 = Mod((y_p * p * m_q1 + y_q * q * m_p2), N);
            long d4 = Mod((y_p * p * m_q2 + y_q * q * m_p2), N);

            return new long[] { d1, d2, d3, d4 };
        }

        static long Mod(long x, long m)
        {
            return (x % m + m) % m;
        }

        static long ModularPower(long a, long p, long mod)
        {
            long res = 1;

            a %= mod;

            while (p > 0)
            {
                if ((p & 1) == 1)
                    res = (res * a) % mod;

                p >>= 1;
                a = (a * a) % mod;
            }
            return res;
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
