//using EmailTask;
using EmailTask;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PublicHelper
{
    /// <summary>
    /// 开发者自定义特殊辅助类
    /// </summary>
    public class SpecialHelper
    {
        /// <summary>
        /// 返回某列值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Field"></param>
        /// <returns></returns>
        public static string[] GetTableFieldVal(ref DataTable dt, string Field)
        {
            string[] arrayVal = null;
            arrayVal = dt.AsEnumerable()
                              .Select(
                                      t => t.Field<string>
                                      (Field).ToString()
                                      ).ToArray();
            if (arrayVal.Count() == 0)
            {
                arrayVal[0] = null;
            }
            return arrayVal;
        }

        /// <summary>
        /// 利用反射获取实体类字段自定义属性
        /// </summary>
        /// <param name="propertyArray"></param>
        /// <returns></returns>
        private static PropertyInfo[] GetPropertyArray(PropertyInfo[] propertyArray)
        {
            foreach (PropertyInfo prop in propertyArray)
            {
                if (prop.IsDefined(typeof(Model.FiledAttribute), true))
                {
                    Model.FiledAttribute attribute = (Model.FiledAttribute)prop.GetCustomAttributes(typeof(Model.FiledAttribute), true)[0];
                    if (!attribute.IsIncrement)
                    {
                        propertyArray = propertyArray.Where(p => !p.Name.Equals(prop.Name)).ToArray();
                    }
                }
                else
                {
                    propertyArray = propertyArray.Where(p => !p.Name.Equals(prop.Name)).ToArray();
                }
            }

            return propertyArray;
        }

        /// <summary>
        /// 创建数据库Table的SQL语句
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="m"></param>
        /// <returns></returns>
        public static object[] CreateTableSql<T>(T m,out DateBaseLocation _dbl)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyArray = type.GetProperties();
            propertyArray = GetPropertyArray(propertyArray);

            Model._MoAttribute _mo = (typeof(T).GetCustomAttributes(typeof(Model._MoAttribute), true))[0] as Model._MoAttribute;
            string StrTableName = _mo.TableName, DataSoureName = _mo.DataSoureName;
            _dbl = _mo._dbl;


            string[] StrSqlNames = propertyArray.Select(p => $"{p.Name},{p.PropertyType.Name}").ToArray();
            string Str = null;
            foreach (string item in StrSqlNames)
            {
                string[] _s = item.Split(',');
                string a = _s[0], b = _s[1];
                string MaxLength = "100", MinLength = "100";
                PropertyInfo[] prop = propertyArray.Where(p => p.Name.Equals(a)).ToArray();
                if (prop.Count() > 0)
                {
                    object[] obj = prop[0].GetCustomAttributes(typeof(RangeAttribute), true);
                    if (obj.Count() > 0)
                    {
                        RangeAttribute range = obj[0] as RangeAttribute;
                        MaxLength = range.Maximum.ToString();
                        MinLength = range.Minimum.ToString();
                    }
                }
                switch (b.ToLower())
                {
                    case "string":
                        b = "NVARCHAR({0})".Fill(MaxLength);
                        break;
                    case "datetime": b = "DateTime"; break;
                    case "int": b = "Int"; break;
                }
                if (Str.StrIsNull())
                    Str = a + " " + b;
                else
                    Str += "," + a + " " + b;
            }
            string szSQL = "CREATE TABLE {0}({1})".Fill(StrTableName, Str);

            return new object[] { DataSoureName, szSQL };
        }

        /// <summary>
        /// 创建Insert语句 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <returns></returns>
        public static object[] CreateInserSql<T>(T m)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyArray = type.GetProperties();
            propertyArray = GetPropertyArray(propertyArray);

            object[] attributes = typeof(T).GetCustomAttributes(typeof(Model._MoAttribute), true);
            string StrTableName = (attributes[0] as Model._MoAttribute).TableName;

            string[] strSqlNames = propertyArray.Select(p => $"[{p.Name}]").ToArray();
            string strSqlName = string.Join(",", strSqlNames);
            string[] strSqlValues = propertyArray.Select(P => $"@{P.Name}").ToArray();
            string strSqlValue = string.Join(",", strSqlValues);
            string szSQL = "INSERT INTO {0} ({1}) VALUES (@strSqlValue)".Fill(StrTableName, strSqlName);
            string SqlVal = string.Join("','", propertyArray.Select(P => $"{P.GetValue(m, null)}").ToArray());
            SqlParameter[] para = propertyArray.Select(p => new SqlParameter($"@{p.Name}", p.GetValue(m, null))).ToArray();
            return new object[] { szSQL, SqlVal, strSqlValue, para };
        }

        /// <summary>
        /// 判断字段值是否已存在于数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="log"></param>
        public static void IsFileValObjExist<T>(T m, ref LogModel log,string ResFieldVal=null)
        {
            try
            {
                object[] attributes = typeof(T).GetCustomAttributes(typeof(Model._MoAttribute), true);
                string SearchTableName = (attributes[0] as Model._MoAttribute).TableName,
                    DataSoureName = (attributes[0] as Model._MoAttribute).DataSoureName;

                string strWhere = string.Empty;
                Type type = typeof(T);
                PropertyInfo[] propertyArray = type.GetProperties();
                foreach (PropertyInfo prop in propertyArray)
                {
                    if (prop.IsDefined(typeof(Model.FiledAttribute), true))
                    {
                        Model.FiledAttribute attribute = (Model.FiledAttribute)prop.GetCustomAttributes(typeof(Model.FiledAttribute), true)[0];
                        if (attribute.JudgeDatabaseIsExist)
                        {
                            strWhere += " AND " + prop.Name + "=" + "'" + prop.GetValue(m) + "'";
                        }
                    }
                }
                string szSQL = "SELECT 1 FROM {0} WHERE  IsDelete='0' {1}".Fill(SearchTableName, strWhere);
                if(!ResFieldVal.StrIsNull())
                    szSQL = "SELECT "+ ResFieldVal + " FROM {0} WHERE  IsDelete='0' {1}".Fill(SearchTableName, strWhere);
                DataTable dt = SqliteDBHelper.Query_dt(szSQL, DataSoureName);
                if (!dt.DtisNull())
                {
                    if (ResFieldVal.StrIsNull())
                        log.resMsg = new ResMsg { MsgCode = MessageLevel.LogWarning, Message = strWhere.Replace("AND", ",字段:") + ",该条数据已存在！" };
                    else
                        log.szStr = dt.Rows[0]["" + ResFieldVal + ""].ToString();
                }
                else
                {
                    log.resMsg = new ResMsg { MsgCode = MessageLevel.LogNormal };
                }
            }
            catch (Exception ex)
            {
                log.Erlv = MessageLevel.LogError;
                log.Erorr = ex;
            }
        }

        #region 实体类校验
        /// <summary>
        /// 实体类校验
        /// </summary>
        public class ValidatetionHelper
        {
            /// <summary>
            /// 校验
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static ValidResult IsValid(object value)
            {
                ValidResult result = new ValidResult();
                try
                {
                    var validationContext = new ValidationContext(value);
                    var results = new List<ValidationResult>();
                    var isValid = Validator.TryValidateObject(value, validationContext, results, true);

                    if (!isValid)
                    {
                        result.IsVaild = false;
                        result.ErrorMembers = new List<ErrorMember>();
                        foreach (var item in results)
                        {
                            result.ErrorMembers.Add(new ErrorMember()
                            {
                                ErrorMessage = item.ErrorMessage,
                                ErrorMemberName = item.MemberNames.FirstOrDefault()
                            });
                        }
                    }
                    else
                    {
                        result.IsVaild = true;
                    }
                }
                catch (Exception ex)
                {
                    result.IsVaild = false;
                    result.ErrorMembers = new List<ErrorMember>();
                    result.ErrorMembers.Add(new ErrorMember()
                    {
                        ErrorMessage = ex.Message,
                        ErrorMemberName = "Internal error"
                    });
                }

                return result;
            }
        }
        public class ValidResult
        {
            public List<ErrorMember> ErrorMembers { get; set; }
            public bool IsVaild { get; set; }
        }

        public class ErrorMember
        {
            public string ErrorMessage { get; set; }
            public string ErrorMemberName { get; set; }
        }
        #endregion


        public class EnumHelper
        {
            /// <summary>
            /// 获取枚举属性
            /// </summary>
            /// <param name="Field"></param>
            /// <returns></returns>
            public static string GetEnumDescription<T>(T Field)
            {
                string value = Field.ToString();
                FieldInfo field = Field.GetType().GetField(value);
                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                    return value;
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                return descriptionAttribute.Description;
            }
        }

        #region 相关SQL配置
        /// <summary>
        /// 相关SQL语句整理
        /// </summary>
        public class SqlHelper
        {

            /*
            * ============================================================
            * 函数名：TaskSqlDics
            * 作者：木子杨
            * 版本：1.0
            * 日期：
            * 描述：//相关SQ
            * ============================================================
            */
            private static  Dictionary<string, string> TaskSqlDics = new Dictionary<string, string>() {
                    //ClientUserInfo
                    {"V01001","" },
                    {"V01002","" },
                    {"V01003","" },
                    {"V01004","" },

                    {"V02001","" },
                    {"V02002","" },
                    {"V02003","" },
                    {"V02004","" },

                    {"V03001","" },
                    {"V03002","" },
                    {"V03003","" },
                    {"V04004","" },

                };

            /*
            * ============================================================
            * 函数名：Name
            * 作者：木子杨
            * 版本：1.0
            * 日期：
            * 描述：相关SQL
            * ============================================================
            */
            public static string TaskSqlDic(string SqlCode) {
                return TaskSqlDics[SqlCode];
            }
        }
        #endregion

        #region 邮件
        /// <summary>
        /// 邮件相关
        /// </summary>
        public class EmailTemplateHelper
        {
            /// <summary>
            /// 根据邮件模板，对应字段赋值
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="m"></param>
            /// <param name="EmailTemplate">模板字符串</param>
            public static void EmailTemplate<T>(T m, ref string EmailTemplate)
            {
                string _l = "<%{0}%>";
                Type type = typeof(T);
                PropertyInfo[] propertyArray = type.GetProperties();
                foreach (PropertyInfo prop in propertyArray)
                {
                    string _Name = string.Format(_l, prop.Name);
                    if (EmailTemplate.Contains(_Name))
                        EmailTemplate = Regex.Replace(EmailTemplate, @"\" + _Name + "", prop.GetValue(m).ToString());
                }

            }
        }

        /// <summary>
        /// 邮件辅助类
        /// </summary>
        public class _EmailHelper
        {
            /// <summary>
            /// 发送邮件
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="m">实体类</param>
            /// <param name="Sn">编号</param>
            /// <param name="log">执行日志</param>
            /// <param name="En">模板名称(无后缀)</param>
            /// <param name="Subject">邮件主题</param>
            public static void SendEmail<T>(T m, string Sn, ref LogModel log, EnumBase.EmailTemplateEn En, string Subject)
            {
                try
                {
                    //string szSQL = SqlHelper.TicketSqlDic[1];
                    DataTable dt = null; //DBHelper.RunDataTableSQL(string.Format(szSQL, Sn));

                    string userEmail = string.Empty, userName = string.Empty;
                    if (!dt.DtisNull())
                    {
                        userEmail = dt.Rows[0]["EmailAddress"].ToString();
                        userName = dt.Rows[0]["UserName"].ToString();
                    }


                    #region 邮件发送参数配置 测试阶段负责人将接收测试提醒  内置发件账户：杨旭东
                    //To
                    List<SendUserInfo> ToUserInfo = new List<SendUserInfo>() {
                        new SendUserInfo() { EmailAddress=userEmail,DisPlayName=userName,DisPlayNameEncoding=System.Text.Encoding.UTF8
                       },
                    };
                    //Cc
                    //Bcc


                    bool isOpenExternalAccount = true; //(System.Configuration.ConfigurationManager.AppSettings["isOpenExternalAccount"] == "false" ? false : true);

                    string BodyTextConext = null;//LogToTXT.FileRead(EnumHelper.GetEnumDescription<EnumBase.PaymentScheduleNode>(En));
                    BodyTextConext = BodyTextConext.Replace("<%userName%>", userName);
                    switch ((int)En)
                    {
                        case 1://付款
                               //TicketInfoModel tic = m as TicketInfoModel;
                               //EmailTemplateHelper.EmailTemplate<TicketInfoModel>(tic, ref BodyTextConext);
                            break;
                        case 2://验收
                               //CheckAndAcceptModel CheckAndAccept = m as CheckAndAcceptModel;
                               //EmailTemplateHelper.EmailTemplate<CheckAndAcceptModel>(CheckAndAccept, ref BodyTextConext);
                            break;
                    }

                    //EmailTemplateHelper.EmailTemplate<LogModel>(log, ref BodyTextConext);
                    EmailSendInfo em = new EmailSendInfo()
                    {
                        IsLoadConfigs = isOpenExternalAccount,
                        ToUserInfo = ToUserInfo,
                        MailPriority = System.Net.Mail.MailPriority.High,
                        Subject = Subject,
                        BodyText = BodyTextConext
                    };
                    EmailHelper eh = new EmailHelper(em);
                    if (!eh.Res)
                    {
                        log.szStr += "/n/r 邮件错误：" + eh.Msg;
                    }
                    else
                    {
                        log.szStr += "/n/r邮件发送成功！";
                    }
                }
                catch (Exception ex)
                {

                }
                #endregion
            }
        }
        #endregion


        }
}
