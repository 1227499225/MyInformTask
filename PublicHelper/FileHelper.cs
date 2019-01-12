using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicHelper
{
   public class FileHelper
    {
        public static void AddLgoToTXT(string logstring)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "operalog.txt";
            if (!System.IO.File.Exists(path))
            {
                FileStream stream = System.IO.File.Create(path);
                stream.Close();
                stream.Dispose();
            }
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(logstring);
            }
        }
        
        /// <summary>
        /// 完整
        /// </summary>
        /// <param name="logstring"></param>

        public static void LogWrite(string logstring)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "/log/" + "log-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                //判断文件是否存在，没有则创建。
                Exists(path);

                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(logstring);
                }

                long size = 0;

                //获取文件大小
                using (FileStream file = System.IO.File.OpenRead(path))
                {
                    size = file.Length;//文件大小。byte
                }

                //判断日志文件大于2M，自动删除。
                if (size > (1024 * 4 * 512))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch
            {

            }
        }

        public static string Exists(string Path)
        {
            
            if (!System.IO.File.Exists(Path))
            {
                if (Path.Contains("."))
                {
                    FileStream stream = System.IO.File.Create(Path);
                    stream.Close();
                    stream.Dispose();
                }
                else {
                    System.IO.Directory.CreateDirectory(Path);
                }

            }
            return Path;
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string FileRead(string FileName = null, string FilePath = null)
        {
            string Conext = string.Empty;
            if (string.IsNullOrEmpty(FilePath))
            {
                FilePath = AppDomain.CurrentDomain.BaseDirectory + "Template\\EmailTemplate\\" + FileName + "";
            }
            try
            {
                if (File.Exists(FilePath))
                {
                    string Str = File.ReadAllText(FilePath);
                    byte[] mybyte = Encoding.UTF8.GetBytes(Str);
                    Conext = Encoding.UTF8.GetString(mybyte);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
            return Conext;
        }
    }
}
