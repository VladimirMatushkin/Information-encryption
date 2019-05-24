using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace SHA1_Collisions
{
    class Program
    {
        private const int MessageLength = 194;
        private const int BytesToSave = 6;

        //static Random random = new Random(1315410023);
        private static XorShiftRandom randomFast = new XorShiftRandom();
        private static byte[] salt = new byte[128];

        // For comparing bits
        private static byte[] bits = { 1, 2, 4, 8, 16, 32, 64, 128 };

        static void Main(string[] args)
        {
            byte[] message1 = new byte[MessageLength];
            byte[] message2 = new byte[MessageLength];
            byte[] hash1 = new byte[20];
            byte[] hash2 = new byte[20];

            // Read messages
            using (FileStream fs = new FileStream("msg.txt", FileMode.Open))
            {
                fs.Read(message1, 0, MessageLength);
                fs.Read(message2, 0, MessageLength);
            }

            CustomSHA1 customSHA1 = new CustomSHA1();
            //using (SHA1Cng sha = new SHA1Cng())

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("-----First preimage resistance:-----");

            customSHA1.ComputeHash(message1, hash1);

            // Find hash2 == hash1
            // Hashes are equal if first 'i' bits of them are equal 
            for (int i = 1; i <= 24; i++)
            {
                int cycleCount = 0;
                bool fl = true;

                while (fl)
                {
                    cycleCount++;

                    RandomSalt(message2);
                    customSHA1.ComputeHash(message2, hash2);

                    fl = CompareHashes(i, hash1, hash2);
                }

                Console.WriteLine($"{i} bit: {cycleCount} cycles");
            }

            Console.WriteLine("-----Second preimage resistance:-----");

            byte[][] secondHashes = new byte[1000000][];
            for (int i = 0; i < 1000000; i++)
            {
                secondHashes[i] = new byte[BytesToSave];
            }

            for (int i = 1; i <= 29; i++)
            {
                int cycleCount = 0;
                bool fl = true;

                RandomSalt(message2);
                customSHA1.ComputeHash(message1, hash1);
                customSHA1.ComputeHash(message2, hash2);
                for (int k = 0; k < BytesToSave; k++)
                {
                    secondHashes[cycleCount][k] = hash2[k];
                }

                while (fl)
                {
                    RandomSalt(message1);
                    RandomSalt(message2);
                    customSHA1.ComputeHash(message1, hash1);
                    customSHA1.ComputeHash(message2, hash2);

                    cycleCount++;
                    for (int q = 0; q < cycleCount; q++)
                    {
                        if (!CompareHashes(i, secondHashes[q], hash1) || !CompareHashes(i, secondHashes[q], hash2))
                        {
                            fl = false;
                            break;
                        }
                    }

                    for (int k = 0; k < BytesToSave; k++)
                    {
                        secondHashes[cycleCount][k] = hash2[k];
                    }
                }

                Console.WriteLine($"{i} bit: {cycleCount} cycles");
            }

            Dictionary<long, List<byte>> dctSecondHashes = new Dictionary<long, List<byte>>(17000000);

            for (int i = 30; i <= 48; i++)
            {
                int cycleCount = 0;
                bool fl = true;

                RandomSalt(message2);
                customSHA1.ComputeHash(message1, hash1);
                customSHA1.ComputeHash(message2, hash2);

                int amountOfBytes = i / 8;
                long message1Key;
                long message2Key = BytesToLong(hash2, amountOfBytes);
                dctSecondHashes.Add(message2Key, new List<byte>(10));

                int amountOfBits = i % 8;
                if (amountOfBits != 0)
                    dctSecondHashes[message2Key].Add(hash2[amountOfBytes]);

                while (fl)
                {
                    cycleCount++;

                    RandomSalt(message1);
                    RandomSalt(message2);
                    customSHA1.ComputeHash(message1, hash1);
                    customSHA1.ComputeHash(message2, hash2);

                    message1Key = BytesToLong(hash1, amountOfBytes);
                    message2Key = BytesToLong(hash2, amountOfBytes);

                    if (dctSecondHashes.ContainsKey(message1Key))
                    {
                        if (amountOfBits == 0)
                        {
                            break;
                        }

                        fl = CompareBytes(dctSecondHashes[message1Key], hash1[amountOfBytes], amountOfBits, amountOfBytes);
                        if (fl == false) break;
                    }

                    if (dctSecondHashes.ContainsKey(message2Key))
                    {
                        if (amountOfBits == 0)
                        {
                            break;
                        }

                        fl = CompareBytes(dctSecondHashes[message2Key], hash2[amountOfBytes], amountOfBits, amountOfBytes);
                        if (fl == false) break;
                    }

                    if (dctSecondHashes.ContainsKey(message2Key) == false)
                        dctSecondHashes.Add(message2Key, new List<byte>(10));
                    if (amountOfBits != 0)
                        dctSecondHashes[message2Key].Add(hash2[amountOfBytes]);
                }

                Console.WriteLine($"{i} bit: {cycleCount} cycles");
                dctSecondHashes.Clear();

            }
            stopwatch.Stop();
            Console.WriteLine($"\nExecution time: {stopwatch.Elapsed}");
        }

        static void RandomSalt(byte[] msg)
        {
            randomFast.NextBytes(salt);

            for (int i = 66; i < MessageLength; i++)
            {
                msg[i] = (byte)((int)salt[i - 66] % 10 + 48);
            }
        }

        // Returns false if hashes are equal
        static bool CompareHashes(int numberOfBits, byte[] hash1, byte[] hash2)
        {
            int byteIndex = 0;
            int bit = 1;

            byteIndex = numberOfBits / 8;
            for (int i = 0; i < byteIndex; i++)
                if (hash1[i] != hash2[i])
                    return true;

            bit = numberOfBits % 8;
            for (int i = 0; i < bit; i++)
                if ((hash1[byteIndex] & bits[i]) != (hash2[byteIndex] & bits[i]))
                    return true;

            return false;
        }

        // Pack amountOfBytes to long
        static long BytesToLong(byte[] bytes, int amountOfBytes)
        {
            long result = 0;
            for(int i = 0; i < amountOfBytes;i++)
            {
                result <<= 8;
                result += bytes[i];
            }
            return result;
        }

        // Returns false if hashes are equal
        static bool CompareBytes(List<byte> bytesToCompare, byte hashByte, int amountOfBits, int amountOfBytes)
        {
            for (int j = 0; j < bytesToCompare.Count; j++)
            {
                int k = 0;
                for (; k < amountOfBits; k++)
                {
                    if ((bytesToCompare[j] & bits[k]) != (hashByte & bits[k]))
                    {
                        break;
                    }
                }
                if (k == amountOfBits)
                {
                    return false;
                }
            }

            bytesToCompare.Add(hashByte);
            return true;
        }
    }
}
