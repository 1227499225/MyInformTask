using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uninstall
{
    public partial class FrmUninstall : Form
    {
        public FrmUninstall()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//文件路径
            DelectDir(path);
            Application.Exit();//关闭
            //if (File.Exists(path))
            //{
            //    // 是文件
            //}
            //else if (Directory.Exists(path))
            //{
            //    // 是文件夹
            //}
            //else
            //{
            //    // 都不是
            //}
        }
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        if (i.Name != "Uninstall.exe")
                            //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                            File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
