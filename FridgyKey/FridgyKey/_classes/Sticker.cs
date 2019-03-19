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
        public static DataTable tbl;
        public static int count;

        public static string query_insert = "insert into [tblSticker] ([userID], [text], [frostID]) values (@username,@text,@frostid);";
        public static string query_delete = "delete from [tblSticker] where [frostID]=@frostid;";
        static public List<string> username;
        static public List<string> text;

        static public void Delete_Story() //готово
        {
            try
            {
                var sql_con = clsDB.sqlCon;
                SqlCommand cmd2 = new SqlCommand(query_delete, sql_con); 
                cmd2.Parameters.AddWithValue("@frostid", User.FrostID); 
                cmd2.ExecuteNonQuery();
                tbl = clsDB.Get_DataTable("select * from [tblSticker];");
                SqlCommand sqlCmd5 = new SqlCommand("select count(1) from [tblSticker]", clsDB.sqlCon);
                count = Convert.ToInt32(sqlCmd5.ExecuteScalar());
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        }

            static public int Get_count()  //готово
        {
            SqlConnection sqlCon = clsDB.sqlCon;
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
               // clsDB.Close_DB_Connection();
            }
        }
        static public List<string> Get_message() //готово
        {
            List<string> ls = new List<string>();
            try
            {
                int countt = count;
                for (int j = 0; j < countt; j++)
                {
                    if ((int)(tbl.Rows[j]["frostID"]) == User.FrostID)
                    {
                        ls.Add(User.Get_name_by_id((int)tbl.Rows[j]["userID"]) + ": " + (string)tbl.Rows[j]["text"]);
                    }
                }
                return ls;

                //DataTable dt = clsDB.Get_DataTable("select * from [tblSticker] where [frostID]="+User.FrostID+";"); 
                //string s = User.Get_name_by_id((int)dt.Rows[i]["userID"]) +": " + (string)dt.Rows[i]["text"];
                //return s;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
               // clsDB.Close_DB_Connection();
            }
        }
        static public void Set_message(string text) //готово
        {
            try
            {
                var sql_con = clsDB.sqlCon;
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@username", User.Get_id_by_name(User.Username));
                cmd2.Parameters.AddWithValue("@frostid", User.FrostID);
                cmd2.Parameters.AddWithValue("@text", text); 
                cmd2.ExecuteNonQuery();
                tbl = clsDB.Get_DataTable("select * from [tblSticker];");
                count++;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        }
    }
}
