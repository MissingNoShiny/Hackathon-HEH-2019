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
using System.Data;

namespace Hackathon
{
    /// <summary>
    /// Logique d'interaction pour UpdateLibraryWindow.xaml
    /// </summary>
    public partial class UpdateLibraryWindow : Window
    {
        private Dictionary<String, DataType> dataTypesNames = new Dictionary<String, DataType> {
            ["String"] = DataType.STRING,
            ["Int"] = DataType.INTEGER,
            ["Bool"] = DataType.BOOLEAN,
            ["Date"] = DataType.DATE
        };

        private LibraryManager libraryManager;
        private List<TextBox> textBoxes;
        private List<ComboBox> comboBoxes;
        
        public UpdateLibraryWindow(LibraryManager libraryManager) {
            this.libraryManager = libraryManager;
            textBoxes = new List<TextBox>();
            comboBoxes = new List<ComboBox>();
            InitializeComponent();
            Theme();
            this.Show();
            
        }

        private void add_object_click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Margin = new Thickness(0,5,0,0);

            TextBox tb = new TextBox();
            tb.Width = 120;
            tb.Height = 23;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            textBoxes.Add(tb);

            ComboBox cb = new ComboBox();
            cb.SelectedIndex = 0;
            cb.Items.Add("String");
            cb.Items.Add("Int");
            cb.Items.Add("Bool");
            cb.Items.Add("Date");
            cb.Width = 82;
            cb.HorizontalAlignment = HorizontalAlignment.Center;
            cb.Margin = new Thickness(10, 0, 10, 0);
            comboBoxes.Add(cb);
           
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/delete_list_button.png"));
            myBrush.ImageSource = image.Source;

            Button btn = new Button();
            btn.Width = 20;
            btn.Height = 23;
            btn.Background = myBrush;
            btn.HorizontalAlignment = HorizontalAlignment.Right;
            btn.Click += delegate {
                textBoxes.Remove(tb);
                comboBoxes.Remove(cb);
                stackpanel.Children.Remove(sp);
            };

            sp.Children.Add(tb);
            sp.Children.Add(cb);
            sp.Children.Add(btn);
            this.stackpanel.Children.Add(sp);
        }

        private void del_object_click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Save_library_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(name.Text)) {
                MessageBox.Show("La bibliothèque doit avoir un nom !", "Erreur", MessageBoxButton.OK);
                return;
            }
            if (textBoxes.Count == 0) {
                MessageBox.Show("La bibliothèue doit avoir au moins un attribut !", "Erreur", MessageBoxButton.OK);
                return;
            }
            if (textBoxes.Any(tb => String.IsNullOrEmpty(tb.Text))) {
                MessageBox.Show("Tous les attributs doivent avoir un nom !", "Erreur", MessageBoxButton.OK);
                return;
            }
            List<String> attributeNames = textBoxes.Select(x => x.Text).ToList();
            List<DataType> dataTypes = comboBoxes.Select(x => (DataType) x.SelectedItem).ToList();
        }

        private void Cancel_library_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*APPLYING THEME*/
        public void Theme()
        {
            if (Hackathon.Properties.Settings.Default.Theme == "Light")
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(217, 217, 217));
                page_title.Foreground = new SolidColorBrush(Colors.Black);
                cancel_library.Foreground = new SolidColorBrush(Colors.Black);
                cancel_library.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/cancel_button_light.png")));
                save_library.Foreground = new SolidColorBrush(Colors.Black);
                save_library.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/validation_button_light.png")));                
                add_object.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/add_button_light.png")));
            }
        }
        }
}
