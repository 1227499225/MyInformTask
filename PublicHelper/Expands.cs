using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PublicHelper
{
   public static class Expands
    {
       
        #region  DataTable 相关
        public static bool DtisNull(this DataTable dt)
        {
            if (dt == null)
                return true;
            if (dt.Rows.Count <= 0)
                return true;
            return false;
        }
        #endregion

        #region 字符串相关
        public static bool StrIsNull(this string Str) {
            return string.IsNullOrEmpty(Str);
        }
        #region 占位符相关
        /// <summary>
        /// 效率不高
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Fill(this string Str, params object[] args)
        {
            return string.Format(Str, args);
        }
        public static string Fill(this string Str, object args)
        {
            return string.Format(Str, args);
        }
        public static string Fill(this string Str, object args0, object args1)
        {
            return string.Format(Str, args0, args1);
        }
        public static string Fill(this string Str, object args0, object args1, object args2)
        {
            return string.Format(Str, args0, args1, args2);
        }
        #endregion
        #endregion

        #region 正则表达式相关
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            else return Regex.IsMatch(s, pattern);
        }

        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }
        #endregion
    }
}
