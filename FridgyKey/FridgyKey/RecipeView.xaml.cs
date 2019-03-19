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
using WpfAnimatedGif;

namespace FridgyKey
{ 
    public partial class RecipeView : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        ListBox ingred;
        List<Ingredient> list_ing = new List<Ingredient>();
        #endregion
        public RecipeView()
        {
            InitializeComponent();
            Resource.Get_BG(canv);
            #region FindName
            ingred = (ListBox)FindName("list_ingr");
            #endregion
            #region вывод ингериентов
            string[] s = new string[]{ "Молоко 200 мл", "Банан 2 шт" };
            ingred.ItemsSource = s;
            #endregion
            #region вывод картинки
             
            #endregion
            #region вывод текста

            #endregion
            #region service
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion
        }
        public RecipeView(int id)
        {
            InitializeComponent();
            Resource.Get_BG(canv);
            #region FindName
            ingred = (ListBox)FindName("list_ingr");
            #endregion
            
            Fill(id);

            #region service
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion
        }
        public void Fill(int id)
        {
            Recipe r = Recipe.Get_recipe_by_id(id);
            int i = Ingredient.Get_id_ing_by_id_recipe(id);
            list_ing = Ingredient.Get_list_ingredients(i);
            ingred.ItemsSource = Ingredient.Get_ing_info(i);  

            var bm = r.icon;
            if (bm == null) { }
            else
            {
                ImageBehavior.SetAnimatedSource(image, r.icon); 
            }
            text.Text = r.text;
            not.ToolTip = r.notation;
        }

        #region methods_control_window
        private void Button_Click(object sender, RoutedEventArgs e)
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
        private void Quest(object sender, MouseButtonEventArgs e)
        {
            Quest objModal = new Quest();
            objModal.Owner = this;
            ApplyEffect(this);

            objModal.ShowDialog();

            ClearEffect(this);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var ingr in list_ing)
            {
                if(!FridgeProduct.Is_have(Product.Get_id_by_name(ingr.product)))
                {
                    Basket.Set_product(ingr.amount, ingr.ei, ingr.product);
                }
            }
        }
    }
}
