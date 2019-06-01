using System;
using System.Windows.Threading;
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

namespace Gooboi
{
    /// <summary>
    /// Logique d'interaction pour ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        public ViewWindow()
        {
            InitializeComponent();
                DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Window_position;
            timer.Interval = TimeSpan.FromSeconds(0.0001);
            timer.Start();
        }
        private void Window_position(object sender, EventArgs e)
        {
            content.Width = Owner.Width / 1.25; ;
            if (Owner.WindowState != WindowState.Maximized)
            {
                this.Top = Owner.Top;
                this.Left = Owner.Left;
                this.Height = Owner.Height;
                this.Width = Owner.Width;
            }
            else
            {
                this.Top = 0;
                this.Left = 0;
                this.Height = Owner.Height;
                this.Width = Owner.Width;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var librarywindow = Application.Current.Windows.OfType<LibraryWindow>().FirstOrDefault();
            librarywindow.Opacity = 1;
            librarywindow.Effect = null;
            Close();
        }
    }
}
