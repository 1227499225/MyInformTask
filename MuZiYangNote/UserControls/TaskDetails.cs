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
            //Control.CheckForIllegalCrossThreadCalls = false;  
            
        }
        ~TaskDetails() { }
        #region 任务配置
        public MdiForm _ParentForm { get; set; } = new MdiForm();
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

        //跟新数据
        private void RefreshData()
        {
            if (_ParentForm._PlainNotes.Count > 0)
            {
                foreach (PlainNoteModel item in _ParentForm._PlainNotes)
                {
                    if (this.ID == item.Id)
                    {
                        item.Topic = this.Title;
                        item.NoteContent = this.txtNoteContent.Text;
                        string szSQL = (SpecialHelper.SqlHelper.TaskSqlDic("V02001")).Fill(item.TaskId);
                        string res= SqliteDBHelper.QueryString(szSQL, _ParentForm.LocationDataBaseName);
                        if (Convert.ToInt32(res) > 0)
                        {
                            //修改
                            item.LastModifiedTime = Convert.ToDateTime(DateTime.Now.ToString("s"));
                            item.ModifyContent = "/";
                            object[] _ob = SpecialHelper.CreateUpdateSql<PlainNoteModel>(item);
                            int i = SqliteDBHelper.ExecuteSql(_ob[0].ToString(), _ob[1].ToString());
                        }
                        else {
                            //新增
                           object[] _ob= SpecialHelper.CreateInserSql<PlainNoteModel>(item); 
                            ArrayList listSql = new ArrayList();
                            listSql.Add(_ob[0].ToString().Replace("@strSqlValue", (_ob[1].ToString())));
                           string taskid= InstHelper.CreatTaskInfo<PlainNoteModel>(
                                SqlList: listSql,
                                CreatorId: Program.ProgramUserId
                                );
                            item.TaskId = taskid;
                        }
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
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
    }

}
