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
        public char a, b, c;    // Three letters of the cipher
        public double P = 0;    // Probability that this cipher is the right one
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
        // this dictionaries need for shifting letters according to vigenere cipher
        private Dictionary<char, int> DctCharToIndex;
        private Dictionary<int, char> DctIndexToChar;

        // char - row, char - column, double - frequency
        private Dictionary<char, Dictionary<char, double>> BaseBigramTable;

        private Cipher[] TopCiphers = new Cipher[10];

        private List<string> EncryptedText = new List<string>();

        public BruteForceSearch(string alphabet)
        {
            HsAlphabet = new HashSet<char>(alphabet);
            int index = 0;
            DctCharToIndex = alphabet.ToDictionary(k => k, v => index++);
            DctIndexToChar = DctCharToIndex.ToDictionary(k => k.Value, k => k.Key);

            for (int i = 0; i < TopCiphers.Length; i++)
                TopCiphers[i] = new Cipher();
            //int count = Alphabet.Count;
            //BaseBigramTable = new double[count, count];

            // TODO is it slower ?
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
        // TODO: may be method name is incorrect
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
        // TODO: although this is brute force method, it is still rather slow
        // for sizeOfAlphabet = 33 and length of EncryptedText = 1500 
        // there 33*33*33 * 1500 * 4 ~= 215622000 "access count" to dictionaries elements and only for them it takes around 5 seconds
        public void BruteForce()
        {
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
                        // calculating probability for this cipher
                        foreach (string line in EncryptedText)
                        {
                            for (int i = 0; i < line.Length - 1; i++)
                            {
                                //char x1 = listAlphabet[(listAlphabet.IndexOf(line[i]) + cipher[i % 3]) % sizeOfAlphabet];
                                //char x2 = listAlphabet[(listAlphabet.IndexOf(line[i+1]) + cipher[(i + 1) % 3]) % sizeOfAlphabet];
                                char x1 = DctIndexToChar[(DctCharToIndex[line[i]] + cipher[i % 3]) % sizeOfAlphabet];
                                char x2 = DctIndexToChar[(DctCharToIndex[line[i + 1]] + cipher[(i + 1) % 3]) % sizeOfAlphabet];
                                p += BaseBigramTable[x1][x2];
                            }
                        }
                        // finding the minimum probability
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
                        // and change it if it's less than current
                        if(p > minimum)
                        {
                            TopCiphers[indexOfMinimum].P = p;
                            TopCiphers[indexOfMinimum].a = DctIndexToChar[a];
                            TopCiphers[indexOfMinimum].b = DctIndexToChar[b];
                            TopCiphers[indexOfMinimum].c = DctIndexToChar[c];
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
                tb.Text += $"'{cipher.a}{cipher.b}{cipher.c}' {cipher.P}\r\n";
            }
        }

        public void DecryptText(string cipher, TextBox tb)
        {
            int sizeOfAlphabet = HsAlphabet.Count;
            // TODO: change name
            int[] lol = new int[3];
            lol[0] = DctCharToIndex[cipher[0]];
            lol[1] = DctCharToIndex[cipher[1]];
            lol[2] = DctCharToIndex[cipher[2]];

            tb.Clear();
            StringBuilder sb = new StringBuilder(EncryptedText[0].Length);
            foreach (string line in EncryptedText)
            {
                sb.Length = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    sb.Append(DctIndexToChar[(DctCharToIndex[line[i]] + lol[i % 3]) % sizeOfAlphabet]);
                }
                tb.AppendText(sb.ToString());
            }
        }
    }
}
