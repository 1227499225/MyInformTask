using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PublicHelper;
using Model;

namespace MuZiYangNote.UserControls
{
    /// <summary>
    /// 条形任务
    /// </summary>
    public partial class StripTypeTaskDetails : BaseUserControl
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


        #region 事件
        //加载
        private void StripTypeTaskDetails_Load(object sender, EventArgs e)
        {
            this.laTitle.Text = this.title;
            this.ID = this.id;
            this.laNoteContent.Text = this.noteContent;
             
        }

        //右键显示
        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    base.OnMouseUp(e);
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        contextMenuStrip1.Show(this, e.Location);
        //    }
        //}

        //右键操作关闭显示
        private void ToolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            string _v = MultiLanguageSetting.SundryLanguage("DeleteModule", "09");//多语言
            OnDataChange(new BaseEv.DataChangeEventArgs(_v.Fill(this.ID, this.title), MessageLevel.LogWarning));
            this.Dispose();
        }
        #endregion


    }
}
