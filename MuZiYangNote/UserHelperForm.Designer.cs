namespace MuZiYangNote
{
    partial class UserHelperForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.laUserHelpeTitle = new System.Windows.Forms.Label();
            this.btnEXMin = new MuZiYangNote.UserControls.ButtonEX();
            this.btnEXMax = new MuZiYangNote.UserControls.ButtonEX();
            this.btnEXClose = new MuZiYangNote.UserControls.ButtonEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.kEditor1 = new KSharpEditor.KEditor();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.panel1.Controls.Add(this.laUserHelpeTitle);
            this.panel1.Controls.Add(this.btnEXMin);
            this.panel1.Controls.Add(this.btnEXMax);
            this.panel1.Controls.Add(this.btnEXClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 36);
            this.panel1.TabIndex = 4;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // laUserHelpeTitle
            // 
            this.laUserHelpeTitle.AutoSize = true;
            this.laUserHelpeTitle.Location = new System.Drawing.Point(3, 7);
            this.laUserHelpeTitle.Name = "laUserHelpeTitle";
            this.laUserHelpeTitle.Size = new System.Drawing.Size(47, 12);
            this.laUserHelpeTitle.TabIndex = 5;
            this.laUserHelpeTitle.Text = "F1 帮助";
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
            this.btnEXMin.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXMin.Location = new System.Drawing.Point(582, 7);
            this.btnEXMin.Name = "btnEXMin";
            this.btnEXMin.Size = new System.Drawing.Size(20, 20);
            this.btnEXMin.TabIndex = 3;
            this.btnEXMin.TextColor = System.Drawing.Color.Black;
            this.btnEXMin.TextEX = "";
            this.btnEXMin.ButtonClick += new System.EventHandler(this.btnEXMin_ButtonClick);
            // 
            // btnEXMax
            // 
            this.btnEXMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXMax.BackColorEnter = System.Drawing.Color.Empty;
            this.btnEXMax.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXMax.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXMax.BackColorMove = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXMax.ButtonTypes = MuZiYangNote.UserControls.ButtonEX.ButtonType.Min;
            this.btnEXMax.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXMax.ImageDefault = global::MuZiYangNote.Properties.Resources.Max;
            this.btnEXMax.ImageLeave = null;
            this.btnEXMax.ImageMove = null;
            this.btnEXMax.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXMax.Location = new System.Drawing.Point(609, 8);
            this.btnEXMax.Name = "btnEXMax";
            this.btnEXMax.Size = new System.Drawing.Size(20, 20);
            this.btnEXMax.TabIndex = 3;
            this.btnEXMax.TextColor = System.Drawing.Color.Black;
            this.btnEXMax.TextEX = "";
            this.btnEXMax.ButtonClick += new System.EventHandler(this.btnEXMax_ButtonClick);
            // 
            // btnEXClose
            // 
            this.btnEXClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXClose.BackColorEnter = System.Drawing.Color.Empty;
            this.btnEXClose.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXClose.BackColorLeave = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXClose.BackColorMove = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.btnEXClose.ButtonTypes = MuZiYangNote.UserControls.ButtonEX.ButtonType.Min;
            this.btnEXClose.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXClose.ImageDefault = global::MuZiYangNote.Properties.Resources.Close;
            this.btnEXClose.ImageLeave = null;
            this.btnEXClose.ImageMove = null;
            this.btnEXClose.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXClose.Location = new System.Drawing.Point(641, 7);
            this.btnEXClose.Name = "btnEXClose";
            this.btnEXClose.Size = new System.Drawing.Size(20, 20);
            this.btnEXClose.TabIndex = 3;
            this.btnEXClose.TextColor = System.Drawing.Color.Black;
            this.btnEXClose.TextEX = "";
            this.btnEXClose.ButtonClick += new System.EventHandler(this.btnEXClose_ButtonClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(668, 302);
            this.panel2.TabIndex = 5;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(668, 302);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(660, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "快捷操作简介";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(654, 270);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("C:\\Users\\wb-xyang2\\Desktop\\MyInformTask\\MuZiYangNote\\Files\\SystemFile\\SystemPages" +
        "\\FmUserHelper.html", System.UriKind.Absolute);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.kEditor1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(660, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "其他";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // kEditor1
            // 
            this.kEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kEditor1.Html = "<p>&nbsp;</p>";
            this.kEditor1.KEditorEventListener = null;
            this.kEditor1.Location = new System.Drawing.Point(3, 3);
            this.kEditor1.Name = "kEditor1";
            this.kEditor1.Size = new System.Drawing.Size(654, 270);
            this.kEditor1.TabIndex = 0;
            this.kEditor1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kEditor1_KeyDown);
            // 
            // UserHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 338);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UserHelperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帮助";
            this.Load += new System.EventHandler(this.FmUserHelper_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UserControls.ButtonEX btnEXMin;
        private UserControls.ButtonEX btnEXMax;
        private UserControls.ButtonEX btnEXClose;
        private System.Windows.Forms.Label laUserHelpeTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private KSharpEditor.KEditor kEditor1;
    }
}