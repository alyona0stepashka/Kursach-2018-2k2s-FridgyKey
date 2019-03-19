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

namespace FridgyKey
{ 
    public partial class Dialog : Window
    {
        string s;
        FridgeProduct r;
        public Dialog()
        {
            InitializeComponent();
        }
        public Dialog(FridgeProduct r)
        {
            InitializeComponent();
            this.r = r;
            s = (Product.Get_koef(r.product)).ToString() + " kkal/100g"; 
            btninfo.ToolTip = s;
            btnplus.ToolTip = "Добавить кол-во";
            btnminus.ToolTip = "Удалить кол-во";
            btndelete.ToolTip = "Удалить весь продукт";
            txtrezult.ToolTip = r.ei;
        }
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            MoveBottomRightEdgeOfWindowToMousePosition();
        }
        private void MoveBottomRightEdgeOfWindowToMousePosition()
        {
            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var mouse = transform.Transform(GetMousePosition());
            Left = mouse.X/* + ActualWidth*/;
            Top = mouse.Y/* + ActualHeight*/;
        }
        public System.Windows.Point GetMousePosition()
        {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new System.Windows.Point(point.X, point.Y);
        }
        private void Plus(object sender, RoutedEventArgs e)
        {
            txtrezult.Visibility = Visibility.Visible;
            btnrezult.Visibility = Visibility.Visible; 
            btnrezult.Content = btnplus.Content;
        }

        private void Minus(object sender, RoutedEventArgs e)
        {
            txtrezult.Visibility = Visibility.Visible;
            btnrezult.Visibility = Visibility.Visible; 
            btnrezult.Content = btnminus.Content;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            FridgeProduct.Delete_product(r);
            NotificationWindow n = new NotificationWindow("Удалено полностью: " + r.product);
            n.Show();
            Close();
        }
 
        private void Action(object sender, RoutedEventArgs e)
        {
            if (txtrezult.Text=="")
            {
                MessageBox.Show("Заполните поле.");
            }
            else
            {
                if ((string)btnrezult.Content == "+")
                {
                    FridgeProduct.Update_product(r, Convert.ToInt32(txtrezult.Text));
                    NotificationWindow n = new NotificationWindow("Добавлено: +" + txtrezult.Text + r.ei + " " + r.product);
                    n.Show();
                }
                else if ((string)btnrezult.Content == "-")
                {
                    if (r.amount < Convert.ToInt32(txtrezult.Text)) MessageBox.Show("Удаляется больше, чем имеется.");
                    else if (r.amount == Convert.ToInt32(txtrezult.Text))
                    {
                        FridgeProduct.Delete_product(r);
                        NotificationWindow n = new NotificationWindow("Удалено полностью: " + r.product);
                        n.Show();
                    }
                    else
                    {
                        FridgeProduct.Update_product(r, (-1) * Convert.ToInt32(txtrezult.Text));
                        NotificationWindow n = new NotificationWindow("Удалено: -" + txtrezult.Text + r.ei + " " + r.product);
                        n.Show();
                    }
                }
                else MessageBox.Show("Выберите операцию.");
                Close();
            }
            
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void txtrezult_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
    }
}
