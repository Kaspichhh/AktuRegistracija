using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient; 

namespace AktuApstrade
{
    class passwordHashing
    {

        public static string CreateStrongHash(string textToHash)
        {
            byte[] salt = System.Text.Encoding.ASCII.GetBytes("BSSALT!@#$%^");
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(textToHash, salt, 1000);
            var encryptor = SHA512.Create();
            var hash = encryptor.ComputeHash(k1.GetBytes(16));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static bool isPasswordvalid(string lietotajs, string parole)
        {
            string hash = CreateStrongHash(parole);
            try
            {

                string MyConnection2 = "server=localhost; port=3306; database=aktuapstrade; username=root; password=qwerty123; SslMode=none";
                string Query = "SELECT `password` FROM `users` WHERE `username` = " + "'" + lietotajs + "'";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MyReader2.Read();
                string a = MyReader2.GetString("password");
                    if (MyReader2.GetString("password") == hash)
                    {
                        MyConn2.Close();
                        return true; 
                    }
                    else
                    {  
                        MyConn2.Close();
                        return false;
                    }
                
            }
            catch (Exception errors)
            {
                var a = errors;
                return false; 
            }
        }
    }
}
