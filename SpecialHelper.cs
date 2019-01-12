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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PublicHelper
{

    /// <summary>
    /// 特殊辅助类
    /// </summary>
    public class SpecialHelper
    {
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
        public static object[] CreateTableSql<T>(T m)
        {
            //CREATE TABLE t1(id varchar(4), score int)
            Type type = typeof(T);
            PropertyInfo[] propertyArray = type.GetProperties();
            propertyArray = GetPropertyArray(propertyArray);

            return new object[] { };
        }

        public static object[] CreateInserSql<T>(T m)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyArray = type.GetProperties();
            propertyArray = GetPropertyArray(propertyArray);
            object[] attributes = typeof(T).GetCustomAttributes(typeof(Model.FiledAttribute), true);
            Model._MoAttribute attributess =  attributes.Where(p => p.GetType().Name.ToString() == "_MoAttribute")  as Model._MoAttribute;
            string StrTableName = attributess.TableName;
            string[] strSqlNames = propertyArray.Select(p => $"[{p.Name}]").ToArray();
            string strSqlName = string.Join(",", strSqlNames);
            string[] strSqlValues = propertyArray.Select(P => $"@{P.Name}").ToArray();
            string strSqlValue = string.Join(",", strSqlValues);
            string szSQL = "INSERT INTO {0} ({1}) VALUES (@strSqlValue)".Fill(StrTableName, strSqlName);
            string SqlVal = string.Join("','", propertyArray.Select(P => $"{P.GetValue(m, null)}").ToArray());
            SqlParameter[] para = propertyArray.Select(p => new SqlParameter($"@{p.Name}", p.GetValue(m, null))).ToArray();
            return new object[] { szSQL, SqlVal, strSqlValue, para };
        }

        public static void IsFileValObjExist<T>(T m, ref LogModel log)
        {
            try
            {
                object[] attributes = typeof(T).GetCustomAttributes(typeof(Model.FiledAttribute), true);
                string SearchTableName = (attributes.Where(p => p.GetType().Name.ToString() == "_MoAttribute") as Model._MoAttribute).TableName;

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


                DataTable dt = SqliteDBHelper.Query_dt(szSQL);
                if (!dt.DtisNull())
                {
                    log.resMsg = new ResMsg { MsgCode = MessageLevel.LogWarning, Message = strWhere.Replace("AND", ",字段:") + ",该条数据已存在！" };
                }
                else
                {
                    log.resMsg = new ResMsg { MsgCode = MessageLevel.LogNormal };
                }
            }
            catch (Exception ex)
            {
                log.Erlv = MessageLevel.LogError; //ErorrLevel.ErLv03;
                log.Erorr = ex;
            }
        }

    }
    #region 实体类校验
    public class ValidatetionHelper
    {
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

    /// <summary>
    /// 相关SQL语句整理
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 相关SQL
        /// </summary>
        public static Dictionary<int, string> TaskSqlDic = new Dictionary<int, string>() {
            //相关SQL
            {1,"" },
        };

    }

    public class _EmailHelper
    {
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

    public class EmailTemplateHelper
    {
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

}
