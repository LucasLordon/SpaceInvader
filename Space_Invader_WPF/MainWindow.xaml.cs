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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace Space_Invader_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool goLeft, goRight;

        List<Rectangle> itemsToRemove = new List<Rectangle>();

        int ennemyImage = 0;
        int bulletTimer = 0;
        int bulletTimerLimit = 90;
        int totalEnemies = 0;
        int enemySpeed = 6;
        bool gameOver = false;

        DispatcherTimer gameTimer = new DispatcherTimer();
        ImageBrush playerSkin = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            playerSkin.ImageSource = new BitmapImage(new Uri("C:/Users/lucas/OneDrive/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/player.png"));
            player.Fill = playerSkin;

            myCanvas.Focus();
        }

        private void GameLoop(object sender, EventArgs e)
        {
           if(goLeft == true && Canvas.GetLeft(player) > 10)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player)-10);
            }
           if(goRight == true && Canvas.GetLeft(player) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);
            }

            bulletTimer -= 3;

            if(bulletTimer < 0)
            {
                enemyBulletMaker(Canvas.GetLeft(player)+20,10);

                bulletTimer = bulletTimerLimit;
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            if (e.Key == Key.Space)
            {
                Rectangle newBullet = new Rectangle()
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };

                Canvas.SetTop(newBullet, Canvas.GetTop(player)-newBullet.Height);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player)+ player.Width/2);

                myCanvas.Children.Add(newBullet);

            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
        }

        private void enemyBulletMaker(double x, double y)
        {
            Rectangle enemyBullet = new Rectangle()
            {
                Tag = "enemyBullet",
                Height = 40,
                Width = 15,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 5
            };
            Canvas.SetTop(enemyBullet, y);
            Canvas.SetLeft(enemyBullet, x);

            myCanvas.Children.Add(enemyBullet);
        }

        private void makeEnemies(int limit)
        {

        }

        private void showGameOver(string messageGameOver)
        {

        }
    }
}
