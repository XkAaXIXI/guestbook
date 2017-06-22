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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Diagnostics;
namespace guestbook
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 记得数据库的连接；对象要转换成字符串再读取；记得执行SQL语句！！！ 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //“我要留言”按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //连接数据库
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=guestbook");
            con.Open();
            string insertcommand = string.Format("insert into guestbook(guest) values('{0}')", guestBox.Text);
            MySqlCommand insertcmd = new MySqlCommand(insertcommand, con);// 把command通过con去执行
            insertcmd.ExecuteNonQuery();// 执行SQL语句 
            con.Close();// 关闭数据库
            con.Dispose();// 释放内存
            MessageBox.Show("留言成功！");
            guestBox.Text = "";
            idBox.Text = "";
        }

        //“查看留言”按钮

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           try
           {
                //连接数据库
                MySqlConnection con = new MySqlConnection("server=localhost;userid=root;password=;database=guestbook");
                con.Open();
                string idNumber = idBox.Text;
               int I = Convert.ToInt32(idNumber);
                string command = "select id,guest from guestbook where id=" + I;
                MySqlCommand cmd = new MySqlCommand(command, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string res = reader.GetString(0);
                    idNumber = reader["id"].ToString();
                    string guest = reader["guest"] + "";
                    //显示id,guest在UI上
                    id1Box.Text = idNumber;
                    guest1Box.Text = guest;
                }
                else
                {
                    MessageBox.Show("此ID不存在！请重新输入ID！");
                }
                reader.Close();
                idBox.Text = "";
                guestBox.Text = "";
                con.Close();
                con.Dispose();
            }
           catch(Exception  ex)
            {
                Debug.WriteLine(ex);
            }
        }
        
        //“修改留言”按键
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=guestbook");
            con.Open();
            string idNumber = idBox.Text;
            int I = Convert.ToInt32(idNumber);
            string updateguest = guestBox.Text;
            // 从查看留言得到ID并修改对应的留言内容。
            string command = "update guestbook set guest = '"+updateguest+"' where id="+I;
            MySqlCommand updatacmd = new MySqlCommand(command, con);
            updatacmd.ExecuteNonQuery();// 执行SQL语句
            MessageBox.Show("修改成功!");
            guestBox.Text = "";
            idBox.Text = "";
            guest1Box.Text = "";
            id1Box.Text = "";
        }

        // “删除留言”按键
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=guestbook");
            con.Open();
            string idNumber = idBox.Text;
            int I = Convert.ToInt32(idNumber);
            string deleteguest = guestBox.Text;
            // 从查看留言得到ID并删除对应的留言内容。
            string command = "delete from guestbook  where id="+I;
            MySqlCommand deletecmd = new MySqlCommand(command, con);
            deletecmd.ExecuteNonQuery();// 执行SQL语句
            MessageBox.Show("删除成功!");
            guestBox.Text = "";
            idBox.Text = "";
            guest1Box.Text = "";
            id1Box.Text = "";
        }
    }
}
