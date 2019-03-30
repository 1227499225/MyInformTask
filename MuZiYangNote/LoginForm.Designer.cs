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
            MuZiYangNote.UserControls.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle9 = new MuZiYangNote.UserControls.TTextBoxBorderRenderStyle();
            MuZiYangNote.UserControls.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle10 = new MuZiYangNote.UserControls.TTextBoxBorderRenderStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.laForgetThePassword = new System.Windows.Forms.Label();
            this.chbAutoLogon = new System.Windows.Forms.CheckBox();
            this.chbRememberThePassword = new System.Windows.Forms.CheckBox();
            this.picbUserImg = new System.Windows.Forms.PictureBox();
            this.panelHead = new System.Windows.Forms.Panel();
            this.btnEXMin = new MuZiYangNote.UserControls.ButtonEX();
            this.btnEXClose = new MuZiYangNote.UserControls.ButtonEX();
            this.txtUserPwd = new MuZiYangNote.UserControls.TextBoxExS();
            this.txtUserAccount = new MuZiYangNote.UserControls.TextBoxExS();
            this.butLogin = new MuZiYangNote.UserControls.ButtonEX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbUserImg)).BeginInit();
            this.panelHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtUserPwd);
            this.panel1.Controls.Add(this.txtUserAccount);
            this.panel1.Controls.Add(this.butLogin);
            this.panel1.Controls.Add(this.laForgetThePassword);
            this.panel1.Controls.Add(this.chbAutoLogon);
            this.panel1.Controls.Add(this.chbRememberThePassword);
            this.panel1.Controls.Add(this.picbUserImg);
            this.panel1.Location = new System.Drawing.Point(1, 44);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 260);
            this.panel1.TabIndex = 2;
            // 
            // laForgetThePassword
            // 
            this.laForgetThePassword.AutoSize = true;
            this.laForgetThePassword.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.laForgetThePassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.laForgetThePassword.Location = new System.Drawing.Point(471, 175);
            this.laForgetThePassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.laForgetThePassword.Name = "laForgetThePassword";
            this.laForgetThePassword.Size = new System.Drawing.Size(67, 15);
            this.laForgetThePassword.TabIndex = 5;
            this.laForgetThePassword.Text = "忘记密码";
            this.laForgetThePassword.MouseEnter += new System.EventHandler(this.laForgetThePassword_MouseEnter);
            this.laForgetThePassword.MouseLeave += new System.EventHandler(this.laForgetThePassword_MouseLeave);
            // 
            // chbAutoLogon
            // 
            this.chbAutoLogon.AutoSize = true;
            this.chbAutoLogon.Location = new System.Drawing.Point(228, 174);
            this.chbAutoLogon.Margin = new System.Windows.Forms.Padding(4);
            this.chbAutoLogon.Name = "chbAutoLogon";
            this.chbAutoLogon.Size = new System.Drawing.Size(89, 19);
            this.chbAutoLogon.TabIndex = 3;
            this.chbAutoLogon.Text = "自动登录";
            this.chbAutoLogon.UseVisualStyleBackColor = true;
            // 
            // chbRememberThePassword
            // 
            this.chbRememberThePassword.AutoSize = true;
            this.chbRememberThePassword.Location = new System.Drawing.Point(348, 174);
            this.chbRememberThePassword.Margin = new System.Windows.Forms.Padding(4);
            this.chbRememberThePassword.Name = "chbRememberThePassword";
            this.chbRememberThePassword.Size = new System.Drawing.Size(89, 19);
            this.chbRememberThePassword.TabIndex = 4;
            this.chbRememberThePassword.Text = "记住密码";
            this.chbRememberThePassword.UseVisualStyleBackColor = true;
            // 
            // picbUserImg
            // 
            this.picbUserImg.Image = global::MuZiYangNote.Properties.Resources.main_7;
            this.picbUserImg.Location = new System.Drawing.Point(37, 78);
            this.picbUserImg.Margin = new System.Windows.Forms.Padding(4);
            this.picbUserImg.Name = "picbUserImg";
            this.picbUserImg.Size = new System.Drawing.Size(160, 138);
            this.picbUserImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbUserImg.TabIndex = 2;
            this.picbUserImg.TabStop = false;
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.panelHead.Controls.Add(this.btnEXMin);
            this.panelHead.Controls.Add(this.btnEXClose);
            this.panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHead.Location = new System.Drawing.Point(0, 0);
            this.panelHead.Margin = new System.Windows.Forms.Padding(4);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(595, 42);
            this.panelHead.TabIndex = 4;
            this.panelHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseDown);
            this.panelHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseMove);
            // 
            // btnEXMin
            // 
            this.btnEXMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXMin.BackColor = System.Drawing.Color.Transparent;
            this.btnEXMin.BackColorEnter = System.Drawing.Color.Empty;
            this.btnEXMin.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXMin.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXMin.BackColorMove = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXMin.ButtonTypes = MuZiYangNote.UserControls.ButtonEX.ButtonType.Min;
            this.btnEXMin.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXMin.ImageDefault = global::MuZiYangNote.Properties.Resources.Min;
            this.btnEXMin.ImageLeave = null;
            this.btnEXMin.ImageMove = null;
            this.btnEXMin.LabelEXImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEXMin.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXMin.Location = new System.Drawing.Point(524, 9);
            this.btnEXMin.Margin = new System.Windows.Forms.Padding(5);
            this.btnEXMin.Name = "btnEXMin";
            this.btnEXMin.Size = new System.Drawing.Size(27, 22);
            this.btnEXMin.TabIndex = 3;
            this.btnEXMin.TextColor = System.Drawing.Color.Black;
            this.btnEXMin.TextEX = "";
            this.btnEXMin.ButtonClick += new System.EventHandler(this.btnEXMin_ButtonClick);
            // 
            // btnEXClose
            // 
            this.btnEXClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXClose.BackColor = System.Drawing.Color.Transparent;
            this.btnEXClose.BackColorEnter = System.Drawing.Color.Empty;
            this.btnEXClose.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXClose.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXClose.BackColorMove = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXClose.ButtonTypes = MuZiYangNote.UserControls.ButtonEX.ButtonType.Min;
            this.btnEXClose.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXClose.ImageDefault = global::MuZiYangNote.Properties.Resources.Close;
            this.btnEXClose.ImageLeave = null;
            this.btnEXClose.ImageMove = null;
            this.btnEXClose.LabelEXImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEXClose.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXClose.Location = new System.Drawing.Point(559, 9);
            this.btnEXClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnEXClose.Name = "btnEXClose";
            this.btnEXClose.Size = new System.Drawing.Size(27, 22);
            this.btnEXClose.TabIndex = 3;
            this.btnEXClose.TextColor = System.Drawing.Color.Black;
            this.btnEXClose.TextEX = "";
            this.btnEXClose.ButtonClick += new System.EventHandler(this.btnEXClose_Click);
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.AllowReturn = false;
            tTextBoxBorderRenderStyle9.ActiveLineColor = System.Drawing.Color.LightSlateGray;
            tTextBoxBorderRenderStyle9.LineColor = System.Drawing.Color.DimGray;
            tTextBoxBorderRenderStyle9.LineWidth = 1F;
            this.txtUserPwd.BorderRenderStyle = tTextBoxBorderRenderStyle9;
            this.txtUserPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserPwd.HideCaretP = false;
            this.txtUserPwd.Location = new System.Drawing.Point(228, 127);
            this.txtUserPwd.Multiline = true;
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.PasswordChar = '*';
            this.txtUserPwd.Size = new System.Drawing.Size(313, 30);
            this.txtUserPwd.TabIndex = 10;
            this.txtUserPwd.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtUserPwd.WaterMarkFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserPwd.WaterMarkText = "请输入密码";
            this.txtUserPwd.WaterMarkTextColor = System.Drawing.Color.Gray;
            this.txtUserPwd.WordWrap = false;
            // 
            // txtUserAccount
            // 
            this.txtUserAccount.AllowReturn = false;
            tTextBoxBorderRenderStyle10.ActiveLineColor = System.Drawing.Color.LightSlateGray;
            tTextBoxBorderRenderStyle10.LineColor = System.Drawing.Color.DimGray;
            tTextBoxBorderRenderStyle10.LineWidth = 1F;
            this.txtUserAccount.BorderRenderStyle = tTextBoxBorderRenderStyle10;
            this.txtUserAccount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserAccount.HideCaretP = false;
            this.txtUserAccount.Location = new System.Drawing.Point(228, 78);
            this.txtUserAccount.Multiline = true;
            this.txtUserAccount.Name = "txtUserAccount";
            this.txtUserAccount.Size = new System.Drawing.Size(313, 30);
            this.txtUserAccount.TabIndex = 10;
            this.txtUserAccount.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtUserAccount.WaterMarkFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserAccount.WaterMarkText = "账户\\手机号";
            this.txtUserAccount.WaterMarkTextColor = System.Drawing.Color.Gray;
            this.txtUserAccount.WordWrap = false;
            // 
            // butLogin
            // 
            this.butLogin.BackColor = System.Drawing.Color.Silver;
            this.butLogin.BackColorEnter = System.Drawing.Color.DarkGray;
            this.butLogin.BackColorEX = System.Drawing.Color.Silver;
            this.butLogin.BackColorLeave = System.Drawing.Color.Silver;
            this.butLogin.BackColorMove = System.Drawing.Color.Silver;
            this.butLogin.ButtonTypes = MuZiYangNote.UserControls.ButtonEX.ButtonType.Max;
            this.butLogin.FontM = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butLogin.ImageDefault = null;
            this.butLogin.ImageLeave = null;
            this.butLogin.ImageMove = null;
            this.butLogin.LabelEXImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.butLogin.LabelEXTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.butLogin.Location = new System.Drawing.Point(228, 201);
            this.butLogin.Margin = new System.Windows.Forms.Padding(5);
            this.butLogin.Name = "butLogin";
            this.butLogin.Size = new System.Drawing.Size(313, 29);
            this.butLogin.TabIndex = 9;
            this.butLogin.TextColor = System.Drawing.Color.Black;
            this.butLogin.TextEX = "登  录";
            this.butLogin.ButtonClick += new System.EventHandler(this.butLogin_ButtonClick);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(595, 305);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbUserImg)).EndInit();
            this.panelHead.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbAutoLogon;
        private System.Windows.Forms.CheckBox chbRememberThePassword;
        private System.Windows.Forms.PictureBox picbUserImg;
        private System.Windows.Forms.Label laForgetThePassword;
        private System.Windows.Forms.Panel panelHead;
        private UserControls.ButtonEX btnEXMin;
        private UserControls.ButtonEX btnEXClose;
        private UserControls.ButtonEX butLogin;
        private UserControls.TextBoxExS txtUserAccount;
        private UserControls.TextBoxExS txtUserPwd;
    }
}