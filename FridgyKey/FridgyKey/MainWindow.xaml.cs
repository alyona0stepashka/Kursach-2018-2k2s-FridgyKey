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
    public partial class MainWindow : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        Label l1,l2,l3,l4,l5,l6,l7,l8, id;
        SolidColorBrush Mcolor = new SolidColorBrush();
        SolidColorBrush Mcolor2 = new SolidColorBrush();
        ListBox chat, fridge;
        TextBox mes;
        TextBlock tb = new TextBlock();
        List<FridgeProduct> lf = new List<FridgeProduct>();
        #endregion
        public MainWindow()
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
            id = (Label)FindName("idtxt");
            chat = (ListBox)FindName("list_chat");
            fridge = (ListBox)FindName("list_products");
            mes = (TextBox)FindName("message");
            #endregion
 

            id.Content = User.FrostID;

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
        public void Fill()
        {
            Clear_list();

            List<string> list_st = Sticker.Get_message();
            foreach(string ss in list_st)
            {
                chat.Items.Add(ss);
            }

            bool fl = false;
            lf = FridgeProduct.Get_product_by_frost_id(); 
            foreach (FridgeProduct f in lf)
            {
                fridge.Items.Add(f.ToString());
                fl = true;
            }
            if (!fl)
            {
                fridge.Items.Add("No products in your fridge.");
            }

            //list_products.SelectedItem.Attributes["style"] = "color:red";

            //Control chk = ((Control)sender).FindControl("list_products");
            //ListBox ch = (ListBox)chk;
            //for (int i = 0; i < fridge.Items.Count; i++)
            //{
            //    if (((string)(fridge.Items[i])).Contains("!"))
            //    {
            //        ListBoxItem it = (ListBoxItem)fridge.Items[i];
            //        it.Attributes.Add("style", "background-color: red;");
            //    }
            //}
        }
        private void Clear_list()
        {
            chat.Items.Clear();
            fridge.Items.Clear();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tb.TextWrapping = TextWrapping.Wrap;
            Sticker.Set_message(mes.Text);
            string s = User.Username + ": " + mes.Text;
            chat.Items.Add(s);
            message.Text = ""; 
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
        private void Method_f(int q)
        {
            Dialog objModal = new Dialog(lf[q]);
            objModal.Owner = this;
            ApplyEffect(this);

            objModal.ShowDialog();

            ClearEffect(this);
            MainWindow w = new MainWindow();
            w.Show(); 
            this.Close();
        }
        private void list_products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fridge.Items.Contains("No products in your fridge."))
            {
                MessageBox.Show("Добавьте продукты в ваш холодильник.");
            }
            else
            {
                int q = list_products.SelectedIndex;
                if (q < 0) MessageBox.Show("Выберите продукт");
                else Method_f(q);
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
        #endregion

        #region methods_menu
        #region methods_click_mouse
        private void Label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow Menu = new MainWindow();
            Menu.Show();
            this.Close();
        }
        private void label17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShoppingBasket d = new ShoppingBasket();
            d.Show();
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

        private void Button_Click22(object sender, RoutedEventArgs e)
        {
            Sticker.Delete_Story();
            Fill();
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
