using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using PublicHelper;

namespace MuZiYangNote.UserControls
{
    public partial class BaseUserControl : UserControl
    {
        public BaseUserControl()
        {
            InitializeComponent();
        }
        #region 自定义事件参数类型，根据需要可设定多种参数便于传递
        //声名委托
        public delegate void DataChangeHandler(object sender, BaseEv.DataChangeEventArgs args);
        // 声明事件
        public event DataChangeHandler DataChange;
        // 调用事件函数

        public void OnDataChange(BaseEv.DataChangeEventArgs args)
        {
            if (DataChange != null)
            {
                DataChange(this, args);
            }
        }
        #endregion


    }
}
