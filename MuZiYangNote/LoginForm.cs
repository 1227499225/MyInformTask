using Model;
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
    public partial class LoginForm : Form
    {
        private MdiForm _ParentForm = null;
        public LoginForm(MdiForm ParentForm)
        {
            InitializeComponent();
            this._ParentForm = ParentForm;
        }
        public bool isOpenEnabled = true;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            btnEXClose.Enabled = true;
            btnEXMin.Enabled = isOpenEnabled;
            //panelHead.Enabled = isOpenEnabled;

            txtUserAccount.Text = "superadmin";
            txtUserPwd.Text = "123456";

            ClientUserModel u = MemoryCacheHelper.GetInfo<ClientUserModel>();
            if (u != null) {
                txtUserAccount.Text = u.ClientUserName;
                txtUserPwd.Text = u.ClientUserPwd;
            }
        }


        #region  按钮事件
        //忘记密码
        private void laForgetThePassword_MouseEnter(object sender, EventArgs e)
        {
            laForgetThePassword.ForeColor = Color.Blue;
            laForgetThePassword.Font = new Font("宋体", 9,FontStyle.Underline);
        }
        //忘记密码
        private void laForgetThePassword_MouseLeave(object sender, EventArgs e)
        {
            laForgetThePassword.ForeColor = Color.Black;
            laForgetThePassword.Font = new Font("宋体", 9, FontStyle.Regular);
        }
        //关闭
        private void btnEXClose_Click(object sender, EventArgs e)
        {
            if (isOpenEnabled)
            {
                this.Dispose();
                this.Close();
            }
            else {
                LogModel log = new LogModel()
                {
                    Erlv = MessageLevel.LogMessage,
                    szStr = "取消登陆！",
                };
                this._ParentForm.ChangeLoginSet(false,ref log);
            }

        }
        private void butLogin_MouseEnter(object sender, EventArgs e)
        {
            butLogin.BackColor = Color.Gray;
        }

        private void butLogin_MouseLeave(object sender, EventArgs e)
        {
            butLogin.BackColor = Color.White;
        }
        #endregion



        #region  标题栏拖动
        private Point mPoint;
        /// <summary>
        /// 鼠标按下标题栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (isOpenEnabled)
                mPoint = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 鼠标在移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isOpenEnabled)
                    this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        //最小化按钮事件
        private void btnEXMin_ButtonClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        private void txtUserPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void butLogin_ButtonClick(object sender, EventArgs e)
        {
            Common.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
            {
                //这里写处理耗时的代码，代码处理完成则自动关闭该窗口
                for (int i = 0; i < 100; i++)
                {
                    System.Threading.Thread.Sleep(10);
                }
            }, null);
            ClientUserModel cum = new ClientUserModel()
            {
                ClientUserName = this.txtUserAccount.Text,
                ClientUserPwd = this.txtUserPwd.Text
            };
            LogModel log = new LogModel();
            SpecialHelper.IsFileValObjExist<ClientUserModel>(cum, ref log, "ClientUserNickname,Id");
            if (log.Erlv == MessageLevel.LogNormal)
            {
                if (_ParentForm != null && !(log.szStr).StrIsNull())
                {
                    _ParentForm.ChangeLoginSet(false, ref log);
                }
                else if((log.szStr).StrIsNull())
                {
                    this.txtUserAccount.BorderRenderStyle =(new UserControls.TTextBoxBorderRenderStyle() { LineColor=Color.Red});
                    this.txtUserPwd.BorderRenderStyle = (new UserControls.TTextBoxBorderRenderStyle() { LineColor = Color.Red });
                }
            }


        }
    }
}
