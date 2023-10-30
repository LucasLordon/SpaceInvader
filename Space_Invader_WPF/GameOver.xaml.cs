﻿using System;
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
        public int score;
        public GameOver()
        {
            gameOverScore = this.gameOverScore;
            InitializeComponent();
            Left = 0;
            Top = 0;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            PlayMenu objMainWindow = new PlayMenu();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        private void btnSaveScore_Click(object sender, RoutedEventArgs e)
        {
            SaveScore objMainWindow = new SaveScore();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
            objMainWindow.score = score;
        }
    }
}
