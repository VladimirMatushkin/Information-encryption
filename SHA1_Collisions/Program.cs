using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

//Console.WriteLine(Encoding.Default.GetString(value));

namespace SHA1_Collisions
{
    class Program
    {
        const int MessageLength = 194;
        static Random random = new Random();

        static void RandomSalt(byte[] msg)
        {
            for (int i = 66; i < MessageLength; i++)
            {
                msg[i] = (byte)(random.Next(0, 10) + '0');
            }
        }

        static void Main(string[] args)
        {
            byte[] message1 = new byte[MessageLength];
            byte[] message2 = new byte[MessageLength];
            byte[] hash1;
            byte[] hash2 = null;

            using (FileStream fs = new FileStream("msg.txt", FileMode.Open))
            {
                fs.Read(message1, 0, MessageLength);
                fs.Read(message2, 0, MessageLength);
            }

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                hash1 = sha.ComputeHash(message1);

                for (int i = 1; i <= 24; i++)
                {
                    int cycleCount = 0;
                    bool fl = true;

                    while (fl)
                    {
                        fl = false;
                        cycleCount++;
                        RandomSalt(message2);
                        hash2 = sha.ComputeHash(message2);

                        void Comparison()
                        {
                            int byteIndex = 0;
                            int bit = 1;
                            for(int j = 0; j < i; j++)
                            {
                                if ((hash1[byteIndex] & bit) != (hash2[byteIndex] & bit))
                                {
                                    fl = true;
                                    return;
                                }
                                if (j % 8 == 7)
                                {
                                    byteIndex++;
                                    bit = 1;
                                }
                                else bit <<= 1;
                            }

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
                        Comparison();
                    }

                    Console.WriteLine($"{i} bit: {cycleCount} cycles");
                }
            }
        }
    }
}