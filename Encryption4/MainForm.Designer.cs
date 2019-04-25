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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbBaseFile
            // 
            this.tbBaseFile.Location = new System.Drawing.Point(12, 6);
            this.tbBaseFile.Name = "tbBaseFile";
            this.tbBaseFile.Size = new System.Drawing.Size(200, 20);
            this.tbBaseFile.TabIndex = 1;
            // 
            // tbEncryptedFile
            // 
            this.tbEncryptedFile.Location = new System.Drawing.Point(12, 34);
            this.tbEncryptedFile.Name = "tbEncryptedFile";
            this.tbEncryptedFile.Size = new System.Drawing.Size(200, 20);
            this.tbEncryptedFile.TabIndex = 2;
            // 
            // btnSelectBaseFile
            // 
            this.btnSelectBaseFile.Location = new System.Drawing.Point(218, 4);
            this.btnSelectBaseFile.Name = "btnSelectBaseFile";
            this.btnSelectBaseFile.Size = new System.Drawing.Size(110, 23);
            this.btnSelectBaseFile.TabIndex = 3;
            this.btnSelectBaseFile.Text = "Open base file";
            this.btnSelectBaseFile.UseVisualStyleBackColor = true;
            this.btnSelectBaseFile.Click += new System.EventHandler(this.BtnOpenBaseFile_Click);
            // 
            // btnOpenEncryptedFile
            // 
            this.btnOpenEncryptedFile.Location = new System.Drawing.Point(218, 31);
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
            this.tbEncryptedText.Size = new System.Drawing.Size(500, 388);
            this.tbEncryptedText.TabIndex = 5;
            // 
            // btnOpenBigramTable
            // 
            this.btnOpenBigramTable.Enabled = false;
            this.btnOpenBigramTable.Location = new System.Drawing.Point(334, 3);
            this.btnOpenBigramTable.Name = "btnOpenBigramTable";
            this.btnOpenBigramTable.Size = new System.Drawing.Size(103, 23);
            this.btnOpenBigramTable.TabIndex = 8;
            this.btnOpenBigramTable.Text = "Open bigram table";
            this.btnOpenBigramTable.UseVisualStyleBackColor = true;
            this.btnOpenBigramTable.Click += new System.EventHandler(this.BtnOpenBigramTable_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(604, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(549, 61);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(265, 388);
            this.textBox1.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

