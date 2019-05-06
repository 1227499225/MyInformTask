using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    [Description("表关系实体类")]
    [_Mo(TableName = "BaseTabelRelationship")]
    public class BaseTabelRelationshipModel: _BaseModel
    {
        [Description("表名")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true,JudgeDatabaseIsExist =true)]
        public string LangName { get; set; }

        [Description("别名")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true, JudgeDatabaseIsExist = true)]
        public string Code { get; set; }

        [Description("表类型")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string Type { get; set; }//业务/基础

        [Description("父级编号")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string ParentId { get; set; } = "0";//0为顶级
    }
}
