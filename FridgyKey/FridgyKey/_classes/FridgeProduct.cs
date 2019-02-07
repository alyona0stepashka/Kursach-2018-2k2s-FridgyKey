using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgyKey 
{
    public static class FridgeProduct
    {
        public static string productID;
        public static int amount;
        public static string ei;

        static string query_insert = "insert into [tblFrost] ([frostID], [productID], [amount], [ei], [valid]) values (@frostid,@name,@amount,@ei,@valid);";
        static public int Get_count()
        {
            SqlConnection sqlCon = clsDB.Get_DB_Connection();
            try
            {
                DataTable dt2 = clsDB.Get_DataTable("select count(*) from [tblFrost] where [frostID]=" + User.FrostID + ";");
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

        static public string Get_product(int i)
        {
            SqlConnection sqlCon = clsDB.Get_DB_Connection();
            try
            {
                DataTable dt = clsDB.Get_DataTable("select * from [tblFrost] where [frostID]=" + User.FrostID + ";");
                DataTable dtf = clsDB.Get_DataTable("select * from [tblKkal] where [id]=" + (int)dt.Rows[i]["productID"] + ";");

                string add = "";
                if ((DateTime)dt.Rows[i]["valid"] <= DateTime.Now) { add = "!!! "; }
                string s = add + (string)dtf.Rows[0]["name"] + " (" + (int)dt.Rows[i]["amount"] + " " + (string)dt.Rows[i]["ei"] + " " + (String.Format("{0}.{1}.{2}", ((DateTime)dt.Rows[i]["valid"]).Day, ((DateTime)dt.Rows[i]["valid"]).Month, ((DateTime)dt.Rows[i]["valid"]).Year) + " )");
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
        static public void Set_product(int _amount, string _ei, DateTime _valid, string name)
        { 
            try
            {
                SqlConnection cn_connection = clsDB.Get_DB_Connection();
                SqlCommand cmd_Command = new SqlCommand(query_insert, cn_connection);
                cmd_Command.Parameters.AddWithValue("@name", Get_id(name));
                cmd_Command.Parameters.AddWithValue("@amount", amount);
                cmd_Command.Parameters.AddWithValue("@frostid", User.FrostID);
                cmd_Command.Parameters.AddWithValue("@ei", _ei);
                cmd_Command.Parameters.AddWithValue("@valid", _valid);
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
        static public int Get_id(string name)
        {
            SqlConnection sqlCon = clsDB.Get_DB_Connection();
            try
            {
                //не работает с русскими символами ????
                DataTable dt = clsDB.Get_DataTable("select * from [tblKkal] where [name]='" + name + "';");
                int id = (int)dt.Rows[0][0];
                return id;
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
    }
}
