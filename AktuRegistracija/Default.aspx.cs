using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;

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
    public static bool Send_Field_Data(Dictionary<string,string> saraksts)
    {
        try
        {
            //This is my connection string i have assigned the database file address path  
            string MyConnection2 = "datasource=localhost;port=3307;username=root;password=root";
            //This is my insert query in which i am taking input from the user through windows forms  
            string Query = "insert into student.studentinfo(idStudentInfo,Name,Father_Name,Age,Semester) values('" + this.IdTextBox.Text + "','" + this.NameTextBox.Text + "','" + this.FnameTextBox.Text + "','" + this.AgeTextBox.Text + "','" + this.SemesterTextBox.Text + "');";
            //This is  MySqlConnection here i have created the object and pass my connection string.  
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            //This is command class which will handle the query and connection object.  
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
            MessageBox.Show("Save Data");
            while (MyReader2.Read())
            {
            }
            MyConn2.Close();
            return true;
        }
        catch(Exception ex)
        {
            return false; 
        }
    }
}