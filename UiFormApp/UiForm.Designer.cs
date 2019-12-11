using System.Diagnostics;

namespace UiFormApp
{
    partial class UiForm
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
            this.downloadBtnLeft = new System.Windows.Forms.Button();
            this.downloadBtnRight = new System.Windows.Forms.Button();
            this.urlTextBoxLeft = new System.Windows.Forms.TextBox();
            this.urlTextBoxRight = new System.Windows.Forms.TextBox();
            this.contentTxbLeft = new System.Windows.Forms.RichTextBox();
            this.contentTxbRight = new System.Windows.Forms.RichTextBox();
            this.logLabelLeft = new System.Windows.Forms.Label();
            this.logLabelRight = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // downloadBtnLeft
            // 
            this.downloadBtnLeft.Location = new System.Drawing.Point(13, 41);
            this.downloadBtnLeft.Name = "downloadBtnLeft";
            this.downloadBtnLeft.Size = new System.Drawing.Size(324, 23);
            this.downloadBtnLeft.TabIndex = 0;
            this.downloadBtnLeft.Text = "Download";
            this.downloadBtnLeft.UseVisualStyleBackColor = true;
            this.downloadBtnLeft.Click += new System.EventHandler(this.DownloadBtnLeft_Click);
            // 
            // downloadBtnRight
            // 
            this.downloadBtnRight.Location = new System.Drawing.Point(354, 41);
            this.downloadBtnRight.Name = "downloadBtnRight";
            this.downloadBtnRight.Size = new System.Drawing.Size(324, 23);
            this.downloadBtnRight.TabIndex = 1;
            this.downloadBtnRight.Text = "Download";
            this.downloadBtnRight.UseVisualStyleBackColor = true;
            this.downloadBtnRight.Click += new System.EventHandler(this.DownloadBtnRight_Click);
            // 
            // urlTextBoxLeft
            // 
            this.urlTextBoxLeft.Location = new System.Drawing.Point(13, 12);
            this.urlTextBoxLeft.Name = "urlTextBoxLeft";
            this.urlTextBoxLeft.Size = new System.Drawing.Size(324, 20);
            this.urlTextBoxLeft.TabIndex = 2;
            // 
            // urlTextBoxRight
            // 
            this.urlTextBoxRight.Location = new System.Drawing.Point(354, 12);
            this.urlTextBoxRight.Name = "urlTextBoxRight";
            this.urlTextBoxRight.Size = new System.Drawing.Size(324, 20);
            this.urlTextBoxRight.TabIndex = 3;
            // 
            // contentTxbLeft
            // 
            this.contentTxbLeft.Location = new System.Drawing.Point(13, 74);
            this.contentTxbLeft.Name = "contentTxbLeft";
            this.contentTxbLeft.Size = new System.Drawing.Size(324, 350);
            this.contentTxbLeft.TabIndex = 4;
            this.contentTxbLeft.Text = "";
            // 
            // contentTxbRight
            // 
            this.contentTxbRight.Location = new System.Drawing.Point(354, 74);
            this.contentTxbRight.Name = "contentTxbRight";
            this.contentTxbRight.Size = new System.Drawing.Size(324, 350);
            this.contentTxbRight.TabIndex = 5;
            this.contentTxbRight.Text = "";
            // 
            // logLabelLeft
            // 
            this.logLabelLeft.AutoSize = true;
            this.logLabelLeft.Location = new System.Drawing.Point(12, 427);
            this.logLabelLeft.Name = "logLabelLeft";
            this.logLabelLeft.Size = new System.Drawing.Size(0, 13);
            this.logLabelLeft.TabIndex = 6;
            // 
            // logLabelRight
            // 
            this.logLabelRight.AutoSize = true;
            this.logLabelRight.Location = new System.Drawing.Point(351, 427);
            this.logLabelRight.Name = "logLabelRight";
            this.logLabelRight.Size = new System.Drawing.Size(0, 13);
            this.logLabelRight.TabIndex = 7;
            // 
            // UiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 470);
            this.Controls.Add(this.logLabelRight);
            this.Controls.Add(this.logLabelLeft);
            this.Controls.Add(this.contentTxbRight);
            this.Controls.Add(this.contentTxbLeft);
            this.Controls.Add(this.urlTextBoxRight);
            this.Controls.Add(this.urlTextBoxLeft);
            this.Controls.Add(this.downloadBtnRight);
            this.Controls.Add(this.downloadBtnLeft);
            this.Name = "UiForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.stopwatch = new Stopwatch();

        }

        #endregion

        private System.Windows.Forms.Button downloadBtnLeft;
        private System.Windows.Forms.Button downloadBtnRight;
        private System.Windows.Forms.TextBox urlTextBoxLeft;
        private System.Windows.Forms.TextBox urlTextBoxRight;
        private System.Windows.Forms.RichTextBox contentTxbLeft;
        private System.Windows.Forms.RichTextBox contentTxbRight;
        private System.Windows.Forms.Label logLabelLeft;
        private System.Windows.Forms.Label logLabelRight;
    }
}
