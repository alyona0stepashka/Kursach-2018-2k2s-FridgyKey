using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgyKey
{
    public static class Hack
    { 
        public static int count;
        public static string Get_Hack(int i) //готово
        {
            SqlConnection sqlCon = clsDB.sqlCon; 
            try
            { 
                DataTable dt = clsDB.Get_DataTable("select * from tblHack;");
                DataTable dt2 = clsDB.Get_DataTable("select count(*) from tblHack;");
                count = (int)dt2.Rows[0][0]; 
                return (string)dt.Rows[i]["text"];
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
    }
}
