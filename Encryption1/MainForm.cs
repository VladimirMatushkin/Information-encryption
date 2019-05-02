using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Encryption1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            /*var columnSymbol = new DataGridViewColumn();
            columnSymbol.ValueType = typeof(char);

            var columnFrequency = new DataGridViewColumn();
            columnSymbol.ValueType = typeof(uint);

            dgvBaseFrequency.Columns.Add(columnSymbol);
            dgvBaseFrequency.Columns.Add(columnFrequency);*/
        }

        private static class DgvColumns
        {
            public const int Symbol = 0;
            public const int SymbolCount = 1;
            public const int SymbolFrequency = 2;
        }

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private FrequencyDecoder frequencyDecoder = new FrequencyDecoder(" АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");

        private void BtnOpenBaseFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbPathToBaseFile.Text = openFileDialog.FileName;
                if (tbPathToEncryptedFile.Text != string.Empty)
                {
                    btnAnalyzeFiles.Enabled = true;
                }
            }
        }

        private void BtnOpenEncryptedFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbPathToEncryptedFile.Text = openFileDialog.FileName;
                if (tbPathToBaseFile.Text != string.Empty)
                {
                    btnAnalyzeFiles.Enabled = true;
                }
            }
        }

        private void BtnAnalyzeFiles_Click(object sender, EventArgs e)
        {
            // Base file
            frequencyDecoder.AnalyzeBaseFile(tbPathToBaseFile.Text);
            PopulateDgv(dgvRows: dgvBaseFrequency.Rows,
                        dct: frequencyDecoder.BaseCharFrequency,
                        count: frequencyDecoder.BaseCharCount);
            dgvBaseFrequency.Sort(dataGridViewColumn: dgvBaseFrequency.Columns[DgvColumns.SymbolCount],
                                  direction: ListSortDirection.Descending);
            // Encrypted file
            frequencyDecoder.AnalyzeEncryptedFile(tbPathToEncryptedFile.Text);
            PopulateDgv(dgvRows: dgvEncryptedFrequency.Rows,
                        dct: frequencyDecoder.EncryptedCharFrequency,
                        count: frequencyDecoder.EncryptedCharCount);
            dgvEncryptedFrequency.Sort(dataGridViewColumn: dgvEncryptedFrequency.Columns[DgvColumns.SymbolCount],
                                       direction: ListSortDirection.Descending);
            // Encrypted text to textBox
            tbEncryptedText.Clear();
            foreach (string s in frequencyDecoder.EncryptedText)
                tbEncryptedText.Text += s;

            btnDecryptText.Enabled = true;
        }

        private void BtnDecryptText_Click(object sender, EventArgs e)
        {
            Dictionary<char, char> symbolMapping = frequencyDecoder.SymbolMapping;
            var encryptedRows = dgvEncryptedFrequency.Rows;
            var baseRows = dgvBaseFrequency.Rows;
            // Match encrypted symbol to base symbol
            for (int i = 0; i < dgvBaseFrequency.RowCount; i++)
            {
                symbolMapping[(char)encryptedRows[i].Cells[0].Value] = (char)baseRows[i].Cells[0].Value;
            }

            // Decrypt text and put it on textBox
            tbEncryptedText.Clear();
            foreach (string s in frequencyDecoder.EncryptedText)
            {
                char rep;
                // TODO: may be do not create new instance of StringBuilder every time ?
                StringBuilder sb = new StringBuilder(s);
                for (int i = 0; i < s.Length; i++)
                {
                    sb[i] = symbolMapping.TryGetValue(sb[i], out rep) ? rep : sb[i];
                }
                tbEncryptedText.Text += sb.ToString();
                sb = null;
                //tb.AppendText(sb.ToString());
                /*tb.AppendText(string.Join(string.Empty, s.Select(c =>
                {
                    char rep;
                    return DctCharMatching.TryGetValue(c, out rep) ? rep : c;
                })));*/
            }

            btnSaveCipher.Enabled = true;
        }

        private void BtnSaveCipher_Click(object sender, EventArgs e)
        {
            var dgvRows = dgvBaseFrequency.Rows;
            using (StreamWriter sw = new StreamWriter("cipher.txt"))
                for (int i = 0; i < dgvBaseFrequency.RowCount; i++)
                {
                    sw.WriteLine("'{0}' - '{1}'", dgvRows[i].Cells[0].Value, dgvRows[i].Cells[0].Value);
                }
        }

        private void PopulateDgv(DataGridViewRowCollection dgvRows, ReadOnlyDictionary<char, uint> dct, uint count)
        {
            int i = 0;
            foreach (KeyValuePair<char, uint> kvp in dct)
            {
                dgvRows.Add();
                dgvRows[i].Cells[DgvColumns.Symbol].Value = kvp.Key;
                dgvRows[i].Cells[DgvColumns.SymbolCount].Value = kvp.Value;
                dgvRows[i].Cells[DgvColumns.SymbolFrequency].Value = (double)(kvp.Value) / count;
                i++;
            }
        }

        private void DgvEncryptedFrequency_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // If selected cells > 1 swap rows and clear selection
            if (dgvEncryptedFrequency.SelectedCells.Count > 1)
            {
                int index1 = dgvEncryptedFrequency.SelectedCells[0].RowIndex;
                int index2 = dgvEncryptedFrequency.SelectedCells[1].RowIndex;
                if (index1 != index2)
                {
                    DataGridViewRow row1 = dgvEncryptedFrequency.Rows[index1];
                    DataGridViewRow row2 = dgvEncryptedFrequency.Rows[index2];

                    char charTmp = (char)row1.Cells[DgvColumns.Symbol].Value;
                    row1.Cells[DgvColumns.Symbol].Value = row2.Cells[DgvColumns.Symbol].Value;
                    row2.Cells[DgvColumns.Symbol].Value = charTmp;

                    uint intTmp = (uint)row1.Cells[DgvColumns.SymbolCount].Value;
                    row1.Cells[DgvColumns.SymbolCount].Value = row2.Cells[DgvColumns.SymbolCount].Value;
                    row2.Cells[DgvColumns.SymbolCount].Value = intTmp;

                    double doubleTmp = (double)row1.Cells[DgvColumns.SymbolFrequency].Value;
                    row1.Cells[DgvColumns.SymbolFrequency].Value = row2.Cells[DgvColumns.SymbolFrequency].Value;
                    row2.Cells[DgvColumns.SymbolFrequency].Value = doubleTmp;

                    dgvEncryptedFrequency.ClearSelection();
                }
            }
        }

        private void DgvBaseFrequency_Scroll(object sender, ScrollEventArgs e)
        {
            dgvEncryptedFrequency.FirstDisplayedScrollingRowIndex = dgvBaseFrequency.FirstDisplayedScrollingRowIndex;
        }

        private void DgvEncryptedFrequency_Scroll(object sender, ScrollEventArgs e)
        {
            dgvBaseFrequency.FirstDisplayedScrollingRowIndex = dgvEncryptedFrequency.FirstDisplayedScrollingRowIndex;
        }
    }
}