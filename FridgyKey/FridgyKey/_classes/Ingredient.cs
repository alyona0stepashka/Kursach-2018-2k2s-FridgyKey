using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgyKey
{
    public class Ingredient
    {
        public static DataTable tbl;
        public static int count;

        public int amount; //кол-во
        public string ei; //единицы измерения
        public string product;
        public static string query_insert = "insert into [tblRecipeIn]([productID], [amount], [ei], [kolvo]) values(@productID,@amount,@ei,@kolvo)";
        public Ingredient(int a, string e, string p)
        {
            amount = a;
            ei = e;
            product = p;
        }

        override public string ToString()
        {
            return product + " " + amount + " " + ei;
        }

        static public int Get_id_ing_by_id_recipe(int id)
        { 
            try
            {
                int c = 0;
                for (int i=0; i<Recipe.count; i++)
                {
                    if ((int)(Recipe.tbl.Rows[i]["Id"])==id)
                    {
                        c = (int)Recipe.tbl.Rows[i]["ingredID"];
                        break;
                    }
                }
                return c; 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
              //  clsDB.Close_DB_Connection();
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
        static public int Get_count_by_id(int j)
        { 
            try
            {
                int c = 0;
                for (int i=0; i<count; i++)
                {
                    if ((int)(tbl.Rows[i]["Id"]) == j)
                    {
                        c = (int)tbl.Rows[i]["kolvo"];
                    }
                }
                return c; 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
              //  clsDB.Close_DB_Connection();
            }
        }
        static public List<Ingredient> Get_list_ingredients(int id)
        { 
            try
            {
                List<Ingredient> l = new List<Ingredient>();
                bool f3 = false;

                int i, count = Get_count_by_id(id), count1 = Get_count();  
                for (i = 0; i < count1; i++)
                    if (((int)tbl.Rows[i]["ID"]) == id) { f3 = true; break; }
                else { f3 = false;  };
                if (f3 == true)
                {
                    for (int z=(i); z<(i+count); z++)
                    {
                        Ingredient ing = new Ingredient((int)tbl.Rows[z]["amount"], (string)tbl.Rows[z]["ei"], Product.Get_product_by_id((int)tbl.Rows[z]["productID"]));
                        l.Add(ing);
                    }
                    return l;
                }
                else return null; 
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
        public static int Get_last_id()
        {
            SqlConnection sqlCon = clsDB.Get_DB_Connection();
            try
            { 
                int count = Get_count();
                if (count == 0)
                {
                    return 0;
                }
                else
                {
                    int count_last = (int)tbl.Rows[(count - 1)][0];
                    return count_last;
                }
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
        public static List<string> Get_ing_info(int id)
        {
            try
            {
                List<string> l = new List<string>();
                List<Ingredient> li = Get_list_ingredients(id);
                foreach (Ingredient r in li)
                    l.Add(r.product + " " + r.amount + " " + r.ei);
                return l;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            } 
        }
        public static int Set_list_ingredients(List<Ingredient> list)
        { 
            try
            {                
                int l_id = Get_last_id();
                int ccount = list.Count;
                var sql_con = clsDB.sqlCon;
                foreach (Ingredient r in list)
                {
                    SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                    cmd2.Parameters.AddWithValue("@productID", Product.Get_id_by_name(r.product));
                    cmd2.Parameters.AddWithValue("@amount", r.amount);
                    cmd2.Parameters.AddWithValue("@ei", r.ei);
                    cmd2.Parameters.AddWithValue("@kolvo", ccount);
                    cmd2.ExecuteNonQuery(); 
                }
                tbl = clsDB.Get_DataTable("select * from [tblRecipeIn];");
                count += ccount;
                return (l_id+1);
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
    }
}
