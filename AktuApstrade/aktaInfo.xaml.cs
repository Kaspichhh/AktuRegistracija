using System;
using System.IO;
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
using MySql.Data.MySqlClient;
using Spire.Doc;

namespace AktuApstrade
{
    /// <summary>
    /// Interaction logic for aktaInfo.xaml
    /// </summary>
    public partial class aktaInfo : Window
    {
        static string exePath = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
        string samplePath = System.IO.Path.GetDirectoryName(exePath) + "\\Files\\sagatave.doc";
        string docxPath = System.IO.Path.GetDirectoryName(exePath) + "\\Files\\result.docx";
        Document document = null;

        public aktaInfo(string contentGUID)
        {
            InitializeComponent();
            fillFields(contentGUID);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public void fillFields(string guid)
        {
            try
            {
                string MyConnection2 = "server=localhost; port=3306; database=akturegistracijadb; username=root; password=qwerty123; SslMode=none";
                string query = "SELECT * FROM `aktudati` WHERE `GUID` = " + "'" + guid + "'";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                while (MyReader2.Read())
                {

                    this.pas_nos_text.Text = MyReader2.GetString("Pasakuma Nosaukums");
                    this.pas_dat_text.Text = MyReader2.GetString("Pasakuma Datums");
                    this.vards_uzvards_text.Text = MyReader2.GetString("Vards Uzvards");
                    this.personas_kods_text.Text = MyReader2.GetString("Personas Kods");
                    this.mail_text.Text = MyReader2.GetString("Epasts");
                    this.tel_tex.Text = MyReader2.GetString("Telefona NR");
                    this.cena_text.Text = MyReader2.GetString("Biletes Cena");
                    this.id_text.Text = MyReader2.GetString("Bilesu ID");
                    this.bil_id_nr.Text = MyReader2.GetString("Bilesu Veidlapas Nummuri");
                    this.pirk_dat_text.Text = MyReader2.GetString("Pirkuma Datums");
                    this.ieg_viet_text.Text = MyReader2.GetString("Iegades vieta");
                    this.tirdz_punkta_text.Text = MyReader2.GetString("Tirdzniecibas vieta");
                    this.other_info_text.Text = MyReader2.GetString("Cits info");
                    this.konta_text.Text = MyReader2.GetString("Bankas Konts");
                    this.swift_text.Text = MyReader2.GetString("Swift Kods");
                    this.bank_nos_text.Text = MyReader2.GetString("Bankas Nosaukums");
                    this.guid_text.Text = MyReader2.GetString("GUID");
                    this.aizpildisanas_datums_text.Text = MyReader2.GetString("Aizpildisanas datums");
                    if (MyReader2.GetString("Statuss") == "Apstrādāts")
                    {
                        this.apstradats_checkbox.IsChecked = true;
                    }
                    else
                    {
                        this.apstradats_checkbox.IsChecked = false;
                    }
                }
                MyReader2.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void aizvert_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saglabat_button_Click(object sender, RoutedEventArgs e)
        {
            string connection_guid = this.guid_text.Text;
            string MyConnection2 = "server=localhost; port=3306; database=akturegistracijadb; username=root; password=qwerty123; SslMode=none";
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            if (this.apstradats_checkbox.IsChecked == true)
            {
                string query = "UPDATE `aktudati` SET `statuss` = 'Apstrādāts' WHERE `GUID` = " + "'" + connection_guid + "'";
                MySqlCommand MyCommand2 = new MySqlCommand(query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MyConn2.Close();
            }
            else
            {
                string query = "UPDATE `aktudati` SET `Statuss` = 'Neapstrādāts' WHERE `GUID` = " + "'" + connection_guid + "'";
                MySqlCommand MyCommand2 = new MySqlCommand(query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MyConn2.Close();
            }
        }

        private string getNextFileName(string fileName)
        {
            int i = 0;
            while (File.Exists(fileName))
            {
                if (i == 0)
                    fileName = fileName.Replace("docx", "(" + ++i + ")" + "docx");
                else
                    fileName = fileName.Replace("(" + i + ")" + "docx", "(" + ++i + ")" + "docx");
            }

            return fileName;
        }

        private void drukat_button_Click(object sender, RoutedEventArgs e)
        {
            document = new Document();
            document.LoadFromFile(samplePath);
            Dictionary<string, string> dictReplace = GetReplaceDictionary();
            foreach (KeyValuePair<string, string> kvp in dictReplace)
            {
                document.Replace(kvp.Key, kvp.Value, true, true);
            }
            document.SaveToFile(docxPath, FileFormat.Docx);
            document.Close();
            ToViewFile(docxPath);
        }

        Dictionary<string, string> GetReplaceDictionary()
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Add("#nosaukums#", this.pas_nos_text.Text);
            replaceDict.Add("#datums#", this.pas_dat_text.Text);
            replaceDict.Add("#vards_uzvards#", this.vards_uzvards_text.Text);
            replaceDict.Add("#personas_kods#", this.personas_kods_text.Text);
            replaceDict.Add("#epasts#", this.mail_text.Text);
            replaceDict.Add("#telnr#", this.tel_tex.Text);
            replaceDict.Add("#konts#", this.konta_text.Text);
            replaceDict.Add("#swift#", this.swift_text.Text);
            replaceDict.Add("#bank_nosaukums#", this.bank_nos_text.Text);
            replaceDict.Add("#cena#", this.cena_text.Text);
            replaceDict.Add("#bilesu_id#", this.id_text.Text);
            replaceDict.Add("#veid_nr#", this.bil_id_nr.Text);
            replaceDict.Add("#pirk_dat#", this.pirk_dat_text.Text);
            replaceDict.Add("#ieg_viet#", this.ieg_viet_text.Text);
            replaceDict.Add("#tirdz_punkts#", this.tirdz_punkta_text.Text);
            replaceDict.Add("#cits_info#", this.other_info_text.Text);
            replaceDict.Add("#aizpildisanas_datums#", this.aizpildisanas_datums_text.Text);
            return replaceDict;
        }
        private void ToViewFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
