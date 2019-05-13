//using EmailTask;
using EmailTask;
using Microsoft.Win32;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Caching;
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
        public static PropertyInfo[] GetPropertyArray(PropertyInfo[] propertyArray)
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
        public static object[] CreateTableSql<T>(T m, out DateBaseLocation _dbl)
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

            return new object[] { DataSoureName, StrTableName, szSQL };
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
            string StrTableName = (attributes[0] as Model._MoAttribute).TableName,
                    DataSoureName = (attributes[0] as Model._MoAttribute).DataSoureName;

            string[] strSqlNames = propertyArray.Select(p => $"[{p.Name}]").ToArray();
            string strSqlName = string.Join(",", strSqlNames);
            string[] strSqlValues = propertyArray.Select(P => $"@{P.Name}").ToArray();
            string strSqlValue = string.Join(",", strSqlValues);
            string szSQL = "INSERT INTO {0} ({1}) VALUES (@strSqlValue)".Fill(StrTableName, strSqlName);
            string SqlVal = "'" + string.Join("','", propertyArray.Select(P => $"{P.GetValue(m, null)}").ToArray()) + "'";
            SqlParameter[] para = propertyArray.Select(p => new SqlParameter($"@{p.Name}", p.GetValue(m, null))).ToArray();
            return new object[] { szSQL, SqlVal, strSqlValue, para, DataSoureName };
        }

        /// <summary>
        /// 创建修改语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <returns></returns>
        public static object[] CreateUpdateSql<T>(T m)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyArray = type.GetProperties();
            propertyArray = GetPropertyArray(propertyArray);
            object[] attributes = typeof(T).GetCustomAttributes(typeof(Model._MoAttribute), true);
            string StrTableName = (attributes[0] as Model._MoAttribute).TableName,
                    DataSoureName = (attributes[0] as Model._MoAttribute).DataSoureName;
            string UpValueStr = string.Empty, WhereStr = string.Empty;
            foreach (PropertyInfo prop in propertyArray)
            {
                if (!("id").Contains(prop.Name.ToLower()))
                {
                    if (!("taskid,snnumber").Contains(prop.Name.ToLower()))
                    {

                        if (UpValueStr.StrIsNull())
                            UpValueStr += prop.Name + "= '" + prop.GetValue(m) + "'";
                        else
                            UpValueStr += "," + prop.Name + "= '" + prop.GetValue(m) + "'";
                    }
                }
                else
                    WhereStr = " Id='" + prop.GetValue(m) + "'";
            }
            string szSQL = "UPDATE  {0} SET {1} WHERE {2}".Fill(StrTableName, UpValueStr, WhereStr);

            return new object[] { szSQL, DataSoureName };
        }

        /// <summary>
        /// 判断字段值是否已存在于数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="log"></param>
        public static void IsFileValObjExist<T>(T m, ref LogModel log, string ResFieldVal = null)
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
                if (!ResFieldVal.StrIsNull())
                    szSQL = "SELECT " + ResFieldVal + " FROM {0} WHERE  IsDelete='0' {1}".Fill(SearchTableName, strWhere);
                DataTable dt = SqliteDBHelper.Query_dt(szSQL, DataSoureName);
                if (!dt.DtisNull())
                {
                    if (ResFieldVal.StrIsNull())
                        log.resMsg = new ResMsg { MsgCode = MessageLevel.LogWarning, Message = strWhere.Replace("AND", ",字段:") + ",该条数据已存在！" };
                    else
                    {
                        if (ResFieldVal.Contains(","))
                        {
                            string[] _v = ResFieldVal.Split(',');
                            foreach (string item in _v)
                            {
                                log.szStr += "," + dt.Rows[0]["" + item + ""].ToString();
                            }
                            if ((log.szStr).StartsWith(","))
                            {
                                log.szStr = (log.szStr).Substring(1, (log.szStr).Length - 1);
                            }
                        }
                        else
                        {
                            log.szStr = dt.Rows[0]["" + ResFieldVal + ""].ToString();
                        }
                    }
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


        //ArrayList去重   
        public static ArrayList GetSingle(ArrayList list)
        {
            ArrayList newList = new ArrayList();
            foreach (Object str in list)
            {
                if (!newList.Contains(str))
                {
                    newList.Add(str);
                }
            }
            return newList;
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
            public static Dictionary<string, string> TaskSqlDics = new Dictionary<string, string>() {
                    //ClientUserInfo
                    {"V01001","SELECT * FROM ClientUserInfo WHERE Id='{0}'" },
                    {"V01002","" },
                    {"V01003","" },
                    {"V01004","" },
                    //PlainNoteInfo
                    {"V02001","SELECT Count(id) FROM PlainNote WHERE TASKID='{0}'" },//是否存在
                    {"V02002",@"SELECT a.*
                                 ,CASE IFNULL(ite.TaskEditPwd,0) WHEN 0 THEN 0 ELSE 1 END AS _ep,CASE IFNULL(ite.TaskQueryPwd,0) WHEN 0 THEN 0 ELSE 1 END _qp 
                                FROM PlainNote a 
                                INNER JOIN InstTask t on a.TaskId=t.Id
                                LEFT JOIN InstTaskEncryption ite on t.Id=ite.TaskId  where t.CreatorId='{0}'
                                order by t.CreateTime desc
                                " },//查询所有
                    {"V02003",@"select pn.* from PlainNote pn 
                                inner join insttask t on pn.TaskId=t.Id
                                where t.CreatorId='{0}' and pn.IsOpen='{1}'" },//查询是否需要打开的便签
                    {"V02004","" },

                    {"V03001","" },
                    {"V03002","" },
                    {"V03003","" },
                    {"V03004","" },
                    //InstSerial
                    {"V04001","SELECT  Number FROM InstSerial  WHERE Type='{0}' AND Prefix='{1}' ORDER BY Number DESC limit 0,1" },
                    {"V04002","" },
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
            public static string TaskSqlDic(string SqlCode)
            {
                return TaskSqlDics[SqlCode];
            }
        }
        #endregion

        #region 邮件辅助类
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

            /// <summary>
            /// 根据业务表名查询并返回模板（不支持页面Table）
            /// </summary>
            /// <param name="MainBusinessName"></param>
            /// <param name="EmailTemplate"></param>
            public static string EmailTemplate<T>(T m,  string EmailTemplate)
            {
                string StrTableName, DataSoureName, TaskId="";
                Type type = typeof(T);
                PropertyInfo[] propertyArray = type.GetProperties();
                object[] attributes = typeof(T).GetCustomAttributes(typeof(Model._MoAttribute), true);
                StrTableName = (attributes[0] as Model._MoAttribute).TableName;
                DataSoureName = (attributes[0] as Model._MoAttribute).DataSoureName;
                foreach (PropertyInfo prop in propertyArray)
                {
                    if(prop.Name.Equals("TaskId"))
                        TaskId=prop.GetValue(m).ToString();
                }
                //只支持两级查询
                string szSQL0 = @"SELECT LangName FROM BaseTabelRelationship WHERE  ParentId IN(
                       SELECT Id FROM BaseTabelRelationship WHERE LangName = '{0}'
                       )";
                string szSQL1 = "SELECT * FROM {0} WHERE TaskId='"+ TaskId + "'",
                       szSQL2 = szSQL1;
                DataSet ds = new DataSet();
                DataTable _dt1 = SqliteDBHelper.Query_dt(szSQL0.Fill(StrTableName), DataSoureName);
                DataTable _dt2 = new DataTable();
                _dt2= SqliteDBHelper.Query_dt(szSQL2.Fill(StrTableName), DataSoureName);
                _dt2.TableName = StrTableName;
                ds.Tables.Add(_dt2);
                szSQL2 = szSQL1;
                if (!_dt1.DtisNull())
                {
                    foreach (DataRow dr in _dt1.Rows)
                    {
                        _dt2 = new DataTable();
                        szSQL2 = szSQL1.Fill(dr.Field<string>("LangName").ToString());
                        _dt2 = SqliteDBHelper.Query_dt(szSQL2, DataSoureName);
                        ds.Tables.Add(_dt2);
                        ds.Tables[_dt2.TableName].TableName = dr.Field<string>("LangName").ToString();
                        szSQL2 = szSQL1;
                    }
                }
                string rex = null;
                ArrayList _ary = new ArrayList();
                //内容循环匹配
                if (EmailTemplate.ToUpper().Contains("<TABLE"))
                {
                    rex = "(?<=(ForData=\"))[.\\s\\S]*?(?=(\"))";
                    Regex regex01 = new Regex(rex, RegexOptions.IgnoreCase);
                    MatchCollection matches01 = regex01.Matches(EmailTemplate);
                    if (matches01.Count > 0)
                    {
                        bool ForData = Convert.ToBoolean(matches01[0].Value);
                        if (ForData)//存在明细填充
                        {
                            rex= "(?<=(<tr))[.\\s\\S]*?(?=(tr>))";
                             regex01 = new Regex(rex, RegexOptions.IgnoreCase);
                             matches01 = regex01.Matches(EmailTemplate);
                            if (matches01.Count > 0)
                            {
                                rex = "(?<=(>))[.\\s\\S]*?(?=(</td>))";
                                regex01 = new Regex(rex, RegexOptions.IgnoreCase);
                                matches01 = regex01.Matches(matches01[0].Value);
                                foreach (Match match in matches01)
                                {
                                    _ary.Add(match.Value);
                                }
                            }

                        }
                    }
                }
                //
                #region 方式二
                 rex = "(?<=(<%))[.\\s\\S]*?(?=(%>))";
                 _ary = new ArrayList();
                RegexOptions options = RegexOptions.IgnoreCase;//不区分大小写
                Regex regex = new Regex(rex, options);
                MatchCollection matches = regex.Matches(EmailTemplate);
                foreach (Match match in matches)
                {
                    _ary.Add(match.Value);
                }
                #endregion
                if (ds.Tables.Count == 0)
                    return EmailTemplate;
                if (_ary.Count == 0)
                    return EmailTemplate;
                string TableName, ColumnName;
                string[]    ColumnValue;
                string[] strA;
                DataTable _dt3 = new DataTable();
                foreach (string st in _ary)
                {
                    if (st.Contains("."))
                    {
                       strA = st.Split('.');
                        if (strA.Count() ==2) {
                            TableName = strA[0];
                            ColumnName = strA[1];
                            _dt3= ds.Tables[TableName];
                            if (!_dt3.DtisNull()) {
                                ColumnValue = SpecialHelper.GetTableFieldVal(ref _dt3, ColumnName);
                                if (ColumnValue.Count() > 0)
                                    EmailTemplate = Regex.Replace(EmailTemplate, @"<%{0}%>".Fill(st), ColumnValue[0], RegexOptions.IgnoreCase);
                            }
                        }
                    }
                }
                return EmailTemplate;
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
            public static void SendEmail<T>(T m,ref LogModel log, EnumBase.EmailTemplateEn En, string Subject)
            {
                try
                {
                    //string szSQL = SqlHelper.TicketSqlDic[1];
                    DataTable dt = null; //DBHelper.RunDataTableSQL(string.Format(szSQL, Sn));

                    string userEmail = "18771564243@qq.com", userName = "草鸡管理员";//string.Empty;
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
                    //string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase
                    string BodyTextConext = FileHelper.FileRead(EnumHelper.GetEnumDescription<EnumBase.EmailTemplateEn>(En));//LogToTXT.FileRead(EnumHelper.GetEnumDescription<EnumBase.PaymentScheduleNode>(En));
                    BodyTextConext = BodyTextConext.Replace("<%userName%>", userName);
                    switch ((int)En)
                    {
                        case 1://付款
                               PlainNoteModel tic = m as PlainNoteModel;
                            BodyTextConext= EmailTemplateHelper.EmailTemplate<PlainNoteModel>(tic,  BodyTextConext);
                            break;
                        case 2://验收
                               //CheckAndAcceptModel CheckAndAccept = m as CheckAndAcceptModel;
                               //EmailTemplateHelper.EmailTemplate<CheckAndAcceptModel>(CheckAndAccept, ref BodyTextConext);
                            break;
                    }
                    SetConfig sc = new SetConfig() { DenyBuiltInSetConfig = false };
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
                        log.Erlv = MessageLevel.LogError;
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



    #region DataTable转换为Model实体
    public static class DataTableToModelHelper
    {
        public static List<T> GetModelFromDB<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        /// <summary>
        /// 将DataRow转换成实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T GetItem<T>(DataRow dr)
        {
            try
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name.ToLower() == column.ColumnName.ToLower())
                        {
                            if (dr[column.ColumnName] == DBNull.Value)
                            {
                                if (column.DataType.Name == "DateTime")
                                    pro.SetValue(obj, DateTime.Now, null);
                                else
                                    pro.SetValue(obj, " ", null);
                                break;
                            }
                            else
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                                break;
                            }
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    #endregion

    #region Json与Model实体互转
    public static class JsonAndMode
    {
        /// <summary>
        /// json类和实体类之间相互转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonString"></param>
        /// <returns></returns>
        public static T Json2Class<T>(string JsonString)
        {
            return JsonConvert.DeserializeObject<T>(JsonString);
        }

        public static string Class2Json<T>(T test)
        {
            return JsonConvert.SerializeObject(test);
        }
    }
    #endregion

    #region 缓存辅助类 01
    public static class MemoryCacheHelper
    {
        //https://www.cnblogs.com/wuhuacong/p/3526335.html
        private static readonly Object _locker = new object();
        private static T GetCacheItem<T>(String key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("Invalid cache key");
            if (cachePopulate == null) throw new ArgumentNullException("cachePopulate");
            if (slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("Either a sliding expiration or absolute must be provided");

            if (MemoryCache.Default[key] == null)
            {
                lock (_locker)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        var item = new CacheItem(key, cachePopulate());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }

            return (T)MemoryCache.Default[key];
        }

        /*
        * ============================================================
        * 函数名：GetInfo
        * 作者：木子杨
        * 版本：1.0
        * 日期： 2019-03-06
        * 描述： 读取字符串
        * ============================================================
        */
        public static string GetInfo(string Key)
        {
            return (MemoryCache.Default[Key]).ToString();
        }

        /*
        * ============================================================
        * 函数名：GetInfo
        * 作者：木子杨
        * 版本：1.0
        * 日期： 2019-03-06
        * 描述： 读取json字符串缓存
        * ============================================================
        */
        public static T GetInfo<T>(string Key = null)
        {
            Key = (string.IsNullOrEmpty(Key) ? "Security_UserFullName" : Key);
            var _v = MemoryCache.Default[Key];
            if (_v != null)
                return JsonAndMode.Json2Class<T>((_v).ToString());
            else
                return default(T);
        }

        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }

        //public static string GetValues(string Key) {
        //    return MemoryCache.Default[Key] as string;
        //}
        /*
        * ============================================================
        * 函数名：GetUserFullName
        * 作者：木子杨
        * 版本：1.0
        * 日期： 2019-03-06
        * 描述： 根据用户的ID，获取用户的信息，并放到缓存里面
        * ============================================================
        */
        public static string GetUserFullName(string Key, TimeSpan _e = new TimeSpan(), string Value = null, bool IsUserInfo = false)
        {
            Key = (IsUserInfo ? "Security_UserFullName" : Key);
            if (_e == new TimeSpan())
                _e = new TimeSpan(0, 60, 0);//1小时过期
            if (IsUserInfo)
            {
                Type type = typeof(ClientUserModel);
                PropertyInfo[] propertyArray = type.GetProperties();
                propertyArray = SpecialHelper.GetPropertyArray(propertyArray);

                object[] attributes = typeof(ClientUserModel).GetCustomAttributes(typeof(Model._MoAttribute), true);
                string StrTableName = (attributes[0] as Model._MoAttribute).TableName,
                        DataSoureName = (attributes[0] as Model._MoAttribute).DataSoureName;
                Value = JsonAndMode.Class2Json<ClientUserModel>(
                    DataTableToModelHelper.GetItem<ClientUserModel>(
                        (
                           SqliteDBHelper.Query_dt
                           (
                                (
                                SpecialHelper.SqlHelper.TaskSqlDic("V01001")
                                ).Fill(Value)
                                , DataSoureName
                            )
                        ).Rows[0])
                    );
            }
            string Info = MemoryCacheHelper.GetCacheItem<string>(Key,
                delegate ()
                {
                    return Value;
                }
                , _e

                );
            return Info;
        }
    }
    #endregion

    #region 缓存辅助类 02
    /// <summary>
    /// 缓存对象数据结构 Cactus.Common https://www.cnblogs.com/RainbowInTheSky/p/5557936.html
    /// </summary>
    [Serializable()]
    public class CacheData
    {
        public object Value { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTimeOffset AbsoluteExpiration { get; set; }
        public DateTime FailureTime
        {
            get
            {
                if (AbsoluteExpiration == System.Runtime.Caching.ObjectCache.InfiniteAbsoluteExpiration)
                {
                    return AbsoluteExpiration.DateTime;
                }
                else { return CreateTime.AddTicks(AbsoluteExpiration.Ticks); }
            }
        }
        public CacheItemPriority Priority { get; set; }
    }

    /// <summary>
    /// 缓存处理类(MemoryCache)
    /// </summary>
    public class CacheHelper
    {
        //在应用程序的同级目录(主要防止外部访问)
        public static string filePath = System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.ConnectionStrings["filecache"].ConnectionString);
        //文件扩展名
        public static string fileExt = ".cache";

        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static object GetCache(string cacheKey)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            //return objCache[cacheKey];
            long i = System.Runtime.Caching.MemoryCache.Default.GetCount();
            CacheItem objCache = System.Runtime.Caching.MemoryCache.Default.GetCacheItem(cacheKey);
            if (objCache == null)
            {
                string _filepath = filePath + cacheKey + fileExt;
                if (System.IO.File.Exists(_filepath))
                {
                    System.IO.FileStream _file = System.IO.File.OpenRead(_filepath);
                    if (_file.CanRead)
                    {
                        // 读取文件的 byte[] 
                        byte[] bytes = new byte[_file.Length];
                        _file.Read(bytes, 0, bytes.Length);
                        _file.Close();
                        // 把 byte[] 转换成 Stream 
                        System.IO.Stream stream = new System.IO.MemoryStream(bytes);

                        System.Diagnostics.Debug.WriteLine("缓存反序列化获取数据：" + cacheKey);
                        object obj = CacheHelper.BinaryDeSerialize(stream);
                        CacheData _data = (CacheData)obj;
                        if (_data != null)
                        {
                            //判断是否过期
                            if (_data.FailureTime >= DateTime.Now)
                            {
                                //将数据添加到内存
                                CacheHelper.SetCacheToMemory(cacheKey, _data);
                                return _data.Value;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("数据过期：" + cacheKey);
                                System.IO.File.Delete(_filepath);
                                //数据过期
                                return null;
                            }
                        }
                        else { return null; }
                    }
                    else { return null; }
                }
                else { return null; }
            }
            else
            {
                CacheData _data = (CacheData)objCache.Value;
                return _data.Value;
            }
        }
        /// <summary>
        /// 内存缓存数
        /// </summary>
        /// <returns></returns>
        public static object GetCacheCount()
        {
            return System.Runtime.Caching.MemoryCache.Default.GetCount();
        }
        /// <summary>
        /// 文件缓存数
        /// </summary>
        /// <returns></returns>
        public static object GetFileCacheCount()
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(filePath);
            return di.GetFiles().Length;
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static bool SetCache(string cacheKey, object objObject, CacheItemPolicy policy)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;            
            //objCache.Insert(cacheKey, objObject);
            string _filepath = filePath + cacheKey + fileExt;
            if (System.IO.Directory.Exists(filePath) == false)
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            //设置缓存数据的相关参数
            CacheData data = new CacheData() { Value = objObject, CreateTime = DateTime.Now, AbsoluteExpiration = policy.AbsoluteExpiration, Priority = policy.Priority };
            CacheItem objCache = new CacheItem(cacheKey, data);
            System.IO.FileStream stream = null;
            if (System.IO.File.Exists(_filepath) == false)
            {
                stream = new System.IO.FileStream(_filepath, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write, System.IO.FileShare.Write);
            }
            else
            {
                stream = new System.IO.FileStream(_filepath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Write);
            }
            System.Diagnostics.Debug.WriteLine("缓存序列化设置数据：" + cacheKey);
            CacheHelper.BinarySerialize(stream, data);
            return System.Runtime.Caching.MemoryCache.Default.Add(objCache, policy);
        }
        public static bool SetCacheToMemory(string cacheKey, CacheData data)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            CacheItem objCache = new CacheItem(cacheKey, data);
            policy.AbsoluteExpiration = data.AbsoluteExpiration;
            policy.Priority = CacheItemPriority.NotRemovable;
            return System.Runtime.Caching.MemoryCache.Default.Add(objCache, policy);
        }

        public static bool SetCache(string cacheKey, object objObject, DateTimeOffset AbsoluteExpiration)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;            
            //objCache.Insert(cacheKey, objObject);
            CacheItemPolicy _priority = new CacheItemPolicy();
            _priority.Priority = CacheItemPriority.NotRemovable;
            _priority.AbsoluteExpiration = AbsoluteExpiration;
            return SetCache(cacheKey, objObject, _priority);
        }

        public static bool SetCache(string cacheKey, object objObject, CacheItemPriority priority)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;            
            //objCache.Insert(cacheKey, objObject);
            CacheItemPolicy _priority = new CacheItemPolicy();
            _priority.Priority = priority;
            _priority.AbsoluteExpiration = System.Runtime.Caching.ObjectCache.InfiniteAbsoluteExpiration;
            return SetCache(cacheKey, objObject, _priority);
        }
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static bool SetCache(string cacheKey, object objObject)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            //objCache.Insert(cacheKey, objObject, null, DateTime.MaxValue, timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
            return CacheHelper.SetCache(cacheKey, objObject, System.Runtime.Caching.CacheItemPriority.NotRemovable);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveCache(string cacheKey)
        {
            //System.Web.Caching.Cache cache = HttpRuntime.Cache;
            //cache.Remove(cacheKey);
            System.Runtime.Caching.MemoryCache.Default.Remove(cacheKey);
            string _filepath = filePath + cacheKey + fileExt;
            System.IO.File.Delete(_filepath);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            //System.Web.Caching.Cache cache = HttpRuntime.Cache;
            //IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            //while (cacheEnum.MoveNext())
            //{
            //    cache.Remove(cacheEnum.Key.ToString());
            //}
            MemoryCache _cache = System.Runtime.Caching.MemoryCache.Default;
            foreach (var _c in _cache.GetValues(null))
            {
                _cache.Remove(_c.Key);
            }
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(filePath);
            di.Delete(true);
        }
        /// <summary>
        /// 清除指定缓存
        /// </summary>
        /// <param name="type">1:内存 2:文件</param>
        public static void RemoveAllCache(int type)
        {
            if (type == 1)
            {
                MemoryCache _cache = System.Runtime.Caching.MemoryCache.Default;
                foreach (var _c in _cache.GetValues(null))
                {
                    _cache.Remove(_c.Key);
                }
            }
            else if (type == 2)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(filePath);
                di.Delete(true);
            }
        }

        #region 流序列化
        public static void BinarySerialize(System.IO.Stream stream, object obj)
        {
            try
            {
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(stream, obj);
            }
            catch (Exception e)
            {
                //IOHelper.WriteDebug(e);
            }
            finally
            {
                //stream.Close();
                stream.Dispose();
            }
        }

        public static object BinaryDeSerialize(System.IO.Stream stream)
        {
            object obj = null;
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                obj = formatter.Deserialize(stream);
            }
            catch (Exception e)
            {
                //IOHelper.WriteDebug(e);
            }
            finally
            {
                //stream.Close();
                stream.Dispose();
            }
            return obj;
        }
        #endregion
    }
    #endregion

    #region
    public class BrowserHelper
    {
        /// <summary>
        /// 调用系统浏览器打开网页
        /// http://m.jb51.net/article/44622.htm
        /// http://www.2cto.com/kf/201412/365633.html
        /// </summary>
        /// <param name="url">打开网页的链接</param>
        public static void OpenBrowserUrl(string url)
        {
            try
            {
                // 64位注册表路径
                var openKey = @"SOFTWARE\Wow6432Node\Google\Chrome";
                if (IntPtr.Size == 4)
                {
                    // 32位注册表路径
                    openKey = @"SOFTWARE\Google\Chrome";
                }
                RegistryKey appPath = Registry.LocalMachine.OpenSubKey(openKey);
                // 谷歌浏览器就用谷歌打开，没找到就用系统默认的浏览器
                // 谷歌卸载了，注册表还没有清空，程序会返回一个"系统找不到指定的文件。"的bug
                if (appPath != null)
                {
                    var result = Process.Start("chrome.exe", url);
                    if (result == null)
                    {
                        OpenIe(url);
                    }
                }
                else
                {
                    var result = Process.Start("chrome.exe", url);
                    if (result == null)
                    {
                        OpenDefaultBrowserUrl(url);
                    }
                }
            }
            catch
            {
                // 出错调用用户默认设置的浏览器，还不行就调用IE
                OpenDefaultBrowserUrl(url);
            }
        }

        /// <summary>
        /// 用IE打开浏览器
        /// </summary>
        /// <param name="url"></param>
        public static void OpenIe(string url)
        {
            try
            {
                Process.Start("iexplore.exe", url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // IE浏览器路径安装：C:\Program Files\Internet Explorer
                // at System.Diagnostics.process.StartWithshellExecuteEx(ProcessStartInfo startInfo)注意这个错误
                try
                {
                    if (File.Exists(@"C:\Program Files\Internet Explorer\iexplore.exe"))
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo
                        {
                            FileName = @"C:\Program Files\Internet Explorer\iexplore.exe",
                            Arguments = url,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        Process.Start(processStartInfo);
                    }
                    else
                    {
                        if (File.Exists(@"C:\Program Files (x86)\Internet Explorer\iexplore.exe"))
                        {
                            ProcessStartInfo processStartInfo = new ProcessStartInfo
                            {
                                FileName = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe",
                                Arguments = url,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            Process.Start(processStartInfo);
                        }
                        else
                        {
                            if (MessageBox.Show(@"系统未安装IE浏览器，是否下载安装？", null, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // 打开下载链接，从微软官网下载
                                OpenDefaultBrowserUrl("http://windows.microsoft.com/zh-cn/internet-explorer/download-ie");
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        /// <summary>
        /// 打开系统默认浏览器（用户自己设置了默认浏览器）
        /// </summary>
        /// <param name="url"></param>
        public static void OpenDefaultBrowserUrl(string url)
        {
            try
            {
                // 方法1
                //从注册表中读取默认浏览器可执行文件路径
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                if (key != null)
                {
                    string s = key.GetValue("").ToString();
                    //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！
                    //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"
                    var lastIndex = s.IndexOf(".exe", StringComparison.Ordinal);
                    if (lastIndex == -1)
                    {
                        lastIndex = s.IndexOf(".EXE", StringComparison.Ordinal);
                    }
                    var path = s.Substring(1, lastIndex + 3);
                    var result = Process.Start(path, url);
                    if (result == null)
                    {
                        // 方法2
                        // 调用系统默认的浏览器 
                        var result1 = Process.Start("explorer.exe", url);
                        if (result1 == null)
                        {
                            // 方法3
                            Process.Start(url);
                        }
                    }
                }
                else
                {
                    // 方法2
                    // 调用系统默认的浏览器 
                    var result1 = Process.Start("explorer.exe", url);
                    if (result1 == null)
                    {
                        // 方法3
                        Process.Start(url);
                    }
                }
            }
            catch
            {
                OpenIe(url);
            }
        }

        /// <summary>
        /// 火狐浏览器打开网页
        /// </summary>
        /// <param name="url"></param>
        public static void OpenFireFox(string url)
        {
            try
            {
                // 64位注册表路径
                var openKey = @"SOFTWARE\Wow6432Node\Mozilla\Mozilla Firefox";
                if (IntPtr.Size == 4)
                {
                    // 32位注册表路径
                    openKey = @"SOFTWARE\Mozilla\Mozilla Firefox";
                }
                RegistryKey appPath = Registry.LocalMachine.OpenSubKey(openKey);
                if (appPath != null)
                {
                    var result = Process.Start("firefox.exe", url);
                    if (result == null)
                    {
                        OpenIe(url);
                    }
                }
                else
                {
                    var result = Process.Start("firefox.exe", url);
                    if (result == null)
                    {
                        OpenDefaultBrowserUrl(url);
                    }
                }
            }
            catch
            {
                OpenDefaultBrowserUrl(url);
            }
        }
    }
    #endregion
}
