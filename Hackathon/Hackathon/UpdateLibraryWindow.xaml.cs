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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Text.RegularExpressions;

namespace Gooboi
{
    /// <summary>
    /// Logique d'interaction pour UpdateLibraryWindow.xaml
    /// </summary>
    public partial class UpdateLibraryWindow : Window
    {
        private static Dictionary<String, DataType> dataTypesNames = new Dictionary<String, DataType> {
            ["String"] = DataType.STRING,
            ["Int"] = DataType.INTEGER,
            ["Bool"] = DataType.BOOLEAN,
            ["Date"] = DataType.DATE
        };
        public static Dictionary<DataType, String> namesDataType = dataTypesNames.ToDictionary((x) => x.Value, (x) => x.Key);

        private LibraryManager libraryManager;
        private List<TextBlock> textBlocks;
        private List<TextBox> textBoxes;
        private List<ComboBox> comboBoxes;
        private int panelIndex;

        private Library library;
        private bool edition;
        
        public UpdateLibraryWindow(LibraryManager libraryManager) {
            initializeWindow();
            edition = false;
            this.libraryManager = libraryManager;
        }

        public UpdateLibraryWindow(LibraryManager libraryManager, Library library) {
            initializeWindow();
            page_title.Content = "ÉDITER UNE COLLECTION";
            edition = true;
            this.libraryManager = libraryManager;
            this.library = library;
            name.Text = name.Name = library.Nom;
            foreach (String attributeName in library.AttributeNames)
                AddRow(attributeName, library.AttributeTypes[attributeName]);
        }

        private void initializeWindow() {
            InitializeComponent();
            Theme();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Window_Position;
            timer.Interval = TimeSpan.FromSeconds(0.0001);
            timer.Start();
            this.Show();            
                textBlocks = new List<TextBlock>();
            textBoxes = new List<TextBox>();
            comboBoxes = new List<ComboBox>();
            panelIndex = 0;
        }

        private void add_object_click(object sender, RoutedEventArgs e)
        {
            if (panelIndex > 9)
                return;
            AddRow();

        }

        private void AddRow(String text = "", DataType datatype = DataType.STRING) {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Margin = new Thickness(0, 5, 0, 0);

            TextBlock txb = new TextBlock();
            txb.FontFamily = new FontFamily("Segoe UI");
            txb.FontSize = 13;
            txb.FontWeight = FontWeights.Bold;
            txb.Foreground = new SolidColorBrush(Colors.DimGray);
            txb.Text = (panelIndex + 1).ToString();            
            txb.Width = 23;
            txb.Height = 23;
            textBlocks.Add(txb);

            TextBox tb = new TextBox();
            tb.Width = 120;
            tb.Height = 25;
            tb.FontSize = 16;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            tb.Background = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
            tb.Text = text;
            if (text != "" && text.All(char.IsLetterOrDigit)) tb.Name = text;
            textBoxes.Add(tb);

            ComboBox cb = new ComboBox();
            cb.SelectedIndex = 0;
            dataTypesNames.Keys.ToList().ForEach(x => cb.Items.Add(x));
            cb.Width = 82;
            cb.HorizontalAlignment = HorizontalAlignment.Center;
            cb.Margin = new Thickness(10, 0, 10, 0);
            cb.SelectedItem = namesDataType[datatype];
            cb.IsEnabled = String.IsNullOrEmpty(text);
            comboBoxes.Add(cb);

            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/delete_list_button.png"));
            myBrush.ImageSource = image.Source;

            Button btn = new Button();
            btn.Width = 25;
            btn.Height = 25;
            btn.Background = myBrush;
            btn.HorizontalAlignment = HorizontalAlignment.Right;
            btn.Click += delegate {
                if (edition && !cb.IsEnabled) {
                    MessageBoxResult dr = MessageBox.Show("Attention, cette action entraînera la suppression de données !\nVoulez-vous continuer ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (dr == MessageBoxResult.No) return;
                    library.RemoveAttribute(tb.Name);
                }
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

        private void Save_library_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(name.Text)) {
                WarningWindow warning = new WarningWindow();//start
                warning.Owner = this;
                warning.Message_show(2);
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
                this.Opacity = 1;
                this.Effect = null;//end
                return;
            }
            if (name.Text.Length > 24) {
                WarningWindow warning = new WarningWindow();//start
                warning.Owner = this;
                warning.Message_show(3);
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
                this.Opacity = 1;
                this.Effect = null;//end
                return;
            }
            if (name.Text.All(char.IsLetterOrDigit) == false) {
                WarningWindow warning = new WarningWindow();//start
                warning.Owner = this;
                warning.Message_show(4);
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
                this.Opacity = 1;
                this.Effect = null;//end
                return;
            }
            if (libraryManager.Libraries.Select(x => x.Nom).Contains(name.Text) && (!edition || name.Name != name.Text)) {
                WarningWindow warning = new WarningWindow();//start
                warning.Owner = this;
                warning.Message_show(5);
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
                this.Opacity = 1;
                this.Effect = null;//end
                return;
            }
            if (textBoxes.Count == 0) {
                WarningWindow warning = new WarningWindow();//start
                warning.Owner = this;
                warning.Message_show(6);
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
                this.Opacity = 1;
                this.Effect = null;//end
                return;
            }
            List<int> emptyTextBoxes = Enumerable.Range(0, textBoxes.Count).Where(i => String.IsNullOrEmpty(textBoxes[i].Text)).ToList();
            emptyTextBoxes = emptyTextBoxes.Select(x => { x += 1; return x; }).ToList();
            if (emptyTextBoxes.Count > 0) {
                WarningWindow warning = new WarningWindow();//start
                warning.Owner = this;
                warning.Message_show(7);
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
                warning.object_content.Text = "Ligne " + String.Join(", ", emptyTextBoxes);
                warning.ShowDialog();
                this.Opacity = 1;
                this.Effect = null;//end
                return;
            }
            List<String> attributeNames = textBoxes.Select(x => x.Text).ToList();
            if (attributeNames.Count != attributeNames.Distinct().Count()) {
                WarningWindow warning = new WarningWindow();//start
                warning.Owner = this;
                warning.Message_show(8);
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
                this.Opacity = 1;
                this.Effect = null;//end
                return;
            }
            foreach (String name in attributeNames){
                if (name.All(char.IsLetterOrDigit) == false) {
                    WarningWindow warning = new WarningWindow();//start
                    warning.Owner = this;
                    warning.Message_show(9);
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
                    this.Opacity = 1;
                    this.Effect = null;//end
                    return;
                }      
            }
            if (!edition) {
                List<DataType> dataTypes = comboBoxes.Select(x => dataTypesNames[(String)x.SelectedItem]).ToList();
                Dictionary<String, DataType> attributeType = attributeNames.Zip(dataTypes, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
                Library tempLibrary = new Library(name.Text, attributeNames, attributeType);
                libraryManager.AddLibrary(tempLibrary);
                tempLibrary.Save(LibraryManager.DefaultLibrariesPath);
            } else {
                for (int i = 0; i < textBoxes.Count; i++) {
                    if (String.IsNullOrEmpty(textBoxes[i].Name))
                        library.AddAttribute(textBoxes[i].Text, dataTypesNames[(String)comboBoxes[i].SelectedItem]);
                    else if (textBoxes[i].Name != textBoxes[i].Text)
                        library.RenameAttribute(textBoxes[i].Name, textBoxes[i].Text);
                }
                library.Save(LibraryManager.DefaultLibrariesPath);
            }
            
            this.Close();
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            mainWindow.Content_Visibility();
            Owner.Focus();
        }

        private void Cancel_library_Click(object sender, RoutedEventArgs e)
        {
            Owner.Focus();
            this.Close();
        }

        /*APPLYING THEME*/
        public void Theme()
        {
            if (Gooboi.Properties.Settings.Default.Theme == "Light")
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
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            if (mainWindow.WindowState != WindowState.Maximized)
            {
                this.Top = mainWindow.Top + 30;
                this.Left = mainWindow.Left + 8;
                this.Height = mainWindow.Height - 38;
                this.Width = mainWindow.Width - 16;
            }
            else
            {
                this.Top = 0;
                this.Left = 0;
                this.Height = Owner.Height;
                this.Width = Owner.Width;
                save_library.Margin = new Thickness(0, 0, 25, 25);
            }                        
        }
        /*DISABLE Touch Cursor*/
        protected override void OnPreviewTouchDown(TouchEventArgs e)
        {
            base.OnPreviewTouchDown(e);
            Cursor = Cursors.None;
        }
        protected override void OnPreviewTouchMove(TouchEventArgs e)
        {
            base.OnPreviewTouchMove(e);
            Cursor = Cursors.None;
        }
        protected override void OnGotMouseCapture(MouseEventArgs e)
        {
            base.OnGotMouseCapture(e);
            Cursor = Cursors.None;
        }
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (e.StylusDevice == null)
                Cursor = Cursors.Arrow;
        }
    }
}
