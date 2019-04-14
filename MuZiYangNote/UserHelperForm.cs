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
using System.Text.RegularExpressions;
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
            //this.laUserHelpeTitle.Text = MultiLanguageSetting.SundryLanguage("Prompt", "08");
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
                OnDataChange(new BaseEv.DataChangeEventArgs("插入图片！文件路径：" + ofd.FileName, MessageLevel.LogAppend));
            }
        }
        string fileName = null, SafeFileName =null;
        public void OnOpenButtonClicked()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.html|*.html";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                kEditor1.Reset();
                fileName = ofd.FileName;
                SafeFileName = ofd.SafeFileNames[0];
                string text = System.IO.File.ReadAllText(ofd.FileName);
                //text=ContentReplace(text);
                kEditor1.InsertNode(text);
                OnDataChange(new BaseEv.DataChangeEventArgs("打开文件！文件路径：" + fileName, MessageLevel.LogAppend));
            }
        }

        public void OnOpenBrowserUrlClicked() {
            if (string.IsNullOrEmpty(fileName))
            {
                OnDataChange(new BaseEv.DataChangeEventArgs("暂无文件可在浏览器打开！", MessageLevel.LogError));
                return;
            }
            BrowserHelper.OpenBrowserUrl(fileName);
            OnDataChange(new BaseEv.DataChangeEventArgs("浏览器中打开文件！文件路径：" + fileName, MessageLevel.LogAppend));
        }

        public void OnRefreshBrowserUrlClicked() {
            if (string.IsNullOrEmpty(fileName))
            {
                OnDataChange(new BaseEv.DataChangeEventArgs("暂无文件，无法刷新文件！", MessageLevel.LogError));
                return;
            }
            kEditor1.Reset();
            string text = System.IO.File.ReadAllText(fileName);
            kEditor1.InsertNode(text);
            OnDataChange(new BaseEv.DataChangeEventArgs("执行刷新！文件路径：" + fileName, MessageLevel.LogAppend));
        }

        public void OnOpenFileNameClicked() {
            if (string.IsNullOrEmpty(SafeFileName))
            {
                OnDataChange(new BaseEv.DataChangeEventArgs("暂无文件，无法打开文件！", MessageLevel.LogError));
                return;
            }
            string _v = fileName.Replace(SafeFileName, "");
            System.Diagnostics.Process.Start(_v);
        }

        public void OnSaveButtonClicked()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.html|*.html";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string Strhtml = kEditor1.Html;
                    //+ @"<stype>
                    //            .table {
                    //                border-collapse: collapse !important;
                    //            }
                    //            .table td,
                    //            .table th {
                    //                    background-color: #fff !important;
                    //             }
                    //            .table-bordered th,
                    //            .table-bordered td {
                    //                border: 1px solid #ddd !important;
                    //            }
                    //        </stype>";
                System.IO.File.WriteAllText(sfd.FileName, Strhtml);
                OnDataChange(new BaseEv.DataChangeEventArgs("保存文件！文件路径：" + sfd.FileName, MessageLevel.LogAppend));
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

        private void tabPage2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.S)
            {
                OnSaveButtonClicked();
            }
        }

        //去除HTML所有标签
        public static string ContentReplace(string input)
        {
            input = Regex.Replace(input, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"-->", "", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"<!--.*", "", RegexOptions.IgnoreCase);

            input = Regex.Replace(input, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            input.Replace("<", "");
            input.Replace(">", "");
            input.Replace("\r\n", "");
            //去两端空格，中间多余空格
            input = Regex.Replace(input.Trim(), "\\s+", " ");
            return input;
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            this.MaxNormalSwitch();
        }
    }
}
