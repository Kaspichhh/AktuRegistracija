using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using MailKit.Net.Smtp;
using MimeKit;



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
    public static bool Send_Field_Data(Dictionary<string, string> saraksts, string mail)
    {
        try
        {
            //This is my connection string i have assigned the database file address path  
            string MyConnection2 = "server=localhost; port=3306; database=akturegistracijadb; username=root; password=qwerty123; SslMode=none";
            //This is my insert query in which i am taking input from the user through windows forms  
            string Query = "INSERT INTO aktudati (`GUID`,`Pasakuma Nosaukums`,`Pasakuma Datums`,`Vards Uzvards`,`Personas Kods`,`Epasts`,`Telefona NR`,`Bankas Konts`,`Swift Kods`,`Bankas Nosaukums`,`Biletes Cena`,`Bilesu ID`,`Bilesu Veidlapas Nummuri`,`Pirkuma Datums`,`Iegades vieta`,`Tirdzniecibas vieta`,`Cits info`,`Aizpildisanas datums`,`Statuss`) VALUES (" + "'"+ Guid.NewGuid() + "'" + ",'" + saraksts["0"] + "'," + "'" + saraksts["1"] + "'," + "'" + saraksts["2"] + "'," + "'" + saraksts["3"] + "'," + "'" + saraksts["4"] + "'," + "'" + saraksts["5"] + "'," + "'" + saraksts["6"] + "'," + "'" + saraksts["7"] + "'," + "'" + saraksts["8"] + "'," + "'" + saraksts["9"] + "'," + "'" + saraksts["10"] + "'," + "'" + saraksts["11"] + "'," + "'" + saraksts["12"] + "'," + "'" + saraksts["13"] + "'," + "'" + saraksts["14"] + "'," + "'" + saraksts["15"] + "'," + "'" + saraksts["16"] + "'," + "'Neapstrādāts');";
            //This is  MySqlConnection here i have created the object and pass my connection string
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            //This is command class which will handle the query and connection object.  
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
            MyConn2.Close();
            try
            {
                SendEmail(mail, saraksts["2"].ToString());
                return true;
            }
            catch (Exception e)
            {
                var errors = e; 
                return false; 
            }

            
        }
        catch (Exception e)
        {
            var errors = e;
            return false;
        }
    }
    [WebMethod]
    public static bool IsEmail(string inputEmail)
    {
           Regex re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase);
           bool a = re.IsMatch(inputEmail);
       return a; 
    }

    public static bool SendEmail(string email, string towho) 
    {
        try
        {
            //Message part
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Naudas atgriešana", "kaspars@bilesuserviss.lv"));
            message.To.Add(new MailboxAddress(email));
            message.Subject = "No-Reply Email";
            var body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns:m='http://schemas.microsoft.com/office/2004/12/omml' xmlns='http://www.w3.org/TR/REC-html40'><head><meta name=Generator content='Microsoft Word 15 (filtered medium)'><style><!--/* Font Definitions */@font-face	{font-family:'Cambria Math';	panose-1:2 4 5 3 5 4 6 3 2 4;}@font-face	{font-family:Calibri;	panose-1:2 15 5 2 2 2 4 3 2 4;}@font-face	{font-family:Verdana;	panose-1:2 11 6 4 3 5 4 4 2 4;}/* Style Definitions */p.MsoNormal, li.MsoNormal, div.MsoNormal	{margin:0in;	margin-bottom:.0001pt;	font-size:11.0pt;	font-family:'Calibri',sans-serif;	mso-fareast-language:EN-US;}a:link, span.MsoHyperlink	{mso-style-priority:99;	color:#0563C1;	text-decoration:underline;}a:visited, span.MsoHyperlinkFollowed	{mso-style-priority:99;	color:#954F72;	text-decoration:underline;}p.msonormal0, li.msonormal0, div.msonormal0	{mso-style-name:msonormal;	mso-margin-top-alt:auto;	margin-right:0in;	mso-margin-bottom-alt:auto;	margin-left:0in;	font-size:11.0pt;	font-family:'Calibri',sans-serif;}span.EmailStyle18	{mso-style-type:personal;	font-family:'Calibri',sans-serif;	color:windowtext;}span.EmailStyle19	{mso-style-type:personal;	font-family:'Calibri',sans-serif;	color:windowtext;}span.EmailStyle20	{mso-style-type:personal;	font-family:'Calibri',sans-serif;	color:windowtext;}span.EmailStyle21	{mso-style-type:personal-compose;	font-family:'Calibri',sans-serif;	color:windowtext;}.MsoChpDefault	{mso-style-type:export-only;	font-size:10.0pt;}@page WordSection1	{size:8.5in 11.0in;	margin:1.0in 1.0in 1.0in 1.0in;}div.WordSection1	{page:WordSection1;}--></style><!--[if gte mso 9]><xml><o:shapedefaults v:ext='edit' spidmax='1026' /></xml><![endif]--><!--[if gte mso 9]><xml><o:shapelayout v:ext='edit'><o:idmap v:ext='edit' data='1' /></o:shapelayout></xml><![endif]--></head><body lang=EN-GB link='#0563C1' vlink='#954F72'><div class=WordSection1><p class=MsoNormal><span lang=LV style='font-size:9.0pt;font-family:'Verdana',sans-serif'>Sveiki "+ towho + ", <o:p></o:p></span></p><p class=MsoNormal><span lang=LV style='font-size:9.0pt;font-family:'Verdana',sans-serif'>Paldies, ka aizpildījāt aktu, dati veiksmīgi saņemti un ar jums sazināsies tuvāko 5 darba dienu laikā.</span><span style='font-size:9.0pt;font-family:'Verdana',sans-serif'><o:p></o:p></span></p><p class=MsoNormal><span lang=LV style='font-size:9.0pt;font-family:'Verdana',sans-serif'>Neskaidrību gadījumā, lūdzu, sazinieties ar mums pa tālāk norādīto kontaktinformāciju.</span><span style='font-size:9.0pt;font-family:'Verdana',sans-serif'><o:p></o:p></span></p><p class=MsoNormal><span lang=LV style='mso-fareast-language:EN-GB'>&nbsp;</span><o:p></o:p></p><p class=MsoNormal style='line-height:115%'><b><span style='font-size:9.0pt;line-height:115%;font-family:'Verdana',sans-serif'>Ar cieņu,<br></span></b><span style='font-size:9.0pt;line-height:115%;font-family:'Verdana',sans-serif'><br></span><b><span style='font-size:10.0pt;line-height:115%;font-family:'Verdana',sans-serif;color:#C00000'>SIA BIĻEŠU SERVISS</span></b><span style='font-size:9.0pt;line-height:115%;font-family:'Verdana',sans-serif'>&nbsp;<br><b>Tālr.:</b> +371 66100335<br><b>E-pasts:</b> <a href='mailto:info@bilesuserviss.lv'>info@bilesuserviss.lv</a><br><a href='http://www.bilesuserviss.lv/'><b><span style='font-size:10.0pt;line-height:115%;color:#C00000'>www.bilesuserviss.lv</span></b></a><o:p></o:p></span></p><p class=MsoNormal><span lang=LV style='font-size:10.0pt;font-family:'Verdana',sans-serif'>&nbsp;</span><span style='font-size:10.0pt;font-family:'Verdana',sans-serif'><o:p></o:p></span></p><p class=MsoNormal><span lang=LV style='font-size:10.0pt;font-family:'Verdana',sans-serif'>&nbsp;</span><span style='font-size:10.0pt;font-family:'Verdana',sans-serif'><o:p></o:p></span></p><p class=MsoNormal><span lang=LV>&nbsp;</span><o:p></o:p></p><p class=MsoNormal><span lang=LV>&nbsp;</span><o:p></o:p></p></div></body></html>"
            };

            //Message attachment part

            //var attachment = new MimePart()
            //    {

                    
            //    };

            var multipart = new Multipart("mixed");
            multipart.Add(body);
            //multipart.Add(attachment);
            message.Body = multipart;

            //Part where message is sent
            var client = new SmtpClient();
            client.Connect("smtp.spx.lv", 465, true);
            client.Authenticate("kaspars@bilesuserviss.lv", "PAROLE");
            client.Send(message);
            client.Disconnect(true);
            return true;
        }
        catch (Exception e)
        {
            var errors = e;
            return false;
        }

    }
}
