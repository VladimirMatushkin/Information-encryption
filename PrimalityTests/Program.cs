using System;
using System.Diagnostics;

namespace PrimalityTests
{
    class Program
    {
        const int AmountOfNumbers = 10000000;
        const int Base1 = 5;
        const int Base2 = 3;

        static void Main(string[] args)
        {
            // 1115913987
            Random random = new Random(1115913987);
            Stopwatch stopwatch = new Stopwatch();
            int[] numbers = new int[AmountOfNumbers];
            int[] digitsCount = new int[9];

            int trialDivision = 0;
            int fermatTestOne = 0;
            int fermatTestTwo = 0;
            int millerRabinTestOne = 0;
            int millerRabinTestTwo = 0;

            // Generate numbers 
            for (int i = 0; i < AmountOfNumbers; i++)
                numbers[i] = random.Next(1, 1000000000);
            // Count digits of generated numbers
            for (int i = 0; i < AmountOfNumbers; i++)
                digitsCount[CountDigit(numbers[i]) - 1]++;

            for (int i = 0; i < 9; i++)
                Console.WriteLine($"{i + 1} digit(s) numbers: {digitsCount[i]}");

            // Trial Division
            stopwatch.Start();
            for (int i = 0; i < AmountOfNumbers; i++)
                if (TrialDivision(numbers[i]))
                    trialDivision++;
            stopwatch.Stop();
            Console.WriteLine("------------------TrialDivision-------------------");
            Console.WriteLine($"Time: {(double)stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"Prime numbers found: {trialDivision}");
            Console.WriteLine("Average time: {0}ms", (double)stopwatch.ElapsedMilliseconds / AmountOfNumbers);

            // Fermat test with one base
            stopwatch.Restart();
            for (int i = 0; i < AmountOfNumbers; i++)
                if (FermatTest(Base1, numbers[i]))
                    fermatTestOne++;
            stopwatch.Stop();
            Console.WriteLine("------------Fermat test with one base-------------");
            Console.WriteLine($"Time: {(double)stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"Prime numbers found: {fermatTestOne}");
            Console.WriteLine("Error: {0}%", Math.Abs(fermatTestOne - trialDivision) / (double)trialDivision);
            Console.WriteLine("Average time: {0}ms", (double)stopwatch.ElapsedMilliseconds / AmountOfNumbers);

            // Fermat test with two bases
            stopwatch.Restart();
            for (int i = 0; i < AmountOfNumbers; i++)
                if (FermatTest(Base1, numbers[i]) && FermatTest(Base2, numbers[i]))
                    fermatTestTwo++;
            stopwatch.Stop();
            Console.WriteLine("------------Fermat test with two bases------------");
            Console.WriteLine($"Time: {(double)stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"Prime numbers found: {fermatTestTwo}");
            Console.WriteLine("Error: {0}%", Math.Abs(fermatTestTwo - trialDivision) / (double)trialDivision);
            Console.WriteLine("Average time: {0}ms", (double)stopwatch.ElapsedMilliseconds / AmountOfNumbers);

            // Miller–Rabin test with one base
            stopwatch.Restart();
            for (int i = 0; i < AmountOfNumbers; i++)
                if (MillerRabinTest(Base1, numbers[i]))
                    millerRabinTestOne++;
            stopwatch.Stop();
            Console.WriteLine("----------Miller–Rabin test with one base---------ms");
            Console.WriteLine($"Time: {(double)stopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"Prime numbers found: {millerRabinTestOne}");
            Console.WriteLine("Error: {0}%", Math.Abs(millerRabinTestOne - trialDivision) / (double)trialDivision);
            Console.WriteLine("Average time: {0}ms", (double)stopwatch.ElapsedMilliseconds / AmountOfNumbers);

            // Miller–Rabin test with two bases
            stopwatch.Restart();
            for (int i = 0; i < AmountOfNumbers; i++)
                if (MillerRabinTest(Base1, numbers[i]) && MillerRabinTest(Base2, numbers[i]))
                    millerRabinTestTwo++;
            stopwatch.Stop();
            Console.WriteLine("----------Miller–Rabin test with two bases--------");
            Console.WriteLine($"Time: {(double)stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"Prime numbers found: {millerRabinTestTwo}");
            Console.WriteLine("Error: {0}%", Math.Abs(millerRabinTestTwo - trialDivision) / (double)trialDivision);
            Console.WriteLine("Average time: {0}ms", (double)stopwatch.ElapsedMilliseconds / AmountOfNumbers);
        }

        static int CountDigit(int n)
        {
            return (int)Math.Floor(Math.Log10(n) + 1);
        }

        // Function to calculate (a^p)%n 
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

        static bool TrialDivision(int n)
        {
            if (n <= 1) return false;
            if (n <= 3) return true;

            if (n % 2 == 0 || n % 3 == 0) return false;

            for (int i = 5; i * i <= n; i += 6)
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;

            return true;
        }

        static bool FermatTest(long a, long n)
        {
            if (ModularPower(a, n - 1, n) != 1)
                return false;

            return true;
        }

        static bool MillerRabinTest(long a, long n)
        {
            if (n <= 1) return false;
            if (n <= 3) return true;
            if (n % 2 == 0) return false;

            long m = n - 1;
            long k = 0;
            while (m % 2 == 0)
            {
                m /= 2;
                k++;
            }

            long T = ModularPower(a, m, n);
            if (T == 1 || T == n - 1)
                return true;

            for (int i = 0; i < k - 1; i++)
            {
                T = (T * T) % n;

                if (T == 1) return false;
                if (T == n - 1) return true;
            }

            return false;
        }
    }
}
