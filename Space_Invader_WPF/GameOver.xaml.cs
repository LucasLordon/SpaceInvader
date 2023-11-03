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

namespace Space_Invader_WPF
{
    /// <summary>
    /// Logique d'interaction pour GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        public int score; //variable publique de type entier ayant pour valeur le score (la variable recoit la valeur du score lorsque la partie ce fini)

        /// <summary>
        /// Constructeur de la classe GameOver, initialise la fenêtre.
        /// </summary>
        public GameOver()
        {
            InitializeComponent(); // Initialise les composants de la fenêtre

            Left = 0; // Positionne la fenêtre tout à gauche de l'écran
            Top = 0; // Positionne la fenêtre tout en haut de l'écran
        }

        /// <summary>
        /// Gère le clic sur le bouton du menu. Crée une nouvelle instance de PlayMenu
        /// et affiche cette fenêtre tout en cachant la fenêtre actuelle.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (le bouton).</param>
        /// <param name="e">Les données de l'événement de clic.</param>
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            PlayMenu objMainWindow = new PlayMenu();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        /// <summary>
        /// Gère le clic sur le bouton pour sauvegarder le score. Crée une nouvelle instance de SaveScore,
        /// l'affiche tout en cachant la fenêtre actuelle, puis affecte la valeur du score à la propriété 'score' de la fenêtre SaveScore.
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement (le bouton).</param>
        /// <param name="e">Les données de l'événement de clic.</param>
        private void btnSaveScore_Click(object sender, RoutedEventArgs e)
        {
            SaveScore saveScoreWindow = new SaveScore();
            this.Visibility = Visibility.Hidden;
            saveScoreWindow.Show();
            //Passe la valeur du score effectuer
            saveScoreWindow.score = score;
        }

    }
}
