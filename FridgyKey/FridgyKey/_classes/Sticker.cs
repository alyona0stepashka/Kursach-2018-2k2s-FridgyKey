using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgyKey 
{
    public static class Sticker
    {
        static public List<string> username;
        static public List<string> text;

        static string query_insert = "insert into [tblSticker] ([user], [text], [frostID]) values (@username,@text,@frostid);";
        static public int Get_count()
        {
            SqlConnection sqlCon = clsDB.Get_DB_Connection();
            try
            {
                DataTable dt2 = clsDB.Get_DataTable("select count(*) from tblSticker where frostID=" + User.FrostID + ";");
                int count = (int)dt2.Rows[0][0];
                return count;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                clsDB.Close_DB_Connection();
            }
        }
        static public string Get_message(int i)
        {
            SqlConnection sqlCon = clsDB.Get_DB_Connection();
            try
            {
                DataTable dt = clsDB.Get_DataTable("select * from [tblSticker] where [frostID]=" + User.FrostID + ";");
                string s = (string)dt.Rows[i]["user"] + ": " + (string)dt.Rows[i]["text"];
                return s;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                clsDB.Close_DB_Connection();
            }
        }
        static public void Set_message(string text)
        { 
            try
            {
                SqlConnection cn_connection = clsDB.Get_DB_Connection(); 
                SqlCommand cmd_Command = new SqlCommand(query_insert, cn_connection);
                cmd_Command.Parameters.AddWithValue("@username", User.Username);
                cmd_Command.Parameters.AddWithValue("@text", text);
                cmd_Command.Parameters.AddWithValue("@frostid", User.FrostID);
                cmd_Command.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                clsDB.Close_DB_Connection();
            }
        }
    }
}
