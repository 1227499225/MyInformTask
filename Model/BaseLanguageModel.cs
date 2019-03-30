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
    [Description("系统存储语言切换")]
    [_Mo(TableName = "BaseLanguage")]
    public class BaseLanguageModel:_BaseModel
    {
        [Description("语种key")]
        [Range(36, 36)]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true,JudgeDatabaseIsExist =true)]
        public string LangName { get; set; }

        [Description("语种类型")]
        [Range(2,10)]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string LangNameType { get; set; }

        [Description("描述")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string Value { get; set; }
    }
}
