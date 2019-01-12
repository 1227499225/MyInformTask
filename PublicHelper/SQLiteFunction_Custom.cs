using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PublicHelper
{
    /// <summary>
    /// sqlite自定义函数
    /// </summary>
   public class SQLiteFunction_Custom
    {
        /// <summary>
        /// 求平方根
        /// </summary>
        [SQLiteFunction(Name = "sqrt", Arguments = 1, FuncType = FunctionType.Scalar)]
        public class Sqrt : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                double d = Convert.ToDouble(args[0]);
                return Math.Sqrt(d);
            }
        }

        /// <summary>
        /// 求平均
        /// </summary>
        [SQLiteFunction(Name = "mean", Arguments = -1, FuncType = FunctionType.Aggregate)]
        public class Mean : SQLiteFunction
        {
            int step = 0;
            public override void Step(object[] args, int stepNumber, ref object contextData)
            {
                double sum = Convert.ToDouble(contextData);
                sum += Convert.ToDouble(args[0]);
                contextData = sum;
                step++;
            }
            public override object Final(object contextData)
            {
                double sum = Convert.ToDouble(contextData);
                double mean = sum / step;
                return mean;
            }
        }

        /// <summary>
        /// 中文排序
        /// </summary>
        [SQLiteFunction(FuncType = FunctionType.Collation, Name = "pinyin")]
        public class PinYin : SQLiteFunction
        {
            public override int Compare(string x, string y)
            {
                return string.Compare(x, y);
            }
        }
        /// <summary>
        /// 正则表达式
        /// </summary>
        [SQLiteFunction(FuncType = FunctionType.Scalar, Name = "regexp")]
        public class REGEXP : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(args[1].ToString(), args[0].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
        }
        /// <summary>
        /// 判断是否纯数字
        /// </summary>
        [SQLiteFunction(FuncType = FunctionType.Scalar, Arguments = 1, Name = "IsInt")]
        public class IsInt : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(args[0].ToString(), @"^[1-9]\d*$");

            }
        }
    }
}
