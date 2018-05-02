using CRUDTest1;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoftSign_Sigle
{
    /// <summary>
    /// SendMeaasge.xaml 的交互逻辑
    /// </summary>
    public partial class SendMeaasge : Window
    {
        string Usernum, Username;
        public SendMeaasge(string usernum,string username)
        {
            InitializeComponent();
            Usernum = usernum;
            Username = username;
        }

        private void btsend_Click(object sender, RoutedEventArgs e)
        {
            string txtcontent=txtmain.Text;
            string cmdtxt = "Insert  into T_Getidea(Getidea_Username, Getidea_Usernum, Getidea_Content, Getidea_Time,Getidea_Type,Getidea_Title) Values(@UserName,@UserNum,@nei,@time,@Getidea_Type,@Getidea_Title)";
          SqlParameter[] parasl = new SqlParameter[] { new SqlParameter("@UserName", Username), new SqlParameter("@UserNum", Usernum), 
              new SqlParameter("@nei", txtcontent), new SqlParameter("@time", DateTime.Now), new SqlParameter("@Getidea_Type", '0') , new SqlParameter("@Getidea_Title", "来自签到系统个人版") };
          if (txtcontent.Length > 0)
              if (SqlHelper.ExecuteNonQuery(cmdtxt, parasl) > 0)
              {
                  MessageBox.Show("已发送成功，感谢你的反馈！");
                  txtmain.Text = "";
              }
              else MessageBox.Show("发送失败，请重新发送！");
        }
    }
}
