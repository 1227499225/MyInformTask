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
                this._ParentForm.ChangeLoginSet(false);
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

            if (_ParentForm != null)
            {
                _ParentForm.ChangeLoginSet(false,txtUserAccount.Text);
            }
        }
    }
}
