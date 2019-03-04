using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    public class BaseModel {

        [Description("基础id")]
        [Filed(IsIncrement = true)]
        public string Id { get; set; } = "{1}";
        /// <summary>
        /// 任务Id--关联所有
        /// </summary>
        [Range(36,36)]
        [Description("任务Id")]
        [Filed(IsIncrement = true)]
        public string TaskId { get; set; } = "{0}";
        
    }
    public class _BaseModel
    {

    }
    public class _BaseDetailModel : BaseModel
    {
        /// <summary>
        /// 备注描述
        /// </summary>
        [Range(1, 2500)]
        [Description("备注描述")]
        [Filed(IsIncrement = true)]
        public string Describe { get; set; } = string.Empty;

        /// <summary>
        /// 被修改行编号
        /// </summary>
        [Filed(IsIncrement = true)]
        public string OriginalContent { get; set; } = string.Empty;
        /// <summary>
        /// 修改内容备注
        /// </summary>
        [Range(1, 2500)]
        [Filed(IsIncrement = true)]
        [Description("修改内容备注")]
        [Required(ErrorMessage = "{0}不能为空！")]
        public string ModifyContent { get; set; } = "|-|";

        /// <summary>
        /// 上次修改时间 主/明细表都需要
        /// </summary>
        [Filed(IsIncrement = true)]
        public DateTime LastModifiedTime { get; set; }

        [Filed(IsIncrement = true)]
        public int IsDelete { get; set; } = 0;
    }
    public class BaseDetailModel : _BaseDetailModel
    {
        /// <summary>
        /// 排序行号
        /// </summary>
        [Filed(IsIncrement = true)]
        public int GridOrder { get; set; } = 0;
    }
    /// <summary>
    /// 主表
    /// </summary>
    public class BaseZhModel : BaseDetailModel
    {
        [Description("便签类型")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = false)]
        public NoteType NotesType = NoteType.GeneralNote;

        [Description("单号")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string SnNumber { get; set; } = string.Empty;

        [Description("主题")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// 便签内容
        /// </summary>
        [Range(1, 2500)]
        [Description("便签内容")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true, JudgeDatabaseIsExist = true)]
        public string NoteContent { get; set; } = string.Empty;
    }




    /// <summary>
    /// 字段属性
    /// </summary>
    public class FiledAttribute : Attribute {
        /// <summary>
        /// 是否添加到数据库
        /// </summary>
        public bool IsIncrement { get; set; }

        /// <summary>
        /// 判断是否已存在
        /// </summary>
        public bool JudgeDatabaseIsExist { get; set; } = false;

    }


    /*
    * ============================================================
    * 函数名：_Mo
    * 作者：木子杨
    * 版本：1.0
    * 日期：
    * 描述：本地分库属性
    * ============================================================
    */
    public class _MoAttribute : Attribute
    {
        public string DataSoureName = SystemVariateBase.DateBaseClient;

        public string TableName { get; set; }
        
        /// <summary>
        /// 本地还是服务器存储
        /// </summary>
        public DateBaseLocation _dbl = DateBaseLocation._SERVER;
    }


    public class RequiresAttribute : RequiredAttribute
    {


    }

}