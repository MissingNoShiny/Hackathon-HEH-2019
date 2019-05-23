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
        private int panelIndex = 0;

        public UpdateLibraryWindow()
        {
            InitializeComponent();
            Theme();
            this.Show();
            
        }

        private void add_object_click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Name = ("sp" + panelIndex);
            sp.Margin = new Thickness(0,5,0,0);

            TextBox tb = new TextBox();
            tb.Name = ("name" + panelIndex);
            tb.Width = 120;
            tb.Height = 23;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            

            ComboBox cb = new ComboBox();
            cb.Name = ("choicedata" + panelIndex);
            cb.SelectedIndex = 0;
            cb.Items.Add("String");
            cb.Items.Add("Int");
            cb.Items.Add("Bool");
            cb.Items.Add("Date");
            cb.Width = 82;
            cb.HorizontalAlignment = HorizontalAlignment.Center;
            cb.Margin = new Thickness(10, 0, 10, 0);
            
           

            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/delete_list_button.png"));
            myBrush.ImageSource = image.Source;

            Button btn = new Button();
            btn.Name = ("del_object" + panelIndex);
            btn.Width = 20;
            btn.Height = 23;
            btn.Background = myBrush;
            btn.HorizontalAlignment = HorizontalAlignment.Right;
            btn.Click += delegate {
                panelIndex--;
                stackpanel.Children.Remove(sp);
            };

            panelIndex++;
            sp.Children.Add(tb);
            sp.Children.Add(cb);
            sp.Children.Add(btn);
            this.stackpanel.Children.Add(sp);

            /*
            TextBox tb = new TextBox();
            tb.Name = ("name" + a++);
            tb.Width = 120;
            tb.Height = 23;
           
            
            this.stackpanel.Children.Add(tb);

            ComboBox cb = new ComboBox();
            cb.Name = ("choicedata" + b++);
            cb.SelectedIndex = 0;
            cb.Items.Add("String");
            cb.Items.Add("Int");
            cb.Items.Add("Bool");
            cb.Items.Add("Date");
            this.stackpanel1.Children.Add(cb);

            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/delete_list_button.png"));
            myBrush.ImageSource = image.Source;

            Button btn = new Button();
            btn.Name = ("del_object" + c++);
            btn.Width = 20;
            btn.Height = 23;
            btn.Background = myBrush;
            
            
            
            this.stackpanel2.Children.Add(btn);
            */
        }

        private void del_object_click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Save_library_Click(object sender, RoutedEventArgs e)
        {

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
