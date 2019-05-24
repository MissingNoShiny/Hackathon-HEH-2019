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
        }

        

        private void Add_picture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Selectionne une image";
            OFD.Filter = "jpeg |*.jpeg | jpg |*.jpg |png |*.png";
            
            if (OFD.ShowDialog() == true)
            {
                img_object.Source = new BitmapImage(new Uri(OFD.FileName));
            }
            img_object.Width = 220;
            img_object.Height = 170;
            
        }
        
}
}
