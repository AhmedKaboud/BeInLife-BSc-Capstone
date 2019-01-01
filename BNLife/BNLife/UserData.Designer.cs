namespace BNLife
{
    partial class UserData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserData));
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.btn_help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_email
            // 
            this.txt_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_email.Location = new System.Drawing.Point(270, 280);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(365, 30);
            this.txt_email.TabIndex = 0;
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.Location = new System.Drawing.Point(270, 331);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(365, 30);
            this.txt_password.TabIndex = 1;
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.Color.Transparent;
            this.btn_submit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_submit.FlatAppearance.BorderSize = 0;
            this.btn_submit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.Location = new System.Drawing.Point(396, 505);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(114, 36);
            this.btn_submit.TabIndex = 2;
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_help
            // 
            this.btn_help.BackColor = System.Drawing.Color.Transparent;
            this.btn_help.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_help.FlatAppearance.BorderSize = 0;
            this.btn_help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_help.Location = new System.Drawing.Point(430, 557);
            this.btn_help.Name = "btn_help";
            this.btn_help.Size = new System.Drawing.Size(50, 23);
            this.btn_help.TabIndex = 3;
            this.btn_help.UseVisualStyleBackColor = false;
            // 
            // UserData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BNLife.Properties.Resources.EmailPassword;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(904, 681);
            this.Controls.Add(this.btn_help);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_email);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserData";
            this.Load += new System.EventHandler(this.UserData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_help;
    }
}