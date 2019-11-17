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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Gooboi
{
    public partial class MainWindow : Window
    {

        private LibraryManager libraryManager;
        PanelWindow panelWindow;
        public bool Admin {
            get; private set;
        }

        public MainWindow()
        {
            InitializeComponent();
            Width = SystemParameters.PrimaryScreenWidth / 2;
            Height = SystemParameters.PrimaryScreenHeight / 2;
            this.Theme();
            this.Show();            
            Left += 150;
            Admin = false;

            libraryManager = new LibraryManager();
            libraryManager.OpenLibraries();
            if (Gooboi.Properties.Settings.Default.Advanced == "Admin")
            {
                Admin_Mode();
            }

            panelWindow = new PanelWindow(this);            
            LoadCollection(libraryManager.Libraries.Count > 0);
            library_list.ItemsSource = libraryManager.Libraries;
            library_list.Columns[0].MaxWidth = 0;
            library_list.Columns[1].MaxWidth = 0;
            library_list.Columns[2].MaxWidth = 0;
            library_list.Columns[3].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            library_list.Columns[4].MaxWidth = 300;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Content_Load;
            timer.Interval = TimeSpan.FromSeconds(0.25);
            timer.Start();
            Is_Library_empty();
            if (this.WindowState == WindowState.Maximized)
            {
                if (libraryManager.Libraries.Count == 0)
                { library_list.Width = 0; }
                else { 
                library_list.Width = window_background.Width; }
                library_list.Height = window_background.Height - 140;
                library_list.RowHeight = 75;
                maximize_button.Width = 0;
                maximize_button.Focusable = false;
                minimize_button.Width = 0;
                minimize_button.Focusable = false;
                search_box.Margin = new Thickness(0, 20, 25, 0);
                search_button.Margin = new Thickness(0, 20, 25, 0);
            }
            else
            {
                maximize_button.Width = 50;
                maximize_button.Focusable = true;
            }
                    
            Application.Current.MainWindow = this;
        }

        //CONTENT
        private void Content_Load(object sender, EventArgs e)
        {
            if (library_list.SelectedItem == null)
            {
                delete_button.Width = 0;
                open_button.Width = 0;
            }
            else
            {
                if (Admin) { delete_button.Width = 50; } else { delete_button.Width = 0; }             
                open_button.Width = 150;
            }
        }

            /*BUTTONS CLICK*/
            private void Navigation_Button(object sender, RoutedEventArgs e) {
            panelWindow.WindowState = WindowState;
            if (!panelWindow.IsLoaded)
            {
                if (Left >= 300)
                {
                    panelWindow = new PanelWindow(this);
                }
                else
                {
                    Left = 300;
                    panelWindow = new PanelWindow(this);
                }
            }
            else
            {
                panelWindow.Close();
            }
            }
        private void Maximize_Button(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        private void Minimize_Button(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            library_list.RowHeight = 50;
            maximize_button.Width = 50;
            maximize_button.Focusable = true;
            minimize_button.Width = 0;
            minimize_button.Focusable = false;
        }
        private void Open_button(object sender, RoutedEventArgs e)
        {
            //OPEN SELECTED BIBLIO
            if (library_list.SelectedItem == null)
                return;
            panelWindow.Close();
            LibraryWindow window = new LibraryWindow(libraryManager, (Library) library_list.SelectedItem);
            window.Owner = this;
            window.Top = this.Top + 30;
            window.Left = this.Left;
            window.Height = this.Height - 30;
            window.Width = this.Width;
            window.Show();
            Opacity = 0;
        }
        private void Add_button(object sender, RoutedEventArgs e)
        {
            //CREER NOUVELLE BIBLIO
            UpdateLibraryWindow window = new UpdateLibraryWindow(libraryManager);
            panelWindow.Close();
            window.Owner = this;            
        }
        public void Delete_action()
        {
                    
            File.Delete(LibraryManager.DefaultLibrariesPath + libraryManager.Libraries.ElementAt(library_list.SelectedIndex).Nom + ".libr");
            libraryManager.Libraries.RemoveAt(library_list.SelectedIndex);
            Is_Library_empty();
                    library_list.Items.Refresh();
        }

        private void Delete_button(object sender, RoutedEventArgs e)
        {
            WarningWindow warning = new WarningWindow();
            warning.Owner = this;
            warning.Message_show(1);
            warning.object_content.Text= libraryManager.Libraries.ElementAt(library_list.SelectedIndex).Nom;
            if (this.WindowState == WindowState.Normal)
            {
                warning.Width = Width / 1.5;
                warning.Left = Left + warning.Width / 4;
                warning.Height = Height / 2;
                warning.Top = Top + warning.Height / 2;
            }
            panelWindow.Close();
            this.Opacity = 0.5;
            BlurEffect blur;
            blur = new BlurEffect();
            blur.Radius = 15;
            this.Effect = blur;
            warning.ShowDialog();
            this.Opacity = 1;
            this.Effect = null;
        }

        /*METHODES*/
        private void LoadCollection(bool nocollection)//vérif si biblitothèque existe ou pas
        {
            if (!nocollection)
            {
                page_title.Content = "BIENVENUE";
                page_content.Width= Double.NaN;
            }
            else
            {
                page_title.Content = "COLLECTIONS";
                Content_Visibility();                
            }
        }

        public void Content_Visibility()
        {
            search_box.Width = 150;
            search_button.Width = 25;
            if (Admin)
            {
                library_list.Width = this.Width;
                library_list.ItemsSource = new List<int>();
                library_list.ItemsSource = libraryManager.Libraries;
                library_list.Columns[0].MaxWidth = 0;
                library_list.Columns[1].MaxWidth = 0;
                library_list.Columns[2].MaxWidth = 0;
                library_list.Columns[3].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
                library_list.Columns[4].MaxWidth = 300;
                Is_Library_empty();
            }
        }
        public void Admin_Mode()
        {
            Admin = !Admin;
            if (Admin)
            {
                page_title.Content = "MODE AVANCÉ";
                Is_Library_empty();
                add_button.Width = 50;
                Content_Visibility();
            }
            else
            {
                page_title.Content = "COLLECTIONS";
                Is_Library_empty();
                add_button.Width = 0;
            }
        }
        
        public void Display_Fullscreen (bool fullscreen)
        {
            if (fullscreen)
            {
                if (panelWindow.IsLoaded)
                {
                    this.WindowStyle = System.Windows.WindowStyle.None;
                    navigation_button.Margin = new Thickness(305, 10, 0, 0);
                    add_button.Margin = new Thickness(305, 0, 0, 10);
                }
                else
                {
                    this.WindowStyle = System.Windows.WindowStyle.None;
                    navigation_button.Margin = new Thickness(10, 10, 0, 0);
                    add_button.Margin = new Thickness(10, 0, 0, 10);
                }
                ui_titlebar.Visibility = Visibility.Collapsed;
                ui_page.Margin = new Thickness(5, 5, 5, 0);
            }
            else
            {
                this.WindowStyle = System.Windows.WindowStyle.None;
                navigation_button.Margin = new Thickness(10, 10, 0, 0);
                add_button.Margin = new Thickness(10, 0, 0, 10);
                ui_titlebar.Visibility = Visibility.Visible;
                ui_page.Margin = new Thickness(0, 30, 0, 0);
            }
        }
             
        /*APPLYING THEME*/
        public void Theme()
        {
            if (Gooboi.Properties.Settings.Default.Theme=="Light")
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
                page_title.Foreground = new SolidColorBrush(Colors.Black);
                page_content.Foreground = new SolidColorBrush(Colors.Black);
                library_list.Foreground = new SolidColorBrush(Colors.Black);
                search_box.Background= new SolidColorBrush(Colors.White);
                search_box.Foreground = new SolidColorBrush(Colors.Black);
                library_list.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                open_button.Foreground = new SolidColorBrush(Colors.Black);
                open_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/open_button_light.png")));
                delete_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/delete_button_light.png")));
                add_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/add_button_light.png")));
                maximize_button.Opacity = 1;
                maximize_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                maximize_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/maximize_button_light.png")));
                minimize_button.Opacity = 1;
                minimize_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                minimize_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/minimize_button_light.png")));
                navigation_button.Opacity = 1;
                navigation_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                navigation_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/navigation_button_light.png")));
            }
            else
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(21, 21, 21));
                page_title.Foreground = new SolidColorBrush(Colors.White);
                page_content.Foreground = new SolidColorBrush(Colors.White);
                library_list.Foreground = new SolidColorBrush(Colors.White);
                search_box.Background = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                search_box.Foreground = new SolidColorBrush(Colors.White);
                library_list.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                open_button.Foreground = new SolidColorBrush(Colors.White);
                open_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/open_button.png")));
                delete_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/delete_button.png")));
                add_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/add_button.png")));
                maximize_button.Opacity = 0.6;
                maximize_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                maximize_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/maximize_button.png")));
                minimize_button.Opacity = 0.6;
                minimize_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                minimize_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/minimize_button.png")));
                navigation_button.Opacity = 0.6;
                navigation_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                navigation_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/navigation_button.png")));
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
            if (search_box.Text == "" && library_list.ItemsSource == libraryManager.Libraries)
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
            if (e.Key == Key.Return){
                Search_field(search_box.Text);
            }
        }

        private void Search_field(string field)
        {
            if (search_box.Width > 0)
            {
                if (search_box.Text == "") {
                    library_list.ItemsSource = new List<int>();
                    library_list.ItemsSource = libraryManager.Libraries;
                }
                else {
                    library_list.ItemsSource = new List<int>();
                    library_list.ItemsSource = libraryManager.Search(field);
                }
                library_list.Columns[0].MaxWidth = 0;
                library_list.Columns[1].MaxWidth = 0;
                library_list.Columns[2].MaxWidth = 0;
                library_list.Columns[3].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
                library_list.Columns[4].MaxWidth = 300;
            }
        }

        private void Resize_window(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                library_list.Width = this.Width;
                library_list.Height = this.Height - 140;
                library_list.Margin = new Thickness(0, 7, 0, 70);
            }
        }

        private void Window_maximized(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                library_list.Width = window_background.Width;
                library_list.Height = window_background.Height - 140;
                library_list.RowHeight = 75;
                maximize_button.Width = 0;
                maximize_button.Focusable = false;
                minimize_button.Width = 50;
                minimize_button.Focusable = true;
            }
        }

        private void Is_Library_empty() {//Verify the emptiness of the library list and hide some elements if it's empty
            if(libraryManager.Libraries.Count == 0) {
                library_list.Width = 0;
                page_content.Width = Double.NaN;
                if (Admin) {
                    page_content.Text = "Aucune collection";
                } 
                else {
                    page_content.Text = "Vous ne possédez pas de collection. Pour éditer, veuillez passer en mode avancé.";
                }
            } 
            else {
                library_list.Height = this.Height - 160;
                page_content.Text = "";
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            panelWindow.DisableBlur();
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            panelWindow.EnableBlur();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}