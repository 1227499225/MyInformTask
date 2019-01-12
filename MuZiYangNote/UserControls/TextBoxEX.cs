using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote.UserControls
{
    class TextBoxEX : TextBox
    {

        /*
        * ============================================================
        * 函数名：WndProc
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：重绘文本框
        * ============================================================
        */
        protected override void WndProc(ref Message m)
        {
            #region
            int WM_CHAR = 0x0102;//此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息
            if (m.Msg == WM_CHAR)
            {
             
            }
            #endregion
            this.Height = this.TextBoxHeight;
            this.AutoSize = this.textBoxAutoSize;
            #region 重绘控件
            int WM_PAINT = 0x000F;//要求一个窗体重画自己
            int WM_NCPAINT = 0x0085;// 程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时
            base.WndProc(ref m);
            this.ControlTypeText = TextBoxType.String;
            this.BorderStyle = BorderStyle.FixedSingle;
            if (m.Msg == WM_PAINT || m.Msg == WM_NCPAINT)
            {
                if (this.BorderWeight % 2 == 0)
                {
                    this.BorderWeight -= 1;
                }
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    using (Pen pen = new Pen(this.BorderColor, this.BorderWeight))
                    {
                        g.DrawRectangle(pen, 0, 0, Size.Width - 1, Size.Height - 1);
                    }
                }
                WmPaint();
            }
            #endregion
        }

        /*
        * ============================================================
        * 函数名：WmPaint
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：WmPaint
        * ============================================================
        */
        private void WmPaint()
        {
            using (Graphics graphics = Graphics.FromHwnd(base.Handle))
            {
                if (Text.Length == 0 && !string.IsNullOrEmpty(this.WaterMarkText) && !Focused)
                {
                    TextFormatFlags format = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        format |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
                    }
                    TextRenderer.DrawText(graphics, this.WaterMarkText, this.WaterMarkFont, base.ClientRectangle, this.WaterMarkTextColor, format);

                }
            }
        }
        #region 自定义属性
        private bool textBoxAutoSize;
        /*
        * ============================================================
        * 函数名：TextBoxAutoSize
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：
        * ============================================================
        */
        public bool TextBoxAutoSize
        {
            get
            {
                return textBoxAutoSize;
            }

            set
            {
                textBoxAutoSize = value;
            }
        }

        private int textBoxHeight;
        /*
        * ============================================================
        * 函数名：TextBoxHeight
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：高度
        * ============================================================
        */
        public int TextBoxHeight
        {
            get
            {
                return textBoxHeight;
            }

            set
            {
                textBoxHeight = value;
            }
        }

        private TextBoxType controlTypeText;
        /*
        * ============================================================
        * 函数名：ControlTypeText
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：控件类型 --根具不同类型做判断---待做
        * ============================================================
        */
        public TextBoxType ControlTypeText
        {
            get
            {
                return controlTypeText;
            }

            set
            {
                controlTypeText = value;
            }
        }

        private int borderWeight;
        /*
        * ============================================================
        * 函数名：BorderWeight
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：边框宽度
        * ============================================================
        */
        public int BorderWeight
        {
            get{return borderWeight; }
            set{borderWeight = value;}
        }

        private Color borderColor;
        /*
        * ============================================================
        * 函数名：borderColor
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：文本边框颜色
        * ============================================================
        */
        public Color BorderColor
        {
            get{ return borderColor; }
            set{borderColor = value;}
        }

        private string _waterMarkText;
        /*
        * ============================================================
        * 函数名：WaterMarkText
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：水标文本
        * ============================================================
        */
        public string WaterMarkText
        {
            get { return _waterMarkText; }
            set { _waterMarkText = value; }
        }
        
        private Font _waterMarkFont;
        /*
* ============================================================
* 函数名：_waterMarkFont
* 作者：木子杨
* 版本：1.0
* 日期：
* 描述：水印格式
* ============================================================
*/
        public Font WaterMarkFont
        {
            get { return _waterMarkFont; }
            set { _waterMarkFont = value; }
        }

        private Color _waterMarkTextColor;
        /*
        * ============================================================
        * 函数名：WaterMarkTextColor
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：水印字体颜色
        * ============================================================
        */
        public Color WaterMarkTextColor
        {
            get{ return _waterMarkTextColor; }
            set { _waterMarkTextColor = value;}
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// 自定义控件类型
        /// </summary>
        public enum TextBoxType
        {
            //数字
            Number,
            //文本
            String,
            //电话
            Phone,
            //邮箱
            Email,
        }

    }
}
//引用案例https://www.cnblogs.com/xiaofengfeng/p/3488574.html