using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

namespace Encryption4
{
    class Cipher : IComparable<Cipher>
    {
        public char a, b, c;
        public double P = 0;
        public int CompareTo(Cipher value)
        {
            return (int)(value.P - this.P);
        }
    }
    // Class for decrypting text by brute force method with key length = 3
    class BruteForceSearch
    {
        public const string CSVFileName = "bigram frequency.csv";

        private HashSet<char> HsAlphabet;
        private Dictionary<char, int> DctAlphabet;
        private Dictionary<int, char> DctLolAlphabet;
        //private double[,] BaseBigramTable;
        // char - row, char - column, double - frequency
        private Dictionary<char, Dictionary<char, double>> BaseBigramTable;

        private Cipher[] TopCiphers = new Cipher[10];

        private List<string> EncryptedText = new List<string>();

        public BruteForceSearch(string alphabet)
        {
            HsAlphabet = new HashSet<char>(alphabet);
            int index = 0;
            DctAlphabet = alphabet.ToDictionary(k => k, v => index++);
            DctLolAlphabet = DctAlphabet.ToDictionary(k => k.Value, k => k.Key);

            for (int i = 0; i < TopCiphers.Length; i++)
                TopCiphers[i] = new Cipher();
            //int count = Alphabet.Count;
            //BaseBigramTable = new double[count, count];

            //b = new Dictionary<char, Dictionary<char, double>>(count);
            // TODO is it slower ?
            //BaseBigramTable = Alphabet.ToDictionary(k => k, k => Alphabet.ToDictionary(v => v, v => 0.0));
            BaseBigramTable = alphabet.ToDictionary(k => k, k => alphabet.ToDictionary(v => v, v => 0.0));

        }

        public void AnalyzeBaseFile(string fileName)
        {
            string line;
            // Read base file
            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251")))
                while ((line = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length - 1; i++)
                    {
                        //if (Alphabet.Contains(line[i]) && Alphabet.Contains(line[i + 1]) && 
                        if (HsAlphabet.Contains(line[i]) && HsAlphabet.Contains(line[i + 1]) &&
                            !(line[i] == ' ' && line[i + 1] == ' '))
                        {
                            BaseBigramTable[line[i]][line[i + 1]]++;
                        }
                    }
                }
            // Calculae frequencies
            foreach (char k in HsAlphabet)
            {
                int bigramCount = 0;
                foreach (char key in HsAlphabet)
                {
                    bigramCount += (int)BaseBigramTable[k][key];
                }
                foreach (char key in HsAlphabet)
                {
                    BaseBigramTable[k][key] /= bigramCount;
                }
            }
            // Write bigram frequency to CSV file
            using (StreamWriter sw = new StreamWriter(CSVFileName, false, Encoding.GetEncoding("Windows-1251")))
            {
                sw.WriteLine("," + string.Join(",", HsAlphabet));
                foreach (char key in HsAlphabet)
                {
                    sw.Write(key + ",");
                    sw.WriteLine(string.Join(",", BaseBigramTable[key].Values));
                }
            }

        }

        public void AnalyzeEncryptedFile(string fileName)
        {
            string line;

            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251")))
                while ((line = sr.ReadLine()) != null)
                {
                    EncryptedText.Add(line);
                }
        }

        public void EncryptedTextToTextBox(TextBox tb)
        {
            foreach (string s in EncryptedText)
            {
                tb.Text += s;
            }
        }

        public void BruteForce()
        {
            //StringBuilder cipher = new StringBuilder(3);
            //List<char> listAlphabet = Alphabet.ToList();

            int sizeOfAlphabet = HsAlphabet.Count;
            int[] cipher = new int[3];

            for (int a = 0; a < sizeOfAlphabet; a++) {
                cipher[0] = a;
                for (int b = 0; b < sizeOfAlphabet; b++) {
                    cipher[1] = b;
                    for (int c = 0; c < sizeOfAlphabet; c++)
                    {
                        cipher[2] = c;
                        double p = 0;
                        foreach (string line in EncryptedText)
                        {
                            for (int i = 0; i < line.Length - 1; i++)
                            {
                                //char x1 = listAlphabet[(listAlphabet.IndexOf(line[i]) + cipher[i % 3]) % sizeOfAlphabet];
                                //char x2 = listAlphabet[(listAlphabet.IndexOf(line[i+1]) + cipher[(i + 1) % 3]) % sizeOfAlphabet];
                                char x1 = DctLolAlphabet[(DctAlphabet[line[i]] + cipher[i % 3]) % sizeOfAlphabet];
                                char x2 = DctLolAlphabet[(DctAlphabet[line[i + 1]] + cipher[(i + 1) % 3]) % sizeOfAlphabet];
                                p += BaseBigramTable[x1][x2];
                            }
                        }

                        int indexOfMinimum = 0;
                        double minimum = TopCiphers[0].P;
                        for(int i = 1; i < TopCiphers.Length; i++)
                        {
                            if(TopCiphers[i].P < minimum)
                            {
                                indexOfMinimum = i;
                                minimum = TopCiphers[i].P;
                            }
                        }

                        if(p > minimum)
                        {
                            TopCiphers[indexOfMinimum].P = p;
                            TopCiphers[indexOfMinimum].a = DctLolAlphabet[a];
                            TopCiphers[indexOfMinimum].b = DctLolAlphabet[b];
                            TopCiphers[indexOfMinimum].c = DctLolAlphabet[c];
                        }
                    }
                }
            }
            Array.Sort(TopCiphers);
        }

        public void TopCiphersToTextBox(TextBox tb)
        {
            tb.Clear();
            foreach(Cipher cipher in TopCiphers)
            {
                tb.Text += String.Format("{0}{1}{2} {3}\r\n", cipher.a, cipher.b, cipher.c, cipher.P);
            }
        }

        public void DecryptText()
        {
            int sizeOfAlphabet = HsAlphabet.Count;
            List<char> listAlphabet = HsAlphabet.ToList();
            int[] lol = new int[3];
            lol[0] = listAlphabet.IndexOf(TopCiphers[0].a);
            lol[1] = listAlphabet.IndexOf(TopCiphers[0].b);
            lol[2] = listAlphabet.IndexOf(TopCiphers[0].c);
           
            using (StreamWriter sw = new StreamWriter("decrypt.txt", false, Encoding.GetEncoding("Windows-1251")))
                foreach (string line in EncryptedText)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        char x1 = listAlphabet[(listAlphabet.IndexOf(line[i]) + lol[i % 3]) % sizeOfAlphabet];
                        sw.Write(x1);
                    }
                }
        }
    }
}
