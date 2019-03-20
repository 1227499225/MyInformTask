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
    /// 任务加密
    /// </summary>   
    [Serializable]
    [Description("任务加密")]
    [_Mo(TableName = "InstTaskEncryption")]
    public class InstTaskEncryptionModel: BaseModel
    {
        [Range(1, 30)]
        [Description("任务查看密码")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string TaskQueryPwd { get; set; }

        [Range(1, 30)]
        [Description("任务编辑密码")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string TaskEditPwd { get; set; }

    }
}
