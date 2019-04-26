namespace Encryption4
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbBaseFile = new System.Windows.Forms.TextBox();
            this.tbEncryptedFile = new System.Windows.Forms.TextBox();
            this.btnSelectBaseFile = new System.Windows.Forms.Button();
            this.btnOpenEncryptedFile = new System.Windows.Forms.Button();
            this.tbEncryptedText = new System.Windows.Forms.TextBox();
            this.btnOpenBigramTable = new System.Windows.Forms.Button();
            this.btnFindTopCiphers = new System.Windows.Forms.Button();
            this.tbTopCiphers = new System.Windows.Forms.TextBox();
            this.tbCipher = new System.Windows.Forms.TextBox();
            this.btnDecryptText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbBaseFile
            // 
            this.tbBaseFile.Location = new System.Drawing.Point(12, 6);
            this.tbBaseFile.Name = "tbBaseFile";
            this.tbBaseFile.Size = new System.Drawing.Size(275, 20);
            this.tbBaseFile.TabIndex = 1;
            // 
            // tbEncryptedFile
            // 
            this.tbEncryptedFile.Location = new System.Drawing.Point(12, 34);
            this.tbEncryptedFile.Name = "tbEncryptedFile";
            this.tbEncryptedFile.Size = new System.Drawing.Size(275, 20);
            this.tbEncryptedFile.TabIndex = 2;
            // 
            // btnSelectBaseFile
            // 
            this.btnSelectBaseFile.Location = new System.Drawing.Point(293, 3);
            this.btnSelectBaseFile.Name = "btnSelectBaseFile";
            this.btnSelectBaseFile.Size = new System.Drawing.Size(110, 23);
            this.btnSelectBaseFile.TabIndex = 3;
            this.btnSelectBaseFile.Text = "Open base file";
            this.btnSelectBaseFile.UseVisualStyleBackColor = true;
            this.btnSelectBaseFile.Click += new System.EventHandler(this.BtnOpenBaseFile_Click);
            // 
            // btnOpenEncryptedFile
            // 
            this.btnOpenEncryptedFile.Location = new System.Drawing.Point(293, 32);
            this.btnOpenEncryptedFile.Name = "btnOpenEncryptedFile";
            this.btnOpenEncryptedFile.Size = new System.Drawing.Size(110, 23);
            this.btnOpenEncryptedFile.TabIndex = 4;
            this.btnOpenEncryptedFile.Text = "Open encrypted file";
            this.btnOpenEncryptedFile.UseVisualStyleBackColor = true;
            this.btnOpenEncryptedFile.Click += new System.EventHandler(this.BtnOpenEncryptedFile_Click);
            // 
            // tbEncryptedText
            // 
            this.tbEncryptedText.Location = new System.Drawing.Point(12, 61);
            this.tbEncryptedText.Multiline = true;
            this.tbEncryptedText.Name = "tbEncryptedText";
            this.tbEncryptedText.Size = new System.Drawing.Size(589, 388);
            this.tbEncryptedText.TabIndex = 5;
            // 
            // btnOpenBigramTable
            // 
            this.btnOpenBigramTable.Enabled = false;
            this.btnOpenBigramTable.Location = new System.Drawing.Point(409, 3);
            this.btnOpenBigramTable.Name = "btnOpenBigramTable";
            this.btnOpenBigramTable.Size = new System.Drawing.Size(103, 23);
            this.btnOpenBigramTable.TabIndex = 8;
            this.btnOpenBigramTable.Text = "Open bigram table";
            this.btnOpenBigramTable.UseVisualStyleBackColor = true;
            this.btnOpenBigramTable.Click += new System.EventHandler(this.BtnOpenBigramTable_Click);
            // 
            // btnFindTopCiphers
            // 
            this.btnFindTopCiphers.Location = new System.Drawing.Point(607, 4);
            this.btnFindTopCiphers.Name = "btnFindTopCiphers";
            this.btnFindTopCiphers.Size = new System.Drawing.Size(107, 23);
            this.btnFindTopCiphers.TabIndex = 9;
            this.btnFindTopCiphers.Text = "Find top 10 ciphers";
            this.btnFindTopCiphers.UseVisualStyleBackColor = true;
            this.btnFindTopCiphers.Click += new System.EventHandler(this.BtnFindTopCiphers_Click);
            // 
            // tbTopCiphers
            // 
            this.tbTopCiphers.Location = new System.Drawing.Point(607, 61);
            this.tbTopCiphers.Multiline = true;
            this.tbTopCiphers.Name = "tbTopCiphers";
            this.tbTopCiphers.Size = new System.Drawing.Size(178, 207);
            this.tbTopCiphers.TabIndex = 10;
            // 
            // tbCipher
            // 
            this.tbCipher.Location = new System.Drawing.Point(607, 291);
            this.tbCipher.Name = "tbCipher";
            this.tbCipher.Size = new System.Drawing.Size(48, 20);
            this.tbCipher.TabIndex = 11;
            // 
            // btnDecryptText
            // 
            this.btnDecryptText.Location = new System.Drawing.Point(710, 289);
            this.btnDecryptText.Name = "btnDecryptText";
            this.btnDecryptText.Size = new System.Drawing.Size(75, 23);
            this.btnDecryptText.TabIndex = 12;
            this.btnDecryptText.Text = "Decrypt text";
            this.btnDecryptText.UseVisualStyleBackColor = true;
            this.btnDecryptText.Click += new System.EventHandler(this.BtnDecryptText_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 461);
            this.Controls.Add(this.btnDecryptText);
            this.Controls.Add(this.tbCipher);
            this.Controls.Add(this.tbTopCiphers);
            this.Controls.Add(this.btnFindTopCiphers);
            this.Controls.Add(this.btnOpenBigramTable);
            this.Controls.Add(this.tbEncryptedText);
            this.Controls.Add(this.btnOpenEncryptedFile);
            this.Controls.Add(this.btnSelectBaseFile);
            this.Controls.Add(this.tbEncryptedFile);
            this.Controls.Add(this.tbBaseFile);
            this.Name = "MainForm";
            this.Text = "Brute force search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBaseFile;
        private System.Windows.Forms.TextBox tbEncryptedFile;
        private System.Windows.Forms.Button btnSelectBaseFile;
        private System.Windows.Forms.Button btnOpenEncryptedFile;
        private System.Windows.Forms.TextBox tbEncryptedText;
        private System.Windows.Forms.Button btnOpenBigramTable;
        private System.Windows.Forms.Button btnFindTopCiphers;
        private System.Windows.Forms.TextBox tbTopCiphers;
        private System.Windows.Forms.TextBox tbCipher;
        private System.Windows.Forms.Button btnDecryptText;
    }
}

