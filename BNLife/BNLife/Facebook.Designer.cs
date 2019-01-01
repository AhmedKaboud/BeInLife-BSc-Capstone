namespace BNLife
{
    partial class Facebook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Facebook));
            this.FB_Webbrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // FB_Webbrowser
            // 
            this.FB_Webbrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FB_Webbrowser.Location = new System.Drawing.Point(0, 0);
            this.FB_Webbrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.FB_Webbrowser.Name = "FB_Webbrowser";
            this.FB_Webbrowser.Size = new System.Drawing.Size(784, 586);
            this.FB_Webbrowser.TabIndex = 0;
            this.FB_Webbrowser.Url = new System.Uri("", System.UriKind.Relative);
            this.FB_Webbrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.FB_Webbrowser_DocumentCompleted);
            // 
            // Facebook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 586);
            this.Controls.Add(this.FB_Webbrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Facebook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facebook";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Facebook_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser FB_Webbrowser;
    }
}