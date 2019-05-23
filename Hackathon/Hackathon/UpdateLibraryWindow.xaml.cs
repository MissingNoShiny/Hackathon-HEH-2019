﻿using System;
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
using System.Data;

namespace Hackathon
{
    /// <summary>
    /// Logique d'interaction pour UpdateLibraryWindow.xaml
    /// </summary>
    public partial class UpdateLibraryWindow : Window
    {
        public UpdateLibraryWindow()
        {
            InitializeComponent();
            this.Show();
            
        }
        int a = 0;
        int b = 0;
        int c = 0;

        private void add_object_click(object sender, RoutedEventArgs e)
        {
            TextBox tb = new TextBox();
            tb.Name = ("name" + a++);
            tb.Width = 120;
            tb.Height = 23;
           
            
            this.stackpanel.Children.Add(tb);

            ComboBox cb = new ComboBox();
            cb.Name = ("choicedata" + b++);
            cb.SelectedIndex = 0;
            cb.Items.Add("String");
            cb.Items.Add("Int");
            cb.Items.Add("Bool");
            cb.Items.Add("Date");
            this.stackpanel1.Children.Add(cb);

            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/delete_button.png"));
            myBrush.ImageSource = image.Source;

            Button btn = new Button();
            btn.Name = ("del_object" + c++);
            btn.Width = 20;
            btn.Height = 23;
            btn.Background = myBrush;
            
            
            
            this.stackpanel2.Children.Add(btn);
        }

        private void del_object_click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Save_library_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_library_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
