using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window
    {
        private Library library;
        private LibraryManager libraryManager;
        private DataTable dataTable;
        public LibraryWindow(LibraryManager libraryManager, Library library)
        {
            this.libraryManager = libraryManager;
            this.library = library;
            InitializeComponent();
            this.Theme();
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            dataTable = new DataTable();
            item_list.DataContext = dataTable.DefaultView;
            item_list.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            UpdateItems();

            if (mainWindow.Admin){
                edit_struct_button.Width = 50;
                page_title.Margin = new Thickness(70, 20, 0, 0);
                edit_struct_button.Focusable = true;
            }
            page_title.Content = library.Nom;
            this.Show();
        }
        private void Add_button(object sender, RoutedEventArgs e)
        {
            UpdateItemWindow window = new UpdateItemWindow(library);
            window.Owner = this;
            window.Show();
        }
        private void Edit_struct_button(object sender, RoutedEventArgs e)
        {
            //EDIT STRUCTURE BIBLIO
            UpdateLibraryWindow window = new UpdateLibraryWindow(libraryManager, library);
            window.Owner = this;
        }
        private void Edit_item_button(object sender, RoutedEventArgs e)
        {
            //EDIT ITEM BIBLIO
            if (item_list.SelectedItem == null)
                return;
            if (item_list.SelectedIndex < 0 || item_list.SelectedIndex > library.Items.Count - 1)
                return;
            UpdateItemWindow window = new UpdateItemWindow(library, item_list.SelectedIndex);
            window.Owner = this;
            window.page_title.Content = "ÉDITER UN ÉLÉMENT";
            window.Show();
        }
        private void Delete_button(object sender, RoutedEventArgs e)
        {
            int index = item_list.SelectedIndex;
            MessageBoxResult dialresult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet item ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (dialresult == MessageBoxResult.Yes) {
                library.RemoveItem(index);
                UpdateItems();
            }
        }
        private void Search_focus(object sender, RoutedEventArgs e)
        {
            if (search_box.Text == "Rechercher")
            {
                search_box.Text = "";
                search_box.Opacity = 1;
            }
        }

        private void Search_unfocus(object sender, RoutedEventArgs e)
        {
            if (search_box.Text == "")
            {
                search_box.Text = "Rechercher";
                search_box.Opacity = 0.4;
            }
        }
        private void Search_Button(object sender, RoutedEventArgs e)
        {
            if (search_box.Text != "" && search_box.Text != "Rechercher")
            {
                Search_field(search_box.Text);
            }
        }

        private void Enter_search_key(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Search_field(search_box.Text);
            }
        }

        private void Search_field(string field)
        {
            if (search_box.Width > 0)
            {
                if (search_box.Text == "")//TODO
                {
                    item_list.ItemsSource = new List<int>();
                    item_list.ItemsSource = library.Items;
                }
                else
                {
                    item_list.ItemsSource = new List<int>();
                    item_list.ItemsSource = library.Search(field);
                }
            }
        }
        private void Resize_window(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                item_list.Width = this.Width-14;
                item_list.Height = this.Height - 140;
                item_list.Margin = new Thickness(0, 7, 0, 70);
            }
        }

        /*APPLYING THEME*/
        public void Theme()
        {
            if (Gooboi.Properties.Settings.Default.Theme == "Light")
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                page_title.Foreground = new SolidColorBrush(Colors.Black);
                page_content.Foreground = new SolidColorBrush(Colors.Black);
                search_box.Background = new SolidColorBrush(Colors.White);
                search_box.Foreground = new SolidColorBrush(Colors.Black);
                item_list.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                viewimage_button.Foreground = new SolidColorBrush(Colors.Black);
                viewimage_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/viewimage_light.png")));
                edit_struct_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_struct_button_light.png")));
                edit_item_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_button_light.png")));
                delete_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/delete_button_light.png")));
                add_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/add_button_light.png")));                
            }
            else
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
                page_title.Foreground = new SolidColorBrush(Colors.White);
                page_content.Foreground = new SolidColorBrush(Colors.White);
                search_box.Background = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                search_box.Foreground = new SolidColorBrush(Colors.White);
                item_list.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                viewimage_button.Foreground = new SolidColorBrush(Colors.White);
                viewimage_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/viewimage.png")));
                edit_struct_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_struct_button.png")));
                edit_item_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_button.png")));
                delete_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/delete_button.png")));
                add_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/add_button.png")));                
            }
        }

        private void Window_maximized(object sender, EventArgs e)
        {
            item_list.Width = image_back.Width;
            item_list.Height = image_back.Height - 140;
        }

        private void Close_window(object sender, RoutedEventArgs e)
        {
            Owner.Focus();
        }

        
        private void UpdateItems() {
            CreateColumns();
            dataTable.Rows.Clear();
            for (int i = 0; i < library.Items.Count; i++) {
                Item item = library.Items[i];
                dataTable.Rows.Add(item.Values.Select(x => x.Value.ToString()).ToList().ToArray());
                item_list.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);                
            }

        }

        private void CreateColumns() {
            dataTable.Columns.Clear();
            foreach (String name in library.AttributeNames) {
                DataColumn dc = new DataColumn(name, typeof(String));
                dataTable.Columns.Add(dc);
            }
        }
        private void LibraryWindowActivated(object sender, EventArgs e)
        {
            UpdateItems();
        }

        private void Viewimage_button(object sender, RoutedEventArgs e)
        {
            if (item_list.SelectedIndex < 0 || library.Items[item_list.SelectedIndex].ImagePath == null)
                return;

            Window pic = new Window();
            pic.Show();

            BitmapImage im = new BitmapImage(library.Items[item_list.SelectedIndex].ImagePath);
            Image i = new Image();
            i.Source = im;

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = im;
            pic.Background = brush;
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
