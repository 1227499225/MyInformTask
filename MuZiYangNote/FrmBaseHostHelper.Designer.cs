namespace MuZiYangNote
{
    partial class FrmBaseHostHelper
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
            this.LaBaseTitle = new System.Windows.Forms.Label();
            this.btnEXMin = new MuZiYangNote.UserControls.ButtonEX();
            this.btnEXMax = new MuZiYangNote.UserControls.ButtonEX();
            this.btnEXClose = new MuZiYangNote.UserControls.ButtonEX();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(116)))), ((int)(((byte)(151)))));
            this.panel1.Controls.Add(this.LaBaseTitle);
            this.panel1.Controls.Add(this.btnEXMin);
            this.panel1.Controls.Add(this.btnEXMax);
            this.panel1.Controls.Add(this.btnEXClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(907, 45);
            this.panel1.TabIndex = 5;
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // LaBaseTitle
            // 
            this.LaBaseTitle.AutoSize = true;
            this.LaBaseTitle.Location = new System.Drawing.Point(4, 9);
            this.LaBaseTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LaBaseTitle.Name = "LaBaseTitle";
            this.LaBaseTitle.Size = new System.Drawing.Size(97, 15);
            this.LaBaseTitle.TabIndex = 5;
            this.LaBaseTitle.Text = "需自定义标题";
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
            this.btnEXMin.Location = new System.Drawing.Point(792, 9);
            this.btnEXMin.Margin = new System.Windows.Forms.Padding(5);
            this.btnEXMin.Name = "btnEXMin";
            this.btnEXMin.Size = new System.Drawing.Size(27, 25);
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
            this.btnEXMax.LabelEXImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEXMax.LabelEXTextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEXMax.Location = new System.Drawing.Point(828, 10);
            this.btnEXMax.Margin = new System.Windows.Forms.Padding(5);
            this.btnEXMax.Name = "btnEXMax";
            this.btnEXMax.Size = new System.Drawing.Size(27, 25);
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
            this.btnEXClose.LabelEXImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEXClose.LabelEXTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEXClose.Location = new System.Drawing.Point(871, 9);
            this.btnEXClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnEXClose.Name = "btnEXClose";
            this.btnEXClose.Size = new System.Drawing.Size(27, 25);
            this.btnEXClose.TabIndex = 3;
            this.btnEXClose.TextColor = System.Drawing.Color.Black;
            this.btnEXClose.TextEX = "";
            this.btnEXClose.ButtonClick += new System.EventHandler(this.btnEXClose_ButtonClick);
            // 
            // FrmBaseHostHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(907, 497);
            this.Controls.Add(this.panel1);
            this.Name = "FrmBaseHostHelper";
            this.Text = "FrmBaseHostHelper";
            this.Load += new System.EventHandler(this.FrmBaseHostHelper_Load);
            this.Resize += new System.EventHandler(this.FrmBaseHostHelper_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UserControls.ButtonEX btnEXMin;
        private UserControls.ButtonEX btnEXMax;
        private UserControls.ButtonEX btnEXClose;
        protected System.Windows.Forms.Label LaBaseTitle;
    }
}