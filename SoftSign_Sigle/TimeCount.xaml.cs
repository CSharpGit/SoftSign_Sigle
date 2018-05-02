using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using CRUDTest1;
using System.Threading;

namespace SoftSign_Sigle
{
    /// <summary>
    /// TimeCount.xaml 的交互逻辑
    /// </summary>
    public partial class TimeCount : Window
    {
        public static bool res = false;
        public bool Res
        {
            get { return res; }
            set { res = value; }
        }
        public TimeCount()
        {
            InitializeComponent();
        }
        string id;
        public TimeCount(string Id)
        {
            InitializeComponent();
            id = Id;
        }
        private int countSecond = 1200;  //倒计时时间
        int seconds, minutes;
        DispatcherTimer dst = new DispatcherTimer();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            dst.Interval = TimeSpan.FromSeconds(1);
            dst.Tick += dst_Tick;
            dst.Start();
            this.Topmost = true;
           
        }
        void dst_Tick(object sender, EventArgs e)
        {
            if (countSecond == 0)
            {
                //dst.Stop();
                //System.Windows.MessageBox.Show("软件关闭，登录时间未记录");
                //this.grid.Children.Clear();
                //grid.Children.Add(text);
                //text.Content = "软件关闭，登录时间未记录";



                this.Close();
                this.Owner.Close();
                //若是不修改数据库此处应该加一条删除语句
                //if (this.grid.Children.Count > 0)
                //{
                //    Thread.Sleep(3000);
                //}
              
            }
            else
            {

                seconds = countSecond % 60;
                minutes = (countSecond - seconds) / 60;
                lb1.Content = "00:"+minutes + ":" + seconds ;
               
                countSecond--;
            
            };
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            string updcmdtxt = "update T_Sign set Sign_Outtime='" + DateTime.Now.ToString() + "',Sign_Hasexit='1' where Sign_Id='" + id + "'";
        
           
            SqlHelper.ExecuteNonQuery(updcmdtxt);    //更新时间
            if (MessageBox.Show("确认注销吗？亲","警告",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                dst.Stop();
                 this.Close();
            this.Owner.Close();
            }
          
        }
            

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dst.Stop();
            string updcmdtxt = "update T_Sign set Sign_Outtime=getdate() where Sign_Id='" + id + "'";


            SqlHelper.ExecuteNonQuery(updcmdtxt);    //更新时间
            this.Owner.Show();
            
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            res = true;
        }
    }
}
