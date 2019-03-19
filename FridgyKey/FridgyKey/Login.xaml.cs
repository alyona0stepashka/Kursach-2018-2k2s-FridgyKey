using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;

namespace FridgyKey
{  
    public partial class Login : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;

        #endregion
        public Login()
        {
            InitializeComponent();

            FillDB();
            


            StandartBuilder builder = new StandartBuilder();
            builder.BuildStandarTheme(); 
            Resource.bg = builder.GetResult();

            #region service
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion 
        }

        #region DB

        public static void FillDB()
        {
            clsDB.sqlCon = clsDB.Get_DB_Connection();

            string query = "select count(1) from ";

            FridgeProduct.tbl = clsDB.Get_DataTable("select * from [tblFrost];");
            SqlCommand sqlCmd1 = new SqlCommand(query + "[tblFrost]", clsDB.sqlCon);
            FridgeProduct.count = Convert.ToInt32(sqlCmd1.ExecuteScalar());

            Ingredient.tbl = clsDB.Get_DataTable("select * from [tblRecipeIn];");
            SqlCommand sqlCmd2 = new SqlCommand(query + "[tblRecipeIn]", clsDB.sqlCon);
            Ingredient.count = Convert.ToInt32(sqlCmd2.ExecuteScalar());

            Product.tbl = clsDB.Get_DataTable("select * from [tblKkal];");
            SqlCommand sqlCmd3 = new SqlCommand(query + "[tblKkal]", clsDB.sqlCon);
            Product.count = Convert.ToInt32(sqlCmd3.ExecuteScalar());

            Recipe.tbl = clsDB.Get_DataTable("select * from [tblRecipeMain];");
            SqlCommand sqlCmd4 = new SqlCommand(query + "[tblRecipeMain]", clsDB.sqlCon);
            Recipe.count = Convert.ToInt32(sqlCmd4.ExecuteScalar());

            Sticker.tbl = clsDB.Get_DataTable("select * from [tblSticker];");
            SqlCommand sqlCmd5 = new SqlCommand(query + "[tblSticker]", clsDB.sqlCon);
            Sticker.count = Convert.ToInt32(sqlCmd5.ExecuteScalar());

            User.tbl = clsDB.Get_DataTable("select * from [tblUser];");
            SqlCommand sqlCmd6 = new SqlCommand(query + "[tblUser]", clsDB.sqlCon);
            User.count = Convert.ToInt32(sqlCmd6.ExecuteScalar());

            Basket.tbl = clsDB.Get_DataTable("select * from [tblBasket];");
            SqlCommand sqlCmd7 = new SqlCommand(query + "[tblBasket]", clsDB.sqlCon);
            Basket.count = Convert.ToInt32(sqlCmd7.ExecuteScalar());
        }

        private void Get_id ()
        { 
            User.Username = txtusername.Text;
            User.FrostID = User.Get_frost_id_by_name(txtusername.Text);
        }
 


        #endregion


        #region methods_control_window
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clsDB.Close_DB_Connection();
            Close();
            //Environment.Exit(0);
        }
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        void MyNotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                MyNotifyIcon.BalloonTipTitle = "Minimize Sucessful";
                MyNotifyIcon.BalloonTipText = "Minimized the app ";
                MyNotifyIcon.ShowBalloonTip(400);
                MyNotifyIcon.Visible = true;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                MyNotifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }
        #endregion

        #region small logica
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {                 
                String query = "SELECT COUNT(1) FROM tblUser WHERE username=@Username AND password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, clsDB.sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtusername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", Convert.ToString(clsDB.Hash(txtpassword.Password)));
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    Get_id();
                    MainWindow Menu = new MainWindow();
                    Menu.Show(); 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Логин или пароль неверен."); 
                }
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register Menu = new Register();
            Menu.Show();
            this.Close();
        }
        #endregion 
    }
}
