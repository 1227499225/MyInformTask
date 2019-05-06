using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MuZiYangNote;
using System.Configuration;
using MuZiYangNote.UserControls;
using PublicHelper;
using Model;
using System.Collections;
using System.IO;

namespace MuZiYangNote
{
    public partial class MdiForm : FormBase
    {
        public MdiForm()
        {
            InitializeComponent();
        }

        ~MdiForm()
        {

        }
     
        #region 全局变量
        /// <summary>
        /// 任务展示模式(默认)
        /// </summary>
        private ShowType _showType = ShowType.Tile;
        /// <summary>
        /// 便签类型
        /// </summary>
        private NoteType _noteType = NoteType.GeneralNote;
        /// <summary>
        /// 当前打开的子窗体
        /// </summary>
        public List<Form> _lc { get; set; } = new List<Form>();
        /// <summary>
        /// 普通任务
        /// </summary>
        public List<Model.PlainNoteModel> _PlainNotes = new List<PlainNoteModel>();
        /// <summary>
        /// 本webBrowser1地或云
        /// </summary>
        public DateBaseLocation _dbl;
        /// <summary>
        /// 本地数据库名称
        /// </summary>
        public string LocationDataBaseName= "SmallSheep.DB";

        /// <summary>
        /// 程序打开时 预加载已设置默认打开模块
        /// </summary>
        private BackgroundWorker backgroundWorkerLoadingIsOpenNote;
        /// <summary>
        /// 线程间调用代理
        /// </summary>
        /// <param name="dt"></param>
        private delegate void DisplayDelegate(DataTable dt);

        #endregion

        /*
        * ============================================================
        * 函数名：Mdiform_Load
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：主界面加载
        * ============================================================
        */
        private void Mdiform_Load(object sender, EventArgs e)
        {
            Mdiform_ControlLocation(Types._a);
            //双缓冲
            fyp01.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(fyp01, true, null);

            //窗体自身支持接受拖拽来的控件
            //this.AllowDrop = true;
            //this.fyp01.AllowDrop = true;//拖进拖出相关

            //DataTable dt = (new SendEmailBI()).GetUser("朱");
            //this.dataGridView1.DataSource = dt;

            //创建backgroundWorkerLoadingIsOpenNote
             CreatWorker();

            new ShowLog(RtbTxt, MessageLevel.LogCustom, @"便签管理器已启动！", (new ShowLog.customColor() { IsEnable = true, _c = Color.Gray }));
        }

        #region
        // 界面上放置大量的控件（尤其是自定义控件）会导致在窗体加载时，速度变得缓慢；当切换页面时，也会时常产生闪烁的问题，非常影响用户体验
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {

        //        CreateParams cp = base.CreateParams;

        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
        //        return cp;

        //    }
        //}//防止闪烁
        #endregion
       
        #region
        #endregion

        #region 多线程处理
        #region BackgroundWorker的应用
        //创建
        public void CreatWorker()
        {
            backgroundWorkerLoadingIsOpenNote = new BackgroundWorker();                      //新建BackgroundWorker
            // worker = new BackgroundWorker();                      //新建BackgroundWorker
            backgroundWorkerLoadingIsOpenNote.WorkerReportsProgress = true;                  //允许报告进度
            backgroundWorkerLoadingIsOpenNote.WorkerSupportsCancellation = true;             //允许取消线程
            backgroundWorkerLoadingIsOpenNote.DoWork += worker_DoWork;                       //设置主要工作逻辑
            backgroundWorkerLoadingIsOpenNote.ProgressChanged += worker_ProgressChanged;     //进度变化的相关处理
            backgroundWorkerLoadingIsOpenNote.RunWorkerCompleted += worker_RunWorkerCompleted;  //线程完成时的处理
        }
        //启动BackgroundWorker
        public void StartWorker()
        {
            new ShowLog(RtbTxt, MessageLevel.LogAppend, "开始检查程序文件及默认配置完整性："); 
            backgroundWorkerLoadingIsOpenNote.RunWorkerAsync();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker tempWorker = sender as BackgroundWorker;
            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(200);    //避免太快，让线程暂停一会再报告进度
            //    tempWorker.ReportProgress(i);//报告进度，触发ProgressChanged事件
            //}
            //if (tempWorker.CancellationPending)  //当点击Cancel按钮时，CancellationPending被设置为true
            //{
            //    e.Cancel = true;  //此处设置Cancel=true后，就可以在RunWorkerCompleted中判断e.Cancelled是否为true
            //    break;
            //}
            #region 本地数据库文件是否完整
            object[] obj01 = SpecialHelper.CreateTableSql<Model.ClientUserModel>(new Model.ClientUserModel(), out _dbl);
            PubConstant pc = new PubConstant(obj01[0].ToString());
            if (!System.IO.File.Exists(pc.SQLiteDBpath.Replace("Data Source=", "")))
            {
                SqliteDBHelper.DeleteDB(pc.SQLiteDBpath);
                SqliteDBHelper.CreateDB(pc.SQLiteDBpath);
            }
            else
            {
                //UserControl
            }
            tempWorker.ReportProgress(0,"本地数据文件完整。");
            //创建本地用户表
            SqliteDBHelper.CreateTable(obj01[2].ToString(), obj01[0].ToString(), obj01[1].ToString());
            //创建本地普通便签表
            obj01 = SpecialHelper.CreateTableSql<Model.PlainNoteModel>(new Model.PlainNoteModel(), out _dbl);
            SqliteDBHelper.CreateTable(obj01[2].ToString(), obj01[0].ToString(), obj01[1].ToString());
            tempWorker.ReportProgress(2, "本地用户文件完整。");
            //创建本地编号记录表
            obj01 = SpecialHelper.CreateTableSql<Model.InstSerialModel>(new Model.InstSerialModel(), out _dbl);
            SqliteDBHelper.CreateTable(obj01[2].ToString(), obj01[0].ToString(), obj01[1].ToString());
            tempWorker.ReportProgress(3, "本地用户编号记录文件完整。");
            //创建本地任务记录表
            obj01 = SpecialHelper.CreateTableSql<Model.InstTaskModel>(new Model.InstTaskModel(), out _dbl);
            SqliteDBHelper.CreateTable(obj01[2].ToString(), obj01[0].ToString(), obj01[1].ToString());
            tempWorker.ReportProgress(4, "本地用户任务记录文件完整。");
            //创建本地任务记录表
            obj01 = SpecialHelper.CreateTableSql<Model.InstTaskEncryptionModel>(new Model.InstTaskEncryptionModel(), out _dbl);
            SqliteDBHelper.CreateTable(obj01[2].ToString(), obj01[0].ToString(), obj01[1].ToString());
            tempWorker.ReportProgress(5, "本地用户任务加密记录文件完整。");
            //创建本地多语言记录
            obj01 = SpecialHelper.CreateTableSql<Model.BaseLanguageModel>(new BaseLanguageModel(), out _dbl);
            SqliteDBHelper.CreateTable(obj01[2].ToString(), obj01[0].ToString(), obj01[1].ToString());
            tempWorker.ReportProgress(5, "本地用户多语言文件完整。");
            //表关系
            obj01 = SpecialHelper.CreateTableSql<Model.BaseTabelRelationshipModel>(new BaseTabelRelationshipModel(), out _dbl);
            SqliteDBHelper.CreateTable(obj01[2].ToString(), obj01[0].ToString(), obj01[1].ToString());

         
            #endregion

            MyConfig mc = new MyConfig();
            AppSettingsSection ap = (mc.ReadConfig()) as AppSettingsSection;

            tempWorker.ReportProgress(6, "开始查询是否存在常开项：");
            //查询 需要被打开的单子
            DataTable dt = SqliteDBHelper.Query_dt((SpecialHelper.SqlHelper.TaskSqlDic("V02003")).Fill(Program.ProgramUserId,"1"),this.LocationDataBaseName);
            e.Result = dt.Rows.Count;
            tempWorker.ReportProgress(7, "当前用户常开项便签数量为："+ e.Result);
            if (dt.DtisNull())
                return;
            tempWorker.ReportProgress(8, "开始读取展示常开项：");
            AddTaskModule(dt);
            
        }
        //改变进度条的值
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            new ShowLog(RtbTxt, MessageLevel.LogAppend, e.UserState.ToString());
        }        
        //线程执行完成
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int _C=0;
            int.TryParse(e.Result.ToString(), out _C);
            if (_C == 0)
                new ShowLog(RtbTxt, MessageLevel.LogCustom, "暂无默认打开项，请参考帮助文档进行配置！",(new ShowLog.customColor() { IsEnable=true,_c=Color.Gray}));
            else
                new ShowLog(RtbTxt, MessageLevel.LogAppend, "已加载完毕！");
        }
        //取消线程
        private void cancelWorker()
        {
            if (backgroundWorkerLoadingIsOpenNote.IsBusy)
                backgroundWorkerLoadingIsOpenNote.CancelAsync();
            else
                MessageBox.Show("There is no thead running now.");
        }
        #endregion
        #endregion

        #region  界面控制
        #region
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        #endregion

        #region 鼠标
        //[DllImport("user32.dll")]
        //public static extern bool ReleaseCapture();
        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        //bool beginMove = false;//初始化鼠标位置  
        //int currentXPosition;
        //int currentYPosition;
        private void Mdiform_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    beginMove = true;
            //    currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标  
            //    currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标  
            //}
        }
        /// <summary>
        /// 获取鼠标移动到的位置 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdiform_MouseMove(object sender, MouseEventArgs e)
        {
            //if (beginMove)
            //{
            //    this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x  
            //    this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标  
            //    currentXPosition = MousePosition.X;
            //    currentYPosition = MousePosition.Y;
            //}
        }
        /// <summary>
        /// 释放鼠标时的位置  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdiform_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    currentXPosition = 0; //设置初始状态  
            //    currentYPosition = 0;
            //    beginMove = false;
            //}
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #region 
        public enum FormSize
        {
            NORMAL = 0,//正常大小
            MAX = 1,//最大化
        };
        const int WM_NCHITTEST = 0x0084;
        const int HTLEFT = 10;      //左边界
        const int HTRIGHT = 11;     //右边界
        const int HTTOP = 12;       //上边界
        const int HTTOPLEFT = 13;   //左上角
        const int HTTOPRIGHT = 14;  //右上角
        const int HTBOTTOM = 15;    //下边界
        const int HTBOTTOMLEFT = 0x10;    //左下角
        const int HTBOTTOMRIGHT = 17;     //右下角

        private Point mPoint;

        /// <summary>
        /// 是否允许最大化
        /// </summary>
        private bool maxVisible = true;
        [Description("是否允许最大化")]
        public bool MaxVisible
        {
            get { return maxVisible; }
            set
            {
                maxVisible = value;
                if (!maxVisible)
                {
                    this.btnEXMax.Location = new System.Drawing.Point(this.btnEXMax.Location.X, 12);
                    this.btnEXMax.Visible = false;
                }
                else
                {
                    this.btnEXMax.Location = new System.Drawing.Point(this.btnEXMax.Location.X - 20, 12);
                    this.btnEXMax.Visible = true;
                }
            }
        }
        #region 标题
        /// <summary>
        /// 窗体标题
        /// </summary>
        private string titleText = "便签";
        [Description("窗体标题")]
        public string TitleText
        {
            get { return titleText; }
            set
            {
                titleText = value;
                MdiTitle.Text = titleText;

            }
        }
        /// <summary>
        /// 窗体标题是否显示
        /// </summary>
        private bool titleVisible = true;
        [Description("窗体标题是否显示")]
        public bool TitleVisible
        {
            get { return titleVisible; }
            set
            {
                titleVisible = value;
                MdiTitle.Visible = titleVisible;
            }
        }
        #endregion

        #region   标题栏
        /// <summary>
        /// 鼠标按下标题栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 鼠标在移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void titleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }

        private void titleBar_DoubleClick(object sender, EventArgs e)
        {
            this.MaxNormalSwitch();
        }

        #endregion
        /// <summary>
        /// 窗口默认大小
        /// FormSize.NORMAL OR FormSize.MAX
        /// </summary>
        private FormSize defaultFormSize = FormSize.NORMAL;
        [Description("窗口默认大小")]
        public FormSize DefaultFormSize
        {
            get { return defaultFormSize; }
            set
            {
                defaultFormSize = value;
                if (defaultFormSize == FormSize.MAX)
                {
                    //防止遮挡任务栏
                    this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                    this.WindowState = FormWindowState.Maximized;
                    //最大化图标切换
                    this.btnEXMax.ImageDefault = global::MuZiYangNote.Properties.Resources.MaxNormal;
                }
            }
        }

        public List<Form> Lc
        {
            get
            {
                return _lc;
            }

            set
            {
                _lc = value;
            }
        }
        #region 无边框窗体移动、放大、缩小
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion
        /// <summary>
        /// 最小化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEXMin_ButtonClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 最大化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEXMax_ButtonClick(object sender, EventArgs e)
        {
            this.MaxNormalSwitch();
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEXClose_ButtonClick(object sender, EventArgs e)
        {
            this.Dispose(true);
            this.Close();
            System.Environment.Exit(0);
        }
        /// <summary>
        /// 最大化和正常状态切换
        /// </summary>
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
                //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
                //最大化图标切换
                this.btnEXMax.ImageDefault = global::MuZiYangNote.Properties.Resources.MaxNormal;
            }
            this.Invalidate();//使重绘
        }
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

        //窗体大小
        private void Mdiform_Resize(object sender, EventArgs e)
        {
            panel2.Width = this.Width;
            gpbTaskList.Width = this.Width;
            RtbTxt.Width = this.Width;
            gpbTaskList.Height = this.Height - panel1.Height - panel2.Height - RtbTxt.Height;
            RtbTxt.Location = new Point(0,this.Height- RtbTxt.Height);

            Mdiform_ControlLocation(Types._null);
        }

        //控件位置
        public void Mdiform_ControlLocation(Types Tys)
        {
            if (Tys==Types._a||Tys==Types._null)
            {
                int x = this.Width / 2 - MdiTitle.Width / 2;
                MdiTitle.Location = new Point(x, MdiTitle.Location.Y);
                panel1.Width = this.Width;
                panel1.Location = new Point(0,0);//顶部模块

                #region 最小化、最大化、关闭 按钮位置控制
                int BtnEXParent_X = btnEXClose.Parent.Width, BtnEXParent_H = btnEXClose.Parent.Height;
                btnEXClose.Location = new Point(BtnEXParent_X -0-btnEXClose.Width, BtnEXParent_H/2- btnEXClose.Height/2);
                btnEXMax.Location = new Point(btnEXClose.Location.X-0-btnEXMax.Width, BtnEXParent_H / 2 - btnEXMax.Height / 2);
                btnEXMin.Location = new Point(btnEXMax.Location.X - 0 - btnEXMin.Width, BtnEXParent_H / 2 - btnEXMin.Height / 2);
                btnEXClose.Size = new Size(btnEXClose.Parent.Height, btnEXClose.Parent.Height);
                btnEXMax.Size = new Size(btnEXClose.Parent.Height, btnEXClose.Parent.Height);
                btnEXMin.Size = new Size(btnEXClose.Parent.Height, btnEXClose.Parent.Height);
                #endregion

                btnShowType.Location = new Point(btnShowType.Parent.Width-10- btnShowType.Width, btnShowType.Parent.Height / 2 - (btnShowType.Height-6) / 2);
                btnAddTask.Location = new Point(btnShowType.Location.X - 10 - btnAddTask.Width, btnAddTask.Parent.Height / 2 - (btnAddTask.Height-6) / 2);
            }
        }

        #endregion

        
        #region  控件平铺

        #region  更新窗体控件 日志显示
        public void DataChanged(object sender, BaseEv.DataChangeEventArgs args)
        {

            if (sender.GetType().Name == "TaskDetails")
            {
                //更新窗体控件
                foreach (Control _cc in (sender as UserControls.TaskDetails).Parent.Parent.Parent.Controls)
                {
                    if (_cc.Name == "RtbTxt")
                    {
                        string Str = args.Str;
                        MessageLevel ty = args.ty;
                        new ShowLog(_cc as RichTextBox, ty, Str, args._c);
                    }
                }
            }
            if (sender.GetType().Name == "StripTypeTaskDetails") {
                //更新窗体控件
                foreach (Control _cc in (sender as UserControls.StripTypeTaskDetails).Parent.Parent.Parent.Controls)
                {
                    if (_cc.Name == "RtbTxt")
                    {
                        string Str = args.Str;
                        MessageLevel ty = args.ty;
                        new ShowLog(_cc as RichTextBox, ty, Str, args._c);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 控件平铺计算
        /// </summary>
        /// <param name="TD"></param>
        /// <returns></returns>
        public Control[] TDProperty(UserControls.TaskDetails TD)
        {
            int gpb_w = fyp01.Width,
                gpb_h = fyp01.Height;
            //比如说尺寸是x，每一个容器占用a，而间距是b，一共放n个容器，那么就是解
            //a* n +(b(n + 1) <= x; （n为整数）。
            //对于宽和高分别解出 n1、n2，就知道横向几列、纵向几列。
            int n = MaxConNums("TD", true);
            //计算一行能放多少个
            //int c = (int)Math.Floor((double)gpb_w / (200+20));//向下取整
            //int Cc =Convert.ToInt32((c.ToString().IndexOf(".") > -1 ? c.ToString().Substring(0, c.ToString().IndexOf(".")): c.ToString()));
            //if (n >= c)//当前数量大于单行数量说明存在多行
            //{
            //    //获取存在多少行
            //    int d = (int)Math.Ceiling((double)n / c);//向上取整
            //    //int Dd = (d.ToString().IndexOf(".") > -1 ? (Convert.ToInt32(d.ToString().Substring(0, d.ToString().IndexOf("."))) + 1) : Convert.ToInt32(d));
            //    Control[] MaxTD = MaxTd(n);//获取当前最大TD控件
            //    MaxTD[MaxTD.Length] = TD;
            //    //var X = MaxTD.Location.X;
            //    //Point p = new Point()
            //    //{
            //    //    Y = d * (100 + 15)+15,
            //    //    X = (n==c?20: X + (200 + 20))//相等的时候做处理
            //    //};
            //    //TD.Location=p;
            //}
            //else {//小于说明不足多行
            //    Point p = new Point()
            //    {
            //        X = n * (20 + 200) + 20,
            //        Y = 15
            //    };
            //    TD.Location = p;
            //}
            Control[] MaxTD = MaxTd(n + 1);//获取当前最大TD控件
            MaxTD[n] = TD;
            return MaxTD;
        }

        /// <summary>
        /// 获取详情控件最大的编号
        /// </summary>
        /// <param name="cName"></param>
        /// <returns></returns>
        public int MaxConNums(string cName, bool GetCount = false)
        {
            int MaxNums = 0;
            List<int> li = new List<int>();
            foreach (Control c in this.fyp01.Controls)
            {
                if (c.Name.ToString().IndexOf(cName) > -1)
                {
                    string s = c.Name.ToString().Substring(c.Name.Length-3, 3);
                    li.Add(Convert.ToInt32(s));
                }
            }
            if (!GetCount)
            {
                if (li.Count > 0)
                    MaxNums = li.Max(x => x);
                return MaxNums + 1;
            }
            else
            {
                return li.Count;
            }

        }
        /// <summary>
        /// 获取当前编号最大控件
        /// </summary>
        /// <returns>返回控件对象</returns>
        public Control[] MaxTd(int Count)
        {
            Control[] cObj = new Control[Count];
            Control Ct = new Control();
            string strName = "TD" + (MaxConNums("TD") - 1).ToString();
            int i = 0, j = Count;
            foreach (Control c in this.fyp01.Controls)
            {
                if (c.Name.ToString() == strName)
                {
                    if (i < j - 1)
                        cObj[i] = c;
                    i++;
                }
            }
            cObj[Count - 1] = (new Control());
            return cObj;
        }
        #endregion


        #region 快捷键
        //快捷键
        private void Mdiform_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Modifiers == ((int)Keys.Alt)&& e.KeyCode == Keys.B)   //Ctrl + Alt + 数字0
            {
                if (!isLogin())
                    return;
                MessageBoxEX testDialog = new MessageBoxEX("欢迎使用开发者模式", "Code");
                if (testDialog.ShowDialog(this) == DialogResult.OK)
                {
                    PublicHelper.ConstantMessage ty = testDialog.ty;

                    switch (ty.Str)
                    {
                        case "New _W":
                            WorkForm wf = new WorkForm();
                            //wf.TopMost = true;//显示最前面
                            wf.Show();//显示  父级窗体可编辑
                            Lc.Add(wf);
                            break;
                        case "New _W View":
                            FrmViewCorrelation _fvc = new FrmViewCorrelation();
                            _fvc.Show();
                            _fvc.Activate();
                            break;
                        case "":
                            //Update Task&nbsp;This IsOpen Equals 1 End 
                            break;
                        default:break;
                    }
                    if (ty.Str.ToLower().Contains("update"))
                        {
                        if (ty.Str.ToLower().Contains("update task")) {
                            if (ty.Str.Contains("isopen equals"))
                            {

                            }
                        }
                    }
                }
            }
            else
            //快速创建便签
            if ((int)e.Modifiers == ((int)Keys.Alt)&&e.KeyCode == Keys.C)
            {
                btnAddTask_Click(sender, e);
            }
            else
            //帮助
            if (e.KeyCode == Keys.F1) {
                F1Open();
            } else if ((int)e.Modifiers == ((int)Keys.Control)&&e.KeyCode==Keys.Z) {
                MessageBoxEX testDialog = new MessageBoxEX("测试", new DataTable(),Types._null);
                //DialogResult _d = testDialog.ShowDialog(this);
            }
            else
             //关闭所有非主界面
             if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.Delete) {
                string _tiShi = "关闭子窗体提示",
                       _conText = string.Empty;
                if (Lc.Count > 0)
                {
                    foreach (Form item in Lc)
                        item.TopMost = false;
                    MessageBoxEX testDialog = new MessageBoxEX(_tiShi, "确认关闭所有子窗体？");
                    testDialog.TopMost = testDialog.TopMost == false ? true : false;
                    DialogResult _d = testDialog.ShowDialog(this);
                    if (_d == DialogResult.Cancel)
                    {
                        return;
                    }
                    foreach (Form item in Lc)
                    {
                        new ShowLog(RtbTxt, MessageLevel.LogError, "子窗体”" + item.Text + "“已关闭！");
                        item.Close();
                        item.Dispose();
                    }
                    Lc.Clear();
                    if (Lc.Count == 0)
                    {
                        _conText = "已处理完毕！";
                    }
                }
                else {
                    _conText = "暂无子窗体可关闭喔！";
                }
                MessageBoxEX.Show(_tiShi, _conText);
            }
        }

        private UserHelperForm _fuh;
        private void F1Open() {
            //if (_fuh == null || _fuh.IsDisposed)
            //{
                _fuh =  new UserHelperForm(this);
                _fuh.DataChange += new UserHelperForm.DataChangeHandler((new MdiForm()).DataChanged);
                _fuh.Show();//未打开，直接打开。
                string _WIDOWSHOW02 = MultiLanguageSetting.SundryLanguage("WidowShow02", "08");
                new ShowLog(RtbTxt, MessageLevel.LogMessage, _WIDOWSHOW02.Fill(_fuh.Text, MultiLanguageSetting.SundryLanguage("Open", "08")));
                _fuh.Activate();//已打开，获得焦点，置顶。
            //}
            //else
            //{
            //    string _WIDOWSHOW01 = MultiLanguageSetting.SundryLanguage("WidowShow01", "08");
            //    new ShowLog(RtbTxt, MessageLevel.LogMessage, _WIDOWSHOW01.Fill(_fuh.Text));
            //    _fuh.Activate();//已打开，获得焦点，置顶。
            //}

            //判断窗体是否已打开
            //    Form _f = Lc.Find(p => p.Name == _fuh.Name);
            //if (_f != null)
            //{
            //    string _PROMPT = MultiLanguageSetting.SundryLanguage("Prompt", "08");
            //    string _ISWIDOWSOPENEN = MultiLanguageSetting.SundryLanguage("IsWidowsOpen", "08");

            //    MessageBoxEX testDialog = new MessageBoxEX(_PROMPT, _ISWIDOWSOPENEN.Fill(_f.Text));
            //    DialogResult _d = testDialog.ShowDialog(this);
            //    if (_d == DialogResult.OK)
            //    {
            //        _f.TopMost = true;
            //        string _WIDOWSHOW01 = MultiLanguageSetting.SundryLanguage("WidowShow01", "08");
            //        new ShowLog(RtbTxt, MessageLevel.LogMessage, _WIDOWSHOW01.Fill(_f.Text));
            //    }
            //    return;
            //}
            ////_fuh.TopMost = true;
            //#region 窗体设置最小
            ////this.WindowState = FormWindowState.Maximized;
            ////MaxNormalSwitch();
            //#endregion
            ////_fuh.Show();
            //string _WIDOWSHOW02 = MultiLanguageSetting.SundryLanguage("WidowShow02", "08");
            //new ShowLog(RtbTxt, MessageLevel.LogMessage, _WIDOWSHOW02.Fill(_fuh.Text, MultiLanguageSetting.SundryLanguage("Open", "08")));
            ////Lc.Add(_fuh);
            ////_fuh.TopMost = false;
        }

        #endregion
        
        #region 文件等拖进拖出 
        //无法触发由于程序使用了管理员启动

        /// <summary>
        /// 如事件在这里完成将其他应用程序拖入的文件拷贝到Winform应用当前的目录中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fyp01_DragDrop(object sender, DragEventArgs e)
        {


        }
        /// <summary>
        ///  在其他应用程序拖入的文件进入时判断当前拖动的对象类型，如果是文件类型，则设置拖动响应类型为Copy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fyp01_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    e.Effect = DragDropEffects.Copy;
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }
        #endregion
        

        #region 窗体文本语言切换
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (Program._LANGUAGETYPE != LanguageEnum.LanguageCN) {
                this.MinNormalSwitch();
                Program._LANGUAGETYPE = LanguageEnum.LanguageCN;
                ManageLanguage.Instance.SetLanguage(Program._LANGUAGETYPE);//语种设置
                string Description = SpecialHelper.EnumHelper.GetEnumDescription<LanguageEnum>(LanguageEnum.LanguageCN);//获取描述
                new ShowLog(RtbTxt, MessageLevel.LogCustom, "当前语种切换为：{0}".Fill(Description), (new ShowLog.customColor() { IsEnable = true, _c = Color.Gray }));
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program._LANGUAGETYPE != LanguageEnum.LanguageEN)
            {
                this.MinNormalSwitch();
                Program._LANGUAGETYPE = LanguageEnum.LanguageEN;//语种配置切换
                ManageLanguage.Instance.SetLanguage(Program._LANGUAGETYPE);//语种切换
                string Description = SpecialHelper.EnumHelper.GetEnumDescription<LanguageEnum>(LanguageEnum.LanguageEN);//获取描述
                new ShowLog(RtbTxt, MessageLevel.LogCustom, "当前语种切换为：{0}".Fill(Description), (new ShowLog.customColor() { IsEnable = true, _c = Color.Gray }));
            }
        }



        #endregion

        #region 控件事件
        //添加便签（自定义控件）
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            AddTaskModule();
        }
        //新增模块
        private void AddTaskModule()
        {
            if (!isLogin())
                return;
            int maxNums = MaxConNums("TD");
            UserControls.TaskDetails TD = new UserControls.TaskDetails();
            string _v = MultiLanguageSetting.SundryLanguage("laTitle", "01");//多语言
            TD.Name = "TD" + DateTime.Now.ToString("yyyyMMddHHmmss") + maxNums.ToString().PadLeft(3, '0');
            TD.ID = Guid.NewGuid().ToString();
            TD.Title = _v + "TD" + maxNums.ToString().PadLeft(3, '0');
            TD.DataChange += new TaskDetails.DataChangeHandler((new MdiForm()).DataChanged);
            TD._ParentForm = this;
            Control[] TDObj = TDProperty(TD);
            this.fyp01.Controls.Add(TDObj[TDObj.Length - 1]);
            //UserControl1 _f = new UserControl1();
            //this.fyp01.Controls.Add(_f);
            this.fyp01.VerticalScroll.Value = this.fyp01.VerticalScroll.Maximum;

            //普通便签
            if (_noteType == NoteType.GeneralNote)
            {
                PlainNoteModel _PlainNoteM = new PlainNoteModel()
                {
                    Id = TD.ID,
                    NotesType = _noteType,
                    Topic = TD.Title,
                    GridOrder = maxNums,
                    NoteContent = string.Empty,
                };
                _PlainNotes.Add(_PlainNoteM);
            }
            //任务便签
            else if (_noteType == NoteType.TaskNote)
            {

            }
        }
        //展示已有模块
        private void AddTaskModule(DataTable dt) {
            foreach (DataRow dr in dt.Rows)
            {
                PlainNoteModel _f = _PlainNotes.Find(p => p.Id == dr["Id"].ToString());
                if (_f == null)
                {
                    int maxNums = MaxConNums("TD");
                    UserControls.TaskDetails TD = new UserControls.TaskDetails();
                    string _v = MultiLanguageSetting.SundryLanguage("laTitle", "01");//多语言
                    TD.Name = dr["SnNumber"].ToString();
                    TD.ID = dr["Id"].ToString();
                    TD.Title = dr["Topic"].ToString();
                    TextBox tb = (TextBox)this.findControl(TD, "txtNoteContent");
                    tb.Text = dr["NoteContent"].ToString();
                    TD.DataChange += new TaskDetails.DataChangeHandler((new MdiForm()).DataChanged);
                    TD._ParentForm = this;
                    Control[] TDObj = TDProperty(TD);
                    //如果调用该函数的线程和控件flowLayoutPanel1位于同一个线程内
                    if (this.fyp01.InvokeRequired == false)
                    {
                        this.fyp01.Controls.Add(TDObj[TDObj.Length - 1]);
                        this.fyp01.VerticalScroll.Value = this.fyp01.VerticalScroll.Maximum;
                    }
                    //如果调用该函数的线程和控件flowLayoutPanel1不在同一个线程
                    else
                    {
                        //通过使用Invoke的方法，让子线程告诉窗体线程来完成相应的控件操作
                        DisplayDelegate disp = new DisplayDelegate(AddTaskModule);
                        //使用控件flowLayoutPanel1的Invoke方法执行Display代理(其类型是DisplayDelegate)
                        this.fyp01.Invoke(disp, dt);
                    }

                    //普通便签
                    if (_noteType == NoteType.GeneralNote)
                    {
                        PlainNoteModel _PlainNoteM = new PlainNoteModel()
                        {
                            Id = TD.ID,
                            NotesType = _noteType,
                            Topic = TD.Title,
                            GridOrder = maxNums,
                            NoteContent = dr["NoteContent"].ToString(),
                            TaskId = dr["TaskId"].ToString(),
                            SnNumber = dr["SnNumber"].ToString(),
                            IsOpen = Convert.ToInt32(dr["IsOpen"].ToString()),
                        };
                        #region 测试邮件
                        //LogModel lo = new LogModel();
                        //SpecialHelper._EmailHelper.SendEmail<Model.PlainNoteModel>(_PlainNoteM, ref lo, EnumBase.EmailTemplateEn.PaEmailTemplate, "测试");
                        #endregion
                        _PlainNotes.Add(_PlainNoteM);
                    }
                    //任务便签
                    else if (_noteType == NoteType.TaskNote)
                    {

                    }
                }
                else {
                    new ShowLog(RtbTxt, MessageLevel.LogCustom, "便签<{0}>已打开！".Fill(_f.Topic), (new ShowLog.customColor() { IsEnable = true, _c = Color.Gray }));

                }

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //多行文本控件值变更事件
        private void RtbTxt_TextChanged(object sender, EventArgs e)
        {
            RtbTxt.SelectionStart = RtbTxt.Text.Length;
            RtbTxt.SelectionLength = 0;
            RtbTxt.Focus();
        }
        //帮助
        private void F1Help_Click(object sender, EventArgs e)
        {
            F1Open();
        }
        //展示切换
        private void btnShowType_Click(object sender, EventArgs e)
        {
            if (this.fyp01.Controls.Count == 0)
            {
                MessageBoxEX.Show("展示模式提示", "暂无任务，无法切换展示模式！");
                return;
            }
            //切换展示模式
            _showType = _showType == ShowType.Tile ? ShowType.List : ShowType.Tile;
            //从列表切换为平铺
            if ((int)_showType == 0)
            {
                if (_PlainNotes.Count > 0)
                {
                    foreach (PlainNoteModel item in _PlainNotes)
                    {
                        var _t = this.fyp01.Controls.Find(item.SnNumber.Replace("General-", ""), true);
                        if (_t.Count() > 0)
                        {
                            Control _td = _t[0];
                            UserControls.TaskDetails TD = new UserControls.TaskDetails();
                            TD.Name = item.SnNumber.Replace("General-", "");
                            TD.ID = item.SnNumber.Replace("General-", "");
                            TD.Title = item.Topic;
                            TD.DataChange += new BaseUserControl.DataChangeHandler((new MdiForm()).DataChanged);
                            if (string.IsNullOrEmpty(item.NoteContent))
                                if (_td.Controls.Find("txtNoteContent", true).Count() > 0)
                                    item.NoteContent = _td.Controls.Find("txtNoteContent", true)[0].Text;
                            TD.TxtNoteContetnStr = item.NoteContent;
                            this.fyp01.Controls.Remove(_td);
                            this.fyp01.Controls.Add(TD);
                        }

                    }
                }
            }
            else
            //从平铺切换为列表
            if ((int)_showType == 1)
            {
                if (_PlainNotes.Count > 0)
                {
                    foreach (PlainNoteModel item in _PlainNotes)
                    {
                        var _t = this.fyp01.Controls.Find(item.SnNumber.Replace("General-", ""), true);
                        if (_t.Count() > 0)
                        {
                            Control _td = _t[0];
                            UserControls.StripTypeTaskDetails TD = new UserControls.StripTypeTaskDetails();
                            TD.Name = item.SnNumber.Replace("General-", "");
                            TD.ID = item.SnNumber.Replace("General-", "");
                            TD.Title = item.Topic;
                            TD.DataChange += new BaseUserControl.DataChangeHandler((new MdiForm()).DataChanged);
                            if (string.IsNullOrEmpty(item.NoteContent))
                                if (_td.Controls.Find("txtNoteContent", true).Count() > 0)
                                    item.NoteContent = _td.Controls.Find("txtNoteContent", true)[0].Text;
                            TD.NoteContent = item.NoteContent;
                            this.fyp01.Controls.Remove(_td);
                            this.fyp01.Controls.Add(TD);
                        }

                    }
                }
            }
        }
        //未登录时,提示语
        private void laNoLogin_MouseLeave(object sender, EventArgs e)
        {
            laNoLogin.ForeColor = Color.Gray;
            laNoLogin.Font = new Font("微软雅黑", 9, FontStyle.Bold);
        }
        //未登录时,提示语
        private void laNoLogin_MouseEnter(object sender, EventArgs e)
        {
            laNoLogin.ForeColor = Color.Black;
            laNoLogin.Font = new Font("微软雅黑", 9, FontStyle.Underline);
        }
        //遮罩层
        private OpaqueCommand _ml = new OpaqueCommand();
        //登录窗体
        private LoginForm _lf = null;
        //未登录文本点击
        private void laNoLogin_Click(object sender, EventArgs e)
        {
            LogModel log = new LogModel();
            IsShowControl(panel1, "label", "laUserName");
            ChangeLoginSet(true,ref log);

        }

        //父级控件，需要变更控件类型名例：Label、Button，需要剔除的变更控件Name
        private void IsShowControl(Control Parents,string StrChildControlTypeName,string StruckDownContorlName=null) {
            foreach (Control item in Parents.Controls)
            {
                //检查是否存在需要变更Visible的控件
                if (item.GetType().Name.ToLower().Equals(StrChildControlTypeName.ToLower()))
                {
                    if (StruckDownContorlName != null)
                    {
                        if (StruckDownContorlName.Contains(","))//多个剔除控件判断
                        {
                            var _v = StruckDownContorlName.Split(',').Contains<string>(item.Name);
                            if (!_v)
                                item.Visible = (item.Visible ? false : true);
                        }
                        else
                        {//单个剔除控件
                            if (!item.Name.ToLower().Equals(StruckDownContorlName.ToLower()))
                                item.Visible = (item.Visible ? false : true);
                        }
                    }
                    else
                    {
                        item.Visible = (item.Visible ? false : true);
                    }
                }
                //当前控件存在子控件
                if (item.Controls.Count > 0)
                {
                    IsShowControl(item, StrChildControlTypeName);
                }
            }
        }

        //登陆
        public void ChangeLoginSet(bool _v,ref LogModel log) {
            if (_v)
            {
                _lf = new LoginForm(this);
                _lf.isOpenEnabled = false;
                fyp01.Visible = false;
                _ml.m_OpaqueLayer = null;
                //开启背景遮罩层
                _ml.ShowOpaqueLayer(this, 200, false, _lf);
                //移除当前控件事件
                laNoLogin.RemoveControlEvent("laNoLogin_Click");
            }
            else {
                fyp01.Visible = true;
                IsShowControl(panel1, "label","laUserName");
                //关闭背景遮罩层
                _ml.HideOpaqueLayer(_lf);
                if (log.Erlv == MessageLevel.LogNormal)
                {
                    if (!(log.szStr).StrIsNull())
                    {
                        string[] _vv = log.szStr.Split(',');
                        laNoLogin.Visible = false;
                        laUserName.Text = _vv[0];
                        laUserName.ForeColor = Color.Red;
                        laUserName.Visible = true;
                        MemoryCacheHelper.GetUserFullName(null, new TimeSpan(), (_vv[1]), true);//(_vv[1]);//存入缓存
                        Program.ProgramUserId = _vv[1];
                        ClientUserModel u = MemoryCacheHelper.GetInfo<ClientUserModel>();//(Program.ProgramUserId);//读取用户本地缓存
                        
                        new ShowLog(RtbTxt, MessageLevel.LogCustom,  "{0} 登陆成功！".Fill(_vv[0]), (new ShowLog.customColor() { IsEnable = true, _c = Color.Gray }));

                        StartWorker();
                    }
                }
                else if (log.Erlv == MessageLevel.LogError)
                {
                    new ShowLog(RtbTxt, log.Erlv, "登录失败！报错信息：{0}" .Fill(log.Erorr.Message));
                }
                else if (log.Erlv == MessageLevel.LogMessage)
                {
                    new ShowLog(RtbTxt, log.Erlv,   log.szStr);
                }
            }
        }

        #region 下拉图标控制
        private void toolStripMenuItem4_DropDownOpened(object sender, EventArgs e)
        {
            toolStripMenuItem4.Image = MuZiYangNote.Properties.Resources.arrow2;
        }
        private void toolStripMenuItem4_DropDownClosed(object sender, EventArgs e)
        {
            toolStripMenuItem4.Image = MuZiYangNote.Properties.Resources.arrow5;
        }
        #endregion
        private void 窗体内部展示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isLogin())
                return;
            DataTable dt = SqliteDBHelper.Query_dt((SpecialHelper.SqlHelper.TaskSqlDic("V02002")).Fill(Program.ProgramUserId), LocationDataBaseName);
            MessageBoxEX testDialog = new MessageBoxEX("历史便签", dt,Types._a);
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                AddTaskModule(testDialog.Dt);
            }
                
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDia = new ColorDialog();

            if (colorDia.ShowDialog() == DialogResult.OK)
            {
                //获取所选择的颜色
                Color colorChoosed = colorDia.Color;
                //改变panel的背景色
                //flowLayoutPanel1.BackColor = colorChoosed;
                //Font _ft=new Font()
            }
        }

        //退出登陆
        private void TsmlLoggedOut_Click(object sender, EventArgs e)
        {
            if ((Program.ProgramUserId).StrIsNull())
            {
                new ShowLog(RtbTxt, MessageLevel.LogNormal, "{0}".Fill(laUserName.Text));
                return;
            }
            Program.ProgramUserId = null;
            new ShowLog(RtbTxt, MessageLevel.LogNormal, "{0}，已退出登陆！".Fill(laUserName.Text));
            laNoLogin.Visible = true;
            laUserName.Visible = false;
            laUserName.Text = "暂未登录，请先登录！";
            laUserName.ForeColor = Color.Gray;
        }
        #endregion

        //在winform中查找控件
        private System.Windows.Forms.Control findControl(System.Windows.Forms.Control control, string controlName)
        {
            Control c1;
            foreach (Control c in control.Controls)
            {
                if (c.Name == controlName)
                {
                    return c;
                }
                else if (c.Controls.Count > 0)
                {
                    c1 = findControl(c, controlName);
                    if (c1 != null)
                    {
                        return c1;
                    }
                }
            }
            return null;
        }

        private bool isLogin() {
            if (string.IsNullOrEmpty(Program.ProgramUserId))
            {
                MessageBoxEX.Show("请先登陆！");
                return false;
            }
            else {
                return true;
            }
        }


    }
}
