namespace BNLife
{
    partial class Home
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.PicBox_Login = new System.Windows.Forms.PictureBox();
            this.btn_signup = new System.Windows.Forms.Button();
            this.btn_Log_in = new System.Windows.Forms.Button();
            this.timer_login = new System.Windows.Forms.Timer(this.components);
            this.lbl_timer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_Login)).BeginInit();
            this.SuspendLayout();
            // 
            // PicBox_Login
            // 
            this.PicBox_Login.BackColor = System.Drawing.Color.Transparent;
            this.PicBox_Login.Location = new System.Drawing.Point(295, 247);
            this.PicBox_Login.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PicBox_Login.Name = "PicBox_Login";
            this.PicBox_Login.Size = new System.Drawing.Size(308, 260);
            this.PicBox_Login.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBox_Login.TabIndex = 4;
            this.PicBox_Login.TabStop = false;
            // 
            // btn_signup
            // 
            this.btn_signup.BackColor = System.Drawing.Color.Transparent;
            this.btn_signup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_signup.FlatAppearance.BorderSize = 0;
            this.btn_signup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_signup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_signup.Location = new System.Drawing.Point(418, 550);
            this.btn_signup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_signup.Name = "btn_signup";
            this.btn_signup.Size = new System.Drawing.Size(66, 22);
            this.btn_signup.TabIndex = 7;
            this.btn_signup.UseVisualStyleBackColor = false;
            this.btn_signup.Click += new System.EventHandler(this.btn_signup_Click);
            // 
            // btn_Log_in
            // 
            this.btn_Log_in.BackColor = System.Drawing.Color.Transparent;
            this.btn_Log_in.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Log_in.FlatAppearance.BorderSize = 0;
            this.btn_Log_in.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Log_in.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Log_in.Location = new System.Drawing.Point(395, 515);
            this.btn_Log_in.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Log_in.Name = "btn_Log_in";
            this.btn_Log_in.Size = new System.Drawing.Size(107, 30);
            this.btn_Log_in.TabIndex = 8;
            this.btn_Log_in.UseVisualStyleBackColor = false;
            // 
            // timer_login
            // 
            this.timer_login.Interval = 1000;
            this.timer_login.Tick += new System.EventHandler(this.timer_login_Tick);
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.BackColor = System.Drawing.Color.Transparent;
            this.lbl_timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_timer.ForeColor = System.Drawing.Color.White;
            this.lbl_timer.Location = new System.Drawing.Point(657, 361);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(185, 32);
            this.lbl_timer.TabIndex = 9;
            this.lbl_timer.Text = "Timer Count: ";
            this.lbl_timer.Click += new System.EventHandler(this.lbl_timer_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BNLife.Properties.Resources.HomePic;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(898, 601);
            this.Controls.Add(this.lbl_timer);
            this.Controls.Add(this.btn_Log_in);
            this.Controls.Add(this.btn_signup);
            this.Controls.Add(this.PicBox_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Activated += new System.EventHandler(this.Home_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_Login)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicBox_Login;
        private System.Windows.Forms.Button btn_signup;
        private System.Windows.Forms.Button btn_Log_in;
        private System.Windows.Forms.Timer timer_login;
        private System.Windows.Forms.Label lbl_timer;
    }
}

