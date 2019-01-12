using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PublicHelper
{
    public class PubConstant
    {

        public string FilePath { get; set; } = System.AppDomain.CurrentDomain.BaseDirectory + "FileSave\\";
        public string SQLiteDBpath { get; set; } = "Data Source=";
        public string SQLiteDBPwd { get; set; } = "1234";
        public PubConstant(string dataName=null)
        {
            this.FilePath= FileHelper.Exists(FilePath);
            this.SQLiteDBpath = SQLiteDBpath+ FilePath + dataName;
        }
        /// <summary>
        /// 获取sqlite数据库位置
        /// </summary>
        /// <returns></returns>
        public string GetSQLite_DB_path()
        {

            FileInfo file = new FileInfo("../../../DBUtility/db.xml");
            //List<string> ls = XmlDBHelper.ReadFromXml_key(file.FullName, "//SQLiteConnectioning", new string[] { "SQLiteConnection" });
            return "";//ls[0].ToString();//获取
        }
        /// <summary>
        /// 获取本地物理路径 MAC
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddressByNetworkInformation()
        {
            string key = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\";
            string macAddress = string.Empty;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                        && adapter.GetPhysicalAddress().ToString().Length != 0)
                    {
                        string fRegistryKey = key + adapter.Id + "\\Connection";
                        RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                        if (rk != null)
                        {
                            string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                            int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                            if (fPnpInstanceID.Length > 3 &&
                                fPnpInstanceID.Substring(0, 3) == "PCI")
                            {
                                macAddress = adapter.GetPhysicalAddress().ToString();
                                for (int i = 1; i < 6; i++)
                                {
                                    macAddress = macAddress.Insert(3 * i - 1, ":");
                                }
                                break;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //这里写异常的处理  
            }
            return macAddress;
        }
    }
}
