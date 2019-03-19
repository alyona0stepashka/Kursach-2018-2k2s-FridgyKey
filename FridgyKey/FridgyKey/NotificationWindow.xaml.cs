using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FridgyKey
{ 
    public partial class NotificationWindow : Window
    {
        Timer timer1;
        public NotificationWindow()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth;
                this.Top = corner.Y - this.ActualHeight;
            }));
        }
        public NotificationWindow(string s)
        {
            InitializeComponent();
            var rand = new Random();
            int i = rand.Next(0, 5);

            string[] w = new string[] { "Тут кое-что произошло", "Опачки", "Ну ничего себе", "Ну, бывает", "Ку-ку", "Тук-тук" };
            block.Text = s;
            blckBig.Text = w[i];

            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth;
                this.Top = corner.Y - this.ActualHeight;
            }));

            //Timer timer = new Timer();
            //timer.Interval = 6000;
            //timer.tick += new EventHadler(TimerTick);
            //timer.Start();
            //this.Close();
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            var form = this;
             
                timer.Interval = 8000; //время ожидания
                timer.Tick += new EventHandler(delegate (object _s, EventArgs _e) {
                    timer.Stop(); 
                    form.Close();
                });
                timer.Start();
        }
    }
}
