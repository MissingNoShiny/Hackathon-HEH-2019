﻿using System;
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

        private static Dictionary<String, DataType> dataTypesNames = new Dictionary<String, DataType> {
            ["String"] = DataType.STRING,
            ["Int"] = DataType.INTEGER,
            ["Bool"] = DataType.BOOLEAN,
            ["Date"] = DataType.DATE
        };
        private static Dictionary<DataType, String> namesDataType = dataTypesNames.ToDictionary((x) => x.Value, (x) => x.Key);
        public UpdateItemWindow(Library library)
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Window_Position;
            timer.Interval = TimeSpan.FromSeconds(0.0001);
            img_object.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/defaultimage.png"));
            timer.Start();
            foreach (String attributeName in library.AttributeNames) {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Margin = new Thickness(0, 5, 0, 0);

                TextBlock attNameTB = new TextBlock();
                attNameTB.FontFamily = new FontFamily("Segoe UI");
                attNameTB.FontSize = 13;
                attNameTB.FontWeight = FontWeights.Bold;
                attNameTB.Foreground = new SolidColorBrush(Colors.DimGray);
                attNameTB.Text = attributeName;
                attNameTB.Width = 50;
                attNameTB.Height = 23;

                TextBox tb = new TextBox();
                tb.Width = 60;
                tb.Height = 23;
                tb.HorizontalAlignment = HorizontalAlignment.Left;

                TextBlock dataTypeTB = new TextBlock();
                dataTypeTB.FontFamily = new FontFamily("Segoe UI");
                dataTypeTB.FontSize = 13;
                dataTypeTB.FontWeight = FontWeights.Bold;
                dataTypeTB.Foreground = new SolidColorBrush(Colors.DimGray);
                dataTypeTB.Text = namesDataType[library.AttributeTypes[attributeName]];
                dataTypeTB.Width = 50;
                dataTypeTB.Height = 23;
                dataTypeTB.TextAlignment = TextAlignment.Right;

                sp.Children.Add(attNameTB);
                sp.Children.Add(tb);
                sp.Children.Add(dataTypeTB);
                stackPanel.Children.Add(sp);
            }
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
