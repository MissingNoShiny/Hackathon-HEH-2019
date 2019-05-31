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
                    MessageBox.Show("Tous les attributs doivent avoir une valeur ! La valeur suivante ne respecte pas cette condition :\n" + textBox.Name, "Erreur", MessageBoxButton.OK);
                    return;
                }
                if (value.Length > 24) {
                    MessageBox.Show("Les valeurs ne peuvent pas dépasser 24 caractères. La valeur suivante ne respecte pas cette condition :\n" + textBox.Name, "Erreur", MessageBoxButton.OK);
                    return;
                }
                switch (library.AttributeTypes[textBox.Name]) {
                    case DataType.STRING:
                        attributes.Add(new Attribute(value));
                        break;
                    case DataType.INTEGER:
                        try {
                            attributes.Add(new Attribute(Int32.Parse(value)));
                        } catch (Exception ex) {
                            attributes.Add(null);
                        }
                        break;
                    case DataType.BOOLEAN:
                        try {
                            attributes.Add(new Attribute(Boolean.Parse(value)));
                        } catch (Exception ex) {
                            attributes.Add(null);
                        }
                        break;
                    case DataType.DATE:
                        try {
                            attributes.Add(new Attribute(DateTime.Parse(value)));
                        } catch (Exception ex) {
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
                MessageBox.Show("Les valeurs suivantes ne sont pas au bon format : " + String.Join(", ", badTextBoxes), "Erreur", MessageBoxButton.OK);
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
            } catch (Exception ex) {
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
                this.Top = Owner.Top + 30;
                this.Left = Owner.Left + 8;
                this.Height = Owner.Height - 38;
                this.Width = Owner.Width - 16;
            }
            else
            {
                this.Top = 0;
                this.Left = 0;
                this.Height = Owner.Height;
                this.Width = Owner.Width;
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
