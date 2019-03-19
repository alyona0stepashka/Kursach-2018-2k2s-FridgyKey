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
    /// <summary>
    /// Логика взаимодействия для AboutBtn.xaml
    /// </summary>
    public partial class AboutBtn : Window
    {
        public AboutBtn()
        {
            InitializeComponent();
            txtmain.Text = "  Привет, "+ User.Username + ". Здесь должно быть написано что-то нудное и скучное о структуре моего творения, но вместо этого я просто пожелаю тебе xорошего дня :)";
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
