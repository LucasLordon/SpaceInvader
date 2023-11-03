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
    /// Logique d'interaction pour PlayMenu.xaml
    /// </summary>
    public partial class PlayMenu : Window
    {
        /// <summary>
        /// Constructeur de la classe PlayMenu, initialise la fenêtre.
        /// </summary>
        public PlayMenu()
        {
            InitializeComponent(); // Initialise les composants de la fenêtre
            Left = 0; // Positionne la fenêtre tout à gauche de l'écran
            Top = 0; // Positionne la fenêtre tout en haut de l'écran
        }

        /// <summary>
        /// Gère le clic sur un bouton pour commencer une partie en solo.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (le bouton).</param>
        /// <param name="e">Les données de l'événement de clic.</param>
        private void btnSolo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow(); // Crée une nouvelle instance de la classe MainWindow (jeu en solo)
            this.Visibility = Visibility.Hidden; // Cache la fenêtre actuelle
            objMainWindow.Show(); // Affiche la fenêtre du jeu en solo
        }

        /// <summary>
        /// Gère le clic sur un bouton pour revenir au menu principal.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (le bouton).</param>
        /// <param name="e">Les données de l'événement de clic.</param>
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu objCredis = new Menu(); // Crée une nouvelle instance de la classe Menu (menu principal)
            this.Visibility = Visibility.Hidden; // Cache la fenêtre actuelle
            objCredis.Show(); // Affiche la fenêtre du menu principal
        }

        /// <summary>
        /// Gère le clic sur un bouton pour afficher les scores élevés.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (le bouton).</param>
        /// <param name="e">Les données de l'événement de clic.</param>
        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            highScore objCredis = new highScore(); // Crée une nouvelle instance de la classe highScore (scores élevés)
            this.Visibility = Visibility.Hidden; // Cache la fenêtre actuelle
            objCredis.Show(); // Affiche la fenêtre des scores élevés
        }
    }
}
