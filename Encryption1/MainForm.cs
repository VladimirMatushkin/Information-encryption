using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

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

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private FrequencyDecoder frequencyDecoder = new FrequencyDecoder(" АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");

        private void BtnOpenBaseFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbPathToBaseFile.Text = openFileDialog.FileName;
            }
        }

        private void BtnOpenEncryptedFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbPathToEncryptedFile.Text = openFileDialog.FileName;
            }
        }

        private void BtnAnalyzeFiles_Click(object sender, EventArgs e)
        {
            frequencyDecoder.AnalyzeBaseFile(tbPathToBaseFile.Text);
            frequencyDecoder.PopulateBaseDGV(dgvBaseFrequency);
            dgvBaseFrequency.Sort(dgvBaseFrequency.Columns[1], ListSortDirection.Descending);

            frequencyDecoder.AnalyzeEncodedFile(tbPathToEncryptedFile.Text);
            frequencyDecoder.PopulateEncodedDGV(dgvEncryptedFrequency);
            dgvEncryptedFrequency.Sort(dgvEncryptedFrequency.Columns[1], ListSortDirection.Descending);

            frequencyDecoder.DecodedTextToTextBox(tbEncryptedText);
        }

        private void DgvEncryptedFrequency_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvEncryptedFrequency.SelectedCells.Count > 1)
            {
                int index1 = dgvEncryptedFrequency.SelectedCells[0].RowIndex;
                int index2 = dgvEncryptedFrequency.SelectedCells[1].RowIndex;
                if (index1 != index2)
                {
                    DataGridViewRow row1 = dgvEncryptedFrequency.Rows[index1];
                    DataGridViewRow row2 = dgvEncryptedFrequency.Rows[index2];

                    char charTmp;
                    charTmp = (char)row1.Cells[0].Value;
                    row1.Cells[0].Value = row2.Cells[0].Value;
                    row2.Cells[0].Value = charTmp;


                    uint intTmp;
                    intTmp = (uint)row1.Cells[1].Value;
                    row1.Cells[1].Value = row2.Cells[1].Value;
                    row2.Cells[1].Value = intTmp;

                    double doubleTmp;
                    doubleTmp = (double)row1.Cells[2].Value;
                    row1.Cells[2].Value = row2.Cells[2].Value;
                    row2.Cells[2].Value = doubleTmp;

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

        private void BtnDecryptText_Click(object sender, EventArgs e)
        {
            frequencyDecoder.DecodeText(dgvBaseFrequency, dgvEncryptedFrequency, tbEncryptedText);
        }

        private void BtnSaveCipher_Click(object sender, EventArgs e)
        {
            using(StreamWriter sw = new StreamWriter("cipher.txt"))
            {
                for(int i = 0; i < dgvBaseFrequency.RowCount; i++)
                {
                    sw.WriteLine("'{0}' - '{1}'", dgvBaseFrequency.Rows[i].Cells[0].Value, dgvEncryptedFrequency.Rows[i].Cells[0].Value);
                }
            }
        }
    }
}
