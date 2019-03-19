using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace FridgyKey
{ 
    public partial class AddRecipe : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        Label l1, l2, l3, l4, l5, l6, l7, l8;
        TextBox txtamount, txtei;
        SolidColorBrush Mcolor = new SolidColorBrush();
        SolidColorBrush Mcolor2 = new SolidColorBrush();
        ComboBox combo;
        List<Ingredient> l = new List<Ingredient>();
        Byte[] Data = null;
        #endregion
        public AddRecipe()
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
            txtamount = (TextBox)FindName("amount");
            txtei = (TextBox)FindName("ei");
            combo = (ComboBox)FindName("comboBox");
            #endregion

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
        public void FillIngred()
        {
            list_in.Items.Clear();
            bool fl = false;  
            foreach (var f in l)
            {
                list_in.Items.Add(f.ToString());
                fl = true;
            }
            if (!fl)
            {
                list_in.Items.Add("No products.");
            }

        }
        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog img = new OpenFileDialog();

                BitmapImage bm1 = new BitmapImage();
                img.ShowDialog();
                var s = img.SafeFileName;
                FileStream fs = new FileStream(img.FileName, FileMode.Open);
                Data = new Byte[fs.Length];
                int read = (int)fs.Length;
                fs.Read(Data, 0, read);
                fs.Close();

                bm1.BeginInit();
                bm1.UriSource = new Uri(img.FileName, UriKind.Relative);
                bm1.CacheOption = BitmapCacheOption.OnLoad;
                bm1.EndInit();

                image.Source = bm1;
                //lpath.Content = System.IO.Path.GetFileNameWithoutExtension(img.FileName);
                lpath.Content = s;

            }
            catch
            {
                MessageBox.Show("open error");
            }
        }
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (combo.SelectedItem == null || txtamount.Text == "" || txtei.Text == "")
            {
                MessageBox.Show("Заполните все поля.");
            }
            else
            {
                l.Add(new Ingredient(Convert.ToInt32(txtamount.Text), txtei.Text, combo.SelectedItem.ToString()));
                FillIngred();
                txtamount.Text = "";
                txtei.Text = "";
                combo.Text = "";
            }
        }
        private void btnCleanProduct_Click(object sender, RoutedEventArgs e)
        {
            txtamount.Text = "";
            txtei.Text = "";
            combo.Text = "";
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
            {
                About Menu = new About();
                Menu.Show();
                this.Close();
            }
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
         

        #region methods_mouse_leave
        
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

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor;
        }
        private void label1_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor2;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtname.Text = "";
            txttext.Text = "";
            txtnot.Text = "";
            lpath.Content = "";
            image.Source = null;
            txtamount.Text = "";
            txtei.Text = "";
            combo.Text = "";
            l.Clear();
            btnrec.Content = "Add recipe!";
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
         
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtname.Text == "" || txttext.Text == "" || l == null/* || image == null*/)
            {
                MessageBox.Show("Заполните все поля.");
            }
            else
            {
                int ingID = Ingredient.Set_list_ingredients(l);
                Recipe.Set_Recipe(txtname.Text, txttext.Text, ingID, txtnot.Text, Data);
                list_in.Items.Clear();
                btnrec.Content = "Added!";
                l.Clear();
                NotificationWindow n = new NotificationWindow("Добавлен новый рецепт: " + txtname.Text);
                n.Show();
                lpath.Content = "";
                image.Source = null;
                txtname.Text = "";
                txtnot.Text = "";
                txttext.Text = "";
                txtamount.Text = "";
                txtei.Text = "";
            }
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
