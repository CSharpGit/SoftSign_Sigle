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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace Updater
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                l1.Content = "正在更新，请稍后";
                l2.Content = "萌哒哒的说一句这时候无论出现什么状况都不要乱点哦！";
             
                PcbsoftVersionToRegister pcbsoftVersionToRegister = new PcbsoftVersionToRegister();
                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(2000);
                
                DataTable tb = new DataTable();
                tb = SqlHelper.ExecuteDataTable("select * from T_Update");
                foreach (DataRow rowNew in tb.Rows)
                {
                    string filename = Convert.ToString(rowNew["FileName"]);
                    string fileversion = Convert.ToString(rowNew["FileVersion"]);//自定义方法，获取更新文件的版本号,从数据库中          
                    byte[] buffer = new Byte[0];
                    buffer = (Byte[])rowNew["FileContent"];
                    FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + filename, FileMode.OpenOrCreate); fs.Write(buffer, 0, buffer.Length);
                    fs.Close();                           //将当前文件的版本写入注册表          
                    pcbsoftVersionToRegister.WriteSoftVersion( fileversion);
                }
               System.Windows.Forms. MessageBox.Show("更新完成.", "提示", MessageBoxButtons.OK);
                this.Close();
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + "SoftSign_Sigle.exe");//关闭升级程序，运行主程序   
            }
            catch (Exception ex)
            {
               System.Windows. MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK);
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
