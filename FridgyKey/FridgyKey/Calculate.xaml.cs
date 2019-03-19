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
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;
using System.Data.Linq;
using System.ComponentModel;

namespace FridgyKey
{ 
    public partial class Calculate : Window
    {
         
        #region var declaration
        int koef = 48;
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        int count_hack;
        Label l1, l2, l3, l4, l5, l6, l7, l8, txtcalc;
        TextBox txtamount;
        SolidColorBrush Mcolor, Mcolor2;
        TextBox hack_txt;
        ComboBox combo;
        ListBox recipe;
        List<string> lr = new List<string>();
        #endregion
        public Calculate()
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
            txtcalc = (Label)FindName("calc");

            combo = (ComboBox)FindName("comboBox");
            hack_txt = (TextBox)FindName("hack_text");
            txtamount = (TextBox)FindName("amount");

            recipe = (ListBox)FindName("list_recipe");
            #endregion

            count_hack = 0;
            Fill();

            #region service
            label1.ToolTip = "Главная страница";
            label2.ToolTip = "Подобрать рецепт";
            label3.ToolTip = "Добавить продукт";
            label4.ToolTip = "Добавить рецепт";
            label17.ToolTip = "Корзина покупок";
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
        public void Fill()
        {
            int product_count = Product.Get_count();
            for (int i = 1; i <= product_count; i++)
                combo.Items.Add(Product.Get_product_by_id(i));
        }
        private void SetProd()
        {
            int z = 0;
            lr.Clear();
            if (combo.SelectedItem == null)
            {
                MessageBox.Show("Выберите продукт.");
            }
            else
            {
                koef = Product.Get_koef(combo.SelectedItem.ToString());

                int[] mas = Recipe.Get_id_by_name_ing(combo.SelectedItem.ToString());
                if (mas[0]!=0)
                {
                    do
                    {
                        z++;
                    } while (mas[z] != 0);

                    int count = z;
                
                    for (int i = 0; i < count; i++)
                            lr.Add((Recipe.Get_recipe_by_id(mas[i])).name);
                }
                else
                {
                    lr.Add("Recipe not found :(");
                    list_recipe.ItemsSource = lr;
                }
                recipe.ItemsSource = null;
                recipe.ItemsSource = lr;
            }
            

        }
        public float Calc_kkal()
        {
            try
            {
                float ii = 0;
                if (txtamount.Text=="")
                {
                    MessageBox.Show("Заполните поле количества в граммах.");
                }
                else
                {
                    txtcalc.Content = "...";
                    float p = (float)Convert.ToDouble(amount.Text);
                    ii = ((float)p * ((float)koef / (float)100));
                }                
                return ii;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
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
            {
                About Menu = new About();
                Menu.Show();
                this.Close();
            }
        }
        private void label17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShoppingBasket d = new ShoppingBasket();
            d.Show();
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

        #region methods_move_mouse
        
        private void list_recipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            if (list_recipe.Items.Contains("Recipe not found :(")) { }
            else
            {
                int id = Recipe.Get_id_by_name(lr[list_recipe.SelectedIndex]);

                RecipeView rv = new RecipeView(id);
                rv.Show();
            }
        }
 
        #endregion 

        #region methods_mouse_leave
         

        private void btncalc_Click(object sender, RoutedEventArgs e)
        {
            SetProd();
            string s = Convert.ToString(Calc_kkal());
            if (s == "")
            {
                MessageBox.Show("Введите количество в граммах.");
            }
            else
            {
                txtcalc.Content = s;
            }
        }

        private void Quest(object sender, MouseButtonEventArgs e)
        {
            Quest objModal = new Quest();
            objModal.Owner = this;
            ApplyEffect(this);

            objModal.ShowDialog();

            ClearEffect(this);
        }

        private void amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

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
        #endregion



    }
}
