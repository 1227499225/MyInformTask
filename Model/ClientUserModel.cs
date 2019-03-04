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
    [Description("本地用户表")]
    [_Mo(_dbl =DateBaseLocation._LOCAL, TableName = "ClientUserInfo")]
    public class ClientUserModel: _BaseDetailModel
    {
        [Range(1, 12)]
        [Description("用户姓名")]
        [Filed(IsIncrement =true,JudgeDatabaseIsExist =true)]
        public string ClientUserName { get; set; }

        [Range(1, 12)]
        [Description("用户昵称")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string ClientUserNickname { get; set; }

        [Description("用户Code")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement = true)]
        public string ClientUserId { get; set; }

        [Range(1, 16)]
        [Description("用户本地密码")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [Filed(IsIncrement=true,JudgeDatabaseIsExist =true)]
        public string ClientUserPwd { get; set; }
    }
}
