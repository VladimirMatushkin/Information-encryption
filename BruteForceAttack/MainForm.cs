using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace BruteForceAttack
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private BruteForceSearch bfs = new BruteForceSearch(" АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");

        private void BtnOpenBaseFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbBaseFile.Text = openFileDialog.FileName;
                btnSelectBaseFile.Enabled = false;

                bfs.AnalyzeBaseFile(tbBaseFile.Text);
                btnOpenBigramTable.Enabled = true;
            }
        }

        private void BtnOpenEncryptedFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbEncryptedFile.Text = openFileDialog.FileName;
                btnOpenEncryptedFile.Enabled = false;

                bfs.AnalyzeEncryptedFile(tbEncryptedFile.Text);

                foreach (string s in bfs.EncryptedText)
                {
                    tbEncryptedText.Text += s;
                }
            }
        }

        private void BtnOpenBigramTable_Click(object sender, EventArgs e)
        {
            Process.Start(BruteForceSearch.CSVFileName);
        }

        private void BtnFindTopCiphers_Click(object sender, EventArgs e)
        {
            bfs.BruteForce();

            tbTopCiphers.Clear();
            foreach (Cipher cipher in bfs.TopCiphers)
            {
                tbTopCiphers.Text += $"'{cipher.a}{cipher.b}{cipher.c}' {cipher.P}\r\n";
            }
        }

        private void BtnDecryptText_Click(object sender, EventArgs e)
        {
            ReadOnlyCollection<string> decryptedText = bfs.DecryptText(tbCipher.Text);

            tbEncryptedText.Clear();
            foreach (string decryptedLine in decryptedText)
            {
                tbEncryptedText.Text += decryptedLine;
            }
        }
    }
}
