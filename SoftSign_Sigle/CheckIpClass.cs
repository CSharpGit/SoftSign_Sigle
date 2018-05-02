using CRUDTest1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoftSign_Sigle
{
    class CheckIpClass
    {
        public static long IP2Long(string ip)//change ip to long
        {
            string[] ipBytes;                                                                           //定义字符串分隔数组
            double num = 0;
            if (!string.IsNullOrEmpty(ip))
            {
                ipBytes = ip.Split('.');                                                                //以.为分隔界限
                for (int i = ipBytes.Length - 1; i >= 0; i--)
                {
                    num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));                     //转换为。。。。
                                                             
                }
            }
            return (long)num;                                                                             //长  返回；
        }
       private static  string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")  //IPV4
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
    public  static string CheckIp()                                                                        //ip检查
        {
            string AddressIP = GetAddressIP();
            long start = IP2Long("192.168.226.0");//此两行确定ip范围  
            long end = IP2Long("192.168.226.255");
            long ipAddress = IP2Long(AddressIP);
            bool inRange = (ipAddress >= start && ipAddress <= end); //check ip从此正式开始
            string AddressIP1 = GetAddressIP();
            long start1 = IP2Long("172.18.64.0"); //为什么有两个ip  是因为三教有两个ip段吗  还是两类私有ip
            long end1 = IP2Long("172.18.79.255");
            long ipAddress1 = IP2Long(AddressIP1);
            bool inRange1 = (ipAddress1 >= start1 && ipAddress1 <= end1);
            if (inRange)
            {
                return AddressIP;
            }
            else if (inRange1)
            {

                return AddressIP;

            }
            else return "0";  //两者都不满足 返回0 此人不在三教
        }
        public static bool ThisIpHasSign(string num)    //判断此人是否已经登录
    {
        string cmdtxt = "select Sign_Name,Sign_Num from T_Sign where datediff(day,Sign_Intime,'" + DateTime.Now.Date + "')=0 and Sign_Type=0 and Sign_Hasexit=0 and Sign_Ip='" + CheckIp() + "'";
        DataTable table = SqlHelper.ExecuteDataTable(cmdtxt);
            if(table.Rows.Count>0)
            {
                if (table.Rows[0]["Sign_Num"].ToString() == num)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            return false;

    }
    }
}
