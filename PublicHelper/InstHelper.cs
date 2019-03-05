using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections;

namespace PublicHelper
{
    /// <summary>
    /// 数据基础数据类型处理类
    /// </summary>
   public static class InstHelper
    {
        /// <summary>
        /// 创建单子
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlList"></param>
        /// <param name="CreatorId"></param>
        /// <returns></returns>
        public static string CreatTaskInfo<T>(ArrayList SqlList,string CreatorId) {
            Type type = typeof(T);
            PropertyInfo[] propertyArray = type.GetProperties();
            propertyArray = SpecialHelper.GetPropertyArray(propertyArray);
            object[] attributes = typeof(T).GetCustomAttributes(typeof(Model._MoAttribute), true);
            string StrTableName = (attributes[0] as Model._MoAttribute).TableName,//表名
                DataSoureName = (attributes[0] as Model._MoAttribute).DataSoureName;

            InstTaskModel _task=CreatTaskInfo(CreatorId, StrTableName, DataSoureName);
            List<string> _list = new List<string>();
            foreach (string item in SqlList)
            {
                string szSQL = item.Fill(_task.Id,_task.SnNumber);
                _list.Add(szSQL);
            }
            SqliteDBHelper.TransActionOperate(_list, DataSoureName);
            return _task.Id;
        }
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private static InstTaskModel CreatTaskInfo(string UserId, string StrTableName,string DataSoureName) {
            InstTaskModel _task = new InstTaskModel();
            _task.Id = Guid.NewGuid().ToString();
            _task.SnNumber = CreateSnnumber(StrTableName,DataSoureName);
            _task.CreatorId = UserId;
            _task.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _task.Title = "/";
            object[] _ob = SpecialHelper.CreateInserSql<InstTaskModel>(_task);
            int i = SqliteDBHelper.ExecuteSql(_ob[0].ToString().Replace("@strSqlValue", (_ob[1].ToString())), _ob[4].ToString());
            return _task;
        }
        //创建编号
        private static string CreateSnnumber(string StrTableName, string DataSoureName) {
           

            string InitiallyStr = "TD";
            string MiddleStr = DateTime.Now.ToString("yyyyMMdd");
            string EndingStr = "";
            int MaxSn = 0;
            string _v= SqliteDBHelper.QueryString((SpecialHelper.SqlHelper.TaskSqlDic("V04001")).Fill(StrTableName, InitiallyStr+"-"+ MiddleStr), DataSoureName);
            if (_v.StrIsNull())
                MaxSn = 1;
            else
                MaxSn = int.Parse(_v) + 1;

            InstSerialModel Serial = new InstSerialModel();
            Serial.Id = Guid.NewGuid().ToString();
            Serial.Type = StrTableName;
            Serial.Prefix = InitiallyStr + "-" + MiddleStr;
            Serial.Number = MaxSn;
            Serial.EnterpriseId = "";
            #region 入库
            object[] _ob=  SpecialHelper.CreateInserSql<InstSerialModel>(Serial);
            int i = SqliteDBHelper.ExecuteSql(_ob[0].ToString().Replace("@strSqlValue", (_ob[1].ToString())), _ob[4].ToString());
            #endregion
            return Serial.Prefix+ MaxSn.ToString("D3");
        }
    }
}
