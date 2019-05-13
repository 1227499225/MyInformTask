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
using System.Data.SqlClient;
using System.Collections;
using static MuZiYangNote.CommandClass;

namespace MuZiYangNote.UserControls
{
    /// <summary>
    /// 块状任务
    /// </summary>
    public partial class TaskDetails : BaseUserControl
    {
        public TaskDetails()
        {
            InitializeComponent();
        }

        ~TaskDetails() { }
        #region 任务配置
        public MdiForm _ParentForm { get; set; }
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

        private string txtNoteContetnStr = string.Empty;
        public string TxtNoteContetnStr
        {
            get
            {
                return txtNoteContetnStr;
            }

            set
            {
                txtNoteContetnStr = value;
            }
        }
        #endregion

        private void btnIsOk_Click(object sender, EventArgs e)
        {

            //if (txtAddress.Text != "")
            //{
            //MessageBoxEX testDialog = new MessageBoxEX(null);
            MessageBoxEX testDialog = new MessageBoxEX("用户信息", new DataTable(), Types._null);
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
            string _v = MultiLanguageSetting.SundryLanguage("AddModule", "09");//多语言
            OnDataChange(new BaseEv.DataChangeEventArgs(_v.Fill(this.Name, this.title), MessageLevel.LogAppend));
            txtChangTitle.LostFocus += new EventHandler(txtChangTitle_LostFocus);
            this.SizeChanged += new EventHandler(TaskDetails_SizeChanged);
            if (!TxtNoteContetnStr.StrIsNull())
            {
                this.RichTbNoteContent.Text = TxtNoteContetnStr;
            }
            timer1.Start();
        }

        private void TaskDetails_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = this.Width;
            panel2.Width = this.Width;
            RichTbNoteContent.Width = panel2.Width;
            btnClose.Location = new Point(btnClose.Parent.Width - btnClose.Width, btnClose.Location.Y);
            //txtChangTitle.Width = txtChangTitle.Parent.Width / txtChangTitle.Width * txtChangTitle.Width;
            //txtChangTitle.Location = new Point(txtChangTitle.Location.X, txtChangTitle.Parent.Height/2- txtChangTitle.Height/2);//上下居中
        }

        //失去焦点
        private void txtChangTitle_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtChangTitle.Text))
            {
                if (laTitle.Text != txtChangTitle.Text)
                {
                    laTitle.Text = txtChangTitle.Text;
                    string _v = MultiLanguageSetting.SundryLanguage("UpdateTitle", "09");//多语言
                    OnDataChange(new BaseEv.DataChangeEventArgs(_v.Fill(this.Name, this.title, laTitle.Text), MessageLevel.LogCustom, new ShowLog.customColor() { IsEnable = true, _c = Color.Blue }));
                    this.title = txtChangTitle.Text;
                }
            }

            RefreshData();

            txtChangTitle.Visible = false;
        }

        private List<_KeyStrModel> _LK = new List<_KeyStrModel>();

        //更新数据
        private void RefreshData()
        {
            if (_ParentForm == null)
                return;
            if (_ParentForm._PlainNotes.Count > 0)
            {
                foreach (PlainNoteModel item in _ParentForm._PlainNotes)
                {
                    _KeyStrModel _k = new _KeyStrModel();
                    if (this.ID == item.Id)
                    {
                        item.Topic = this.Title;
                        item.NoteContent = this.RichTbNoteContent.Text;
                        _k.Key = item.Id;
                        _k.Value = item as Object;
                        lock (_LK)
                        {
                            _KeyStrModel a = _LK.Select(p => p.Key == _k.Key) as _KeyStrModel;
                            if (a == null)
                            {
                                _LK.Add(_k);
                            }
                            else
                            {
                                _LK.Remove(a);
                                _LK.Add(_k);
                            }
                        }
                    }
                }

                if (_LK.Count > 0)
                {
                    if (timer1.Enabled == false)
                        timer1.Start();
                }

            }
        }

        //新增修改数据
        private void ActionSql()
        {
            lock (_LK)
            {
                if (_LK.Count == 0)
                {
                    return;
                }
                foreach (_KeyStrModel _L in _LK)
                {
                    PlainNoteModel item = _L.Value as PlainNoteModel;
                    string szSQL = (SpecialHelper.SqlHelper.TaskSqlDic("V02001")).Fill(item.TaskId);
                    string res = SqliteDBHelper.QueryString(szSQL, _ParentForm.LocationDataBaseName);

                    if (Convert.ToInt32(res) > 0)
                    {
                        //修改
                        item.LastModifiedTime = Convert.ToDateTime(DateTime.Now.ToString("s"));
                        item.ModifyContent = "/";
                        object[] _ob = SpecialHelper.CreateUpdateSql<PlainNoteModel>(item);
                        int i = SqliteDBHelper.ExecuteSql(_ob[0].ToString(), _ob[1].ToString());
                        _L.Key = "0";
                    }
                    else
                    {
                        //新增
                        object[] _ob = SpecialHelper.CreateInserSql<PlainNoteModel>(item);
                        ArrayList listSql = new ArrayList();
                        listSql.Add(_ob[0].ToString().Replace("@strSqlValue", (_ob[1].ToString())));
                        string taskid = InstHelper.CreatTaskInfo<PlainNoteModel>(
                             SqlList: listSql,
                             CreatorId: Program.ProgramUserId
                             );
                        item.TaskId = taskid;
                        _L.Key = "0";
                    }
                }
                _LK.RemoveAll(p => p.Key == "0");
            }
        }

        private void btnClose_ButtonClick(object sender, EventArgs e)
        {
            string _v = MultiLanguageSetting.SundryLanguage("ClosingModule", "09");//多语言
            OnDataChange(new BaseEv.DataChangeEventArgs(_v.Fill(this.Name, this.title), MessageLevel.LogWarning));
            PlainNoteModel _p = (_ParentForm._PlainNotes).Find(p => p.Id == this.id);
            if (_p != null)
                (_ParentForm._PlainNotes).Remove(_p);
            this.Dispose();
        }

        private void laTitle_DoubleClick(object sender, EventArgs e)
        {
            txtChangTitle.Visible = true;
            txtChangTitle.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ActionSql();
            int _i = 0;
            lock (_LK)
            {
                _i = _LK.Count;
            }
            if (_i > 0)
            {
                if (timer1.Interval != 100)
                    this.timer1.Interval = 100;
            }
            else
            {
                this.timer1.Stop();
            }
        }

        public static async Task<string> DownLoadAsync(string content, PlainNoteModel p)
        {
            return await Task.Run(() =>
            {
                return "Jesse";
            });
        }

        private StateLevel StateLevel { get; set; } = StateLevel.LvZero;
        private Keys NowRichKeys;

        private void RichTbNoteContent_TextChanged(object sender, EventArgs e)
        {
            string RichTbNoteContentStr = RichTbNoteContent.Text;//获取文本
            CommandClass _c = new CommandClass(RichTbNoteContentStr);//根据文本返回命令相关
            if (_c.rts.Count > 0)//存在命令信息
            {
                if (NowRichKeys != Keys.Back)//当前操作之前操作按键非BACKSPACE键
                {
                    if (RichTbNoteContentStr.EndsWith(">", StringComparison.CurrentCultureIgnoreCase))//文本最后一位字符串是>当前命令结束符
                    {
                        if (RichTbNoteContentStr.EndsWith("\n>", StringComparison.CurrentCultureIgnoreCase))//文本最后三位是否换行及命令结束符
                        {
                            RichTbNoteContentStr = RichTbNoteContentStr.Substring(0, RichTbNoteContentStr.Length - 3) + ">";//添加命令结束符
                            RichTbNoteContent.Text = RichTbNoteContentStr;
                        }
                        ChangeColor(_c.rts);//UI界面处理
                    }
                    else if (RichTbNoteContentStr.EndsWith(">end>", StringComparison.CurrentCultureIgnoreCase))//命令结束
                    {

                    }
                }
            }
            else
            {
                RefreshData();
            }
        }


        private void RichTbNoteContent_Click(object sender, EventArgs e)
        {
            _ParentForm.pda.Starts = true;
            _ParentForm.pda.Parame1 = this.Name;
            _ParentForm.pda.Parame2 = this.Title;
        }

        private void RichTbNoteContent_KeyDown(object sender, KeyEventArgs e)
        {
            NowRichKeys = e.KeyData;
        }
        #region 鼠标按下是记忆格式，鼠标弹起时设置格式。
        Color SelectionColor;
        Font SelectionFont;
        private void RichTbNoteContent_MouseDown(object sender, MouseEventArgs e)
        {
            SelectionColor = RichTbNoteContent.SelectionColor;
            SelectionFont = RichTbNoteContent.SelectionFont;
        }

        private void RichTbNoteContent_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectionFont != null)
            {
                RichTbNoteContent.SelectionColor = SelectionColor;
                RichTbNoteContent.SelectionFont = SelectionFont;
            }
        }
        #endregion

        #region 
        /// <summary>
        /// 改变richTextBox中指定字符串的颜色
        /// 调用即可
        /// </summary>
        /// <param name="str" value="为指定的字符串"></param>
        internal void ChangeColor(List<ReturnStr> rs)
        {
            foreach (ReturnStr item in rs)
            {
                ArrayList list = GetIndexArray(RichTbNoteContent.Text, item.Command);
                if (item.StateLevel == StateLevel.LvOne)//执行
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        int index = (int)list[i];
                        RichTbNoteContent.SelectionColor = item.color;
                        RichTbNoteContent.Select(index, item.Command.Length);

                        RichTbNoteContent.SelectionColor = item.color;
                        RichTbNoteContent.AppendText(item.CommandStr);
                    }
                }
                if (item.StateLevel == StateLevel.LvZero)//清除
                {
                    string Str = RichTbNoteContent.Text;
                    if (!item.CommandStr.StrIsNull())
                        RichTbNoteContent.Text = Str.Replace(item.CommandStr.ToLower(), "");
                }
            }
        }
        internal ArrayList GetIndexArray(String inputStr, String findStr)
        {
            ArrayList list = new ArrayList();
            int start = 0;
            while (start < inputStr.Length)
            {
                int index = inputStr.IndexOf(findStr, start, StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0)
                {
                    list.Add(index);
                    start = index + findStr.Length;
                }
                else
                {
                    break;
                }
            }
            return list;
        }



        #endregion

        #region 
        //        //将RichTextBox的内容直接写入数据库：
        //private void button1_Click(object sender, EventArgs e)
        //        {
        //            System.IO.MemoryStream mstream = new System.IO.MemoryStream();
        //            this.richTextBox1.SaveFile(mstream, RichTextBoxStreamType.RichText);
        //            //将流转换成数组
        //            byte[] bWrite = mstream.ToArray();
        //            //将数组写入数据库
        //            System.Data.SqlClient.SqlParameter[] pram ={
        //          sqlHelper.MakeInParam("@XX",System.Data.SqlDbType.Image)
        //   };
        //            pram[0].Value = bWrite;
        //            sqlHelper.RunSql("insert into XXX (XX) values (@XX)", pram);
        //        }
        //        //将数据库中的RTF读出并填充到RichTextBox
        //private void button2_Click(object sender, EventArgs e)
        //        {
        //            //从数据库中读出数据
        //            DataTable dt = sqlHelper.GetDataTable("select XX from XXX where .....");
        //            byte[] bWrite = (byte[])dt.Rows[0][0];
        //            //将数组转换成stream
        //            System.IO.MemoryStream mstream = new System.IO.MemoryStream(bWrite, false);
        //            //将stream填充到RichTextBox
        //            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
        //        }

        //richTextBox中高亮显示选中字符串或文本
        //private void 突出显示(string 要查找字符串)
        //{
        //    //首先找到要查找字符串的起始位置
        //    int 开始位置 = richTextBox短语显示.Find(要查找字符串);
        //    //判断一下是否找到,如果找不到那么开始位置是-1
        //    if (开始位置 >= 0)
        //    {
        //        richTextBox短语显示.SelectionStart = 开始位置;
        //        //得到字符串的长度
        //        richTextBox短语显示.SelectionLength = 要查找字符串.Length;
        //        //然后就可以改变这个字符串的颜色
        //        richTextBox短语显示.SelectionColor = Color.Red;
        //    }
        //}
        //总结:要使用程序来对选中的文本或字符串做一些格式处理需要使用richTextBox,普通的TextBox不行.
        //其中最常用的就是:
        //richTextBox.Find:用来查找字符串并得到其起始位置
        //richTextBox.SelectionStart:获取或设置要选中的字符串起始位置
        //richTextBox.SelectionLength:获取或设置要选中的字符串的长度
        //最后就是格式设置了, 上面的实例中只改变了一个颜色,
        //根据你自己的需要还可以改变大小, 字体等等.

        //比如:richTextBox短语显示.SelectionFont = new Font("黑体", 13);
        #endregion


    }
}


//避免输出同时存储导致界面卡顿：
//界面加载开始定时器
//存储集合数据为空，定时器停止
//添加数据，数据集添加数据，启动定时器,开始处理数据集数据
//根据数据库是否存在判断，已有则执行修改，反之新增。
//新增数据至数据集时，若数据集中存在当前数据，则根据Key辨识后删除在新增。