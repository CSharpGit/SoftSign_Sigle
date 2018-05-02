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
    /// Person.xaml 的交互逻辑
    /// </summary>
    public partial class Person : Window
    {
        PersonClass person;
        public Person(PersonClass newperson)
        {
            InitializeComponent();
            person = newperson;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            username.Text = person.name.Trim();
            usernum.Text = person.num.Trim();
            userphone.Text = person.phone.Trim();
            userclass.Text = person.classname.Trim();
            useracade.Text = person.acade.Trim();
            userposi.Text = person.position.Trim();
            userqq.Text = person.qq.Trim();
            usersex.Text = person.sex.Trim();
            userbedroom.Text = person.bedroom.Trim();
        }
        bool issave = false;
        private void btchange_Click(object sender, RoutedEventArgs e)
        {
            if (issave)
            {
                if(userphone.Text.Length<0||userbedroom.Text.Length<0||userqq.Text.Length<0||usersex.Text.Length<0)
                {
                    MessageBox.Show("不允许有空值！");
                    return;
                }
                string cmdtxt = "update T_User set User_Sex=@sex,User_Phone=@phone,User_QQ=@qq,User_Bedroom=@bedroom where User_Num=@num";
                SqlParameter[] parasl = new SqlParameter[]
                         {new SqlParameter ("@sex",usersex.Text.Trim()),new SqlParameter ("@phone",userphone.Text.Trim()),new SqlParameter ("@qq",userqq.Text.Trim()),
                         new SqlParameter ("@bedroom",userbedroom.Text.Trim()),new SqlParameter ("@num",person.num.Trim() )};
                if(SqlHelper.ExecuteNonQuery(cmdtxt,parasl)<0)
                {
                    MessageBox.Show("修改失败！");
                    return;
                }
                MessageBox.Show("修改成功！");
                this.Close();
            }
            else
            {
                userphone.Foreground = new SolidColorBrush(Colors.Blue);
                userbedroom.Foreground = new SolidColorBrush(Colors.Blue);
                userqq.Foreground = new SolidColorBrush(Colors.Blue);
                usersex.Foreground = new SolidColorBrush(Colors.Blue);
                usersex.Focus();
                userphone.IsReadOnly = false; userbedroom.IsReadOnly = false;
                userqq.IsReadOnly = false; usersex.IsReadOnly = false;
                btchange.Content = "保存";
                issave = true;
            }
        }
    }
}
