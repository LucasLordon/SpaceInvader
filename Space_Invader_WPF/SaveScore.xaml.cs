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
    public partial class SaveScore : Window
    {
        public int score;
        public SaveScore()
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
        }
        private bool isQueryExecuted = false;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!isQueryExecuted)
            {
                string player = TextBoxPseudo.Text;
                string connstring = "server=localhost; uid=root; pwd=4$a6mJ#ieQ95&MK9LF$R; database=db_space_invaders;";
                MySqlConnection con = new MySqlConnection(connstring);

                try
                {
                    con.Open();

                    string sql = "INSERT INTO db_space_invaders.t_joueur (jouPseudo, jouNombrePoints) VALUES (@jouPseudo, @jouNombrePoints)";
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@jouPseudo", player);
                    cmd.Parameters.AddWithValue("@jouNombrePoints", score);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Score ajouté avec succès !");

                    isQueryExecuted = true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Erreur de base de données : " + ex.Message);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
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
