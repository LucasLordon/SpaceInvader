using MySql.Data.MySqlClient;// Dans le cas d'une erreur désinstallée le package NuGet "MySql.Data" et le réinstaller
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
    /// Logique d'interaction pour highScore.xaml
    /// </summary>
    public partial class highScore : Window
    {
        public highScore()
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
            // Récupère les 10 meilleurs scores depuis la base de données lors de l'initialisation de la fenêtre.
            List<ScoreData> topPlayers = GetTop10Players();
            // Met à jour les étiquettes de la fenêtre avec les données des 10 meilleurs scores.
            UpdateLabels(topPlayers);
        }

        // Méthode appelée lorsqu'un utilisateur clique sur le bouton "Menu".
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            // Crée une instance de la classe PlayMenu et affiche le menu principal, masquant cette fenêtre.
            PlayMenu objMainWindow = new PlayMenu();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        // Classe pour stocker les données des scores.
        public class ScoreData
        {
            public int Place { get; set; }
            public string Pseudo { get; set; }
            public int Score { get; set; }
        }

        // Récupère les 10 meilleurs scores à partir de la base de données.
        public List<ScoreData> GetTop10Players()
        {
            List<ScoreData> topPlayers = new List<ScoreData>();

            // Connexion à la base de données MySQL
            string connectionstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders; port=6033;";

            // Utilisation de "using" pour s'assurer que la connexion à la base de données est correctement gérée et fermée.
            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open(); // Ouvre la connexion à la base de données.
                string query = "SELECT jouPseudo, jouNombrePoints FROM db_space_invaders.t_joueur ORDER BY jouNombrePoints DESC LIMIT 10";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Utilisation de "using" pour s'assurer que le lecteur est correctement géré et fermé.
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    int place = 1;
                    while (reader.Read())
                    {
                        string pseudo = reader["jouPseudo"].ToString();
                        int score = Convert.ToInt32(reader["jouNombrePoints"]);
                        topPlayers.Add(new ScoreData { Place = place, Pseudo = pseudo, Score = score });
                        place++;
                    }
                }
            }

            return topPlayers;
        }

        // Met à jour les étiquettes de la fenêtre avec les données des 10 meilleurs scores.
        private void UpdateLabels(List<ScoreData> topPlayers)
        {
            if (topPlayers.Count >= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    Label rankLabel = (Label)this.FindName("Rank" + (i + 1));
                    Label nameLabel = (Label)this.FindName("Name" + (i + 1));
                    Label scoreLabel = (Label)this.FindName("Score" + (i + 1));

                    rankLabel.Content = topPlayers[i].Place.ToString();
                    nameLabel.Content = topPlayers[i].Pseudo;
                    scoreLabel.Content = topPlayers[i].Score.ToString();
                }
            }
        }

    }
}
