using System;
using System.Drawing;
using System.Windows.Forms;

namespace PolyalphabeticCipher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static class DgvColumns
        {
            public const int KeyLength = 0;
            public const int IndexOfCoincedence = 1;
        }

        private const int MinKeyLength = 2;
        private const int MaxKeyLength = 20;
        private const double RussianTextIoC = 0.05;

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private Vigenere vigenere = new Vigenere("АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ");

        private void BtnOpenEncryptedFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbEncryptedFile.Text = openFileDialog.FileName;
                btnDecrypt.Enabled = true;

                vigenere.ReadEncryptedFile(tbEncryptedFile.Text);
                tbEncryptedText.Text = vigenere.EncryptedText;

                vigenere.CalculateIndexOfCoincedence(MinKeyLength, MaxKeyLength);
                PopulateDgv(dgvIndexOfCoincidence);
            }
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            vigenere.DecryptText(int.Parse(tbKeyLength.Text));
            tbEncryptedText.Text = vigenere.DecryptedText;
        }

        public void PopulateDgv(DataGridView dgv)
        {
            int i = 0;
            var dgvRows = dgv.Rows;
            foreach (double indexOfCoincedence in vigenere.IndexOfCoincedence)
            {
                dgvRows.Add();
                dgvRows[i].Cells[DgvColumns.KeyLength].Value = i + MinKeyLength;
                dgvRows[i].Cells[DgvColumns.IndexOfCoincedence].Value = indexOfCoincedence;
                if (indexOfCoincedence > RussianTextIoC)
                {
                    dgvRows[i].DefaultCellStyle.BackColor = Color.Aqua;
                }
                i++;
            }
        }
    }
}