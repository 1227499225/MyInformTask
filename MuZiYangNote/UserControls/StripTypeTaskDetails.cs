using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote.UserControls
{
    /// <summary>
    /// 条形任务
    /// </summary>
    public partial class StripTypeTaskDetails : UserControl
    {
        public StripTypeTaskDetails()
        {
            InitializeComponent();
        }

        #region 任务配置
        private string title = string.Empty;
        [Description("用于显示文本")]
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }
        private string id = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        private string noteContent= string.Empty;
        public string NoteContent
        {
            get
            {
                return noteContent;
            }

            set
            {
                noteContent = value;
            }
        }
        #endregion

        private void StripTypeTaskDetails_Load(object sender, EventArgs e)
        {
            this.laTitle.Text = this.title;
            this.ID = this.id;
            this.laNoteContent.Text = this.noteContent;
        }
    }
}
