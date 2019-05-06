using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace PublicHelper
{
    /// <summary>
    /// 系统显示日志记录、支持其他线程访问  该功暂不上线
    /// </summary>
    public class ShowLog
    {

        public class customColor {
            public bool IsEnable { get; set; } = false;
            public Color _c = new Color();
        }
        public ShowLog( RichTextBox RtbTxt, MessageLevel ty, string Str, customColor c =null) {
            switch (ty)
            {
                case MessageLevel.LogAppend: LogAppend( RtbTxt, Str); break;
                case MessageLevel.LogError: LogError( RtbTxt, Str);break;
                case MessageLevel.LogWarning: LogWarning( RtbTxt, Str);break;
                case MessageLevel.LogCustom: LogCustom(RtbTxt,Str,c) ; break;
                default:LogMessage( RtbTxt, Str); break;
            }
        }
        #region 
        public System.Windows.Forms.RichTextBox RtbTxt { get; set; }
        public delegate void LogAppendDelegate( RichTextBox RtbTxt,Color color, string text);
        public void LogAppend( RichTextBox RtbTxt,Color color, string text)
        {
            if (RtbTxt.InvokeRequired == false)
            {
                RtbTxt.AppendText("\n");
                RtbTxt.SelectionColor = color;
                RtbTxt.AppendText(text);
            }
            else
            {
                LogAppendDelegate la = new LogAppendDelegate(LogAppend);
                RtbTxt.Invoke(la, RtbTxt, color, text);
            }
        }
        public void LogError( RichTextBox RtbTxt,string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            la.Invoke( RtbTxt, Color.Red, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        public void LogWarning( RichTextBox RtbTxt,string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            la.Invoke( RtbTxt, Color.Violet, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        public void LogMessage( RichTextBox RtbTxt,string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            la.Invoke( RtbTxt, Color.Black, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        public void LogAppend( RichTextBox RtbTxt,string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            la.Invoke( RtbTxt, Color.Gray, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        public void LogCustom(RichTextBox RtbTxt, string text, customColor c) {
            if (c.IsEnable)
            {
                LogAppendDelegate la = new LogAppendDelegate(LogAppend);
                la.Invoke(RtbTxt, c._c, DateTime.Now.ToString("HH:mm:ss ") + text);
            }
        }
        #endregion
    }
}
