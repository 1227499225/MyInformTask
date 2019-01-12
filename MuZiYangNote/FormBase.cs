using Model;
using MuZiYangNote.MultiLanguageConfig;
using PublicHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PublicHelper.SpecialHelper;

namespace MuZiYangNote
{
    /// <summary>
    /// 多语言
    /// </summary>
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
            ManageLanguage.Instance.RegObject(this);
        }
        #region 3、语种切换接口
        /// <summary>
        /// 语言切换的接口
        /// </summary>
        public virtual void Language()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(this.GetType());
            ArrayList list = new ArrayList();
            FindControls(list, this);
            this.Text = resources.GetString("$this.Text");

            foreach (Control ctl in list)
            {
                switch (((ctl.GetType()).Name).ToLower())
                {
                    //菜单
                    case "menustrip":
                        foreach (ToolStripMenuItem item in (ctl as MenuStrip).Items)
                        {
                            resources.ApplyResources(item, item.Name);
                            foreach (ToolStripMenuItem subItem in item.DropDownItems)
                            {
                                resources.ApplyResources(subItem, subItem.Name);
                            }
                        };
                        break;
                    case "tabcontrol":
                        foreach (TabPage item in (ctl as TabControl).TabPages)
                        {
                            resources.ApplyResources(item, item.Name);
                        };
                        break;
                    //多格式文本框
                    case "richtextbox":break;
                    //文本框
                    case "textbox":break;
                    //容器
                    case "panel":break;
                    case "flowlayoutpanel":break;
                    case "tooltip": break;
                    //其他
                    default:
                        if (ctl.Visible)//只为显示的其他控件做转换
                        {
                            string _v = MultiLanguageSetting.SundryLanguage(ctl.Name, null, null, false);
                            if (_v.StrIsNull())
                                resources.ApplyResources(ctl, ctl.Name);
                            else
                                ctl.Text = _v;
                        }

                        break;
                }
            }


        }

        /// <summary>
        /// 把可以本地化的控件放入LIST
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ctl"></param>
        private void FindControls(ArrayList list, Control ctl)
        {
            //容器不可以本地化
            if (ctl is ContainerControl)
            {
                // MessageBox.Show(ctl.Name);
            }
            else
            {
                list.Add(ctl);
            }

            if (ctl.HasChildren)
            {
                foreach (Control c in ctl.Controls)
                {
                    if (c is Form)
                    {
                    }
                    else
                    {
                        FindControls(list, c);
                    }
                }
            }
        }
        #endregion
    }
    #region 2、语种管理器
    public class ManageLanguage
    {
        public static ManageLanguage Instance = new ManageLanguage();

        ArrayList objectList = new ArrayList();

        //1、语种管理器

        /// <summary>
        /// 注册FORM
        /// </summary>
        /// <param name="item"></param>
        public void RegObject(FormBase item)
        {
            if (objectList.Contains(item) != true)
            {
                objectList.Add(item);
            }
        }
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="lg">语言种类</param>
        public void SetLanguage(LanguageEnum lg)
        {
            switch (lg)
            {
                case LanguageEnum.LanguageCN:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
                    CallBackLanguage();
                    break;
                case LanguageEnum.LanguageEN:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("EN");
                    CallBackLanguage();
                    break;
            }
        }

        /// <summary>
        /// 遍历注册过的FORM，切换语言
        /// </summary>
        void CallBackLanguage()
        {
            //遍历所有Form，以切换其语言
            foreach (FormBase form in objectList)
            {
                form.Language();
            }
        }
    }
    #endregion

    #region 杂项多语言
    //根据当前的语言区域,更新主窗口菜单的语言
    public class MultiLanguageSetting
    {
        public static string SundryLanguage(string Filde, string _Code = null, string oldFilde = null,bool ifTheyKnow=true)
        {
            string _v = string.Empty, BaseName = "MuZiYangNote.MultiLanguageConfig.";
            if (Program._LANGUAGETYPE == LanguageEnum.LanguageCN)
                Filde = Filde + "Cn";
            else
                Filde = Filde + "En";
            if (!string.IsNullOrEmpty(_Code)) {
                switch (_Code)
                {
                    case "09"://日志部分多语言
                        _v = Language<LogsCnOrEn>(BaseName + "LogsCnOrEn", Filde);
                        break;
                    case "01"://模块标签
                        _v = Language<TaskDetailsCnOrEn>(BaseName + "TaskDetailsCnOrEn", Filde);
                        break;
                    //case "02"://条纹标签
                    //    if (Program._LANGUAGETYPE == LanguageEnum.LanguageCN)
                    //        _v = Language<StripTypeTaskDetailsCn>(BaseName + "StripTypeTaskDetailsCn", Filde);
                    //break;
                    case "08":
                            _v = (Language<CommonCnOrEn>(BaseName + "CommonCnOrEn", Filde));
                        break;
                    default:break;

                }
            }
            //不知道编号的话就查找所有的资源文件
            if (!ifTheyKnow)
            {
                _v = Language<LogsCnOrEn>(BaseName + "LogsCnOrEn", Filde);
                if (!_v.StrIsNull())
                    return _v;
                _v = Language<TaskDetailsCnOrEn>(BaseName + "TaskDetailsCnOrEn", Filde);

                if (!_v.StrIsNull())
                    return _v;

                _v = Language<CommonCnOrEn>(BaseName + "CommonCnOrEn", Filde);
                if (!_v.StrIsNull())
                    return _v;

            }

            if (string.IsNullOrEmpty(_v) && ifTheyKnow)
            return _v = (SundryLanguage("LanguageError", "08", Filde));

            if(Filde.Replace("En","").Replace("Cn","") == "LanguageError")
                _v= _v.Fill(oldFilde);


            return _v;
        }
        //根据当前的语言区域,更新主窗口的语言信息
        private static string Language<T>(string BaseName, string Filde)
        {
            System.Resources.ResourceManager resManagerA = new System.Resources.ResourceManager(BaseName, typeof(T).Assembly);
            string _Str = resManagerA.GetString(Filde);
            return _Str;
        }
    }
    #endregion
}
