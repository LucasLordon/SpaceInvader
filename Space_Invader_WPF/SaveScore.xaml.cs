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
using MySql.Data.MySqlClient;

namespace Space_Invader_WPF
{
    /// <summary>
    /// Logique d'interaction pour SaveScore.xaml
    /// </summary>
    public partial class SaveScore : Window
    {
        public int score;
        public SaveScore()
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
        }
        private bool isQueryExecuted = false; // Variable de contrôle pour suivre si la requête a déjà été effectuée.

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!isQueryExecuted) // Vérifiez si la requête n'a pas encore été exécutée.
            {
                string player = TextBoxPseudo.Text;
                string connstring = "server=localhost; uid=root; pwd=4$a6mJ#ieQ95&MK9LF$R; database=db_space_invaders;";
                MySqlConnection con = new MySqlConnection(connstring);

                try
                {
                    con.Open();

                    // Créez la commande SQL d'insertion pour ajouter un nouveau score au joueur.
                    string sql = "INSERT INTO db_space_invaders.t_joueur (jouPseudo, jouNombrePoints) VALUES (@jouPseudo, @jouNombrePoints)";
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    // Ajoutez les paramètres pour le nom du joueur et le score.
                    cmd.Parameters.AddWithValue("@jouPseudo", player);
                    cmd.Parameters.AddWithValue("@jouNombrePoints", score);

                    // Exécutez la commande d'insertion.
                    cmd.ExecuteNonQuery();

                    // Affichez un message de succès en utilisant MessageBox.Show.
                    MessageBox.Show("Score ajouté avec succès !");

                    // Définissez la variable de contrôle pour indiquer que la requête a été exécutée.
                    isQueryExecuted = true;
                }
                catch (MySqlException ex)
                {
                    // Gérez les erreurs de connexion à la base de données en affichant une boîte de dialogue.
                    MessageBox.Show("Erreur de base de données : " + ex.Message);
                }
                finally
                {
                    // Assurez-vous de fermer la connexion à la base de données.
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                // Affichez un message pour indiquer que la requête a déjà été exécutée.
                MessageBox.Show("La requête a déjà été exécutée.");
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            PlayMenu objMainWindow = new PlayMenu();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

    }
}
