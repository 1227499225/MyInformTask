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

        //获取Rect消息
        private const int EM_GETRECT = 178;
        //设置Rect消息
        private const int EM_SETRECT = 179;
        //绘制消息
        private const int WM_PAINT = 0xF;
        public TextBoxEX()
        {
            _textMargin = new Padding(1);
            //不允许回国
            AllowReturn = false;
            //禁止折行
            WordWrap = false;

           
        }
        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, ref Rectangle lParam);

        /*
        * ============================================================
        * 函数名：OnSizeChanged
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：尺寸变化时重新设置字体的显示位置居中
        * ============================================================
        */
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetTextDispLayout();
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            SetTextDispLayout();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetTextDispLayout();
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            SetTextDispLayout();
        }
        /*
        * ============================================================
        * 函数名：SetTextDispLayout
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：设置文本显示布局位置
        * ============================================================
        */
        public void SetTextDispLayout()
        {
            if (this.Text == "")
                return;
            Rectangle rect = new Rectangle();
            SendMessage(this.Handle, 0, (IntPtr)0, ref rect);
            SizeF size = CreateGraphics().MeasureString(Text, Font);
            rect.Y = (int)(Height - size.Height) / 2 + TextMargin.Top;
            rect.X = 1 + TextMargin.Left;
            rect.Height = Height - 2;
            rect.Width = Width - TextMargin.Right - TextMargin.Left - 2;
            SendMessage(this.Handle, EM_SETRECT, IntPtr.Zero, ref rect);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //如果不允许回车 屏蔽回车 换行键值
            if (!AllowReturn
                && ((int)e.KeyChar == (int)Keys.Return || (int)e.KeyChar == (int)Keys.LineFeed))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

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

        /*
        * ============================================================
        * 函数名：AllowReturn
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：是否允许有回车
        * ============================================================
        */
        public bool AllowReturn { get; set; }

        private Padding _textMargin;

        /*
        * ============================================================
        * 函数名：TextMargin
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：Text Padding值
        * ============================================================
        */
        public Padding TextMargin { get { return _textMargin; } set { _textMargin = value; SetTextDispLayout(); } }

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
            get { return borderWeight; }
            set { borderWeight = value; }
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
            get { return borderColor; }
            set { borderColor = value; }
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
            get { return _waterMarkTextColor; }
            set { _waterMarkTextColor = value; }
        }

        #endregion



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