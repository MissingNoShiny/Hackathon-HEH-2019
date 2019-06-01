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

namespace Gooboi
{
    /// <summary>
    /// Logique d'interaction pour WarningWindow.xaml
    /// </summary>
    
    public partial class WarningWindow : Window
    {       
        public WarningWindow()
        {
            InitializeComponent();            
        }

        public void Message_show(int message)
        {
            switch (message)
            {
                case 1:
                    page_content.Text = "Êtes-vous sûr de vouloir supprimer cet élément ? Cette action est irréversible.";
                    break;
                case 2:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer la collection. La collection doit avoir un nom.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 3:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible de créer la bibliothèque. Le nom ne peut pas dépasser 24 caractères.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 4:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer la collection. Le nom ne peut contenir que des lettres ou des chiffres.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 5:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer la collection. Une autre du même nom existe déjà, veuillez en choisir un différent.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 6:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer la collection. La collection doit avoir au moins un attribut.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 7:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer la collection. Un attribut n'a pas de nom.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 8:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer la collection. Tous les attributs doivent avoir un nom différent.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 9:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer la collection. Les noms d'attributs ne doivent contenir que des lettres ou des chiffres.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 10:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer l'élément. L'un des champs n'est pas rempli.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 11:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer l'élément. Le contenu ne peuvent pas dépasser 24 caractères.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                case 12:
                    page_title.Content = "Erreur";
                    page_content.Text = "Impossible d'enregistrer l'élément. L'une des valeurs n'est pas au bon format.";
                    yes.Width = 0;
                    yes.Focusable = false;
                    no.Content = "Ok";
                    break;
                default:
                    page_content.Text = "ERREUR";
                    break;
            }
        }

        private void No_btn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Yes_btn(object sender, RoutedEventArgs e)
        {
            if (object_content.Text == "")
            {
                Yes_item();
            }
            else
            {
                Yes_libr();
            }
            Close();
        }
        private void Yes_libr()
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            mainWindow.Delete_action();
        }
        private void Yes_item()
        {
            var librarywindow = Application.Current.Windows.OfType<LibraryWindow>().FirstOrDefault();
            librarywindow.Delete_action();
        }
    }
}
