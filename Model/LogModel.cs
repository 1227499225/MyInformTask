using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LogModel
    {
        /// <summary>
        /// 返回给接口使用方
        /// </summary>
        public ResMsg resMsg { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 接口XML数据
        /// </summary>
        public string XmlStr { get; set; }
        /// <summary>
        /// 执行szSQL
        /// </summary>
        public string szSQL { get; set; }
        /// <summary>
        /// 用户自定义信息
        /// </summary>
        public string szStr { get; set; } = string.Empty;

        /// <summary>
        /// 报错等级
        /// </summary>
        public MessageLevel Erlv { get; set; } = MessageLevel.LogNormal;
        /// <summary>
        /// 接口服务自身报错信息
        /// </summary>
        public Exception Erorr { get; set; }
    }

    /// <summary>
    /// 返回接口使用方
    /// </summary>
    [Serializable]
    public class ResMsg
    {
        /// <summary>
        /// 编号
        /// </summary>
        public MessageLevel MsgCode { get; set; } = MessageLevel.LogNormal;
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; } = "正常";

    }
}
