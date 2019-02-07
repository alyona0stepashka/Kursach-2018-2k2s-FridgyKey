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
using FridgyKey._classes;

namespace FridgyKey.Windows
{
    public partial class Login : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;

        #endregion
        public Login()
        {
            InitializeComponent();
            #region service
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion

        }

        #region DB
        private void Get_id()
        {
            DataTable dt = clsDB.Get_DataTable("select top(1) frostID from tblUser where username='" + txtusername.Text + "';");
            User.Username = txtusername.Text;
            User.FrostID = (int)dt.Rows[0]["frostID"];
        }



        #endregion


        #region methods_control_window
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
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
            SqlConnection sqlCon = clsDB.Get_DB_Connection();
            //@"Data Source=DESKTOP-B9P5U74\SQLEXPRESS; Initial Catalog=FridgyKeyDB; Integrated Security=true;");
            try
            {
                String query = "SELECT COUNT(1) FROM tblUser WHERE username=@Username AND password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
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
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                clsDB.Close_DB_Connection();
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
