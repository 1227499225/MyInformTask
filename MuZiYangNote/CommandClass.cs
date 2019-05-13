using Model;
using PublicHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MuZiYangNote
{
   public class CommandClass
    {
        internal static LogModel log = new LogModel();
        internal List<ReturnStr> rts { get; set; } = new List<ReturnStr>();
        private const string MZY = ">MZY>";//开始
        private const string Update = "Update";
        private const string END = ">END>";//结束

        internal CommandClass(object Parame) {
            if (Parame == null)
            {
                log.Erlv = MessageLevel.LogWarning;
                log.szStr = "指令不能为空";
                return;
            }
            if (((string)Parame).ToLower().Contains(MZY.ToLower())) {
                ReturnStr rs = new ReturnStr();
                if (((string)Parame).ToUpper().EndsWith(END))
                {
                    string rex = "(?<=({0}))[.\\s\\S]*?(?=({1}))";
                    string CommandStr = null;
                    Regex regex01 = new Regex(rex.Fill(MZY,END), RegexOptions.IgnoreCase);
                    MatchCollection matches01 = regex01.Matches((string)Parame);
                    foreach (Match match in matches01)
                    {
                        CommandStr = match.Value;
                    }
                    rs = new ReturnStr() {
                        StateLevel = StateLevel.LvZero,
                        Command = END,
                        CommandStr = MZY+CommandStr+END
                    };
                }
                else {
                    rs = new ReturnStr()
                    {
                        StateLevel = StateLevel.LvOne,
                        Command = MZY,
                        color = System.Drawing.Color.Blue,
                        CommandStr = "\n>"
                    };
                }
                rts.Add(rs);
            }
        }

        //返回指令操作
        internal class ReturnStr  {
            internal StateLevel StateLevel { get; set; }
            //对应命令内容
            internal string Command { get; set; }
            internal string CommandStr { get; set; }
            //反馈命令颜色
            internal System.Drawing.Color color { get; set; } = System.Drawing.Color.Blue;
        }

        public class TaskDetailsCommand{
            internal static ArrayList CommandFormat = new ArrayList()
            {
                //更新任务打开状态为1/Update Task Open State to 1
              CommandFormat.Add("(?<=(ForData=\"))[.\\s\\S]*?(?=(\"))"),//是否常开
              CommandFormat.Add(""),
            };
            TaskDetailsCommand(object Parame) {
                if (Parame == null)
                {
                    log.Erlv = MessageLevel.LogWarning;
                    log.szStr = "指令不能为空";
                    return;
                }
            }
        }
        ////命令格式
        //internal class CommandFormat {
        //    internal string 
        //}
    }
}
