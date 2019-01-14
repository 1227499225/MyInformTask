using MuZiYangNote.UserControls;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
}
