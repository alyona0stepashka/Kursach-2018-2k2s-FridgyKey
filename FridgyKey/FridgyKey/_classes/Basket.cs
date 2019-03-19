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
    public class Basket
    {
        public static DataTable tbl;
        public static int count;

        public string product;
        public int amount;
        public string ei; 
        public static string query_insert = "insert into [tblBasket] ([frostID], [productID], [amount], [ei]) values (@frost,@name,@amount,@ei);";
        public static string query_update = "update [tblBasket] set [amount]=@amount, [productID]=@name, [ei]=@ei where [frostID]=@frost;";
        public static string query_delete = "delete from [tblBasket] where [amount]=@amount and [frostID]=@frost and [productID]=@name and [ei]=@ei;";
        public Basket(string prod, int am, string e)
        {
            product = prod;
            amount = am;
            ei = e; 
        }

        override public string ToString() //готово
        {
            SqlConnection sqlCon = clsDB.sqlCon;
            try
            { 
                string s = product + " (" + amount + " " + ei + " )";
                return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                //clsDB.Close_DB_Connection();
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
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
               // clsDB.Close_DB_Connection();
            }
        }       
        static public List<Basket> Get_product_by_frost_id() //готово
        {
            List<Basket> r = new List<Basket>();
            SqlConnection sqlCon = clsDB.sqlCon;
            try
            { 
                for (int j=0; j<count; j++)
                {
                    if ((int)tbl.Rows[j]["frostID"]==User.FrostID)
                    {
                        r.Add(new Basket(Product.Get_product_by_id((int)tbl.Rows[j]["productID"]), (int)tbl.Rows[j]["amount"], (string)tbl.Rows[j]["ei"]));
                    }
                }
                return r;  
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
        static public void Set_product(int _amount, string _ei, string name) //готово
        {
            try
            {
                var sql_con = clsDB.sqlCon;
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@name", Get_id_by_name(name));
                int i = User.FrostID;
                cmd2.Parameters.AddWithValue("@frost", i);
                cmd2.Parameters.AddWithValue("@amount", _amount);
                cmd2.Parameters.AddWithValue("@ei", _ei); 
                cmd2.ExecuteNonQuery();
                tbl = clsDB.Get_DataTable("select * from [tblBasket];");
                count++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        } 
        static public int Get_id_by_name(string name) //готово
        {
            try
            {
                return Product.Get_id_by_name(name);
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
        static public void Update_product(Basket f, int delta) //------
        {
            try
            {
                int _amount = f.amount + delta;
                var sql_con = clsDB.sqlCon;
                SqlCommand cmd2 = new SqlCommand(query_update, sql_con);
                cmd2.Parameters.AddWithValue("@name", Get_id_by_name(f.product));
                int i = User.FrostID;
                cmd2.Parameters.AddWithValue("@frost", i);
                cmd2.Parameters.AddWithValue("@amount", _amount);
                cmd2.Parameters.AddWithValue("@ei", f.ei); 
                cmd2.ExecuteNonQuery();
                tbl = clsDB.Get_DataTable("select * from [tblBasket];");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        }
        static public void Delete_product(Basket f) 
        {
            try
            {
                var sql_con = clsDB.sqlCon;
                SqlCommand cmd2 = new SqlCommand(query_delete, sql_con);
                cmd2.Parameters.AddWithValue("@name", Get_id_by_name(f.product));
                int i = User.FrostID;
                cmd2.Parameters.AddWithValue("@frost", i);
                cmd2.Parameters.AddWithValue("@amount", f.amount);
                cmd2.Parameters.AddWithValue("@ei", f.ei); 
                cmd2.ExecuteNonQuery();
                tbl = clsDB.Get_DataTable("select * from [tblBasket];");
                count--;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               // clsDB.Close_DB_Connection();
            }
        }
    }
}
