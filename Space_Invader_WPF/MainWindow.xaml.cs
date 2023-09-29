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
using System.Windows.Interop;
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
        const int MAXBULLET = 90;
        int ennemyImage = 0;
        int bulletTimer = 0;
        int bulletTimerLimit = 90;
        int totalEnemies = 0;
        int totalBullets = MAXBULLET;
        int enemySpeed = 6;
        int score=0;
        bool gameOver = false;
        int bulletFireAutorised = 5;

        DispatcherTimer gameTimer = new DispatcherTimer();
        ImageBrush playerSkin = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            playerSkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/player.png"));
            player.Fill = playerSkin;

            myCanvas.Focus();
            makeEnemies(60);
        }
        private void GameLoop(object sender, EventArgs e)
        {
            bulletFireAutorised++;
            Rect playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            enemiesLeft.Content = "Enemies Left : " + totalEnemies;
            bulletsLeft.Content = "Bullets Left : " + totalBullets;
            scoreSpreader.Content = "Score : " + score;
            if (goLeft == true && Canvas.GetLeft(player) > 10)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
            }
            if (goRight == true && Canvas.GetLeft(player) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);
            }

            bulletTimer -= 3;
            if (totalEnemies <= 15)
            {
                bulletTimer -= 10;
            }
             
            else if ( totalEnemies <= 30 )
            {
                bulletTimer -= 6  ;
            }
            if (bulletTimer < 0)
            {
                enemyBulletMaker(Canvas.GetLeft(player) + 20, 10);

                bulletTimer = bulletTimerLimit;
            }
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (Canvas.GetTop(x) < 10)
                    {
                        itemsToRemove.Add(x);
                    }
                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (bullet.IntersectsWith(enemyHit))
                            {
                                itemsToRemove.Add(x);
                                itemsToRemove.Add(y);
                                totalEnemies -= 1;
                                if (totalBullets - totalEnemies > 0)
                                {
                                    score = score + (totalBullets - totalEnemies) + MAXBULLET / totalEnemies;
                                }
                                else
                                    score += 10;
                            }
                        }
                    }
                }
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);

                    if (Canvas.GetLeft(x) > 820)
                    {
                        Canvas.SetLeft(x, -80);
                        Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height + 10));
                    }
                    Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemy))
                    {
                        showGameOver("You were Killed by the invaders!!");
                    }
                }
                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                    if (Canvas.GetTop(x) > 480)
                    {
                        itemsToRemove.Add(x);
                    }
                    Rect enemyBullets = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (enemyBullets.IntersectsWith(playerHitBox))
                    {
                        showGameOver("You were Killed by the invaders Bullet !!");
                    }
                }
            }
            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }
            if (totalEnemies == 1)
            {
                enemySpeed = 30;
            }
            else if(totalEnemies <= 10)
            {
                enemySpeed = 20;
            }
            else if (totalEnemies <= 40)
            {
                enemySpeed = 12;
            }

            if (totalEnemies == 0)
            {
                showGameOver("You win, saved the world");
            }
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left) || (e.Key == Key.A))
            {
                goLeft = true;
            }
            if ((e.Key == Key.Right) || (e.Key == Key.D))
            {
                goRight = true;
            }
            
        }  
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left)|| (e.Key == Key.A))
            {
                goLeft = false;
            }
            if ((e.Key == Key.Right)|| (e.Key == Key.D))
            {
                goRight = false;
            }
            if (e.Key == Key.Space)
            {
                if(totalBullets>0 && bulletFireAutorised>=5)
                {
                    Rectangle newBullet = new Rectangle()
                    {
                        Tag = "bullet",
                        Height = 20,
                        Width = 5,
                        Fill = Brushes.White,
                        Stroke = Brushes.Red
                    };
                    Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);

                    myCanvas.Children.Add(newBullet);
                    totalBullets --;
                    bulletFireAutorised=0;
                }

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
            totalEnemies = limit;
            int left = 0;

            for (int i = 0; i < limit; i++)
            {
                ImageBrush EnemySkin = new ImageBrush();

                Rectangle newEnemy = new Rectangle()
                {
                    Tag = "enemy",
                    Height = 45,
                    Width = 45,
                    Fill = EnemySkin,
                };
                Canvas.SetTop(newEnemy, 30);
                Canvas.SetLeft(newEnemy, left);
                myCanvas.Children.Add(newEnemy);
                left -= 60;

                ennemyImage++;

                if (ennemyImage > 8)
                {
                    ennemyImage = 1;
                }

                switch (ennemyImage)
                {
                    case 1:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader1.gif"));
                        break;
                    case 2:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader2.gif"));
                        break;
                    case 3:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader3.gif"));
                        break;
                    case 4:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader4.gif"));
                        break;
                    case 5:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader5.gif"));
                        break;
                    case 6:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader6.gif"));
                        break;
                    case 7:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader7.gif"));
                        break;
                    case 8:
                        EnemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pu61qgw/Documents/GitHub/SpaceInvader/Space_Invader_WPF/Image/invader8.gif"));
                        break;

                }

            }
        }
        private void showGameOver(string messageGameOver)
        {
            bulletsLeft.Content ="";
            enemiesLeft.Content = "";
            scoreSpreader.Content = "";
            gameOver = true;
            gameTimer.Stop();
            enemiesLeft.Content += "   " + messageGameOver +" Voici votre score : "+ score+ "! press Enter to play again !";
        }
    }
}