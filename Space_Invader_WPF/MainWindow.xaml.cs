using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.Windows.Themes;
using Model;

namespace Space_Invader_WPF
{
    public partial class MainWindow : Window
    {
        const int STARTAMMOSTOCK = 90;
        const int STARTALIENNUMBER = 60;
        const int ALIENWIDTH = 45;
        const int ALIENHEIGHT = 45;
        const int ALIENDEFAULTSPEED = 10;
        const int ALIENSPACEBETWEEN = 10;
        const int ALIENBULLETWIDTH = 5;
        const int ALIENBULLETHEIGHT = 20;
        const int TOPLIMIT = 40;
        const int WINDOWSWIDTH = 800;
        const int WINDOWSHEIGT = 500;

        int alienSpeed = ALIENDEFAULTSPEED;
        int aliensLeft = STARTALIENNUMBER;
        int score = 0;
        int bulletLeft = STARTAMMOSTOCK;
        int GameLoopCounter = 0;
        int AlienBulletFireDelay = 10;
        int AlienBulletFireSpeed = 5;

        bool gameOver = false; // Initialisation de la variable gameOver à false

        string messageGameOver = "";

        private Rectangle newPlayer;

        List<BulletPair> bulletPairs = new List<BulletPair>();
        List<AlienBulletPair> alienBulletPairs = new List<AlienBulletPair>();
        List<AlienPair> alienPairs = new List<AlienPair>();

        Player player = new Player(386, 406, "../net6.0-windows/Image/player.png", STARTAMMOSTOCK, 28, 48);

        DispatcherTimer gameTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            Left = 0;
            Top = 0;

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            makePlayer();
            createAlien(STARTALIENNUMBER);
            myCanvas.Focus();

        }
        private void GameLoop(object sender, EventArgs e)
        {
            createAlienBullet();
            UpdateGameLoopCounter();
            updatePlayer();
            updateAliens();
            updateBullets();
            updateAlienBullets();
            CheckIntersections(bulletPairs, alienBulletPairs, alienPairs, player);
            progressiveDifficulty();
            ScreenUpdate();
            checkGameOver();
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left) || (e.Key == Key.A))
            {
                player.GoLeft(10);
            }
            if ((e.Key == Key.Right) || (e.Key == Key.D))
            {
                player.GoRight(WINDOWSWIDTH);
            }

        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Bullet bullet = player.Shoot();
                if (bullet != null)
                {
                    makeBullet(bullet);
                    bulletLeft--;
                }
            }
        }
        private void UpdateGameLoopCounter()
        {
            if (GameLoopCounter == 10)
                GameLoopCounter = 1;
            else
                GameLoopCounter++;
        }
        private void makePlayer()
        {
            string cheminRelatif;
            string cheminAbsolu;

            ImageBrush PlayerSkin = new ImageBrush();

            cheminRelatif = player.ImageLink;
            cheminAbsolu = System.IO.Path.GetFullPath(cheminRelatif);
            PlayerSkin.ImageSource = new BitmapImage(new Uri(cheminAbsolu));

            newPlayer = new Rectangle()
            {
                Tag = "Player",
                Height = player.height,
                Width = player.width,
                Fill = PlayerSkin
            };
            Canvas.SetTop(newPlayer, player.posY);
            Canvas.SetLeft(newPlayer, player.posX);

            Canvas.SetZIndex(newPlayer, int.MaxValue);

            myCanvas.Children.Add(newPlayer);
        }
        private void updatePlayer()
        {
            drawPlayer();
        }
        private void drawPlayer()
        {
            Canvas.SetTop(newPlayer, player.posY);
            Canvas.SetLeft(newPlayer, player.posX);
        }
        private void makeBullet(Bullet bullet)
        {
            Rectangle newBullet = new Rectangle()
            {
                Tag = "bullet",
                Height = bullet.height,
                Width = bullet.width,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(bullet.fillColor)),
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(bullet.strokeColor))
            };
            Canvas.SetTop(newBullet, bullet.posY);
            Canvas.SetLeft(newBullet, bullet.posX);

            myCanvas.Children.Add(newBullet);

            BulletPair bulletPair = new BulletPair
            {
                Bullet = bullet,
                NewBullet = newBullet
            };

            bulletPairs.Add(bulletPair);
        }
        private void updateBullets()
        {
            foreach (BulletPair bulletPair in bulletPairs)
            {
                updateBullet(bulletPair.Bullet, bulletPair.NewBullet);
            }
        }
        private void updateBullet(Bullet bullet, Rectangle newBullet)
        {

            if (bullet.needToDelete == false)
            {
                bullet.MoveUp(TOPLIMIT);
                drawBullet(bullet, newBullet);
            }
            else
            {
                myCanvas.Children.Remove(newBullet);
            }
        }
        private void drawBullet(Bullet bullet, Rectangle newBullet)
        {
            Canvas.SetTop(newBullet, bullet.posY);
            Canvas.SetLeft(newBullet, bullet.posX);
        }
        private void createAlienBullet()
        {
            GameLoopCounter += AlienBulletFireDelay;
            if (GameLoopCounter >= 500)
            {
                GameLoopCounter -= 500;
                Bullet alienBullet = new Bullet(player.posX + (player.width / 2) - (ALIENBULLETWIDTH / 2), TOPLIMIT, ALIENBULLETHEIGHT, ALIENBULLETWIDTH, AlienBulletFireSpeed, "Yellow", "Black");
                makeAlienBullet(alienBullet);
            }

        }
        private void makeAlienBullet(Bullet alienBullet)
        {
            Rectangle newAlienBullet = new Rectangle()
            {
                Tag = "alienBullet",
                Height = alienBullet.height,
                Width = alienBullet.width,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(alienBullet.fillColor)),
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(alienBullet.strokeColor))
            };
            Canvas.SetTop(newAlienBullet, alienBullet.posY);
            Canvas.SetLeft(newAlienBullet, alienBullet.posX);

            myCanvas.Children.Add(newAlienBullet);

            AlienBulletPair alienBulletPair = new AlienBulletPair
            {
                AlienBullet = alienBullet,
                NewAlienBullet = newAlienBullet
            };

            alienBulletPairs.Add(alienBulletPair);
        }
        private void updateAlienBullets()
        {
            foreach (AlienBulletPair alienBulletPair in alienBulletPairs)
            {
                updateAlienBullet(alienBulletPair.AlienBullet, alienBulletPair.NewAlienBullet);
            }
        }
        private void updateAlienBullet(Bullet alienBullet, Rectangle newAlienBullet)
        {

            if (alienBullet.needToDelete == false)
            {
                alienBullet.MoveDown(WINDOWSHEIGT);
                drawAlienBullet(alienBullet, newAlienBullet);
            }
            else
            {
                myCanvas.Children.Remove(newAlienBullet);
            }
        }
        private void drawAlienBullet(Bullet alienBullet, Rectangle newAlienBullet)
        {
            Canvas.SetTop(newAlienBullet, alienBullet.posY);
            Canvas.SetLeft(newAlienBullet, alienBullet.posX);
        }
        private void createAlien(int ALIENNUMBER)
        {
            int AlienType = 1;
            int Space = 0;
            string imageLink = "";
            for (int i = 0; i < ALIENNUMBER; i++)
            {
                switch (AlienType)
                {
                    case 1:
                        imageLink = "../net6.0-windows/Image/invader1.gif";
                        break;
                    case 2:
                        imageLink = "../net6.0-windows/Image/invader2.gif";
                        break;
                    case 3:
                        imageLink = "../net6.0-windows/Image/invader3.gif";
                        break;
                    case 4:
                        imageLink = "../net6.0-windows/Image/invader4.gif";
                        break;
                    case 5:
                        imageLink = "../net6.0-windows/Image/invader5.gif";
                        break;
                    case 6:
                        imageLink = "../net6.0-windows/Image/invader6.gif";
                        break;
                    case 7:
                        imageLink = "../net6.0-windows/Image/invader7.gif";
                        break;
                    case 8:
                        imageLink = "../net6.0-windows/Image/invader8.gif";
                        break;
                }
                Alien alien = new Alien(-ALIENWIDTH - Space - ALIENSPACEBETWEEN, TOPLIMIT, imageLink, ALIENWIDTH, ALIENHEIGHT, alienSpeed);
                makeAlien(alien);
                Space += ALIENWIDTH + ALIENSPACEBETWEEN;
                if (AlienType == 8)
                    AlienType = 1;
                else
                    AlienType++;
            }
        }
        private void makeAlien(Alien alien)
        {
            string cheminRelatif;
            string cheminAbsolu;

            ImageBrush AlienSkin = new ImageBrush();

            cheminRelatif = alien.ImageLink;
            cheminAbsolu = System.IO.Path.GetFullPath(cheminRelatif);
            AlienSkin.ImageSource = new BitmapImage(new Uri(cheminAbsolu));

            Rectangle newAlien = new Rectangle()
            {
                Tag = "Alien",
                Height = alien.height,
                Width = alien.width,
                Fill = AlienSkin
            };
            Canvas.SetTop(newAlien, alien.posY);
            Canvas.SetLeft(newAlien, alien.posX);

            Canvas.SetZIndex(newAlien, int.MaxValue - 1);

            myCanvas.Children.Add(newAlien);

            AlienPair alienPair = new AlienPair
            {
                Alien = alien,
                NewAlien = newAlien
            };
            alienPairs.Add(alienPair);
        }
        private void updateAliens()
        {
            foreach (AlienPair alienPair in alienPairs)
            {
                updateAlien(alienPair.Alien, alienPair.NewAlien);
            }
        }
        private void updateAlien(Alien alien, Rectangle newAlien)
        {
            alien.move(WINDOWSWIDTH);
            drawAlien(alien, newAlien);
        }
        private void drawAlien(Alien alien, Rectangle newAlien)
        {
            Canvas.SetTop(newAlien, alien.posY);
            Canvas.SetLeft(newAlien, alien.posX);
        }
        private void checkGameOver()
        {
            if (gameOver == true)
            {
                showGameOver(messageGameOver);
            }
        }
        public void showGameOver(string messageGameOver)
        {
            this.Visibility = Visibility.Hidden;
            float ratio;
            ratio = (float)Math.Round(((float)(STARTALIENNUMBER - aliensLeft) / (float)((STARTAMMOSTOCK + 1) - bulletLeft)), 2);
            gameOver = true;
            gameTimer.Stop();
            GameOver gameOverWindow = new GameOver(); // Créez une instance de la fenêtre GameOver
            gameOverWindow.score = score;
            gameOverWindow.gameOverScore.Content = "Voici votre score : " + score + "\nEnnemy restant : " + aliensLeft + "\nMunition restantes : " + bulletLeft + "\nRation (tir/mort) : " + ratio; gameOverWindow.ShowDialog(); // Affichez la fenêtre de manière modale
            
        }
        public static bool IsAnIntersection(int X1, int Y1, int W1, int H1, int X2, int Y2, int W2, int H2)
        {
            return !(X1 + W1 < X2 || X2 + W2 < X1 || Y1 + H1 < Y2 || Y2 + H2 < Y1);
        }
        public bool CheckIntersections(List<BulletPair> bullets, List<AlienBulletPair> alienBullets, List<AlienPair> aliens, Player player)
        {


            // Créer des copies des listes
            List<BulletPair> bulletsCopy = new List<BulletPair>(bullets);
            List<AlienBulletPair> alienBulletsCopy = new List<AlienBulletPair>(alienBullets);
            List<AlienPair> aliensCopy = new List<AlienPair>(aliens);

            foreach (BulletPair bulletPair in bulletsCopy)
            {
                foreach (AlienBulletPair alienBulletPair in alienBulletsCopy)
                {
                    if (IsAnIntersection(bulletPair.Bullet.posX, bulletPair.Bullet.posY, bulletPair.Bullet.width, bulletPair.Bullet.height,
                       alienBulletPair.AlienBullet.posX, alienBulletPair.AlienBullet.posY, alienBulletPair.AlienBullet.width, alienBulletPair.AlienBullet.height))
                    {
                        // Intersection entre Bullet et AlienBullet
                        bullets.Remove(bulletPair);
                        myCanvas.Children.Remove(bulletPair.NewBullet);
                        alienBullets.Remove(alienBulletPair);
                        myCanvas.Children.Remove(alienBulletPair.NewAlienBullet);
                        score += 10;

                    }
                }

                foreach (AlienPair alienPair in aliensCopy)
                {
                    if (IsAnIntersection(bulletPair.Bullet.posX, bulletPair.Bullet.posY, bulletPair.Bullet.width, bulletPair.Bullet.height,
                        alienPair.Alien.posX, alienPair.Alien.posY, alienPair.Alien.width, alienPair.Alien.height))
                    {
                        // Intersection entre Bullet et Alien
                        bullets.Remove(bulletPair);
                        myCanvas.Children.Remove(bulletPair.NewBullet);
                        aliens.Remove(alienPair);
                        myCanvas.Children.Remove(alienPair.NewAlien);
                        aliensLeft--;
                        if (bulletLeft - aliensLeft > 0)
                        {
                            score = 1000+score + (bulletLeft - aliensLeft) + STARTAMMOSTOCK / (aliensLeft + 3);
                        }
                        else
                            score += 10;
                    }
                }
            }
            foreach (AlienBulletPair alienBulletPair in alienBulletsCopy)
            {
                if (IsAnIntersection(player.posX, player.posY, player.width, player.height,
                    alienBulletPair.AlienBullet.posX, alienBulletPair.AlienBullet.posY, alienBulletPair.AlienBullet.width, alienBulletPair.AlienBullet.height))
                {
                    // Intersection entre AlienBullet et Player
                    messageGameOver = "Tu est mort par un tir alien.";
                    gameOver = true;
                    alienBullets.Remove(alienBulletPair);
                    myCanvas.Children.Remove(alienBulletPair.NewAlienBullet);
                }
            }
            foreach (AlienPair alienPair in aliensCopy)
            {
                if (IsAnIntersection(player.posX, player.posY, player.width, player.height,
                    alienPair.Alien.posX, alienPair.Alien.posY, alienPair.Alien.width, alienPair.Alien.height))
                {
                    messageGameOver = "Tu est mort au contact d'un alien.";
                    gameOver = true;
                    aliens.Remove(alienPair);
                    myCanvas.Children.Remove(alienPair.NewAlien);
                }
            }
            return gameOver;
        }
        private void ScreenUpdate()
        {
            alienLeft.Content = "Alien Left : " + aliensLeft;
            bulletsLeft.Content = "Bullets Left : " + bulletLeft;
            scoreSpreader.Content = "Score : " + score;
        }
        private void progressiveDifficulty()
        {
            if (aliensLeft <= 50)
            {
                AlienBulletFireDelay = 15;
            }
            if (aliensLeft <= 45)
            {
                AlienBulletFireSpeed = 7;
            }
            if (aliensLeft <= 40)
            {
                alienSpeed = 15;
                AlienBulletFireDelay = 25;
            }
            if (aliensLeft <= 35)
            {
                AlienBulletFireSpeed = 9;
            }
            if (aliensLeft <= 30)
            {
                AlienBulletFireDelay = 35;
            }
            if (aliensLeft <= 25)
            {
                AlienBulletFireSpeed = 11;
            }
            if (aliensLeft <= 20)
            {
                alienSpeed = 20;
                AlienBulletFireDelay = 45;
            }
            if (aliensLeft <= 15)
            {
                AlienBulletFireSpeed = 13;
            }
            if (aliensLeft <= 10)
            {
                AlienBulletFireDelay = 55;
            }
            if (aliensLeft <= 5)
            {
                alienSpeed = 25;
                AlienBulletFireDelay = 65;
                AlienBulletFireSpeed = 15;
            }
            if (aliensLeft == 1)
            {
                AlienBulletFireDelay = 80;
                AlienBulletFireSpeed = 20;
            }
            increasAlienSpeed(alienSpeed, alienPairs);

        }
        private void increasAlienSpeed(int speed, List<AlienPair> aliens)
        {
            List<AlienPair> aliensCopy = new List<AlienPair>(aliens);

            foreach (AlienPair alienPair in aliensCopy)
            {
                alienPair.Alien.speed = speed;

            }
        }
        public class BulletPair
        {
            public Bullet Bullet { get; set; }
            public Rectangle NewBullet { get; set; }
        }
        public class AlienBulletPair
        {
            public Bullet AlienBullet { get; set; }
            public Rectangle NewAlienBullet { get; set; }
        }
        public class AlienPair
        {
            public Alien Alien { get; set; }
            public Rectangle NewAlien { get; set; }
        }
    }
}