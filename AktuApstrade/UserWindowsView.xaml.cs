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
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace AktuApstrade
{
    /// <summary>
    /// Interaction logic for UserWindowsView.xaml
    /// </summary>
    public partial class UserWindowsView : Window
    {
        public UserWindowsView()
        {
            InitializeComponent();
            //System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            //ni.Icon = new System.Drawing.Icon(System.AppDomain.CurrentDomain.BaseDirectory + "/Images/icon.ico");
            //ni.Visible = true;
            //ni.DoubleClick +=
            //    delegate (object sender, EventArgs args)
            //    {
            //        this.Show();
            //        this.WindowState = WindowState.Normal;
            //    };
        }
        //protected override void OnStateChanged(EventArgs e)
        //{
        //    if (WindowState == System.Windows.WindowState.Minimized)
        //        this.Hide();

        //    base.OnStateChanged(e);
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadData(this.statusComboBox.Text.ToString());
        }

        public void loadData(string statuss)
        {
            try
            {
                string connectionString = "server=localhost; port=3306; database=akturegistracijadb; username=root; password=qwerty123; SslMode=none";
                string query = "SELECT `Pasakuma Nosaukums`,`Pasakuma Datums`,`Vards Uzvards`,`Epasts`,`Telefona NR`,`Statuss`, `GUID`,`Aizpildisanas datums` FROM `aktudati` WHERE `Statuss` = " + "'" + statuss + "'";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {

                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(command);
                        da.Fill(dt);
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    conn.Close();
                    dataGrid.Columns[6].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentRowIndex = dataGrid.Items.IndexOf(dataGrid.SelectedItem);
            {
                if (dataGrid.SelectedItem != null)
                {
                    DataRowView row = dataGrid.SelectedItem as DataRowView;
                    string targetGUID = row.Row.ItemArray[6].ToString();
                    aktaInfo info = new aktaInfo(targetGUID, false);
                    info.Show();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            aktaInfo new_info = new aktaInfo(Guid.NewGuid().ToString(), true);
            new_info.Show(); 
        }

        private void statusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //loadData(statusComboBox.Text.ToString());
            }
            catch (Exception errors)
            {
                MessageBox.Show(errors.ToString());
            }
        }
    }
}
