using CRUDTest1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SoftSign_Sigle
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        DispatcherTimer max = new DispatcherTimer();
        private void btclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void btsign_Click(object sender, RoutedEventArgs e)
        {
            string usernum = UserNum.Text.ToString();
            if (usernum.Length <= 0)
                return;
            string cmdtxt = "select User_Num from T_User where User_Num='" + usernum + "'";
            DataTable table = new DataTable();
            table = SqlHelper.ExecuteDataTable(cmdtxt);
            if (table.Rows.Count <= 0)
                MessageBox.Show("用户名不存在!");
            else
            {
                if (remusername != usernum)
                {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    cfa.AppSettings.Settings["usernum"].Value = usernum;
                    cfa.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                }
                Online newwind = new Online(usernum);     //Online 窗体实例化 构造函数跨窗体传值
                newwind.Show();

                this.Close();   //关闭此窗口
            }
        }
        string remusername = ConfigurationManager.AppSettings["usernum"].ToString().Trim();
        private void MainWindow_Loaded(object sender, RoutedEventArgs e) //加载时判断是否连接网络
        {
            //FileInfo fi1 = new FileInfo(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Log.txt")); 
            //if (fi1.Exists==false)
            //{
            //    FileStream fs = new FileStream(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Log.txt"), FileMode.OpenOrCreate);
            //    string Log = "提醒框修改为每一小时提醒一次，未签退而关机或其他终止程序的行为发生不会记录签到时间。";
            //    int i = 82;
            //    byte[] br = new byte[i];
            //    br = System.Text.Encoding.Default.GetBytes(Log);
            //    fs.Write(br,0,i);
            //    fs.Close();
            //    System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + "Log.txt");
            //}
            //PcbsoftVersionToRegister pcbsoftVersionToRegister = new PcbsoftVersionToRegister();//自定义类，将版本号写入客户端注册表中
            //DataTable updata = new DataTable();
            //updata = SqlHelper.ExecuteDataTable("select FileName,FileVersion from T_Update ");
            ////DataSet ds = updateApplicationLogic.GetFilename();//这里是自定义方法，在数据库中存放升级文件的表中获取文件的名称 
            //if (updata.Rows.Count == 0) return; //如果无升级文件则不执行操作
            //try
            //{
            //    foreach (DataRow rowNew in updata.Rows)
            //    {
                  
            //        string filename = Convert.ToString(rowNew["FileName"]).Trim();//获取文件名 
            //        string fileversion = Convert.ToString(rowNew["FileVersion"]).Trim();//获取文件版本号                 
            //        string oldfileversion = pcbsoftVersionToRegister.ReadSoftVersion();//从注册表中读取文件版本信息                 
            //        if (oldfileversion.Length == 0) oldfileversion = "1.0.0.0";
            //        if (string.Compare(fileversion, oldfileversion) == 1)//判断是否有新版本
            //        {
            //            if (System.Windows.MessageBox.Show("软件有更新,是否升级?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //            {
            //                //foreach (Process p in Process.GetProcesses())
            //                //{
            //                //    if (p.ProcessName == "MR_Sign.exe")//A中写B。EXE，反之则反。
            //                //    {
            //                //        p.Kill();
            //                //        this.Close();

            //                //    }
            //                //}   
            //                new System.Threading.Thread(() => System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + "Updater.exe")).Start();//关闭主程序，启动升级程序



            //                //string fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "remove.bat");
            //                //StreamWriter bat = new StreamWriter(fileName, false, Encoding.Default);
            //                //bat.WriteLine(string.Format("del \"{0}\" /q", System.Windows.Forms.Application.ExecutablePath));
            //                //bat.WriteLine(string.Format("del \"{0}\" /q", fileName));
            //                //bat.Close();
            //                //ProcessStartInfo info = new ProcessStartInfo(fileName);
            //                //info.WindowStyle = ProcessWindowStyle.Hidden;
            //                //Process.Start(info);
            //                //Environment.Exit(0);

            //                Application.Current.Shutdown();
            //            }
            //            break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK);
            //}
            max.Interval = TimeSpan.FromMilliseconds(1);
            max.Tick += max_Tick;
            max.Start();
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("已有另一個签到系统在運行！", "提示信息");
                this.Close();
            }
              
            try
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                //檢測系統是否有這一進程存在，如果已有，則不允許再打開。
              
                SqlConnection conn = new SqlConnection(SqlHelper.connstr);
                conn.Open();
                //UserNum.Text = remusername;     
                //UserNum.Focus();
                //UserNum.Select(UserNum.Text.Length, 0);
            }
            catch
            {
                MessageBox.Show("未连接到网络！");
                this.Close();
            }

        }

        void max_Tick(object sender, EventArgs e)
        {
            if (this.WindowState==WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }
        //private int timespan = 0;   //这属于全局变量的声明嘛？？？？？？
        private void DragWindow(object sender, MouseButtonEventArgs e)   //允许拖动窗口
        {
            this.DragMove();
        }

        private void HypeLink1_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://59.72.194.42");
        }

        private void UserNum_KeyDown(object sender, KeyEventArgs e)//按键焦点触发
        {
            if (e.Key == Key.Enter)
                btsign_Click(sender, e);
        }

        private void UserNum_GotFocus(object sender, RoutedEventArgs e)
        {
            UserNum.Text = "";
        }

       
    

    }

}
