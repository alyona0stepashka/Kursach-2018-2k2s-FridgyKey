using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FridgyKey
{
    public class Recipe
    {
        public static DataTable tbl;
        public static int count;

        public string name;
        public string text;
        public Byte[] ico;
        public struct Good
        {
            public Recipe recipe;
            public double massa;
        }
        public BitmapImage icon { get; }
        public string notation;
        public List<Ingredient> list_ingredients;
        public static string query_insert = "insert into [tblRecipeMain]([name],[text],[ingredID], [notation],[image]) values(@name, @text, @ingredID, @notation, @image)"; 

        #region konstr
        public Recipe(string n, string t, string nn, Byte[] i, List<Ingredient> l)
        { 
            name = n;
            text = t;
            notation = nn;
            ico = i;
            list_ingredients = l;
        }
        public Recipe(string n, string t, string nn, BitmapImage i, List<Ingredient> l)
        {
            name = n;
            text = t;
            notation = nn;
            icon = i;
            list_ingredients = l;
        }
        public Recipe(string n, string t, string nn, List<Ingredient> l)
        {
            name = n;
            text = t;
            notation = nn;
            icon = null;
            list_ingredients = l;
        }
        #endregion

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
        static public int[] Get_id_by_name_ing(string name) //готово
        {
            int[] massiv_in = new int[99]; ; int mi=0;
            int[] massiv_rec = new int[99]; ; int mr=0;
            int[] massiv_er = new int[] { 0 };
            int i, prodID, q1,q2, z, count1 = Ingredient.Get_count(); 
            try
            { 
                prodID = Product.Get_id_by_name(name);

                bool f2 = false; 
                for (i = 0; i < count1; i++) 
                    if (((int)Ingredient.tbl.Rows[i]["productID"]) == prodID) { f2 = true; massiv_in[mi]= (int)Ingredient.tbl.Rows[i]["Id"]; mi++; }
                if (f2 == false) return massiv_er;

                bool f3 = false; 
                int index = 0, ingID; 
                    do
                    {
                    int count2 = Get_count();
                        ingID = massiv_in[index];
                        for (i = 1; i <= count2; i++)
                        {
                            q1 = (int)tbl.Rows[i - 1]["ingredID"];
                            if (i == count2) { q2 = 99; }
                            else { q2 = (int)tbl.Rows[i]["ingredID"]; }
                            for (z = q1; z < q2; z++) if (z == ingID)
                                {
                                    f3 = true; massiv_rec[mr] = (int)tbl.Rows[i - 1]["ID"];
                                    mr++;
                                }
                        }
                        index++;
                    } while (massiv_in[index] != 0);
                if (f3 == false) return massiv_er;
                else return massiv_rec;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return massiv_er;
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        }
        static public Recipe Get_recipe_by_id(int i) //готово
        {  
            try
            {   
                var im = tbl.Rows[i-1]["image"];
                if (im.GetType() != typeof(DBNull))
                {
                    MemoryStream mStream = new MemoryStream((Byte[])im);
                    var geti = new BitmapImage();
                    geti.BeginInit();
                    geti.StreamSource = mStream;
                    geti.EndInit();
                       
                    Recipe rec = new Recipe(((string)tbl.Rows[i-1]["name"]), ((string)tbl.Rows[i-1]["text"]), ((string)tbl.Rows[i-1]["notation"]), geti, (Ingredient.Get_list_ingredients((int)tbl.Rows[i - 1]["ingredID"])));
                    return rec;
                }
                else
                {
                    Recipe rec = new Recipe(((string)tbl.Rows[i-1]["name"]), ((string)tbl.Rows[i-1]["text"]), ((string)tbl.Rows[i-1]["notation"]), (Ingredient.Get_list_ingredients((int)tbl.Rows[i-1]["ingredID"])));
                    return rec;
                }
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
        static public int Get_id_by_name(string name) //готово
        { 
            try
            {
                int i;
                for (i = 0; i < count; i++)
                {
                    if (((string)tbl.Rows[i]["name"]) == name) break;
                }
                return (int)tbl.Rows[i]["id"];
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        }
        static public double Get_massa_by_id(int i)
        { 
            try
            {
                int bad_count=0;
                int good_count = 0;
                int count1 = FridgeProduct.Get_count();
                Recipe r = Get_recipe_by_id(i);
                List <FridgeProduct> list_fridge = FridgeProduct.Get_product_by_frost_id();
                for (int j = 0; j < r.list_ingredients.Count; j++)
                {
                    bool f = false;
                    foreach(FridgeProduct fpr in list_fridge)
                    {
                        if ((r.list_ingredients[j].product)==(fpr.product))
                        {
                            f = true; 
                        }
                        else
                        {
                            f = false; 
                        }
                        if (f == true) break;
                    }
                    if (f==true)
                    {
                        good_count++;
                    }
                    else
                    {
                        bad_count++;
                    }
                }
                return (double)((double)bad_count/ (double)(r.list_ingredients.Count));
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
        static public List<Good> Get_good_recipe()
        {
            List<Good> g = new List<Good>();
            try
            {
                for (int i=1; i<=Get_count();i++)
                {
                    var gg = new Good();
                    gg.recipe = Get_recipe_by_id(i);
                    gg.massa = Get_massa_by_id(i);
                    g.Add(gg);
                }
                return g;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return g;
            }
            finally
            {
                //clsDB.Close_DB_Connection();
            }
        }
        static public void Set_Recipe(string name, string text, int ingredID, string notation, Byte[] image)
        {
            try
            {
                var sql_con = clsDB.sqlCon;
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@name", name); 
                cmd2.Parameters.AddWithValue("@text", text);
                cmd2.Parameters.AddWithValue("@ingredID", ingredID);
                cmd2.Parameters.AddWithValue("@notation", notation);
                cmd2.Parameters.AddWithValue("@image", image);
                cmd2.ExecuteNonQuery();
                tbl = clsDB.Get_DataTable("select * from [tblRecipeMain];");
                count++;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
               // clsDB.Close_DB_Connection();
            }
        }

    }
}
