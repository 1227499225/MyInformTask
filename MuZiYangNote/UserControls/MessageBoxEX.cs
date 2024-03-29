﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Model;
using MuZiYangNote.RewriteControlClass;

namespace MuZiYangNote.UserControls
{
    public partial class MessageBoxEX : Form
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern int ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0XA1;   //.定义鼠標左鍵按下
        private const int HTCAPTION = 2;



        private string _titleText = "提示";

        public string TitleText
        {
            get { return _titleText; }
            set { _titleText = value; }
        }


        private string _contentText = "暂无信息!";

        public string ContentText
        {
            get { return _contentText; }
            set { _contentText = value; }
        }

        public DataTable Dt
        {
            get { return _dt; }
            set { _dt = value; }
        }

        public Types DataTableType { get; set; } = Types._null;

        private DataTable _dt = new DataTable();

        public MessageBoxEX(string text)
        {
            this._contentText = text;
            InitializeComponent();
        }
        public MessageBoxEX(string title, string text)
        {
            this.TitleText = title;
            this._contentText = text;
            InitializeComponent();
        }
        public MessageBoxEX(string title, DataTable dt,Types _dataTableType)
        {
            this.TitleText = title;
            Dt = dt;//(new MyObject.SendEmailBI()).GetUser("朱");
            DataTableType = _dataTableType;
            InitializeComponent();
        }
        //public MessageBoxEX()
        //{
        //    InitializeComponent();
        //}
        #region event
        /// <summary>
        /// 窗体load的时候讲文本赋值给消息框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageBoxEX_Load(object sender, EventArgs e)
        {
            if (this._contentText.Trim() != "")
            {
                this.lblTitalContent.Text = this._titleText;
                this.lblMessage.Text = this._contentText;
                this.dataGridView1.Visible = false;
                this.lblMessage.Visible = true;
            }
            if (Dt != null)
            {
                if (Dt.Rows.Count > 0)
                {
                    this.lblMessage.Visible = false;
                    this.dataGridView1.Visible = true;
                    this.dataGridView1.DefaultCellStyle.BackColor = Color.Gray;
                    this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//设置为整行被选中
                    this.dataGridView1.ReadOnly = true;
                    //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    this.dataGridView1.DataSource = Dt;
                    if (DataTableType == Types._a)
                    {
                        this.dataGridView1.AutoGenerateColumns = false;
                        TextAndImageColumn ColumnRoleName = new TextAndImageColumn();
                        ColumnRoleName.DataPropertyName = "Power";
                        ColumnRoleName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        ColumnRoleName.Name = "Power";
                        ColumnRoleName.HeaderText = "权限";
                        ColumnRoleName.Width = 100;
                        this.dataGridView1.Columns.Add(ColumnRoleName);
                    }
                }
            }
        }

        /// <summary>
        ///  为DataGridView控件设置双缓冲
        /// </summary>
        /// <param name="control">要设置双缓冲的DataGridView控件</param>
        /// <param name="isDoubleBuffered"> 是否使用双缓冲</param>
        public void SetDoubleBufferedForDataGridView(ref DataGridView control, bool isDoubleBuffered)
        {
            //获取控件的Type
            Type dgvType = control.GetType();

            //通过Type获取控件的指定属性

            //BindingFlags.Instance                    指定实例成员将包括在搜索中
            //BindingFlags.NonPublic                 指定非公共成员将包括在搜索中
            System.Reflection.PropertyInfo properInfo = dgvType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            //为控件的属性设置值
            properInfo.SetValue(control, isDoubleBuffered, null);
        }
        /// <summary>
        /// 鼠标按下标题栏移动窗体//lblTitalContent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            //为当前的应用程序释放鼠标捕获
            ReleaseCapture();
            //发送消息﹐让系統误以为在标题栏上按下鼠标
            SendMessage((int)this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public PublicHelper.ConstantMessage ty = new PublicHelper.ConstantMessage();
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (Dt != null)
            {
                if (Dt.Rows.Count > 0)
                {
                    string[] str = new string[dataGridView1.Rows.Count];
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Selected == true)
                        {
                            string id = dataGridView1.Rows[i].Cells["Id"].Value.ToString();
                            DataTable DataTable0 = (dataGridView1.DataSource as DataTable);
                            Dt = DataTable0.Clone();
                            DataRow[] dr = DataTable0.Select("Id='" + id + "'");
                            foreach (DataRow item in dr)
                            {
                                DataRow drs = Dt.NewRow();
                                drs = item;
                                Dt.Rows.Add(drs.ItemArray);
                            }
                            //dataGridView1.SelectedRows[0].Cells[1].Value//当前选中行
                        }
                    }
                }
            }
            //开发者模式
            if (this._contentText == "Code")
            {
                ty.Str = this.txtSuperAdminCode.Text;
            }

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }
        #region 外部调用展示数据
        //用于展示
        public static void _Show(string text)
        {
            MessageBoxEX msgbox = new MessageBoxEX(text);
            msgbox.Show();
        }
        /// <summary>
        /// 对话框自定义内容
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Show(string text)
        {
            MessageBoxEX msgbox = new MessageBoxEX(text);
            return msgbox.ShowDialog();
        }

        /// <summary>
        /// 自定义标题及内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Show(string title, string text)
        {
            MessageBoxEX msgbox = new MessageBoxEX(title, text);
            return msgbox.ShowDialog();
        }
        /// <summary>
        /// 自定义表格及展示Table表格
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DialogResult Show(string title, DataTable dt,Types _ty)
        {
            DataTable dts = new DataTable();
            MessageBoxEX msgbox = new MessageBoxEX(title, dt, _ty);
            return msgbox.ShowDialog();
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 加载完后第一次显示执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageBoxEX_Shown(object sender, EventArgs e)
        {
            if (_contentText == "Code")
            {
                plSuperAdmincode.BringToFront();//设置处于最顶层
                txtSuperAdminCode.Focus(); ;//赋予焦点
            }
            else
            {
                plSuperAdmincode.Visible = false;
            }
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageBoxEX_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.O)
            {  //确定

                btnOK_Click(sender, e);
            }
            else
            if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.N)
            { //取消
                btnOK_Click(sender, e);
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex > -1)
            {
                int intGrade01 = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells["_ep"].Value);
                int intGrade02 = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells["_qp"].Value);
                if (intGrade01 == 0 && intGrade02 == 0)
                {//都不加密
                    //dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
                if (intGrade01 == 1 && intGrade02 == 1)
                {//都加密
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
                if ((intGrade01 == 0 && intGrade02 == 1) || (intGrade01 == 1 && intGrade02 == 0))
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Black;

                    //Image.FromFile
                    TextAndImageCell cell = dataGridView1.Rows[e.RowIndex].Cells["Power"] as TextAndImageCell;
                    //cell.Image = GetImage(@"H:\小样儿书签\MyInformTask最新版本\MuZiYangNote\Resources\1526998417(1).jpg");
                }

            }
        }
        public System.Drawing.Image GetImage(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);

            fs.Close();

            return result;

        }
    }
}

