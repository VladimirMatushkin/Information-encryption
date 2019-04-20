using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private BruteForceSearch bfs = new BruteForceSearch(" АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");

        private void BtnSelectBaseFile_Click(object sender, EventArgs e)
        { 
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbBaseFile.Text = openFileDialog.FileName;
            }
        }

        private void BtnOpenEncryptedFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbEncryptedFile.Text = openFileDialog.FileName;
            }
        }

        private void BtnAnalyzeBaseFile_Click(object sender, EventArgs e)
        {
            bfs.AnalyzeBaseFile(tbBaseFile.Text);
        }
    }
}

