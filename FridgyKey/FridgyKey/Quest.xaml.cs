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
    /// Логика взаимодействия для Quest.xaml
    /// </summary>
    public partial class Quest : Window
    {
        public Quest()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void q11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q1.Visibility == Visibility.Collapsed)
            {
                grid.Height += q1.Height;
                Point point = new Point(0.5, 0.5);
                q11.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q11.RenderTransform = rot;
                q1.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q1.Height;
                Point point = new Point(0.5, 0.5);
            q11.RenderTransformOrigin = point;
            RotateTransform rot = new RotateTransform(0);
            q11.RenderTransform = rot;
            q1.Visibility = Visibility.Collapsed;
            }
            
        }

        private void q22_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q2.Visibility == Visibility.Collapsed)
            {
                grid.Height += q2.Height;
                Point point = new Point(0.5, 0.5);
                q22.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q22.RenderTransform = rot;
                q2.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q2.Height;
                Point point = new Point(0.5, 0.5);
                q22.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(0);
                q22.RenderTransform = rot;
                q2.Visibility = Visibility.Collapsed;
            } 
        }

        private void q33_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q3.Visibility == Visibility.Collapsed)
            {
                grid.Height += q3.Height;
                Point point = new Point(0.5, 0.5);
                q33.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q33.RenderTransform = rot;
                q3.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q3.Height;
                Point point = new Point(0.5, 0.5);
                q33.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(0);
                q33.RenderTransform = rot;
                q3.Visibility = Visibility.Collapsed;
            } 
        }

        private void q44_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q4.Visibility == Visibility.Collapsed)
            {
                grid.Height += q4.Height;
                Point point = new Point(0.5, 0.5);
                q44.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q44.RenderTransform = rot;
                q4.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q4.Height;
                Point point = new Point(0.5, 0.5);
                q44.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(0);
                q44.RenderTransform = rot;
                q4.Visibility = Visibility.Collapsed;
            }
        }

        private void q55_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q5.Visibility == Visibility.Collapsed)
            {
                grid.Height += q5.Height;
                Point point = new Point(0.5, 0.5);
                q55.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q55.RenderTransform = rot;
                q5.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q5.Height;
                Point point = new Point(0.5, 0.5);
                q55.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(0);
                q55.RenderTransform = rot;
                q5.Visibility = Visibility.Collapsed;
            } 
        }

        private void q66_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q6.Visibility == Visibility.Collapsed)
            {
                grid.Height += q6.Height;
                Point point = new Point(0.5, 0.5);
                q66.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q66.RenderTransform = rot;
                q6.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q6.Height;
                Point point = new Point(0.5, 0.5);
                q66.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(0);
                q66.RenderTransform = rot;
                q6.Visibility = Visibility.Collapsed;
            } 
        }

        private void q77_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q7.Visibility == Visibility.Collapsed)
            {
                grid.Height += q7.Height;
                Point point = new Point(0.5, 0.5);
                q77.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q77.RenderTransform = rot;
                q7.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q7.Height;
                Point point = new Point(0.5, 0.5);
                q77.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(0);
                q77.RenderTransform = rot;
                q7.Visibility = Visibility.Collapsed;
            } 
        }

        private void q88_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q8.Visibility == Visibility.Collapsed)
            {
                grid.Height += q8.Height;
                Point point = new Point(0.5, 0.5);
                q88.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q88.RenderTransform = rot;
                q8.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q8.Height;
                Point point = new Point(0.5, 0.5);
                q88.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(0);
                q88.RenderTransform = rot;
                q8.Visibility = Visibility.Collapsed;
            } 
        }

        private void q99_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (q9.Visibility == Visibility.Collapsed)
            {
                grid.Height += q9.Height;
                Point point = new Point(0.5, 0.5);
                q99.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(180);
                q99.RenderTransform = rot;
                q9.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Height -= q9.Height;
                Point point = new Point(0.5, 0.5);
                q99.RenderTransformOrigin = point;
                RotateTransform rot = new RotateTransform(0);
                q99.RenderTransform = rot;
                q9.Visibility = Visibility.Collapsed;
            } 
        }
    }
}
