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
        public MainWindow()
        {
            InitializeComponent();
            Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
            Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
            this.Theme();
            this.Show();            
            Left += 150;
            //CHECK IF COLLECTION(s) > DISPLAY WelcomeScreen
            LoadCollection(false);
            Open_Panel();            
            if (this.WindowState == WindowState.Normal)
            {
                maximize_button.Width = 50;
                maximize_button.Focusable = true;
            }
        }
        /*BUTTONS CLICK*/
        private void Open_Navigation_Button(object sender, RoutedEventArgs e)
        {
            navigation_button.Width = 0;
            close_navigation_button.Width = 50;
            navigation_button.Focusable = false;
            close_navigation_button.Focusable = true;
            Open_Panel();
        }
        private void Close_Navigation_Button(object sender, RoutedEventArgs e)
        {
            navigation_button.Width = 50;
            close_navigation_button.Width = 0;
            navigation_button.Focusable = true;
            close_navigation_button.Focusable = false;
            var pannelwindow = Application.Current.Windows.OfType<PannelWindow>().FirstOrDefault();
            pannelwindow.Close();
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
            LibraryWindow window = new LibraryWindow();
            window.Owner = this;
            window.WindowState = this.WindowState;
        }
        private void Add_button(object sender, RoutedEventArgs e)
        {
            //CREER NOUVELLE BIBLIO
            UpdateLibraryWindow window = new UpdateLibraryWindow();
        }
        private void Edit_button(object sender, RoutedEventArgs e)
        {
            //EDIT SELECTED BIBLIO
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
                open_button.Width = 50;
                library_list.Width = this.Width;
                library_list.Height = this.Height - 100;
            }
        }
        private void Open_Panel()
        {
            if (Left >= 300)
            {
                PannelWindow pannel = new PannelWindow();
                pannel.Owner = this;
                pannel.EnableBlur();
                pannel.WindowState = this.WindowState;
                if (add_button.Width == 0)
                {
                    pannel.admin_button.Content = "Mode avancé";
                }
                else
                {
                    pannel.admin_button.Content = "Utilisateur";
                }
                pannel.Show();
                
            }
            else { Left = 300; Open_Panel(); }
        }
        public void Admin_Mode()
        {
            if (add_button.Width == 0)
            {
                //AFFICHE LES BOUTONS MODE AVANCE
                page_title.Content = "MODE AVANCÉ";
                page_content.Content = "Aucune bibliothèque";
                add_button.Width = 50;
                delete_button.Width = 50;
                open_button.Width = 150;                
            }
            else
            {
                //REPASSE EN VUE USER  !!REEXECUTER LOADCOLLECTION!!
                page_title.Content = "BIBLIOTHÈQUES";
                add_button.Width = 0;
                delete_button.Width = 0;
            }
            navigation_button.Width = 50;
            close_navigation_button.Width = 0;
            navigation_button.Focusable = true;
            close_navigation_button.Focusable = false;
        }
        
        public void Display_Fullscreen (bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowStyle = System.Windows.WindowStyle.None;
                close_navigation_button.Margin = new Thickness(300, 10, 0, 0);
            }
            else
            {
                this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                close_navigation_button.Margin = new Thickness(10, 10, 0, 0);
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
                //library_list.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                maximize_button.Opacity = 1;
                maximize_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                maximize_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/maximize_button_light.png")));
                minimize_button.Opacity = 1;
                minimize_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                minimize_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/minimize_button_light.png")));
                navigation_button.Opacity = 1;
                navigation_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                navigation_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/navigation_button_light.png")));
                close_navigation_button.Opacity = 1;
                close_navigation_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                close_navigation_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/navigation_button_light.png")));
            }
            else
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
                page_title.Foreground = new SolidColorBrush(Colors.White);
                page_content.Foreground = new SolidColorBrush(Colors.White);
                //library_list.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                maximize_button.Opacity = 0.6;
                maximize_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                maximize_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/maximize_button.png")));
                minimize_button.Opacity = 0.6;
                minimize_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                minimize_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/minimize_button.png")));
                navigation_button.Opacity = 0.6;
                navigation_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                navigation_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/navigation_button.png")));
                close_navigation_button.Opacity = 0.6;
                close_navigation_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                close_navigation_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/navigation_button.png")));
            }
        }

        private void Search_focus(object sender, RoutedEventArgs e)
        {
            if (search_box.Text == "Rechercher")
            {
                search_box.Text = "";
            }
        }

        private void Search_unfocus(object sender, RoutedEventArgs e)
        {
            if (search_box.Text == "")
            {
                search_box.Text = "Rechercher";
            }
        }
    }
}