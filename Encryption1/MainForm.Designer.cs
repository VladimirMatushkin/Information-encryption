namespace Encryption1
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
            this.tbPathToBaseFile = new System.Windows.Forms.TextBox();
            this.tbPathToEncryptedFile = new System.Windows.Forms.TextBox();
            this.btnOpenBaseFile = new System.Windows.Forms.Button();
            this.btnOpenEncryptedFile = new System.Windows.Forms.Button();
            this.tbEncryptedText = new System.Windows.Forms.TextBox();
            this.dgvBaseFrequency = new System.Windows.Forms.DataGridView();
            this.dgvEncryptedFrequency = new System.Windows.Forms.DataGridView();
            this.btnAnalyzeFiles = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDecryptText = new System.Windows.Forms.Button();
            this.btnSaveCipher = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaseFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEncryptedFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPathToBaseFile
            // 
            this.tbPathToBaseFile.Location = new System.Drawing.Point(12, 12);
            this.tbPathToBaseFile.Name = "tbPathToBaseFile";
            this.tbPathToBaseFile.Size = new System.Drawing.Size(200, 20);
            this.tbPathToBaseFile.TabIndex = 0;
            this.tbPathToBaseFile.Text = "belkin.txt";
            // 
            // tbPathToEncryptedFile
            // 
            this.tbPathToEncryptedFile.Location = new System.Drawing.Point(12, 39);
            this.tbPathToEncryptedFile.Name = "tbPathToEncryptedFile";
            this.tbPathToEncryptedFile.Size = new System.Drawing.Size(200, 20);
            this.tbPathToEncryptedFile.TabIndex = 1;
            this.tbPathToEncryptedFile.Text = "case9";
            // 
            // btnOpenBaseFile
            // 
            this.btnOpenBaseFile.Location = new System.Drawing.Point(218, 9);
            this.btnOpenBaseFile.Name = "btnOpenBaseFile";
            this.btnOpenBaseFile.Size = new System.Drawing.Size(110, 23);
            this.btnOpenBaseFile.TabIndex = 2;
            this.btnOpenBaseFile.Text = "Open base file";
            this.btnOpenBaseFile.UseVisualStyleBackColor = true;
            this.btnOpenBaseFile.Click += new System.EventHandler(this.BtnOpenBaseFile_Click);
            // 
            // btnOpenEncryptedFile
            // 
            this.btnOpenEncryptedFile.Location = new System.Drawing.Point(218, 39);
            this.btnOpenEncryptedFile.Name = "btnOpenEncryptedFile";
            this.btnOpenEncryptedFile.Size = new System.Drawing.Size(110, 23);
            this.btnOpenEncryptedFile.TabIndex = 3;
            this.btnOpenEncryptedFile.Text = "Open encrypted file";
            this.btnOpenEncryptedFile.UseVisualStyleBackColor = true;
            this.btnOpenEncryptedFile.Click += new System.EventHandler(this.BtnOpenEncryptedFile_Click);
            // 
            // tbEncryptedText
            // 
            this.tbEncryptedText.Location = new System.Drawing.Point(12, 68);
            this.tbEncryptedText.Multiline = true;
            this.tbEncryptedText.Name = "tbEncryptedText";
            this.tbEncryptedText.Size = new System.Drawing.Size(513, 410);
            this.tbEncryptedText.TabIndex = 4;
            // 
            // dgvBaseFrequency
            // 
            this.dgvBaseFrequency.AllowUserToAddRows = false;
            this.dgvBaseFrequency.AllowUserToDeleteRows = false;
            this.dgvBaseFrequency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaseFrequency.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvBaseFrequency.Location = new System.Drawing.Point(531, 25);
            this.dgvBaseFrequency.Name = "dgvBaseFrequency";
            this.dgvBaseFrequency.ReadOnly = true;
            this.dgvBaseFrequency.RowHeadersVisible = false;
            this.dgvBaseFrequency.Size = new System.Drawing.Size(200, 427);
            this.dgvBaseFrequency.TabIndex = 5;
            this.dgvBaseFrequency.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DgvBaseFrequency_Scroll);
            // 
            // dgvEncryptedFrequency
            // 
            this.dgvEncryptedFrequency.AllowUserToAddRows = false;
            this.dgvEncryptedFrequency.AllowUserToDeleteRows = false;
            this.dgvEncryptedFrequency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEncryptedFrequency.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvEncryptedFrequency.Location = new System.Drawing.Point(744, 25);
            this.dgvEncryptedFrequency.Name = "dgvEncryptedFrequency";
            this.dgvEncryptedFrequency.ReadOnly = true;
            this.dgvEncryptedFrequency.RowHeadersVisible = false;
            this.dgvEncryptedFrequency.Size = new System.Drawing.Size(200, 427);
            this.dgvEncryptedFrequency.TabIndex = 6;
            this.dgvEncryptedFrequency.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvEncryptedFrequency_CellMouseClick);
            this.dgvEncryptedFrequency.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DgvEncryptedFrequency_Scroll);
            // 
            // btnAnalyzeFiles
            // 
            this.btnAnalyzeFiles.Location = new System.Drawing.Point(531, 458);
            this.btnAnalyzeFiles.Name = "btnAnalyzeFiles";
            this.btnAnalyzeFiles.Size = new System.Drawing.Size(75, 23);
            this.btnAnalyzeFiles.TabIndex = 7;
            this.btnAnalyzeFiles.Text = "Analyze files";
            this.btnAnalyzeFiles.UseVisualStyleBackColor = true;
            this.btnAnalyzeFiles.Click += new System.EventHandler(this.BtnAnalyzeFiles_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Symbol";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Count";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Frequency";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.Width = 80;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Symbol";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Count";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Frequency";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // btnDecryptText
            // 
            this.btnDecryptText.Location = new System.Drawing.Point(627, 458);
            this.btnDecryptText.Name = "btnDecryptText";
            this.btnDecryptText.Size = new System.Drawing.Size(75, 23);
            this.btnDecryptText.TabIndex = 8;
            this.btnDecryptText.Text = "Decrypt text";
            this.btnDecryptText.UseVisualStyleBackColor = true;
            this.btnDecryptText.Click += new System.EventHandler(this.BtnDecryptText_Click);
            // 
            // btnSaveCipher
            // 
            this.btnSaveCipher.Location = new System.Drawing.Point(718, 458);
            this.btnSaveCipher.Name = "btnSaveCipher";
            this.btnSaveCipher.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCipher.TabIndex = 9;
            this.btnSaveCipher.Text = "Save cipher";
            this.btnSaveCipher.UseVisualStyleBackColor = true;
            this.btnSaveCipher.Click += new System.EventHandler(this.BtnSaveCipher_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 490);
            this.Controls.Add(this.btnSaveCipher);
            this.Controls.Add(this.btnDecryptText);
            this.Controls.Add(this.btnAnalyzeFiles);
            this.Controls.Add(this.dgvEncryptedFrequency);
            this.Controls.Add(this.dgvBaseFrequency);
            this.Controls.Add(this.tbEncryptedText);
            this.Controls.Add(this.btnOpenEncryptedFile);
            this.Controls.Add(this.btnOpenBaseFile);
            this.Controls.Add(this.tbPathToEncryptedFile);
            this.Controls.Add(this.tbPathToBaseFile);
            this.Name = "MainForm";
            this.Text = "Monoalphabetic substitution cypher";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaseFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEncryptedFrequency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPathToBaseFile;
        private System.Windows.Forms.TextBox tbPathToEncryptedFile;
        private System.Windows.Forms.Button btnOpenBaseFile;
        private System.Windows.Forms.Button btnOpenEncryptedFile;
        private System.Windows.Forms.TextBox tbEncryptedText;
        private System.Windows.Forms.DataGridView dgvBaseFrequency;
        private System.Windows.Forms.DataGridView dgvEncryptedFrequency;
        private System.Windows.Forms.Button btnAnalyzeFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button btnDecryptText;
        private System.Windows.Forms.Button btnSaveCipher;
    }
}

