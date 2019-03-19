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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;
using System.Collections.ObjectModel;

namespace FridgyKey
{
    public partial class AddProduct : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        Label l1, l2, l3, l4, l5, l6, l7, l8, txtad, txtfridge;
        SolidColorBrush Mcolor = new SolidColorBrush();
        SolidColorBrush Mcolor2 = new SolidColorBrush();
        ComboBox combo;
        Image im;
        TextBox name, txtam, txtprice, txtkkal;
        DatePicker dat;
        #endregion
        public AddProduct()
        {
            InitializeComponent();

            Resource.Get_BG(canv);
            #region FindName
            l1 = (Label)FindName("label1");
            l2 = (Label)FindName("label2");
            l3 = (Label)FindName("label3");
            l4 = (Label)FindName("label4");
            l5 = (Label)FindName("label5");
            l6 = (Label)FindName("label6");
            l7 = (Label)FindName("label7");
            l8 = (Label)FindName("label8");
            combo = (ComboBox)FindName("comboBoxFridge");
            im = (Image)FindName("panda_im");
            txtfridge = (Label)FindName("txtaddFridge");
            txtad = (Label)FindName("txtadd");
            name = (TextBox)FindName("nam");
            txtprice = (TextBox)FindName("price");
            txtam = (TextBox)FindName("txtamount");
            dat = (DatePicker)FindName("dato"); 
            txtkkal = (TextBox)FindName("kkal");
            #endregion 

            Fill(); 
            #region service
            label1.ToolTip = "Главная страница";
            label2.ToolTip = "Подобрать рецепт";
            label3.ToolTip = "Добавить продукт";
            label17.ToolTip = "Корзина покупок";
            label4.ToolTip = "Добавить рецепт";
            label5.ToolTip = "Калькулятор калорийности";
            label6.ToolTip = "Настройки";
            label7.ToolTip = "Поиск рецептов";
            label8.ToolTip = "Выход";

            Mcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C2CAD1"));
            Mcolor2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#313937"));

            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion
        }
        #region small logica
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (combo.SelectedItem == null || txtam.Text == "" || dat.SelectedDate == null || txtprice.Text == null)
            {
               txtfridge.Content = "Заполните все поля.";
            }
            else
            {
                FridgeProduct.Set_product(Convert.ToInt32(txtam.Text), txtprice.Text, (DateTime)dat.SelectedDate, (string)(combo.SelectedItem));
                string s = "В холодильник добавлено: " + (string)combo.SelectedItem + " " + txtam.Text + txtprice.Text;
                combo.SelectedItem = null;
                txtam.Text = "";
                dat.Text = null;
                txtprice.Text = null;
                txtfridge.Content = "♥";

                NotificationWindow n = new NotificationWindow(s);
                n.Show();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || txtkkal.Text == "")
            {
                txtad.Content = "Заполните все поля.";
            }
            else
            {
                Product.Set_product(Convert.ToInt32(txtkkal.Text), name.Text);
                string s = "В базу данных добавлено: " + name.Text + " " + txtkkal.Text + " kkal/100g";
                name.Text = "";
                txtkkal.Text = "";
                txtad.Content = "♥";

                NotificationWindow n = new NotificationWindow(s);
                n.Show();

                Fill();
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            combo.SelectedItem = null;
            txtam.Text = "";
            dat.Text = null;
            txtprice.Text = null;
            txtfridge.Content = "";
        } 
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            txtkkal.Text = "";
            txtad.Content = "";
        } 
        private void comboBoxFridge_Selected(object sender, RoutedEventArgs e)
        { 
            txtfridge.Content = (string)(combo.SelectedItem);
        }
        public void Fill() 
        {
            int product_count = Product.Get_count();
            for (int i = 1; i <= product_count; i++)
                combo.Items.Add(Product.Get_product_by_id(i));
        }
        #endregion

        #region methods_menu
        #region methods_click_mouse
        private void Label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow Menu = new MainWindow();
            Menu.Show();
            this.Close();
        } 
        private void label7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
                About Menu = new About();
                Menu.Show();
                this.Close(); 
        }
        private void Label8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login Menu = new Login();
            Menu.Show();
            this.Close();
        }
        private void label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetRecipe Menu = new GetRecipe();
            Menu.Show();
            this.Close();
        } 
        private void label3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddProduct Menu = new AddProduct();
            Menu.Show();
            this.Close();
        } 
        private void label6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Settings Menu = new Settings();
            Menu.Show();
            this.Close();
        }
        private void label17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShoppingBasket d = new ShoppingBasket();
            d.Show();
            this.Close();
        }
        private void label5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Calculate Menu = new Calculate();
            Menu.Show();
            this.Close();
        } 
        private void label4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddRecipe Menu = new AddRecipe();
            Menu.Show();
            this.Close();
        }
        #endregion  

        private void Quest(object sender, MouseButtonEventArgs e)
        {
            Quest objModal = new Quest();
            objModal.Owner = this;
            ApplyEffect(this);

            objModal.ShowDialog();

            ClearEffect(this);
        }
        private void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 4;
            win.Effect = objBlur;
        }
        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }
        private void About_p(object sender, MouseButtonEventArgs e)
        {
            AboutBtn objModal = new AboutBtn();
            objModal.Owner = this;
            ApplyEffect(this);

            objModal.ShowDialog();

            ClearEffect(this);
        }

        private void txtamount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region methods_mouse_leave
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor;
        }
        private void label1_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor2;
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

    }
}
    