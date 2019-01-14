namespace MuZiYangNote
{
    partial class MdiForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAddTask = new System.Windows.Forms.Button();
            this.RtbTxt = new System.Windows.Forms.RichTextBox();
            this.btnShowType = new System.Windows.Forms.Button();
            this.gpbTaskList = new System.Windows.Forms.GroupBox();
            this.fyp01 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.laNoLogin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MdiTitle = new System.Windows.Forms.Label();
            this.btnEXMin = new MuZiYangNote.UserControls.ButtonEX();
            this.btnEXMax = new MuZiYangNote.UserControls.ButtonEX();
            this.btnEXClose = new MuZiYangNote.UserControls.ButtonEX();
            this.MStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.F1Help = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmNoteHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmDailyOperationHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmF1Help = new System.Windows.Forms.ToolStripMenuItem();
            this.个人中心ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.个人资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.昵称修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更换头像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.完善个人资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.个人简介ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登录地址ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看登录IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.禁止IP登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.账户设置toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.登录邮件提醒ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpbTaskList.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddTask
            // 
            this.btnAddTask.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAddTask.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnAddTask, "btnAddTask");
            this.btnAddTask.Name = "btnAddTask";
            this.toolTip1.SetToolTip(this.btnAddTask, resources.GetString("btnAddTask.ToolTip"));
            this.btnAddTask.UseVisualStyleBackColor = false;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // RtbTxt
            // 
            resources.ApplyResources(this.RtbTxt, "RtbTxt");
            this.RtbTxt.Name = "RtbTxt";
            this.RtbTxt.ReadOnly = true;
            this.RtbTxt.TextChanged += new System.EventHandler(this.RtbTxt_TextChanged);
            // 
            // btnShowType
            // 
            this.btnShowType.BackColor = System.Drawing.Color.Gainsboro;
            this.btnShowType.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnShowType, "btnShowType");
            this.btnShowType.Name = "btnShowType";
            this.btnShowType.UseVisualStyleBackColor = false;
            this.btnShowType.Click += new System.EventHandler(this.btnShowType_Click);
            // 
            // gpbTaskList
            // 
            resources.ApplyResources(this.gpbTaskList, "gpbTaskList");
            this.gpbTaskList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gpbTaskList.Controls.Add(this.fyp01);
            this.gpbTaskList.Name = "gpbTaskList";
            this.gpbTaskList.TabStop = false;
            // 
            // fyp01
            // 
            resources.ApplyResources(this.fyp01, "fyp01");
            this.fyp01.BackColor = System.Drawing.Color.White;
            this.fyp01.Name = "fyp01";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.laNoLogin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.MdiTitle);
            this.panel1.Controls.Add(this.btnEXMin);
            this.panel1.Controls.Add(this.btnEXMax);
            this.panel1.Controls.Add(this.btnEXClose);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.DoubleClick += new System.EventHandler(this.titleBar_DoubleClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseMove);
            // 
            // laNoLogin
            // 
            resources.ApplyResources(this.laNoLogin, "laNoLogin");
            this.laNoLogin.ForeColor = System.Drawing.Color.Gray;
            this.laNoLogin.Name = "laNoLogin";
            this.laNoLogin.Click += new System.EventHandler(this.laNoLogin_Click);
            this.laNoLogin.MouseEnter += new System.EventHandler(this.laNoLogin_MouseEnter);
            this.laNoLogin.MouseLeave += new System.EventHandler(this.laNoLogin_MouseLeave);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::MuZiYangNote.Properties.Resources.main_7;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // MdiTitle
            // 
            resources.ApplyResources(this.MdiTitle, "MdiTitle");
            this.MdiTitle.Name = "MdiTitle";
            // 
            // btnEXMin
            // 
            resources.ApplyResources(this.btnEXMin, "btnEXMin");
            this.btnEXMin.BackColor = System.Drawing.Color.Transparent;
            this.btnEXMin.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXMin.BackColorLeave = System.Drawing.Color.DimGray;
            this.btnEXMin.BackColorMove = System.Drawing.Color.DimGray;
            this.btnEXMin.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXMin.ImageDefault = global::MuZiYangNote.Properties.Resources.Min;
            this.btnEXMin.ImageLeave = null;
            this.btnEXMin.ImageMove = null;
            this.btnEXMin.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXMin.Name = "btnEXMin";
            this.btnEXMin.TextColor = System.Drawing.Color.Black;
            this.btnEXMin.TextEX = "";
            this.btnEXMin.ButtonClick += new System.EventHandler(this.btnEXMin_ButtonClick);
            // 
            // btnEXMax
            // 
            resources.ApplyResources(this.btnEXMax, "btnEXMax");
            this.btnEXMax.BackColor = System.Drawing.Color.DimGray;
            this.btnEXMax.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXMax.BackColorLeave = System.Drawing.Color.DimGray;
            this.btnEXMax.BackColorMove = System.Drawing.Color.DimGray;
            this.btnEXMax.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXMax.ImageDefault = global::MuZiYangNote.Properties.Resources.Max;
            this.btnEXMax.ImageLeave = null;
            this.btnEXMax.ImageMove = null;
            this.btnEXMax.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXMax.Name = "btnEXMax";
            this.btnEXMax.TextColor = System.Drawing.Color.Black;
            this.btnEXMax.TextEX = "";
            this.btnEXMax.ButtonClick += new System.EventHandler(this.btnEXMax_ButtonClick);
            // 
            // btnEXClose
            // 
            resources.ApplyResources(this.btnEXClose, "btnEXClose");
            this.btnEXClose.BackColor = System.Drawing.Color.DimGray;
            this.btnEXClose.BackColorEX = System.Drawing.Color.Transparent;
            this.btnEXClose.BackColorLeave = System.Drawing.Color.DimGray;
            this.btnEXClose.BackColorMove = System.Drawing.Color.DimGray;
            this.btnEXClose.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXClose.ImageDefault = global::MuZiYangNote.Properties.Resources.Close;
            this.btnEXClose.ImageLeave = null;
            this.btnEXClose.ImageMove = null;
            this.btnEXClose.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXClose.Name = "btnEXClose";
            this.btnEXClose.TextColor = System.Drawing.Color.Black;
            this.btnEXClose.TextEX = "";
            this.btnEXClose.ButtonClick += new System.EventHandler(this.btnEXClose_ButtonClick);
            // 
            // MStrip
            // 
            this.MStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.MStrip, "MStrip");
            this.MStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.F1Help,
            this.个人中心ToolStripMenuItem});
            this.MStrip.Name = "MStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            resources.ApplyResources(this.ToolStripMenuItem, "ToolStripMenuItem");
            this.ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // F1Help
            // 
            this.F1Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmNoteHistory,
            this.TsmDailyOperationHistory,
            this.TsmF1Help});
            this.F1Help.Name = "F1Help";
            resources.ApplyResources(this.F1Help, "F1Help");
            // 
            // TsmNoteHistory
            // 
            this.TsmNoteHistory.Name = "TsmNoteHistory";
            resources.ApplyResources(this.TsmNoteHistory, "TsmNoteHistory");
            // 
            // TsmDailyOperationHistory
            // 
            this.TsmDailyOperationHistory.Name = "TsmDailyOperationHistory";
            resources.ApplyResources(this.TsmDailyOperationHistory, "TsmDailyOperationHistory");
            // 
            // TsmF1Help
            // 
            this.TsmF1Help.Name = "TsmF1Help";
            resources.ApplyResources(this.TsmF1Help, "TsmF1Help");
            this.TsmF1Help.Click += new System.EventHandler(this.F1Help_Click);
            // 
            // 个人中心ToolStripMenuItem
            // 
            this.个人中心ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.个人资料ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.登录地址ToolStripMenuItem,
            this.账户设置toolStripMenuItem4});
            this.个人中心ToolStripMenuItem.Name = "个人中心ToolStripMenuItem";
            resources.ApplyResources(this.个人中心ToolStripMenuItem, "个人中心ToolStripMenuItem");
            // 
            // 个人资料ToolStripMenuItem
            // 
            this.个人资料ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.昵称修改ToolStripMenuItem,
            this.更换头像ToolStripMenuItem,
            this.完善个人资料ToolStripMenuItem,
            this.个人简介ToolStripMenuItem});
            this.个人资料ToolStripMenuItem.Name = "个人资料ToolStripMenuItem";
            resources.ApplyResources(this.个人资料ToolStripMenuItem, "个人资料ToolStripMenuItem");
            // 
            // 昵称修改ToolStripMenuItem
            // 
            this.昵称修改ToolStripMenuItem.Name = "昵称修改ToolStripMenuItem";
            resources.ApplyResources(this.昵称修改ToolStripMenuItem, "昵称修改ToolStripMenuItem");
            // 
            // 更换头像ToolStripMenuItem
            // 
            this.更换头像ToolStripMenuItem.Name = "更换头像ToolStripMenuItem";
            resources.ApplyResources(this.更换头像ToolStripMenuItem, "更换头像ToolStripMenuItem");
            // 
            // 完善个人资料ToolStripMenuItem
            // 
            this.完善个人资料ToolStripMenuItem.Name = "完善个人资料ToolStripMenuItem";
            resources.ApplyResources(this.完善个人资料ToolStripMenuItem, "完善个人资料ToolStripMenuItem");
            // 
            // 个人简介ToolStripMenuItem
            // 
            this.个人简介ToolStripMenuItem.Name = "个人简介ToolStripMenuItem";
            resources.ApplyResources(this.个人简介ToolStripMenuItem, "个人简介ToolStripMenuItem");
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改密码ToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            resources.ApplyResources(this.修改密码ToolStripMenuItem, "修改密码ToolStripMenuItem");
            // 
            // 登录地址ToolStripMenuItem
            // 
            this.登录地址ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看登录IPToolStripMenuItem,
            this.禁止IP登录ToolStripMenuItem});
            this.登录地址ToolStripMenuItem.Name = "登录地址ToolStripMenuItem";
            resources.ApplyResources(this.登录地址ToolStripMenuItem, "登录地址ToolStripMenuItem");
            // 
            // 查看登录IPToolStripMenuItem
            // 
            this.查看登录IPToolStripMenuItem.Name = "查看登录IPToolStripMenuItem";
            resources.ApplyResources(this.查看登录IPToolStripMenuItem, "查看登录IPToolStripMenuItem");
            // 
            // 禁止IP登录ToolStripMenuItem
            // 
            this.禁止IP登录ToolStripMenuItem.Name = "禁止IP登录ToolStripMenuItem";
            resources.ApplyResources(this.禁止IP登录ToolStripMenuItem, "禁止IP登录ToolStripMenuItem");
            // 
            // 账户设置toolStripMenuItem4
            // 
            this.账户设置toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登录邮件提醒ToolStripMenuItem});
            this.账户设置toolStripMenuItem4.Name = "账户设置toolStripMenuItem4";
            resources.ApplyResources(this.账户设置toolStripMenuItem4, "账户设置toolStripMenuItem4");
            // 
            // 登录邮件提醒ToolStripMenuItem
            // 
            this.登录邮件提醒ToolStripMenuItem.CheckOnClick = true;
            this.登录邮件提醒ToolStripMenuItem.Name = "登录邮件提醒ToolStripMenuItem";
            resources.ApplyResources(this.登录邮件提醒ToolStripMenuItem, "登录邮件提醒ToolStripMenuItem");
            // 
            // MdiForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RtbTxt);
            this.Controls.Add(this.btnShowType);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.gpbTaskList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MStrip;
            this.Name = "MdiForm";
            this.Load += new System.EventHandler(this.Mdiform_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Mdiform_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Mdiform_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Mdiform_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Mdiform_MouseUp);
            this.Resize += new System.EventHandler(this.Mdiform_Resize);
            this.gpbTaskList.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MStrip.ResumeLayout(false);
            this.MStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private UserControls.ButtonEX btnEXMax;
        private UserControls.ButtonEX btnEXClose;
        private UserControls.ButtonEX btnEXMin;
        private System.Windows.Forms.GroupBox gpbTaskList;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.RichTextBox RtbTxt;
        public System.Windows.Forms.FlowLayoutPanel fyp01;
        private System.Windows.Forms.Button btnShowType;
        private System.Windows.Forms.MenuStrip MStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.Label MdiTitle;
        private System.Windows.Forms.ToolStripMenuItem F1Help;
        private System.Windows.Forms.ToolStripMenuItem TsmNoteHistory;
        private System.Windows.Forms.ToolStripMenuItem TsmDailyOperationHistory;
        private System.Windows.Forms.ToolStripMenuItem TsmF1Help;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem 个人中心ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 个人资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 昵称修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更换头像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 完善个人资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 个人简介ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登录地址ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看登录IPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 禁止IP登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 账户设置toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem 登录邮件提醒ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label laNoLogin;
    }
}

