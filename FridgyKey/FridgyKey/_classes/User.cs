using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FridgyKey
{
    public static class User
    {
        public static DataTable tbl;
        public static int count;
         
        static public string Username;
        static public int FrostID;
        public static string query_insert = "insert into [tblUser] ([username], [password], [frostID]) values (@Username, @Password, @FrostID)";

        static public int Get_count() //готово
        { 
            try
            { 
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
        static public string Get_name_by_id(int id)
        { 
            try
            {
                string c = "";
                for (int i = 0; i < count; i++)
                {
                    if ((int)(tbl.Rows[i]["Id"]) == id)
                    {
                        c = (string)tbl.Rows[i]["username"];
                    }
                }
                return c;


                //DataTable dt2 = clsDB.Get_DataTable("select [username] from [tblUser] where [Id]=" + id + ";");
                //string s = (string)dt2.Rows[0][0];
                //return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
               // clsDB.Close_DB_Connection();
            }
        }
        static public int Get_id_by_name(string name) //готово
        { 
            try
            {
                int i; 
                for (i = 0; i < Get_count(); i++)
                    if (((string)tbl.Rows[i]["username"]) == name) break;
                return (int)tbl.Rows[i]["id"];
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
        static public int Get_frost_id_by_name(string name) //готово
        {
            try
            {
                int i;
                for (i = 0; i < Get_count(); i++)
                    if (((string)tbl.Rows[i]["username"]) == name) break;
                return (int)tbl.Rows[i]["frostID"];
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
        public static int Get_max_frostid()
        { 
            try
            { 
                List<int> l = new List<int>();
                int count1 = Get_count();
                for (int j=0;j<count1;j++)
                {
                    l.Add((int)tbl.Rows[j]["frostID"]);
                }
                return l.Max(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        }
        public static void Set_User(int f, string n, string h, int id)
        {
            var sqlCon = clsDB.sqlCon;
            try
            {
                if (f==1)
                { 
                    SqlCommand sqlCmd = new SqlCommand(query_insert, sqlCon); 
                    sqlCmd.Parameters.AddWithValue("@Username", n);
                    sqlCmd.Parameters.AddWithValue("@Password", h);
                    sqlCmd.Parameters.AddWithValue("@FrostID", id);
                    sqlCmd.ExecuteNonQuery();
                    Username = n;
                    FrostID = id;
                }
                else if (f==-1)
                { 
                    SqlCommand sqlCmd = new SqlCommand(query_insert, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Username", n);
                    sqlCmd.Parameters.AddWithValue("@Password", h); 
                    sqlCmd.Parameters.AddWithValue("@FrostID", id);
                    sqlCmd.ExecuteNonQuery();
                    Username = n;
                    FrostID = id;
                }
                tbl = clsDB.Get_DataTable("select * from [tblUser];");
                count++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               // sqlCon.Close();
            }
        }
    }
}
