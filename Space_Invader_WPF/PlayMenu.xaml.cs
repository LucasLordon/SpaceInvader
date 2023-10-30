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
    /// Logique d'interaction pour PlayMenu.xaml
    /// </summary>
    public partial class PlayMenu : Window
    {
        public PlayMenu()
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
        }

        private void btnSolo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu objCredis = new Menu();
            this.Visibility = Visibility.Hidden;
            objCredis.Show();
        }
        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            highScore objCredis = new highScore();
            this.Visibility = Visibility.Hidden;
            objCredis.Show();
        }
    }
}
