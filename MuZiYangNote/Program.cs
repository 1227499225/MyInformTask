using MuZiYangNote.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote
{
    static class Program
    {
        public static string Min = "";
        private static DateTime Period = Convert.ToDateTime("2019-3-6");
        /// <summary>
        /// 超级管理员代码
        /// </summary>
        public static string SuperAdminCode;
        /// <summary>
        /// 默认语言
        /// </summary>
        public static Model.LanguageEnum _LANGUAGETYPE = Model.LanguageEnum.LanguageCN;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ExceedATrialPeriod();
            if (IsRunning())
            {
                MessageBoxEX.Show("木子杨便签已经打开,无法打开多个！");
                return;
            }
            Application.Run(new MdiForm());
        }
        public static bool IsRunning()
        {
            Process current = default(Process);
            current = System.Diagnostics.Process.GetCurrentProcess();
            Process[] processes = null;
            processes = System.Diagnostics.Process.GetProcessesByName(current.ProcessName);

            Process process = default(Process);

            foreach (Process tempLoopVar_process in processes)
            {
                process = tempLoopVar_process;

                if (process.Id != current.Id)
                {
                    if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return true;

                    }

                }
            }
            return false;
        }
        /// <summary>
        /// 超过使用期
        /// </summary>
        public static void ExceedATrialPeriod() {
            //试用期小于当前日期
            if (DateTime.Compare(Period, DateTime.Now) < 0) {
                MessageBoxEX.Show("已超过使用期限！");
                string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                Process.Start(path + "Uninstall.exe");
                Application.Exit();
            }

        }
    }
}
