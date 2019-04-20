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
            this.SuspendLayout();
            // 
            // tbPathToBaseFile
            // 
            this.tbPathToBaseFile.Location = new System.Drawing.Point(12, 12);
            this.tbPathToBaseFile.Name = "tbPathToBaseFile";
            this.tbPathToBaseFile.Size = new System.Drawing.Size(200, 20);
            this.tbPathToBaseFile.TabIndex = 0;
            // 
            // tbPathToEncryptedFile
            // 
            this.tbPathToEncryptedFile.Location = new System.Drawing.Point(12, 39);
            this.tbPathToEncryptedFile.Name = "tbPathToEncryptedFile";
            this.tbPathToEncryptedFile.Size = new System.Drawing.Size(200, 20);
            this.tbPathToEncryptedFile.TabIndex = 1;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenEncryptedFile);
            this.Controls.Add(this.btnOpenBaseFile);
            this.Controls.Add(this.tbPathToEncryptedFile);
            this.Controls.Add(this.tbPathToBaseFile);
            this.Name = "MainForm";
            this.Text = "Monoalphabetic substitution cypher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPathToBaseFile;
        private System.Windows.Forms.TextBox tbPathToEncryptedFile;
        private System.Windows.Forms.Button btnOpenBaseFile;
        private System.Windows.Forms.Button btnOpenEncryptedFile;
    }
}

