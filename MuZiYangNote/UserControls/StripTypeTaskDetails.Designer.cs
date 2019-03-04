namespace MuZiYangNote.UserControls
{
    partial class StripTypeTaskDetails
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
            this.components = new System.ComponentModel.Container();
            this.laTitle = new System.Windows.Forms.Label();
            this.laNoteContent = new System.Windows.Forms.Label();
            this.laContent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.AutoSize = true;
            this.laTitle.Location = new System.Drawing.Point(3, 11);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(53, 12);
            this.laTitle.TabIndex = 0;
            this.laTitle.Text = "标题：空";
            // 
            // laNoteContent
            // 
            this.laNoteContent.AutoSize = true;
            this.laNoteContent.Location = new System.Drawing.Point(146, 11);
            this.laNoteContent.Name = "laNoteContent";
            this.laNoteContent.Size = new System.Drawing.Size(41, 12);
            this.laNoteContent.TabIndex = 1;
            this.laNoteContent.Text = "label1";
            // 
            // laContent
            // 
            this.laContent.AutoSize = true;
            this.laContent.Location = new System.Drawing.Point(99, 11);
            this.laContent.Name = "laContent";
            this.laContent.Size = new System.Drawing.Size(41, 12);
            this.laContent.TabIndex = 2;
            this.laContent.Text = "内容：";
            // 
            // label1
            // 
            this.label1.ContextMenuStrip = this.contextMenuStrip1;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(625, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "***";
            this.toolTip1.SetToolTip(this.label1, "右键操作");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemClose,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // ToolStripMenuItemClose
            // 
            this.ToolStripMenuItemClose.Name = "ToolStripMenuItemClose";
            this.ToolStripMenuItemClose.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemClose.Text = "关闭";
            this.ToolStripMenuItemClose.Click += new System.EventHandler(this.ToolStripMenuItemClose_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // StripTypeTaskDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.laContent);
            this.Controls.Add(this.laNoteContent);
            this.Controls.Add(this.laTitle);
            this.Name = "StripTypeTaskDetails";
            this.Size = new System.Drawing.Size(635, 37);
            this.Load += new System.EventHandler(this.StripTypeTaskDetails_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.Label laNoteContent;
        private System.Windows.Forms.Label laContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemClose;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
