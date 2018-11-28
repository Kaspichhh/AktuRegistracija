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
            if(passwordHashing.isPasswordvalid(this.lietotajs_textbox.Text.ToString(), this.passwordbox.Password.ToString()) == true)
            {
                UserWindowsView userWindowsView = new UserWindowsView();
                userWindowsView.Show();
                this.Close();
            }
            else
            {
                this.wrongPasswordInfo.Visibility = Visibility.Visible;
                this.passwordbox.Clear();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
