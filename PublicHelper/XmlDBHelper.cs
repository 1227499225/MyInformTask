
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PublicHelper
{
   public class XmlDBHelper
    {
        /// <summary>
        /// 从 XML 文件读取地址信息.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Pareats">节点路径：//a</param>
        /// <param name="jos">获取当前节点下唯一一个节点的属性集合：获取name的值就填写name</param>
        /// <returns></returns>
        public static List<string> ReadFromXmlFile(string fileName, string Pareats, string[] jos)
        {
            List<string> result = new List<string>();
            //StreamReader sr = new StreamReader(fileName, Encoding.Default);
            //sr.Close();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode xn = doc.SelectSingleNode("" + Pareats + "");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xn1 in xnl)
            {
                XmlElement xe = (XmlElement)xn1;
                if (jos.Length > 0)
                {
                    for (int i = 0; i < jos.Length; i++)
                    {
                        result.Add(xe.GetAttribute(jos[i]).ToString());
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 通过key查找结果value
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="Pareats"></param>
        /// <param name="key">key值集合{"A","B"}</param>
        /// <returns></returns>
        public static List<string> ReadFromXml_key(string fileName, string Pareats, string[] key)
        {
            List<string> result = new List<string>();
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//过虑注释
            XmlReader reader = XmlReader.Create(fileName, settings);
            doc.Load(reader);
            XmlNode xn = doc.SelectSingleNode(Pareats);
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xn1 in xnl)
            {
                XmlElement xe = (XmlElement)xn1;
                if (key.Length > 0)
                {
                    for (int i = 0; i < key.Length; i++)
                    {
                        string keyVal = xe.GetAttribute("key").ToString();
                        if (key[i] == keyVal)
                        {
                            result.Add(xe.GetAttribute("value").ToString());
                        }
                    }
                }
            }
            return result;
        }
    }
}
