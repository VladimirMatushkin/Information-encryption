using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Vigenere vigenere = new Vigenere("АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ");

        private void BtnOpenEncryptedFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbEncryptedFile.Text = openFileDialog.FileName;
                vigenere.ReadEncryptedFile(tbEncryptedFile.Text);
                vigenere.EncryptedTextToTextBox(tbEncryptedText);
                vigenere.CalculateIndexOfCoincedence(2, 20);
                vigenere.PopulateDGV(dgvIndexOfCoincidence, 2);

                openFileDialog.Dispose();
            }
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            vigenere.DecryptText(int.Parse(tbKeyLength.Text), tbEncryptedText);
        }
    }
}
