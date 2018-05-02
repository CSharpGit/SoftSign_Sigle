using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace Updater
{
    public class PcbsoftVersionToRegister
    {
        public string ReadSoftVersion()
        {
            string version;
            FileInfo fi = new FileInfo(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Version.txt"));
            if (fi.Exists == false)
            {
                fi.Create();
                version = "";

            }
            else
            {
                using (FileStream fsRead = new FileStream(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Version.txt"), FileMode.Open))
                {
                    int fsLen = (int)fsRead.Length;
                    byte[] heByte = new byte[fsLen];
                    int r = fsRead.Read(heByte, 0, heByte.Length);
                    string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                    version = myStr;
                }
            }
            return version;
        }
        public void WriteSoftVersion(string version)
        {
            FileInfo fi = new FileInfo(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Version.txt"));
            if (fi.Exists == true)
            {
                fi.Delete();
            }
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(version);
            using (FileStream fsWrite = new FileStream(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Version.txt"), FileMode.Append))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
    }
}
