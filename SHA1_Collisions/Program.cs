using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;
//Console.WriteLine(Encoding.Default.GetString(value));

namespace SHA1_Collisions
{
    class Program
    {
        const int MessageLength = 194;
        const int BytesToSave = 6;
        static Random random = new Random();

        static void RandomSalt(byte[] msg)
        {
            for (int i = 66; i < MessageLength; i++)
            {
                msg[i] = (byte)(random.Next(0, 10) + '0');
            }
        }

        // Returns false if hashes are equal
        static bool CompareHashes(int numberOfBits, byte[] hash1, byte[] hash2)
        {
            int byteIndex = 0;
            int bit = 1;
            for (int j = 0; j < numberOfBits; j++)
            {
                if ((hash1[byteIndex] & bit) != (hash2[byteIndex] & bit))
                { 
                    return true;
                }
                if (j % 8 == 7)
                {
                    byteIndex++;
                    bit = 1;
                }
                else bit <<= 1;
            }
            return false;
            //for (int j = 0; j <= i / 8; j++)
            //    for (int bit = 1; bit <= 128; bit <<= 1)
            //    {
            //        if ((hash1[j] & bit) != (hash2[j] & bit))
            //        {
            //            fl = true;
            //            return;
            //        }
            //    }
        }

        static void Main(string[] args)
        {
            byte[] message1 = new byte[MessageLength];
            byte[] message2 = new byte[MessageLength];
            byte[] hash1;
            byte[] hash2;

            using (FileStream fs = new FileStream("msg.txt", FileMode.Open))
            {
                fs.Read(message1, 0, MessageLength);
                fs.Read(message2, 0, MessageLength);
            }

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                Console.WriteLine("-----First preimage resistance:-----");
                hash1 = sha.ComputeHash(message1);

                for (int i = 1; i <= 24; i++)
                {
                    int cycleCount = 0;
                    bool fl = true;

                    while (fl)
                    {
                        cycleCount++;
                        RandomSalt(message2);
                        hash2 = sha.ComputeHash(message2);

                        fl = CompareHashes(i, hash1, hash2);
                    }

                    Console.WriteLine($"{i} bit: {cycleCount} cycles");
                }

                byte[][] firstHashes = new byte[1000000][];
                byte[][] secondHashes = new byte[1000000][];
                for (int i = 0; i < 1000000; i++)
                {
                    firstHashes[i] = new byte[BytesToSave];
                    secondHashes[i] = new byte[BytesToSave];
                }

                TaskFactory taskFactory = new TaskFactory();

                Console.WriteLine("-----Second preimage resistance-----");
                for (int i = 1; i <= 48; i++)
                {
                    int cycleCount = 0;
                    bool fl = true;

                    while (fl)
                    {
                        RandomSalt(message1);
                        RandomSalt(message2);
                        hash1 = sha.ComputeHash(message1);
                        hash2 = sha.ComputeHash(message2);

                        for (int j = 0; j < BytesToSave; j++)
                        {
                            firstHashes[cycleCount][j] = hash1[j];
                            secondHashes[cycleCount][j] = hash2[j];
                        }

                        cycleCount++;
                        //Task<bool> taskObj = Task.Run<bool>(() => MyTask(cycleCount, i, hash1, secondHashes));
                        Task<bool> task1 = taskFactory.StartNew(() => MyTask(0, cycleCount / 2, i, hash1, secondHashes));
                        Task<bool> task2 = taskFactory.StartNew(() => MyTask(0, cycleCount / 2, i, hash2, firstHashes));
                        Task<bool> task3 = taskFactory.StartNew(() => MyTask(cycleCount / 2, cycleCount, i, hash1, secondHashes));
                        Task<bool> task4 = taskFactory.StartNew(() => MyTask(cycleCount / 2, cycleCount, i, hash2, firstHashes));
                        if (task1.Result || task2.Result || task3.Result || task4.Result)
                            break;
                        //for (int j = 0; j < cycleCount; j++)
                        //{
                        //    fl = CompareHashes(i, hash1, secondHashes[j]) && CompareHashes(i, hash2, firstHashes[j]);
                        //    if (!fl)
                        //        break;
                        //}

                    }

                    Console.WriteLine($"{i} bit: {cycleCount} cycles");
                }
            }
        }

        static bool MyTask(int start, int end, int numberOfBits, byte[] hash, byte[][] previousHashes)
        {
            for (int j = start; j < end; j++)
            {
                if (CompareHashes(numberOfBits, hash, previousHashes[j]) == false)
                    return true;
            }
            return false;
        }
    }
}