using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace FridgyKey
{
    public static class clsDB
    {
        public static SqlConnection sqlCon;
        public static SqlConnection Get_DB_Connection() //готово
        {
            string cn_string = Properties.Settings.Default.connection_str;
            SqlConnection cn_connection = new SqlConnection(cn_string);

            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            return cn_connection;
        }
        public static DataTable Get_DataTable(string query) //готово
        {
            SqlConnection cn_connection = Get_DB_Connection();

            DataTable table = new DataTable(); 
            SqlDataAdapter adapter = new SqlDataAdapter(query, cn_connection);
            adapter.Fill(table);
          

            return table;
        }
        public static void Execute_SQL(string query) //готово
        {
            SqlConnection cn_connection = Get_DB_Connection();

            SqlCommand cmd_Command = new SqlCommand(query, cn_connection);
            cmd_Command.ExecuteNonQuery();
        }
        public static void Close_DB_Connection() //готово
        {
            string cn_string = Properties.Settings.Default.connection_str;
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Closed) cn_connection.Close();
        }



        public static string Hash(string input) //готово
        {
            byte[] hash = Encoding.ASCII.GetBytes(input);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string output = "";
            foreach (var b in hashenc)
            {
                output += b.ToString("x2");
            }
            return output;
        }
    }
}
