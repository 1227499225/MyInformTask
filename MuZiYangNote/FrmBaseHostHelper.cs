using Model;
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
    public partial class FrmBaseHostHelper : FormBase
    {
        private Point mPoint;
        public FrmBaseHostHelper()
        {
            InitializeComponent();
        }

        #region 界面
        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            this.MaxNormalSwitch();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        private void FrmBaseHostHelper_Load(object sender, EventArgs e)
        {
            Mdiform_ControlLocation(Types._a);
        }

        private void FrmBaseHostHelper_Resize(object sender, EventArgs e)
        {
            Mdiform_ControlLocation(Types._null);
        }

        private void btnEXMin_ButtonClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnEXMax_ButtonClick(object sender, EventArgs e)
        {
            this.MaxNormalSwitch();
        }

        private void btnEXClose_ButtonClick(object sender, EventArgs e)
        {
            this.Dispose(true);
            this.Close();
            //System.Environment.Exit(0);
        }

        private void MaxNormalSwitch()
        {
            if (this.WindowState == FormWindowState.Maximized)//如果当前状态是最大化状态 则窗体需要恢复默认大小
            {
                this.WindowState = FormWindowState.Normal;
                //
                this.btnEXMax.ImageDefault = global::MuZiYangNote.Properties.Resources.Max;
            }
            else
            {
                //防止遮挡任务栏
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
                //最大化图标切换
                this.btnEXMax.ImageDefault = global::MuZiYangNote.Properties.Resources.MaxNormal;
            }
            this.Invalidate();//使重绘
        }
        //控件位置
        public void Mdiform_ControlLocation(Types Tys)
        {
            if (Tys == Types._a || Tys == Types._null)
            {
                //int x = this.Width / 2 - MdiTitle.Width / 2;
                //MdiTitle.Location = new Point(x, MdiTitle.Location.Y);
                panel1.Width = this.Width;
                panel1.Location = new Point(0, 0);//顶部模块

                LaBaseTitle.Location = new Point(10, LaBaseTitle.Parent.Height/2- LaBaseTitle.Height/2);

                #region 最小化、最大化、关闭 按钮位置控制
                int BtnEXParent_X = btnEXClose.Parent.Width, BtnEXParent_H = btnEXClose.Parent.Height;
                btnEXClose.Location = new Point(BtnEXParent_X - 0 - btnEXClose.Width, BtnEXParent_H / 2 - btnEXClose.Height / 2);
                btnEXMax.Location = new Point(btnEXClose.Location.X - 0 - btnEXMax.Width, BtnEXParent_H / 2 - btnEXMax.Height / 2);
                btnEXMin.Location = new Point(btnEXMax.Location.X - 0 - btnEXMin.Width, BtnEXParent_H / 2 - btnEXMin.Height / 2);
                btnEXClose.Size = new Size(btnEXClose.Parent.Height, btnEXClose.Parent.Height);
                btnEXMax.Size = new Size(btnEXClose.Parent.Height, btnEXClose.Parent.Height);
                btnEXMin.Size = new Size(btnEXClose.Parent.Height, btnEXClose.Parent.Height);
                #endregion
            }
        }

        #endregion


    }
}
