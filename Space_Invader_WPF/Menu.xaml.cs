using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; // Importation de classes de l'espace de noms Windows
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_Invader_WPF
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        /// <summary>
        /// Constructeur de la classe Menu, initialise la fenêtre.
        /// </summary>
        public Menu()
        {
            InitializeComponent(); // Initialise les composants de la fenêtre
            Left = 0; // Positionne la fenêtre tout à gauche de l'écran
            Top = 0; // Positionne la fenêtre tout en haut de l'écran
        }

        /// <summary>
        /// Gère le clic sur un bouton pour commencer le jeu.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (le bouton).</param>
        /// <param name="e">Les données de l'événement de clic.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayMenu objMainWindow = new PlayMenu(); // Crée une nouvelle instance de la classe PlayMenu
            this.Visibility = Visibility.Hidden; // Cache la fenêtre actuelle
            objMainWindow.Show(); // Affiche la nouvelle fenêtre du jeu
        }

        /// <summary>
        /// Gère le clic sur un bouton pour afficher les crédits.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (le bouton).</param>
        /// <param name="e">Les données de l'événement de clic.</param>
        private void Button_Credit(object sender, RoutedEventArgs e)
        {
            Credis objCredis = new Credis(); // Crée une nouvelle instance de la classe Credis (crédits)
            this.Visibility = Visibility.Hidden; // Cache la fenêtre actuelle
            objCredis.Show(); // Affiche la nouvelle fenêtre des crédits
        }
    }
}
