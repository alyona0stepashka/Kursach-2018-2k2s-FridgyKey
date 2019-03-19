using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FridgyKey
{ 
    public partial class ShoppingBasket : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        Label l1,l2,l3,l4,l5,l6,l7,l8;
        SolidColorBrush Mcolor = new SolidColorBrush();
        SolidColorBrush Mcolor2 = new SolidColorBrush();
        ListBox fridge;  
        List<Basket> lf = new List<Basket>();
        #endregion
        public ShoppingBasket()
        {
            InitializeComponent();
            Resource.Get_BG(canv);
            #region FindNme
            l1 = (Label)FindName("label1");
            l2 = (Label)FindName("label2");
            l3 = (Label)FindName("label3");
            l4 = (Label)FindName("label4");
            l5 = (Label)FindName("label5");
            l6 = (Label)FindName("label6");
            l7 = (Label)FindName("label7");
            l8 = (Label)FindName("label8"); 
            fridge = (ListBox)FindName("list_products"); 
            #endregion
             
             
            Fill();

            #region service
            label1.ToolTip = "Главная страница";
            label2.ToolTip = "Подобрать рецепт";
            label3.ToolTip = "Добавить продукт";
            label4.ToolTip = "Добавить рецепт";
            label5.ToolTip = "Калькулятор калорийности";
            label6.ToolTip = "Настройки";
            label17.ToolTip = "Корзина покупок";
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
        public void FillBasket()
        {
            bool fl = false;
            fridge.Items.Clear();
            lf.Clear();
            lf = Basket.Get_product_by_frost_id(); 
            foreach (var f in lf)
            {
                fridge.Items.Add(f.ToString());
                fl = true;
            }
            if (!fl)
            {
                fridge.Items.Add("No products in your shopping basket.");
            }
        }
        public void Fill()
        {
            FillBasket();

                int product_count = Product.Get_count();
                for (int i = 1; i <= product_count; i++)
                    combo_product.Items.Add(Product.Get_product_by_id(i)); 
        }
        private void Clear_list()
        { 
            fridge.Items.Clear();
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
        private void list_products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { } 
        private void Quest(object sender, MouseButtonEventArgs e)
        {

            Quest objModal = new Quest();
            objModal.Owner = this;
            ApplyEffect(this);

            objModal.ShowDialog();

            ClearEffect(this);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (combo_product.SelectedItem==null || txt_amount.Text=="" || txt_ei.Text=="")
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                Basket.Set_product(Convert.ToInt32(txt_amount.Text), txt_ei.Text, combo_product.SelectedItem.ToString());
                FillBasket();
                NotificationWindow n = new NotificationWindow("В корзину добавлено: " + combo_product.SelectedItem.ToString() + " " + Convert.ToInt32(txt_amount.Text) + " " + txt_ei.Text);
                n.Show();
                txt_amount.Text = "";
                txt_ei.Text = "";
                combo_product.SelectedItem = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((string)fridge.SelectedItem=="No products in your shopping basket.")
            {
                MessageBox.Show("Добавьте продукты в вашу корзину покупок.");
            }
            else
            {
                Basket b = lf[list_products.SelectedIndex];
                Basket.Delete_product(b);
                FillBasket();
                NotificationWindow n = new NotificationWindow("Из корзины удалено: " + b.product + " " + b.amount + " " + b.ei);
                n.Show();
            }
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

        #region methods_mouse_menu
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor; 
        }
        private void label1_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor2;
        } 
        #endregion   
                
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
