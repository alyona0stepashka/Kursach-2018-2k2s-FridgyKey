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
using System.Data;
using System.Data.SqlClient;

namespace FridgyKey
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
            int f;
            try
            {
                if (txtpassword.Password.Length < 2)
                {
                    MessageBox.Show("Пароль должен быть не менее 2 символов.");
                }
                else
                {
                string query = "SELECT COUNT(1) FROM tblUser WHERE username=@Username AND password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, clsDB.sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtusername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", Convert.ToString(clsDB.Hash(txtpassword.Password)));
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MessageBox.Show("Такой пользователь уже существует.");
                }
                else
                {
                    if (txtfrostid.Text != "" && txtusername.Text != "" && txtpassword.Password != "")
                    {
                        f = 1;
                        User.Set_User(f, txtusername.Text, Convert.ToString(clsDB.Hash(txtpassword.Password)), Convert.ToInt32(txtfrostid.Text));
                        NotificationWindow n = new NotificationWindow(txtusername.Text + " успешно зарегистрирован!");
                        n.Show();
                    }
                    else if (txtfrostid.Text == "" && txtusername.Text != "" && txtpassword.Password != "")
                    {
                        f = -1;
                        User.Set_User(f, txtusername.Text, Convert.ToString(clsDB.Hash(txtpassword.Password)), User.Get_max_frostid() + 1);
                        NotificationWindow n = new NotificationWindow(txtusername.Text + " успешно зарегистрирован!");
                        n.Show();
                    }
                    else { MessageBox.Show("Заполните необходимые поля."); }
                     

                    MainWindow Menu = new MainWindow();
                    Menu.Show();
                    this.Close();
                }
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            } 
        } 

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

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Close();
        } 
        private void txtfrostid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
    }
}
