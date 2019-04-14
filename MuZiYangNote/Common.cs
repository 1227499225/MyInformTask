using MuZiYangNote.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote
{
    class Common
    {
        /*
        * ============================================================
        * 函数名：ShowProcessing
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：等待窗体
        * ============================================================
        */
        public static void ShowProcessing(string msg, Form owner, ParameterizedThreadStart work, object workArg = null)
        {
            FrmProcessing processingForm = new FrmProcessing(msg);
            dynamic expObj = new ExpandoObject();
            expObj.Form = processingForm;
            expObj.WorkArg = workArg;
            processingForm.SetWorkAction(work, expObj);
            processingForm.ShowDialog(owner);
            if (processingForm.WorkException != null)
            {
                throw processingForm.WorkException;
            }
            //调用
            //Common.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
            //{
            //    //这里写处理耗时的代码，代码处理完成则自动关闭该窗口
            //    for (int i = 0; i < 5000000000000000000; i++)
            //    {}
            //}, null);
        }
    }

    /*
    * ============================================================
    * 类名：OpaqueCommand
    * 作者：木子杨
    * 版本：1.0
    * 日期：
    * 描述：透明层
    * ============================================================
    */
    class OpaqueCommand
    {
        public UserControls.MaskLayer m_OpaqueLayer = null;//半透明蒙板层

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="ParentControl">父级控件</param>
        /// <param name="ChildControl">子级控件</param>
        /// <param name="alpha">透明度</param>
        /// <param name="isShowLoadingImage">是否显示图标</param>
        public void ShowOpaqueLayer(Control ParentControl,  int alpha, bool isShowLoadingImage, Form ChildControl = null)
        {
            try
            {
                if (ParentControl.Controls.Find("MaskLayer", false).Count() > 0)
                    ParentControl.Controls.Remove((ParentControl.Controls.Find("MaskLayer", false))[0]);
                if (ParentControl.Controls.Find("MaskLayer", false).Count() == 0)
                {
                    //if (this.m_OpaqueLayer == null)
                    {
                        this.m_OpaqueLayer = new UserControls.MaskLayer(alpha, isShowLoadingImage);//实例化
                        this.m_OpaqueLayer.Name = "MaskLayer";
                        ParentControl.Controls.Add(this.m_OpaqueLayer);//添加控件
                        //ParentControl.Controls.SetChildIndex(this.m_OpaqueLayer, 1000);//最前显示
                        this.m_OpaqueLayer.Dock = DockStyle.Fill;
                        this.m_OpaqueLayer.BringToFront();

                        if (ChildControl != null)
                        {
                            //非顶级窗体
                            ChildControl.TopLevel = false;
                            //位置
                            int x = (int)(0.5 * (this.m_OpaqueLayer.Width - ChildControl.Width));
                            int y = (int)(0.5 * (this.m_OpaqueLayer.Height - ChildControl.Height));
                            ChildControl.Location = new System.Drawing.Point(x, y);
                            ChildControl.Parent = this.m_OpaqueLayer;
                            ChildControl.Show();
                        }
                    }
                    this.m_OpaqueLayer.Enabled = true;
                    this.m_OpaqueLayer.Visible = true;
                }
                else {
                    this.m_OpaqueLayer.Enabled = true;
                    this.m_OpaqueLayer.Visible = true;
                }
            }
            catch (Exception)
            {
                HideOpaqueLayer(ChildControl);
                MessageBoxEX._Show("登录界面开启失败！");
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 隐藏遮罩层
        /// </summary>
        public void HideOpaqueLayer(Form ChildControl)
        {
            try
            {
                if (this.m_OpaqueLayer != null)
                {
                    this.m_OpaqueLayer.Visible = false;
                    this.m_OpaqueLayer.Enabled = false;
                    this.m_OpaqueLayer.Controls.Remove(ChildControl);
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
    public static class ExpandContorl
    {
        /// <summary>
        /// 移除控件某个事件
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="eventName">需要移除的控件名称eg:EventClick</param>
        public static void RemoveControlEvent(this Control control, string eventName)
        {
            FieldInfo _fl = typeof(Control).GetField(eventName, BindingFlags.Static | BindingFlags.NonPublic);
            if (_fl != null)
            {
                object _obj = _fl.GetValue(control);
                PropertyInfo _pi = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                EventHandlerList _eventlist = (EventHandlerList)_pi.GetValue(control, null);
                if (_obj != null && _eventlist != null)
                    _eventlist.RemoveHandler(_obj, _eventlist[_obj]);
            }
        }
    }
}
