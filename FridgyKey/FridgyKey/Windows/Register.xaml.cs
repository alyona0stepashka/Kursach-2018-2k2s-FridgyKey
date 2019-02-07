using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using FridgyKey._classes;

namespace FridgyKey.Windows
{
    public partial class Register : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        #endregion
        public Register()
        {
            InitializeComponent();
            #region service
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion
        }

        private void btnGoRegister_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.connection_str);
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed) sqlCon.Open();
                if (txtfrostid.Text != null && txtusername.Text != null && txtpassword.Password != null)
                {
                    String query = "insert into tblUser (Username, Password, FrostID) values (@Username, @Password, @FrostID)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Username", txtusername.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", Convert.ToString(clsDB.Hash(txtpassword.Password)));
                    sqlCmd.Parameters.AddWithValue("@FrostID", txtfrostid.Text);
                    sqlCmd.ExecuteNonQuery();

                }
                else
                {

                    //как сделать заполнение FrostID как max(FrostID)+1 ??????????
                    MessageBox.Show("Fill all field.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            MainWindow Menu = new MainWindow();
            Menu.Show();
            this.Close();
            User.Username = txtusername.Text;
            User.FrostID = Convert.ToInt32(txtfrostid.Text);
        }


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
    }
}

