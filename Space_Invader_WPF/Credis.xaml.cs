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

using System.Windows.Media.Animation;

namespace Space_Invader_WPF
{
    public partial class Credis : Window
    {
        public Credis()
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
            Loaded += Credis_Movement_Loaded;
        }

        private void Credis_Movement_Loaded(object sender, RoutedEventArgs e)
        {
            BtnMenu.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 175, 0, 0), TimeSpan.FromSeconds(8)));
            TextCredis.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, -610, 0, 0), TimeSpan.FromSeconds(10)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu objCredis = new Menu();
            this.Visibility = Visibility.Hidden;
            objCredis.Show();
        }


    }
}
