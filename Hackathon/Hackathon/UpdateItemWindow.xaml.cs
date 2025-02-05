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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Gooboi
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
        private List<TextBox> textBoxes;
        private Library library;
        private Item item;
        private bool edition;
        private Uri imagePath;
        public UpdateItemWindow(Library library)
        {
            this.library = library;
            edition = false;
            InitializeWindow();
        }

        public UpdateItemWindow(Library library, int index) {
            this.library = library;
            item = library.Items[index];
            edition = true;
            InitializeWindow();
            for (int i = 0; i < textBoxes.Count; i++) {
                textBoxes[i].Text = item.Values[i].Value.ToString();
            }
            if (item.ImagePath != null) {
                try {
                    img_object.Source = new BitmapImage(item.ImagePath);
                    img_object.Width = 150;
                    img_object.Height = 150;
                    imagePath = item.ImagePath;
                } catch(Exception) {
                    MessageBox.Show("Erreur lors du chargement de l'image", "Erreur", MessageBoxButton.OK);
                }
            }
        }

        private void InitializeWindow() {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Window_Position;
            timer.Interval = TimeSpan.FromSeconds(0.0001);
            img_object.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/defaultimage.png"));
            timer.Start();
            Theme();
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
                attNameTB.Width = 100;
                attNameTB.Height = 23;
                attNameTB.Margin = new Thickness(0, 0, 10, 0);
                attNameTB.TextAlignment = TextAlignment.Right;

                TextBox tb = new TextBox();
                tb.Width = 120;
                tb.Height = 25;
                tb.Opacity = 0.5;
                tb.FontSize = 16;
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.Name = attributeName;
                textBoxes.Add(tb);

                TextBlock dataTypeTB = new TextBlock();
                dataTypeTB.FontFamily = new FontFamily("Segoe UI");
                dataTypeTB.FontSize = 13;
                dataTypeTB.FontWeight = FontWeights.Bold;
                dataTypeTB.Foreground = new SolidColorBrush(Colors.DimGray);
                dataTypeTB.Text = namesDataType[library.AttributeTypes[attributeName]];
                dataTypeTB.Width = 50;
                dataTypeTB.Height = 23;
                dataTypeTB.Margin = new Thickness(10, 0, 0, 0);
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
            OFD.Title = "Importer une image";
            OFD.Filter = "*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (OFD.ShowDialog() == true)
            {
                imagePath = new Uri(OFD.FileName);
                img_object.Source = new BitmapImage(imagePath);
                img_object.Width = 150;
                img_object.Height = 150;
            }            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Owner.Focus();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<Attribute> attributes = new List<Attribute>();
            foreach (TextBox textBox in textBoxes) {
                String value = textBox.Text;
                if (String.IsNullOrEmpty(value)) {
                    WarningWindow warning = new WarningWindow();//start
                    warning.Owner = this;
                    warning.Message_show(10);
                    if (this.WindowState == WindowState.Normal)
                    {
                        warning.Width = Width / 1.5;
                        warning.Left = Left + warning.Width / 4;
                        warning.Height = Height / 2;
                        warning.Top = Top + warning.Height / 2;
                    }
                    this.Opacity = 0.5;
                    BlurEffect blur;
                    blur = new BlurEffect();
                    blur.Radius = 15;
                    this.Effect = blur;
                    warning.ShowDialog();
                    warning.object_content.Text = textBox.Name;
                    this.Opacity = 1;
                    this.Effect = null;//end 
                    return;
                }
                if (value.Length > 24) {
                    WarningWindow warning = new WarningWindow();//start
                    warning.Owner = this;
                    warning.Message_show(11);
                    if (this.WindowState == WindowState.Normal)
                    {
                        warning.Width = Width / 1.5;
                        warning.Left = Left + warning.Width / 4;
                        warning.Height = Height / 2;
                        warning.Top = Top + warning.Height / 2;
                    }
                    this.Opacity = 0.5;
                    BlurEffect blur;
                    blur = new BlurEffect();
                    blur.Radius = 15;
                    this.Effect = blur;
                    warning.ShowDialog();
                    warning.object_content.Text = textBox.Name;
                    this.Opacity = 1;
                    this.Effect = null;//end 
                    return;
                }
                switch (library.AttributeTypes[textBox.Name]) {
                    case DataType.STRING:
                        attributes.Add(new Attribute(value));
                        break;
                    case DataType.INTEGER:
                        try {
                            attributes.Add(new Attribute(Int32.Parse(value)));
                        } catch (Exception) {
                            attributes.Add(null);
                        }
                        break;
                    case DataType.BOOLEAN:
                        try {
                            attributes.Add(new Attribute(Boolean.Parse(value)));
                        } catch (Exception) {
                            attributes.Add(null);
                        }
                        break;
                    case DataType.DATE:
                        try {
                            attributes.Add(new Attribute(DateTime.Parse(value)));
                        } catch (Exception) {
                            attributes.Add(null);
                        }
                        break;
                }
            }
            List<String> badTextBoxes = new List<String>();
            for (int i = 0; i < attributes.Count; i++) {
                if (attributes[i] == null)
                    badTextBoxes.Add(textBoxes[i].Name);
            }
            if (badTextBoxes.Count > 0) {
                WarningWindow warning = new WarningWindow();//start
                warning.Owner = this;
                warning.Message_show(12);
                if (this.WindowState == WindowState.Normal)
                {
                    warning.Width = Width / 1.5;
                    warning.Left = Left + warning.Width / 4;
                    warning.Height = Height / 2;
                    warning.Top = Top + warning.Height / 2;
                }
                this.Opacity = 0.5;
                BlurEffect blur;
                blur = new BlurEffect();
                blur.Radius = 15;
                this.Effect = blur;
                warning.ShowDialog();
                warning.object_content.Text = String.Join(", ", badTextBoxes);
                this.Opacity = 1;
                this.Effect = null;//end 
                return;
            }
            try {
                if (edition) {
                    if (item.ImagePath != imagePath)
                        library.ModifyItem(item, new Item(attributes, imagePath));
                    else
                        library.ModifyItem(item, new Item(attributes, item.ImagePath));
                } else {
                    if (imagePath != null)
                        library.AddItem(new Item(attributes, imagePath));
                    else
                        library.AddItem(new Item(attributes));
                }       
            } catch (Exception) {
                String action = edition ? "édition" : "ajout";
                MessageBox.Show($"Une erreur s'est produite lors de l'{action} de l'objet.", "Erreur", MessageBoxButton.OK);
                return;
            }
            library.Save(LibraryManager.DefaultLibrariesPath);
            this.Close();
            Owner.Focus();
        }

        private void Cancel_library_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Owner.Focus();
        }
        private void Window_Position(object sender, EventArgs e)
        {           
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
                save_changes.Margin = new Thickness(0, 0, 25, 25);
                cancel_changes.Margin = new Thickness(0, 0, 80, 25);
                add_picture.Margin = new Thickness(0, 0, 135, 25);
            }
        }
        /*APPLYING THEME*/
        public void Theme()
        {
            if (Gooboi.Properties.Settings.Default.Theme == "Light")
            {
                windowitem_background.Background = new SolidColorBrush(Color.FromRgb(217, 217, 217));
                page_title.Foreground = new SolidColorBrush(Colors.Black);
                cancel_library.Foreground = new SolidColorBrush(Colors.Black);
                cancel_library.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/back_button_light.png")));
                save_changes.Foreground = new SolidColorBrush(Colors.Black);
                save_changes.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/validation_button_light.png")));
                add_picture.Foreground = new SolidColorBrush(Colors.Black);
                add_picture.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/uploadimage_button_light.png")));
                cancel_changes.Foreground = new SolidColorBrush(Colors.Black);
                cancel_changes.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/delete_button_light.png")));
            }
        }
    }
}
