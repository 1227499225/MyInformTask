using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using PublicHelper;

namespace PublicHelper
{
    /// <summary>
    /// http://www.cnblogs.com/leemano/p/6578050.html
    /// </summary>
   public class SqliteDBHelper
    {
        private static PubConstant pub = new PubConstant();  //实例化公共类
        public  SqliteDBHelper(){

        }

        /// <summary>
        /// 建库
        /// </summary>
        public static void CreateDB(string DataSource=null) 
       {
           try
           {
               SQLiteConnection cn = new SQLiteConnection(DataSource);
               cn.Open();
               //cn.ChangePassword(pub.SQLiteDBPwd);//加密
                //cn.SetPassword("111");//解密
               cn.Close();
           }
           catch (Exception ex)
           {
            
           }
       }
        /// <summary>
        /// 删除数据库
        /// </summary>
        public static void DeleteDB(string SQLiteDBpath = null)
       {
            if (!SQLiteDBpath.StrIsNull())
                pub.SQLiteDBpath = SQLiteDBpath;
           // = @"d:\test\123.sqlite";
           if (System.IO.File.Exists(pub.SQLiteDBpath))
           {
               System.IO.File.Delete(  pub.SQLiteDBpath);
           }
       }
        public static string _sd(string DataSoure, string SQLiteDBpath) {
            if (!DataSoure.StrIsNull())
            {
                SQLiteDBpath += DataSoure;
            }
            return SQLiteDBpath;
        }
        /// <summary>
       /// 添加表
        /// </summary>
       public static void CreateTable(string CommandText,string DataSoure=null)
        {
            // = @"d:\test\123.sqlite";
            SQLiteConnection cn = new SQLiteConnection(_sd(DataSoure,pub.SQLiteDBpath));
            if (cn.State!= System.Data.ConnectionState.Open)
            {
                cn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = cn;
                cmd.CommandText = CommandText; //"CREATE TABLE t1(id varchar(4),score int)";
                //cmd.CommandText = "CREATE TABLE IF NOT EXISTS t1(id varchar(4),score int)";
                cmd.ExecuteNonQuery();
            }
            cn.Close();
        }
       /// <summary>
       /// 删除表
       /// </summary>
       public static void DeleteTable(string CommandText,string DataSoure=null)
       {
           // = @"d:\test\123.sqlite";
           SQLiteConnection cn = new SQLiteConnection( _sd(DataSoure,pub.SQLiteDBpath));
           if (cn.State != System.Data.ConnectionState.Open)
           {
               cn.Open();
               SQLiteCommand cmd = new SQLiteCommand();
               cmd.Connection = cn;
               cmd.CommandText = CommandText; //"DROP TABLE IF EXISTS t1";
               cmd.ExecuteNonQuery();
           }
           cn.Close();
       }
       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="CommandText"></param>
       public static DataSet Query_ds(string CommandText,string DataSoure=null) 
       {
           DataSet ds = new DataSet();
           try
           {
               SQLiteConnection cn = new SQLiteConnection(  _sd(DataSoure,pub.SQLiteDBpath));
               cn.Open();
               if (CommandText.Contains("regexp"))
               {
                   SQLiteFunction.RegisterFunction(typeof(PublicHelper.SQLiteFunction_Custom.REGEXP));//注册函数
               }
               SQLiteCommand sqlCmd = new SQLiteCommand(cn);//"PRAGMA integrity_check"
               sqlCmd = new SQLiteCommand(CommandText,cn);
               sqlCmd.CommandType = CommandType.Text;
               sqlCmd.ExecuteNonQuery();
               SQLiteDataAdapter liteAdapter = new SQLiteDataAdapter(sqlCmd);
               liteAdapter.Fill(ds);

               //SQLiteDataAdapter da = new SQLiteDataAdapter(CommandText,cn);
               //da.Fill(ds);
               //while(sr.Read())
               //{
               //sr.gets
               //}
               cn.Close();
           }
           catch (Exception ex)
           {
               //ErrorHistoryS.ErrorHistoryS_DOC(ex);
           }
           return ds;
       }
       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="CommandText"></param>
       /// <returns></returns>
       public static DataTable Query_dt(string CommandText,string DataSoure=null) 
       {
           DataTable dt = new DataTable();
           SQLiteConnection cn = new SQLiteConnection( _sd(DataSoure,pub.SQLiteDBpath));
           cn.Open();
           SQLiteDataAdapter da = new SQLiteDataAdapter(CommandText, cn);
           da.Fill(dt);
           cn.Close();
           return dt;
       }
       /// <summary>
       /// 事务
       /// </summary>
       /// <param name="cn"></param>
       /// <param name="cmd"></param>
       public static void TransActionOperate(List<string> SQL_list,string DataSoure=null)
       {
           SQLiteConnection cn = new SQLiteConnection( _sd(DataSoure,pub.SQLiteDBpath));
           cn.Open();//打开连接  
           using (SQLiteTransaction tr = cn.BeginTransaction())
           { 
           try
           {
                string s = "";
                int n = 0;
                //cmd.CommandText = "INSERT INTO t2(id,score) VALUES(@id,@score)";
                //cmd.Parameters.Add("id", DbType.String);
                //cmd.Parameters.Add("score", DbType.Int32);
                for (int i = 0; i < SQL_list.Count; i++)
                {
                    SQLiteCommand cmd = new SQLiteCommand(cn);
                    cmd.Transaction = tr;  
                    s = i.ToString();
                    n = i;
                    //cmd.Parameters[0].Value = s;
                    //cmd.Parameters[1].Value = n;
                    cmd.CommandText = SQL_list[i];
                    cmd.ExecuteNonQuery();
                }
                tr.Commit();//提交事务
           }
           catch (Exception ex)
           {
               tr.Rollback();//回滚事务
               //ErrorHistoryS.ErrorHistoryS_DOC(ex);
           }
           }
       }
       /// <summary>
       /// 参数(parameter)化,执行一条SQl语句
       /// </summary>
       /// <param name="path"></param>
       /// <param name="szSQL"></param>
       /// <param name="parameter"></param>
       /// <returns></returns>
       static bool TransActionOperate_1( string szSQL, SQLiteParameter[] parameter,string DataSoure=null)
       {
           bool blnRes = false;
            using (SQLiteConnection cn = new SQLiteConnection(_sd(DataSoure,pub.SQLiteDBpath)))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    PrepareCommand(cmd, cn, null, szSQL, parameter);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        blnRes = true;
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        blnRes = false;
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        cn.Close();
                    }
                }
                return blnRes;
            }

           //using (SQLiteTransaction tr = cn.BeginTransaction())
           //{
           //    string s = "";
           //    int n = 0;
           //    cmd.CommandText = "INSERT INTO t2(id,score) VALUES(@id,@score)";
           //    cmd.Parameters.Add("id", DbType.String);
           //    cmd.Parameters.Add("score", DbType.Int32);
           //    for (int i = 0; i < 10; i++)
           //    {
           //        s = i.ToString();
           //        n = i;
           //        cmd.Parameters[0].Value = s;
           //        cmd.Parameters[1].Value = n;
           //        cmd.ExecuteNonQuery();
           //    }
           //    tr.Commit();
           //}
       }
    
       private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
       {
           try
           {
               if (conn.State != ConnectionState.Open)
                   conn.Open();
               cmd.Connection = conn;
               cmd.CommandText = cmdText;
               if (trans != null)
                   cmd.Transaction = trans;
               cmd.CommandType = CommandType.Text;//cmdType;
               if (cmdParms != null)
               {
                   foreach (SQLiteParameter parameter in cmdParms)
                   {
                       if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                           (parameter.Value == null))
                       {
                           parameter.Value = DBNull.Value;
                       }
                       cmd.Parameters.Add(parameter);

                       //cmd.Parameters.Add(parameter.Value);
                   }
               }
           }
           catch (Exception)
           {

               throw;
           }

       }
  

   }

}
