using KSharpEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote
{
    public partial class Form1 : Form, IKEditorEventListener
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kEditor1.KEditorEventListener = this;
        }
        public void OnEditorErrorOccured(Exception ex)
        {
            //hrow new NotImplementedException();
        }

        public void OnEditorLoadComplete()
        {
            //throw new NotImplementedException();
        }

        public void OnInsertImageClicked()
        {
            // throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.jpg|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string n = "<img src=\"file://" + ofd.FileName + "\" />";
                kEditor1.InsertNode(n);
            }
        }

        public void OnOpenButtonClicked()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.html|*.html";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string text = System.IO.File.ReadAllText(ofd.FileName);
                kEditor1.InsertNode(text);
            }
        }

        public void OnSaveButtonClicked()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.html|*.html";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(sfd.FileName, kEditor1.Html);
            }
        }
    }
}
