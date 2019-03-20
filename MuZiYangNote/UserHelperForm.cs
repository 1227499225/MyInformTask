using KSharpEditor;
using Model;
using MuZiYangNote.UserControls;
using PublicHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote
{
    public partial class UserHelperForm : FormBase,IKEditorEventListener
    {

        private Form _parentForm = new Form();

        public UserHelperForm(Form parentForm=null)
        {
            InitializeComponent();
            this._parentForm = parentForm;

        }



        private void FmUserHelper_Load(object sender, EventArgs e)
        {
            //后续添加中英文切换
            //this.laUserHelpeTitle.Text = MultiLanguageSetting.SundryLanguage("Prompt","08");
            this.LanguageChange();
            this.DataChange += new DataChangeHandler(DataChanged);
            FileInfo file = new FileInfo("../../Files/SystemFile/SystemPages/FmUserHelper.html");
            webBrowser1.Url = new Uri(file.FullName, UriKind.Absolute);

            kEditor1.KEditorEventListener = this;//富文本
        }
        private void LanguageChange()
        {
            this.MinNormalSwitch();
            ManageLanguage.Instance.SetLanguage(Program._LANGUAGETYPE);//语种设置
        }


        public void DataChanged(object sender, BaseEv.DataChangeEventArgs args)
        {
            if (this._parentForm != null) {
                //更新窗体控件
                Control _cc = this._parentForm.Controls.Find("RtbTxt", true)[0];
                if (_cc.Name == "RtbTxt")
                {
                    string Str = args.Str;
                    Model.MessageLevel ty = args.ty;
                    new PublicHelper.ShowLog(_cc as RichTextBox, ty, Str, args._c);
                }
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
            OnDataChange(new BaseEv.DataChangeEventArgs(_WIDOWSHOW02.Fill(this.Text, MultiLanguageSetting.SundryLanguage("Close", "08")), Model.MessageLevel.LogMessage, (new PublicHelper.ShowLog.customColor() { IsEnable = true, _c = Color.Red })));
            if (this._parentForm != null)
            {
                (this._parentForm as MdiForm)._lc.Remove(this);
            }
            this.Dispose(true);
            this.Close();
        }

        #region 富文本框对应事件
        public void OnEditorErrorOccured(Exception ex)
        {
            //hrow new NotImplementedException();
        }

        public void OnEditorLoadComplete()
        {
            //throw new NotImplementedException();
        }

        public void OnInsertImageClicked()
        {
            // throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.jpg|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string n = "<img src=\"file://" + ofd.FileName + "\" />";
                kEditor1.InsertNode(n);
            }
        }

        public void OnOpenButtonClicked()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.html|*.html";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                kEditor1.ClearConext();
                string text = System.IO.File.ReadAllText(ofd.FileName);
                kEditor1.InsertNode(text);
            }
        }

        public void OnSaveButtonClicked()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.html|*.html";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(sfd.FileName, kEditor1.Html);
            }
        }
        #endregion

        private void btnEXMax_ButtonClick(object sender, EventArgs e)
        {
            this.MaxNormalSwitch();
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
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
                //最大化图标切换
                this.btnEXMax.ImageDefault = global::MuZiYangNote.Properties.Resources.MaxNormal;
            }
            this.Invalidate();//使重绘
        }

        private void btnEXMin_ButtonClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void kEditor1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.S)
            {
                OnSaveButtonClicked();
            }
        }
    }
}
