using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace BruteForceAttack
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

        private HashSet<char> _alphabet;
        // this dictionaries need for shifting letters according to vigenere cipher
        private Dictionary<char, int> _symbolToIndex;
        private Dictionary<int, char> _indexToSymbol;

        // char - row, char - column, double - frequency
        private Dictionary<char, Dictionary<char, double>> _baseBigramTable;

        private Cipher[] _topCiphers = new Cipher[10];

        private List<string> _encryptedText = new List<string>();

        public BruteForceSearch(string alphabet)
        {
            int sizeOfAlphabet = alphabet.Length;
            _alphabet = new HashSet<char>(sizeOfAlphabet);
            _symbolToIndex = new Dictionary<char, int>(sizeOfAlphabet);
            _indexToSymbol = new Dictionary<int, char>(sizeOfAlphabet);
            _baseBigramTable = new Dictionary<char, Dictionary<char, double>>(sizeOfAlphabet);

            int index = 0;
            foreach (char key in alphabet)
            {
                _alphabet.Add(key);
                _symbolToIndex.Add(key, index);
                _indexToSymbol.Add(index, key);

                //_baseBigramTable = alphabet.ToDictionary(k => k, k => alphabet.ToDictionary(v => v, v => 0.0));
                _baseBigramTable.Add(key, new Dictionary<char, double>(sizeOfAlphabet));
                foreach (char secondKey in alphabet)
                {
                    _baseBigramTable[key].Add(secondKey, 0.0);
                }

                index++;
            }

            for (int i = 0; i < _topCiphers.Length; i++)
                _topCiphers[i] = new Cipher();
        }

        public ReadOnlyCollection<Cipher> TopCiphers
        {
            get { return Array.AsReadOnly<Cipher>(_topCiphers); }
        }

        public ReadOnlyCollection<string> EncryptedText
        {
            get { return _encryptedText.AsReadOnly(); }
        }

        public void AnalyzeBaseFile(string fileName)
        {
            string line;
            // Read base file
            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                while ((line = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length - 1; i++)
                    {
                        if (_alphabet.Contains(line[i]) && _alphabet.Contains(line[i + 1]) &&
                            !(line[i] == ' ' && line[i + 1] == ' '))
                        {
                            _baseBigramTable[line[i]][line[i + 1]]++;
                        }
                    }
                }
            // Calculae frequencies
            foreach (char k in _alphabet)
            {
                int bigramCount = 0;
                foreach (char key in _alphabet)
                {
                    bigramCount += (int)_baseBigramTable[k][key];
                }
                foreach (char key in _alphabet)
                {
                    _baseBigramTable[k][key] /= bigramCount;
                }
            }
            // Write bigram frequency to CSV file
            using (StreamWriter sw = new StreamWriter(CSVFileName, false, Encoding.Default))
            {
                sw.WriteLine("," + string.Join(",", _alphabet));
                foreach (char key in _alphabet)
                {
                    sw.Write(key + ",");
                    sw.WriteLine(string.Join(",", _baseBigramTable[key].Values));
                }
            }

        }
        
        public void AnalyzeEncryptedFile(string fileName)
        {
            string line;

            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                while ((line = sr.ReadLine()) != null)
                {
                    _encryptedText.Add(line);
                }
        }

        // TODO: although this is brute force method, it is still rather slow
        // for sizeOfAlphabet = 33 and length of EncryptedText = 1500 
        // there 33*33*33 * 1500 * 4 ~= 215622000 "access count" to dictionaries elements and only for them it takes around 5 seconds
        public void BruteForce()
        {
            int sizeOfAlphabet = _alphabet.Count;
            int[] cipher = new int[3];

            for (int a = 0; a < sizeOfAlphabet; a++)
            {
                cipher[0] = a;
                for (int b = 0; b < sizeOfAlphabet; b++)
                {
                    cipher[1] = b;
                    for (int c = 0; c < sizeOfAlphabet; c++)
                    {
                        cipher[2] = c;
                        double p = 0;
                        // calculating probability for this cipher
                        foreach (string line in _encryptedText)
                        {
                            for (int i = 0; i < line.Length - 1; i++)
                            {
                                char x1 = _indexToSymbol[(_symbolToIndex[line[i]] + cipher[i % 3]) % sizeOfAlphabet];
                                char x2 = _indexToSymbol[(_symbolToIndex[line[i + 1]] + cipher[(i + 1) % 3]) % sizeOfAlphabet];
                                p += _baseBigramTable[x1][x2];
                            }
                        }
                        // finding the minimum probability
                        int indexOfMinimum = 0;
                        double minimum = _topCiphers[0].P;
                        for (int i = 1; i < _topCiphers.Length; i++)
                        {
                            if (_topCiphers[i].P < minimum)
                            {
                                indexOfMinimum = i;
                                minimum = _topCiphers[i].P;
                            }
                        }
                        // and change it if it's less than current
                        if (p > minimum)
                        {
                            _topCiphers[indexOfMinimum].P = p;
                            _topCiphers[indexOfMinimum].a = _indexToSymbol[a];
                            _topCiphers[indexOfMinimum].b = _indexToSymbol[b];
                            _topCiphers[indexOfMinimum].c = _indexToSymbol[c];
                        }
                    }
                }
            }
            Array.Sort(_topCiphers);
        }

        public ReadOnlyCollection<string> DecryptText(string cipher)
        {
            int sizeOfAlphabet = _alphabet.Count;
            // TODO: change name
            int[] lol = new int[3];
            lol[0] = _symbolToIndex[cipher[0]];
            lol[1] = _symbolToIndex[cipher[1]];
            lol[2] = _symbolToIndex[cipher[2]];

            List<string> decryptedText = new List<string>(_encryptedText.Count);
            StringBuilder sb = new StringBuilder(_encryptedText[0].Length);
            foreach (string line in _encryptedText)
            {
                sb.Length = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    sb.Append(_indexToSymbol[(_symbolToIndex[line[i]] + lol[i % 3]) % sizeOfAlphabet]);
                }
                decryptedText.Add(sb.ToString());
            }

            return decryptedText.AsReadOnly();
        }
    }
}
