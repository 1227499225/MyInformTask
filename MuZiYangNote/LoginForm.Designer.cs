namespace MuZiYangNote
{
    partial class LoginForm
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
            this.butLogin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.laForgetThePassword = new System.Windows.Forms.Label();
            this.chbAutoLogon = new System.Windows.Forms.CheckBox();
            this.chbRememberThePassword = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelHead = new System.Windows.Forms.Panel();
            this.btnEXMin = new MuZiYangNote.UserControls.ButtonEX();
            this.btnEXClose = new MuZiYangNote.UserControls.ButtonEX();
            this.txtUserPwd = new MuZiYangNote.UserControls.TextBoxEX();
            this.txtUserAccount = new MuZiYangNote.UserControls.TextBoxEX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // butLogin
            // 
            this.butLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.butLogin.BackColor = System.Drawing.Color.White;
            this.butLogin.FlatAppearance.BorderSize = 0;
            this.butLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butLogin.Location = new System.Drawing.Point(171, 161);
            this.butLogin.Name = "butLogin";
            this.butLogin.Size = new System.Drawing.Size(235, 23);
            this.butLogin.TabIndex = 1;
            this.butLogin.Text = "登  录";
            this.butLogin.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel1.Controls.Add(this.laForgetThePassword);
            this.panel1.Controls.Add(this.chbAutoLogon);
            this.panel1.Controls.Add(this.chbRememberThePassword);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txtUserPwd);
            this.panel1.Controls.Add(this.butLogin);
            this.panel1.Controls.Add(this.txtUserAccount);
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 205);
            this.panel1.TabIndex = 2;
            // 
            // laForgetThePassword
            // 
            this.laForgetThePassword.AutoSize = true;
            this.laForgetThePassword.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.laForgetThePassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.laForgetThePassword.Location = new System.Drawing.Point(353, 140);
            this.laForgetThePassword.Name = "laForgetThePassword";
            this.laForgetThePassword.Size = new System.Drawing.Size(53, 12);
            this.laForgetThePassword.TabIndex = 4;
            this.laForgetThePassword.Text = "忘记密码";
            this.laForgetThePassword.MouseEnter += new System.EventHandler(this.laForgetThePassword_MouseEnter);
            this.laForgetThePassword.MouseLeave += new System.EventHandler(this.laForgetThePassword_MouseLeave);
            // 
            // chbAutoLogon
            // 
            this.chbAutoLogon.AutoSize = true;
            this.chbAutoLogon.Location = new System.Drawing.Point(171, 139);
            this.chbAutoLogon.Name = "chbAutoLogon";
            this.chbAutoLogon.Size = new System.Drawing.Size(72, 16);
            this.chbAutoLogon.TabIndex = 3;
            this.chbAutoLogon.Text = "自动登录";
            this.chbAutoLogon.UseVisualStyleBackColor = true;
            // 
            // chbRememberThePassword
            // 
            this.chbRememberThePassword.AutoSize = true;
            this.chbRememberThePassword.Location = new System.Drawing.Point(261, 139);
            this.chbRememberThePassword.Name = "chbRememberThePassword";
            this.chbRememberThePassword.Size = new System.Drawing.Size(72, 16);
            this.chbRememberThePassword.TabIndex = 3;
            this.chbRememberThePassword.Text = "记住密码";
            this.chbRememberThePassword.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MuZiYangNote.Properties.Resources.main_7;
            this.pictureBox1.Location = new System.Drawing.Point(28, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 110);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.panelHead.Controls.Add(this.btnEXMin);
            this.panelHead.Controls.Add(this.btnEXClose);
            this.panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHead.Location = new System.Drawing.Point(0, 0);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(446, 34);
            this.panelHead.TabIndex = 4;
            this.panelHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseDown);
            this.panelHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseMove);
            // 
            // btnEXMin
            // 
            this.btnEXMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXMin.BackColor = System.Drawing.Color.Transparent;
            this.btnEXMin.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXMin.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXMin.BackColorMove = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXMin.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXMin.ImageDefault = global::MuZiYangNote.Properties.Resources.Min;
            this.btnEXMin.ImageLeave = null;
            this.btnEXMin.ImageMove = null;
            this.btnEXMin.Location = new System.Drawing.Point(393, 7);
            this.btnEXMin.Name = "btnEXMin";
            this.btnEXMin.Size = new System.Drawing.Size(20, 18);
            this.btnEXMin.TabIndex = 3;
            this.btnEXMin.TextColor = System.Drawing.Color.Black;
            this.btnEXMin.TextEX = "";
            this.btnEXMin.ButtonClick += new System.EventHandler(this.btnEXMin_ButtonClick);
            // 
            // btnEXClose
            // 
            this.btnEXClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXClose.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXClose.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXClose.BackColorMove = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXClose.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXClose.ImageDefault = global::MuZiYangNote.Properties.Resources.Close;
            this.btnEXClose.ImageLeave = null;
            this.btnEXClose.ImageMove = null;
            this.btnEXClose.Location = new System.Drawing.Point(419, 7);
            this.btnEXClose.Name = "btnEXClose";
            this.btnEXClose.Size = new System.Drawing.Size(20, 18);
            this.btnEXClose.TabIndex = 3;
            this.btnEXClose.TextColor = System.Drawing.Color.Black;
            this.btnEXClose.TextEX = "";
            this.btnEXClose.ButtonClick += new System.EventHandler(this.btnEXClose_Click);
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.BorderColor = System.Drawing.Color.Empty;
            this.txtUserPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserPwd.BorderWeight = -1;
            this.txtUserPwd.ControlTypeText = MuZiYangNote.UserControls.TextBoxEX.TextBoxType.String;
            this.txtUserPwd.Location = new System.Drawing.Point(171, 99);
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.PasswordChar = '*';
            this.txtUserPwd.Size = new System.Drawing.Size(235, 21);
            this.txtUserPwd.TabIndex = 0;
            this.txtUserPwd.TextBoxHeight = 30;
            this.txtUserPwd.WaterMarkFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserPwd.WaterMarkText = "请输入密码";
            this.txtUserPwd.WaterMarkTextColor = System.Drawing.Color.Gray;
            this.txtUserPwd.TextChanged += new System.EventHandler(this.txtUserPwd_TextChanged);
            // 
            // txtUserAccount
            // 
            this.txtUserAccount.BorderColor = System.Drawing.Color.Empty;
            this.txtUserAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserAccount.BorderWeight = -1;
            this.txtUserAccount.ControlTypeText = MuZiYangNote.UserControls.TextBoxEX.TextBoxType.String;
            this.txtUserAccount.Location = new System.Drawing.Point(171, 61);
            this.txtUserAccount.Name = "txtUserAccount";
            this.txtUserAccount.Size = new System.Drawing.Size(235, 21);
            this.txtUserAccount.TabIndex = 0;
            this.txtUserAccount.TextBoxHeight = 30;
            this.txtUserAccount.WaterMarkFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserAccount.WaterMarkText = "账户\\手机号";
            this.txtUserAccount.WaterMarkTextColor = System.Drawing.Color.Gray;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 244);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelHead.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.TextBoxEX txtUserAccount;
        private UserControls.TextBoxEX txtUserPwd;
        private System.Windows.Forms.Button butLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbAutoLogon;
        private System.Windows.Forms.CheckBox chbRememberThePassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label laForgetThePassword;
        private System.Windows.Forms.Panel panelHead;
        private UserControls.ButtonEX btnEXMin;
        private UserControls.ButtonEX btnEXClose;
    }
}