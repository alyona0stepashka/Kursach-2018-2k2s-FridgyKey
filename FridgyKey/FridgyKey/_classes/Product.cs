using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgyKey
{
    public class Product
    {
        public static DataTable tbl;
        public static int count;

        public string name;
        public string kkal;
        public static string query_insert = "insert into [tblKkal] ([name], [kkal]) values (@name,@amount);";
        static public int Get_koef(string name) //готово
        {  
            try
            {
                int i, count1 = Get_count();  
                for (i = 0; i < count1; i++)
                    if (((string)tbl.Rows[i]["name"])==name) break;
                    return (int)tbl.Rows[i]["kkal"]; 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return 1;
            }
            finally
            {
               // clsDB.Close_DB_Connection();
            }
        }
        static public string Get_product_by_id(int i) //готово
        {
            SqlConnection sqlCon = clsDB.sqlCon;
            try
            {
                string c = "";
                for (int j = 0; j < count; j++)
                {
                    if ((int)(tbl.Rows[j]["Id"]) == i)
                    {
                        c = (string)tbl.Rows[j]["name"];
                    }
                }
                return c;

                //DataTable dtf = clsDB.Get_DataTable("select [name] from [tblKkal] where [Id]="+i+";");

                //string s = (string)dtf.Rows[0][0];
                //return s;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        }
        static public int Get_id_by_name(string name) //готово
        {
            SqlConnection sqlCon = clsDB.sqlCon;
            try
            {
                int i, count1 = Get_count(); 
                for (i = 0; i <count1; i++)
                    if (((string)tbl.Rows[i]["name"]) == name) break;
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
        static public void Set_product(int _amount, string name) //готово
        { 
            try
            {
                var sql_con = clsDB.sqlCon;
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@name", name);
                cmd2.Parameters.AddWithValue("@amount", _amount);
                cmd2.ExecuteNonQuery();
                tbl = clsDB.Get_DataTable("select * from [tblKkal];");
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
