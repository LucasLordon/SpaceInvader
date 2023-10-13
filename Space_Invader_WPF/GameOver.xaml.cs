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
        private void btnMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            // Changez l'image du bouton lorsque la souris passe dessus
            imageBtnMenu.Source = new BitmapImage(new Uri("/MenuButtonWhite.png", UriKind.Relative));
        }

        private void btnMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            // Revenez à l'image d'origine lorsque la souris s'en va
            imageBtnMenu.Source = new BitmapImage(new Uri("/MenuButton.png", UriKind.Relative));
        }

    }
}
