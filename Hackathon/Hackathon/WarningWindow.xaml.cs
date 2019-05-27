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
    /// Logique d'interaction pour WarningWindow.xaml
    /// </summary>
    public partial class WarningWindow : Window
    {
        MainWindow mainWindow;
        public WarningWindow()
        {
            InitializeComponent();
            page_content.Text = "Êtes-vous sûr de vouloir supprimer cette bibliothèque ? Cette action est irréversible.";
        }

        private void No_btn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Yes_btn(object sender, RoutedEventArgs e)
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            mainWindow.Delete_action();
            Close();
        }
    }
}
