using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.ComponentModel;
using System.Collections.Specialized;

namespace EmailTask
{
    /// <summary>
    /// 邮件辅助类
    /// </summary>
    public class EmailHelper
    {
        private static AppSettingsSection ap = ((new ReadConfigs()).ReadConfig()) as AppSettingsSection;
        private static SetConfig sc = new SetConfig();
        /// <summary>
        /// 是否成功
        /// </summary>
        [Description("邮件是否发送成功")]
        public bool Res { get; set; } = false;
        /// <summary>
        /// 报错信息
        /// </summary>
        [Description("报错信息!")]
        public string Msg { get; set; } = string.Empty;
        /// <summary>
        /// 请在appSettings中配置发送人信息EmailAddress、EmailPwd、EmailName、Port、Host
        /// </summary>
        /// <param name="em"></param>
        public EmailHelper(EmailSendInfo em)
        {
            if (!em.SetConfig.DenyBuiltInSetConfig)
            {//不使用默认或自定义项
                if (em.IsLoadConfigs)
                {//使用配置文件
                    if (ap.Settings.Count == 0)
                    {
                        NameValueCollection ap = System.Configuration.ConfigurationManager.AppSettings;
                        sc = new SetConfig()
                        {
                            EmailAddress = ap["EmailAddress"],
                            AccountNumber = ap["AccountNumber"],
                            EmailPwd = ap["EmailPwd"],
                            EmailName = ap["EmailName"],
                            Port = Convert.ToInt32(ap["Port"] == "" ? "0" : ap["Port"]),
                            Host = ap["Host"]
                        };
                    }
                    else
                    {
                        sc = new SetConfig()
                        {
                            EmailAddress = ap.Settings["EmailAddress"].Value,
                            EmailPwd = ap.Settings["EmailPwd"].Value,
                            EmailName = ap.Settings["EmailName"].Value,
                            Port = Convert.ToInt32(ap.Settings["Port"].Value == "" ? "0" : ap.Settings["Port"].Value),
                            Host = ap.Settings["Host"].Value
                        };
                    }
                }
            }
            else
            {
                sc = em.SetConfig;
            }
            SendEmail(em);
        }
        private void SendEmail(EmailSendInfo em)
        {
            try
            {
                //System.Threading.Thread.Sleep(50000);
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();
                //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
                mailMessage.From = new MailAddress(sc.EmailAddress, sc.EmailName);
                //收件人邮箱地址。
                foreach (var a in em.ToUserInfo)
                {
                    mailMessage.To.Add(new MailAddress(a.EmailAddress, a.DisPlayName, a.DisPlayNameEncoding));
                }
                //抄送
                if (em.CcUserInfo.Count > 0)
                {
                    foreach (var a in em.CcUserInfo)
                    {
                        mailMessage.CC.Add(new MailAddress(a.EmailAddress, a.DisPlayName, a.DisPlayNameEncoding));
                    }
                }
                //密送
                if (em.BccUserInfo.Count > 0)
                {
                    foreach (var a in em.BccUserInfo)
                    {
                        mailMessage.CC.Add(new MailAddress(a.EmailAddress, a.DisPlayName, a.DisPlayNameEncoding));
                    }
                }
                //附件
                if (em.FileInfo.Count>0)
                {
                    foreach (var a in em.FileInfo)
                    {
                        //System.Net.Mime.MediaTypeNames.Application.Rtf
                        mailMessage.Attachments.Add(new Attachment(a.FileName,a.FileType));
                    }
                }
                //邮件标题。
                mailMessage.Subject = em.Subject;
                //邮件内容。
                mailMessage.Body = em.BodyText; ;
                mailMessage.BodyEncoding =System.Text.Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                ////设置邮件的发送级别              
                mailMessage.Priority = em.MailPriority;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();
                //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
                client.Host = sc.Host; //"smtp.qq.com";
                //端口
                if (sc.Port != 0)
                    client.Port = sc.Port;
                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new System.Net.NetworkCredential(sc.AccountNumber, sc.EmailPwd);//("1247765299@qq.com", "thqqtnpzyfscfhdd");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                //加这段之前用公司邮箱发送报错：根据验证过程，远程证书无效
                //加上后解决问题
                ServicePointManager.ServerCertificateValidationCallback =
                                                                    delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };

                //发送
                client.Send(mailMessage);
                Res = true;
            }
            catch (Exception ex)
            {
                string LogStr = "--------------------------------------------" + DateTime.Now.ToString() + "----------------------------------------------"
                                + "\r\n" + ex.Message + "\r\n"
                                + JsonHelper.GetJson<EmailSendInfo>(em) + "\r\n";
                LogCs.LogWrite(LogStr);
                Msg = LogStr;
            }
        }
    }
    //public class Result {

    //}
    /// <summary>
    /// 发件人配置
    /// </summary>
    public class SetConfig
    {
        [Description("如果自定义配置信息，需将DenyBuiltInSetConfig值修改为false")]
        public bool DenyBuiltInSetConfig = true;
        public string EmailAddress { get; set; } = "1247765299@qq.com";
        public string EmailName { get; set; } = "木子杨-测试";
        public string AccountNumber { get; set; } = "1247765299@qq.com";
        public string EmailPwd { get; set; } = "mwwxucqfhcsfiebg";
        public int Port { get; set; } = 0;
        public string Host { get; set; } = "smtp.qq.com";
    }
    /// <summary>
    /// 收件人信息
    /// </summary>
    public class EmailSendInfo
    {
        public List<SendUserInfo> ToUserInfo { get; set; } = new List<SendUserInfo>();
        public List<SendUserInfo> CcUserInfo { get; set; } = new List<SendUserInfo>();
        public List<SendUserInfo> BccUserInfo { get; set; } = new List<SendUserInfo>();
        /// <summary>
        /// 是否加载配置文件中配置信息
        /// </summary>
        public bool IsLoadConfigs { get; set; } = false;
        public MailPriority MailPriority { get; set; } = MailPriority.Normal;
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public List<FileInfo> FileInfo { get; set; } = new List<FileInfo>();

        public SetConfig SetConfig { get; set; } = new SetConfig();
    }
    public class FileInfo {
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
    public class SendUserInfo {
        public string DisPlayName { get; set; }
        public string EmailAddress { get; set; }
        public string Sex { get; set; }
        public Encoding DisPlayNameEncoding { get; set; } = System.Text.Encoding.UTF8;
    }
    public class ReadConfigs {
        public Object ReadConfig()
        {
            List<string> list = new List<string>();
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            //AppDomain.CurrentDomain.DynamicDirectory
            file.ExeConfigFilename = "Web.config";// "App.config";
            Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
            Object myApp =string.Empty;
            if (config != null)
            {
                myApp = (AppSettingsSection)config.GetSection("appSettings");
            }

            
            return myApp;
        }
        public string ReadConfig(string key)
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = "App.config";
            Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);

            var myApp = (AppSettingsSection)config.GetSection("appSettings");
            return myApp.Settings["" + key + ""].Value;

        }
        public string WriteConfig(string key, string val)
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = "App.config";
            Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);

            var myApp = (AppSettingsSection)config.GetSection("appSettings");
            myApp.Settings[key].Value = val;
            config.Save();
            return val;

        }
    }
    public class LogCs {
            public static void LogWrite(string logstring)
            {
                try
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/log/" + "Email-log-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    //判断文件是否存在，没有则创建。
                    if (!System.IO.File.Exists(path))
                    {
                        FileStream stream = System.IO.File.Create(path);
                        stream.Close();
                        stream.Dispose();
                    }

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
                        //System.IO.File.Delete(path);
                    }
                }
                catch
                {

                }
            }
        }
    public class JsonHelper
        {
            /// <summary>
            /// 把对象序列化 JSON 字符串 
            /// </summary>
            /// <typeparam name="T">对象类型</typeparam>
            /// <param name="obj">对象实体</param>
            /// <returns>JSON字符串</returns>
            public static string GetJson<T>(T obj)
            {
                //记住 添加引用 System.ServiceModel.Web 
                /**
                 * 如果不添加上面的引用,System.Runtime.Serialization.Json; Json将报错
                 * */
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    json.WriteObject(ms, obj);
                    string szJson = Encoding.UTF8.GetString(ms.ToArray());
                    return szJson;
                }
            }
            /// <summary>
            /// 把JSON字符串还原为对象
            /// </summary>
            /// <typeparam name="T">对象类型</typeparam>
            /// <param name="szJson">JSON字符串</param>
            /// <returns>对象实体</returns>
            public static T ParseFormJson<T>(string szJson)
            {
                T obj = Activator.CreateInstance<T>();
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
                {
                    DataContractJsonSerializer dcj = new DataContractJsonSerializer(typeof(T));
                    return (T)dcj.ReadObject(ms);
                }
            }
        }
    
}
