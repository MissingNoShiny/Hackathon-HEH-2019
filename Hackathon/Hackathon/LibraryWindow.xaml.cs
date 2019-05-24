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

namespace Hackathon
{
    /// <summary>
    /// Logique d'interaction pour LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window
    {
        private LibraryManager libraryManager;
        Library library;
        public LibraryWindow(LibraryManager libraryManager)
        {
            this.libraryManager = libraryManager;
            InitializeComponent();
            this.Theme();
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            if (mainWindow.Admin){
                edit_struct_button.Width = 50;
                edit_struct_button.Focusable = true;
            }
            this.Title = "NOM DE LA BIBLIO";
            this.Show();
        }
        private void Add_button(object sender, RoutedEventArgs e)
        {
            //CREER NOUVELLE BIBLIO
        }
        private void Edit_struct_button(object sender, RoutedEventArgs e)
        {
            //EDIT STRUCTURE BIBLIO
            UpdateLibraryWindow window = new UpdateLibraryWindow(libraryManager);
            window.Owner = this;
            window.WindowState = this.WindowState;
            window.Left = this.Left + 8;
            window.Top = this.Top + 30;
            window.Width = this.Width - 16;
            window.Height = this.Height - 38;
            window.page_title.Content = "ÉDITER UNE BIBLIOTHÈQUE";
        }
        private void Edit_item_button(object sender, RoutedEventArgs e)
        {
            //EDIT ITEM BIBLIO
        }
        private void Delete_button(object sender, RoutedEventArgs e)
        {
            //DELETE SELECTED BIBLIO
            MessageBoxResult dialresult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet item ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (dialresult == MessageBoxResult.Yes) {
                //TODO: delete the selected item
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
            if (search_box.Text != "" && search_box.Width>0)
            {
                //lance la recherche
                MessageBox.Show("Recherche de : " + field);
            }
        }
        private void Resize_window(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                item_list.Width = this.Width;
                item_list.Height = this.Height - 100;
                item_list.Margin = new Thickness(0, 7, 0, 70);
                rectangle_grid.Width = this.Width;
            }
        }

        /*APPLYING THEME*/
        public void Theme()
        {
            if (Hackathon.Properties.Settings.Default.Theme == "Light")
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                page_title.Foreground = new SolidColorBrush(Colors.Black);
                page_content.Foreground = new SolidColorBrush(Colors.Black);
                search_box.Background = new SolidColorBrush(Colors.White);
                search_box.Foreground = new SolidColorBrush(Colors.Black);
                item_list.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
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
                edit_struct_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_struct_button.png")));
                edit_item_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_button.png")));
                delete_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/delete_button.png")));
                add_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/add_button.png")));                
            }
        }

        private void Window_maximized(object sender, EventArgs e)
        {
            item_list.Width = this.Width;
            item_list.Height = this.Height - 100;
            rectangle_grid.Width = 2000;
        }
    }
}
