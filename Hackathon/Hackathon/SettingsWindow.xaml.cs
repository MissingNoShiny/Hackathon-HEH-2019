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

namespace Hackathon
{
    /// <summary>
    /// Logique d'interaction pour SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.Theme();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Window_Position;
            timer.Interval = TimeSpan.FromSeconds(0.0001);
            timer.Start();
        }

        /*BUTTONS CLICK*/
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Theme_Button(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Theme == "Dark")
            {
                Properties.Settings.Default.Theme = "Light";
            }
            else
            {
                Properties.Settings.Default.Theme = "Dark";
            }
            Properties.Settings.Default.Save();
            this.Theme();
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            mainWindow.Theme();
            var pannelwindow = Application.Current.Windows.OfType<PanelWindow>().FirstOrDefault();
            pannelwindow.Theme();
        }

        /*METHODES*/
        public void Content_Display(int content)
        {
            if (content == 0)
            {
                page_title.Content = "PARAMÈTRES";
                about_list.Width = 0;
                settings_list.Width = 300;
                theme_button.Focusable = true;
            }
            else
            {
                page_title.Content = "À PROPOS";
                about_list.Width = 300;
                settings_list.Width = 0;
                theme_button.Focusable = false;
            }
        }
        private void Window_Position(object sender, EventArgs e)
        {
            this.Top = Owner.Top + 30;
            this.Left = Owner.Left;
            this.Height = Owner.Height - 30;
            this.WindowState = Owner.WindowState;
        }

        /*APPLYING THEME*/
        private void Theme()
        {
            if (Properties.Settings.Default.Theme == "Light")
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(217, 217, 217));
                page_title.Foreground = new SolidColorBrush(Colors.Black);
                dev.Foreground = new SolidColorBrush(Colors.Black);
                dev_list.Foreground = new SolidColorBrush(Colors.Black);
                ui.Foreground = new SolidColorBrush(Colors.Black);
                ui_list.Foreground = new SolidColorBrush(Colors.Black);
                theme_set.Foreground = new SolidColorBrush(Colors.Black);
                theme_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                theme_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/theme_button_light.png")));
                back_button.BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                back_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/back_button_light.png")));
            }
            else
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(44, 44, 44));
                page_title.Foreground = new SolidColorBrush(Colors.White);
                theme_set.Foreground = new SolidColorBrush(Colors.White);
                theme_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                theme_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/theme_button.png")));
                back_button.BorderBrush = new SolidColorBrush(Color.FromArgb(77, 77, 77, 77));
                back_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/back_button.png")));
            }
        }       
    }
}
