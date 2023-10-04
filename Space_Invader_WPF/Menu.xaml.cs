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
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        private void Button_Credit(object sender, RoutedEventArgs e)
        {
            Credis objCredis = new Credis();
            this.Visibility = Visibility.Hidden;
            objCredis.Show();
        }
    }
}