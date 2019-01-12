using Model;
using MuZiYangNote.UserControls;
using PublicHelper;
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
    public partial class UserHelperForm : FormBase
    {

        private Form _parentForm = new Form();

        public UserHelperForm(Form parentForm)
        {
            InitializeComponent();
            this._parentForm = parentForm;

        }

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

        private void FmUserHelper_Load(object sender, EventArgs e)
        {
            //后续添加中英文切换
            //this.laUserHelpeTitle.Text = MultiLanguageSetting.SundryLanguage("Prompt","08");
            this.LanguageChange();
            this.DataChange += new DataChangeHandler(DataChanged);
        }
        private void LanguageChange()
        {
            this.MinNormalSwitch();
            ManageLanguage.Instance.SetLanguage(Program._LANGUAGETYPE);//语种设置

        }


        public void DataChanged(object sender, DataChangeEventArgs args)
        {
            //更新窗体控件
            Control _cc = this._parentForm.Controls.Find("RtbTxt", true)[0];
            if (_cc.Name == "RtbTxt")
            {
                string Str = args.Str;
                Model.MessageLevel ty = args.ty;
                new PublicHelper.ShowLog(_cc as RichTextBox, ty, Str, args._c);
            }
        }

        #region 界面控制基础代码
        #region  标题栏拖动
        private Point mPoint;
        /// <summary>
        /// 鼠标按下标题栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 鼠标在移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        #endregion
        private void MinNormalSwitch()
        {
            if (this.WindowState == FormWindowState.Maximized)//如果当前状态是最大化状态 则窗体需要恢复默认大小
            {
                this.WindowState = FormWindowState.Normal;
                //
                this.btnEXMax.ImageDefault = global::MuZiYangNote.Properties.Resources.Max;
            }
        }
        #endregion
        private void btnEXClose_ButtonClick(object sender, EventArgs e)
        {
            string _WIDOWSHOW02 = MultiLanguageSetting.SundryLanguage("WidowShow02", "08");
            OnDataChange(new DataChangeEventArgs(_WIDOWSHOW02.Fill(this.Text, MultiLanguageSetting.SundryLanguage("Close", "08")), Model.MessageLevel.LogMessage, (new PublicHelper.ShowLog.customColor() { IsEnable = true, _c = Color.Red })));
            (this._parentForm as MdiForm)._lc.Remove(this);
            this.Dispose(true);
            this.Close();
        }
    }
}
