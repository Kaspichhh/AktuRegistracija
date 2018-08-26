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
using MySql.Data.MySqlClient;

namespace AktuApstrade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.lietotajs_textbox.Focus();
        }

        public bool isfilled()
        {
            if (lietotajs_textbox.Text != "" && passwordbox.Password.ToString() != "")
            {
                this.login_button.IsEnabled = true;
                return true;
            }
            else
            {
                this.login_button.IsEnabled = false;
                return false; 
            }
        } 

        private void checkuser(string lietotajs)
        {
            Dictionary<string,string> dbresponse = new Dictionary<string, string>();
            try
            {
                string MyConnection2 = "server=localhost; port=3306; database=aktuapstrade; username=root; password=qwerty123; SslMode=none";
                string Query = "SELECT `password` FROM `users` WHERE `username` = " +"'" +  lietotajs + "'";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();     
                MyReader2.Read(); 
                string a = MyReader2.GetString("password"); 
                if (MyReader2.GetString("password") == this.passwordbox.Password.ToString())
                {
                    UserWindowsView userWindowsView = new UserWindowsView();
                    userWindowsView.Show();
                    this.Close();
                }
                else
                {
                    this.wrongPasswordInfo.Visibility = Visibility.Visible;
                }
                MyConn2.Close();
            }
            catch (Exception e)
            {
                var errors = e;
            }
        }

        private void lietotajs_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isfilled();
        }

        private void passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            isfilled();
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            checkuser(this.lietotajs_textbox.Text.ToString());
        }
    }
}
