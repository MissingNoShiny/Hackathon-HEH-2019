﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hackathon
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
            //CHECK IF COLLECTION(s) > DISPLAY WelcomeScreen
            LoadCollection(false);

            libraryManager = new LibraryManager();
            panelWindow = new PanelWindow(this);
            Admin = false;

            navigation_button.Width = 50;
            library_list.ItemsSource = libraryManager.Libraries;
            if (this.WindowState == WindowState.Normal)
            {
                maximize_button.Width = 50;
                maximize_button.Focusable = true;
            }
            Application.Current.MainWindow = this;
        }
        /*BUTTONS CLICK*/
        private void Navigation_Button(object sender, RoutedEventArgs e) {
            panelWindow.WindowState = WindowState;
            if (!panelWindow.IsLoaded)
            {
                panelWindow = new PanelWindow(this);
            }
            else
            {
                panelWindow.Close();
            }
            }
        private void Maximize_Button(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            maximize_button.Width = 0;
            maximize_button.Focusable = false;
            minimize_button.Width = 50;
            minimize_button.Focusable = true;
        }
        private void Minimize_Button(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            maximize_button.Width = 50;
            maximize_button.Focusable = true;
            minimize_button.Width = 0;
            minimize_button.Focusable = false;
        }
        private void Open_button(object sender, RoutedEventArgs e)
        {
            //OPEN SELECTED BIBLIO
            LibraryWindow window = new LibraryWindow(libraryManager);
            window.Owner = this;
            window.WindowState = this.WindowState;
        }
        private void Add_button(object sender, RoutedEventArgs e)
        {
            //CREER NOUVELLE BIBLIO
            UpdateLibraryWindow window = new UpdateLibraryWindow(libraryManager);
            window.Owner = this;
            window.WindowState = this.WindowState;
            window.Left = this.Left+8;
            window.Top = this.Top+30;
            window.Width = this.Width-16;
            window.Height = this.Height-38;
            panelWindow.Close();
        }

        private void Delete_button(object sender, RoutedEventArgs e)
        {
            //DELETE SELECTED BIBLIO
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
                page_title.Content = "BIBLIOTHÈQUES";
                Content_Visibility();                
            }
        }

        private void Content_Visibility()
        {
            open_button.Width = 150;
            search_box.Width = 150;
            search_button.Width = 25;
            library_list.Width = this.Width;
            library_list.Height = this.Height - 100;
        }
        public void Admin_Mode()
        {
            Admin = !Admin;
            if (Admin)
            {
                //AFFICHE LES BOUTONS MODE AVANCE
                page_title.Content = "MODE AVANCÉ";
                page_content.Content = "Aucune bibliothèque";
                add_button.Width = 50;
                delete_button.Width = 50;
                Content_Visibility();
            }
            else
            {
                //REPASSE EN VUE USER  !!REEXECUTER LOADCOLLECTION!!
                page_title.Content = "BIBLIOTHÈQUES";
                add_button.Width = 0;
                delete_button.Width = 0;
            }            
        }
        
        public void Display_Fullscreen (bool fullscreen)
        {
            if (fullscreen)
            {
                if (panelWindow.IsLoaded)
                {
                    this.WindowStyle = System.Windows.WindowStyle.None;
                    navigation_button.Margin = new Thickness(300, 10, 0, 0);
                }
                else
                {
                    this.WindowStyle = System.Windows.WindowStyle.None;
                    navigation_button.Margin = new Thickness(10, 10, 0, 0);
                }
            }
            else
            {
                this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                navigation_button.Margin = new Thickness(10, 10, 0, 0);
            }
        }
             
        /*APPLYING THEME*/
        public void Theme()
        {
            if (Hackathon.Properties.Settings.Default.Theme=="Light")
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                page_title.Foreground = new SolidColorBrush(Colors.Black);
                page_content.Foreground = new SolidColorBrush(Colors.Black);
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
                window_background.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
                page_title.Foreground = new SolidColorBrush(Colors.White);
                page_content.Foreground = new SolidColorBrush(Colors.White);
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
            if (e.Key == Key.Return){
                Search_field(search_box.Text);
            }
        }

        private void Search_field(string field)
        {
            if (search_box.Text != "" && search_box.Width > 0)
            {
                //lance la recherche
                MessageBox.Show("Recherche de : "+field);
            }
        }

        private void Resize_window(object sender, SizeChangedEventArgs e)
        {
            //IF library >0
            library_list.Width = this.Width;
            library_list.Height = this.Height - 100;
            library_list.Margin = new Thickness(0, 7, 0, 70);
            rectangle_grid.Width = this.Width;
        }
    }
}