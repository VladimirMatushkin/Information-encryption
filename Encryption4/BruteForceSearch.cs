using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Encryption4
{
    class BruteForceSearch
    {
        private HashSet<char> HsAlphabet;
        private double[,] BaseBigramTable;
        private Dictionary<char, Dictionary<char, double>> b;

        public BruteForceSearch(string alphabet)
        {
            HsAlphabet = new HashSet<char>(alphabet);
            int count = HsAlphabet.Count;
            BaseBigramTable = new double[count, count];

            //b = new Dictionary<char, Dictionary<char, double>>(count);
            // TODO is it slower ?
            b = HsAlphabet.ToDictionary(k => k, k => HsAlphabet.ToDictionary(v => v, v => 0.0));

        }

        public void AnalyzeBaseFile(string fileName)
        {
            string line;

            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251")))
                while ((line = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length - 1; i++)
                    {
                        if (HsAlphabet.Contains(line[i]) && HsAlphabet.Contains(line[i + 1]) && !(line[i] == ' ' && line[i] == line[i + 1]))
                        {
                            b[line[i]][line[i + 1]]++;
                        }
                    }
                }

            foreach (char k in HsAlphabet)
            {
                int bigramCount = 0;
                foreach (char key in HsAlphabet)
                {
                    bigramCount += (int)b[k][key];
                }
                foreach (char key in HsAlphabet)
                {
                    b[k][key] /= bigramCount;
                }
            }

            using (StreamWriter sw = new StreamWriter("bigram frequency.csv", false, Encoding.GetEncoding("Windows-1251")))
            {
                sw.WriteLine("," + string.Join(",", HsAlphabet));
                foreach (char key in HsAlphabet)
                {
                    sw.Write(key + ",");
                    sw.WriteLine(string.Join(",", b[key].Values));
                }
            }

        }
    }
}
