using CRUDTest1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SoftSign_Sigle
{
    /// <summary>
    /// Online.xaml 的交互逻辑
    /// </summary>
    
    
    public partial class Online : Window
    {
        string UserNum;   //此类中皆可使用的变量
        private DispatcherTimer timer;

        public Online(string usernum)
        {
            InitializeComponent();
            UserNum = usernum;
        }
        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        private void InitialTray()
        {
            //设置托盘的各个属性
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.BalloonTipText = "签到在线...";
            notifyIcon.Icon = new System.Drawing.Icon(System.Windows.Forms.Application.StartupPath + "/rjyfzx.ico");
            notifyIcon.Text = "签到在线";
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            System.Windows.Forms.MenuItem main = new System.Windows.Forms.MenuItem("显示主界面");
            main.Click += main_Click;
            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += new EventHandler(exit_Click);

            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { main, exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            //窗体状态改变时候触发
            this.StateChanged += new EventHandler(SysTray_StateChanged);
        }
        void main_Click(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Normal;
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //如果鼠标左键单击
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }

        /// <summary>
        /// 窗体状态改变时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SysTray_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
            }
        }


        private void Lognin() //登入时判定
        {
            bool hassign = CheckIpClass.ThisIpHasSign(UserNum.Trim());
            if (hassign)
            {
                //System.Windows.MessageBox.Show("同一IP地址不能同时在线两个用户！");
                //MainWindow newwind = new MainWindow();
                //newwind.Show();
                //this.Close();
                //return;
            }
            string AddressIP = CheckIpClass.CheckIp();
            if (AddressIP == "0")
            {
                System.Windows.MessageBox.Show("请在第三教学楼登录！");
                MainWindow newwind = new MainWindow();
                newwind.Show();
                this.Close();
                return;
            }
            else
            {
                string smd = "update T_Sign set Sign_Hasexit='1' where Sign_Num='" + UserNum.Trim() + "'";
                SqlHelper.ExecuteNonQuery(smd);
                string cmdtxt = "insert into T_Sign(Sign_Name,Sign_Num,Sign_Intime,Sign_Outtime,Sign_Hasexit,Sign_Type,Sign_Ip) values(@Sign_Name,@Sign_Num,getdate(),getdate(),@Sign_Hasexit,0,'" + AddressIP + "')";
                SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@Sign_Name",table.Rows[0]["User_Name"].ToString()),new SqlParameter("@Sign_Num",UserNum.Trim()),new SqlParameter("@Sign_Hasexit",'0')};

                SqlHelper.ExecuteNonQuery(cmdtxt, para);
            }



            return;

        }
        string id;
        private int Excit()// 退出注销方法 录入注销时间
        {

            string updcmdtxt = "update T_Sign set Sign_Outtime=getdate(),Sign_Hasexit='1' where Sign_Id='" + id + "'";
            return SqlHelper.ExecuteNonQuery(updcmdtxt); //返回改动的行数 int值
        }
        private void exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.MessageBox.Show("确定退出?",
                                               "签到系统",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                if (Excit() > 0)
                    System.Windows.Application.Current.Shutdown();
            }
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)//左键扯动窗体
        {
            this.DragMove();
            this.WindowState = WindowState.Normal;
        }

        private void hidden_Click(object sender, RoutedEventArgs e)  //隐藏窗体
        {

            this.Visibility = Visibility.Hidden;
        }
        DataTable table = new DataTable();
        DataTable tbmessage1 = new DataTable();
        DataTable tbmessage2 = new DataTable();

        private void Load()   //窗口加载时。。。页面显示设计
        {

            string cmdtxt = "select User_Name, User_Class,User_Num,User_Academy, User_Position,User_Sex,User_Phone,User_QQ,User_Bedroom from T_User where User_Num='" + UserNum + "'";
            table.Clear();
            table = SqlHelper.ExecuteDataTable(cmdtxt);
            Lognin();
            username.Content = table.Rows[0]["User_Name"].ToString();
            usernum.Content = table.Rows[0]["User_Num"].ToString();
            userposi.Content = table.Rows[0]["User_Position"].ToString();
            usertime2.Content = (DateTime.Now - tagain).ToString().Substring(0, 8);
            usertime1.Content = TimeSpanAdd(timein);
            usertime3.Content = WeekSpanAdd(timein);
            string cmdtxt1 = "select Sign_Id from T_Sign where Sign_Num='" + UserNum.Trim() + "' order by Sign_Id desc";

            id = SqlHelper.ExecuteScalar(cmdtxt1).ToString();
            #region 通知发布方法
            string cmdmestx = "select top 3 Massage_Title,Massage_Notice from T_Massage order by Massage_Id desc";
           
            tbmessage1 = SqlHelper.ExecuteDataTable(cmdmestx);

            if (tbmessage1.Rows.Count > 0)
            {
               
                t1.Text=   tbmessage1.Rows[0]["Massage_Title"].ToString().Substring(0,10)+"...";
                n1.Content = tbmessage1.Rows[0]["Massage_Notice"].ToString().Substring(0, 10) + "...";
                t2.Text = tbmessage1.Rows[1]["Massage_Title"].ToString().Substring(0, 10) + "...";
                n2.Content = tbmessage1.Rows[1]["Massage_Notice"].ToString().Substring(0, 10) + "..."; 
                t3.Text = tbmessage1.Rows[2]["Massage_Title"].ToString().Substring(0, 10) + "...";
                n3.Content = tbmessage1.Rows[2]["Massage_Notice"].ToString().Substring(0, 10) + "...";

                
            }
            
            #endregion



        }
        DateTime timein = new DateTime();
        DateTime tagain=new DateTime();
        DispatcherTimer linker = new DispatcherTimer();
        DispatcherTimer dct = new DispatcherTimer();//定义timer 

        private void Window_Loaded(object sender, RoutedEventArgs e)//窗口加载后立马执行的事件
        {
            try
            {
                InitialTray();
                timein = DateTime.Now;
                tagain = DateTime.Now;
                Load();
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);//每秒刷新一次
                timer.Tick += timer1_Tick; //事件注册
                timer.Start(); //窗体加载后开始计时
                dct.Interval = TimeSpan.FromMilliseconds(1);//按钮改变效果 每毫秒刷新一次
                dct.Tick += dct_Tick;
                dct.Start();
                linker.Interval = TimeSpan.FromMinutes(1);
                linker.Tick += linker_Tick;
                linker.Start();
                //Storyboard sb1 = Resources["turns"] as Storyboard;//时针动画实例演示
                //sb1.Begin();


                string sl = "select User_Image from T_User where User_Num=@id";
                SqlDataReader da = SqlHelper.ExecuteReader(sl, new SqlParameter("@id", UserNum));

                try
                {
                    if (da.Read())
                    {
                        byte[] b = (byte[])da["User_Image"];
                        MemoryStream ms = new MemoryStream(b);
                        BitmapImage bm = new BitmapImage();
                        bm.BeginInit();
                        bm.StreamSource = ms;
                        bm.EndInit();
                        pb1.Source = bm;
                    }
                }
                catch 
                {
                    
                    
                }
            }


            catch (Exception ee)
            {
                System.Windows.MessageBox.Show(ee.Message);
            }
        }

        void linker_Tick(object sender, EventArgs e)//判断是否离线
        {
            try
            {
                isLinked();
                string updcmdtxt = "update T_Sign set Sign_Outtime=getdate() where Sign_Id='" + id + "'";
                SqlHelper.ExecuteNonQuery(updcmdtxt);    //更新时间
            }
            catch
            {

                System.Windows.Forms.MessageBox.Show("网络出现故障！软件已停止");
                System.Windows.Application.Current.Shutdown();
            }
        }
        //按钮选中状态改变效果计时器方法
        void dct_Tick(object sender, EventArgs e)
        {
            #region 占用资源过多
            if (exit.IsMouseOver == true)
            {
                ImageBrush b = new ImageBrush();

                b.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + "/Picture/redselected.png", UriKind.Relative));

                b.Stretch = Stretch.Fill;

                exit.Background = b;

            }
            else
            {
                ImageBrush a = new ImageBrush();

                a.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Picture\rednone.png", UriKind.Relative));

                a.Stretch = Stretch.Fill;

                exit.Background = a;
            }
            if (hidden.IsMouseOver == true)
            {
                ImageBrush c = new ImageBrush();

                c.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Picture\greenselected.png", UriKind.Relative));

                c.Stretch = Stretch.Fill;

                hidden.Background = c;
            }
            else
            {
                ImageBrush d = new ImageBrush();

                d.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Picture\greennone.png", UriKind.Relative));

                d.Stretch = Stretch.Fill;

                hidden.Background = d;
            }
            if (min.IsMouseOver == true)
            {
                ImageBrush q = new ImageBrush();

                q.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Picture\yellowselected.png", UriKind.Relative));

                q.Stretch = Stretch.Fill;

                min.Background = q;
            }
            else
            {
                ImageBrush w = new ImageBrush();

                w.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + @"\Picture\yellownone.png", UriKind.RelativeOrAbsolute));

                w.Stretch = Stretch.Fill;

                min.Background = w;
            }
            #endregion

        }


        private string TimeSpanAdd(DateTime span)//添加签到时间 一天时间之和 
        {
            string selectx = "select Sign_Intime,Sign_Outtime from T_Sign where Sign_Num ='" + UserNum + "' and  datediff(day ,Sign_Intime,getdate()) =0 and Sign_Hasexit='1'";
            DataTable timespantable = SqlHelper.ExecuteDataTable(selectx);
            TimeSpan timespan = TimeSpan.Zero;  //时间间隔归零化
            foreach (DataRow row in timespantable.Rows)
            {
                timespan += (Convert.ToDateTime(row["Sign_Outtime"]) - Convert.ToDateTime(row["Sign_Intime"])); //从此可以看出来T-SQL语句与c#还是有所不同 要强制转换一下  这是以前的时间间隔
            }
            timespan += (DateTime.Now - Convert.ToDateTime(span));
            timpan = timespan;
            return timespan.ToString().Substring(0, 8);
        }
        TimeSpan timpan;
        TimeSpan weekspan;
        private string WeekSpanAdd(DateTime span)//一周时间之和
        {
            string selectx = "select Sign_Intime,Sign_Outtime from T_Sign where Sign_Num ='" + UserNum + "' and  datediff( week,Sign_Intime,getdate()) =0 and Sign_Hasexit='1'";
            DataTable timespantable = SqlHelper.ExecuteDataTable(selectx);
            TimeSpan timespan = TimeSpan.Zero;  //时间间隔归零化
            foreach (DataRow row in timespantable.Rows)
            {
                timespan += (Convert.ToDateTime(row["Sign_Outtime"]) - Convert.ToDateTime(row["Sign_Intime"])); //从此可以看出来T-SQL语句与c#还是有所不同 要强制转换一下  这是以前的时间间隔
            }
            timespan += (DateTime.Now - Convert.ToDateTime(span));
            weekspan = timespan;
            return weekspan.ToString().Substring(0, 8);
        }

        private void timer1_Tick(object sender, EventArgs e) //很多方法都在这里 主要有2小时窗口提示
        {
           
            if (count > 30 && count < 40)  //疯狂模式的限制条件
            {
                string b = "";
                Random random = new Random();
                int a = random.Next(1, 17);
                switch (a)
                {
                    case 1: b = "working.ani"; break;
                    case 2: b = "App (7).ani"; break;
                    case 3: b = "58.ani"; break;
                    case 4: b = "AppStarting.cur"; break;
                    case 5: b = "11.cur"; break;
                    case 6: b = "12.ani"; break;
                    case 7: b = "13.cur"; break;
                    case 8: b = "14.cur"; break;
                    case 9: b = "21.ani"; break;
                    case 10: b = "Ultra_AppStarting.ani"; break;
                    case 11: b = "Ultra_Arrow.ani"; break;
                    case 12: b = "51.ani"; break;
                    case 13: b = "52.ani"; break;
                    case 14: b = "53.ani"; break;
                    case 15: b = "App (10).ani"; break;
                    case 16: b = "Ultra_IBeam.ani"; break;
                    default: b = "uperleft.cur"; break;
                }
                System.Windows.Input.Cursor cur = new System.Windows.Input.Cursor(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "ani", b));

                this.Cursor = cur;
            }
            usertime1.Content = (timpan + (DateTime.Now - timein)).ToString().Substring(0, 8);
            usertime2.Content = (DateTime.Now - tagain).ToString().Substring(0, 8);//当前登录时间显示
            usertime3.Content = (weekspan + (DateTime.Now - timein)).ToString().Substring(0, 8);
            TimeCount tc = new TimeCount(id);
            tc.Owner = this;
            if (tc.Res)
            {
                
                tagain = DateTime.Now;               
                tc.Res = false;
                usertime2.Content = (DateTime.Now - tagain).ToString().Substring(0, 8);
                
                
            }
            double timespan = (DateTime.Now - tagain).TotalSeconds;
            //if ((timespan %3600.0)>0.0&&(timespan%3600.0)<1.0 )
            //{
            //    try
            //    {                  
            //        //tc.Show();
            //        //this.Hide();                                                                                                                                                 
            //    }
            //    catch
            //    {
            //    }
            //}
        }


        private bool isLinked()   //第二次判定 在已经登入的时间段中是否有人再次登陆
        {

            if (SqlHelper.OpenConnection())
            {
                if (Convert.ToBoolean(SqlHelper.ExecuteScalar("select Sign_Hasexit from T_Sign where Sign_Id='" + id + "'")))
                {

                    System.Windows.MessageBox.Show("您已在其他地方登陆！软件即将关闭！");
                    System.Windows.Application.Current.Shutdown();

                }
                return true;
            }
            else
            {
                System.Windows.MessageBox.Show("网络中断，您已离线！");
                MainWindow win = new MainWindow();  //实例化显示主窗口
                win.Show();
                this.Close();
                return false;
            }

        }
      
       

        private void Button_Click(object sender, RoutedEventArgs e) //联系管理员
        {
            SendMeaasge win = new SendMeaasge(usernum.Content.ToString(), username.Content.ToString());
            win.Show();
        }

        private void exit_Click_1(object sender, RoutedEventArgs e)  //开始注销引用注销方法
        {
            if (System.Windows.MessageBox.Show("确定退出？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                if (Excit() > 0)
                {
                    linker.Stop();



                    this.Close();
                    
                }
        }

        private void HypeLink1_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://59.72.194.42/");
        }

        private void Ellipse_MouseRightButtonDown(object sender, MouseButtonEventArgs e)//圆形头像的自定义
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

            ofd.Filter = "*jpg|*.JPG|*.GIF|*.GIF|*.BMP|*.BMP";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string filepath = ofd.FileName;

                FileStream fs = new FileStream(filepath, FileMode.Open);

                byte[] imageBytes = new byte[fs.Length];

                BinaryReader br = new BinaryReader(fs);

                imageBytes = br.ReadBytes(Convert.ToInt32(fs.Length));

                string sql = "update  T_User set User_Image=@imagebytes where User_Num=@id";

                SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@imagebytes", imageBytes), new SqlParameter("@id", UserNum));

                string sl = "select User_Image from T_User where User_Num=@id";
                SqlDataReader da = SqlHelper.ExecuteReader(sl, new SqlParameter("@id", UserNum));

                if (da.Read())
                {
                    byte[] b = (byte[])da["User_Image"];

                    MemoryStream ms = new MemoryStream(b);

                    BitmapImage bm = new BitmapImage();
                    bm.BeginInit();
                    bm.StreamSource = ms;
                    bm.EndInit();

                    pb1.Source = bm;
                }
            }


        }
        int count = 0;
        private void min_Click(object sender, RoutedEventArgs e)//鼠标改变方法
        {
            count++;
            string b = "";
            Random random = new Random();
            int a = random.Next(1, 17);
            if (count == 50)
            {
                Appgame ag = new Appgame();
                ag.Show();
            }
            if ((count % 3) == 0)
            {

                switch (a)
                {
                    case 1: b = "working.ani"; break;
                    case 2: b = "App (7).ani"; break;
                    case 3: b = "58.ani"; break;
                    case 4: b = "AppStarting.cur"; break;
                    case 5: b = "11.cur"; break;
                    case 6: b = "12.ani"; break;
                    case 7: b = "13.cur"; break;
                    case 8: b = "14.cur"; break;
                    case 9: b = "21.ani"; break;
                    case 10: b = "Ultra_AppStarting.ani"; break;
                    case 11: b = "Ultra_Arrow.ani"; break;
                    case 12: b = "51.ani"; break;
                    case 13: b = "52.ani"; break;
                    case 14: b = "53.ani"; break;
                    case 15: b = "App (10).ani"; break;
                    case 16: b = "Ultra_IBeam.ani"; break;
                    default: b = "uperleft.cur"; break;
                }
                System.Windows.Input.Cursor cur = new System.Windows.Input.Cursor(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "ani", b));

                this.Cursor = cur;
            }

        }

       

      
            

        private void HypeLink2_Click(object sender, RoutedEventArgs e)
        {
            
           
            System.Diagnostics.Process.Start("http://59.72.194.42/Message?MesId=11");
            ImageBrush im = new ImageBrush();
            im.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + "/Picture/选中后.png", UriKind.Relative));
            im.Stretch = Stretch.Fill;
            b1.Background = im;
            
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush im = new ImageBrush();
            im.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath+"/Picture/选中后.png", UriKind.Relative));
            im.Stretch = Stretch.Fill;
            b1.Background = im;
           
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush im = new ImageBrush();
            im.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + "/Picture/选中后.png", UriKind.Relative));
            im.Stretch = Stretch.Fill;
            b2.Background = im;
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush im = new ImageBrush();
            im.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + "/Picture/选中后.png", UriKind.Relative));
            im.Stretch = Stretch.Fill;
            b3.Background = im;
        }

        private void HypeLink3_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://59.72.194.42/Message?MesId=11");
            ImageBrush im = new ImageBrush();
            im.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + "/Picture/选中后.png", UriKind.Relative));
            im.Stretch = Stretch.Fill;
            b2.Background = im;
        }

        private void HypeLink4_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://59.72.194.42/Message?MesId=11");
            ImageBrush im = new ImageBrush();
            im.ImageSource = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + "/Picture/选中后.png", UriKind.Relative));
            im.Stretch = Stretch.Fill;
            b3.Background = im;
        }
        }


    }

