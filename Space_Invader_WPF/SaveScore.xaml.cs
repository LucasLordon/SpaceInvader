using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient; //Dans le cas d'une erreur désinstallée le package NuGet "MySql.Data" et le réinstaller

namespace Space_Invader_WPF
{
    public partial class SaveScore : Window
    {
        public int score;

        public SaveScore()
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
        }

        private bool isQueryExecuted = false; //Variable servant à empêcher d'enregistrer plusieurs fois le même score

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (!isQueryExecuted)
            {
                // Récupère le pseudo du joueur à partir du pseudo entrer par l'utilisateur
                string player = TextBoxPseudo.Text;
                // Connexion à la base de données MySQL
                string connectionString = "server=localhost; uid=root; pwd=root; database=db_space_invaders; port=6033;";

                // Crée une nouvelle instance de la classe MySqlConnection pour établir une connexion à la base de données MySQL.
                // "connectionString" contient la chaîne de connexion avec les informations nécessaires pour se connecter à la base de données, telles que le serveur, le nom d'utilisateur, le mot de passe, la base de données, et le port.
                MySqlConnection connection = new MySqlConnection(connectionString);


                try
                {
                    // Ouvre la connexion à la base de données
                    connection.Open();

                    // Prépare une requête SQL pour insérer le pseudo et le score dans la base de données
                    string query = "INSERT INTO db_space_invaders.t_joueur (jouPseudo, jouNombrePoints) VALUES (@jouPseudo, @jouNombrePoints)";

                    // Crée une nouvelle instance de la classe MySqlCommand pour préparer une requête SQL.
                    // "query" contient la requête SQL à exécuter, et "connection" est l'objet de connexion à la base de données.
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Ajoute les paramètres avec les valeurs du joueur et du score
                    cmd.Parameters.AddWithValue("@jouPseudo", player);
                    cmd.Parameters.AddWithValue("@jouNombrePoints", score);

                    // Exécute la requête SQL
                    cmd.ExecuteNonQuery();

                    // Affiche un message de succès
                    MessageBox.Show("Score ajouté avec succès !");

                    // Marque la requête comme exécutée
                    isQueryExecuted = true;
                }
                catch (MySqlException thrownException)
                {
                    // En cas d'erreur MySQL, affiche un message d'erreur
                    MessageBox.Show("Erreur de base de données : " + thrownException.Message);
                }
                finally
                {
                    // Ferme la connexion à la base de données, si elle est ouverte
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                // Affiche un message si la requête a déjà été exécutée
                MessageBox.Show("La requête a déjà été exécutée.");
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            // Crée une instance de la classe PlayMenu et affiche le menu principal
            PlayMenu objMainWindow = new PlayMenu();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}
