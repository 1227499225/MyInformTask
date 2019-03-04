namespace MuZiYangNote
{
    partial class Form1
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
            this.kEditor1 = new KSharpEditor.KEditor();
            this.SuspendLayout();
            // 
            // kEditor1
            // 
            this.kEditor1.Html = "<p>&nbsp;</p>";
            this.kEditor1.KEditorEventListener = null;
            this.kEditor1.Location = new System.Drawing.Point(3, -1);
            this.kEditor1.Name = "kEditor1";
            this.kEditor1.Size = new System.Drawing.Size(707, 328);
            this.kEditor1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 331);
            this.Controls.Add(this.kEditor1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private KSharpEditor.KEditor kEditor1;

        #endregion

        //private KSharpEditor.KEditor kEditor1;
    }
}