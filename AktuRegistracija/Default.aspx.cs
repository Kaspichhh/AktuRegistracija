using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;
using MySql.Data.MySqlClient; 


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    [WebMethod]
    public static bool ValidateBankAccount(string bankAccount)
    {
        bankAccount = bankAccount.ToUpper(); //IN ORDER TO COPE WITH THE REGEX BELOW
        if (String.IsNullOrEmpty(bankAccount))
            return false;
        else if (System.Text.RegularExpressions.Regex.IsMatch(bankAccount, "^[A-Z0-9]"))
        {
            bankAccount = bankAccount.Replace(" ", String.Empty);
            string bank =
            bankAccount.Substring(4, bankAccount.Length - 4) + bankAccount.Substring(0, 4);
            int asciiShift = 55;
            StringBuilder sb = new StringBuilder();
            foreach (char c in bank)
            {
                int v;
                if (Char.IsLetter(c)) v = c - asciiShift;
                else v = int.Parse(c.ToString());
                sb.Append(v);
            }
            string checkSumString = sb.ToString();
            int checksum = int.Parse(checkSumString.Substring(0, 1));
            for (int i = 1; i < checkSumString.Length; i++)
            {
                int v = int.Parse(checkSumString.Substring(i, 1));
                checksum *= 10;
                checksum += v;
                checksum %= 97;
            }
            return checksum == 1;
        }
        else
            return false;
    }

    [WebMethod]
    public static bool Send_Field_Data(Dictionary<string, string> saraksts)
    {
        try
        {
            //This is my connection string i have assigned the database file address path  
            string MyConnection2 = "server=localhost; port=3306; database=akturegistracijadb; username=root; password=qwerty123; SslMode=none";
            //This is my insert query in which i am taking input from the user through windows forms  
            string Query = "INSERT INTO aktudati (`Pasakuma Nosaukums`,`Pasakuma Datums`,`Vards Uzvards`,`Personas Kods`,`Kontakti`,`Bankas Konts`,`Swift Kods`,`Bankas Nosaukums`,`Biletes Cena`,`Bilesu ID`,`Bilesu Veidlapas Nummuri`,`Pirkuma Datums`,`Iegades vieta`,`Tirdzniecibas vieta`,`Cits info`,`Statuss`) VALUES (" + "'" + saraksts["0"] + "'," + "'" + saraksts["1"] + "'," + "'" + saraksts["2"] + "'," + "'" + saraksts["3"] + "'," + "'" + saraksts["4"] + "'," + "'" + saraksts["5"] + "'," + "'" + saraksts["6"] + "'," + "'" + saraksts["7"] + "'," + "'" + saraksts["8"] + "'," + "'" + saraksts["9"] + "'," + "'" + saraksts["10"] + "'," + "'" + saraksts["11"] + "'," + "'" + saraksts["12"] + "'," + "'" + saraksts["13"] + "'," + "'" + saraksts["14"] + "'," + "'Neapstrādāts');";
            //This is  MySqlConnection here i have created the object and pass my connection string
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            //This is command class which will handle the query and connection object.  
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
            MyConn2.Close();
            return true;
        }
        catch (Exception e)
        {
            var errors = e;
            return false;
        }
    }
}