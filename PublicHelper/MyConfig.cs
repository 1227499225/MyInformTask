using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicHelper
{
    /// <summary>
    /// 获取配置信息
    /// </summary>
    public class MyConfig
    {
        public Object ReadConfig()
        {
            List<string> list = new List<string>();
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = System.Windows.Forms.Application.ExecutablePath + ".config";
            Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);

            var myApp = (AppSettingsSection)config.GetSection("appSettings");
            return myApp;
        }
        public string ReadConfig(string key)
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = System.Windows.Forms.Application.ExecutablePath + ".config";
            Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);

            var myApp = (AppSettingsSection)config.GetSection("appSettings");
            return myApp.Settings[""+ key + ""].Value;

        }
        public string WriteConfig(string key,string val)
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = System.Windows.Forms.Application.ExecutablePath + ".config";
            Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);

            var myApp = (AppSettingsSection)config.GetSection("appSettings");
            myApp.Settings[key].Value = val;
            config.Save();
            return val;

        }
    }
}
