namespace MuZiYangNote.UserControls
{
    partial class ButtonEX
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
            this.labelEX = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelEX
            // 
            this.labelEX.BackColor = System.Drawing.Color.Transparent;
            this.labelEX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEX.Location = new System.Drawing.Point(0, 0);
            this.labelEX.Name = "labelEX";
            this.labelEX.Size = new System.Drawing.Size(20, 20);
            this.labelEX.TabIndex = 0;
            this.labelEX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelEX.Click += new System.EventHandler(this.label_Click);
            this.labelEX.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            this.labelEX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_MouseMove);
            // 
            // ButtonEX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelEX);
            this.Name = "ButtonEX";
            this.Size = new System.Drawing.Size(20, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelEX;
    }
}
