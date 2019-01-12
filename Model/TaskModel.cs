using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 任务实体类
    /// </summary>    
    [Serializable]
    [Description("任务实体类")]
    [_Mo(TableName = "InstTask")]
    public class TaskModel: _BaseDetailModel
    {
        [Range(1, 30)]
        [Description("标题")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string Title { get; set; } = string.Empty;

        [Description("单号")]
        [Range(10, 26)]
        [Filed(IsIncrement = true)]
        public string SnNumber { get; set; } = string.Empty;

        /// <summary>
        /// 创建者
        /// </summary>
        [Filed(IsIncrement = true)]
        public string CreatorId { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Filed(IsIncrement = true)]
        public string CreateTime { get; set; } = DateTime.Now.ToString();

        /// <summary>
        /// 状态
        /// </summary>
        [Filed(IsIncrement = true)]
        public int State { get; set; } = (int)StateLevel.LvZero;

    }
}
