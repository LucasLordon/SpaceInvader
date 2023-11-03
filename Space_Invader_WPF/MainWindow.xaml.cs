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
    /// <summary>
    /// Code du jeu
    /// </summary>
    public partial class MainWindow : Window
    {
        const int STARTAMMOSTOCK = 80; //nombre de munition du player
        const int STARTALIENNUMBER = 60; //nombre d'alien
        const int ALIENWIDTH = 45;  //largeur des alien
        const int ALIENHEIGHT = 45; //hauteur des alien
        const int ALIENDEFAULTSPEED = 10; //vitesse des alien
        const int ALIENSPACEBETWEEN = 10; //espace entre chaque alien
        const int ALIENBULLETWIDTH = 5; //largeur des tir d'alien
        const int ALIENBULLETHEIGHT = 20; //hauteur des tir d'alien
        const int TOPLIMIT = 40; //limit supérieur de l'espace de jeu
        const int WINDOWSWIDTH = 800; //largeur de la fenêtre
        const int WINDOWSHEIGT = 500; //Hauteur de la fenêtre

        int alienSpeed = ALIENDEFAULTSPEED; 
        int aliensLeft = STARTALIENNUMBER;
        int score = 0;
        int bulletLeft = STARTAMMOSTOCK;
        int GameLoopCounter = 0;
        int AlienBulletFireDelay = 10;
        int AlienBulletFireSpeed = 5;

        bool gameOver = false;

        private Rectangle newPlayer; //playerGraphique

        List<BulletPair> bulletPairs = new List<BulletPair>();
        List<AlienBulletPair> alienBulletPairs = new List<AlienBulletPair>();
        List<AlienPair> alienPairs = new List<AlienPair>();

        Player player = new Player(386, 406, "../net6.0-windows/Image/player.png", STARTAMMOSTOCK, 28, 48); //Player logique

        DispatcherTimer gameTimer = new DispatcherTimer();

        /// <summary>
        ///Constructeur de la classe MainWindow, initialise la fenêtre.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent(); // Initialise les composants de la fenêtre
            Left = 0; // Positionne la fenêtre tout à gauche de l'écran
            Top = 0; // Positionne la fenêtre tout en haut de l'écran

            // Associe la méthode "GameLoop" à l'événement Tick du timer "gameTimer".
            gameTimer.Tick += GameLoop;

            // Définit l'intervalle du timer "gameTimer" à 20 millisecondes.
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            // Démarre le timer "gameTimer" pour déclencher l'événement Tick à intervalles réguliers.
            gameTimer.Start();

            //Crée le Player graphique
            makePlayer();

            //Crée les alien logique
            createAlien(STARTALIENNUMBER);

            // Définit le focus (la mise au premier plan) sur l'élément "myCanvas".
            myCanvas.Focus();

        }

        /// <summary>
        /// Méthode qui ce répète jusqu'à ce que la variable "gameOver" soit true, tout le jeu fonctionne grace à cette à cette méthode
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement</param>
        /// <param name="e">Les données de l'événement</param>
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

        /// <summary>
        /// Vérifie si une touche est appuiée
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement</param>
        /// <param name="e">Les données de l'événement</param>
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

        /// <summary>
        /// Verifie si une touche vien d'arreter d'être appuier
        /// </summary>
        /// <param name="sender">L'objet déclencheur de l'événement</param>
        /// <param name="e">Les données de l'événement</param>
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            //Permet d'empecher de "spamer" en restant apuier sur espace
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

        /// <summary>
        /// Méthode permettant de compter les tick de 1 à 10
        /// </summary>
        private void UpdateGameLoopCounter()
        {
            if (GameLoopCounter == 10)
                GameLoopCounter = 1;
            else
                GameLoopCounter++;
        }

        /// <summary>
        /// Méthode permettant de crée le player graphique
        /// </summary>
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

            //Fait en sorte que le player soit au premié plan du canva
            Canvas.SetZIndex(newPlayer, int.MaxValue);
            myCanvas.Children.Add(newPlayer);
        }

        /// <summary>
        /// méthode permetant d'actualiser le player
        /// </summary>
        private void updatePlayer()
        {
            drawPlayer();
        }

        /// <summary>
        /// méthode permetant d'actualiser le player graphique
        /// </summary>
        private void drawPlayer()
        {
            Canvas.SetTop(newPlayer, player.posY);
            Canvas.SetLeft(newPlayer, player.posX);
        }

        /// <summary>
        /// méthode créant les bullet graphique
        /// </summary>
        /// <param name="bullet">bullet logique</param>
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

            //association des deux bullet
            BulletPair bulletPair = new BulletPair
            {
                Bullet = bullet,
                NewBullet = newBullet
            };

            bulletPairs.Add(bulletPair);
        }

        /// <summary>
        /// Actualise tout les bullets
        /// </summary>
        private void updateBullets()
        {
            foreach (BulletPair bulletPair in bulletPairs)
            {
                updateBullet(bulletPair.Bullet, bulletPair.NewBullet);
            }
        }

        /// <summary>
        /// Actualise un bullet
        /// </summary>
        /// <param name="bullet">bullet logique</param>
        /// <param name="newBullet">bullet graphique</param>
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

        /// <summary>
        /// méthode permetant d'actualiser un bullet graphique
        /// </summary>
        /// <param name="bullet">bullet logique</param>
        /// <param name="newBullet">bullet graphique</param>
        private void drawBullet(Bullet bullet, Rectangle newBullet)
        {
            Canvas.SetTop(newBullet, bullet.posY);
            Canvas.SetLeft(newBullet, bullet.posX);
        }

        /// <summary>
        /// Crée les alien logique
        /// </summary>
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

        /// <summary>
        /// Ccrée les alien graphique
        /// </summary>
        /// <param name="alienBullet">alienBullet logique</param>
        public void makeAlienBullet(Bullet alienBullet)
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

            //association des deux alienBullet
            AlienBulletPair alienBulletPair = new AlienBulletPair
            {
                AlienBullet = alienBullet,
                NewAlienBullet = newAlienBullet
            };

            alienBulletPairs.Add(alienBulletPair);
        }

        /// <summary>
        /// Actualise tout les alienBullets
        /// </summary>
        public void updateAlienBullets()
        {
            foreach (AlienBulletPair alienBulletPair in alienBulletPairs)
            {
                updateAlienBullet(alienBulletPair.AlienBullet, alienBulletPair.NewAlienBullet);
            }
        }

        /// <summary>
        /// Actualise un alienBullet
        /// </summary>
        /// <param name="alienBullet">alienBullet logique</param>
        /// <param name="newAlienBullet">alienBullet graphique</param>
        public void updateAlienBullet(Bullet alienBullet, Rectangle newAlienBullet)
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

        /// <summary>
        /// méthode permetant d'actualiser un alienBullet graphique
        /// </summary>
        /// <param name="alienBullet">alienBullet logique</param>
        /// <param name="newAlienBullet">alienBullet graphique</param>
        public void drawAlienBullet(Bullet alienBullet, Rectangle newAlienBullet)
        {
            Canvas.SetTop(newAlienBullet, alienBullet.posY);
            Canvas.SetLeft(newAlienBullet, alienBullet.posX);
        }

        /// <summary>
        /// méthode créant les alien logique
        /// </summary>
        /// <param name="ALIENNUMBER">Nombre d'alien que l'on veut crée</param>
        public void createAlien(int ALIENNUMBER)
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

        /// <summary>
        /// Crée les alien graphique
        /// </summary>
        /// <param name="alien">alien logique</param>
        public void makeAlien(Alien alien)
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

            //permet d'afficher les alien au deuxième plan
            Canvas.SetZIndex(newAlien, int.MaxValue - 1);

            myCanvas.Children.Add(newAlien);

            //association des deux alien
            AlienPair alienPair = new AlienPair
            {
                Alien = alien,
                NewAlien = newAlien
            };
            alienPairs.Add(alienPair);
        }

        /// <summary>
        /// Actualise tout les aliens
        /// </summary>
        public void updateAliens()
        {
            foreach (AlienPair alienPair in alienPairs)
            {
                updateAlien(alienPair.Alien, alienPair.NewAlien);
            }
        }

        /// <summary>
        /// Actualise un alien
        /// </summary>
        /// <param name="alien">alien logique</param>
        /// <param name="newAlien">alien graphique</param>
        public void updateAlien(Alien alien, Rectangle newAlien)
        {
            alien.move(WINDOWSWIDTH);
            drawAlien(alien, newAlien);
        }

        /// <summary>
        /// méthode permetant d'actualiser un alien graphique
        /// </summary>
        /// <param name="alien">alien logique</param>
        /// <param name="newAlien">alien graphique</param>
        public void drawAlien(Alien alien, Rectangle newAlien)
        {
            Canvas.SetTop(newAlien, alien.posY);
            Canvas.SetLeft(newAlien, alien.posX);
        }

        /// <summary>
        /// Verifie si la partie est perdu
        /// </summary>
        public void checkGameOver()
        {
            if (gameOver == true)
            {
                showGameOver();
            }
        }

        /// <summary>
        /// Ouvre la page Game Over
        /// </summary>
        public void showGameOver()
        {
            this.Visibility = Visibility.Hidden;
            float ratio;
            ratio = (float)Math.Round(((float)(STARTALIENNUMBER - aliensLeft) / (float)((STARTAMMOSTOCK + 1) - bulletLeft)), 2);
            gameOver = true;
            gameTimer.Stop();
            GameOver gameOverWindow = new GameOver();
            gameOverWindow.score = score;
            gameOverWindow.gameOverScore.Content = "Voici votre score : " + score + "\nEnnemy restant : " + aliensLeft + "\nMunition restantes : " + bulletLeft + "\nRatio (tir/mort) : " + ratio; gameOverWindow.ShowDialog();

        }

        /// <summary>
        /// Méthode vérifian si il y a une colision entre 2 rectangle
        /// </summary>
        /// <param name="X1">Position en X du rectangle n°1 (coin supérieur gauche)</param>
        /// <param name="Y1">Position en Y du rectangle n°1 (coin supérieur gauche)</param>
        /// <param name="W1">Largeur du rectangle n°1 (rectangle, vers la droite de posY)</param>
        /// <param name="H1">Hauteur du rectangle n°1 (rectangle, vers le bas de posX)</param>
        /// <param name="X2">Position en X du rectangle n°2 (coin supérieur gauche)</param>
        /// <param name="Y2">Position en Y du rectangle n°2 (coin supérieur gauche)</param>
        /// <param name="W2">Largeur du rectangle n°2 (rectangle, vers la droite de posY)</param>
        /// <param name="H2">Hauteur du rectangle n°2 (rectangle, vers le bas de posX)</param>
        /// <returns></returns>
        public static bool IsAnIntersection(int X1, int Y1, int W1, int H1, int X2, int Y2, int W2, int H2)
        {
            return !(X1 + W1 < X2 || X2 + W2 < X1 || Y1 + H1 < Y2 || Y2 + H2 < Y1);
        }

        /// <summary>
        /// Permet de géré le comportement du jeu en cas d'intersection
        /// </summary>
        /// <param name="bullets">Liste des liason entre les bullets graphique et logique</param>
        /// <param name="alienBullets">Liste des liason entre les alienBullets graphique et logique</param>
        /// <param name="aliens">Liste des liason entre les aliens graphique et logique</param>
        /// <param name="player">Player</param>
        /// <returns>Retourne si la pertie est fini</returns>
        public bool CheckIntersections(List<BulletPair> bullets, List<AlienBulletPair> alienBullets, List<AlienPair> aliens, Player player)
        {
            //C'est trois liste sont des copie des liste original afin de pouvoir parcourire et modifer ces liste en simultané (TODO parcourire à l'envers les liste afin de pouvoir les modifier)
            List<BulletPair> bulletsCopy = new List<BulletPair>(bullets);
            List<AlienBulletPair> alienBulletsCopy = new List<AlienBulletPair>(alienBullets);
            List<AlienPair> aliensCopy = new List<AlienPair>(aliens);

            //Colision des bullet
            foreach (BulletPair bulletPair in bulletsCopy)
            {   //Colision entre les bullet et les alienBullet
                foreach (AlienBulletPair alienBulletPair in alienBulletsCopy)
                {
                    if (IsAnIntersection(bulletPair.Bullet.posX, bulletPair.Bullet.posY, bulletPair.Bullet.width, bulletPair.Bullet.height,
                       alienBulletPair.AlienBullet.posX, alienBulletPair.AlienBullet.posY, alienBulletPair.AlienBullet.width, alienBulletPair.AlienBullet.height))
                    {
                        bullets.Remove(bulletPair);
                        myCanvas.Children.Remove(bulletPair.NewBullet);
                        alienBullets.Remove(alienBulletPair);
                        myCanvas.Children.Remove(alienBulletPair.NewAlienBullet);
                        score += 10;

                    }
                }

                //Colision entre les bullet et les alien
                foreach (AlienPair alienPair in aliensCopy)
                {
                    if (IsAnIntersection(bulletPair.Bullet.posX, bulletPair.Bullet.posY, bulletPair.Bullet.width, bulletPair.Bullet.height,
                        alienPair.Alien.posX, alienPair.Alien.posY, alienPair.Alien.width, alienPair.Alien.height))
                    {

                        bullets.Remove(bulletPair);
                        myCanvas.Children.Remove(bulletPair.NewBullet);
                        aliens.Remove(alienPair);
                        myCanvas.Children.Remove(alienPair.NewAlien);
                        if (aliensLeft != 0)
                        {
                            aliensLeft--;
                        }

                        if (bulletLeft - aliensLeft > 0)
                        {
                            score = score + (bulletLeft - aliensLeft) + STARTAMMOSTOCK;
                        }
                        else
                            score += 10;
                    }
                }
            }

            //Colision des alienBullet
            foreach (AlienBulletPair alienBulletPair in alienBulletsCopy)
            {
                //Colision entre les alienBullet et le player
                if (IsAnIntersection(player.posX, player.posY, player.width, player.height,
                    alienBulletPair.AlienBullet.posX, alienBulletPair.AlienBullet.posY, alienBulletPair.AlienBullet.width, alienBulletPair.AlienBullet.height))
                {
                    gameOver = true;
                    alienBullets.Remove(alienBulletPair);
                    myCanvas.Children.Remove(alienBulletPair.NewAlienBullet);
                }
            }
            //Colision des alien
            foreach (AlienPair alienPair in aliensCopy)
            {
                //Colision entre les alien et le player
                if (IsAnIntersection(player.posX, player.posY, player.width, player.height,
                    alienPair.Alien.posX, alienPair.Alien.posY, alienPair.Alien.width, alienPair.Alien.height))
                {
                    aliens.Remove(alienPair);
                    myCanvas.Children.Remove(alienPair.NewAlien);
                }
            }
            return gameOver;
        }

        /// <summary>
        /// Actualise les information présente au dessu de l'espace de jeu
        /// </summary>
        public void ScreenUpdate()
        {
            alienLeft.Content = "Alien Left : " + aliensLeft;
            bulletsLeft.Content = "Bullets Left : " + bulletLeft;
            scoreSpreader.Content = "Score : " + score;
        }

        /// <summary>
        /// Augmente la dificulter
        /// </summary>
        public void progressiveDifficulty()
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

        /// <summary>
        /// Augmente le speed des alien
        /// </summary>
        /// <param name="speed">speed actuel</param>
        /// <param name="aliens">liste des alien logique et graphique</param>
        public void increasAlienSpeed(int speed, List<AlienPair> aliens)
        {
            List<AlienPair> aliensCopy = new List<AlienPair>(aliens);

            foreach (AlienPair alienPair in aliensCopy)
            {
                alienPair.Alien.speed = speed;
            
            }
        }

        /// <summary>
        /// List qui contien l'assotiation des bullet graphique et logique
        /// </summary>
        public class BulletPair
        {
            public Bullet Bullet { get; set; }
            public Rectangle NewBullet { get; set; }
        }

        /// <summary>
        /// List qui contien l'assotiation des alienBullet graphique et logique
        /// </summary>
        public class AlienBulletPair
        {
            public Bullet AlienBullet { get; set; }
            public Rectangle NewAlienBullet { get; set; }
        }

        /// <summary>
        /// List qui contien l'assotiation des alien graphique et logique
        /// </summary>
        public class AlienPair
        {
            public Alien Alien { get; set; }
            public Rectangle NewAlien { get; set; }
        }
    }
}