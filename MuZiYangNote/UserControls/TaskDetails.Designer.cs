namespace MuZiYangNote.UserControls
{
    partial class TaskDetails
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.laTitle = new System.Windows.Forms.Label();
            this.txtChangTitle = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new MuZiYangNote.UserControls.ButtonEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNoteContent = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.AutoSize = true;
            this.laTitle.Location = new System.Drawing.Point(6, 9);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(29, 12);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "标题";
            this.laTitle.DoubleClick += new System.EventHandler(this.laTitle_DoubleClick);
            // 
            // txtChangTitle
            // 
            this.txtChangTitle.Location = new System.Drawing.Point(3, 2);
            this.txtChangTitle.Name = "txtChangTitle";
            this.txtChangTitle.Size = new System.Drawing.Size(121, 21);
            this.txtChangTitle.TabIndex = 3;
            this.txtChangTitle.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtChangTitle);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.laTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 26);
            this.panel1.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnClose.BackColorEX = System.Drawing.Color.Transparent;
            this.btnClose.BackColorLeave = System.Drawing.SystemColors.ButtonShadow;
            this.btnClose.BackColorMove = System.Drawing.SystemColors.ButtonShadow;
            this.btnClose.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ImageDefault = global::MuZiYangNote.Properties.Resources.Close;
            this.btnClose.ImageLeave = null;
            this.btnClose.ImageMove = null;
            this.btnClose.Location = new System.Drawing.Point(158, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 2;
            this.btnClose.TextColor = System.Drawing.Color.Black;
            this.btnClose.TextEX = "";
            this.btnClose.ButtonClick += new System.EventHandler(this.btnClose_ButtonClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtNoteContent);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(183, 177);
            this.panel2.TabIndex = 5;
            // 
            // txtNoteContent
            // 
            this.txtNoteContent.Location = new System.Drawing.Point(2, 6);
            this.txtNoteContent.Multiline = true;
            this.txtNoteContent.Name = "txtNoteContent";
            this.txtNoteContent.Size = new System.Drawing.Size(178, 168);
            this.txtNoteContent.TabIndex = 0;
            // 
            // TaskDetails
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TaskDetails";
            this.Size = new System.Drawing.Size(183, 203);
            this.Load += new System.EventHandler(this.TaskDetails_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laTitle;
        private ButtonEX btnClose;
        private System.Windows.Forms.TextBox txtChangTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNoteContent;
    }
}
