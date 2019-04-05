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
                MessageBoxEX testDialog =new MessageBoxEX("用户信息", new DataTable(),Types._null);
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
            OnDataChange(new BaseEv.DataChangeEventArgs(_v.Fill(this.Name, this.title), MessageLevel.LogAppend));
            txtChangTitle.LostFocus += new EventHandler(txtChangTitle_LostFocus);

            if (!TxtNoteContetnStr.StrIsNull()) {
                txtNoteContent.Text = TxtNoteContetnStr;
            }

            //laCreationTime.Parent= txtNoteContent.Parent;
            //laCreationTime.BackColor = Color.Transparent;
            //laCreationTime.Dock = DockStyle.Bottom;
            //laCreationTime.Location = new Point(80, 80);

            timer1.Start();
            //timer1.
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
        //跟新数据
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
                        item.NoteContent = this.txtNoteContent.Text;
                        _k.Key = item.Id;
                        _k.Value = item as Object;
                        lock (_LK)
                        {
                            _KeyStrModel a = _LK.Select(p => p.Key == _k.Key) as _KeyStrModel;
                            if (a == null)
                            {
                                _LK.Add(_k);
                            }
                            else {
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
        private void ActionSql() {
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
                _LK.RemoveAll(p=>p.Key=="0");
            }
        }

        private void btnClose_ButtonClick(object sender, EventArgs e)
        {
            string _v = MultiLanguageSetting.SundryLanguage("DeleteModule","09");//多语言
            OnDataChange(new BaseEv.DataChangeEventArgs(_v.Fill(this.Name, this.title), MessageLevel.LogWarning));
            PlainNoteModel _p= (_ParentForm._PlainNotes).Find(p=>p.Id==this.id);
            if (_p != null)
                (_ParentForm._PlainNotes).Remove(_p);
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

        private void txtNoteContent_TextChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ActionSql();
            int _i = 0;
            lock(_LK) {
                _i = _LK.Count;
            }
            if (_i > 0)
            {
                if (timer1.Interval != 100)
                    this.timer1.Interval = 100;
            }
            else {
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
    }

}

//界面加载开始定时器
//存储集合数据为空，定时器停止
//添加数据，数据集添加数据，启动定时器,开始处理数据集数据
//根据数据库是否存在判断，已有则执行修改，反之新增。
//新增数据至数据集时，若数据集中存在当前数据，则根据Key辨识后删除在新增。