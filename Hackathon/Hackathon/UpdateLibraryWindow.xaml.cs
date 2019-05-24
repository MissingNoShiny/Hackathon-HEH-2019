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
        private List<TextBlock> textBlocks;
        private List<TextBox> textBoxes;
        private List<ComboBox> comboBoxes;
        private int panelIndex;
        
        public UpdateLibraryWindow(LibraryManager libraryManager) {
            this.libraryManager = libraryManager;
            textBlocks = new List<TextBlock>();
            textBoxes = new List<TextBox>();
            comboBoxes = new List<ComboBox>();
            panelIndex = 0;
            InitializeComponent();
            Theme();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Window_Position;
            timer.Interval = TimeSpan.FromSeconds(0.0001);
            timer.Start();
            this.Show();
            
        }

        private void add_object_click(object sender, RoutedEventArgs e)
        {
            if (panelIndex > 9)
                return;

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Margin = new Thickness(0,5,0,0);

            TextBlock txb = new TextBlock();
            txb.FontFamily = new FontFamily("Segoe UI");
            txb.FontSize = 13;
            txb.FontWeight = FontWeights.Bold;
            txb.Foreground = new SolidColorBrush(Colors.DimGray);
            txb.Text = (panelIndex+1).ToString();
            txb.Width = 23;
            txb.Height = 23;
            textBlocks.Add(txb);

            TextBox tb = new TextBox();
            tb.Width = 120;
            tb.Height = 23;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            textBoxes.Add(tb);

            ComboBox cb = new ComboBox();
            cb.SelectedIndex = 0;
            dataTypesNames.Keys.ToList().ForEach(x => cb.Items.Add(x));
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
                int index = Int32.Parse(txb.Text);
                foreach (TextBlock textBlock in textBlocks.GetRange(index, textBlocks.Count - index)) {
                    textBlock.Text = (Int32.Parse(textBlock.Text) - 1).ToString();
                }
                panelIndex--;
                textBlocks.Remove(txb);
                textBoxes.Remove(tb);
                comboBoxes.Remove(cb);
                stackpanel.Children.Remove(sp);
            };

            sp.Children.Add(txb);
            sp.Children.Add(tb);
            sp.Children.Add(cb);
            sp.Children.Add(btn);
            this.stackpanel.Children.Add(sp);
            panelIndex++;
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
            if (name.Text.Length > 24) {
                MessageBox.Show("Le nom de la bibliothèque ne peut pas dépasser 24 caractères.", "Erreur", MessageBoxButton.OK);
                return;
            }
            if (libraryManager.Libraries.Select(x => x.Name).Contains(name.Text)) {
                MessageBox.Show("Une bibliothèque du même nom existe déjà, veuillez en choisir un différent.", "Erreur", MessageBoxButton.OK);
                return;
            }
            if (textBoxes.Count == 0) {
                MessageBox.Show("La bibliothèque doit avoir au moins un attribut !", "Erreur", MessageBoxButton.OK);
                return;
            }
            List<int> emptyTextBoxes = Enumerable.Range(0, textBoxes.Count).Where(i => String.IsNullOrEmpty(textBoxes[i].Text)).ToList();
            emptyTextBoxes = emptyTextBoxes.Select(x => { x += 1; return x; }).ToList();
            if (emptyTextBoxes.Count > 0) {
                MessageBox.Show("Tous les attributs doivent avoir un nom !\nLes attributs suivants n'en ont pas : " + String.Join(", ", emptyTextBoxes), "Erreur", MessageBoxButton.OK);
                return;
            }
            List<String> attributeNames = textBoxes.Select(x => x.Text).ToList();
            List<DataType> dataTypes = comboBoxes.Select(x => dataTypesNames[(String) x.SelectedItem]).ToList();
            Dictionary<String, DataType> attributeType = attributeNames.Zip(dataTypes, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
            libraryManager.AddLibrary(new Library(name.Text, attributeNames, attributeType));
            libraryManager.SaveLibraries();
            this.Close();
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
                nameText.Foreground = new SolidColorBrush(Colors.Black);
                info.Foreground = new SolidColorBrush(Colors.Black);
                cancel_library.Foreground = new SolidColorBrush(Colors.Black);
                cancel_library.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/back_button_light.png")));
                save_library.Foreground = new SolidColorBrush(Colors.Black);
                save_library.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/validation_button_light.png")));                
                add_object.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/add_button_light.png")));
            }
        }
        private void Window_Position(object sender, EventArgs e)
        {
            this.Top = Owner.Top + 30;
            this.Left = Owner.Left+8;
            this.Height = Owner.Height - 38;
            this.Width = Owner.Width-16;
            this.WindowState = Owner.WindowState;
        }
    }
}
