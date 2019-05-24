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
using Microsoft.Win32;

namespace Hackathon
{
    /// <summary>
    /// Logique d'interaction pour UpdateItemWindow.xaml
    /// </summary>
    public partial class UpdateItemWindow : Window
    {
        public UpdateItemWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Window_Position;
            timer.Interval = TimeSpan.FromSeconds(0.0001);
            img_object.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/defaultimage.png"));
            timer.Start();
        }
        private void Add_picture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Selectionne une image";
            OFD.Filter = "*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (OFD.ShowDialog() == true)
            {
                img_object.Source = new BitmapImage(new Uri(OFD.FileName));
            }            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Owner.Focus();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //SAVE changes
        }

        private void Cancel_library_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Owner.Focus();
        }
        private void Window_Position(object sender, EventArgs e)
        {
            this.Top = Owner.Top + 30;
            this.Left = Owner.Left + 8;
            this.Height = Owner.Height - 38;
            this.Width = Owner.Width - 16;
            this.WindowState = Owner.WindowState;
        }
    }
}
