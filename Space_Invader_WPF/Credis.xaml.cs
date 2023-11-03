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

using System.Windows.Media.Animation; // Importation du système gérant les animations

namespace Space_Invader_WPF
{
    /// <summary>
    /// Logique d'interaction pour Credis.xaml
    /// </summary>
    public partial class Credis : Window
    {
        /// <summary>
        /// Constructeur de la classe Credis, initialise la fenêtre.
        /// </summary>
        public Credis()
        {
            InitializeComponent(); // Initialise les composants de la fenêtre
            Left = 0; // Positionne la fenêtre tout à gauche de l'écran
            Top = 0; // Positionne la fenêtre tout en haut de l'écran
            Loaded += Credis_Movement_Loaded; // Associe l'événement "Loaded" à la méthode "Credis_Movement_Loaded"
        }

        /// <summary>
        /// Gère l'événement "Loaded" de la fenêtre pour démarrer des animations.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (la fenêtre).</param>
        /// <param name="e">Les données de l'événement "Loaded".</param>
        private void Credis_Movement_Loaded(object sender, RoutedEventArgs e)
        {
            BtnMenu.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 175, 0, 0), TimeSpan.FromSeconds(8))); // Anime le bouton de menu en déplaçant sa marge
            TextCredis.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, -610, 0, 0), TimeSpan.FromSeconds(10))); // Anime le texte des crédits en déplaçant sa marge
        }

        /// <summary>
        /// Gère le clic sur un bouton pour revenir au menu principal.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (le bouton).</param>
        /// <param name="e">Les données de l'événement de clic.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu objCredis = new Menu(); // Crée une nouvelle instance de la classe Menu
            this.Visibility = Visibility.Hidden; // Cache la fenêtre actuelle
            objCredis.Show(); // Affiche la nouvelle fenêtre du menu principal
        }
    }
}
