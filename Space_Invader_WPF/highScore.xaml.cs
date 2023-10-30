using MySql.Data.MySqlClient;
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
            List<ScoreData> topPlayers = GetTop10Players();
            UpdateLabels(topPlayers);
        }
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            PlayMenu objMainWindow = new PlayMenu();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
        public class ScoreData
        {
            public int Place { get; set; }
            public string Pseudo { get; set; }
            public int Score { get; set; }
        }

        public List<ScoreData> GetTop10Players()
        {
            List<ScoreData> topPlayers = new List<ScoreData>();

            string connectionstring = "server=localhost; uid=root; pwd=4$a6mJ#ieQ95&MK9LF$R; database=db_space_invaders;";
            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {
                con.Open();
                string sql = "SELECT jouPseudo, jouNombrePoints FROM db_space_invaders.t_joueur ORDER BY jouNombrePoints DESC LIMIT 10";
                MySqlCommand cmd = new MySqlCommand(sql, con);

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
