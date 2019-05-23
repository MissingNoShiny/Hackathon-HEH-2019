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
        public LibraryWindow()
        {
            InitializeComponent();
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
        }

        private void Delete_button(object sender, RoutedEventArgs e)
        {
            //DELETE SELECTED BIBLIO
        }
    }
}
