using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Encryption2
{
    class Vigenere
    {
        private HashSet<char> HsAlphabet;

        private double[] IndexOfCoincidence;
        // this dictionaries need for shifting letters according to vigenere cipher
        private Dictionary<char, int> DctCharToIndex;
        private Dictionary<int, char> DctIndexToChar;
        private int[] shift;

        private string EncryptedText;

        public Vigenere(string alphabet)
        {
            HsAlphabet = new HashSet<char>(alphabet);
            int index = 0;
            DctCharToIndex = alphabet.ToDictionary(k => k, v => index++);
            DctIndexToChar = DctCharToIndex.ToDictionary(k => k.Value, k => k.Key);
        }

        public void ReadEncryptedFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251")))
                EncryptedText = sr.ReadToEnd();
        }

        public void EncryptedTextToTextBox(TextBox tb)
        {
            tb.Text += EncryptedText;
        }

        public void CalculateIndexOfCoincedence(int minKeyLength, int maxKeyLength)
        {
            Dictionary<char, int> symbolsCount = HsAlphabet.ToDictionary(k => k, k => 0);
            IndexOfCoincidence = new double[maxKeyLength - minKeyLength + 1];
            int indexCounter = -1;

            for (int keyLength = minKeyLength; keyLength <= maxKeyLength; keyLength++)
            {
                IndexOfCoincidence[++indexCounter] = 0;

                for (int k = 0; k < keyLength; k++)
                {
                    int totalSymbols = 0;
                    int sum = 0;
                    for (int i = k; i < EncryptedText.Length - keyLength; i += keyLength)
                    {
                        symbolsCount[EncryptedText[i]]++;
                    }

                    foreach (char symbol in HsAlphabet)
                    {
                        int symbolCount = symbolsCount[symbol];
                        sum += symbolCount * (symbolCount - 1);
                        totalSymbols += symbolCount;
                        symbolsCount[symbol] = 0;
                    }

                    IndexOfCoincidence[indexCounter] += (double)sum / (totalSymbols * (totalSymbols - 1));
                }
                IndexOfCoincidence[indexCounter] /= keyLength;
            }
        }

        public void PopulateDGV(DataGridView dgv, int minKeyLength)
        {
            for (int i = 0; i < IndexOfCoincidence.Length; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                dgv.Rows.Add();
                dgv.Rows[i].Cells[0].Value = minKeyLength++;
                dgv.Rows[i].Cells[1].Value = IndexOfCoincidence[i];
                if (IndexOfCoincidence[i] > 0.05)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                }
            }
        }

        public void DecryptText(int keyLength, TextBox tb)
        {
            Dictionary<char, int> symbolsCount = HsAlphabet.ToDictionary(k => k, k => 0);
            shift = new int[keyLength];
            int spaceIndex = DctCharToIndex[' '];
            // find shifts
            for (int k = 0; k < keyLength; k++)
            {
                foreach (char key in HsAlphabet)
                {
                    symbolsCount[key] = 0;
                }
                // count symbols
                for (int i = k; i < EncryptedText.Length - keyLength; i += keyLength)
                {
                    symbolsCount[EncryptedText[i]]++;
                }
                // find most occurrence symbol that will be 'space' character
                char encryptedSpace = symbolsCount.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                shift[k] = spaceIndex - DctCharToIndex[encryptedSpace];
                Debug.Print(shift[k] + "\n");
            }
            // dectypt text
            int sizeOfAlphabet = HsAlphabet.Count;
            tb.Clear();
            StringBuilder sb = new StringBuilder(EncryptedText.Length);
            for (int i = 0; i < EncryptedText.Length; i++)
            {
                sb.Append(DctIndexToChar[(DctCharToIndex[EncryptedText[i]] + shift[i % keyLength]) % sizeOfAlphabet]);
            }
            tb.AppendText(sb.ToString());
        }
    }
}
