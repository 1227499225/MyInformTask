using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MuZiYangNote;
using System.Configuration;
using PublicHelper;
using Model;

namespace MuZiYangNote.UserControls
{
    /// <summary>
    /// 块状任务
    /// </summary>
    public partial class TaskDetails : UserControl
    {
        #region 自定义事件参数类型，根据需要可设定多种参数便于传递
        //声名委托
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        // 声明事件
        public event DataChangeHandler DataChange;
        // 调用事件函数

        public void OnDataChange(DataChangeEventArgs args)
        {
            if (DataChange != null)
            {
                DataChange(this, args);
            }
        }
        #endregion
        public TaskDetails()
        {
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;  
            
        }
        ~TaskDetails() { }
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
        #endregion

        private void btnIsOk_Click(object sender, EventArgs e)
        {
           
            //if (txtAddress.Text != "")
            //{
                //MessageBoxEX testDialog = new MessageBoxEX(null);
                MessageBoxEX testDialog =new MessageBoxEX("用户信息", new DataTable());
                if (testDialog.ShowDialog(this) == DialogResult.OK)
                {
                  testDialog.ty.Str = "";
                }

            //}
            //else {
            //    MessageBoxEX.Show("提示","请写收件人信息！");
            //}
            
        }

        private void TaskDetails_Load(object sender, EventArgs e)
        {
            laTitle.Text = this.title;
            string _v = MultiLanguageSetting.SundryLanguage( "AddModule","09");//多语言
            OnDataChange(new DataChangeEventArgs(_v.Fill(this.title.Substring(this.title.Length-5,5), this.title), MessageLevel.LogAppend));
            txtChangTitle.LostFocus += new EventHandler(txtChangTitle_LostFocus);

        }
        /// <summary>
        /// 失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtChangTitle_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtChangTitle.Text))
            {
                if (laTitle.Text != txtChangTitle.Text) {
                    laTitle.Text = txtChangTitle.Text;
                    string _v = MultiLanguageSetting.SundryLanguage("UpdateTitle", "09");//多语言
                    OnDataChange(new DataChangeEventArgs(_v.Fill(this.ID, this.title, laTitle.Text), MessageLevel.LogCustom, new ShowLog.customColor() { IsEnable = true, _c = Color.Blue }));
                    this.title = txtChangTitle.Text;
                }
            }
            txtChangTitle.Visible = false;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }

        private void btnClose_ButtonClick(object sender, EventArgs e)
        {
            string _v = MultiLanguageSetting.SundryLanguage("DeleteModule","09");//多语言
            OnDataChange(new DataChangeEventArgs(_v.Fill(this.ID, this.title), MessageLevel.LogWarning));
            this.Dispose();
        }

        private void laTitle_DoubleClick(object sender, EventArgs e)
        {
            txtChangTitle.Visible = true;
            txtChangTitle.Focus();
        }

        private void txtChangTitle_MouseLeave(object sender, EventArgs e)
        {

        }


    }

    public class DataChangeEventArgs : EventArgs
    {
        public string Str{ get; set; }

        public MessageLevel ty{ get; set; }

        public ShowLog.customColor _c { get; set; }

        public DataChangeEventArgs(string s1, MessageLevel s2, ShowLog.customColor c=null)
        {
            Str = s1;
            ty = s2;
            _c = c;
        }
    }
}
