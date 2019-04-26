using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace Encryption4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private BruteForceSearch bfs = new BruteForceSearch("АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ");

        private void BtnOpenBaseFile_Click(object sender, EventArgs e)
        { 
            if(openFileDialog.ShowDialog() == DialogResult.OK)
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
                bfs.EncryptedTextToTextBox(tbEncryptedText);
            }
        }

        private void BtnOpenBigramTable_Click(object sender, EventArgs e)
        {
            Process.Start(BruteForceSearch.CSVFileName);
        }

        private void BtnFindTopCiphers_Click(object sender, EventArgs e)
        {
            bfs.BruteForce();
            bfs.TopCiphersToTextBox(tbTopCiphers);
        }

        private void BtnDecryptText_Click(object sender, EventArgs e)
        {
            bfs.DecryptText(tbCipher.Text, tbEncryptedText);
        }
    }
}

