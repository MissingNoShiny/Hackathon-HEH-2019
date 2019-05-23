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
        Library library;
        public LibraryWindow()
        {
            InitializeComponent();
            this.Theme();
            this.Show();
        }
        private void Open_button(object sender, RoutedEventArgs e)
        {
            //OPEN SELECTED BIBLIO
        }
        private void Add_button(object sender, RoutedEventArgs e)
        {
            //CREER NOUVELLE BIBLIO
        }
        private void Edit_struct_button(object sender, RoutedEventArgs e)
        {
            //EDIT STRUCTURE BIBLIO
            UpdateLibraryWindow window = new UpdateLibraryWindow();
            window.Owner = this;
            window.WindowState = this.WindowState;
        }
        private void Edit_item_button(object sender, RoutedEventArgs e)
        {
            //EDIT ITEM BIBLIO
        }
        private void Delete_button(object sender, RoutedEventArgs e)
        {
            //DELETE SELECTED BIBLIO
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
                edit_struct_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_button_light.png")));
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
                edit_struct_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_button.png")));
                edit_item_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/edit_button.png")));
                delete_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/delete_button.png")));
                add_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/add_button.png")));                
            }
        }
    }
}
