using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

// Class for decoding text based on a simple monoalphabetic substitution cipher
namespace Encryption1
{
    class FrequencyDecoder
    {
        private HashSet<char> HsAlphabet;
        // char - symbol, uint - total symbol count 
        private Dictionary<char, uint> DctBaseFile;
        private Dictionary<char, uint> DctEncodedFile;

        // total symbols count
        private uint BaseCharCount = 0;
        private uint EncodedCharCount = 0;

        // match encoding char(key) to decoding char(value)
        private Dictionary<char, char> DctCharMatching;

        private List<string> EncodedText = new List<string>();

        // TODO: add support for range, like A-Z
        public FrequencyDecoder(string alphabet)
        {
            HsAlphabet = new HashSet<char>(alphabet);
            InitializeDictionaries();
        }

        public void DecodedTextToTextBox(TextBox tb)
        {
            foreach (string s in EncodedText)
            {
                tb.Text += s;
            }
        }

        public void AnalyzeBaseFile(string fileName)
        {
            string line;

            using (StreamReader sr = new StreamReader(fileName))
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (char c in line)
                    {
                        if (HsAlphabet.Contains(c))
                        {
                            DctBaseFile[c]++;
                        }
                    }
                }
        }

        public void AnalyzeEncodedFile(string fileName)
        {
            string line;

            using (StreamReader sr = new StreamReader(fileName))
                while ((line = sr.ReadLine()) != null)
                {
                    EncodedText.Add(line);
                    foreach (char c in line)
                    {
                        if (HsAlphabet.Contains(c))
                        {
                            DctEncodedFile[c]++;
                        }
                    }
                }
        }

        public void PopulateBaseDGV(DataGridView dgv)
        {
            PopulateDGV(dgv, DctBaseFile, BaseCharCount);
        }

        public void PopulateEncodedDGV(DataGridView dgv)
        {
            PopulateDGV(dgv, DctEncodedFile, EncodedCharCount);
        }

        public void DecodeText(DataGridView baseDgv, DataGridView encodedDgv, TextBox tb)
        {
            for (int i = 0; i < baseDgv.RowCount; i++)
            {
                DctCharMatching[(char)encodedDgv.Rows[i].Cells[0].Value] = (char)baseDgv.Rows[i].Cells[0].Value;
            }

            tb.Clear();
            foreach (string s in EncodedText)
            {
                char rep;
                StringBuilder sb = new StringBuilder(s);
                for (int i = 0; i < sb.Length; i++)
                {
                    sb[i] = DctCharMatching.TryGetValue(sb[i], out rep) ? rep : sb[i];
                }
                tb.Text += sb.ToString();
                sb = null;
                //tb.AppendText(sb.ToString());
                /*tb.AppendText(string.Join(string.Empty, s.Select(c =>
                {
                    char rep;
                    return DctCharMatching.TryGetValue(c, out rep) ? rep : c;
                })));*/
            }
        }

        private void InitializeDictionaries()
        {
            DctBaseFile = new Dictionary<char, uint>(HsAlphabet.Count);
            DctEncodedFile = new Dictionary<char, uint>(HsAlphabet.Count);
            DctCharMatching = new Dictionary<char, char>(HsAlphabet.Count);

            foreach (char key in HsAlphabet)
            {
                DctBaseFile.Add(key, 0);
                DctEncodedFile.Add(key, 0);
                DctCharMatching.Add(key, Char.MinValue);
            }
        }

        private void PopulateDGV(DataGridView dgv, Dictionary<char, uint> dct, uint count)
        {
            int i = 0;

            foreach (KeyValuePair<char, uint> kvp in dct)
            {
                dgv.Rows.Add();
                dgv.Rows[i].Cells[0].Value = kvp.Key;
                dgv.Rows[i].Cells[1].Value = kvp.Value;
                count += kvp.Value;
                i++;
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[2].Value = (double)((uint)row.Cells[1].Value) / count;
            }
        }
    }
}