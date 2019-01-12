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
            this.laTitle = new System.Windows.Forms.Label();
            this.laNoteContent = new System.Windows.Forms.Label();
            this.laContent = new System.Windows.Forms.Label();
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
            // StripTypeTaskDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.laContent);
            this.Controls.Add(this.laNoteContent);
            this.Controls.Add(this.laTitle);
            this.Name = "StripTypeTaskDetails";
            this.Size = new System.Drawing.Size(635, 37);
            this.Load += new System.EventHandler(this.StripTypeTaskDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.Label laNoteContent;
        private System.Windows.Forms.Label laContent;
    }
}
