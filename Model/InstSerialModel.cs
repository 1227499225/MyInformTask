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
    /// 编号实体类
    /// </summary>    
    [Serializable]
    [Description("编号实体类")]
    [_Mo(TableName = "InstSerial")]
    public class InstSerialModel
    {
        [Description("编号")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Description("分类")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string Type { get; set; }

        [Description("规则")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string Prefix { get; set; }

        [Description("排序号")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public int Number { get; set; } = 1;

        [Description("大类号")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string EnterpriseId { get; set; }
    }
}
