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
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //txtUserAccount.VerticalContentAlignment
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
            this.Dispose();
            this.Close();
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
    }
}
