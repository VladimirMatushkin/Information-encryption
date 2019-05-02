using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Encryption2
{
    class Vigenere
    {
        private readonly HashSet<char> _alphabet;

        private double[] _indexOfCoincidence;
        // this dictionaries need for shifting letters according to vigenere cipher
        private readonly Dictionary<char, int> _symbolToIndex;
        private readonly Dictionary<int, char> _indexToSymbol;
        // difference between base and encrypted symbol
        private int[] _shift;

        private string _encryptedText;
        private StringBuilder _decryptedText;

        public Vigenere(string alphabet)
        {
            int sizeOfAlphabet = alphabet.Length;
            _alphabet = new HashSet<char>(sizeOfAlphabet);
            _symbolToIndex = new Dictionary<char, int>(sizeOfAlphabet);
            _indexToSymbol = new Dictionary<int, char>(sizeOfAlphabet);

            int index = 0;
            foreach (char key in alphabet)
            {
                _alphabet.Add(key);
                _symbolToIndex.Add(key, index);
                _indexToSymbol.Add(index, key);
                index++;
            }
        }

        public ReadOnlyCollection<double> IndexOfCoincedence
        {
            get { return Array.AsReadOnly<double>(_indexOfCoincidence); }
        }

        public string EncryptedText
        {
            get { return _encryptedText; }
        }

        public string DecryptedText
        {
            get { return _decryptedText.ToString(); }
        }

        public void ReadEncryptedFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251")))
                _encryptedText = sr.ReadToEnd();

            _decryptedText = new StringBuilder(_encryptedText.Length);
        }

        public void CalculateIndexOfCoincedence(int minKeyLength, int maxKeyLength)
        {
            Dictionary<char, int> symbolsCount = _alphabet.ToDictionary(k => k, k => 0);
            _indexOfCoincidence = new double[maxKeyLength - minKeyLength + 1];
            int indexCounter = -1;

            for (int keyLength = minKeyLength; keyLength <= maxKeyLength; keyLength++)
            {
                _indexOfCoincidence[++indexCounter] = 0;

                for (int k = 0; k < keyLength; k++)
                {
                    int totalSymbols = 0;
                    int sum = 0;
                    for (int i = k; i < _encryptedText.Length - keyLength; i += keyLength)
                    {
                        symbolsCount[_encryptedText[i]]++;
                    }

                    foreach (char symbol in _alphabet)
                    {
                        int symbolCount = symbolsCount[symbol];
                        sum += symbolCount * (symbolCount - 1);
                        totalSymbols += symbolCount;
                        symbolsCount[symbol] = 0;
                    }

                    _indexOfCoincidence[indexCounter] += (double)sum / (totalSymbols * (totalSymbols - 1));
                }
                _indexOfCoincidence[indexCounter] /= keyLength;
            }
        }

        public void DecryptText(int keyLength)
        {
            Dictionary<char, int> symbolsCount = _alphabet.ToDictionary(k => k, k => 0);
            _shift = new int[keyLength];
            int spaceIndex = _symbolToIndex[' '];

            // Find shifts
            for (int k = 0; k < keyLength; k++)
            {
                foreach (char key in _alphabet)
                {
                    symbolsCount[key] = 0;
                }
                // Count symbols
                for (int i = k; i < _encryptedText.Length - keyLength; i += keyLength)
                {
                    symbolsCount[_encryptedText[i]]++;
                }
                // Find most occurrence symbol that will be 'space' character
                char encryptedSpace = symbolsCount.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                _shift[k] = spaceIndex - _symbolToIndex[encryptedSpace];
            }

            // Decrypt text
            int sizeOfAlphabet = _alphabet.Count;
            _decryptedText.Length = 0;
            for (int i = 0; i < _encryptedText.Length; i++)
            {
                int indexOfEncryptedSymbol = _symbolToIndex[_encryptedText[i]];
                int indexOfDecryptedSymbol = (indexOfEncryptedSymbol + _shift[i % keyLength]);
                if (indexOfDecryptedSymbol < 0)
                    indexOfDecryptedSymbol += sizeOfAlphabet;
                char decryptedSymbol = _indexToSymbol[indexOfDecryptedSymbol % sizeOfAlphabet];
                _decryptedText.Append(decryptedSymbol);
            }
        }
    }
}