﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote.UserControls
{
    public partial class ButtonEX : UserControl
    {
        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        public event EventHandler ButtonClick;

        /// <summary>
        /// 控件的默认图片
        /// </summary>
        private Image _imageDefault = null;

        [Description("控件的默认图片")]
        public Image ImageDefault
        {
            get { return _imageDefault; }
            set
            {
                _imageDefault = value;
                labelEX.Image = _imageDefault;
            }
        }
        /// <summary>
        /// 光标移动到控件上方显示的图片
        /// </summary>
        private Image _imageMove = null;
        [Description("光标移动到控件上方显示的图片")]
        public Image ImageMove
        {
            get { return _imageMove; }
            set { _imageMove = value; }
        }
        /// <summary>
        /// 光标离开控件显示的图片
        /// </summary>
        private Image _imageLeave = null;
        [Description("光标离开控件显示的图片")]
        public Image ImageLeave
        {
            get { return _imageLeave; }
            set { _imageLeave = value; }
        }
        /// <summary>
        /// 控件的背景色
        /// </summary>
        private Color _backColorEX = Color.Transparent;

        [Description("控件的背景色")]
        public Color BackColorEX
        {
            get { return _backColorEX; }
            set
            {
                _backColorEX = value;
                labelEX.BackColor = _backColorEX;
            }
        }
        
        private Color backColorEnter;
        public Color BackColorEnter
        {
            get{return backColorEnter;}
            set{backColorEnter = value;}
        }

        public ButtonType buttonTypes=ButtonType.Min;
        public ButtonType ButtonTypes
        {
            get { return buttonTypes; }
            set { buttonTypes = value; }
        }
       
        /// <summary>
        /// 鼠标移动到控件上方显示的颜色
        /// </summary>
        private Color backColorMove = Color.Transparent;
        [Description("鼠标移动到控件上方显示的颜色")]
        public Color BackColorMove
        {
            get { return backColorMove; }
            set { backColorMove = value; }
        }
        
        /// <summary>
        /// 鼠标离开控件显示的背景色
        /// </summary>
        private Color backColorLeave = Color.Transparent;
        [Description("鼠标离开控件显示的背景色")]
        public Color BackColorLeave
        {
            get { return backColorLeave; }
            set { backColorLeave = value; }
        }
       
        /// <summary>
        /// 控件的文字显示
        /// </summary>
        private string textEX = "";
        [Description("显示的文字")]
        public string TextEX
        {
            get { return textEX; }
            set
            {
                textEX = value;
                labelEX.Text = textEX;
            }
        }
        
        /// <summary>
        /// 文字的颜色
        /// </summary>
        private Color textColor = Color.Black;
        [Description("文字的颜色")]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                labelEX.ForeColor = textColor;
            }
        }
       
        /// <summary>
        /// 用于显示文本的字体
        /// </summary>
        private Font fontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [Description("用于显示文本的字体")]
        public Font FontM
        {
            get { return fontM; }
            set
            {
                fontM = value;
                labelEX.Font = fontM;
            }
        }

        private ContentAlignment labelEXTextAlign = ContentAlignment.MiddleCenter;
        /*
        * ============================================================
        * 函数名：LabelEXTextAlign
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：文本位置
        * ============================================================
        */
        public ContentAlignment LabelEXTextAlign
        {
            get
            {
                return labelEXTextAlign;
            }

            set
            {
                labelEXTextAlign = value;
                labelEX.TextAlign = labelEXTextAlign;
            }
        }
        private ContentAlignment labelEXImageAlign= ContentAlignment.MiddleCenter;

        /*
        * ============================================================
        * 函数名：LabelEXTextAlign
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：图片位置
        * ============================================================
        */
        public ContentAlignment LabelEXImageAlign {
            get
            {
                return labelEXImageAlign;
            }

            set
            {
                labelEXImageAlign = value;
                labelEX.ImageAlign = labelEXImageAlign;
            }
        }





        public ButtonEX()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(sender, e);
            }
        }

        /// <summary>
        /// 鼠标移动到控件上显示的背景色和背景图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_MouseMove(object sender, MouseEventArgs e)
        {
            if (backColorMove != Color.Transparent)
            {
                if (ButtonTypes == ButtonType.Min)
                {
                    BackColorEX = Color.White;
                }
                else {
                    BackColorEX = backColorEnter;
                }
                    

            }
            if (_imageMove != null)
            {
                _imageDefault = _imageMove;
            }
            this.Invalidate();
        }

        /// <summary>
        /// 鼠标离开控件后显示的背景色和背景图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_MouseLeave(object sender, EventArgs e)
        {
            if (backColorLeave != Color.Transparent)
            {
                BackColorEX = backColorLeave;
            }
            if (_imageLeave != null)
            {
                _imageDefault = _imageLeave;
            }
            this.Invalidate();
        }


        /*
        * ============================================================
        * 枚举：ButtonType
        * 作者：木子杨
        * 版本：1.0
        * 日期：
        * 描述：按钮类型
        * ============================================================
        */
        public enum ButtonType {
            Min,
            Max
        }
    }
}