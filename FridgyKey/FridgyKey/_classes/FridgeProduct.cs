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
    public class FridgeProduct
    {
        public static DataTable tbl;
        public static int count;

        public string product;
        public int amount;
        public string ei;
        public DateTime val;
        public static string query_insert = "insert into [tblFrost] ([frostID], [productID], [amount], [ei], [valid]) values (@frost,@name,@amount,@ei,@valid);";
        public static string query_update = "update [tblFrost] set [amount]=@amount where [frostID]=@frost and [productID]=@name and [ei]=@ei;"; // and [valid]=@valid
        public static string query_delete = "delete from [tblFrost] where [amount]=@amount and [frostID]=@frost and [productID]=@name and [ei]=@ei;"; // and [valid]=@valid
        public FridgeProduct(string prod, int am, string e, DateTime dt)
        {
            product = prod;
            amount = am;
            ei = e;
            val = dt;
        }

        override public string ToString() //готово
        {
            SqlConnection sqlCon = clsDB.sqlCon;
            try
            {
                string add = "";
                if (val <= DateTime.Now) { add = "!!! "; }
                string s = add + product + " (" + amount + " " + ei + " " + (String.Format("{0}.{1}.{2}", val.Day, val.Month, val.Year) + " )");
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

        public static bool Is_have(int prod_id)
        {
            try
            {
                bool f = false;
                for (int j = 0; j < count; j++)
                {
                    if ((int)tbl.Rows[j]["frostID"] == User.FrostID)
                    {
                        if ((int)tbl.Rows[j][2]==prod_id)
                        {
                            f = true;
                            break;
                        }
                    }
                }
                return f;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
               // clsDB.Close_DB_Connection();
            }
        }       
        static public List<FridgeProduct> Get_product_by_frost_id() //готово
        {
            List<FridgeProduct> r = new List<FridgeProduct>();
            SqlConnection sqlCon = clsDB.sqlCon;
            try
            { 
                for (int j=0; j<count; j++)
                {
                    if ((int)tbl.Rows[j]["frostID"]==User.FrostID)
                    {
                        r.Add(new FridgeProduct(Product.Get_product_by_id((int)tbl.Rows[j]["productID"]), (int)tbl.Rows[j]["amount"], (string)tbl.Rows[j]["ei"], (DateTime)tbl.Rows[j]["valid"]));
                    }
                }
                return r; 

                //FridgeProduct r = new FridgeProduct();
                //DataTable dt = clsDB.Get_DataTable("select * from [tblFrost] where [frostID]=" + User.FrostID + ";");
                //DataTable dtf = clsDB.Get_DataTable("select * from [tblKkal] where [id]=" + (int)dt.Rows[i]["productID"] + ";");

                //r.val = (DateTime)dt.Rows[i]["valid"];
                //r.product = (string)dtf.Rows[0]["name"];
                //r.amount = (int)dt.Rows[i]["amount"];
                //r.ei = (string)dt.Rows[i]["ei"];
                //return r;
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
        static public void Set_product(int _amount, string _ei, DateTime _valid, string name) //готово
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
                cmd2.Parameters.AddWithValue("@valid", _valid);
                cmd2.ExecuteNonQuery();
                tbl = clsDB.Get_DataTable("select * from [tblFrost];");
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
        static public void Update_product(FridgeProduct f, int delta) 
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
                tbl = clsDB.Get_DataTable("select * from [tblFrost];");
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
        static public void Delete_product(FridgeProduct f) 
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
                tbl = clsDB.Get_DataTable("select * from [tblFrost];");
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
