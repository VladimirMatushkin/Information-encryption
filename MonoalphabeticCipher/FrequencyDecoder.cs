using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

// Class for decryption text based on a simple monoalphabetic substitution cipher
namespace MonoalphabeticCipher
{
    class FrequencyDecoder
    {
        private readonly HashSet<char> _alphabet;
        // char - symbol, uint - symbol count 
        private readonly Dictionary<char, uint> _baseCharFrequency;
        private readonly Dictionary<char, uint> _encryptedCharFrequency;
        // matching encrypted symbol(key) to base symbol(value)
        private readonly Dictionary<char, char> _symbolMapping;

        // total symbols count
        private uint _baseCharCount = 0;
        private uint _encryptedCharCount = 0;

        private List<string> _encryptedText = new List<string>(1);

        // TODO: add support for range, like A-Z
        public FrequencyDecoder(string alphabet)
        {
            int sizeOfAlphabet = alphabet.Length;
            _alphabet = new HashSet<char>(sizeOfAlphabet);
            _baseCharFrequency = new Dictionary<char, uint>(sizeOfAlphabet);
            _encryptedCharFrequency = new Dictionary<char, uint>(sizeOfAlphabet);
            _symbolMapping = new Dictionary<char, char>(sizeOfAlphabet);

            foreach (char key in alphabet)
            {
                _alphabet.Add(key);
                _baseCharFrequency.Add(key, 0);
                _encryptedCharFrequency.Add(key, 0);
                _symbolMapping.Add(key, Char.MinValue);
            }
        }

        public ReadOnlyDictionary<char, uint> BaseCharFrequency
        {
            get { return new ReadOnlyDictionary<char, uint>(_baseCharFrequency); }
        }

        public ReadOnlyDictionary<char, uint> EncryptedCharFrequency
        {
            get { return new ReadOnlyDictionary<char, uint>(_encryptedCharFrequency); }
        }

        public Dictionary<char, char> SymbolMapping
        {
            get { return _symbolMapping; }
        }

        public uint BaseCharCount
        {
            get { return _baseCharCount; }
        }

        public uint EncryptedCharCount
        {
            get { return _encryptedCharCount; }
        }

        public ReadOnlyCollection<string> EncryptedText
        {
            get { return _encryptedText.AsReadOnly(); }
        }

        public void AnalyzeBaseFile(string fileName)
        {
            string line;

            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251")))
                while ((line = sr.ReadLine()) != null)
                    foreach (char c in line)
                        if (_alphabet.Contains(c))
                        {
                            _baseCharFrequency[c]++;
                        }

            foreach (uint symbolCount in _baseCharFrequency.Values)
                _baseCharCount += symbolCount;
        }

        public void AnalyzeEncryptedFile(string fileName)
        {
            string line;

            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251")))
                while ((line = sr.ReadLine()) != null)
                {
                    _encryptedText.Add(line);
                    foreach (char c in line)
                        if (_alphabet.Contains(c))
                        {
                            _encryptedCharFrequency[c]++;
                        }
                }

            foreach (uint symbolCount in _encryptedCharFrequency.Values)
                _encryptedCharCount += symbolCount;
        }
    }
}