namespace PolyalphabeticCipher
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
            this.tbEncryptedFile = new System.Windows.Forms.TextBox();
            this.btnOpenEncryptedFile = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.tbEncryptedText = new System.Windows.Forms.TextBox();
            this.dgvIndexOfCoincidence = new System.Windows.Forms.DataGridView();
            this.KeyLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndexOfCoincidence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbKeyLength = new System.Windows.Forms.TextBox();
            this.lblKeyLength = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndexOfCoincidence)).BeginInit();
            this.SuspendLayout();
            // 
            // tbEncryptedFile
            // 
            this.tbEncryptedFile.Location = new System.Drawing.Point(13, 13);
            this.tbEncryptedFile.Name = "tbEncryptedFile";
            this.tbEncryptedFile.Size = new System.Drawing.Size(150, 20);
            this.tbEncryptedFile.TabIndex = 0;
            // 
            // btnOpenEncryptedFile
            // 
            this.btnOpenEncryptedFile.Location = new System.Drawing.Point(179, 11);
            this.btnOpenEncryptedFile.Name = "btnOpenEncryptedFile";
            this.btnOpenEncryptedFile.Size = new System.Drawing.Size(110, 23);
            this.btnOpenEncryptedFile.TabIndex = 1;
            this.btnOpenEncryptedFile.Text = "Open encrypted file";
            this.btnOpenEncryptedFile.UseVisualStyleBackColor = true;
            this.btnOpenEncryptedFile.Click += new System.EventHandler(this.BtnOpenEncryptedFile_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Enabled = false;
            this.btnDecrypt.Location = new System.Drawing.Point(674, 12);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(54, 23);
            this.btnDecrypt.TabIndex = 2;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.BtnDecrypt_Click);
            // 
            // tbEncryptedText
            // 
            this.tbEncryptedText.Location = new System.Drawing.Point(13, 40);
            this.tbEncryptedText.Multiline = true;
            this.tbEncryptedText.Name = "tbEncryptedText";
            this.tbEncryptedText.Size = new System.Drawing.Size(531, 398);
            this.tbEncryptedText.TabIndex = 3;
            // 
            // dgvIndexOfCoincidence
            // 
            this.dgvIndexOfCoincidence.AllowUserToAddRows = false;
            this.dgvIndexOfCoincidence.AllowUserToDeleteRows = false;
            this.dgvIndexOfCoincidence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIndexOfCoincidence.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KeyLength,
            this.IndexOfCoincidence});
            this.dgvIndexOfCoincidence.Location = new System.Drawing.Point(550, 41);
            this.dgvIndexOfCoincidence.Name = "dgvIndexOfCoincidence";
            this.dgvIndexOfCoincidence.ReadOnly = true;
            this.dgvIndexOfCoincidence.RowHeadersVisible = false;
            this.dgvIndexOfCoincidence.Size = new System.Drawing.Size(180, 397);
            this.dgvIndexOfCoincidence.TabIndex = 4;
            // 
            // KeyLength
            // 
            this.KeyLength.HeaderText = "Длина ключа";
            this.KeyLength.Name = "KeyLength";
            this.KeyLength.ReadOnly = true;
            this.KeyLength.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.KeyLength.Width = 60;
            // 
            // IndexOfCoincidence
            // 
            this.IndexOfCoincidence.HeaderText = "Индекс";
            this.IndexOfCoincidence.Name = "IndexOfCoincidence";
            this.IndexOfCoincidence.ReadOnly = true;
            this.IndexOfCoincidence.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // tbKeyLength
            // 
            this.tbKeyLength.Location = new System.Drawing.Point(623, 13);
            this.tbKeyLength.Name = "tbKeyLength";
            this.tbKeyLength.Size = new System.Drawing.Size(45, 20);
            this.tbKeyLength.TabIndex = 5;
            // 
            // lblKeyLength
            // 
            this.lblKeyLength.AutoSize = true;
            this.lblKeyLength.Location = new System.Drawing.Point(547, 17);
            this.lblKeyLength.Name = "lblKeyLength";
            this.lblKeyLength.Size = new System.Drawing.Size(57, 13);
            this.lblKeyLength.TabIndex = 6;
            this.lblKeyLength.Text = "Key length";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 450);
            this.Controls.Add(this.lblKeyLength);
            this.Controls.Add(this.tbKeyLength);
            this.Controls.Add(this.dgvIndexOfCoincidence);
            this.Controls.Add(this.tbEncryptedText);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnOpenEncryptedFile);
            this.Controls.Add(this.tbEncryptedFile);
            this.Name = "MainForm";
            this.Text = "Vigenere";
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndexOfCoincidence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbEncryptedFile;
        private System.Windows.Forms.Button btnOpenEncryptedFile;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.TextBox tbEncryptedText;
        private System.Windows.Forms.DataGridView dgvIndexOfCoincidence;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexOfCoincidence;
        private System.Windows.Forms.TextBox tbKeyLength;
        private System.Windows.Forms.Label lblKeyLength;
    }
}

